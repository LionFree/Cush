using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Cush.Common.FileHandling;
using Cush.WPF.Controls.Helpers;

namespace Cush.WPF.Controls
{
    //[StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof (ListBoxItem))]
    [TemplatePart(Name = nameof(OpenOtherFileButton), Type = typeof (Button))]
    public sealed class MRUFileMenu : Control
    {
        //-------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-------------------------------------------------------------------

        #region Public Methods

        public override void OnApplyTemplate()
        {
            // Happens before ControlLoaded.
            base.OnApplyTemplate();

            // Attach binding for control loaded event
            Loaded += ControlLoaded;
            var openOtherButton = GetTemplateChild(OpenOtherFileButton) as Button;
            if (openOtherButton != null)
            {
                openOtherButton.Click += OnOpenOtherClick;
            }

            WireUpMouseEvents(PART_PinnedList);
            WireUpMouseEvents(PART_UnpinnedList);

            var contextMenuCommandBinding = new CommandBinding(
                ContextMenuRoutedCommand, ExecutedContextMenuCommand, (s, e) => { e.CanExecute = true; });

            // attach CommandBinding to root control
            CommandBindings.Add(contextMenuCommandBinding);
            WireUpContextMenu("PART_PinnedContext");
            WireUpContextMenu("PART_UnpinnedContext");
        }

        private void OnOpenOtherClick(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(OpenOtherFileClickedEvent));
        }

        #endregion

        //-------------------------------------------------------------------
        //
        //  Constructors
        //
        //-------------------------------------------------------------------

        #region Constructors

        /// <summary>
        ///     Default DependencyObject constructor
        /// </summary>
        /// <remarks>
        ///     Automatic determination of current Dispatcher. Use alternative constructor
        ///     that accepts a Dispatcher for best performance.
        /// </remarks>
        public MRUFileMenu()
        {
            SizeChanged += MRUFileMenu_SizeChanged;
            SetCurrentValue(MRUItemsSourceProperty, new ObservableCollection<MRUEntry>());
        }

