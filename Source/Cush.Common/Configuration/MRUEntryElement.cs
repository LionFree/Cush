using System.Configuration;
using Cush.Common.FileHandling;

namespace Cush.Common.Configuration
{
    public class MRUEntryElement : ConfigurationElement
    {
        private const string PathProperty = "fullPath";
        private const string PinnedProperty = "pinned";
        private const string TagProperty = "tag";

        public MRUEntryElement() : this(string.Empty, false, string.Empty)
        {}

        public MRUEntryElement(MRUEntry entry):this(entry.FullPath, entry.Pinned, entry.Tag) { }

        public MRUEntryElement(string fullPath, bool pinned, string tag)
        {
            FullPath = fullPath;
            Pinned = pinned;
            Tag = tag;
        }

        /// <summary>
        /// Gets the FullPath setting.
        /// </summary>
        [ConfigurationProperty(PathProperty, IsRequired = true)]
        public string FullPath
        {
            get { return (string)this[PathProperty]; }
            set { this[PathProperty] = value; }
        }

        /// <summary>
        /// Gets the Pinned setting.
        /// </summary>
        [ConfigurationProperty(PinnedProperty, IsRequired = false, DefaultValue = false)]
        public bool Pinned
        {
            get { return (bool)this[PinnedProperty]; }
            set { this[PinnedProperty] = value; }
        }

        /// <summary>
        /// Gets the Tag setting.
        /// </summary>
        [ConfigurationProperty(TagProperty, IsRequired = true)]
        public string Tag
        {
            get { return (string)this[TagProperty]; }
            set { this[TagProperty] = value; }
        }
    }
}
