using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cush.WPF.Controls
{
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

        private delegate void EmptyDelegate();
        ~CushTabItem()
        {
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.Invoke(new EmptyDelegate(() =>
                {
                    Loaded -= CushTabItem_Loaded;
                }));
            }
        }

        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register("HeaderFontSize", typeof(double), typeof(CushTabItem), new PropertyMetadata(26.67, (obj, args) =>
            {
                var item = (CushTabItem)obj;

                if (item.CloseButton == null)
                {
                    item.ApplyTemplate();
                }

                var fontDpiSize = (double)args.NewValue;

                double fontHeight = Math.Ceiling(fontDpiSize * item.RootLabel.FontFamily.LineSpacing);

                var newMargin = (Math.Round(fontHeight) / 2.2) - (item.RootLabel.Padding.Top);

                var previousMargin = item.CloseButton.Margin;
                item.NewButtonMargin = new Thickness(previousMargin.Left, newMargin, previousMargin.Right, previousMargin.Bottom);
                item.CloseButton.Margin = item.NewButtonMargin;

                item.CloseButton.UpdateLayout();

            }));

        public bool CloseButtonEnabled
        {
            get { return (bool)GetValue(CloseButtonEnabledProperty); }
            set { SetValue(CloseButtonEnabledProperty, value); }
        }

        public static readonly DependencyProperty CloseButtonEnabledProperty =
            DependencyProperty.Register("CloseButtonEnabled", typeof(bool), typeof(CushTabItem), new PropertyMetadata(false));

        internal Button CloseButton = null;
        internal Thickness NewButtonMargin;
        internal Label RootLabel = null;
        private bool _closeButtonClickUnloaded;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            bool closeButtonNullBefore = CloseButton == null; //TabControl's multi-loading/unloading issue

            CloseButton = GetTemplateChild("PART_CloseButton") as Button;
            CloseButton.Margin = NewButtonMargin;

            if (closeButtonNullBefore)
                CloseButton.Click += closeButton_Click;


            CloseButton.Visibility = CloseButtonEnabled ? Visibility.Visible : Visibility.Hidden;

            RootLabel = GetTemplateChild("root") as Label;
        }

        void closeButton_Click(object sender, RoutedEventArgs e)
        {
            //Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type TabControl}}, Path=InternalCloseTabCommand
            // Click event fires BEFORE the command does so we have time to set and handle the event before hand.

            if (CloseTabCommand != null)
            {
                // force the command handler to run
                CloseTabCommand.Execute(CloseTabCommandParameter);
                // cheat and dereference the handler now
                CloseTabCommand = null;
                CloseTabCommandParameter = null;
            }

            if (OwningTabControl == null) // see #555
                throw new InvalidOperationException();

            // run the command handler for the TabControl
            var itemFromContainer = OwningTabControl.ItemContainerGenerator.ItemFromContainer(this);

            var data = itemFromContainer == DependencyProperty.UnsetValue ? Content : itemFromContainer;
            OwningTabControl.InternalCloseTabCommand.Execute(new Tuple<object, CushTabItem>(data, this));
        }

        public ICommand CloseTabCommand { get { return (ICommand)GetValue(CloseTabCommandProperty); } set { SetValue(CloseTabCommandProperty, value); } }
        public static readonly DependencyProperty CloseTabCommandProperty = DependencyProperty.Register("CloseTabCommand", typeof(ICommand), typeof(CushTabItem));

        public object CloseTabCommandParameter { get { return GetValue(CloseTabCommandParameterProperty); } set { SetValue(CloseTabCommandParameterProperty, value); } }
        public static readonly DependencyProperty CloseTabCommandParameterProperty =
            DependencyProperty.Register("CloseTabCommandParameter", typeof(object), typeof(CushTabItem), new PropertyMetadata(null));

        public BaseTabControl OwningTabControl { get; internal set; }

        protected override void OnSelected(RoutedEventArgs e)
        {
            if (CloseButton != null)
                if (CloseButtonEnabled)
                    CloseButton.Visibility = Visibility.Visible;

            base.OnSelected(e);
        }

        protected override void OnUnselected(RoutedEventArgs e)
        {
            if (CloseButton != null)
                CloseButton.Visibility = Visibility.Hidden;

            base.OnUnselected(e);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (CloseButton != null)
                if (CloseButtonEnabled)
                    CloseButton.Visibility = Visibility.Visible;

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (!IsSelected)
                if (CloseButton != null)
                    if (CloseButtonEnabled)
                        CloseButton.Visibility = Visibility.Hidden;

            base.OnMouseLeave(e);
        }
    }
}