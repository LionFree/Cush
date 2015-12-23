using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using Cush.Common.Annotations;
using Cush.Common.Exceptions;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes
{
    [DebuggerDisplay("Theme={_currentScheme.Theme.DisplayName}, Accent={_currentScheme.Accent.DisplayName}")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class ColorSchemeManager : ResourceContainer
    {
        public delegate void ColorSchemeChangedEventHandler(object sender, ChangedEventArgs<IColorScheme> e);

        private static IKeyedResourceContainer _baseTheme;
        private static IColorSchemeExtensionContainer _container;
        private static IColorScheme _currentScheme;
        private static readonly ThreadSafeObservableCollection<ISchemedElement> Elements;

        static ColorSchemeManager()
        {
            Elements = new ThreadSafeObservableCollection<ISchemedElement>();
            //CurrentScheme = new ColorScheme();
        }

        /// <summary>
        ///     The base theme.
        /// </summary>
        [SuppressMessage("ReSharper", "ConvertToAutoPropertyWithPrivateSetter")]
        public static IKeyedResourceContainer BaseTheme => _baseTheme;

        /// <summary>
        ///     The loaded accents.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static List<IKeyedResourceContainer> Accents => _container.Accents;

        /// <summary>
        ///     The loaded themes.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static List<IKeyedResourceContainer> Themes => _container.Themes;

        /// <summary>
        ///     The current color scheme, which will be pushed to all registered elements.
        /// </summary>
        public static IColorScheme CurrentScheme
        {
            get { return _currentScheme; }
            protected set
            {
                if (_currentScheme == value) return;
                DisconnectEventHandlersFromCurrentScheme();
                var oldScheme = _currentScheme;
                _currentScheme = value;
                OnColorSchemeChanged(oldScheme);
                ConnectEventHandlersToCurrentScheme();
            }
        }


        /// <summary>
        ///     Takes an <see cref="IColorSchemeExtensionContainer " /> and populates the <see cref="ColorSchemeManager" />
        ///     with the Accents, Themes, and default scheme.
        /// </summary>
        public static void RegisterSchemes(IColorSchemeExtensionContainer container)
        {
            _container = container;
            CurrentScheme = container.DefaultScheme;
            _baseTheme = container.BaseTheme;
        }

        /// <summary>
        ///     Registers a given <see cref="ISchemedElement" /> with the <see cref="ColorSchemeManager" />.
        /// </summary>
        /// <param name="element">The <see cref="ISchemedElement" /> to register.</param>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static void Register(ISchemedElement element)
        {
            if (element == null) return;
            if (Elements.Contains(element)) return;
            PushResourcesIntoElement(element);
            ConnectEventHandlersToElement(element);
            Elements.Add(element);
        }

        private static void ConnectEventHandlersToElement(ISchemedElement element)
        {
            element.CurrentScheme.AccentChanged += PullSchemeItemFromElement;
            element.CurrentScheme.ThemeChanged += PullSchemeItemFromElement;
            element.SchemeChanged += PullSchemeFromElement;
        }

        private static void DisconnectEventHandlersFromElement(ISchemedElement element)
        {
            element.CurrentScheme.AccentChanged -= PullSchemeItemFromElement;
            element.CurrentScheme.ThemeChanged -= PullSchemeItemFromElement;
            element.SchemeChanged -= PullSchemeFromElement;
        }

        /// <summary>
        ///     Lists the <see cref="ResourceDictionary" />s in an <see cref="ISchemedElement" />.
        ///     Adds a <see paramref="comment" /> before the list, if one is given.
        ///     The comment is useful for identifying the point in code where resources are added to the given
        ///     <see cref="ISchemedElement" />.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static void ListDictionaries(ISchemedElement element, string comment = "")
        {
            Trace.WriteLine(Environment.NewLine + $"{comment}");
            Trace.WriteLine($"{element},  Dictionaries: {element.Resources.MergedDictionaries.Count}");
            foreach (var item in element.Resources.MergedDictionaries)
            {
                Trace.WriteLine($"  {item.Source}");
            }
        }

        /// <summary>
        ///     Unregisters a given <see cref="ISchemedElement" /> from the <see cref="ColorSchemeManager" />.
        /// </summary>
        /// <param name="element">The <see cref="ISchemedElement" /> to unregister.</param>
        public static void Release(ISchemedElement element)
        {
            if (element?.CurrentScheme == null) return;
            if (!Elements.Contains(element)) return;
            foreach (var item in Elements.Where(item => item == element))
            {
                DisconnectEventHandlersFromElement(item);
            }
            Elements.Remove(element);
        }


        // TODO: Make this an IDisposable and do dispose instead
        public static void Clear()
        {
            foreach (var item in Elements)
            {
                DisconnectEventHandlersFromElement(item);
            }
            Elements.Clear();
        }

        // When an element's accent or theme changes, update the current scheme and all the elements to match.
        private static void PullSchemeItemFromElement(object sender, ChangedEventArgs<IKeyedResourceContainer> args)
        {
            Trace.WriteLine("Accent or Theme changed on an ISchemedElement.");
        }

        // When an element's scheme changes, update the current scheme and all the elements to match.
        private static void PullSchemeFromElement(object sender, ChangedEventArgs<IColorScheme> args)
        {
            Trace.WriteLine("ColorScheme changed on an ISchemedElement.");
        }

        /// <summary>
        ///     Updates the color scheme of the given element by pushing resources into the element's
        ///     <see cref="ResourceDictionary" />.
        /// </summary>
        /// <param name="element"></param>
        public static void PushResourcesIntoElement(ISchemedElement element)
        {
            ThrowHelper.IfNullThenThrow(() => element);
            if (element.CurrentScheme == _currentScheme) return;

            ReplaceElementResourcesWithManagedScheme(element);
            element.CurrentScheme = CurrentScheme;
        }

        private static void ReplaceElementResourcesWithManagedScheme(ISchemedElement element)
        {
            if (element.CurrentScheme == _currentScheme) return;
            if (element.CurrentScheme == null)
            {
                AddManagedSchemeToElementResources(element);
            }
            else
            {
                ReplaceResources(element.Resources, _baseTheme);
                ReplaceResources(element.Resources, element.CurrentScheme.Accent, CurrentScheme.Accent);
                ReplaceResources(element.Resources, element.CurrentScheme.Theme, CurrentScheme.Theme);
            }
            element.CurrentScheme = (ColorScheme) CurrentScheme;
        }

        private static void AddManagedSchemeToElementResources(ISchemedElement element)
        {
            element.Resources.MergedDictionaries.Add(_baseTheme.Resources);
            element.Resources.MergedDictionaries.Add(CurrentScheme.Accent.Resources);
            element.Resources.MergedDictionaries.Add(CurrentScheme.Theme.Resources);
        }

        private static void ReplaceResources(ResourceDictionary resources, IKeyedResourceContainer newResources)
        {
            var oldThemeResources = resources.MergedDictionaries
                .FirstOrDefault(d => d.Source == resources.Source);
            SwapResources(resources, oldThemeResources, newResources.Resources);
        }

        private static void ReplaceResources(ResourceDictionary resources, IKeyedResourceContainer oldResources,
            IKeyedResourceContainer newResources)
        {
            if (oldResources == null || oldResources.Guid == newResources.Guid) return;
            var oldThemeResources = resources.MergedDictionaries
                .FirstOrDefault(d => d.Source == oldResources.Resources.Source);
            SwapResources(resources, oldThemeResources, newResources.Resources);
        }

        private static void SwapResources([NotNull] ResourceDictionary resources,
            [NotNull] ResourceDictionary oldResources,
            [NotNull] ResourceDictionary newResources)
        {
            resources.MergedDictionaries.Remove(oldResources);
            resources.MergedDictionaries.Add(newResources);
        }

        /// <summary>
        ///     Sets the current (managed) color scheme, which will be pushed to all registered <see cref="ISchemedElement" />s.
        /// </summary>
        /// <param name="theme">The theme to register.</param>
        /// <param name="accent">The accent to register.</param>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static void SetManagedColorScheme(IKeyedResourceContainer theme, IKeyedResourceContainer accent)
        {
            ThrowHelper.IfNullThenThrow(() => theme);
            ThrowHelper.IfNullThenThrow(() => accent);

            SetManagedAccent(accent);
            SetManagedTheme(theme);
        }

        /// <summary>
        ///     Sets the current (managed) theme, which will be pushed to all registered <see cref="ISchemedElement" />s.
        /// </summary>
        /// <param name="newValue">The theme to register.</param>
        public static void SetManagedTheme(IKeyedResourceContainer newValue)
        {
            ThrowHelper.IfNullThenThrow(() => newValue);
            if (_currentScheme.Theme.Guid == newValue.Guid) return;

            CurrentScheme.Theme = newValue;
        }

        /// <summary>
        ///     Sets the current (managed) accent, which will be pushed to all registered <see cref="ISchemedElement" />s.
        /// </summary>
        /// <param name="newValue">The accent to register.</param>
        public static void SetManagedAccent(IKeyedResourceContainer newValue)
        {
            ThrowHelper.IfNullThenThrow(() => newValue);
            if (_currentScheme.Accent.Guid == newValue.Guid) return;

            CurrentScheme.Accent = newValue;
        }

        private static void OnManagedResourcesChanged(object sender, ChangedEventArgs<IKeyedResourceContainer> e)
        {
            Trace.WriteLine("Resources changed.  Pushing resources to elements.");

            foreach (var item in Elements)
            {
                ReplaceResources(item.Resources, e.OldItem, e.NewItem);
            }
        }

        private static void OnColorSchemeChanged(IColorScheme oldScheme)
        {
            OnManagedResourcesChanged(null,
                new ChangedEventArgs<IKeyedResourceContainer>(oldScheme?.Accent, CurrentScheme.Accent));
            OnManagedResourcesChanged(null,
                new ChangedEventArgs<IKeyedResourceContainer>(oldScheme?.Theme, CurrentScheme.Theme));
        }

        private static void DisconnectEventHandlersFromCurrentScheme()
        {
            if (_currentScheme == null) return;
            _currentScheme.AccentChanged -= OnManagedResourcesChanged;
            _currentScheme.ThemeChanged -= OnManagedResourcesChanged;
        }

        private static void ConnectEventHandlersToCurrentScheme()
        {
            if (_currentScheme == null) return;
            _currentScheme.AccentChanged += OnManagedResourcesChanged;
            _currentScheme.ThemeChanged += OnManagedResourcesChanged;
        }
    }
}