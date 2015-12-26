using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Cush.WPF.Controls
{
    public sealed class MRUListSection : ConfigurationSection
    {
        #region Constructors

        /// <summary>
        ///     Predefines the valid properties and prepares
        ///     the property collection.
        /// </summary>
        static MRUListSection()
        {
            // Predefine properties here
            SPropMRUList = new ConfigurationProperty(
                "MRUListValue",
                typeof (MRUEntryCollection),
                null,
                ConfigurationPropertyOptions.IsRequired);


            SProperties = new ConfigurationPropertyCollection {SPropMRUList};
        }

        #endregion

        #region Static Fields

        private static string _spath;
        private static MRUListSection _instance;
        private static readonly MRUListSection DefaultInstance = new MRUListSection();

        private static MRUListSection _mSection;

        private static readonly ConfigurationProperty SPropMRUList;

        private static readonly ConfigurationPropertyCollection SProperties;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the MRUListValue setting.
        /// </summary>
        [ConfigurationProperty("MRUListValue", IsRequired = true)]
        public MRUEntryCollection MRUList
        {
            get { return (MRUEntryCollection) base[SPropMRUList]; }
            set { base[SPropMRUList] = value; }
        }


        /// <summary>
        ///     Override the Properties collection and return our custom one.
        /// </summary>
        protected override ConfigurationPropertyCollection Properties
        {
            get { return SProperties; }
        }

        #endregion

        #region Pattern

        public static MRUListSection Default
        {
            get { return DefaultInstance; }
        }

        /// <summary>
        ///     Get this configuration set from the application's default config file
        /// </summary>
        public static MRUListSection Open()
        {
            var assy =
                Assembly.GetEntryAssembly();
            return Open(assy.Location);
        }

        /// <summary>
        ///     Get this configuration set from a specific config file
        /// </summary>
        public static MRUListSection Open(string path)
        {
            if (_instance == null)
            {
                _spath = path.EndsWith(".config",
                    StringComparison.InvariantCultureIgnoreCase)
                    ? path.Remove(path.Length - 7)
                    : path;

                var config =
                    ConfigurationManager.OpenExeConfiguration(path);
                if (config.Sections["MRUList"] == null)
                {
                    _instance = new MRUListSection();
                    config.Sections.Add("MRUList", _instance);
                    config.Save(ConfigurationSaveMode.Modified);
                }
                else
                    _instance = (MRUListSection) config.Sections["MRUList"];
            }
            return _instance;
        }

        /// <summary>
        ///     Save the current property values to the config file
        /// </summary>
        public void Save()
        {
            // The Configuration has to be opened anew each time we want to 
            // update the file contents. Otherwise, the update of other custom 
            // configuration sections will cause an exception to occur when we 
            // try to save our modifications, stating that another app has 
            // modified the file since we opened it.
            var config = ConfigurationManager.OpenExeConfiguration(_spath);
            var section = (MRUListSection) config.Sections["MRUList"];
            //
            // TODO: Add code to copy all properties from "this" to "section"
            //
            section.MRUList = MRUList;
            config.Save(ConfigurationSaveMode.Modified);
        }

        /// <summary>
        ///     Create a full copy of the current properties
        /// </summary>
        public MRUListSection Copy()
        {
            var copy = new MRUListSection();
            var xml = SerializeSection(this,
                "MRUList1", ConfigurationSaveMode.Full);
            XmlReader rdr =
                new XmlTextReader(new StringReader(xml));
            copy.DeserializeSection(rdr);
            return copy;
        }


        public static MRUListSection GetSection()
        {
            return GetSection("MRUList");
        }

        public static MRUListSection GetSection(string definedName)
        {
            if (_mSection == null)
            {
                _mSection = ConfigurationManager.GetSection(definedName) as MRUListSection;
                if (_mSection == null)
                    throw new ConfigurationErrorsException("The <" + definedName +
                                                           "> section is not defined in your .config file!");
            }

            return _mSection;
        }

        #endregion
    }
}