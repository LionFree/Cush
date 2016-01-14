using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Cush.Native.Win32;

namespace Cush.WPF.Controls
{
    [TemplatePart(Name = "PART_Min", Type = typeof (Button))]
    [TemplatePart(Name = "PART_Max", Type = typeof (Button))]
    [TemplatePart(Name = "PART_Close", Type = typeof (Button))]
    [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class WindowStateButtons : ContentControl, INotifyPropertyChanged
    {
        public delegate void ClosingWindowEventHandler(object sender, ClosingWindowEventHandlerArgs args);

        public static readonly DependencyProperty LightMinButtonStyleProperty =
            DependencyProperty.Register("LightMinButtonStyle", typeof (Style), typeof (WindowStateButtons),
                new PropertyMetadata(null, OnThemeChanged));

        public static readonly DependencyProperty LightMaxButtonStyleProperty =
            DependencyProperty.Register("LightMaxButtonStyle", typeof (Style), typeof (WindowStateButtons),
                new PropertyMetadata(null, OnThemeChanged));

        public static readonly DependencyProperty LightCloseButtonStyleProperty =
            DependencyProperty.Register("LightCloseButtonStyle", typeof (Style), typeof (WindowStateButtons),
                new PropertyMetadata(null, OnThemeChanged));

        public static readonly DependencyProperty DarkMinButtonStyleProperty =
            DependencyProperty.Register("DarkMinButtonStyle", typeof (Style), typeof (WindowStateButtons),
                new PropertyMetadata(null, OnThemeChanged));

        public static readonly DependencyProperty DarkMaxButtonStyleProperty =
            DependencyProperty.Register("DarkMaxButtonStyle", typeof (Style), typeof (WindowStateButtons),
                new PropertyMetadata(null, OnThemeChanged));

        public static readonly DependencyProperty DarkCloseButtonStyleProperty =
            DependencyProperty.Register("DarkCloseButtonStyle", typeof (Style), typeof (WindowStateButtons),
                new PropertyMetadata(null, OnThemeChanged));

        public static readonly DependencyProperty ThemeProperty =
            DependencyProperty.Register("Theme", typeof (Theme), typeof (WindowStateButtons),
                new PropertyMetadata(Theme.Light, OnThemeChanged));

        private static string _minimizeText;
        private static string _maximizeText;
        private static string _closeText;
        private static string _restoreText;

        private Window _parentWindow;
        private Button _closeButton;
        private Button _maxButton;
        private Button _minButton;
        private SafeLibraryHandle _user32;

        static WindowStateButtons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (WindowStateButtons),
                new FrameworkPropertyMetadata(typeof (WindowStateButtons)));
        }

        public WindowStateButtons()
        {
            Loaded += WindowButtonCommands_Loaded;
        }

