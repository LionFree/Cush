using System;
using System.Xml.Serialization;

namespace Cush.Common
{
    [Serializable]
    public class ObservableKeyedItem : BindableBase, IKeyedItem
    {
        protected ObservableKeyedItem(string displayName)
        {
            Guid = Guid.NewGuid();
            DisplayName = displayName;
        }

        public Guid Guid { get; }

        private string _displayName;

        [XmlAttribute("DisplayName")]
        public string DisplayName
        {
            get { return _displayName; }
            set { SetProperty(ref _displayName, value, nameof(DisplayName)); }
        }

        public override bool Equals(object obj)
        {
            // If parameter is null or cannot be cast to this type, then return false.
            var p = obj as ObservableKeyedItem;
            if (p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Guid == p.Guid);
        }

        public bool Equals(IKeyedItem p)
        {
            // If parameter is null return false:
            if (p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Guid == p.Guid);
        }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }
    }
}