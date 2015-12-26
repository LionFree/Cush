using System.Diagnostics;
using System.Windows.Media;

// ReSharper disable once CheckNamespace

namespace Cush.WPF.ColorSchemes
{
    /// <summary>
    ///     Metadata for binding theme elements to menu items.
    ///     This class presupposes that the menu item will include the name of the color displayed next to a color swatch.
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class ThemeMenuData
    {
        /// <summary>
        ///     The DisplayName of the selected theme element.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The <see cref="Brush" /> representing the color of the border around the displayed color swatch.
        /// </summary>
        public Brush BorderColorBrush { get; set; }

        /// <summary>
        ///     The <see cref="Brush" /> representing the color of the interior of the displayed color swatch.
        /// </summary>
        public Brush ColorBrush { get; set; }
    }
}