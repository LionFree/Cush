using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
//using System.Windows;
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
        ///     using the given <see cref="P:DisplayName" /> and <see cref="System.Windows.ResourceDictionary" /> source URI.
        /// </summary>
        /// <param name="displayName">The display name of the associated resources.</param>
        /// <param name="resourceUri">The source URI of the <see cref="System.Windows.ResourceDictionary" /> to store.</param>
        [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
        public ResourceExtension(string displayName, string resourceUri)
            : base(displayName, resourceUri)
        {
        }

        public virtual void OnImportsSatisfied()
        {
        }
    }
}