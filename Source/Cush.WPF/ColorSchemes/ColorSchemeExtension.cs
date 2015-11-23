using System.Diagnostics;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes
{
    /// <summary>
    ///     Provides an implementation of an importable <see cref="IColorScheme" /> extension.
    /// </summary>
    [DebuggerDisplay("{DisplayName}: Theme={Theme.DisplayName}, Accent={Accent.DisplayName}")]
    public class ColorSchemeExtension : ColorScheme, IColorSchemeExtension
    {
        public ColorSchemeExtension(string displayName, IKeyedResourceContainer accent, IKeyedResourceContainer theme)
            : base(displayName, theme, accent)
        {
        }

        public virtual void OnImportsSatisfied()
        {
        }
    }
}