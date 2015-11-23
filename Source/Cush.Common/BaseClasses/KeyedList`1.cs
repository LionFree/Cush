using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Cush.Common.Attributes;

using ThrowHelper = Cush.Common.Exceptions.ThrowHelper;

namespace Cush.Common
{
    /// <summary>
    ///     Represents a generic collection of named values.
    /// </summary>
    /// <typeparam name="T">The type of values in the collection.</typeparam>
    /// <filterpriority>1</filterpriority>
    [DataContract, __DynamicallyInvokable, Serializable]
    public sealed class KeyedList<T> : IEnumerable
    {
        private readonly Dictionary<string, T> _funcs = new Dictionary<string, T>();

        /// <summary>
        ///     Gets a <see cref="T:Dictionary" /> containing the names and values of the <see cref="T:KeyedList" />.
        ///     Note: item names are case-insensitive.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Dictionary" /> containing the names and values of the <see cref="T:KeyedList" />.
        /// </returns>
        [__DynamicallyInvokable]
        public Dictionary<string, T> Items
        {
            [__DynamicallyInvokable]
            get { return _funcs; }
        }

        /// <summary>
        ///     Gets or sets the value associated with the specified name.
        /// </summary>
        /// <param name="key">The name of the item to set or get.</param>
        /// <returns>
        ///     The value associated with the specified name.
        ///     If the specified key is not found, a get operation throws a
        ///     <see cref="T:System.Collections.Generic.KeyNotFoundException" />,
        ///     and a set operation creates a new element with the specified key.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="key" /> is null.</exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">
        ///     The property is retrieved and
        ///     <paramref name="key" /> does not exist in the collection.
        /// </exception>
        [__DynamicallyInvokable]
        public T this[string key]
        {
            [__DynamicallyInvokable]
            get
            {
                var upperKey = key.ToUpper();
                if (_funcs.ContainsKey(upperKey))
                {
                    return _funcs[upperKey];
                }
                throw new KeyNotFoundException("Key not found: " + key);
            }
            [__DynamicallyInvokable]
            set
            {
                var upperKey = key.ToUpper();
                if (!_funcs.ContainsKey(upperKey))
                {
                    _funcs.Add(upperKey, value);
                }
                else
                {
                    _funcs[upperKey] = value;
                }
            }
        }

        /// <summary>
        ///     Gets the number of elements contained in the <see cref="T:KeyedList" />.
        /// </summary>
        /// <returns>
        ///     The number of elements contained in the <see cref="T:KeyedList" />.
        ///     Retrieving the value of this property is an O(1) operation.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        [__DynamicallyInvokable]
        public int Count
        {
            [__DynamicallyInvokable]
            get { return _funcs.Count; }
        }

        [__DynamicallyInvokable]
        public IEnumerator GetEnumerator()
        {
            return _funcs.GetEnumerator();
        }

        [__DynamicallyInvokable]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Determines whether an item is in the <see cref="T:KeyedList" />.
        /// </summary>
        /// <param name="key">The name of the item to locate in the <see cref="T:KeyedList" />.</param>
        [__DynamicallyInvokable]
        public bool Contains(string key)
        {
            return _funcs.Any(item => item.Key == key.ToUpper());
        }

        /// <summary>
        ///     Adds an item to the <see cref="T:KeyedList" />.
        /// </summary>
        /// <param name="key">The name of the item to add.</param>
        /// <param name="value">The item to add to the list.</param>
        /// <returns>The item added to the <see cref="T:KeyedList" />.</returns>
        [__DynamicallyInvokable]
        public T Add(string key, T value)
        {
            ThrowHelper.IfNullOrEmptyThenThrow(() => key, Strings.EXCEPTION_KeyCannotBeNull);

            var upperKey = key.ToUpper();
            if (_funcs.ContainsKey(upperKey))
            {
                _funcs[upperKey] = value;
            }
            else
            {
                _funcs.Add(upperKey, value);
            }

            return this[upperKey];
        }

        /// <summary>
        ///     Removes an item from the <see cref="T:KeyedList" />.
        /// </summary>
        /// <param name="key">The name of the item to remove.</param>
        [__DynamicallyInvokable]
        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key)) return;
            var upperKey = key.ToUpper();
            if (!_funcs.ContainsKey(upperKey)) return;
            _funcs.Remove(upperKey);
        }

        /// <summary>
        ///     Removes all items from the <see cref="T:KeyedList" />.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:KeyedList" /> is read-only. </exception>
        /// <filterpriority>2</filterpriority>
        [__DynamicallyInvokable]
        public void Clear()
        {
            _funcs.Clear();
        }
    }
}