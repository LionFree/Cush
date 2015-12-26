using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Cush.Native;
using Cush.Native.Shell;
using Cush.WPF.ColorSchemes;
using Cush.WPF.Controls.Helpers;
using Cush.WPF.Controls.Native;
using SystemCommands = System.Windows.SystemCommands;
using SystemParameters = System.Windows.SystemParameters;

namespace Cush.WPF.Controls
{
    [TemplatePart(Name = PART_Icon, Type = typeof (UIElement))]
    [TemplatePart(Name = PART_TitleBar, Type = typeof (UIElement))]
    [TemplatePart(Name = PART_WindowTitleBackground, Type = typeof (UIElement))]
    [TemplatePart(Name = PART_WindowTitleThumb, Type = typeof (Thumb))]
    [TemplatePart(Name = PART_WindowStateButtons, Type = typeof (WindowStateButtons))]
    [TemplatePart(Name = PART_LeftWindowCommands, Type = typeof (WindowCommands))]
    [TemplatePart(Name = PART_RightWindowCommands, Type = typeof (WindowCommands))]
    [TemplatePart(Name = PART_DialogContainer, Type = typeof (Grid))]
    [TemplatePart(Name = PART_OverlayBox, Type = typeof (Grid))]
    [TemplatePart(Name = PART_FlyoutModal, Type = typeof (Rectangle))]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
    public class CushWindow : Window, ISchemedElement
    {
        static CushWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (CushWindow),
                new FrameworkPropertyMetadata(typeof (CushWindow)));
        }


        [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
        public CushWindow()
        {
            Loaded += CushWindow_Loaded;
        }

        private void CushWindow_Loaded(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "AfterLoaded", true);

            if (!ShowTitleBar)
            {
                //Disables the system menu for reasons other than clicking an invisible titlebar.
                var handle = new WindowInteropHelper(this).Handle;
                UnsafeNativeMethods.SetWindowLong(handle, UnsafeNativeMethods.GWL_STYLE,
                    UnsafeNativeMethods.GetWindowLong(handle, UnsafeNativeMethods.GWL_STYLE) &
                    ~UnsafeNativeMethods.WS_SYSMENU);
            }

            if (Flyouts == null)
            {
                Flyouts = new FlyoutsControl();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ColorSchemeManager.Register(this);

            if (LeftWindowCommands == null)
                LeftWindowCommands = new WindowCommands();
            if (RightWindowCommands == null)
                RightWindowCommands = new WindowCommands();

            LeftWindowCommands.ParentWindow = this;
            RightWindowCommands.ParentWindow = this;

            LeftWindowCommandsPresenter = GetTemplateChild(PART_LeftWindowCommands) as ContentPresenter;
            RightWindowCommandsPresenter = GetTemplateChild(PART_RightWindowCommands) as ContentPresenter;
            WindowStateButtons = GetTemplateChild(PART_WindowStateButtons) as WindowCommands;

            if (WindowStateButtons != null)
            {
                WindowStateButtons.ParentWindow = this;
            }

            OverlayBox = GetTemplateChild(PART_OverlayBox) as Grid;
            DialogContainer = GetTemplateChild(PART_DialogContainer) as Grid;

            _flyoutModal = (Rectangle) GetTemplateChild(PART_FlyoutModal);
            if (_flyoutModal == null) throw new Exception("CushWindow _flyoutModal is null.");

            _flyoutModal.PreviewMouseDown += FlyoutsPreviewMouseDown;
            PreviewMouseDown += FlyoutsPreviewMouseDown;

            _icon = GetTemplateChild(PART_Icon) as UIElement;
            _titleBar = GetTemplateChild(PART_TitleBar) as UIElement;
            _titleBarBackground = GetTemplateChild(PART_WindowTitleBackground) as UIElement;
            _windowTitleThumb = GetTemplateChild(PART_WindowTitleThumb) as Thumb;

            SetVisibiltyForAllTitleElements(TitlebarHeight > 0);
        }

        private void FlyoutsPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = (e.OriginalSource as DependencyObject);
            if (element != null)
            {
                // no preview if we just clicked these elements
                if (element.TryFindParent<Flyout>() != null
                    || Equals(element.TryFindParent<ContentControl>(), _icon)
                    || element.TryFindParent<WindowCommands>() != null
                    || element.TryFindParent<WindowStateButtons>() != null)
                {
                    return;
                }
            }

            if (Flyouts.OverrideExternalCloseButton == null)
            {
                foreach (
                    var flyout in
                        Flyouts.GetFlyouts()
                            .Where(
                                x =>
                                    x.IsOpen && x.ExternalCloseButton == e.ChangedButton &&
                                    (!x.IsPinned || Flyouts.OverrideIsPinned)))
                {
                    flyout.IsOpen = false;
                    e.Handled = true;
                }
            }
            else if (Flyouts.OverrideExternalCloseButton == e.ChangedButton)
            {
                foreach (
                    var flyout in Flyouts.GetFlyouts().Where(x => x.IsOpen && (!x.IsPinned || Flyouts.OverrideIsPinned))
                    )
                {
                    flyout.IsOpen = false;
                    e.Handled = true;
                }
            }
        }


        private void SetVisibiltyForAllTitleElements(bool visible)
        {
            SetVisibilityForIcon();
            var newVisibility = visible && ShowTitleBar ? Visibility.Visible : Visibility.Collapsed;

            if (_titleBar != null)
            {
                _titleBar.Visibility = newVisibility;
            }
            if (_titleBarBackground != null)
            {
                _titleBarBackground.Visibility = newVisibility;
            }
            if (LeftWindowCommandsPresenter != null)
            {
                LeftWindowCommandsPresenter.Visibility =
                    LeftWindowCommandsOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.HiddenTitleBar)
                        ? Visibility.Visible
                        : newVisibility;
            }
            if (RightWindowCommandsPresenter != null)
            {
                RightWindowCommandsPresenter.Visibility =
                    RightWindowCommandsOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.HiddenTitleBar)
                        ? Visibility.Visible
                        : newVisibility;
            }
            if (WindowStateButtons != null)
            {
                WindowStateButtons.Visibility = Visibility.Visible;
                WindowStateButtons.Visibility =
                    WindowStateButtonsOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.HiddenTitleBar)
                        ? Visibility.Visible
                        : newVisibility;
            }

            SetWindowEvents();

            //_titleBar.MouseDown += TitleBarMouseDown;
            //    _titleBar.MouseUp += TitleBarMouseUp;
            //    _titleBar.MouseMove += TitleBarMouseMove;
            //}
            //else
            //{
            //    MouseDown += TitleBarMouseDown;
            //    MouseUp += TitleBarMouseUp;
            //    MouseMove += TitleBarMouseMove;
            //}
        }

        private void SetWindowEvents()
        {
            // clear all event handlers first
            ClearWindowEvents();

            // set mouse down/up for icon
            if (_icon != null && _icon.Visibility == Visibility.Visible)
            {
                _icon.MouseDown += IconMouseDown;
            }

            if (_windowTitleThumb != null)
            {
                _windowTitleThumb.DragDelta += WindowTitleThumbMoveOnDragDelta;
                _windowTitleThumb.MouseDoubleClick += WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                _windowTitleThumb.MouseRightButtonUp += WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }

            // handle size if we have a Grid for the title (e.g. clean window have a centered title)
            if (_titleBar != null && _titleBar.GetType() == typeof (Grid))
            {
                SizeChanged += CushWindow_SizeChanged;
            }
        }

        private void CushWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // this all works only for centered title

            var titleBarGrid = (Grid) _titleBar;
            var titleBarLabel = (Label) titleBarGrid.Children[0];
            var titleControl = (ContentControl) titleBarLabel.Content;
            var iconContentControl = (ContentControl) _icon;

            // Half of this MetroWindow
            var halfDistance = Width/2;
            // Distance between center and left/right
            var distanceToCenter = titleControl.ActualWidth/2;
            // Distance between right edge from LeftWindowCommands to left window side
            var distanceFromLeft = iconContentControl.ActualWidth + LeftWindowCommands.ActualWidth;
            // Distance between left edge from RightWindowCommands to right window side
            var distanceFromRight = WindowStateButtons.ActualWidth + RightWindowCommands.ActualWidth;
            // Margin
            const double horizontalMargin = 5.0;

            if ((distanceFromLeft + distanceToCenter + horizontalMargin < halfDistance) &&
                (distanceFromRight + distanceToCenter + horizontalMargin < halfDistance))
            {
                Grid.SetColumn(titleBarGrid, 0);
                Grid.SetColumnSpan(titleBarGrid, 5);
            }
            else
            {
                Grid.SetColumn(titleBarGrid, 2);
                Grid.SetColumnSpan(titleBarGrid, 1);
            }
        }

        private void ClearWindowEvents()
        {
            // clear all event handlers first:
            if (_windowTitleThumb != null)
            {
                _windowTitleThumb.DragDelta -= WindowTitleThumbMoveOnDragDelta;
                _windowTitleThumb.MouseDoubleClick -= WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                _windowTitleThumb.MouseRightButtonUp -= WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }
            if (_icon != null)
            {
                _icon.MouseDown -= IconMouseDown;
            }
            SizeChanged -= CushWindow_SizeChanged;
        }

        private void WindowTitleThumbSystemMenuOnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            DoWindowTitleThumbSystemMenuOnMouseRightButtonUp(this, e);
        }

        internal static void DoWindowTitleThumbSystemMenuOnMouseRightButtonUp(CushWindow window, MouseButtonEventArgs e)
        {
            if (window.ShowSystemMenuOnRightClick)
            {
                // show menu only if mouse pos is on title bar or if we have a window with none style and no title bar
                var mousePos = e.GetPosition(window);
                if ((mousePos.Y <= window.TitlebarHeight && window.TitlebarHeight > 0) ||
                    (window.UseNoneWindowStyle && window.TitlebarHeight <= 0))
                {
                    ShowSystemMenuPhysicalCoordinates(window, window.PointToScreen(mousePos));
                }
            }
        }

        private void WindowTitleThumbChangeWindowStateOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DoWindowTitleThumbChangeWindowStateOnMouseDoubleClick(this, e);
        }

        private void WindowTitleThumbMoveOnDragDelta(object sender, DragDeltaEventArgs e)
        {
            DoWindowTitleThumbMoveOnDragDelta(this, e);
        }

        internal static void DoWindowTitleThumbMoveOnDragDelta(CushWindow window, DragDeltaEventArgs dragDeltaEventArgs)
        {
            // drag only if IsWindowDraggable is set to true
            if (!window.IsWindowDraggable ||
                (!(Math.Abs(dragDeltaEventArgs.HorizontalChange) > 2) &&
                 !(Math.Abs(dragDeltaEventArgs.VerticalChange) > 2))) return;

            var windowHandle = new WindowInteropHelper(window).Handle;
            var cursorPos = NativeMethods.GetCursorPos();

            // if the window is maximized dragging is only allowed on title bar (also if not visible)
            var windowIsMaximized = window.WindowState == WindowState.Maximized;
            var isMouseOnTitlebar = Mouse.GetPosition(window._windowTitleThumb).Y <= window.TitlebarHeight &&
                                    window.TitlebarHeight > 0;
            if (!isMouseOnTitlebar && windowIsMaximized)
            {
                return;
            }

            if (windowIsMaximized)
            {
                window.Top = 2;
                window.Left = Math.Max(cursorPos.x - window.RestoreBounds.Width/2, 0);
            }
            var lParam = (int) (uint) cursorPos.x | (cursorPos.y << 16);
            NativeMethods.SendMessage(windowHandle, WM.LBUTTONUP, (IntPtr) HT.CAPTION, (IntPtr) lParam);
            NativeMethods.SendMessage(windowHandle, WM.SYSCOMMAND, (IntPtr) SC.MOUSEMOVE, IntPtr.Zero);
        }

        internal static void DoWindowTitleThumbChangeWindowStateOnMouseDoubleClick(CushWindow window,
            MouseButtonEventArgs mouseButtonEventArgs)
        {
            // restore/maximize only with left button
            if (mouseButtonEventArgs.ChangedButton == MouseButton.Left)
            {
                // we can maximize or restore the window if the title bar height is set (also if title bar is hidden)
                var canResize = window.ResizeMode == ResizeMode.CanResizeWithGrip ||
                                window.ResizeMode == ResizeMode.CanResize;
                var mousePos = Mouse.GetPosition(window);
                var isMouseOnTitlebar = mousePos.Y <= window.TitlebarHeight && window.TitlebarHeight > 0;
                if (canResize && isMouseOnTitlebar)
                {
                    if (window.WindowState == WindowState.Maximized)
                    {
                        SystemCommands.RestoreWindow(window);
                    }
                    else
                    {
                        SystemCommands.MaximizeWindow(window);
                    }
                    mouseButtonEventArgs.Handled = true;
                }
            }
        }


        private void IconMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount == 2)
                {
                    Close();
                }
                else
                {
                    ShowSystemMenuPhysicalCoordinates(this, PointToScreen(new Point(0, TitlebarHeight)));
                }
            }
        }

        private void SetVisibilityForIcon()
        {
            if (_icon != null)
            {
                var isVisible = (IconOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.HiddenTitleBar) &&
                                 !ShowTitleBar)
                                || (ShowIconOnTitleBar && ShowTitleBar);
                var iconVisibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
                _icon.Visibility = iconVisibility;
            }
        }

        private static void OnUseNoneWindowStylePropertyChangedCallback(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                // if UseNoneWindowStyle = true no title bar should be shown
                var useNoneWindowStyle = (bool) e.NewValue;
                var window = (CushWindow) d;
                window.ToggleNoneWindowStyle(useNoneWindowStyle);
            }
        }

        private void ToggleNoneWindowStyle(bool useNoneWindowStyle)
        {
            // UseNoneWindowStyle means no title bar, window commands or min, max, close buttons
            if (useNoneWindowStyle)
            {
                ShowTitleBar = false;
            }
            if (LeftWindowCommandsPresenter != null)
            {
                LeftWindowCommandsPresenter.Visibility = useNoneWindowStyle ? Visibility.Collapsed : Visibility.Visible;
            }
            if (RightWindowCommandsPresenter != null)
            {
                RightWindowCommandsPresenter.Visibility = useNoneWindowStyle ? Visibility.Collapsed : Visibility.Visible;
            }
        }


        //protected override void OnStateChanged(EventArgs e)
        //{
        //    if (WindowStateButtons != null)
        //    {
        //        WindowStateButtons.RefreshMaximiseIconState();
        //    }

        //    base.OnStateChanged(e);
        //}

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        protected void TitleBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            var mousePosition = e.GetPosition(this);
            var isIconClick = ShowIconOnTitleBar && mousePosition.X <= TitlebarHeight &&
                              mousePosition.Y <= TitlebarHeight;

            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    if (isIconClick)
                    {
                        if (e.ClickCount == 2)
                        {
                            Close();
                        }
                        else
                        {
                            ShowSystemMenuPhysicalCoordinates(this, PointToScreen(new Point(0, TitlebarHeight)));
                        }
                    }
                    else
                    {
                        var windowHandle = new WindowInteropHelper(this).Handle;
                        UnsafeNativeMethods.ReleaseCapture();

                        var mPoint = Mouse.GetPosition(this);
                        var wpfPoint = PointToScreen(mPoint);
                        var x = Convert.ToUInt16(wpfPoint.X);
                        var y = Convert.ToInt16(wpfPoint.Y);

                        var lParam = x | (y << 16);

                        UnsafeNativeMethods.SendMessage(windowHandle, Constants.WM_NCLBUTTONDOWN, Constants.HT_CAPTION,
                            lParam);
                        if (e.ClickCount == 2 &&
                            (ResizeMode == ResizeMode.CanResizeWithGrip || ResizeMode == ResizeMode.CanResize) &&
                            mPoint.Y <= TitlebarHeight)
                        {
                            WindowState = WindowState == WindowState.Maximized
                                ? WindowState.Normal
                                : WindowState.Maximized;
                        }
                    }
                    break;

                case MouseButton.Right:
                    ShowSystemMenuPhysicalCoordinates(this, PointToScreen(mousePosition));
                    break;
                case MouseButton.Middle:
                case MouseButton.XButton1:
                case MouseButton.XButton2:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected void TitleBarMouseUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
        }

        private void TitleBarMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                _isDragging = false;
            }

            if (_isDragging && WindowState == WindowState.Maximized && ResizeMode != ResizeMode.NoResize)
            {
                // Calculating correct left coordinate for multi-screen system.
                var mouseAbsolute = PointToScreen(Mouse.GetPosition(this));
                var width = RestoreBounds.Width;
                var left = mouseAbsolute.X - width/2;

                // Check if the mouse is at the top of the screen if TitleBar is not visible
                if (_titleBar.Visibility != Visibility.Visible && mouseAbsolute.Y > TitlebarHeight)
                    return;

                // Aligning window's position to fit the screen.
                var virtualScreenWidth = SystemParameters.VirtualScreenWidth;
                left = left + width > virtualScreenWidth ? virtualScreenWidth - width : left;

                var mousePosition = e.MouseDevice.GetPosition(this);

                // When dragging the window down at the very top of the border,
                // move the window a bit upwards to avoid showing the resize handle as soon as the mouse button is released
                Top = mousePosition.Y < 5 ? -5 : mouseAbsolute.Y - mousePosition.Y;
                Left = left;

                // Restore window to normal state.
                WindowState = WindowState.Normal;
            }
        }

        internal T GetPart<T>(string name) where T : DependencyObject
        {
            return (T) GetTemplateChild(name);
        }

        private static void ShowSystemMenuPhysicalCoordinates(Window window, Point physicalScreenLocation)
        {
            if (window == null) return;

            var hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd == IntPtr.Zero || !UnsafeNativeMethods.IsWindow(hwnd))
                return;

            var hmenu = UnsafeNativeMethods.GetSystemMenu(hwnd, false);

            var cmd = UnsafeNativeMethods.TrackPopupMenuEx(hmenu, NonCLSCompliantConstants.TPM_LEFTBUTTON | NonCLSCompliantConstants.TPM_RETURNCMD,
                (int) physicalScreenLocation.X, (int) physicalScreenLocation.Y, hwnd, IntPtr.Zero);
            if (0 != cmd)
                UnsafeNativeMethods.PostMessage(hwnd, NonCLSCompliantConstants.SYSCOMMAND, new IntPtr(cmd), IntPtr.Zero);
        }

        internal void HandleFlyoutStatusChange(Flyout flyout, IEnumerable<Flyout> visibleFlyouts)
        {
            //checks a recently opened flyout's position.
            if (flyout.Position == Position.Left || flyout.Position == Position.Right || flyout.Position == Position.Top)
            {
                //get it's zindex
                var zIndex = flyout.IsOpen ? Panel.GetZIndex(flyout) + 3 : visibleFlyouts.Count() + 2;

                // Note: ShowWindowCommandsOnTop is here for backwards compatibility reasons
                //if the the corresponding behavior has the right flag, set the window commands' and icon zIndex to a number that is higher than the flyout's.
                if (_icon != null)
                {
                    _icon.SetValue(Panel.ZIndexProperty,
                        IconOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.Flyouts) ? zIndex : 1);
                }

                if (LeftWindowCommandsPresenter != null)
                {
                    LeftWindowCommandsPresenter.SetValue(Panel.ZIndexProperty,
                        LeftWindowCommandsOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.Flyouts) ? zIndex : 1);
                }

                if (RightWindowCommandsPresenter != null)
                {
                    RightWindowCommandsPresenter.SetValue(Panel.ZIndexProperty,
                        RightWindowCommandsOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.Flyouts) ? zIndex : 1);
                }

                if (WindowStateButtons != null)
                {
                    WindowStateButtons.SetValue(Panel.ZIndexProperty,
                        WindowStateButtonsOverlayBehavior.HasFlag(WindowCommandsOverlayBehavior.Flyouts) ? zIndex : 1);
                }

                this.HandleWindowCommandsForFlyouts(visibleFlyouts);
            }

            _flyoutModal.Visibility = visibleFlyouts.Any(x => x.IsModal) ? Visibility.Visible : Visibility.Hidden;

            RaiseEvent(new FlyoutStatusChangedRoutedEventArgs(FlyoutsStatusChangedEvent, this)
            {
                ChangedFlyout = flyout
            });
        }

        /// <summary>
        ///     Begins to show the MetroWindow's overlay effect.
        /// </summary>
        /// <returns>A task representing the process.</returns>
        public Task ShowOverlayAsync()
        {
            if (IsOverlayVisible() && _overlayStoryboard == null)
                return new Task(() => { }); //No Task.FromResult in .NET 4.

            Dispatcher.VerifyAccess();

            OverlayBox.Visibility = Visibility.Visible;

            var tcs = new TaskCompletionSource<object>();

            var sb = (Storyboard) Template.Resources["OverlayFastSemiFadeIn"];

            sb = sb.Clone();

            EventHandler completionHandler = null;
            completionHandler = (sender, args) =>
            {
                sb.Completed -= completionHandler;

                if (_overlayStoryboard == sb)
                {
                    _overlayStoryboard = null;
                }

                tcs.TrySetResult(null);
            };

            sb.Completed += completionHandler;

            OverlayBox.BeginStoryboard(sb);

            _overlayStoryboard = sb;

            return tcs.Task;
        }

        /// <summary>
        ///     Begins to hide the MetroWindow's overlay effect.
        /// </summary>
        /// <returns>A task representing the process.</returns>
        public Task HideOverlayAsync()
        {
            if (OverlayBox.Visibility == Visibility.Visible && OverlayBox.Opacity == 0.0)
                return new Task(() => { }); //No Task.FromResult in .NET 4.

            Dispatcher.VerifyAccess();

            var tcs = new TaskCompletionSource<object>();

            var sb = (Storyboard) Template.Resources["OverlayFastSemiFadeOut"];

            sb = sb.Clone();

            EventHandler completionHandler = null;
            completionHandler = (sender, args) =>
            {
                sb.Completed -= completionHandler;

                if (_overlayStoryboard == sb)
                {
                    OverlayBox.Visibility = Visibility.Hidden;
                    _overlayStoryboard = null;
                }

                tcs.TrySetResult(null);
            };

            sb.Completed += completionHandler;

            OverlayBox.BeginStoryboard(sb);

            _overlayStoryboard = sb;

            return tcs.Task;
        }

        public bool IsOverlayVisible()
        {
            return OverlayBox.Visibility == Visibility.Visible && OverlayBox.Opacity >= 0.7;
        }

        public void ShowOverlay()
        {
            OverlayBox.Visibility = Visibility.Visible;
            OverlayBox.SetCurrentValue(OpacityProperty, 0.7);
        }

        public void HideOverlay()
        {
            OverlayBox.SetCurrentValue(OpacityProperty, 0.0);
            OverlayBox.Visibility = Visibility.Hidden;
        }

        #region Fields and Properties

        private const string PART_Icon = "PART_Icon";
        private const string PART_TitleBar = "PART_TitleBar";
        private const string PART_WindowTitleBackground = "PART_WindowTitleBackground";
        private const string PART_WindowTitleThumb = "PART_WindowTitleThumb";
        private const string PART_WindowStateButtons = "PART_WindowStateButtons";
        private const string PART_LeftWindowCommands = "PART_LeftWindowCommands";
        private const string PART_RightWindowCommands = "PART_RightWindowCommands";
        private const string PART_OverlayBox = "PART_OverlayBox";
        private const string PART_DialogContainer = "PART_DialogContainer";
        private const string PART_FlyoutModal = "PART_FlyoutModal";

        public static readonly DependencyProperty DialogOptionsProperty = DependencyProperty.Register("DialogOptions",
            typeof (DialogSettings), typeof (CushWindow),
            new PropertyMetadata(new DialogSettings {AnimateHide = false, AnimateShow = true}));

        public DialogSettings DialogOptions
        {
            get { return (DialogSettings) GetValue(DialogOptionsProperty); }
            set { SetValue(DialogOptionsProperty, value); }
        }

        public static readonly DependencyProperty ShowIconOnTitleBarProperty =
            DependencyProperty.Register("ShowIconOnTitleBar", typeof (bool), typeof (CushWindow),
                new PropertyMetadata(true));

        public static readonly DependencyProperty IconEdgeModeProperty = DependencyProperty.Register("IconEdgeMode",
            typeof (EdgeMode), typeof (CushWindow), new PropertyMetadata(EdgeMode.Aliased));

        public static readonly DependencyProperty IconBitmapScalingModeProperty =
            DependencyProperty.Register("IconBitmapScalingMode", typeof (BitmapScalingMode), typeof (CushWindow),
                new PropertyMetadata(BitmapScalingMode.HighQuality));

        public static readonly DependencyProperty ShowTitleBarProperty = DependencyProperty.Register("ShowTitleBar",
            typeof (bool), typeof (CushWindow), new PropertyMetadata(true));

        public static readonly DependencyProperty ShowMinButtonProperty = DependencyProperty.Register("ShowMinButton",
            typeof (bool), typeof (CushWindow), new PropertyMetadata(true));

        public static readonly DependencyProperty ShowCloseButtonProperty =
            DependencyProperty.Register("ShowCloseButton", typeof (bool), typeof (CushWindow),
                new PropertyMetadata(true));

        public static readonly DependencyProperty ShowMaxRestoreButtonProperty =
            DependencyProperty.Register("ShowMaxRestoreButton", typeof (bool), typeof (CushWindow),
                new PropertyMetadata(true));

        public static readonly DependencyProperty TitlebarHeightProperty = DependencyProperty.Register(
            "TitlebarHeight", typeof (int), typeof (CushWindow), new PropertyMetadata(25));

        public static readonly DependencyProperty TitleCapsProperty = DependencyProperty.Register("TitleCaps",
            typeof (bool), typeof (CushWindow), new PropertyMetadata(true));

        public static readonly DependencyProperty SaveWindowPositionProperty =
            DependencyProperty.Register("SaveWindowPosition", typeof (bool), typeof (CushWindow),
                new PropertyMetadata(false));

        public static readonly DependencyProperty WindowPlacementSettingsProperty =
            DependencyProperty.Register("WindowPlacementSettings", typeof (IWindowPlacementSettings),
                typeof (CushWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register("TitleForeground", typeof (Brush), typeof (CushWindow));

        public static readonly DependencyProperty IgnoreTaskbarOnMaximizeProperty =
            DependencyProperty.Register("IgnoreTaskbarOnMaximize", typeof (bool), typeof (CushWindow),
                new PropertyMetadata(false));

        public static readonly DependencyProperty GlowBrushProperty = DependencyProperty.Register("GlowBrush",
            typeof (SolidColorBrush), typeof (CushWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty FlyoutsProperty = DependencyProperty.Register("Flyouts",
            typeof (FlyoutsControl), typeof (CushWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty WindowTransitionsEnabledProperty =
            DependencyProperty.Register("WindowTransitionsEnabled", typeof (bool), typeof (CushWindow),
                new PropertyMetadata(true));

        public static readonly DependencyProperty UseNoneWindowStyleProperty =
            DependencyProperty.Register("UseNoneWindowStyle", typeof (bool), typeof (CushWindow), new PropertyMetadata(
                false, OnUseNoneWindowStylePropertyChangedCallback));

        public static readonly DependencyProperty ShowSystemMenuOnRightClickProperty =
            DependencyProperty.Register("ShowSystemMenuOnRightClick", typeof (bool), typeof (CushWindow),
                new PropertyMetadata(true));

        public static readonly DependencyProperty IsMinButtonEnabledProperty =
            DependencyProperty.Register("IsMinButtonEnabled", typeof (bool), typeof (CushWindow),
                new PropertyMetadata(true));

        public static readonly DependencyProperty IsMaxRestoreButtonEnabledProperty =
            DependencyProperty.Register("IsMaxRestoreButtonEnabled", typeof (bool), typeof (CushWindow),
                new PropertyMetadata(true));

        public static readonly DependencyProperty IsCloseButtonEnabledProperty =
            DependencyProperty.Register("IsCloseButtonEnabled", typeof (bool), typeof (CushWindow),
                new PropertyMetadata(true));

        public static readonly DependencyProperty WindowTitleBrushProperty =
            DependencyProperty.Register("WindowTitleBrush", typeof (Brush), typeof (CushWindow),
                new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty TitleTemplateProperty = DependencyProperty.Register("TitleTemplate",
            typeof (DataTemplate), typeof (CushWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty NonActiveGlowBrushProperty =
            DependencyProperty.Register("NonActiveGlowBrush", typeof (Brush), typeof (CushWindow),
                new PropertyMetadata(new SolidColorBrush(Color.FromRgb(153, 153, 153)))); // #999999

        public static readonly DependencyProperty NonActiveBorderBrushProperty =
            DependencyProperty.Register("NonActiveBorderBrush", typeof (Brush), typeof (CushWindow),
                new PropertyMetadata(Brushes.Gray));

        public static readonly DependencyProperty NonActiveWindowTitleBrushProperty =
            DependencyProperty.Register("NonActiveWindowTitleBrush", typeof (Brush), typeof (CushWindow),
                new PropertyMetadata(Brushes.Gray));

        public static readonly DependencyProperty IconTemplateProperty = DependencyProperty.Register("IconTemplate",
            typeof (DataTemplate), typeof (CushWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty LeftWindowCommandsProperty =
            DependencyProperty.Register("LeftWindowCommands", typeof (WindowCommands), typeof (CushWindow),
                new PropertyMetadata(null));

        public static readonly DependencyProperty RightWindowCommandsProperty =
            DependencyProperty.Register("RightWindowCommands", typeof (WindowCommands), typeof (CushWindow),
                new PropertyMetadata(null));

        public static readonly DependencyProperty TitleAlignmentProperty = DependencyProperty.Register(
            "TitleAlignment", typeof (HorizontalAlignment), typeof (CushWindow),
            new PropertyMetadata(HorizontalAlignment.Stretch));

        public static readonly DependencyProperty OverrideDefaultWindowCommandsBrushProperty =
            DependencyProperty.Register("OverrideDefaultWindowCommandsBrush", typeof (SolidColorBrush),
                typeof (CushWindow));

        public static readonly DependencyProperty LeftWindowCommandsOverlayBehaviorProperty =
            DependencyProperty.Register("LeftWindowCommandsOverlayBehavior", typeof (WindowCommandsOverlayBehavior),
                typeof (CushWindow), new PropertyMetadata(WindowCommandsOverlayBehavior.Always));

        public static readonly DependencyProperty RightWindowCommandsOverlayBehaviorProperty =
            DependencyProperty.Register("RightWindowCommandsOverlayBehavior", typeof (WindowCommandsOverlayBehavior),
                typeof (CushWindow), new PropertyMetadata(WindowCommandsOverlayBehavior.Always));

        public static readonly DependencyProperty WindowStateButtonsOverlayBehaviorProperty =
            DependencyProperty.Register("WindowStateButtonsOverlayBehavior", typeof (WindowCommandsOverlayBehavior),
                typeof (CushWindow), new PropertyMetadata(WindowCommandsOverlayBehavior.Always));

        public static readonly DependencyProperty IconOverlayBehaviorProperty =
            DependencyProperty.Register("IconOverlayBehavior", typeof (WindowCommandsOverlayBehavior),
                typeof (CushWindow), new PropertyMetadata(WindowCommandsOverlayBehavior.Never));


        public static readonly DependencyProperty IsWindowDraggableProperty =
            DependencyProperty.Register("IsWindowDraggable", typeof (bool), typeof (CushWindow),
                new PropertyMetadata(true));

        public static readonly RoutedEvent FlyoutsStatusChangedEvent = EventManager.RegisterRoutedEvent(
            "FlyoutsStatusChanged", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (CushWindow));

        private Rectangle _flyoutModal;
        private UIElement _icon;

        private bool _isDragging;
        private UIElement _titleBar;
        private UIElement _titleBarBackground;
        private Thumb _windowTitleThumb;
        private Storyboard _overlayStoryboard;

        internal ContentPresenter LeftWindowCommandsPresenter;
        internal ContentPresenter RightWindowCommandsPresenter;
        internal Grid OverlayBox;
        internal Grid DialogContainer;


        /// <summary>
        ///     Gets/sets if the min button is enabled.
        /// </summary>
        public bool IsMinButtonEnabled
        {
            get { return (bool) GetValue(IsMinButtonEnabledProperty); }
            set { SetValue(IsMinButtonEnabledProperty, value); }
        }

        /// <summary>
        ///     Gets/sets if the max/restore button is enabled.
        /// </summary>
        public bool IsMaxRestoreButtonEnabled
        {
            get { return (bool) GetValue(IsMaxRestoreButtonEnabledProperty); }
            set { SetValue(IsMaxRestoreButtonEnabledProperty, value); }
        }

        /// <summary>
        ///     Gets/sets if the close button is enabled.
        /// </summary>
        public bool IsCloseButtonEnabled
        {
            get { return (bool) GetValue(IsCloseButtonEnabledProperty); }
            set { SetValue(IsCloseButtonEnabledProperty, value); }
        }

        /// <summary>
        ///     Allows easy handling of window commands brush. Theme is also applied based on this brush.
        /// </summary>
        public SolidColorBrush OverrideDefaultWindowCommandsBrush
        {
            get { return (SolidColorBrush) GetValue(OverrideDefaultWindowCommandsBrushProperty); }
            set { SetValue(OverrideDefaultWindowCommandsBrushProperty, value); }
        }

        /// <summary>
        ///     Gets/sets if the the system menu should popup on right click.
        /// </summary>
        public bool ShowSystemMenuOnRightClick
        {
            get { return (bool) GetValue(ShowSystemMenuOnRightClickProperty); }
            set { SetValue(ShowSystemMenuOnRightClickProperty, value); }
        }

        /// <summary>
        ///     Gets/sets the left window commands that hosts the user commands.
        /// </summary>
        public WindowCommands LeftWindowCommands
        {
            get { return (WindowCommands) GetValue(LeftWindowCommandsProperty); }
            set { SetValue(LeftWindowCommandsProperty, value); }
        }

        /// <summary>
        ///     Gets/sets the right window commands that hosts the user commands.
        /// </summary>
        public WindowCommands RightWindowCommands
        {
            get { return (WindowCommands) GetValue(RightWindowCommandsProperty); }
            set { SetValue(RightWindowCommandsProperty, value); }
        }

        /// <summary>
        ///     Gets/sets the brush used for the Window's non-active glow.
        /// </summary>
        public Brush NonActiveGlowBrush
        {
            get { return (Brush) GetValue(NonActiveGlowBrushProperty); }
            set { SetValue(NonActiveGlowBrushProperty, value); }
        }

        /// <summary>
        ///     Gets/sets the brush used for the Window's non-active border.
        /// </summary>
        public Brush NonActiveBorderBrush
        {
            get { return (Brush) GetValue(NonActiveBorderBrushProperty); }
            set { SetValue(NonActiveBorderBrushProperty, value); }
        }

        /// <summary>
        ///     Gets/sets the brush used for the Window's non-active title bar.
        /// </summary>
        public Brush NonActiveWindowTitleBrush
        {
            get { return (Brush) GetValue(NonActiveWindowTitleBrushProperty); }
            set { SetValue(NonActiveWindowTitleBrushProperty, value); }
        }

        /// <summary>
        ///     Gets/sets the icon content template to show a custom icon.
        /// </summary>
        public DataTemplate IconTemplate
        {
            get { return (DataTemplate) GetValue(IconTemplateProperty); }
            set { SetValue(IconTemplateProperty, value); }
        }


        /// <summary>
        ///     Gets/sets the brush used for the Window's title bar.
        /// </summary>
        public Brush WindowTitleBrush
        {
            get { return (Brush) GetValue(WindowTitleBrushProperty); }
            set { SetValue(WindowTitleBrushProperty, value); }
        }

        /// <summary>
        ///     Gets/sets edge mode of the titlebar icon.
        /// </summary>
        public EdgeMode IconEdgeMode
        {
            get { return (EdgeMode) GetValue(IconEdgeModeProperty); }
            set { SetValue(IconEdgeModeProperty, value); }
        }

        /// <summary>
        ///     Gets/sets bitmap scaling mode of the titlebar icon.
        /// </summary>
        public BitmapScalingMode IconBitmapScalingMode
        {
            get { return (BitmapScalingMode) GetValue(IconBitmapScalingModeProperty); }
            set { SetValue(IconBitmapScalingModeProperty, value); }
        }

        /// <summary>
        ///     Gets/sets the title horizontal alignment.
        /// </summary>
        public HorizontalAlignment TitleAlignment
        {
            get { return (HorizontalAlignment) GetValue(TitleAlignmentProperty); }
            set { SetValue(TitleAlignmentProperty, value); }
        }

        public bool WindowTransitionsEnabled
        {
            get { return (bool) GetValue(WindowTransitionsEnabledProperty); }
            set { SetValue(WindowTransitionsEnabledProperty, value); }
        }

        public FlyoutsControl Flyouts
        {
            get { return (FlyoutsControl) GetValue(FlyoutsProperty); }
            set { SetValue(FlyoutsProperty, value); }
        }

        public WindowCommands WindowStateButtons { get; set; }

        public bool IgnoreTaskbarOnMaximize
        {
            get { return (bool) GetValue(IgnoreTaskbarOnMaximizeProperty); }
            set { SetValue(IgnoreTaskbarOnMaximizeProperty, value); }
        }

        public Brush TitleForeground
        {
            get { return (Brush) GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }

        /// <summary>
        ///     Gets/sets the title content template to show a custom title.
        /// </summary>
        public DataTemplate TitleTemplate
        {
            get { return (DataTemplate) GetValue(TitleTemplateProperty); }
            set { SetValue(TitleTemplateProperty, value); }
        }

        /// <summary>
        ///     Gets/sets whether the WindowStyle is None or not.
        /// </summary>
        public bool UseNoneWindowStyle
        {
            get { return (bool) GetValue(UseNoneWindowStyleProperty); }
            set { SetValue(UseNoneWindowStyleProperty, value); }
        }

        public bool SaveWindowPosition
        {
            get { return (bool) GetValue(SaveWindowPositionProperty); }
            set { SetValue(SaveWindowPositionProperty, value); }
        }

        public IWindowPlacementSettings WindowPlacementSettings
        {
            get { return (IWindowPlacementSettings) GetValue(WindowPlacementSettingsProperty); }
            set { SetValue(WindowPlacementSettingsProperty, value); }
        }

        public bool ShowIconOnTitleBar
        {
            get { return (bool) GetValue(ShowIconOnTitleBarProperty); }
            set { SetValue(ShowIconOnTitleBarProperty, value); }
        }

        public bool ShowTitleBar
        {
            get { return (bool) GetValue(ShowTitleBarProperty); }
            set { SetValue(ShowTitleBarProperty, value); }
        }

        public bool ShowMinButton
        {
            get { return (bool) GetValue(ShowMinButtonProperty); }
            set { SetValue(ShowMinButtonProperty, value); }
        }

        public bool ShowCloseButton
        {
            get { return (bool) GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }

        public bool IsWindowDraggable
        {
            get { return (bool) GetValue(IsWindowDraggableProperty); }
            set { SetValue(IsWindowDraggableProperty, value); }
        }

        public int TitlebarHeight
        {
            get { return (int) GetValue(TitlebarHeightProperty); }
            set { SetValue(TitlebarHeightProperty, value); }
        }

        public bool ShowMaxRestoreButton
        {
            get { return (bool) GetValue(ShowMaxRestoreButtonProperty); }
            set { SetValue(ShowMaxRestoreButtonProperty, value); }
        }

        public bool TitleCaps
        {
            get { return (bool) GetValue(TitleCapsProperty); }
            set { SetValue(TitleCapsProperty, value); }
        }

        public SolidColorBrush GlowBrush
        {
            get { return (SolidColorBrush) GetValue(GlowBrushProperty); }
            set { SetValue(GlowBrushProperty, value); }
        }

        public string WindowTitle => TitleCaps ? Title.ToUpper() : Title;

        public WindowCommandsOverlayBehavior LeftWindowCommandsOverlayBehavior
        {
            get { return (WindowCommandsOverlayBehavior) GetValue(LeftWindowCommandsOverlayBehaviorProperty); }
            set { SetValue(LeftWindowCommandsOverlayBehaviorProperty, value); }
        }

        public WindowCommandsOverlayBehavior RightWindowCommandsOverlayBehavior
        {
            get { return (WindowCommandsOverlayBehavior) GetValue(RightWindowCommandsOverlayBehaviorProperty); }
            set { SetValue(RightWindowCommandsOverlayBehaviorProperty, value); }
        }

        public WindowCommandsOverlayBehavior WindowStateButtonsOverlayBehavior
        {
            get { return (WindowCommandsOverlayBehavior) GetValue(WindowStateButtonsOverlayBehaviorProperty); }
            set { SetValue(WindowStateButtonsOverlayBehaviorProperty, value); }
        }

        public WindowCommandsOverlayBehavior IconOverlayBehavior
        {
            get { return (WindowCommandsOverlayBehavior) GetValue(IconOverlayBehaviorProperty); }
            set { SetValue(IconOverlayBehaviorProperty, value); }
        }

        // Provide CLR accessors for the event
        public event RoutedEventHandler FlyoutsStatusChanged
        {
            add { AddHandler(FlyoutsStatusChangedEvent, value); }
            remove { RemoveHandler(FlyoutsStatusChangedEvent, value); }
        }

        #endregion

        /// <summary>
        ///     Gets or sets the current <see cref="T:IColorScheme"/>.
        /// </summary>
        public IColorScheme ColorScheme { get; set; }
    }
}