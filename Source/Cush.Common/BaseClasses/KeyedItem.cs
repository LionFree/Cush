﻿using System;

namespace Cush.Common
{
    public class KeyedItem : IKeyedItem
    {
        protected KeyedItem(string displayName) : this(Guid.NewGuid(), displayName)
        {
        }

        protected KeyedItem(Guid guid, string displayName)
        {
            Guid = guid;
            DisplayName = displayName;
        }

        public Guid Guid { get; }

        public string DisplayName { get; }

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
