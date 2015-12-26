using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Cush.Common.Helpers;
using Cush.WPF.Interfaces;

namespace Cush.WPF
{
    /// <summary>
    ///     A container wrapping a WPF <see cref="ResourceDictionary" />.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
    [DebuggerDisplay("{DisplayName}: Count={Resources.Count}")]
    public class ResourceContainer : IKeyedResourceContainer
    {
        /// <summary>
        ///     Gets or sets the value indicating the <see cref="ResourceDictionary" /> of the <see cref="ResourceContainer" />.
        /// </summary>
        public ResourceDictionary Resources { get; set; }

        /// <summary>
        ///     Gets the globally-unique identifier of the <see cref="ResourceContainer" />.
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        ///     Gets the value indicating the display name of the <see cref="ResourceContainer" />.
        /// </summary>
        public string DisplayName { get; set; }

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResourceContainer" /> class.
        /// </summary>
        public ResourceContainer()
            : this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResourceContainer" /> class
        ///     using the given name and resource address URI.
        /// </summary>
        /// <param name="name">The name of the new ResourceContainer.</param>
        /// <param name="resourceAddress">The URI of the ResourceDictionary.</param>
        public ResourceContainer(string name, string resourceAddress)
        {
            if (null == resourceAddress) throw new ArgumentNullException(nameof(resourceAddress));
            if (null == name) throw new ArgumentNullException(nameof(name));
            DisplayName = name;
            
            Resources = string.IsNullOrEmpty(resourceAddress) 
                ? new ResourceDictionary() 
                : new ResourceDictionary { Source = new Uri(resourceAddress) };
            

            Guid = GuidHelper.Create(GuidHelper.UrlNamespace, resourceAddress);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResourceContainer" /> class
        ///     using the given name and <see cref="IResourceContainer" />.
        /// </summary>
        /// <param name="name">The name of the new ResourceContainer.</param>
        /// <param name="container">The <see cref="IResourceContainer" /> to wrap.</param>
        public ResourceContainer(string name, IResourceContainer container)
            : this(name, container.Resources.Source.AbsoluteUri)
        {
            if (null == container) throw new ArgumentNullException(nameof(container));
        }
        
        #endregion
    }
}