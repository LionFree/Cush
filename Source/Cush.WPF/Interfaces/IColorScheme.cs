using Cush.Common;

namespace Cush.WPF.Interfaces
{
    /// <summary>
    ///     Provides the interface for a generalized container of color scheme resources.
    /// </summary>
    public interface IColorScheme : IKeyedItem
    {
        /// <summary>
        ///     Gets the the default Theme.
        /// </summary>
        IKeyedResourceContainer Theme { get; set; }

        /// <summary>
        ///     Gets the the default Accent.
        /// </summary>
        IKeyedResourceContainer Accent { get; set; }
    }
}