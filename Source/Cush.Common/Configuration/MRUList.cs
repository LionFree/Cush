using System;
using System.Collections;
using System.Collections.Generic;
using Cush.Common.FileHandling;
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Cush.Common.Configuration
{
    [Serializable]
    public class MRUList
    {
        private readonly ArrayList _mrus;

        public MRUList():this(new List<MRUEntry>())
        {
        }

        public MRUList(IEnumerable<MRUEntry> entries)
        {
            _mrus = new ArrayList();
            foreach (var item in entries)
            {
                Add(item);
            }
        }
        
        public IEnumerator GetEnumerator()
        {
            return _mrus.GetEnumerator();
        }

        public int Count => _mrus.Count;

        public void Add(MRUEntry entry)
        {
            _mrus.Add(new DictionaryEntry(entry.FullPath, entry));
        }

        public void Remove(MRUEntry entry)
        {
            var de = new DictionaryEntry(entry.FullPath, entry);
            if (_mrus.Contains(de))
            {
                _mrus.Remove(de);
            }
        }

        public void Clear()
        {
            _mrus.Clear();
        }
    }
}