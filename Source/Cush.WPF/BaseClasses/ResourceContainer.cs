using System;
using System.Diagnostics;
using System.Windows;
using Cush.Common.Helpers;
using Cush.WPF.Interfaces;

namespace Cush.WPF
{
    /// <summary>
    ///     A container wrapping a WPF <see cref="ResourceDictionary" />.
    /// </summary>
    [DebuggerDisplay("{DisplayName}: Count={Resources.Count}")]
    public class ResourceContainer : IKeyedResourceContainer
    {
        private readonly string _displayName;
        private readonly Guid _guid;

        /// <summary>
        ///     Gets or sets the value indicating the <see cref="ResourceDictionary" /> of the <see cref="ResourceContainer" />.
        /// </summary>
        public ResourceDictionary Resources { get; set; }

        /// <summary>
        ///     Gets the globally-unique identifier of the <see cref="ResourceContainer" />.
        /// </summary>
        public Guid Guid
        {
            get { return _guid; }
        }

        /// <summary>
        ///     Gets the value indicating the display name of the <see cref="ResourceContainer" />.
        /// </summary>
        public string DisplayName
        {
            get { return _displayName; }
        }
        
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
            _displayName = name;
            
            Resources = string.IsNullOrEmpty(resourceAddress) 
                ? new ResourceDictionary() 
                : new ResourceDictionary { Source = new Uri(resourceAddress) };
            

            _guid = GuidHelper.Create(GuidHelper.UrlNamespace, resourceAddress);
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