using System.Diagnostics;
using Cush.WPF.Interfaces;
// ReSharper disable MemberCanBeProtected.Global

namespace Cush.WPF.ColorSchemes
{
    /// <summary>
    ///     Provides an implementation of an importable <see cref="IColorScheme" /> extension.
    /// </summary>
    [DebuggerDisplay("{DisplayName}: Theme={Theme.DisplayName}, Accent={Accent.DisplayName}")]
    public class ColorSchemeExtension : ColorScheme, IColorSchemeExtension
    {
        public ColorSchemeExtension(string displayName, IKeyedResourceContainer baseColors, IKeyedResourceContainer theme, IKeyedResourceContainer accent)
            : base(displayName, baseColors, theme, accent)
        {
        }

        public virtual void OnImportsSatisfied()
        {
        }
    }
}