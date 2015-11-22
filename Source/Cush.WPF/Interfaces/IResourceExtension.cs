using System.ComponentModel.Composition;

namespace Cush.WPF.Interfaces
{
    /// <summary>
    ///     Provides the interface for an importable <see cref="IResourceContainer"/> extension.
    /// </summary>
    public interface IResourceExtension : IKeyedResourceContainer, IPartImportsSatisfiedNotification
    {
    }
}