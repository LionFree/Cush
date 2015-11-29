using System;

namespace Cush.Common
{
    public class KeyedItem : IKeyedItem
    {
        private readonly string _displayName;
        private readonly Guid _guid;

        protected KeyedItem(Guid guid, string displayName)
        {
            _guid = guid;
            _displayName = displayName;
        }

        public Guid Guid
        {
            get { return _guid; }
        }

        public string DisplayName
        {
            get { return _displayName; }
        }

        public override bool Equals(object obj)
        {
            // If parameter is null or cannot be cast to KeyedItem, then return false.
            var p = obj as KeyedItem;
            if (p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Guid == p.Guid);
        }

        public bool Equals(KeyedItem p)
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
