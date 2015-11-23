using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using Cush.Common.Attributes;

using ThrowHelper = Cush.Common.Exceptions.ThrowHelper;

namespace Cush.Common
{
    /// <summary>
    ///     Represents a strongly typed rolling list of objects that can be accessed by index,
    ///     and can be limited to a certain number of elements.
    ///     When the list is full, and a user adds an additional item, the list will discard the
    ///     first item on the list ("First-In, First-Out").
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <filterpriority>1</filterpriority>
    [DebuggerTypeProxy(typeof (CollectionDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    [__DynamicallyInvokable]
    [Serializable]
    public class BoundedList<T> : IBoundedList<T>, IList, IReadOnlyList<T>
    {
        private static readonly List<T> EmptyList = new List<T>();
        private readonly List<T> _items;
        [NonSerialized] private readonly object _syncRoot = new object();
        private int _capacity;
        private int _version;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Cush.Common.BoundedList" /> class
        ///     that is empty and has the default initial capacity.
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [__DynamicallyInvokable]
        public BoundedList() : this(EmptyList, 1)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Cush.Common.BoundedList" /> class that is empty and has the
        ///     specified initial
        ///     capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can store before trimming the excess.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity" /> is less than 1. </exception>
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [__DynamicallyInvokable]
        public BoundedList(int capacity) : this(new T[capacity], capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Cush.Common.BoundedList" /> class that contains elements copied from
        ///     the
        ///     specified collection and has sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="collection" /> is null.</exception>
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [__DynamicallyInvokable]
        public BoundedList(ICollection<T> collection) : this(collection, Math.Max(1, collection.Count()))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Cush.Common.BoundedList" /> class that contains elements copied from
        ///     the
        ///     specified collection and has sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        /// <param name="capacity">The number of elements that the new list can store before trimming the excess.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="collection" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity" /> is less than 1.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="capacity" /> is less than the length of the
        ///     given array.
        /// </exception>
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [__DynamicallyInvokable]
        public BoundedList(ICollection<T> collection, int capacity)
        {
            if (null == collection) ThrowHelper.ThrowArgumentNullException(() => collection);

            if (capacity < 1)
                ThrowHelper.ThrowArgumentOutOfRangeException(() => capacity, Strings.EXCEPTION_CapacityMustBeAtLeastOne);

            // ReSharper disable once PossibleNullReferenceException
            if (capacity < collection.Count)
                ThrowHelper.ThrowArgumentOutOfRangeException(() => capacity,
                    Strings.EXCEPTION_CapacityMustBeAtLeastAsLongAsArray);

            _items = EmptyList;

            var count = collection.Count;
            if (count > 0)
            {
                _items = new List<T>(count);
                _items.AddRange(collection);
            }

            Capacity = capacity;
        }

        /// <summary>
        ///     Gets or sets the total number of elements the <see cref="T:Cush.Common.BoundedList" /> will hold.
        /// </summary>
        /// <returns>
        ///     The number of elements that the <see cref="T:Cush.Common.BoundedList" /> will contain.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <see cref="P:Cush.Common.BoundedList.Capacity" /> is set to a value that is less than
        ///     <see cref="P:Cush.Common.BoundedList.Count" />.
        /// </exception>
        /// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
        [__DynamicallyInvokable]
        public int Capacity
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"),
             __DynamicallyInvokable] get
            {
                return _capacity;
            }

            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"),
             __DynamicallyInvokable]
            set
            {
                if (_capacity == value) return;
                _items.Capacity = value;
                _capacity = value;
            }
        }

        /// <summary>
        ///     Gets the number of elements actually contained in the <see cref="T:Cush.Common.BoundedList" />.
        /// </summary>
        /// <returns>
        ///     The number of elements actually contained in the <see cref="T:Cush.Common.BoundedList" />.
        /// </returns>
        [__DynamicallyInvokable]
        public int Count
        {
            [__DynamicallyInvokable,
             TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
            {
                return _items.Count;
            }
        }

        [__DynamicallyInvokable]
        bool ICollection<T>.IsReadOnly
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"),
             __DynamicallyInvokable] get
            {
                return false;
            }
        }

        /// <summary>
        ///     Gets or sets the element at the specified index.
        /// </summary>
        /// <returns>
        ///     The element at the specified index.
        /// </returns>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is less than 0
        ///     -or- <paramref name="index" /> is equal to or greater than <see cref="P:Cush.Common.BoundedList.Count" />.
        /// </exception>
        [__DynamicallyInvokable]
        public T this[int index]
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"),
             __DynamicallyInvokable] get
            {
                return _items[index];
            }

            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"),
             __DynamicallyInvokable]
            set
            {
                _items[index] = value;
                ++_version;
            }
        }

        [__DynamicallyInvokable]
        void ICollection<T>.Add(T item)
        {
            Add(item);
            ++_version;
        }

        /// <summary>
        ///     Removes the first occurrance of a specific object from the <see cref="T:Cush.Common.BoundedList" />.
        /// </summary>
        /// <param name="item">
        ///     The object to remove from the <see cref="T:Cush.Common.BoundedList" />.
        ///     The value can be null for reference types.
        /// </param>
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [__DynamicallyInvokable]
        public bool Remove(T item)
        {
            ThrowHelper.IfNullThenThrow(() => item);

            if (!_items.Contains(item)) return false;
            _items.Remove(item);
            return true;
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [__DynamicallyInvokable]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [__DynamicallyInvokable]
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        ///     Determines whether an element is in the <see cref="T:Cush.Common.BoundedList" />.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="item" /> is found in the <see cref="T:Cush.Common.BoundedList" />; otherwise, false.
        /// </returns>
        /// <param name="item">
        ///     The object to locate in the <see cref="T:Cush.Common.BoundedList" />.
        ///     The value can be null for reference types.
        /// </param>
        [__DynamicallyInvokable]
        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        /// <summary>
        ///     Copies the entire <see cref="T:BoundedList" /> to a compatible one-dimensional array, starting at the specified
        ///     index of the target array.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied
        ///     from <see cref="T:BoundedList" />. The <see cref="T:System.Array" /> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> is less than 0.</exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The number of elements in the source <see cref="T:BoundedList" /> is
        ///     greater than the available space from <paramref name="arrayIndex" /> to the end of the destination
        ///     <paramref name="array" />.
        /// </exception>
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [__DynamicallyInvokable]
        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Searches for the specified object and returns the zero-based index of the first occurrence within the entire
        ///     <see cref="T:Cush.Common.BoundedList" />.
        /// </summary>
        /// <returns>
        ///     The zero-based index of the first occurrence of <paramref name="item" /> within the entire
        ///     <see cref="T:Cush.Common.BoundedList" />, if found; otherwise, –1.
        /// </returns>
        /// <param name="item">
        ///     The object to locate in the <see cref="T:Cush.Common.BoundedList" />.
        ///     The value can be null for reference types.
        /// </param>
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [__DynamicallyInvokable]
        public int IndexOf(T item)
        {
            return _items.IndexOf(item);
        }

        /// <summary>
        ///     Removes all elements from the <see cref="T:BoundedList" />.
        /// </summary>
        [__DynamicallyInvokable]
        public void Clear()
        {
            lock (_syncRoot)
            {
                _items.Clear();
            }
            ++_version;
        }

        /// <summary>
        ///     Copies the elements of the <see cref="T:Cush.Common.BoundedList" /> to a new array.
        /// </summary>
        /// <returns>
        ///     An array containing copies of the elements of the <see cref="T:Cush.Common.BoundedList" />.
        /// </returns>
        [__DynamicallyInvokable]
        public T[] ToArray()
        {
            return _items.ToArray();
        }

        /// <summary>
        ///     Inserts an element into the <see cref="T:Cush.Common.BoundedList" /> at the specified index.
        ///     If this causes the count to exceed the capacity, the item will be inserted, and then the
        ///     first item in the list will be removed.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is less than 0.-or-<paramref name="index" />
        ///     is greater than <see cref="P:Cush.Common.BoundedList.Count" />.
        /// </exception>
        [__DynamicallyInvokable]
        public void Insert(int index, T item)
        {
            var capacity = _items.Capacity;

            lock (_syncRoot)
            {
                if (_items.Count >= capacity)
                    _items.Capacity++;


                _items.Insert(index, item);

                if (_items.Count >= capacity)
                {
                    _items.RemoveAt(0);
                    _items.Capacity = capacity;
                }
            }

            ++_version;
        }

        /// <summary>
        ///     Removes the element at the specified index of the <see cref="T:BoundedList" />.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is less than 0. -or-
        ///     <paramref name="index" /> is equal to or greater than <see cref="P:BoundedList.Count" />.
        /// </exception>
        [__DynamicallyInvokable]
        public void RemoveAt(int index)
        {
            lock (_syncRoot)
            {
                _items.RemoveAt(index);
                ++_version;
            }
        }

        [__DynamicallyInvokable]
        void IList.Insert(int index, object item)
        {
            ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, "item");

            try
            {
                Insert(index, (T) item);
            }
            catch (InvalidCastException)
            {
                ThrowHelper.ThrowWrongValueTypeArgumentException(() => item, typeof (T));
            }
        }

        [__DynamicallyInvokable]
        int IList.Add(object item)
        {
            ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, "item");
            try
            {
                Add((T) item);
            }
            catch (InvalidCastException) { ThrowHelper.ThrowWrongValueTypeArgumentException(() => item, typeof (T)); }
            return Count - 1;
        }

        [__DynamicallyInvokable]
        bool IList.IsFixedSize
        {
            [__DynamicallyInvokable] get { return false; }
        }

        [__DynamicallyInvokable]
        bool IList.IsReadOnly
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"),
             __DynamicallyInvokable] get
            {
                return false;
            }
        }

        [__DynamicallyInvokable]
        bool ICollection.IsSynchronized
        {
            [__DynamicallyInvokable] get { return false; }
        }

        [__DynamicallyInvokable]
        object ICollection.SyncRoot
        {
            [__DynamicallyInvokable]
            get
            {
                //if (_syncRoot == null)
                //    Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);
                return _syncRoot;
            }
        }

        [__DynamicallyInvokable]
        object IList.this[int index]
        {
            [__DynamicallyInvokable] get { return this[index]; }

            [__DynamicallyInvokable]
            set
            {
                ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, "value");

                try
                {
                    this[index] = (T) value;
                }
                catch (InvalidCastException) {ThrowHelper.ThrowWrongValueTypeArgumentException(() => value, typeof (T));} 
            }
        }

        [__DynamicallyInvokable]
        void IList.Remove(object item)
        {
            if (!IsCompatibleObject(item)) return;
            Remove((T) item);
        }

        [__DynamicallyInvokable]
        bool IList.Contains(object item)
        {
            return IsCompatibleObject(item) && Contains((T) item);
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [__DynamicallyInvokable]
        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection) _items).CopyTo(array, index);
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [__DynamicallyInvokable]
        int IList.IndexOf(object value)
        {
            return ((IList) _items).IndexOf(value);
        }

        /// <summary>
        ///     Adds an item to the <see cref="T:Cush.Common.BoundedList" />.
        /// </summary>
        /// <returns>
        ///     The position into which the new element was inserted.
        /// </returns>
        /// <param name="item">
        ///     The object to add to the <see cref="T:item" />.
        /// </param>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:item" /> is read-only.-or- The <see cref="T:item" /> has a fixed size.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        [__DynamicallyInvokable]
        public int Add(T item)
        {
            ThrowHelper.IfNullThenThrow(() => item);

            lock (_syncRoot)
            {
                if (_items.Contains(item)) return _items.IndexOf(item);
                _items.Add(item);

                if (_capacity > 0 && _items.Count > _capacity)
                    _items.RemoveAt(0);
            }

            ++_version;

            return _items.Count - 1;
        }

        /// <summary>
        ///     Returns a read-only <see cref="T:System.Collections.Generic.IList`1" /> wrapper for the current collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> that acts as a read-only wrapper around the
        ///     current <see cref="T:Cush.Common.BoundedList" />.
        /// </returns>
        public ReadOnlyCollection<T> AsReadOnly()
        {
            return new ReadOnlyCollection<T>(this);
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [__DynamicallyInvokable]
        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        ///     Searches for the specified object and returns the zero-based index of the first occurrence within the range of
        ///     elements in the <see cref="T:Cush.Common.BoundedList" /> that extends from the specified index to the
        ///     last element.
        /// </summary>
        /// <returns>
        ///     The zero-based index of the first occurrence of <paramref name="item" /> within the range of elements in the
        ///     <see cref="T:Cush.Common.BoundedList" /> that extends from <paramref name="index" /> to the last element,
        ///     if found; otherwise, –1.
        /// </returns>
        /// <param name="item">
        ///     The object to locate in the <see cref="T:Cush.Common.BoundedList" />. The value can be null for
        ///     reference types.
        /// </param>
        /// <param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is outside the range of valid indexes
        ///     for the <see cref="T:Cush.Common.BoundedList" />.
        /// </exception>
        [__DynamicallyInvokable]
        public int IndexOf(T item, int index)
        {
            if (index >= _capacity) ThrowHelper.ThrowArgumentOutOfRangeException(() => index);
            return _items.IndexOf(item, index);
        }

        /// <summary>
        ///     Searches for the specified object and returns the zero-based index of the first occurrence within the range of
        ///     elements in the <see cref="T:Cush.Common.BoundedList" /> that starts at the specified index and contains the
        ///     specified number of elements.
        /// </summary>
        /// <returns>
        ///     The zero-based index of the first occurrence of <paramref name="item" /> within the range of elements in the
        ///     <see cref="T:Cush.Common.BoundedList" /> that starts at <paramref name="index" /> and contains
        ///     <paramref name="count" /> number of elements, if found; otherwise, –1.
        /// </returns>
        /// <param name="item">
        ///     The object to locate in the <see cref="T:Cush.Common.BoundedList" />. The value can be null for
        ///     reference types.
        /// </param>
        /// <param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is outside the range of valid indexes
        ///     for the <see cref="T:Cush.Common.BoundedList" />.-or-<paramref name="count" /> is less than 0.-or-
        ///     <paramref name="index" /> and <paramref name="count" /> do not specify a valid section in the
        ///     <see cref="T:Cush.Common.BoundedList" />.
        /// </exception>
        [__DynamicallyInvokable]
        public int IndexOf(T item, int index, int count)
        {
            return _items.IndexOf(item, index, count);
        }

        /// <summary>
        ///     Adds the elements of the specified collection to the end of the <see cref="T:Cush.Common.BoundedList" />.
        /// </summary>
        /// <param name="collection">
        ///     The collection whose elements should be added to the end of the <see cref="T:Cush.Common.BoundedList" />.
        ///     The collection itself cannot be null, but it can contain elements that are null, if type <typeparamref name="T" />
        ///     is a
        ///     reference type.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="collection" /> is null.</exception>
        [__DynamicallyInvokable]
        public void AddRange(IEnumerable<T> collection)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(collection, "collection");

            lock (_syncRoot)
            {
                foreach (var item in collection)
                {
                    Add(item);
                }
            }
        }

        /// <summary>
        ///     Sorts the elements in a range of elements in <see cref="T:Cush.Common.BoundedList" /> using the specified comparer.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range to sort.</param>
        /// <param name="count">The length of the range to sort.</param>
        /// <param name="comparer">
        ///     The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing
        ///     elements, or null to use the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default" />.
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is less than 0.-or-
        ///     <paramref name="count" /> is less than 0.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="index" /> and <paramref name="count" /> do not specify a
        ///     valid range in the <see cref="T:Cush.Common.BoundedList" />.-or-The implementation of <paramref name="comparer" />
        ///     caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an
        ///     item with itself.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     <paramref name="comparer" /> is null, and the default comparer
        ///     <see cref="P:System.Collections.Generic.Comparer`1.Default" /> cannot find implementation of the
        ///     <see cref="T:System.IComparable`1" /> generic interface or the <see cref="T:System.IComparable" /> interface for
        ///     type <typeparamref name="T" />.
        /// </exception>
        [__DynamicallyInvokable]
        public void Sort(int index, int count, IComparer<T> comparer)
        {
            _items.Sort(index, count, comparer);
            ++_version;
        }

        /// <summary>
        ///     Sorts the elements in the entire <see cref="T:Cush.Common.BoundedList" /> using the specified
        ///     <see cref="T:System.Comparison`1" />.
        /// </summary>
        /// <param name="comparison">The <see cref="T:System.Comparison`1" /> to use when comparing elements.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="comparison" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The implementation of <paramref name="comparison" /> caused an error
        ///     during the sort. For example, <paramref name="comparison" /> might not return 0 when comparing an item with itself.
        /// </exception>
        [__DynamicallyInvokable]
        public void Sort(Comparison<T> comparison)
        {
            _items.Sort(comparison);
        }

        /// <summary>
        ///     Sorts the elements in the entire <see cref="T:Cush.Common.BoundedList" /> using the specified comparer.
        /// </summary>
        /// <param name="comparer">
        ///     The <see cref="T:Cush.Common.BoundedList" /> implementation to use when comparing elements, or
        ///     null to use the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default" />.
        /// </param>
        /// <exception cref="T:System.InvalidOperationException">
        ///     <paramref name="comparer" /> is null, and the default comparer
        ///     <see cref="P:System.Collections.Generic.Comparer`1.Default" /> cannot find implementation of the
        ///     <see cref="T:System.IComparable`1" /> generic interface or the <see cref="T:System.IComparable" /> interface for
        ///     type <typeparamref name="T" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The implementation of <paramref name="comparer" /> caused an error during
        ///     the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.
        /// </exception>
        [__DynamicallyInvokable]
        public void Sort(IComparer<T> comparer)
        {
            Sort(0, Count, comparer);
        }

        /// <summary>
        ///     Sorts the elements in the entire <see cref="T:Cush.Common.BoundedList" /> using the default comparer.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The default comparer
        ///     <see cref="P:System.Collections.Generic.Comparer`1.Default" /> cannot find an implementation of the
        ///     <see cref="T:System.IComparable`1" /> generic interface or the <see cref="T:System.IComparable" /> interface for
        ///     type <typeparamref name="T" />.
        /// </exception>
        [__DynamicallyInvokable]
        public void Sort()
        {
            Sort(0, Count, null);
        }

        /// <summary>
        ///     Creates a <see cref="T:System.Collections.Generic.List`1" /> from a <see cref="T:Cush.Common.BoundedList" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.List`1" /> that contains elements from the
        ///     <see cref="T:Cush.Common.BoundedList" />.
        /// </returns>
        [__DynamicallyInvokable]
        public List<T> ToList()
        {
            return new List<T>(_items);
        }

        private static bool IsCompatibleObject(object value)
        {
            // Non-null values are fine.  Only accept nulls if T is a class or Nullable<U>.
            // Note that default(T) is not equal to null for value types except when T is Nullable<U>. 
            return ((value is T) || (value == null && default(T) == null));
        }

        public struct Enumerator : IEnumerator<T>
        {
            private readonly BoundedList<T> _list;
            private readonly int _version;
            private T _current;
            private int _index;

            public Enumerator(BoundedList<T> list)
            {
                _list = list;
                _index = 0;
                _version = list._version;
                _current = default(T);
            }

            /// <summary>
            ///     Gets the element at the current position of the enumerator.
            /// </summary>
            /// <returns>
            ///     The element in the <see cref="T:Cush.Common.BoundedList" /> at the current position of the enumerator.
            /// </returns>
            [__DynamicallyInvokable]
            public T Current
            {
                [__DynamicallyInvokable,
                 TargetedPatchingOptOut(
                     "Performance critical to inline this type of method across NGen image boundaries")]
                get
                {
                    return _current;
                }
            }

            [__DynamicallyInvokable]
            object IEnumerator.Current
            {
                [__DynamicallyInvokable]
                get
                {
                    if (_index == 0 || _index == _list.Count + 1)
                        ThrowHelper.ThrowInvalidOperationException(Strings.EXCEPTION_EnumerationOperationCannotHappen);
                    return Current;
                }
            }

            /// <summary>
            ///     Releases all resources used by the <see cref="T:Cush.Common.BoundedList.Enumerator" />.
            /// </summary>
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
            [__DynamicallyInvokable]
            public void Dispose()
            {
            }

            /// <summary>
            ///     Advances the enumerator to the next element of the <see cref="T:Cush.Common.BoundedList" />.
            /// </summary>
            /// <returns>
            ///     true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of
            ///     the collection.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
            [__DynamicallyInvokable]
            public bool MoveNext()
            {
                var list = _list;
                if (_version != list._version || (uint) _index >= (uint) list.Count)
                    return MoveNextRare();
                _current = list._items[_index];
                ++_index;
                return true;
            }

            [__DynamicallyInvokable]
            void IEnumerator.Reset()
            {
                if (_version != _list._version)
                    ThrowHelper.ThrowInvalidOperationException(Strings.EXCEPTION_EnumerationFailedVersion);
                _index = 0;
                _current = default(T);
            }

            private bool MoveNextRare()
            {
                if (_version != _list._version)
                    ThrowHelper.ThrowInvalidOperationException(Strings.EXCEPTION_EnumerationFailedVersion);
                _index = _list.Count + 1;
                _current = default(T);
                return false;
            }
        }
    }
}