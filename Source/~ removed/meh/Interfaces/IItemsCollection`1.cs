using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;
using Cush.Common.Attributes;

namespace meh.Interfaces
{
    [__DynamicallyInvokable]
    public interface IItemsCollection<T> : ICollection<T>, INotifyPropertyChanged,INotifyCollectionChanged
        where T : INotifyPropertyChanged
    {
        [__DynamicallyInvokable, DataMember]
        ObservableCollection<T> Items { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

        [__DynamicallyInvokable]
        ObservableCollection<T> ModifiedItems { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

        /// <summary>
        ///     Gets or sets the element at the specified index.
        /// </summary>
        /// <returns>
        ///     The element at the specified index.
        /// </returns>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is not a valid index in the
        ///     <see cref="T:meh.Interfaces.IItemsCollection`1" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     The property is set and the
        ///     <see cref="T:meh.Interfaces.IItemsCollection`1" /> is read-only.
        /// </exception>
        [__DynamicallyInvokable]
        T this[int index] { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e);

        /// <summary>
        ///     Inserts an item to the <see cref="T:meh.Interfaces.IItemsCollection`1" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted. </param>
        /// <param name="value">The object to insert into the <see cref="T:meh.Interfaces.IItemsCollection`1" />. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is not a valid index in the
        ///     <see cref="T:meh.Interfaces.IItemsCollection`1" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:meh.Interfaces.IItemsCollection`1" /> is read-only.-or- The
        ///     <see cref="T:meh.Interfaces.IItemsCollection`1" /> has a fixed size.
        /// </exception>
        /// <exception cref="T:System.NullReferenceException">
        ///     <paramref name="value" /> is null reference in the
        ///     <see cref="T:meh.Interfaces.IItemsCollection`1" />.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        [__DynamicallyInvokable]
        void Insert(int index, T value);

        /// <summary>
        ///     Removes the <see cref="T:meh.Interfaces.IItemsCollection`1" /> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is not a valid index in the
        ///     <see cref="T:meh.Interfaces.IItemsCollection`1" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:meh.Interfaces.IItemsCollection`1" /> is read-only.-or- The
        ///     <see cref="T:meh.Interfaces.IItemsCollection`1" /> has a fixed size.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        [__DynamicallyInvokable]
        void RemoveAt(int index);

        /// <summary>
        ///     Determines the index of a specific item in the <see cref="T:meh.Interfaces.IItemsCollection`1" />.
        /// </summary>
        /// <returns>
        ///     The index of <paramref name="value" /> if found in the list; otherwise, -1.
        /// </returns>
        /// <param name="value">The object to locate in the <see cref="T:meh.Interfaces.IItemsCollection`1" />. </param>
        /// <filterpriority>2</filterpriority>
        [__DynamicallyInvokable]
        int IndexOf(T value);


        ///// <summary>
        /////     Removes the first occurrence of a specific object from the <see cref="T:Cush.Common.IItemsCollection`1" />.
        ///// </summary>
        ///// <param name="value">The object to remove from the <see cref="T:Cush.Common.IItemsCollection`1" />. </param>
        ///// <exception cref="T:System.NotSupportedException">
        /////     The <see cref="T:Cush.Common.IItemsCollection`1" /> is read-only.-or- The
        /////     <see cref="T:Cush.Common.IItemsCollection`1" /> has a fixed size.
        ///// </exception>
        ///// <filterpriority>2</filterpriority>
        //[__DynamicallyInvokable]
        //void Remove(T value);

        ///// <summary>
        /////     Adds an item to the <see cref="T:Cush.Common.IItemsCollection`1" />.
        ///// </summary>
        ///// <returns>
        /////     The position into which the new element was inserted, or -1 to indicate that the item was not inserted into the
        /////     collection,
        ///// </returns>
        ///// <param name="value">The object to add to the <see cref="T:Cush.Common.IItemsCollection`1" />. </param>
        ///// <exception cref="T:System.NotSupportedException">
        /////     The <see cref="T:Cush.Common.IItemsCollection`1" /> is read-only.-or- The
        /////     <see cref="T:Cush.Common.IItemsCollection`1" /> has a fixed size.
        ///// </exception>
        ///// <filterpriority>2</filterpriority>
        //[__DynamicallyInvokable]
        //int Add(T value);

        ///// <summary>
        /////     Determines whether the <see cref="T:Cush.Common.IItemsCollection`1" /> contains a specific value.
        ///// </summary>
        ///// <returns>
        /////     true if the <see cref="T:System.Object" /> is found in the <see cref="T:Cush.Common.IItemsCollection`1" />;
        /////     otherwise,
        /////     false.
        ///// </returns>
        ///// <param name="value">The object to locate in the <see cref="T:Cush.Common.IItemsCollection`1" />. </param>
        ///// <filterpriority>2</filterpriority>
        //[__DynamicallyInvokable]
        //bool Contains(T value);

        ///// <summary>
        /////     Removes all items from the <see cref="T:Cush.Common.IItemsCollection`1" />.
        ///// </summary>
        ///// <exception cref="T:System.NotSupportedException">The <see cref="T:Cush.Common.IItemsCollection`1" /> is read-only. </exception>
        ///// <filterpriority>2</filterpriority>
        //[__DynamicallyInvokable]
        //void Clear();

    }
}