using System.Collections.Generic;
using System.Diagnostics;
using Cush.Common.Exceptions;


namespace Cush.WPF
{
    internal sealed class CollectionDebugView<T>
    {
        private readonly ICollection<T> _collection;

        public CollectionDebugView(ICollection<T> collection)
        {
            if (collection == null)
                ThrowHelper.ThrowArgumentNullException("collection");
            _collection = collection;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get
            {
                var array = new T[_collection.Count];
                _collection.CopyTo(array, 0);
                return array;
            }
        }
    }
}