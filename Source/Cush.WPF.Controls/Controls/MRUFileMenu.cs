using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Cush.Common.Exceptions;
using Cush.WPF.Controls.Helpers;

namespace Cush.WPF.Controls
{
    // TemplatePart 
    // Represents an attribute that is applied to the class definition to identify the types of the named parts that are used for control templating.
    // http://msdn.microsoft.com/en-us/library/system.windows.templatepartattribute(v=vs.95).aspx
    //
    // http://xamlcoder.com/cs/blogs/joe/archive/2007/12/13/building-custom-template-able-wpf-controls.aspx
    //
    // Guidelines for Designing Stylable Controls
    // http://msdn.microsoft.com/en-us/library/ms752339.aspx

    [TemplatePart(Name = "Files", Type = typeof (ObservableCollection<MRUEntry>))]
    public sealed class MRUFileMenu : Control
    {
        #region Constructors

        static MRUFileMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (MRUFileMenu),
                new FrameworkPropertyMetadata(
                    typeof (MRUFileMenu)));
        }

        // public constructor
        public MRUFileMenu()
        {
            SizeChanged += MRUFileMenu_SizeChanged;
        }

        #endregion

        #region Static Properties

        private static readonly MRUMenuHelper Helper = MRUMenuHelper.GetInstance();

        public static RoutedCommand OpenOtherFileCommand = new RoutedCommand();
        public static RoutedCommand ContextMenuCommand = new RoutedCommand();

        public static readonly RoutedEvent OpenOtherFileClickedEvent = EventManager.RegisterRoutedEvent(
            "OpenOtherFileClicked", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (MRUFileMenu));

        public static readonly DependencyProperty OpenOtherFileCommandProperty = DependencyProperty.Register(
            "OpenOtherFile", typeof (ICommand), typeof (MRUFileMenu),
            new FrameworkPropertyMetadata(null, OnOpenOtherFileChanged));

        public static readonly RoutedEvent RecentFileSelectedEvent = EventManager.RegisterRoutedEvent(
            "RecentFileSelected", RoutingStrategy.Bubble, typeof (SelectionChangedEventHandler), typeof (MRUFileMenu));

        public static DependencyProperty OpenRecentFileCommandProperty = DependencyProperty.Register(
            "OpenRecentFile", typeof (ICommand), typeof (MRUFileMenu),
            new FrameworkPropertyMetadata(null));

        public static readonly RoutedEvent OpenACopyEvent = EventManager.RegisterRoutedEvent(
            "OpenACopy", RoutingStrategy.Bubble, typeof (SelectionChangedEventHandler), typeof (MRUFileMenu));

        public static readonly RoutedEvent PinClickedEvent = EventManager.RegisterRoutedEvent(
            "PinClicked", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (MRUFileMenu));

        public static readonly DependencyProperty UnpinnedFilesProperty = DependencyProperty.Register(
            "UnpinnedFiles",
            typeof (ObservableCollection<MRUEntry>),
            typeof (MRUFileMenu),
            new PropertyMetadata(new ObservableCollection<MRUEntry>()));

        public static readonly DependencyProperty PinnedFilesProperty = DependencyProperty.Register(
            "PinnedFiles",
            typeof (ObservableCollection<MRUEntry>),
            typeof (MRUFileMenu),
            new PropertyMetadata(new ObservableCollection<MRUEntry>()));

        public static DependencyProperty HotForegroundColorProperty = DependencyProperty.Register(
            "HotForegroundColor",
            typeof (SolidColorBrush),
            typeof (MRUFileMenu),
            new UIPropertyMetadata(new SolidColorBrush {Color = ColorHelper.HexToMediaColor("#FF9D9D9D")}));

        public static DependencyProperty ColdForegroundColorProperty = DependencyProperty.Register(
            "ColdForegroundColor",
            typeof (Brush),
            typeof (MRUFileMenu),
            new UIPropertyMetadata(new SolidColorBrush {Color = Colors.Black}));

        public static DependencyProperty HighlightDarkColorProperty = DependencyProperty.Register(
            "HighlightDarkColor",
            typeof (Brush),
            typeof (MRUFileMenu),
            new UIPropertyMetadata(new SolidColorBrush {Color = ColorHelper.HexToMediaColor("#FF086F9E")}));

        public static readonly DependencyProperty BreadcrumbsVisibleProperty = DependencyProperty.Register(
            "BreadcrumbsVisible",
            typeof (bool),
            typeof (MRUFileMenu),
            new PropertyMetadata(true));

        public static readonly DependencyProperty OpenOtherTextProperty = DependencyProperty.Register(
            "OpenOtherText",
            typeof (string),
            typeof (MRUFileMenu),
            new UIPropertyMetadata(Controls.Strings.TEXT_OpenOtherFiles));

        public static DependencyProperty VerticalScrollBarVisibilityProperty = DependencyProperty.Register(
            "VerticalScrollBarVisibility",
            typeof (ScrollBarVisibility),
            typeof (MRUFileMenu),
            new UIPropertyMetadata(ScrollBarVisibility.Disabled));

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius",
            typeof (CornerRadius),
            typeof (MRUFileMenu),
            new UIPropertyMetadata(new CornerRadius(0)));

        public static readonly DependencyProperty HighlightBackgroundColorProperty = DependencyProperty.Register(
            "HighlightBackgroundColor",
            typeof (Brush),
            typeof (MRUFileMenu),
            new UIPropertyMetadata(Brushes.Red));

        public static readonly DependencyProperty HighlightForegroundColorProperty = DependencyProperty.Register(
            "HighlightForegroundColor",
            typeof (Brush),
            typeof (MRUFileMenu),
            new UIPropertyMetadata(Brushes.White));

        public static readonly DependencyProperty AccentColorProperty = DependencyProperty.Register(
            "AccentColor",
            typeof (Brush),
            typeof (MRUFileMenu),
            new UIPropertyMetadata(Brushes.Black));

        public static readonly DependencyProperty OpenACopyVisibleProperty = DependencyProperty.Register(
            "OpenACopyVisible",
            typeof (bool),
            typeof (MRUFileMenu),
            new PropertyMetadata(false));

        public static readonly DependencyProperty FilesProperty = DependencyProperty.Register(
            "Files", typeof (ObservableCollection<MRUEntry>), typeof (MRUFileMenu),
            new UIPropertyMetadata(new ObservableCollection<MRUEntry>(), OnFilesChanged));

        public static readonly RoutedEvent FilesChangedEvent =
            EventManager.RegisterRoutedEvent("FilesChanged",
                RoutingStrategy.Bubble,
                typeof (RoutedEventHandler),
                typeof (MRUFileMenu));

        #endregion

        #region Static Methods

        private static void OnOpenOtherFileChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var b = (MRUFileMenu) d;
            OnCommandChanged((ICommand) e.OldValue,
                (ICommand) e.NewValue,
                b.OnOpenOtherCanExecuteChanged,
                ref b._openOtherCanExecuteChangedHandler);
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

        private static void OnFilesChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var mruFileMenu = o as MRUFileMenu;
            if (mruFileMenu != null)
                mruFileMenu.OnFilesChanged((ObservableCollection<MRUEntry>) e.OldValue,
                    (ObservableCollection<MRUEntry>) e.NewValue);
        }

        #endregion

        #region Internal / Private Properties

        private readonly List<ListBox> _alreadyUpdated = new List<ListBox>();
        private EventHandler _openOtherCanExecuteChangedHandler;
        private bool _unselecting;
        private bool _isOpenOtherCommandExecuting;

        internal ObservableCollection<MRUEntry> UnpinnedFiles
        {
            get { return (ObservableCollection<MRUEntry>) GetValue(UnpinnedFilesProperty); }
        }

        internal ObservableCollection<MRUEntry> PinnedFiles
        {
            get { return (ObservableCollection<MRUEntry>) GetValue(PinnedFilesProperty); }
        }

        private void OnOpenOtherCanExecuteChanged(object sender, EventArgs e)
        {
            OnCommandCanExecuteChanged(OpenOtherFile,
                _isOpenOtherCommandExecuting,
                value => _isOpenOtherCommandExecuting = value);
        }

        private void OnFilesChanged(ICollection<MRUEntry> oldValue,
            ObservableCollection<MRUEntry> newValue)
        {
            // fire text changed event
            Files = newValue;

            foreach (var item in oldValue.Where(item => !newValue.Contains(item)))
            {
                RemoveFromLists(item);
            }

            foreach (var item in newValue.Where(item => !oldValue.Contains(item)))
            {
                AddToLists(item);
            }

            RaiseEvent(new RoutedEventArgs(FilesChangedEvent, this));
        }

        #endregion

        #region Private Methods

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


        private void MRUFileMenu_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (IsLoaded)
            {
                ResizeListEntries();
            }
        }

        private void ExecutedContextMenuCommand(object sender, ExecutedRoutedEventArgs e)
        {
            //Trace.WriteLine("ExecutedContextMenuCommand Start: " + DateTime.Now.Second + "." + DateTime.Now.Millisecond);

            var listBoxItem = e.OriginalSource as ListBoxItem;
            if (listBoxItem == null) return;

            var item = listBoxItem.Content as MRUEntry;
            if (item == null) return;

            var command = e.Parameter as string;
            if (command == null) return;

            // parse out the command...
            switch (command)
            {
                case "_Remove from list":
                    RemoveFromLists(item);
                    break;

                case "_Open":
                    Select(item,
                        new SelectionChangedEventArgs(RecentFileSelectedEvent,
                            new List<MRUEntry>(),
                            new List<MRUEntry> {item}));
                    break;

                case "Ope_n a copy":
                    var openCopyArgs = new SelectionChangedEventArgs(OpenACopyEvent,
                        new List<MRUEntry>(),
                        new List<MRUEntry> {item});
                    RaiseEvent(openCopyArgs);
                    break;

                case "_Copy path to clipboard":
                    Clipboard.SetText(item.FullPath);
                    break;

                case "_Pin to list":
                case "_Unpin from list":
                    PinClickAction(item);
                    break;

                case "Cl_ear unpinned files":
                    UnpinnedFiles.Clear();

                    Helper.UpdateFileLists(Files, PinnedFiles, UnpinnedFiles);

                    Helper.UpdateSeparators(this);
                    break;

                default:
                    throw new ArgumentException("Bad command from context menu.");
            }
        }

        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            // Do this only after the template is applied (e.g., in the control's Loaded event handler).
            ResizeListEntries();
            Helper.UpdateSeparators(this);
        }

        private void PinClickAction(MRUEntry data)
        {
            // Move it from one list to the other.
            MoveItemBetweenLists(data, data.Pinned ? PinnedFiles : UnpinnedFiles,
                data.Pinned ? UnpinnedFiles : PinnedFiles);

            // Change the pin.
            data.Pinned = !data.Pinned;
        }

        private void MoveItemBetweenLists(MRUEntry item, ICollection<MRUEntry> source, IList<MRUEntry> target)
        {
            // The simple method
            source.Remove(item);
            target.Insert(0, item);

            // Update the Files collection.
            Helper.UpdateFileLists(Files, PinnedFiles, UnpinnedFiles);


            // TODO: Add the graphical animation from one listbox to the other.


            // Resize the paths to fit the listboxItems.
            if (IsLoaded)
            {
                ResizeListEntries();
            }

            // Update the separator borders
            Helper.UpdateSeparators(this);

            // Update the listbox styles
            var pinnedList = GetTemplateChild("PART_PinnedList") as ListBox;
            if (pinnedList != null)
                Helper.UpdateListboxStyle(
                    pinnedList, HotForegroundColor, HighlightBackgroundColor, _alreadyUpdated, true);

            var unpinnedList = GetTemplateChild("PART_UnpinnedList") as ListBox;
            if (unpinnedList != null)
                Helper.UpdateListboxStyle(
                    unpinnedList, HotForegroundColor, HighlightBackgroundColor, _alreadyUpdated, true);
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

        private void ExecutedOpenOtherFileCommand(object sender, ExecutedRoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(OpenOtherFileClickedEvent));
            OpenOtherFile?.Execute(null);
        }

        private void AddToLists(MRUEntry item)
        {
            ThrowHelper.IfNullThenThrow(() => item);
            
            // The 'collection' refers to the ObservableCollection.
            // The 'list' refers to the pinned and unpinned ListBox UI elements.

            // The user may be attempting to populate the list with a stored MRU list.
            // Check to see if the file still exists before adding it.
            if (string.IsNullOrEmpty(item.FullPath) || (!File.Exists(item.FullPath)))
            {
                HandleMissingFile(item.FullPath);
                return;
            }

            // Check if the item is already in the collection.
            var j = IndexOf(item.FullPath);
            if (j != -1)
            {
                // If it is already in the collection, then remove it so it can be placed at the top of the list.
                item = Files[j];

                if (item.Pinned)
                {
                    var k = PinnedFiles.IndexOf(item.FullPath);
                    PinnedFiles.RemoveAt(k);
                }
                else if (!item.Pinned)
                {
                    var k = UnpinnedFiles.IndexOf(item.FullPath);
                    UnpinnedFiles.RemoveAt(k);
                }
            }

            // Add Item To its list
            if (item.Pinned)
            {
                PinnedFiles.Insert(0, item);
            }
            else if (!item.Pinned)
            {
                UnpinnedFiles.Insert(0, item);
            }

            // Adjust the Files list to match the Pinned and Unpinned lists.
            Helper.UpdateFileLists(Files, PinnedFiles, UnpinnedFiles);

            // Shorten the list Entries.

            if (IsLoaded)
            {
                //Trace.WriteLine("--------------------- Resize from Add");
                ResizeListEntries();
            }

            // Update the separators.
            Helper.UpdateSeparators(this);
        }

        private void RemoveFromLists(MRUEntry item)
        {
            if (item == null) throw new ArgumentNullException("item");

            // Check if the item is already in the collection.
            var j = Files.IndexOf(item.FullPath);
            if (j == -1) return;

            // If it is already in the collection, then remove it so it can be placed at the top of the list.
            item = Files[j];

            if (item.Pinned)
            {
                var k = PinnedFiles.IndexOf(item.FullPath);
                PinnedFiles.RemoveAt(k);
            }
            else if (!item.Pinned)
            {
                var k = UnpinnedFiles.IndexOf(item.FullPath);
                UnpinnedFiles.RemoveAt(k);
            }

            // Adjust the Files list to match the Pinned and Unpinned lists.
            Helper.UpdateFileLists(Files, PinnedFiles, UnpinnedFiles);

            // Update the separators.
            Helper.UpdateSeparators(this);
        }

        private int IndexOf(string fullPath)
        {
            var pinnedIndex = PinnedFiles.IndexOf(fullPath);
            var unpinnedIndex = UnpinnedFiles.IndexOf(fullPath);
            var index = (pinnedIndex != -1) ? pinnedIndex : unpinnedIndex;
            return index;
        }

        private void ResizeListEntries()
        {
            // This method resizes the path string such that it will fit within the 
            // boundaries of the listboxitem, with or without ellipses.

            // Update the Layout to prevent the app from not seeing the ListBoxItem.
            UpdateLayout();

            // Do this only after the template is applied (e.g., in the control's Loaded event handler).
            if (PinnedFiles.Count + UnpinnedFiles.Count > 0)

                if (PinnedFiles.Count > 0)
                    Helper.ShortenListEntries(this, PinnedFiles, "PART_PinnedList");

            if (UnpinnedFiles.Count > 0)
                Helper.ShortenListEntries(this, UnpinnedFiles, "PART_UnpinnedList");
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
                UpdateLayout();
                _unselecting = false;
                return;
            }

            var mruEntry = Helper.GetItemFromSelectionEvent(e);
            if (mruEntry == null) throw new ArgumentNullException("sender");

            // Do a hit test on the mouse relative to the pin border.
            var pt = Mouse.GetPosition(sender as UIElement);

            VisualTreeHelper.HitTest(sender as Visual,
                MyHitTestFilter,
                x => HitTestResultBehavior.Continue,
                new PointHitTestParameters(pt));
            box.UnselectAll();

            Select(mruEntry, e);

            _unselecting = false;
        }

        private void Select(MRUEntry mruEntry, SelectionChangedEventArgs e)
        {
            // Move the item to the top of the list.
            var list = mruEntry.Pinned ? PinnedFiles : UnpinnedFiles;

            MoveItemBetweenLists(mruEntry, list, list);

            UpdateLayout();

            RaiseEvent(new SelectionChangedEventArgs(RecentFileSelectedEvent, e.RemovedItems, e.AddedItems));
            OpenRecentFile.Execute(e);
        }

        #endregion

        #region Public Methods

        public override void OnApplyTemplate()
        {
            // Happens before ControlLoaded.
            base.OnApplyTemplate();

            #region Attach binding for control loaded event

            Loaded += ControlLoaded;

            #endregion

            #region Attach binding for Open Other Sheets button

            var openOtherCommandBinding = new CommandBinding(OpenOtherFileCommand, ExecutedOpenOtherFileCommand,
                (s, e) => { e.CanExecute = true; });

            // attach CommandBinding to root control
            CommandBindings.Add(openOtherCommandBinding);

            var openOtherButton = GetTemplateChild("PART_OpenOtherFileButton") as Button;
            if (openOtherButton != null)
            {
                openOtherButton.Command = OpenOtherFileCommand;
            }

            #endregion

            #region Wire up the Open Recent Sheet commands and events

            var pinnedList = GetTemplateChild("PART_PinnedList") as ListBox;
            var unpinnedList = GetTemplateChild("PART_UnpinnedList") as ListBox;

            if (pinnedList != null)
            {
                pinnedList.PreviewMouseDown += HitTestForPin;
                pinnedList.SelectionChanged += OnRecentFileSelected;
                pinnedList.LayoutUpdated += (s, e) => Helper.UpdateListboxStyle(
                    pinnedList, HotForegroundColor, HighlightBackgroundColor, _alreadyUpdated);
            }

            if (unpinnedList != null)
            {
                unpinnedList.PreviewMouseDown += HitTestForPin;
                unpinnedList.SelectionChanged += OnRecentFileSelected;
                unpinnedList.LayoutUpdated += (s, e) => Helper.UpdateListboxStyle(
                    unpinnedList, HotForegroundColor, HighlightBackgroundColor, _alreadyUpdated);
            }

            #endregion

            #region Attach binding for context menu

            var contextMenuCommandBinding = new CommandBinding(
                ContextMenuCommand, ExecutedContextMenuCommand, (s, e) => { e.CanExecute = true; });

            // attach CommandBinding to root control
            CommandBindings.Add(contextMenuCommandBinding);

            var pinnedContextMenu = GetTemplateChild("PART_PinnedContext") as ContextMenu;
            if (pinnedContextMenu != null)
            {
                foreach (MenuItem item in pinnedContextMenu.Items)
                {
                    item.Command = ContextMenuCommand;
                    item.CommandParameter = item.Header;
                }
            }
            var unpinnedContextMenu = GetTemplateChild("PART_UnpinnedContext") as ContextMenu;
            if (unpinnedContextMenu != null)
            {
                foreach (MenuItem item in unpinnedContextMenu.Items)
                {
                    item.Command = ContextMenuCommand;
                    item.CommandParameter = item.Header;
                }
            }

            #endregion
        }

        /// <summary>
        ///     Adds an <see cref="MRUEntry" /> to the Files list.
        /// </summary>
        /// <param name="item">
        ///     The <see cref="MRUEntry" /> to add.
        /// </param>
        public void Add(MRUEntry item)
        {
            Files.Add(item);
        }

        /// <summary>
        ///     Handle add a file that does not exist or has no path.
        /// </summary>
        public void HandleMissingFile(string fullPath)
        {
            //if (string.IsNullOrEmpty(fullPath))
            //{
            //    Trace.WriteLine("fullPath was passed empty or null.");
            //}
        }

        #endregion

        #region Public Properties

        public ICommand OpenOtherFile
        {
            get { return (ICommand) GetValue(OpenOtherFileCommandProperty); }
            set { SetValue(OpenOtherFileCommandProperty, value); }
        }

        public ICommand OpenRecentFile
        {
            get { return (ICommand) GetValue(OpenRecentFileCommandProperty); }
            set { SetValue(OpenRecentFileCommandProperty, value); }
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

        public Brush HighlightBackgroundColor
        {
            get { return (Brush) GetValue(HighlightBackgroundColorProperty); }
            set { SetValue(HighlightBackgroundColorProperty, value); }
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

        /// <summary>
        ///     Gets or sets the collection of MRUEntries stored
        ///     in this <see cref="MRUFileMenu" />.
        /// </summary>
        public ObservableCollection<MRUEntry> Files
        {
            get { return (ObservableCollection<MRUEntry>) GetValue(FilesProperty); }
            set { SetValue(FilesProperty, value); }
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
        public event RoutedEventHandler PinClicked
        {
            add { AddHandler(PinClickedEvent, value); }
            remove { RemoveHandler(PinClickedEvent, value); }
        }

        public event RoutedEventHandler FilesChanged
        {
            add { AddHandler(FilesChangedEvent, value); }
            remove { RemoveHandler(FilesChangedEvent, value); }
        }

        #endregion
    }
}