using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Cush.WPF.ColorSchemes;
using Cush.WPF.Interfaces;

namespace Cush.WPF.Controls
{
    /// <summary>
    ///     A sliding panel control that is hosted in a MetroWindow via a FlyoutsControl.
    ///     <see cref="CushWindow" />
    ///     <seealso cref="FlyoutsControl" />
    /// </summary>
    [TemplatePart(Name = "PART_BackButton", Type = typeof (Button))]
    [TemplatePart(Name = "PART_WindowTitleThumb", Type = typeof (Thumb))]
    [TemplatePart(Name = "_header", Type = typeof (ContentPresenter))]
    [TemplatePart(Name = "_content", Type = typeof (ContentPresenter))]
    public class Flyout : ContentControl, ISchemedElement
    {
        /// <summary>
        ///     An event that is raised when IsOpen changes.
        /// </summary>
        public static readonly RoutedEvent IsOpenChangedEvent =
            EventManager.RegisterRoutedEvent("IsOpenChanged", RoutingStrategy.Bubble,
                typeof (RoutedEventHandler), typeof (Flyout));

        /// <summary>
        ///     An event that is raised when the closing animation has finished.
        /// </summary>
        public static readonly RoutedEvent ClosingFinishedEvent =
            EventManager.RegisterRoutedEvent("ClosingFinished", RoutingStrategy.Bubble,
                typeof (RoutedEventHandler), typeof (Flyout));

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof (string),
            typeof (Flyout), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position",
            typeof (Position), typeof (Flyout), new PropertyMetadata(Position.Left, PositionChanged));

