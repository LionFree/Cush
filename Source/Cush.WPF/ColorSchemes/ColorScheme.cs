using System;
using System.Diagnostics;
using Cush.Common.Exceptions;
using Cush.Common.Helpers;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes
{
    /// <summary>
    ///     Represents the background theme of the application.
    /// </summary>
    [DebuggerDisplay("Theme={Theme.DisplayName}, Accent={Accent.DisplayName}")]
    public class ColorScheme : IColorScheme
    {
        private IKeyedResourceContainer _accent;
        private IKeyedResourceContainer _theme;

        /// <summary>
        ///     Gets the globally-unique identifier of the <see cref="ColorScheme" />.
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        ///     Gets the value indicating the <see cref="DisplayName" /> of the <see cref="ColorScheme" />.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        ///     Gets or sets the value indicating the <see cref="Theme" /> of the <see cref="ColorScheme" />.
        /// </summary>
        public IKeyedResourceContainer Theme
        {
            get { return _theme; }
            set
            {
                if (_theme == value) return;
                var args = new ChangedEventArgs<IKeyedResourceContainer>(_theme, value);
                _theme = value;
                RaiseEvent(ThemeChanged, args);
            }
        }

        /// <summary>
        ///     Gets or sets the value indicating the <see cref="Accent" /> of the <see cref="ColorScheme" />.
        /// </summary>
        public IKeyedResourceContainer Accent
        {
            get { return _accent; }
            set
            {
                if (_accent == value) return;
                var args = new ChangedEventArgs<IKeyedResourceContainer>(_accent, value);
                _accent = value;
                RaiseEvent(AccentChanged, args);
            }
        }

        public event EventHandler<ChangedEventArgs<IKeyedResourceContainer>> ThemeChanged;
        public event EventHandler<ChangedEventArgs<IKeyedResourceContainer>> AccentChanged;

        private void RaiseEvent<T>(EventHandler<T> handler, T e)
        {
            handler?.Invoke(this, e);
        }

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ColorScheme" /> class,
        ///     using the default values.
        /// </summary>
        public ColorScheme() : this(string.Empty, new ResourceContainer(), new ResourceContainer())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ColorScheme" /> class,
        ///     using the given <see cref="IColorScheme" />.
        /// </summary>
        public ColorScheme(IColorScheme colorScheme)
            : this(colorScheme.DisplayName, colorScheme.Theme, colorScheme.Accent)
        {
            ThrowHelper.IfNullThenThrow(() => colorScheme);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ColorScheme" /> class,
        ///     using the given theme and accent.
        /// </summary>
        public ColorScheme(string displayName, IKeyedResourceContainer theme, IKeyedResourceContainer accent)
        {
            ThrowHelper.IfNullThenThrow(() => theme);
            ThrowHelper.IfNullThenThrow(() => accent);
            ThrowHelper.IfNullThenThrow(() => theme.Resources.Source);
            ThrowHelper.IfNullThenThrow(() => accent.Resources.Source);
            Theme = theme;
            Accent = accent;

            Guid = GuidHelper.Create(GuidHelper.UrlNamespace,
                Theme.Resources.Source.AbsoluteUri +
                Accent.Resources.Source.AbsoluteUri);

            DisplayName = displayName;
        }

        #endregion
    }
}