using System;
using System.Diagnostics;
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
        private readonly string _displayName;
        private readonly Guid _guid;

        /// <summary>
        ///     Gets the globally-unique identifier of the <see cref="ColorScheme" />.
        /// </summary>
        public Guid Guid
        {
            get { return _guid; }
        }

        /// <summary>
        ///     Gets the value indicating the <see cref="DisplayName" /> of the <see cref="ColorScheme" />.
        /// </summary>
        public string DisplayName
        {
            get { return _displayName; }
        }

        /// <summary>
        ///     Gets or sets the value indicating the <see cref="Theme" /> of the <see cref="ColorScheme" />.
        /// </summary>
        public IKeyedResourceContainer Theme { get; set; }

        /// <summary>
        ///     Gets or sets the value indicating the <see cref="Accent" /> of the <see cref="ColorScheme" />.
        /// </summary>
        public IKeyedResourceContainer Accent { get; set; }
        
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
            if (null == colorScheme) throw new ArgumentNullException("colorScheme");
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ColorScheme" /> class,
        ///     using the given theme and accent.
        /// </summary>
        public ColorScheme(string displayName, IKeyedResourceContainer theme, IKeyedResourceContainer accent)
        {
            if (null == theme) throw new ArgumentNullException("theme");
            if (null == accent) throw new ArgumentNullException("accent");
            Theme = theme;
            Accent = accent;
            _guid = GuidHelper.Create(GuidHelper.UrlNamespace,
                Theme.Resources.Source.AbsoluteUri +
                Accent.Resources.Source.AbsoluteUri);
                
            _displayName = displayName;
        }

        #endregion
    }
}