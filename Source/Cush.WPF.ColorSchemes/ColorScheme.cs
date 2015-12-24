using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Cush.Common;
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
        private IKeyedResourceContainer _base;
        private IKeyedResourceContainer _theme;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ColorScheme" /> class,
        ///     using the given <see cref="IColorScheme" />.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public ColorScheme(IColorScheme colorScheme)
            : this(colorScheme.DisplayName, colorScheme.Base, colorScheme.Theme, colorScheme.Accent)
        {
            ThrowHelper.IfNullThenThrow(() => colorScheme);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ColorScheme" /> class,
        ///     using the given theme and accent.
        /// </summary>
        [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
        public ColorScheme(string displayName, IKeyedResourceContainer baseTheme, IKeyedResourceContainer theme,
            IKeyedResourceContainer accent)
        {
            ThrowHelper.IfNullThenThrow(() => baseTheme);
            ThrowHelper.IfNullThenThrow(() => theme);
            ThrowHelper.IfNullThenThrow(() => accent);
            ThrowHelper.IfNullThenThrow(() => baseTheme.Resources.Source);
            ThrowHelper.IfNullThenThrow(() => theme.Resources.Source);
            ThrowHelper.IfNullThenThrow(() => accent.Resources.Source);
            Base = baseTheme;
            Theme = theme;
            Accent = accent;

            Guid = GuidHelper.Create(GuidHelper.UrlNamespace,
                Base.Resources.Source.AbsoluteUri +
                Theme.Resources.Source.AbsoluteUri +
                Accent.Resources.Source.AbsoluteUri);

            DisplayName = displayName;
        }

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
                SetAndRaise(ref _theme, value, ThemeChanged);
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
                SetAndRaise(ref _accent, value, AccentChanged);
            }
        }


        /// <summary>
        ///     Gets or sets the value indicating the <see cref="Base" /> of the <see cref="ColorScheme" />.
        /// </summary>
        public IKeyedResourceContainer Base
        {
            get { return _base; }
            set
            {
                if (_base == value) return;
                SetAndRaise(ref _base, value, BaseChanged);
            }
        }

        /// <summary>
        ///     Called whenever the Base colorset changes.
        /// </summary>
        public event EventHandler<ChangedEventArgs<IKeyedResourceContainer>> BaseChanged;

        /// <summary>
        ///     Called whenever the Theme changes.
        /// </summary>
        public event EventHandler<ChangedEventArgs<IKeyedResourceContainer>> ThemeChanged;

        /// <summary>
        ///     Called whenever the Accent changes.
        /// </summary>
        public event EventHandler<ChangedEventArgs<IKeyedResourceContainer>> AccentChanged;

        private void SetAndRaise(ref IKeyedResourceContainer field, IKeyedResourceContainer value,
            EventHandler<ChangedEventArgs<IKeyedResourceContainer>> handler)
        {
            var args = new ChangedEventArgs<IKeyedResourceContainer>(field, value);
            field = value;
            SafeRaise.Raise(handler, this, args);
        }
    }
}