using System.Collections.Generic;
using System.Diagnostics;

using ThrowHelper = Cush.Common.Exceptions.ThrowHelper;

namespace Cush.Common
{
    internal sealed class CollectionDebugView<T>
    {
        private readonly ICollection<T> _collection;

        public CollectionDebugView(ICollection<T> collection)
        {
            ThrowHelper.IfNullThenThrow(() => collection);
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