using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using Cush.Common.Attributes;

namespace Cush.Common
{
    public interface IItemsCollection : INotifyPropertyChanged, INotifyCollectionChanged, ICollection, IEnumerable
    {
        /// <summary>
        ///     Gets or sets the element at the specified index.
        /// </summary>
        /// <returns>
        ///     The element at the specified index.
        /// </returns>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is not a valid index in the
        ///     <see cref="T:Cush.Common.IItemsCollection" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     The property is set and the
        ///     <see cref="T:Cush.Common.IItemsCollection" /> is read-only.
        /// </exception>
        [__DynamicallyInvokable]
        object this[int index] { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e);

        /// <summary>
        ///     Adds an item to the <see cref="T:Cush.Common.IItemsCollection" />.
        /// </summary>
        /// <returns>
        ///     The position into which the new element was inserted, or -1 to indicate that the item was not inserted into the
        ///     collection,
        /// </returns>
        /// <param name="value">The object to add to the <see cref="T:Cush.Common.IItemsCollection" />. </param>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:Cush.Common.IItemsCollection" /> is read-only.-or- The
        ///     <see cref="T:Cush.Common.IItemsCollection" /> has a fixed size.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        [__DynamicallyInvokable]
        int Add(object value);

        /// <summary>
        ///     Determines whether the <see cref="T:Cush.Common.IItemsCollection" /> contains a specific value.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="T:System.Object" /> is found in the <see cref="T:Cush.Common.IItemsCollection" />;
        ///     otherwise,
        ///     false.
        /// </returns>
        /// <param name="value">The object to locate in the <see cref="T:Cush.Common.IItemsCollection" />. </param>
        /// <filterpriority>2</filterpriority>
        [__DynamicallyInvokable]
        bool Contains(object value);

        /// <summary>
        ///     Removes all items from the <see cref="T:Cush.Common.IItemsCollection" />.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:Cush.Common.IItemsCollection" /> is read-only. </exception>
        /// <filterpriority>2</filterpriority>
        [__DynamicallyInvokable]
        void Clear();

        /// <summary>
        ///     Inserts an item to the <see cref="T:Cush.Common.IItemsCollection" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted. </param>
        /// <param name="value">The object to insert into the <see cref="T:Cush.Common.IItemsCollection" />. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is not a valid index in the
        ///     <see cref="T:Cush.Common.IItemsCollection" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:Cush.Common.IItemsCollection" /> is read-only.-or- The
        ///     <see cref="T:Cush.Common.IItemsCollection" /> has a fixed size.
        /// </exception>
        /// <exception cref="T:System.NullReferenceException">
        ///     <paramref name="value" /> is null reference in the
        ///     <see cref="T:Cush.Common.IItemsCollection" />.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        [__DynamicallyInvokable]
        void Insert(int index, object value);

        /// <summary>
        ///     Removes the first occurrence of a specific object from the <see cref="T:Cush.Common.IItemsCollection" />.
        /// </summary>
        /// <param name="value">The object to remove from the <see cref="T:Cush.Common.IItemsCollection" />. </param>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:Cush.Common.IItemsCollection" /> is read-only.-or- The
        ///     <see cref="T:Cush.Common.IItemsCollection" /> has a fixed size.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        [__DynamicallyInvokable]
        void Remove(object value);

        /// <summary>
        ///     Removes the <see cref="T:Cush.Common.IItemsCollection" /> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is not a valid index in the
        ///     <see cref="T:Cush.Common.IItemsCollection" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:Cush.Common.IItemsCollection" /> is read-only.-or- The
        ///     <see cref="T:Cush.Common.IItemsCollection" /> has a fixed size.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        [__DynamicallyInvokable]
        void RemoveAt(int index);

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:Cush.Common.IItemsCollection"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The index of <paramref name="value"/> if found in the list; otherwise, -1.
        /// </returns>
        /// <param name="value">The object to locate in the <see cref="T:Cush.Common.IItemsCollection"/>. </param><filterpriority>2</filterpriority>
        [__DynamicallyInvokable]
        int IndexOf(object value);
    }
}