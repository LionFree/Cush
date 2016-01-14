using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Threading;

namespace Cush.Common
{
    //internal class ThreadSafeObservableCollection<T> : ObservableCollection<T>
    //{
    //    public override event NotifyCollectionChangedEventHandler CollectionChanged;

    //    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    //    {
    //        var collectionChanged = CollectionChanged;
    //        if (collectionChanged == null) return;

    //        foreach (var @delegate in collectionChanged.GetInvocationList())
    //        {
    //            var nh = (NotifyCollectionChangedEventHandler)@delegate;
    //            var dispObj = nh.Target as DispatcherObject;
    //            var dispatcher = dispObj?.Dispatcher;
    //            if (dispatcher != null && !dispatcher.CheckAccess())
    //            {
    //                var notificationHandler = nh;
    //                dispatcher.BeginInvoke(
    //                    (Action)(() => notificationHandler.Invoke(this,
    //                        new NotifyCollectionChangedEventArgs(
    //                            NotifyCollectionChangedAction.Reset))),
    //                    DispatcherPriority.DataBind);
    //                continue;
    //            }
    //            nh.Invoke(this, e);
    //        }
    //    }
    //}

    [DebuggerTypeProxy(typeof(CollectionDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
    public sealed class ThreadSafeObservableCollection<T> : ObservableCollection<T>
    {
        private readonly object _syncLock = new object();

        private BoundedList<T> _modifiedItems;

        private bool _suspendCollectionChangeNotification;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:ThreadSafeObservableCollection`1" /> class.
        /// </summary>
        public ThreadSafeObservableCollection() : this(new List<T>())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:ThreadSafeObservableCollection`1" /> class that contains elements
        ///     copied from the specified list.
        /// </summary>
        /// <param name="list">The list from which the elements are copied.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="list" /> parameter cannot be null.</exception>
        public ThreadSafeObservableCollection(List<T> list) : this((IEnumerable<T>)list)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:ThreadSafeObservableCollection`1" /> class that contains elements
        ///     copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection from which the elements are copied.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="collection" /> parameter cannot be null.</exception>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public ThreadSafeObservableCollection(IEnumerable<T> collection) : base(collection)
        {
            ModifiedItems = new BoundedList<T>(500);
            base.CollectionChanged += OnCollectionChanged;
            WireUpItems(Items);
            _suspendCollectionChangeNotification = false;
        }

        public BoundedList<T> ModifiedItems
        {
            get
            { return _modifiedItems; }

            private set
            {
                if (_modifiedItems == value) return;
                _modifiedItems = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ModifiedItems"));
            }
        }

        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        public void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is T)
            {
                ModifiedItems.Add((T)sender);
            }

            OnPropertyChanged(new PropertyChangedEventArgs(e.PropertyName));
        }

        public void AddItems(IList<T> items)
        {
            lock (_syncLock)
            {
                SuspendCollectionChangeNotification();
                foreach (var i in items)
                {
                    InsertItem(Count, i);
                }
                NotifyChanges();
            }
        }

        private void NotifyChanges()
        {
            ResumeCollectionChangeNotification();
            var arg
                 = new NotifyCollectionChangedEventArgs
                      (NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(arg);
        }

        /// <summary>
        /// This method removes the given generic list of items as a range
        /// into current collection by casting them as type T.
        /// It then notifies once after all items are removed.
        /// </summary>
        /// <param name="items">The source collection.</param>
        public void RemoveItems(IList<T> items)
        {
            lock (_syncLock)
            {
                SuspendCollectionChangeNotification();
                foreach (var i in items)
                {
                    Remove(i);
                }
                NotifyChanges();
            }
        }

        /// <summary>
        /// Resumes collection changed notification.
        /// </summary>
        public void ResumeCollectionChangeNotification()
        {
            _suspendCollectionChangeNotification = false;
        }

        /// <summary>
        /// Suspends collection changed notification.
        /// </summary>
        public void SuspendCollectionChangeNotification()
        {
            _suspendCollectionChangeNotification = true;
        }

        private void RaiseCollectionChangedEvent(NotifyCollectionChangedEventArgs args)
        {
            if (_suspendCollectionChangeNotification) return;

            var eventToRaise = CollectionChanged;
            if (eventToRaise == null) return;

            foreach (var @delegate in eventToRaise.GetInvocationList())
            {
                var handler = (NotifyCollectionChangedEventHandler)@delegate;
                var dispObj = handler.Target as DispatcherObject;
                var dispatcher = dispObj?.Dispatcher;

                if (dispatcher != null && !dispatcher.CheckAccess())
                {
                    var notificationHandler = handler;
                    dispatcher.BeginInvoke(
                        (Action)(() => notificationHandler.Invoke(this,
                            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset))),
                        DispatcherPriority.DataBind);
                    continue;
                }
                handler.Invoke(this, args);
            }
        }

        [DebuggerStepThrough]
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            using (BlockReentrancy())
            {
                if (_suspendCollectionChangeNotification) return;

                if (typeof(T).GetInterfaces().Contains(typeof(INotifyPropertyChanged)))
                {
                    WireUpItems(e.NewItems);
                    UnHookItems(e.OldItems);
                }

                RaiseCollectionChangedEvent(e);
            }
        }

        private void WireUpItems(IEnumerable itemsList)
        {
            if (itemsList == null) return;

            foreach (var item in itemsList.OfType<INotifyPropertyChanged>())
            {
                ModifiedItems.Add((T)item);
                item.PropertyChanged += OnItemPropertyChanged;
            }
        }

        private void UnHookItems(IEnumerable itemsList)
        {
            if (itemsList == null) return;
            foreach (var item in itemsList.OfType<INotifyPropertyChanged>())
            {
                ModifiedItems.Add((T)item);
                item.PropertyChanged -= OnItemPropertyChanged;
            }
        }
    }
}
