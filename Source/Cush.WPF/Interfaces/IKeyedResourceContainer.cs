using System.Windows;
using Cush.Common;

namespace Cush.WPF.Interfaces
{
    /// <summary>
    ///     Provides the interface for a globally-unique <see cref="ResourceDictionary" /> container.
    /// </summary>
    public interface IKeyedResourceContainer : IResourceContainer, IKeyedItem
    {
    }
}