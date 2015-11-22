using System.Windows;

namespace Cush.WPF.Interfaces
{
    /// <summary>
    ///     Provides the interface for a <see cref="ResourceDictionary"/> container.
    /// </summary>
    public interface IResourceContainer
    {
        /// <summary>
        ///     Gets the resource dictionary.
        /// </summary>
        ResourceDictionary Resources { get; }
    }
}