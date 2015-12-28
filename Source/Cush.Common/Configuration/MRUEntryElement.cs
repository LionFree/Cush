using System.Configuration;

namespace Cush.Common.Configuration
{
    public class MRUEntryElement : ConfigurationElement
    {
        #region Constructors

        /// <summary>
        /// Predefines the valid properties and prepares
        /// the property collection.
        /// </summary>
        static MRUEntryElement()
        {
            // Predefine properties here
            SPropFullPath = new ConfigurationProperty("fullPath", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

            SPropPinned = new ConfigurationProperty("pinned", typeof(bool), false, ConfigurationPropertyOptions.IsRequired);

            SProperties = new ConfigurationPropertyCollection { SPropFullPath, SPropPinned };
        }

        #endregion


        #region Static Fields
        private static readonly ConfigurationProperty SPropFullPath;
        private static readonly ConfigurationProperty SPropPinned;

        private static readonly ConfigurationPropertyCollection SProperties;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Name setting.
        /// </summary>
        [ConfigurationProperty("fullPath", IsRequired = true)]
        public string FullPath
        {
            get { return (string)base[SPropFullPath]; }
            set { base[SPropFullPath] = value; }
        }

        /// <summary>
        /// Gets the Type setting.
        /// </summary>
        [ConfigurationProperty("pinned")]
        public bool Pinned
        {
            get { return (bool)base[SPropPinned]; }
            set { base[SPropPinned] = value; }
        }


        /// <summary>
        /// Override the Properties collection and return our custom one.
        /// </summary>
        protected override ConfigurationPropertyCollection Properties
        {
            get { return SProperties; }
        }
        #endregion


    }
}
