using System.Diagnostics;
using System.Windows;
using Cush.WPF.Interfaces;

namespace Cush.WPF
{
    /// <summary>
    ///     Provides an implementation of an importable <see cref="IResourceContainer" /> extension.
    /// </summary>
    [DebuggerDisplay("{DisplayName}: URI={Resources.Source}")]
    public class ResourceExtension : ResourceContainer, IResourceExtension
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="ResourceExtension" /> class
        ///     using the given <see cref="P:DisplayName" /> and <see cref="ResourceDictionary" /> source URI.
        /// </summary>
        /// <param name="displayName">The display name of the associated resources.</param>
        /// <param name="resourceUri">The source URI of the <see cref="ResourceDictionary" /> to store.</param>
        public ResourceExtension(string displayName, string resourceUri)
            : base(displayName, resourceUri)
        {
        }

        public virtual void OnImportsSatisfied()
        {
        }
    }
}