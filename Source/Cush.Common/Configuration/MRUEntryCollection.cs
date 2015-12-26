using System.Configuration;

namespace Cush.WPF.Controls
{
    [ConfigurationCollection(typeof(MRUEntryElement),
        CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
    public class MRUEntryCollection : ConfigurationElementCollection
    {
        #region Constructors

        static MRUEntryCollection()
        {
            MProperties = new ConfigurationPropertyCollection();
        }

        #endregion

        #region Fields

        private static readonly ConfigurationPropertyCollection MProperties;

        #endregion

        #region Properties

        protected override ConfigurationPropertyCollection Properties
        {
            get { return MProperties; }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        #endregion

        #region Indexers

        public MRUEntryElement this[int index]
        {
            get { return (MRUEntryElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                base.BaseAdd(index, value);
            }
        }

        public new MRUEntryElement this[string name]
        {
            get { return (MRUEntryElement)BaseGet(name); }
        }

        #endregion

        #region Overrides

        protected override ConfigurationElement CreateNewElement()
        {
            return new MRUEntryElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var mruEntryElement = element as MRUEntryElement;

            return mruEntryElement != null ? mruEntryElement.FullPath : string.Empty;
        }

        #endregion

        #region Methods

        public void Add(MRUEntryElement thing)
        {
            base.BaseAdd(thing);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Remove(MRUEntryElement thing)
        {
            BaseRemove(GetElementKey(thing));
        }

        public void Clear()
        {
            BaseClear();
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public string GetKey(int index)
        {
            return (string)BaseGetKey(index);
        }

        #endregion
    }
}