        /// <summary>
        ///     Gets or sets the value indicating current light style for the minimize button.
        /// </summary>
        public Style LightMinButtonStyle
        {
            get { return (Style) GetValue(LightMinButtonStyleProperty); }
            set { SetValue(LightMinButtonStyleProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the value indicating current light style for the maximize button.
        /// </summary>
        public Style LightMaxButtonStyle
        {
            get { return (Style) GetValue(LightMaxButtonStyleProperty); }
            set { SetValue(LightMaxButtonStyleProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the value indicating current light style for the _close button.
        /// </summary>
        public Style LightCloseButtonStyle
        {
            get { return (Style) GetValue(LightCloseButtonStyleProperty); }
            set { SetValue(LightCloseButtonStyleProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the value indicating current dark style for the minimize button.
        /// </summary>
        public Style DarkMinButtonStyle
        {
            get { return (Style) GetValue(DarkMinButtonStyleProperty); }
            set { SetValue(DarkMinButtonStyleProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the value indicating current dark style for the maximize button.
        /// </summary>
        public Style DarkMaxButtonStyle
        {
            get { return (Style) GetValue(DarkMaxButtonStyleProperty); }
            set { SetValue(DarkMaxButtonStyleProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the value indicating current dark style for the _close button.
        /// </summary>
        public Style DarkCloseButtonStyle
        {
            get { return (Style) GetValue(DarkCloseButtonStyleProperty); }
            set { SetValue(DarkCloseButtonStyleProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the value indicating current theme.
        /// </summary>
        public Theme Theme
        {
            get { return (Theme) GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        public string MinimizeText
        {
            get
            {
                if (string.IsNullOrEmpty(_minimizeText))
                {
                    _minimizeText = GetCaption(900);
                }
                return _minimizeText;
            }
        }

        public string MaximizeText
        {
            get
            {
                if (string.IsNullOrEmpty(_maximizeText))
                {
                    _maximizeText = GetCaption(901);
                }
                return _maximizeText;
            }
        }

        public string CloseText
        {
            get
            {
                if (string.IsNullOrEmpty(_closeText))
                {
                    _closeText = GetCaption(905);
                }
                return _closeText;
            }
        }

        public string RestoreText
        {
            get
            {
                if (string.IsNullOrEmpty(_restoreText))
                {
                    _restoreText = GetCaption(903);
                }
                return _restoreText;
            }
        }

        public Window ParentWindow
        {
            get { return _parentWindow; }
            set
            {
                if (Equals(_parentWindow, value))
                {
                    return;
                }
                _parentWindow = value;
                RaisePropertyChanged(nameof(ParentWindow));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event ClosingWindowEventHandler ClosingWindow;

        private static void OnThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue)
            {
                return;
            }

            ((WindowStateButtons) d).ApplyTheme();
        }

        private void WindowButtonCommands_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= WindowButtonCommands_Loaded;
            var parentWindow = ParentWindow;
            if (null == parentWindow)
            {
                ParentWindow = this.TryFindParent<Window>();
            }
        }

        private string GetCaption(int id)
        {
            if (_user32 == null)
            {
                _user32 = Native.UnsafeNativeMethods.LoadLibrary(Environment.SystemDirectory + "\\User32.dll");
            }

            var sb = new StringBuilder(256);
            Native.UnsafeNativeMethods.LoadString(_user32, (uint) id, sb, sb.Capacity);
            return sb.ToString().Replace("&", "");
        }

        public void ApplyTheme()
        {
            if (_closeButton != null)
            {
                _closeButton.Style = (Theme == Theme.Light) ? LightCloseButtonStyle : DarkCloseButtonStyle;
            }
            if (_maxButton != null)
            {
                _maxButton.Style = (Theme == Theme.Light) ? LightMaxButtonStyle : DarkMaxButtonStyle;
            }
            if (_minButton != null)
            {
                _minButton.Style = (Theme == Theme.Light) ? LightMinButtonStyle : DarkMinButtonStyle;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _closeButton = Template.FindName("PART_Close", this) as Button;
            if (_closeButton != null)
            {
                _closeButton.Click += CloseClick;
            }

            _maxButton = Template.FindName("PART_Max", this) as Button;
            if (_maxButton != null)
            {
                _maxButton.Click += MaximizeClick;
            }

            _minButton = Template.FindName("PART_Min", this) as Button;
            if (_minButton != null)
            {
                _minButton.Click += MinimizeClick;
            }

            ApplyTheme();
        }

        protected void OnClosingWindow(ClosingWindowEventHandlerArgs args)
        {
            var handler = ClosingWindow;
            handler?.Invoke(this, args);
        }

        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            if (null == ParentWindow) return;
            SystemCommands.MinimizeWindow(ParentWindow);
        }

        private void MaximizeClick(object sender, RoutedEventArgs e)
        {
            if (null == ParentWindow) return;
            if (ParentWindow.WindowState == WindowState.Maximized)
            {
                SystemCommands.RestoreWindow(ParentWindow);
            }
            else
            {
                SystemCommands.MaximizeWindow(ParentWindow);
            }
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            var closingWindowEventHandlerArgs = new ClosingWindowEventHandlerArgs();
            OnClosingWindow(closingWindowEventHandlerArgs);

            if (closingWindowEventHandlerArgs.Cancelled)
            {
                return;
            }

            ParentWindow?.Close();
        }

        protected virtual void RaisePropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}