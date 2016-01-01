using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cush.WPF.Controls
{
    /// <summary>
    /// An extended TabItem.
    /// </summary>
    public class CushTabItem : TabItem
    {
        public CushTabItem()
        {
            DefaultStyleKey = typeof(CushTabItem);
            Unloaded += CushTabItem_Unloaded;
            Loaded += CushTabItem_Loaded;
        }

        void CushTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            if (CloseButton != null && _closeButtonClickUnloaded)
            {
                CloseButton.Click += closeButton_Click;

                _closeButtonClickUnloaded = false;
            }
        }

        void CushTabItem_Unloaded(object sender, RoutedEventArgs e)
        {
            Unloaded -= CushTabItem_Unloaded;
            CloseButton.Click -= closeButton_Click;

            _closeButtonClickUnloaded = true;
        }
        
        internal Button CloseButton = null;
        internal Thickness NewButtonMargin;
        internal ContentPresenter ContentSite;
        private bool _closeButtonClickUnloaded;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            AdjustCloseButton();
            ContentSite=GetTemplateChild("ContentSite") as ContentPresenter;
        }

        private void AdjustCloseButton()
        {
            CloseButton = CloseButton ?? GetTemplateChild("PART_CloseButton") as Button;
            if (CloseButton == null) return;

            CloseButton.Margin = NewButtonMargin;

            //TabControl's multi-loading/unloading issue
            CloseButton.Click -= closeButton_Click;
            CloseButton.Click += closeButton_Click;
        }

        void closeButton_Click(object sender, RoutedEventArgs e)
        {
            //Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type TabControl}}, Path=InternalCloseTabCommand
            // Click event fires BEFORE the command does so we have time to set and handle the event before hand.

            var closeTabCommand = this.CloseTabCommand;
            var closeTabCommandParameter = CloseTabCommandParameter;
            if (closeTabCommand != null)
            {
                if (closeTabCommand.CanExecute(closeTabCommandParameter))
                {
                    // force the command handler to run
                    closeTabCommand.Execute(closeTabCommandParameter);
                }
                // cheat and dereference the handler now
                CloseTabCommand = null;
                CloseTabCommandParameter = null;
            }

            var owningTabControl = this.TryFindParent<BaseTabControl>();
            if (owningTabControl == null) // see #555
                throw new InvalidOperationException();

            // run the command handler for the TabControl
            var itemFromContainer = owningTabControl.ItemContainerGenerator.ItemFromContainer(this);

            var data = itemFromContainer == DependencyProperty.UnsetValue ? Content : itemFromContainer;
            owningTabControl.InternalCloseTabCommand.Execute(new Tuple<object, CushTabItem>(data, this));
        }

        public static readonly DependencyProperty CloseButtonEnabledProperty =
            DependencyProperty.Register("CloseButtonEnabled",
                                        typeof(bool),
                                        typeof(CushTabItem),
                                        new FrameworkPropertyMetadata(false,
                                                                      FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits,
                                                                      OnCloseButtonEnabledPropertyChangedCallback));

        private static void OnCloseButtonEnabledPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var item = dependencyObject as CushTabItem;
            item?.AdjustCloseButton();
        }

        /// <summary>
        /// Gets/sets whether the Close Button is visible.
        /// </summary>
        public bool CloseButtonEnabled
        {
            get { return (bool)GetValue(CloseButtonEnabledProperty); }
            set { SetValue(CloseButtonEnabledProperty, value); }
        }

        /// <summary>
        /// Gets/sets the command that is executed when the Close Button is clicked.
        /// </summary>
        public ICommand CloseTabCommand { get { return (ICommand)GetValue(CloseTabCommandProperty); } set { SetValue(CloseTabCommandProperty, value); } }
        public static readonly DependencyProperty CloseTabCommandProperty = DependencyProperty.Register("CloseTabCommand", typeof(ICommand), typeof(CushTabItem));

        /// <summary>
        /// Gets/sets the command parameter which is passed to the close button command.
        /// </summary>
        public object CloseTabCommandParameter { get { return GetValue(CloseTabCommandParameterProperty); } set { SetValue(CloseTabCommandParameterProperty, value); } }
        public static readonly DependencyProperty CloseTabCommandParameterProperty =
            DependencyProperty.Register("CloseTabCommandParameter", typeof(object), typeof(CushTabItem), new PropertyMetadata(null));

        //public BaseTabControl OwningTabControl { get; internal set; }

        //protected override void OnSelected(RoutedEventArgs e)
        //{
        //    if (CloseButton != null)
        //        if (CloseButtonEnabled)
        //            CloseButton.Visibility = Visibility.Visible;

        //    base.OnSelected(e);
        //}

        //protected override void OnUnselected(RoutedEventArgs e)
        //{
        //    if (CloseButton != null)
        //        CloseButton.Visibility = Visibility.Hidden;

        //    base.OnUnselected(e);
        //}

        //protected override void OnMouseEnter(MouseEventArgs e)
        //{
        //    if (CloseButton != null)
        //        if (CloseButtonEnabled)
        //            CloseButton.Visibility = Visibility.Visible;

        //    base.OnMouseEnter(e);
        //}

        //protected override void OnMouseLeave(MouseEventArgs e)
        //{
        //    if (!IsSelected)
        //        if (CloseButton != null)
        //            if (CloseButtonEnabled)
        //                CloseButton.Visibility = Visibility.Hidden;

        //    base.OnMouseLeave(e);
        //}
    }
}