        static MRUFileMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (MRUFileMenu),
                new FrameworkPropertyMetadata(typeof (MRUFileMenu)));
        }

        #endregion

        //-------------------------------------------------------------------
        //
        //  Public Properties
        //
        //-------------------------------------------------------------------

        #region Public Properties

        public static readonly RoutedCommand ContextMenuRoutedCommand = new RoutedCommand();

        public static readonly DependencyProperty OpenRecentFileCommandProperty
            = DependencyProperty.Register(
                "OpenRecentFileCommand", typeof (ICommand), typeof (MRUFileMenu));

        public ICommand OpenRecentFileCommand
        {
            get { return (ICommand) GetValue(OpenRecentFileCommandProperty); }
            set { SetValue(OpenRecentFileCommandProperty, value); }
        }

        public static readonly DependencyProperty OpenACopyCommandProperty
            = DependencyProperty.Register(
                "OpenACopyCommand", typeof(ICommand), typeof(MRUFileMenu));

        public ICommand OpenACopyCommand
        {
            get { return (ICommand)GetValue(OpenACopyCommandProperty); }
            set { SetValue(OpenACopyCommandProperty, value); }
        }

        public static readonly DependencyProperty OpenOtherFileCommandProperty
            = DependencyProperty.Register(
                "OpenOtherFileCommand",
                typeof (ICommand),
                typeof (MRUFileMenu),
                new FrameworkPropertyMetadata(null, OnOpenOtherFileChanged));

        private static void OnOpenOtherFileChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var b = (MRUFileMenu) d;
            OnCommandChanged((ICommand) e.OldValue,
                (ICommand) e.NewValue,
                b.OnOpenOtherCanExecuteChanged,
                ref b._openOtherCanExecuteChangedHandler);
        }

        private void OnOpenOtherCanExecuteChanged(object sender, EventArgs e)
        {
            OnCommandCanExecuteChanged(OpenOtherFileCommand,
                _isOpenOtherCommandExecuting,
                value => _isOpenOtherCommandExecuting = value);
        }
        
        private static void OnCommandChanged(ICommand oldCommand,
            ICommand newCommand,
            EventHandler handler,
            ref EventHandler reference)
        {
            if (oldCommand != null)
            {
                oldCommand.CanExecuteChanged -= handler;
            }
            if (newCommand != null)
            {
                //the command system uses WeakReferences internally,
                //so we have to hold a reference to the canExecuteChanged handler ourselves
                if (reference == null)
                    reference = handler;

                newCommand.CanExecuteChanged += handler;
            }
            else
            {
                reference = null;
            }
        }

        private void OnCommandCanExecuteChanged(ICommand command,
            bool executing,
            Action<bool> setExecuting)
        {
            if (executing) return;
            setExecuting(true);

            try
            {
                //use our custom class as the parameter
                var parameter = new CommandCanExecuteParameter(command, null, null);
                CommandHelper.CanExecute(this, parameter);
            }
            finally
            {
                setExecuting(false);
            }
        }


        public ICommand OpenOtherFileCommand
        {
            get { return (ICommand) GetValue(OpenOtherFileCommandProperty); }
            set { SetValue(OpenOtherFileCommandProperty, value); }
        }

        public bool ValidateFiles
        {
            get { return (bool) GetValue(ValidateFilesProperty); }
            set { SetValue(ValidateFilesProperty, value); }
        }

        public static readonly DependencyProperty MRUItemsSourceProperty
            = DependencyProperty.Register("MRUItemsSource",
                typeof (ObservableCollection<MRUEntry>),
                typeof (MRUFileMenu),
                new FrameworkPropertyMetadata(new ObservableCollection<MRUEntry>(),
                    OnMRUItemsSourceChanged));


        public ObservableCollection<MRUEntry> MRUItemsSource
        {
            private get { return (ObservableCollection<MRUEntry>) GetValue(MRUItemsSourceProperty); }
            set { SetValue(MRUItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ValidateFilesProperty = DependencyProperty.Register(
            "ValidateFiles",
            typeof (bool),
            typeof (MRUFileMenu),
            new PropertyMetadata(true));
        
        public static readonly DependencyProperty BreadcrumbsVisibleProperty = DependencyProperty.Register(
            "BreadcrumbsVisible", typeof (bool), typeof (MRUFileMenu), new PropertyMetadata(true));

        public static readonly DependencyProperty OpenOtherTextProperty = DependencyProperty.Register(
            "OpenOtherText", typeof (string), typeof (MRUFileMenu), new UIPropertyMetadata("Open other files"));

        public static readonly DependencyProperty VerticalScrollBarVisibilityProperty =
            DependencyProperty.Register("VerticalScrollBarVisibility", typeof (ScrollBarVisibility),
                typeof (MRUFileMenu), new UIPropertyMetadata(ScrollBarVisibility.Disabled));

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof (CornerRadius), typeof (MRUFileMenu), new UIPropertyMetadata(new CornerRadius(0)));

        public static readonly DependencyProperty OpenACopyVisibleProperty = DependencyProperty.Register(
            "OpenACopyVisible", typeof (bool), typeof (MRUFileMenu), new PropertyMetadata(false));

        public static readonly DependencyProperty HotForegroundColorProperty = DependencyProperty.Register(
            "HotForegroundColor", typeof (SolidColorBrush), typeof (MRUFileMenu),
            new UIPropertyMetadata(new SolidColorBrush {Color = ColorHelper.HexToMediaColor("#FF9D9D9D")}));

        public static readonly DependencyProperty ColdForegroundColorProperty = DependencyProperty.Register(
            "ColdForegroundColor", typeof (Brush), typeof (MRUFileMenu),
            new UIPropertyMetadata(new SolidColorBrush {Color = Colors.Black}));

        public static readonly DependencyProperty HighlightDarkColorProperty = DependencyProperty.Register(
            "HighlightDarkColor", typeof (Brush), typeof (MRUFileMenu),
            new UIPropertyMetadata(new SolidColorBrush {Color = ColorHelper.HexToMediaColor("#FF086F9E")}));

        public static readonly DependencyProperty HighlightBackgroundColorBrushProperty =
            DependencyProperty.Register("HighlightBackgroundColorBrush", typeof (Brush), typeof (MRUFileMenu),
                new UIPropertyMetadata(Brushes.Red));

        public static readonly DependencyProperty HighlightForegroundColorProperty = DependencyProperty.Register(
            "HighlightForegroundColor", typeof (Brush), typeof (MRUFileMenu), new UIPropertyMetadata(Brushes.White));

        public static readonly DependencyProperty AccentColorProperty = DependencyProperty.Register(
            "AccentColor", typeof (Brush), typeof (MRUFileMenu), new UIPropertyMetadata(Brushes.Black));

        public static readonly DependencyProperty HotBackgroundColorProperty = DependencyProperty.Register(
            "HotBackgroundColor",
            typeof (SolidColorBrush),
            typeof (MRUFileMenu),
            new UIPropertyMetadata(new SolidColorBrush {Color = Colors.DarkGray}));

        public static readonly DependencyProperty ColdBackgroundColorProperty = DependencyProperty.Register(
            "ColdBackgroundColor",
            typeof (Brush),
            typeof (MRUFileMenu),
            new UIPropertyMetadata(new SolidColorBrush {Color = ColorHelper.HexToMediaColor("#D5D5D5")}));

        public SolidColorBrush HotBackgroundColor
        {
            get { return (SolidColorBrush) GetValue(HotBackgroundColorProperty); }
            set { SetValue(HotBackgroundColorProperty, value); }
        }

        public SolidColorBrush ColdBackgroundColor
        {
            get { return (SolidColorBrush) GetValue(ColdBackgroundColorProperty); }
            set { SetValue(ColdBackgroundColorProperty, value); }
        }

        public SolidColorBrush HotForegroundColor
        {
            get { return (SolidColorBrush) GetValue(HotForegroundColorProperty); }
            set { SetValue(HotForegroundColorProperty, value); }
        }

        public SolidColorBrush ColdForegroundColor
        {
            get { return (SolidColorBrush) GetValue(ColdForegroundColorProperty); }
            set { SetValue(ColdForegroundColorProperty, value); }
        }

        public SolidColorBrush HighlightDarkColor
        {
            get { return (SolidColorBrush) GetValue(HighlightDarkColorProperty); }
            set { SetValue(HighlightDarkColorProperty, value); }
        }

        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get { return (ScrollBarVisibility) GetValue(VerticalScrollBarVisibilityProperty); }
            set { SetValue(VerticalScrollBarVisibilityProperty, value); }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius) GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public Brush HighlightBackgroundColorBrush
        {
            get { return (Brush) GetValue(HighlightBackgroundColorBrushProperty); }
            set { SetValue(HighlightBackgroundColorBrushProperty, value); }
        }

        public Brush HighlightForegroundColor
        {
            get { return (Brush) GetValue(HighlightForegroundColorProperty); }
            set { SetValue(HighlightForegroundColorProperty, value); }
        }

        public Brush AccentColor
        {
            get { return (Brush) GetValue(AccentColorProperty); }
            set { SetValue(AccentColorProperty, value); }
        }

        public bool OpenACopyVisible
        {
            get { return (bool) GetValue(OpenACopyVisibleProperty); }
            set { SetValue(OpenACopyVisibleProperty, value); }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the file's path should
        ///     be displayed as breadcrumbs within the MRU entry.
        /// </summary>
        public bool BreadcrumbsVisible
        {
            get { return (bool) GetValue(BreadcrumbsVisibleProperty); }
            set { SetValue(BreadcrumbsVisibleProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the text shown on the "Open Other Files" button.
        /// </summary>
        public string OpenOtherText
        {
            get { return (string) GetValue(OpenOtherTextProperty); }
            set { SetValue(OpenOtherTextProperty, value); }
        }

        public event RoutedEventHandler OpenOtherFileClicked
        {
            add { AddHandler(OpenOtherFileClickedEvent, value); }
            remove { RemoveHandler(OpenOtherFileClickedEvent, value); }
        }

        public event SelectionChangedEventHandler RecentFileSelected
        {
            add { AddHandler(RecentFileSelectedEvent, value); }
            remove { RemoveHandler(RecentFileSelectedEvent, value); }
        }

        public event SelectionChangedEventHandler OpenACopy
        {
            add { AddHandler(OpenACopyEvent, value); }
            remove { RemoveHandler(OpenACopyEvent, value); }
        }

        // Provide CLR accessors for the event 
        // ReSharper disable once EventNeverSubscribedTo.Global
        public event RoutedEventHandler PinClicked
        {
            add { AddHandler(PinClickedEvent, value); }
            remove { RemoveHandler(PinClickedEvent, value); }
        }


        public static readonly RoutedEvent OpenOtherFileClickedEvent = EventManager.RegisterRoutedEvent(
            "OpenOtherFileClicked", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (MRUFileMenu));

        public static readonly RoutedEvent RecentFileSelectedEvent = EventManager.RegisterRoutedEvent(
            "RecentFileSelected", RoutingStrategy.Bubble, typeof (SelectionChangedEventHandler), typeof (MRUFileMenu));

        public static readonly RoutedEvent OpenACopyEvent = EventManager.RegisterRoutedEvent(
            "OpenACopy", RoutingStrategy.Bubble, typeof (SelectionChangedEventHandler), typeof (MRUFileMenu));

        public static readonly RoutedEvent PinClickedEvent = EventManager.RegisterRoutedEvent(
            "PinClicked", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (MRUFileMenu));

        #endregion

        //-------------------------------------------------------------------
        //
        //  Protected Methods
        //
        //-------------------------------------------------------------------

        #region Protected Methods

        #endregion

        //-------------------------------------------------------------------
        //
        //  Private Methods
        //
        //-------------------------------------------------------------------

        #region Private Methods

        private static void OnMRUItemsSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = obj as MRUFileMenu;
            if (control == null) return;

            var oldList = e.OldValue as INotifyCollectionChanged;
            var newList = e.NewValue as INotifyCollectionChanged;

            if (oldList != null)
                oldList.CollectionChanged -= control.OnItemsCollectionChanged;

            if (newList != null)
                newList.CollectionChanged += control.OnItemsCollectionChanged;

            control.UpdateSeparators();
        }
        
        private void OnItemsCollectionChanged(object source, NotifyCollectionChangedEventArgs args)
        {
            InvalidateVisual(); //Re-render MyControl

            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (MRUEntry item in args.NewItems)
                    {
                        item.PropertyChanged += OnItemPropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (MRUEntry item in args.OldItems)
                    {
                        item.PropertyChanged -= OnItemPropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    foreach (MRUEntry item in args.NewItems)
                    {
                        item.PropertyChanged += OnItemPropertyChanged;
                    }
                    foreach (MRUEntry item in args.OldItems)
                    {
                        item.PropertyChanged -= OnItemPropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    var src = source as IEnumerable<MRUEntry>;
                    if (src == null) return;
                    foreach (var item in src)
                        item.PropertyChanged += OnItemPropertyChanged;
                    break;
            }
        }

        private void OnItemPropertyChanged(object source, PropertyChangedEventArgs args)
        {
            InvalidateVisual(); //Just re-render.
            UpdateSeparators();
        }
        
        private void WireUpMouseEvents(string partName)
        {
            var list = GetTemplateChild(partName) as ListBox;
            if (list == null) return;
            list.PreviewMouseDown += HitTestForPin;
            list.SelectionChanged += OnRecentFileSelected;
        }
        
        private void WireUpContextMenu(string partName)
        {
            var contextMenu = GetTemplateChild(partName) as ContextMenu;
            if (contextMenu == null) return;

            foreach (MenuItem item in contextMenu.Items)
            {
                item.Command = ContextMenuRoutedCommand;
                item.CommandParameter = item.Header;
            }
        }
        
        private void MRUFileMenu_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (IsLoaded)
            {
                ResizeListEntries();
            }
        }

        private void ExecutedContextMenuCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var listBoxItem = e.OriginalSource as ListBoxItem;
            var item = listBoxItem?.Content as MRUEntry;
            if (item == null) return;

            var command = e.Parameter as string;
            if (command == null) return;

            // parse out the command...
            switch (command)
            {
                case "_Remove from list":
                    MRUItemsSource.Remove(item);
                    break;

                case "_Open":
                    OnOpenRecentFile(item);
                    break;

                case "Ope_n a copy":
                    OnOpenACopy(item);
                    break;
                    
                case "_Copy path to clipboard":
                    Clipboard.SetText(item.FullPath);
                    break;

                case "_Pin to list":
                case "_Unpin from list":
                    PinClickAction(item);
                    break;

                case "Cl_ear unpinned files":
                    ClearUnpinnedFiles();
                    UpdateSeparators();
                    break;

                default:
                    throw new ArgumentException("Bad command from context menu.");
            }
        }

        private void OnOpenACopy(MRUEntry entry)
        {
            RaiseEvent(new SelectionChangedEventArgs(OpenACopyEvent, new List<MRUEntry>(), new List<MRUEntry> {entry}));
            OpenACopyCommand?.Execute(entry);
        }

        private void OnOpenRecentFile(MRUEntry entry)
        {
            // Move the item to the top of the list.
            MRUItemsSource.Move(MRUItemsSource.IndexOf(entry), 0);

            RaiseEvent(new SelectionChangedEventArgs(RecentFileSelectedEvent, new List<MRUEntry>(), new List<MRUEntry> { entry }));
            OpenRecentFileCommand?.Execute(entry);
        }

        private void ClearUnpinnedFiles()
        {
            for (var i = 0; i < MRUItemsSource.Count; i++)
            {
                if (MRUItemsSource[i].Pinned) continue;
                MRUItemsSource.RemoveAt(i);
                i--;
            }
        }

        private List<MRUEntry> PinnedFiles2 => MRUItemsSource.Where(item => item.Pinned).ToList();
        private List<MRUEntry> UnpinnedFiles2 => MRUItemsSource.Where(item => !item.Pinned).ToList();


        private void UpdateSeparators()
        {
            // try to find the template
            var temp = Template;
            if (temp == null) return;

            var pinSeparator = Template.FindName(PART_PinnedSeparator, this) as Border;
            var unpinSeparator = Template.FindName(PART_UnpinnedSeparator, this) as Border;

            InvalidateVisual();
            
            if (pinSeparator != null)
            {
                pinSeparator.Visibility = PinnedFiles2.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            }

            if (unpinSeparator != null)
            {
                unpinSeparator.Visibility = UnpinnedFiles2.Count > 0
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            // Do this only after the template is applied (e.g., in the control's Loaded event handler).
            ResizeListEntries();
            UpdateSeparators();
        }

        private void PinClickAction(MRUEntry entry)
        {
            // Move it to the top of the list.
            MRUItemsSource.Move(MRUItemsSource.IndexOf(entry), 0);
            
            // Change the pin.
            entry.Pinned = !entry.Pinned;

            // Update the separator borders
            UpdateSeparators();
        }
      
        private void HitTestForPin(object sender, MouseButtonEventArgs e)
        {
            var obj = sender as Visual;
            if (e.LeftButton != MouseButtonState.Pressed || obj == null) return;
            VisualTreeHelper.HitTest(obj, MyHitTestFilter, x => HitTestResultBehavior.Continue,
                new PointHitTestParameters(e.GetPosition(sender as UIElement)));
        }

        private HitTestFilterBehavior MyHitTestFilter(DependencyObject o)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed) return HitTestFilterBehavior.ContinueSkipChildren;

            var frameworkElement = o as FrameworkElement;
            if (frameworkElement != null)
            {
                var part = frameworkElement.Name;

                if (part == "PART_PinBorder")
                {
                    // The pin('s border) was clicked, so 
                    // Perform action on the hit visual object.

                    // Get the MRUEntry from the visual tree.
                    var item = TreeHelper.FindVisualParent<ListBoxItem>(o);

                    if (item != null)
                    {
                        var mruEntry = item.Content as MRUEntry;
                        PinClickAction(mruEntry);

                        // raise the pin clicked event.
                        RaiseEvent(new RoutedEventArgs(PinClickedEvent));
                    }
                    return HitTestFilterBehavior.ContinueSkipChildren;
                }
            }

            return HitTestFilterBehavior.Continue;
        }

        private void ResizeListEntries()
        {
            // This method resizes the path string such that it will fit within the 
            // boundaries of the listboxitem, with or without ellipses.
            if (MRUItemsSource.Count <= 0) return;

            // Update the Layout to prevent the app from not seeing the ListBoxItem.
            InvalidateVisual();

            // Do this only after the template is applied (e.g., in the control's Loaded event handler).
            Helper.ShortenListEntries(this, MRUItemsSource, nameof(PART_PinnedList));
            Helper.ShortenListEntries(this, MRUItemsSource, nameof(PART_UnpinnedList));

            //if (PinnedFiles2.Count + UnpinnedFiles2.Count > 0)

            //    if (PinnedFiles2.Count > 0)
            //        Helper.ShortenListEntries(this, PinnedFiles2, "PART_PinnedList");

            //if (UnpinnedFiles2.Count > 0)
            //    Helper.ShortenListEntries(this, UnpinnedFiles2, "PART_UnpinnedList");
        }

        

        private void OnRecentFileSelected(object sender, SelectionChangedEventArgs e)
        {
            // Need to unclick the item, in case of file-not-found, etc. 
            var box = sender as ListBox;

            if (box == null || _unselecting) return;

            _unselecting = true;

            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                box.UnselectAll();
                InvalidateVisual();
                _unselecting = false;
                return;
            }

            var mruEntry = Helper.GetItemFromSelectionEvent(e);
            if (mruEntry == null) throw new ArgumentNullException(nameof(sender));

            // Do a hit test on the mouse relative to the pin border.
            var pt = Mouse.GetPosition((UIElement) sender);

            VisualTreeHelper.HitTest((Visual) sender,
                MyHitTestFilter,
                x => HitTestResultBehavior.Continue,
                new PointHitTestParameters(pt));
            box.UnselectAll();

            OnOpenRecentFile(mruEntry);


            _unselecting = false;
        }



        

        #endregion

        //-------------------------------------------------------------------
        //
        //  Private Fields
        //
        //-------------------------------------------------------------------

        #region Private Fields

        private const string PART_PinnedSeparator = "PART_PinnedSeparator";
        private const string PART_UnpinnedSeparator = "PART_UnpinnedSeparator";
        private const string PART_PinnedList = "PART_PinnedList";
        private const string PART_UnpinnedList = "PART_UnpinnedList";
        private const string OpenOtherFileButton = "PART_OpenOtherFileButton";
        private static readonly MRUMenuHelper Helper = MRUMenuHelper.GetInstance();
        private bool _unselecting;
        private bool _isOpenOtherCommandExecuting;
        private EventHandler _openOtherCanExecuteChangedHandler;

        #endregion
    }
}