        public static readonly DependencyProperty IsPinnedProperty = DependencyProperty.Register("IsPinned",
            typeof (bool), typeof (Flyout), new PropertyMetadata(true));

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof (bool),
            typeof (Flyout),
            new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                IsOpenedChanged));

        public static readonly DependencyProperty AnimateOnPositionChangeProperty =
            DependencyProperty.Register("AnimateOnPositionChange", typeof (bool), typeof (Flyout),
                new PropertyMetadata(true));

        public static readonly DependencyProperty AnimateOpacityProperty = DependencyProperty.Register(
            "AnimateOpacity", typeof (bool), typeof (Flyout),
            new FrameworkPropertyMetadata(false, AnimateOpacityChanged));

        public static readonly DependencyProperty IsModalProperty = DependencyProperty.Register("IsModal", typeof (bool),
            typeof (Flyout));

        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(
            "HeaderTemplate", typeof (DataTemplate), typeof (Flyout));

        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.RegisterAttached("CloseCommand", typeof (ICommand), typeof (Flyout),
                new UIPropertyMetadata(null));

        public static readonly DependencyProperty ThemeProperty = DependencyProperty.Register("Theme",
            typeof (FlyoutTheme), typeof (Flyout), new FrameworkPropertyMetadata(FlyoutTheme.Adapt, OnThemeChanged));

        public static readonly DependencyProperty ExternalCloseButtonProperty =
            DependencyProperty.Register("ExternalCloseButton", typeof (MouseButton), typeof (Flyout),
                new PropertyMetadata(MouseButton.Left));

        public static readonly DependencyProperty CloseButtonVisibilityProperty =
            DependencyProperty.Register("CloseButtonVisibility", typeof (Visibility), typeof (Flyout),
                new FrameworkPropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty TitleVisibilityProperty =
            DependencyProperty.Register("TitleVisibility", typeof (Visibility), typeof (Flyout),
                new FrameworkPropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty AreAnimationsEnabledProperty =
            DependencyProperty.Register("AreAnimationsEnabled", typeof (bool), typeof (Flyout),
                new PropertyMetadata(true));

        public static readonly DependencyProperty FocusedElementProperty = DependencyProperty.Register(
            "FocusedElement", typeof (FrameworkElement), typeof (Flyout), new UIPropertyMetadata(null));

        public static readonly DependencyProperty AllowFocusElementProperty =
            DependencyProperty.Register("AllowFocusElement", typeof (bool), typeof (Flyout), new PropertyMetadata(true));

        ContentPresenter _content;
        SplineDoubleKeyFrame _fadeOutFrame;
        ContentPresenter _header;
        SplineDoubleKeyFrame _hideFrame;
        SplineDoubleKeyFrame _hideFrameY;
        Storyboard _hideStoryboard;

        Grid _root;
        SplineDoubleKeyFrame _showFrame;
        SplineDoubleKeyFrame _showFrameY;
        Thumb _windowTitleThumb;

        private CushWindow _parentWindow;

        static Flyout()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (Flyout), new FrameworkPropertyMetadata(typeof (Flyout)));
        }

        public Flyout()
        {
            ColorSchemeManager.Register(this);
            Loaded += (sender, args) => UpdateFlyoutTheme();
        }

        internal PropertyChangeNotifier IsOpenPropertyChangeNotifier { get; set; }
        internal PropertyChangeNotifier ThemePropertyChangeNotifier { get; set; }

        public bool AreAnimationsEnabled
        {
            get { return (bool) GetValue(AreAnimationsEnabledProperty); }
            set { SetValue(AreAnimationsEnabledProperty, value); }
        }

        /// <summary>
        ///     Gets/sets if the title is visible in this flyout.
        /// </summary>
        public Visibility TitleVisibility
        {
            get { return (Visibility) GetValue(TitleVisibilityProperty); }
            set { SetValue(TitleVisibilityProperty, value); }
        }

        /// <summary>
        ///     Gets/sets if the close button is visible in this flyout.
        /// </summary>
        public Visibility CloseButtonVisibility
        {
            get { return (Visibility) GetValue(CloseButtonVisibilityProperty); }
            set { SetValue(CloseButtonVisibilityProperty, value); }
        }

        /// <summary>
        ///     An ICommand that executes when the flyout's close button is clicked.
        ///     Note that this won't execute when <see cref="IsOpen" /> is set to <c>false</c>.
        /// </summary>
        public ICommand CloseCommand
        {
            get { return (ICommand) GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }

        /// <summary>
        ///     A DataTemplate for the flyout's header.
        /// </summary>
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate) GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        /// <summary>
        ///     Gets/sets whether this flyout is visible.
        /// </summary>
        public bool IsOpen
        {
            get { return (bool) GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        /// <summary>
        ///     Gets/sets whether this flyout uses the open/close animation when changing the <see cref="Position" /> property.
        ///     (default is true)
        /// </summary>
        public bool AnimateOnPositionChange
        {
            get { return (bool) GetValue(AnimateOnPositionChangeProperty); }
            set { SetValue(AnimateOnPositionChangeProperty, value); }
        }

        /// <summary>
        ///     Gets/sets whether this flyout animates the opacity of the flyout when opening/closing.
        /// </summary>
        public bool AnimateOpacity
        {
            get { return (bool) GetValue(AnimateOpacityProperty); }
            set { SetValue(AnimateOpacityProperty, value); }
        }

        /// <summary>
        ///     Gets/sets whether this flyout stays open when the user clicks outside of it.
        /// </summary>
        public bool IsPinned
        {
            get { return (bool) GetValue(IsPinnedProperty); }
            set { SetValue(IsPinnedProperty, value); }
        }

        /// <summary>
        ///     Gets/sets the mouse button that closes the flyout on an external mouse click.
        /// </summary>
        public MouseButton ExternalCloseButton
        {
            get { return (MouseButton) GetValue(ExternalCloseButtonProperty); }
            set { SetValue(ExternalCloseButtonProperty, value); }
        }

        /// <summary>
        ///     Gets/sets whether this flyout is modal.
        /// </summary>
        public bool IsModal
        {
            get { return (bool) GetValue(IsModalProperty); }
            set { SetValue(IsModalProperty, value); }
        }

        /// <summary>
        ///     Gets/sets this flyout's position in the FlyoutsControl/MetroWindow.
        /// </summary>
        public Position Position
        {
            get { return (Position) GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        /// <summary>
        ///     Gets/sets the flyout's header.
        /// </summary>
        public string Header
        {
            get { return (string) GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the theme of this flyout.
        /// </summary>
        public FlyoutTheme Theme
        {
            get { return (FlyoutTheme) GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the focused element.
        /// </summary>
        public FrameworkElement FocusedElement
        {
            get { return (FrameworkElement) GetValue(FocusedElementProperty); }
            set { SetValue(FocusedElementProperty, value); }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the flyout should try focus an element.
        /// </summary>
        public bool AllowFocusElement
        {
            get { return (bool) GetValue(AllowFocusElementProperty); }
            set { SetValue(AllowFocusElementProperty, value); }
        }

        private CushWindow ParentWindow => _parentWindow ?? (_parentWindow = this.TryFindParent<CushWindow>());

        public event RoutedEventHandler IsOpenChanged
        {
            add { AddHandler(IsOpenChangedEvent, value); }
            remove { RemoveHandler(IsOpenChangedEvent, value); }
        }

        public event RoutedEventHandler ClosingFinished
        {
            add { AddHandler(ClosingFinishedEvent, value); }
            remove { RemoveHandler(ClosingFinishedEvent, value); }
        }

        private void UpdateFlyoutTheme()
        {
            var flyoutsControl = this.TryFindParent<FlyoutsControl>();

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                Visibility = flyoutsControl != null ? Visibility.Collapsed : Visibility.Visible;
            }

            var window = ParentWindow;
            if (window != null)
            {
                var windowScheme = window.ColorScheme;

                if (windowScheme != null)
                {
                    var accent = windowScheme.Accent;

                    ChangeFlyoutTheme(accent, windowScheme.Theme);
                }

                // we must certain to get the right foreground for window commands and buttons
                if (flyoutsControl != null && IsOpen)
                {
                    flyoutsControl.HandleFlyoutStatusChange(this, window);
                }
            }
        }

        internal void ChangeFlyoutTheme(IKeyedResourceContainer windowAccent, IKeyedResourceContainer windowTheme)
        {
            // Beware: Über-dumb code ahead!
            switch (Theme)
            {
                case FlyoutTheme.Accent:
                    ColorScheme.Accent = windowAccent;
                    ColorScheme.Theme = windowTheme;
                    //ColorSchemeManager.UpdateColorScheme(windowAccent, windowTheme);
                    SetResourceReference(BackgroundProperty, "HighlightBrush");
                    SetResourceReference(ForegroundProperty, "IdealForegroundColorBrush");
                    break;

                case FlyoutTheme.Adapt:
                    ColorScheme.Accent = windowAccent;
                    ColorScheme.Theme = windowTheme;
                    break;

                //case FlyoutTheme.Dark:
                //    ColorSchemeManager.UpdateColorScheme(windowAccent, ColorSchemeManager.GetTheme("BaseDark"));
                //    break;

                //case FlyoutTheme.Light:
                //    ColorSchemeManager.UpdateColorScheme(this.Resources, windowAccent, ColorSchemeManager.GetTheme("BaseLight"));
                //    break;
            }
        }

        private void UpdateOpacityChange()
        {
            if (_root == null || _fadeOutFrame == null || DesignerProperties.GetIsInDesignMode(this)) return;
            if (!AnimateOpacity)
            {
                _fadeOutFrame.Value = 1;
                _root.Opacity = 1;
            }
            else
            {
                _fadeOutFrame.Value = 0;
                if (!IsOpen) _root.Opacity = 0;
            }
        }

        private static void IsOpenedChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var flyout = (Flyout) dependencyObject;

            Action openedChangedAction = () =>
            {
                if (e.NewValue != e.OldValue)
                {
                    if (flyout.AreAnimationsEnabled)
                    {
                        if ((bool) e.NewValue)
                        {
                            if (flyout._hideStoryboard != null)
                            {
                                // don't let the storyboard end it's completed event
                                // otherwise it could be hidden on start
                                flyout._hideStoryboard.Completed -= flyout.HideStoryboard_Completed;
                            }
                            flyout.Visibility = Visibility.Visible;
                            flyout.ApplyAnimation(flyout.Position, flyout.AnimateOpacity);
                            flyout.TryFocusElement();
                        }
                        else
                        {
                            // focus the Flyout itself to avoid nasty FocusVisual painting (it's visible until the Flyout is closed)
                            flyout.Focus();
                            if (flyout._hideStoryboard != null)
                            {
                                flyout._hideStoryboard.Completed += flyout.HideStoryboard_Completed;
                            }
                            else
                            {
                                flyout.Hide();
                            }
                        }
                        VisualStateManager.GoToState(flyout, (bool) e.NewValue == false ? "Hide" : "Show", true);
                    }
                    else
                    {
                        if ((bool) e.NewValue)
                        {
                            flyout.Visibility = Visibility.Visible;
                            flyout.TryFocusElement();
                        }
                        else
                        {
                            // focus the Flyout itself to avoid nasty FocusVisual painting (it's visible until the Flyout is closed)
                            flyout.Focus();
                            flyout.Hide();
                        }
                        VisualStateManager.GoToState(flyout, (bool) e.NewValue == false ? "HideDirect" : "ShowDirect",
                            true);
                    }
                }

                flyout.RaiseEvent(new RoutedEventArgs(IsOpenChangedEvent));
            };

            flyout.Dispatcher.BeginInvoke(DispatcherPriority.Background, openedChangedAction);
        }

        private void HideStoryboard_Completed(object sender, EventArgs e)
        {
            _hideStoryboard.Completed -= HideStoryboard_Completed;

            Hide();
        }

        private void Hide()
        {
            // hide the flyout, we should get better performance and prevent showing the flyout on any resizing events
            Visibility = Visibility.Hidden;

            RaiseEvent(new RoutedEventArgs(ClosingFinishedEvent));
        }

        private void TryFocusElement()
        {
            if (AllowFocusElement)
            {
                // first focus itself
                Focus();

                if (FocusedElement != null)
                {
                    FocusedElement.Focus();
                }
                else if (_content == null || !_content.MoveFocus(new TraversalRequest(FocusNavigationDirection.First)))
                {
                    _header?.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                }
            }
        }

        private static void OnThemeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var flyout = (Flyout) dependencyObject;
            flyout.UpdateFlyoutTheme();
        }

        private static void AnimateOpacityChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var flyout = (Flyout) dependencyObject;
            flyout.UpdateOpacityChange();
        }

        private static void PositionChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var flyout = (Flyout) dependencyObject;
            var wasOpen = flyout.IsOpen;
            if (wasOpen && flyout.AnimateOnPositionChange)
            {
                flyout.ApplyAnimation((Position) e.NewValue, flyout.AnimateOpacity);
                VisualStateManager.GoToState(flyout, "Hide", true);
            }
            else
            {
                flyout.ApplyAnimation((Position) e.NewValue, flyout.AnimateOpacity, false);
            }

            if (wasOpen && flyout.AnimateOnPositionChange)
            {
                flyout.ApplyAnimation((Position) e.NewValue, flyout.AnimateOpacity);
                VisualStateManager.GoToState(flyout, "Show", true);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _root = (Grid) GetTemplateChild("_root");
            if (_root == null)
                return;

            _header = (ContentPresenter) GetTemplateChild("_header");
            _content = (ContentPresenter) GetTemplateChild("_content");
            _windowTitleThumb = GetTemplateChild("PART_WindowTitleThumb") as Thumb;

            if (_windowTitleThumb != null)
            {
                _windowTitleThumb.DragDelta -= WindowTitleThumbMoveOnDragDelta;
                _windowTitleThumb.MouseDoubleClick -= WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                _windowTitleThumb.MouseRightButtonUp -= WindowTitleThumbSystemMenuOnMouseRightButtonUp;

                _windowTitleThumb.DragDelta += WindowTitleThumbMoveOnDragDelta;
                _windowTitleThumb.MouseDoubleClick += WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                _windowTitleThumb.MouseRightButtonUp += WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }

            _hideStoryboard = (Storyboard) GetTemplateChild("HideStoryboard");
            _hideFrame = (SplineDoubleKeyFrame) GetTemplateChild("_hideFrame");
            _hideFrameY = (SplineDoubleKeyFrame) GetTemplateChild("_hideFrameY");
            _showFrame = (SplineDoubleKeyFrame) GetTemplateChild("_showFrame");
            _showFrameY = (SplineDoubleKeyFrame) GetTemplateChild("_showFrameY");
            _fadeOutFrame = (SplineDoubleKeyFrame) GetTemplateChild("_fadeOutFrame");

            if (_hideFrame == null || _showFrame == null || _hideFrameY == null || _showFrameY == null ||
                _fadeOutFrame == null)
                return;

            ApplyAnimation(Position, AnimateOpacity);
        }

        protected internal void CleanUp(FlyoutsControl flyoutsControl)
        {
            if (_windowTitleThumb != null)
            {
                _windowTitleThumb.DragDelta -= WindowTitleThumbMoveOnDragDelta;
                _windowTitleThumb.MouseDoubleClick -= WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                _windowTitleThumb.MouseRightButtonUp -= WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }
            _parentWindow = null;
        }

        private void WindowTitleThumbMoveOnDragDelta(object sender, DragDeltaEventArgs dragDeltaEventArgs)
        {
            var window = ParentWindow;
            if (window != null && Position != Position.Bottom)
            {
                CushWindow.DoWindowTitleThumbMoveOnDragDelta(window, dragDeltaEventArgs);
            }
        }

        private void WindowTitleThumbChangeWindowStateOnMouseDoubleClick(object sender,
            MouseButtonEventArgs mouseButtonEventArgs)
        {
            var window = ParentWindow;
            if (window != null && Position != Position.Bottom)
            {
                CushWindow.DoWindowTitleThumbChangeWindowStateOnMouseDoubleClick(window, mouseButtonEventArgs);
            }
        }

        private void WindowTitleThumbSystemMenuOnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var window = ParentWindow;
            if (window != null && Position != Position.Bottom)
            {
                CushWindow.DoWindowTitleThumbSystemMenuOnMouseRightButtonUp(window, e);
            }
        }

        internal void ApplyAnimation(Position position, bool animateOpacity, bool resetShowFrame = true)
        {
            if (_root == null || _hideFrame == null || _showFrame == null || _hideFrameY == null || _showFrameY == null ||
                _fadeOutFrame == null)
                return;

            if (Position == Position.Left || Position == Position.Right)
                _showFrame.Value = 0;
            if (Position == Position.Top || Position == Position.Bottom)
                _showFrameY.Value = 0;

            // I mean, we don't need this anymore, because we use ActualWidth and ActualHeight of the _root
            //_root.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

            if (!animateOpacity)
            {
                _fadeOutFrame.Value = 1;
                _root.Opacity = 1;
            }
            else
            {
                _fadeOutFrame.Value = 0;
                if (!IsOpen) _root.Opacity = 0;
            }

            switch (position)
            {
                default:
                    HorizontalAlignment = HorizontalAlignment.Left;
                    VerticalAlignment = VerticalAlignment.Stretch;
                    _hideFrame.Value = -_root.ActualWidth;
                    if (resetShowFrame)
                        _root.RenderTransform = new TranslateTransform(-_root.ActualWidth, 0);
                    break;
                case Position.Right:
                    HorizontalAlignment = HorizontalAlignment.Right;
                    VerticalAlignment = VerticalAlignment.Stretch;
                    _hideFrame.Value = _root.ActualWidth;
                    if (resetShowFrame)
                        _root.RenderTransform = new TranslateTransform(_root.ActualWidth, 0);
                    break;
                case Position.Top:
                    HorizontalAlignment = HorizontalAlignment.Stretch;
                    VerticalAlignment = VerticalAlignment.Top;
                    _hideFrameY.Value = -_root.ActualHeight - 1;
                    if (resetShowFrame)
                        _root.RenderTransform = new TranslateTransform(0, -_root.ActualHeight - 1);
                    break;
                case Position.Bottom:
                    HorizontalAlignment = HorizontalAlignment.Stretch;
                    VerticalAlignment = VerticalAlignment.Bottom;
                    _hideFrameY.Value = _root.ActualHeight;
                    if (resetShowFrame)
                        _root.RenderTransform = new TranslateTransform(0, _root.ActualHeight);
                    break;
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            if (!IsOpen)
                return; // no changes for invisible flyouts, ApplyAnimation is called now in visible changed event
            if (!sizeInfo.WidthChanged && !sizeInfo.HeightChanged) return;
            if (_root == null || _hideFrame == null || _showFrame == null || _hideFrameY == null || _showFrameY == null)
                return; // don't bother checking IsOpen and calling ApplyAnimation

            if (Position == Position.Left || Position == Position.Right)
                _showFrame.Value = 0;
            if (Position == Position.Top || Position == Position.Bottom)
                _showFrameY.Value = 0;

            switch (Position)
            {
                default:
                    _hideFrame.Value = -_root.ActualWidth;
                    break;
                case Position.Right:
                    _hideFrame.Value = _root.ActualWidth;
                    break;
                case Position.Top:
                    _hideFrameY.Value = -_root.ActualHeight - 1;
                    break;
                case Position.Bottom:
                    _hideFrameY.Value = _root.ActualHeight;
                    break;
            }
        }

        /// <summary>
        ///     Gets or sets the current <see cref="T:IColorScheme"/>.
        /// </summary>
        public IColorScheme ColorScheme { get; set; }
        
    }
}