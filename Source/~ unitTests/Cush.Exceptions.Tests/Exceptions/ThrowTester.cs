using System;
using System.Collections;
using Cush.Common.Exceptions;

namespace Cush.Exceptions.Tests.Exceptions
{
    internal class ThrowTester<T> : IList
    {
        private const int PrivateCount = 0;
        private const bool PrivateIsFixedSize = false;
        private const bool PrivateIsReadOnly = false;
        private const bool PrivateIsSynchronized = false;
        private readonly object _syncRoot = new object();

        int IList.Add(object value)
        {
            ThrowHelper.IfNullAndNullsAreIllegalThenThrow(() => value, typeof (T));
            return 0;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return PrivateCount; }
        }

        public object SyncRoot
        {
            get { return _syncRoot; }
        }

        public bool IsSynchronized
        {
            get { return PrivateIsSynchronized; }
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public object this[int index]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { return PrivateIsReadOnly; }
        }

        public bool IsFixedSize
        {
            get { return PrivateIsFixedSize; }
        }

        public void Test_Name(object item)
        {
            ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, "item");
        }
    }
}