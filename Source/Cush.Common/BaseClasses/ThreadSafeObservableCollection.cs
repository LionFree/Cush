using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Threading;

namespace Cush.Common
{
    public class ThreadSafeObservableCollection<T> : ObservableCollection<T>
    {
        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var collectionChanged = CollectionChanged;
            if (collectionChanged == null) return;

            foreach (var @delegate in collectionChanged.GetInvocationList())
            {
                var nh = (NotifyCollectionChangedEventHandler) @delegate;
                var dispObj = nh.Target as DispatcherObject;
                if (dispObj != null)
                {
                    var dispatcher = dispObj.Dispatcher;
                    if (dispatcher != null && !dispatcher.CheckAccess())
                    {
                        var notificationHandler = nh;
                        dispatcher.BeginInvoke(
                            (Action)(() => notificationHandler.Invoke(this,
                                new NotifyCollectionChangedEventArgs(
                                    NotifyCollectionChangedAction.Reset))),
                            DispatcherPriority.DataBind);
                        continue;
                    }
                }
                nh.Invoke(this, e);
            }
        }
    }
}
