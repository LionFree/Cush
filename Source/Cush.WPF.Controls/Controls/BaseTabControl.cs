using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cush.WPF.Controls
{
    public abstract class BaseTabControl : TabControl
    {
        protected BaseTabControl()
        {
            InternalCloseTabCommand = new DefaultCloseTabCommand(this);

            //Loaded += BaseTabControl_Loaded;
            //Unloaded += BaseTabControl_Unloaded;
        }

        public Thickness TabStripMargin
        {
            get { return (Thickness)GetValue(TabStripMarginProperty); }
            set { SetValue(TabStripMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TabStripMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TabStripMarginProperty =
            DependencyProperty.Register("TabStripMargin", typeof(Thickness), typeof(BaseTabControl), new PropertyMetadata(new Thickness(0)));


        //void BaseTabControl_Unloaded(object sender, RoutedEventArgs e)
        //{
        //    Loaded -= BaseTabControl_Loaded;
        //    Unloaded -= BaseTabControl_Unloaded;
        //}

        //void BaseTabControl_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //Ensure each tabitem knows what the owning tab is.

        //    if (ItemsSource == null)
        //        foreach (TabItem item in Items)
        //            if (item is CushTabItem)
        //                ((CushTabItem)item).OwningTabControl = this;

        //}
        
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TabItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CushTabItem(); //Overrides the TabControl's default behavior and returns a CushTabItem instead of a regular one.
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            if (!Equals(element, item))
                element.SetCurrentValue(DataContextProperty, item); //dont want to set the datacontext to itself.

            base.PrepareContainerForItemOverride(element, item);
        }

        /// <summary>
        /// Get/sets the command that executes when a MetroTabItem's close button is clicked.
        /// </summary>
        public ICommand CloseTabCommand
        {
            get { return (ICommand)GetValue(CloseTabCommandProperty); }
            set { SetValue(CloseTabCommandProperty, value); }
        }

        public static readonly DependencyProperty CloseTabCommandProperty =
            DependencyProperty.Register("CloseTabCommand", typeof(ICommand), typeof(BaseTabControl), new PropertyMetadata(null));

        internal ICommand InternalCloseTabCommand
        {
            get { return (ICommand)GetValue(InternalCloseTabCommandProperty); }
            set { SetValue(InternalCloseTabCommandProperty, value); }
        }

        private static readonly DependencyProperty InternalCloseTabCommandProperty =
            DependencyProperty.Register("InternalCloseTabCommand", typeof(ICommand), typeof(BaseTabControl), new PropertyMetadata(null));


        public delegate void TabItemClosingEventHandler(object sender, TabItemClosingEventArgs e);

        /// <summary>
        /// An event that is raised when a TabItem is closed.
        /// </summary>
        public event TabItemClosingEventHandler TabItemClosingEvent;

        internal bool RaiseTabItemClosingEvent(CushTabItem closingItem)
        {
            if (TabItemClosingEvent == null) return false;
            foreach (var @delegate in TabItemClosingEvent.GetInvocationList())
            {
                var subHandler = (TabItemClosingEventHandler) @delegate;
                var args = new TabItemClosingEventArgs(closingItem);
                subHandler.Invoke(this, args);
                if (args.Cancel)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Event arguments that are created when a TabItem is closed.
        /// </summary>
        public class TabItemClosingEventArgs : CancelEventArgs
        {
            internal TabItemClosingEventArgs(CushTabItem item)
            {
                ClosingTabItem = item;
            }

            public CushTabItem ClosingTabItem { get; private set; }
        }

        internal class DefaultCloseTabCommand : ICommand
        {
            private readonly BaseTabControl _owner;
            internal DefaultCloseTabCommand(BaseTabControl owner)
            {
                _owner = owner;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

#pragma warning disable 67
            public virtual event EventHandler CanExecuteChanged;
#pragma warning restore 67

            public void Execute(object parameter)
            {
                if (parameter == null) return;

                var paramData = (Tuple<object, CushTabItem>)parameter;

                if (_owner.CloseTabCommand != null) // TODO: let CushTabControl define parameter to pass to command
                    _owner.CloseTabCommand.Execute(null);
                else
                {
                    if (paramData.Item2 == null) return;

                    var tabItem = paramData.Item2;

                    // KIDS: don't try this at home
                    // this is not good MVVM habits and I'm only doing it
                    // because I want the demos to be absolutely bitching

                    // the control is allowed to cancel this event
                    if (_owner.RaiseTabItemClosingEvent(tabItem)) return;

                    if (_owner.ItemsSource == null)
                    {
                        // if the list is hard-coded (i.e. has no ItemsSource)
                        // then we remove the item from the collection
                        _owner.Items.Remove(tabItem);
                    }
                    else
                    {
                        // if ItemsSource is something we cannot work with, bail out
                        var collection = _owner.ItemsSource as IList;
                        if (collection == null) return;

                        // find the item and kill it (I mean, remove it)
                        foreach (var item in _owner.ItemsSource.Cast<object>().Where(item => tabItem.DataContext == item))
                        {
                            collection.Remove(item);
                            break;
                        }
                    }
                }
            }
        }
    }
}