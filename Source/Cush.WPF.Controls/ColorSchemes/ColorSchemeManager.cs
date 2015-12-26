using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using Cush.Common;
using Cush.Common.Annotations;
using Cush.Common.Exceptions;
using Cush.Common.Logging;
using Cush.Composition;
using Cush.WPF.Controls.ColorSchemes;
using Cush.WPF.Interfaces;

// ReSharper disable once CheckNamespace

namespace Cush.WPF.ColorSchemes
{
    [DebuggerDisplay("Theme={_currentScheme.Theme.DisplayName}, Accent={_currentScheme.Accent.DisplayName}")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public sealed class ColorSchemeManager : ResourceContainer
    {
        public delegate void ColorSchemeChangedEventHandler(object sender, ChangedEventArgs<IColorScheme> e);

        private static IKeyedResourceContainer _baseTheme;
        private static IColorSchemeExtensionContainer _container;
        private static IColorScheme _currentScheme;
        private static readonly ThreadSafeObservableCollection<ISchemedElement> Elements;

        static ColorSchemeManager()
        {
            Elements = new ThreadSafeObservableCollection<ISchemedElement>();
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
        public static IEnumerable<IKeyedResourceContainer> Accents => _container.Accents;

        /// <summary>
        ///     The loaded themes.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static IEnumerable<IKeyedResourceContainer> Themes => _container.Themes;

        /// <summary>
        ///     The current color scheme, which will be pushed to all registered elements.
        /// </summary>
        public static IColorScheme CurrentScheme
        {
            get { return _currentScheme; }
            private set
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
        ///     Loads any <see cref="ColorScheme" />s from local assemblies,
        ///     and registers them with the <see cref="T:ColorSchemeManager" />.
        ///     CALL THIS (OR DO IT MANUALLY) BEFORE CREATING ANY WINDOWS.
        /// </summary>
        /// <param name="logger">The <see cref="T:ILogger" /> to use when logging progress and error messages.</param>
        public static void ComposeColorSchemeExtensions(ILogger logger)
        {
            var composer = new ColorSchemeComposer(logger);
            try
            {
                using (var collector = new ImportCollector(logger))
                {
                    collector.ImportParts();
                    composer.ComposeImports(collector);
                }
                PopulateSchemes(composer.Container);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        /// <summary>
        ///     Takes an <see cref="IColorSchemeExtensionContainer " /> and populates the <see cref="ColorSchemeManager" />
        ///     with the Accents, Themes, and default scheme.
        /// </summary>
        public static void PopulateSchemes(IColorSchemeExtensionContainer container)
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
            Elements.Add(element);
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
        ///     Lists the <see cref="ISchemedElement" />s that are registered in the <see cref="ColorSchemeManager" />.
        ///     Adds a <see paramref="comment" /> before the list, if one is given.
        ///     The comment is useful for identifying the point in code where the list is generated.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static void ListRegisteredElements(string comment = "")
        {
            Trace.WriteLine(Environment.NewLine + $"{comment}");
            foreach (var item in Elements)
            {
                Trace.WriteLine($"  {item}");
            }
        }


        /// <summary>
        ///     Unregisters a given <see cref="ISchemedElement" /> from the <see cref="ColorSchemeManager" />.
        /// </summary>
        /// <param name="element">The <see cref="ISchemedElement" /> to unregister.</param>
        public static void Release(ISchemedElement element)
        {
            if (element?.ColorScheme == null) return;
            if (!Elements.Contains(element)) return;
            //foreach (var item in Elements.Where(item => item == element))
            //{
            //    DisconnectEventHandlersFromElement(item);
            //}
            Elements.Remove(element);
        }

        // TODO: Make this an IDisposable and do dispose instead
        public static void Clear()
        {
            //foreach (var item in Elements)
            //{
            //    DisconnectEventHandlersFromElement(item);
            //}
            Elements.Clear();
        }


        /// <summary>
        ///     Sets the current (managed) color scheme, which will be pushed to all registered <see cref="ISchemedElement" />s.
        /// </summary>
        /// <param name="scheme">The scheme to set.</param>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static void SetManagedColorScheme(IColorScheme scheme)
        {
            ThrowHelper.IfNullThenThrow(() => scheme);
            SetManagedColorScheme(scheme.Theme, scheme.Accent);
        }

        /// <summary>
        ///     Sets the current (managed) color scheme, which will be pushed to all registered <see cref="ISchemedElement" />s.
        /// </summary>
        /// <param name="theme">The theme to set.</param>
        /// <param name="accent">The accent to set.</param>
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
        /// <param name="themeName">The theme to set.</param>
        public static void SetManagedTheme(string themeName)
        {
            ThrowHelper.IfNullOrEmptyThenThrow(() => themeName);
            var newValue = Themes.FirstOrDefault(t => t.DisplayName == themeName);
            if (newValue == null) return;
            SetManagedTheme(newValue);
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
        /// <param name="accentName">The theme to set.</param>
        public static void SetManagedAccent(string accentName)
        {
            ThrowHelper.IfNullOrEmptyThenThrow(() => accentName);
            var newValue = Accents.FirstOrDefault(t => t.DisplayName == accentName);
            if (newValue == null) return;
            SetManagedAccent(newValue);
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

        #region privates

        /// <summary>
        ///     Updates the color scheme of the given element by pushing resources into the element's
        ///     <see cref="ResourceDictionary" />.
        /// </summary>
        /// <param name="element"></param>
        private static void PushResourcesIntoElement(ISchemedElement element)
        {
            ThrowHelper.IfNullThenThrow(() => element);
            if (element.ColorScheme == _currentScheme) return;

            ReplaceElementResourcesWithManagedScheme(element);

            // ReSharper disable once SuspiciousTypeConversion.Global
            var uiElement = element as UIElement;
            uiElement?.InvalidateVisual();

            element.ColorScheme = CurrentScheme;
        }

        private static void ReplaceElementResourcesWithManagedScheme(ISchemedElement element)
        {
            if (element.ColorScheme == _currentScheme) return;
            if (element.ColorScheme == null)
            {
                AddManagedSchemeToElementResources(element);
            }
            else
            {
                ReplaceResources(element.Resources, _baseTheme);
                ReplaceResources(element.Resources, element.ColorScheme.Accent, CurrentScheme.Accent);
                ReplaceResources(element.Resources, element.ColorScheme.Theme, CurrentScheme.Theme);
            }
            element.ColorScheme = (ColorScheme) CurrentScheme;
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


        private static void OnManagedResourcesChanged(object sender, ChangedEventArgs<IKeyedResourceContainer> e)
        {
            if (Elements.Count == 0) return;

            //Trace.WriteLine($"scheme changed: replacing resources in {Elements.Count} elements.");
            //ListRegisteredElements("Elements:");

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
            _currentScheme.BaseChanged -= OnManagedResourcesChanged;
            _currentScheme.AccentChanged -= OnManagedResourcesChanged;
            _currentScheme.ThemeChanged -= OnManagedResourcesChanged;
        }

        private static void ConnectEventHandlersToCurrentScheme()
        {
            if (_currentScheme == null) return;
            _currentScheme.BaseChanged += OnManagedResourcesChanged;
            _currentScheme.AccentChanged += OnManagedResourcesChanged;
            _currentScheme.ThemeChanged += OnManagedResourcesChanged;
        }

        #endregion
    }
}