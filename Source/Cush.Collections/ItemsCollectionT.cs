using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Cush.Common;

namespace Cush.Collections
{
    [DataContract]
    public abstract class ItemsCollection<T> : BindableBase where T : BindableBase
    {
        private ObservableCollection<T> _items;
        private ObservableCollection<T> _modifiedItems;

        protected ItemsCollection()
        {
            Items = new ObservableCollection<T>();
            ModifiedItems = new ObservableCollection<T>();

            Items.CollectionChanged += OnCollectionChanged;
        }

        [DataMember]
        public ObservableCollection<T> Items
        {
            get { return _items; }
            set
            {
                if (_items == value) return;
                _items = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<T> ModifiedItems
        {
            get { return _modifiedItems; }
            set
            {
                if (_modifiedItems == value) return;
                _modifiedItems = value;
                OnPropertyChanged();
            }
        }

        public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _modifiedItems.Clear();

            if (e.NewItems != null)
            {
                foreach (T item in e.NewItems)
                {
                    _modifiedItems.Add(item);
                    item.PropertyChanged += OnItemPropertyChanged;
                }
            }

            if (e.OldItems == null) return;
            foreach (T item in e.OldItems)
            {
                _modifiedItems.Add(item);
                item.PropertyChanged -= OnItemPropertyChanged;
            }
        }

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public virtual void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }
}