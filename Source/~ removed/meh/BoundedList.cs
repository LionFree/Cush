using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Cush.Extensibility
{
    /// <summary>
    ///     Represents a strongly typed rolling list of objects that can be accessed by index,
    ///     and can be limited to a certain number of elements.
    ///     When the list is full, and a user adds an additional item, the list will discard the
    ///     first item on the list (the "bottom of the stack").
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <filterpriority>1</filterpriority>
    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
    public class BoundedList<T>
    {
        protected readonly List<T> Items = new List<T>();
        private int _capacity;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:BoundedList" /> class
        ///     that is empty and has the default initial capacity.
        /// </summary>
        public BoundedList() : this(0)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:BoundedList" /> class that is empty and has the specified initial
        ///     capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity" /> is less than 0. </exception>
        public BoundedList(int capacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException("capacity");
            _capacity = capacity;
        }

        /// <summary>
        ///     Gets the number of elements actually contained in the <see cref="T:BoundedList" />.
        /// </summary>
        /// <returns>
        ///     The number of elements actually contained in the <see cref="T:BoundedList" />.
        /// </returns>
        public int Count
        {
            get { return Items.Count; }
        }

        /// <summary>
        ///     Gets or sets the total number of elements the internal data structure can hold before resizing.
        /// </summary>
        /// <returns>
        ///     The number of elements that the <see cref="T:BoundedList" /> can contain before resizing is required.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <see cref="P:BoundedList.Capacity" /> is set to a value that is less than <see cref="P:BoundedList.Count" />.
        /// </exception>
        /// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (_capacity == value) return;
                Items.Capacity = _capacity;
                _capacity = value;
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
        ///     -or- <paramref name="index" /> is equal to or greater than <see cref="P:BoundedList.Count" />.
        /// </exception>
        public T this[int index]
        {
            get { return Items[index]; }
            set { Items[index] = value; }
        }

        /// <summary>
        ///     Gets a value indicating whether the <see cref="T:BoundedList" /> is read-only.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="T:BoundedList" /> is read-only; otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        ///     Determines whether an element is in the <see cref="T:BoundedList" />.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="item" /> is found in the <see cref="T:BoundedList" />; otherwise, false.
        /// </returns>
        /// <param name="item">
        ///     The object to locate in the <see cref="T:BoundedList" />.
        ///     The value can be null for reference types.
        /// </param>
        public bool Contains(T item)
        {
            return Items.Contains(item);
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
        public void CopyTo(T[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Adds an item to the <see cref="T:BoundedList" />.
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
        public int Add(T item)
        {
            if (item == null) throw new ArgumentException("item");

            if (Items.Contains(item)) return Items.IndexOf(item);
            Items.Add(item);

            if (_capacity > 0 && Items.Count > _capacity)
                Items.RemoveAt(0);

            return Items.Count - 1;
        }

        /// <summary>
        ///     Searches for the specified object and returns the zero-based index of the first occurrence within the entire
        ///     <see cref="T:BoundedList" />.
        /// </summary>
        /// <returns>
        ///     The zero-based index of the first occurrence of <paramref name="item" /> within the entire
        ///     <see cref="T:BoundedList" />, if found; otherwise, –1.
        /// </returns>
        /// <param name="item">
        ///     The object to locate in the <see cref="T:BoundedList" />.
        ///     The value can be null for reference types.
        /// </param>
        public int IndexOf(T item)
        {
            return Items.IndexOf(item);
        }

        /// <summary>
        ///     Removes the first occurrance of a specific object from the <see cref="T:BoundedList" />.
        /// </summary>
        /// <param name="item">
        ///     The object to remove from the <see cref="T:BoundedList" />.
        ///     The value can be null for reference types.
        /// </param>
        public bool Remove(T item)
        {
            if (item == null) throw new ArgumentException("item");

            if (!Items.Contains(item)) return false;
            Items.Remove(item);
            return true;
        }

        /// <summary>
        ///     Removes all elements from the <see cref="T:BoundedList" />.
        /// </summary>
        public void Clear()
        {
            Items.Clear();
        }

        /// <summary>
        ///     Copies the elements of the <see cref="T:BoundedList" /> to a new array.
        /// </summary>
        /// <returns>
        ///     An array containing copies of the elements of the <see cref="T:BoundedList" />.
        /// </returns>
        public T[] ToArray()
        {
            return Items.ToArray();
        }

        /// <summary>
        ///     Inserts an element into the <see cref="T:BoundedList" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is less than 0.-or-<paramref name="index" />
        ///     is greater than <see cref="P:BoundedList.Count" />.
        /// </exception>
        public void Insert(int index, T item)
        {
            Items.Insert(index, item);
        }

        /// <summary>
        ///     Removes the element at the specified index of the <see cref="T:BoundedList" />.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is less than 0. -or-
        ///     <paramref name="index" /> is equal to or greater than <see cref="P:BoundedList.Count" />.
        /// </exception>
        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }
    }
}