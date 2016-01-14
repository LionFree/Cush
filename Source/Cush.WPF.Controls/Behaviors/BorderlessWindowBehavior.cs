﻿using System;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Interop;
using Cush.Common.Exceptions;
using Cush.Native;
using Cush.WPF.Controls.Helpers;
using Cush.WPF.Controls.Native;
using Microsoft.Windows.Shell;

namespace Cush.WPF.Controls.Behaviors
{
    /// <summary>
    /// With this class we can make custom window styles.
    /// </summary>
    public class BorderlessWindowBehavior : Behavior<Window>
    {
        private IntPtr handle;
        private HwndSource hwndSource;
        private WindowChrome windowChrome;
        private PropertyChangeNotifier borderThicknessChangeNotifier;
        private Thickness? savedBorderThickness;
        private PropertyChangeNotifier topMostChangeNotifier;
        private bool savedTopMost;

        protected override void OnAttached()
        {
            windowChrome = new WindowChrome
            {
                ResizeBorderThickness = SystemParameters.WindowResizeBorderThickness, 
                CaptionHeight = 0,
                CornerRadius = new CornerRadius(0),
                GlassFrameThickness = new Thickness(0),
                UseAeroCaptionButtons = false
            };

            var CushWindow = AssociatedObject as CushWindow;
            if (CushWindow != null)
            {
                windowChrome.IgnoreTaskbarOnMaximize = CushWindow.IgnoreTaskbarOnMaximize;
                windowChrome.UseNoneWindowStyle = CushWindow.UseNoneWindowStyle;
                System.ComponentModel.DependencyPropertyDescriptor.FromProperty(CushWindow.IgnoreTaskbarOnMaximizeProperty, typeof(CushWindow))
                      .AddValueChanged(AssociatedObject, IgnoreTaskbarOnMaximizePropertyChangedCallback);
                System.ComponentModel.DependencyPropertyDescriptor.FromProperty(CushWindow.UseNoneWindowStyleProperty, typeof(CushWindow))
                      .AddValueChanged(AssociatedObject, UseNoneWindowStylePropertyChangedCallback);
            }

            AssociatedObject.SetValue(WindowChrome.WindowChromeProperty, windowChrome);

            // no transparency, because it hase more then one unwanted issues
            var windowHandle = new WindowInteropHelper(AssociatedObject).Handle;
            if (!AssociatedObject.IsLoaded && windowHandle == IntPtr.Zero)
            {
                try
                {
                    AssociatedObject.AllowsTransparency = false;
                }
                catch (Exception)
                {
                    //For some reason, we can't determine if the window has loaded or not, so we swallow the exception.
                }
            }
            AssociatedObject.WindowStyle = WindowStyle.None;

            savedBorderThickness = AssociatedObject.BorderThickness;
            borderThicknessChangeNotifier = new PropertyChangeNotifier(this.AssociatedObject, Window.BorderThicknessProperty);
            borderThicknessChangeNotifier.ValueChanged += BorderThicknessChangeNotifierOnValueChanged;

            savedTopMost = AssociatedObject.Topmost;
            topMostChangeNotifier = new PropertyChangeNotifier(this.AssociatedObject, Window.TopmostProperty);
            topMostChangeNotifier.ValueChanged += TopMostChangeNotifierOnValueChanged;

            AssociatedObject.Loaded += AssociatedObject_Loaded;
            AssociatedObject.Unloaded += AssociatedObject_Unloaded;
            AssociatedObject.SourceInitialized += AssociatedObject_SourceInitialized;
            AssociatedObject.StateChanged += OnAssociatedObjectHandleMaximize;

            // handle the maximized state here too (to handle the border in a correct way)
            this.HandleMaximize();

            base.OnAttached();
        }

        private void BorderThicknessChangeNotifierOnValueChanged(object sender, EventArgs e)
        {
            savedBorderThickness = AssociatedObject.BorderThickness;
        }

        private void TopMostChangeNotifierOnValueChanged(object sender, EventArgs e)
        {
            savedTopMost = AssociatedObject.Topmost;
        }

        private void UseNoneWindowStylePropertyChangedCallback(object sender, EventArgs e)
        {
            var CushWindow = sender as CushWindow;
            if (CushWindow != null && windowChrome != null)
            {
                if (!Equals(windowChrome.UseNoneWindowStyle, CushWindow.UseNoneWindowStyle))
                {
                    windowChrome.UseNoneWindowStyle = CushWindow.UseNoneWindowStyle;
                    this.ForceRedrawWindowFromPropertyChanged();
                }
            }
        }

        private void IgnoreTaskbarOnMaximizePropertyChangedCallback(object sender, EventArgs e)
        {
            var CushWindow = sender as CushWindow;
            if (CushWindow != null && windowChrome != null)
            {
                if (!Equals(windowChrome.IgnoreTaskbarOnMaximize, CushWindow.IgnoreTaskbarOnMaximize))
                {
                    // another special hack to avoid nasty resizing
                    // repro
                    // ResizeMode="NoResize"
                    // WindowState="Maximized"
                    // IgnoreTaskbarOnMaximize="True"
                    // this only happens if we change this at runtime
                    var removed = _ModifyStyle(Cush.Native.Shell.WS.MAXIMIZEBOX | Cush.Native.Shell.WS.MINIMIZEBOX | Cush.Native.Shell.WS.THICKFRAME, 0);
                    windowChrome.IgnoreTaskbarOnMaximize = CushWindow.IgnoreTaskbarOnMaximize;
                    this.ForceRedrawWindowFromPropertyChanged();
                    if (removed)
                    {
                        _ModifyStyle(0, Cush.Native.Shell.WS.MAXIMIZEBOX | Cush.Native.Shell.WS.MINIMIZEBOX | Cush.Native.Shell.WS.THICKFRAME);
                    }
                }
            }
        }

        /// <summary>Add and remove a native WindowStyle from the HWND.</summary>
        /// <param name="removeStyle">The styles to be removed.  These can be bitwise combined.</param>
        /// <param name="addStyle">The styles to be added.  These can be bitwise combined.</param>
        /// <returns>Whether the styles of the HWND were modified as a result of this call.</returns>
        /// <SecurityNote>
        ///   Critical : Calls critical methods
        /// </SecurityNote>
        [SecurityCritical]
        private bool _ModifyStyle(Cush.Native.Shell.WS removeStyle, Cush.Native.Shell.WS addStyle)
        {
            if (this.handle == IntPtr.Zero)
            {
                return false;
            }
            var intPtr = Cush.Native.Shell.NativeMethods.GetWindowLongPtr(this.handle, Cush.Native.Shell.GWL.STYLE);
            var dwStyle = (Cush.Native.Shell.WS)(Environment.Is64BitProcess ? intPtr.ToInt64() : intPtr.ToInt32());
            var dwNewStyle = (dwStyle & ~removeStyle) | addStyle;
            if (dwStyle == dwNewStyle)
            {
                return false;
            }

            Cush.Native.Shell.NativeMethods.SetWindowLongPtr(this.handle, Cush.Native.Shell.GWL.STYLE, new IntPtr((int)dwNewStyle));
            return true;
        }

        private void ForceRedrawWindowFromPropertyChanged()
        {
            this.HandleMaximize();
            if (this.handle != IntPtr.Zero)
            {
                UnsafeNativeMethods.RedrawWindow(this.handle, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate | RedrawWindowFlags.Frame);
            }
        }

        private bool isCleanedUp;

        private void Cleanup()
        {
            if (!isCleanedUp)
            {
                isCleanedUp = true;

                // clean up events
                if (AssociatedObject is CushWindow)
                {
                    System.ComponentModel.DependencyPropertyDescriptor.FromProperty(CushWindow.IgnoreTaskbarOnMaximizeProperty, typeof(CushWindow))
                          .RemoveValueChanged(AssociatedObject, IgnoreTaskbarOnMaximizePropertyChangedCallback);
                    System.ComponentModel.DependencyPropertyDescriptor.FromProperty(CushWindow.UseNoneWindowStyleProperty, typeof(CushWindow))
                          .RemoveValueChanged(AssociatedObject, UseNoneWindowStylePropertyChangedCallback);
                }
                AssociatedObject.Loaded -= AssociatedObject_Loaded;
                AssociatedObject.Unloaded -= AssociatedObject_Unloaded;
                AssociatedObject.SourceInitialized -= AssociatedObject_SourceInitialized;
                AssociatedObject.StateChanged -= OnAssociatedObjectHandleMaximize;
                if (hwndSource != null)
                {
                    hwndSource.RemoveHook(WindowProc);
                }
                windowChrome = null;
            }
        }

        protected override void OnDetaching()
        {
            Cleanup();
            base.OnDetaching();
        }

        private void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
            Cleanup();
        }

        private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            var returnval = IntPtr.Zero;

            switch (msg)
            {
                case Constants.WM_NCPAINT:
                    handled = true;
                    break;
                case Constants.WM_NCACTIVATE:
                    /* As per http://msdn.microsoft.com/en-us/library/ms632633(VS.85).aspx , "-1" lParam "does not repaint the nonclient area to reflect the state change." */
                    returnval = UnsafeNativeMethods.DefWindowProc(hwnd, msg, wParam, new IntPtr(-1));
                    handled = true;
                    break;
            }

            return returnval;
        }

        private void OnAssociatedObjectHandleMaximize(object sender, EventArgs e)
        {
            HandleMaximize();
        }

        private void HandleMaximize()
        {
            borderThicknessChangeNotifier.ValueChanged -= BorderThicknessChangeNotifierOnValueChanged;
            topMostChangeNotifier.ValueChanged -= TopMostChangeNotifierOnValueChanged;

            var CushWindow = AssociatedObject as CushWindow;
            //var enableDWMDropShadow = EnableDWMDropShadow;
            //if (CushWindow != null)
            //{
            //    enableDWMDropShadow = CushWindow.GlowBrush == null && (CushWindow.EnableDWMDropShadow || EnableDWMDropShadow);
            //}

            if (AssociatedObject.WindowState == WindowState.Maximized)
            {
                // remove resize border and window border, so we can move the window from top monitor position
                // note (punker76): check this, maybe we doesn't need this anymore
                windowChrome.ResizeBorderThickness = new Thickness(0);
                AssociatedObject.BorderThickness = new Thickness(0);

                var ignoreTaskBar = CushWindow != null && CushWindow.IgnoreTaskbarOnMaximize;
                if (ignoreTaskBar && handle != IntPtr.Zero)
                {
                    // WindowChrome handles the size false if the main monitor is lesser the monitor where the window is maximized
                    // so set the window pos/size twice
                    IntPtr monitor = UnsafeNativeMethods.MonitorFromWindow(handle, Constants.MONITOR_DEFAULTTONEAREST);
                    if (monitor != IntPtr.Zero)
                    {
                        var monitorInfo = new MONITORINFO();
                        UnsafeNativeMethods.GetMonitorInfo(monitor, monitorInfo);

                        //ignoreTaskBar = CushWindow.IgnoreTaskbarOnMaximize || CushWindow.UseNoneWindowStyle;
                        var x = ignoreTaskBar ? monitorInfo.rcMonitor.left : monitorInfo.rcWork.left;
                        var y = ignoreTaskBar ? monitorInfo.rcMonitor.top : monitorInfo.rcWork.top;
                        var cx = ignoreTaskBar ? Math.Abs(monitorInfo.rcMonitor.right - x) : Math.Abs(monitorInfo.rcWork.right - x);
                        var cy = ignoreTaskBar ? Math.Abs(monitorInfo.rcMonitor.bottom - y) : Math.Abs(monitorInfo.rcWork.bottom - y);
                        UnsafeNativeMethods.SetWindowPos(handle, new IntPtr(-2), x, y, cx, cy, 0x0040);
                    }
                }
            }
            else
            {
                // note (punker76): check this, maybe we doesn't need this anymore
                windowChrome.ResizeBorderThickness = SystemParameters.WindowResizeBorderThickness;
                //if (!enableDWMDropShadow)
                //{
                //    AssociatedObject.BorderThickness = savedBorderThickness.GetValueOrDefault(new Thickness(0));
                //}
            }

            // fix nasty TopMost bug
            // - set TopMost="True"
            // - start mahapps demo
            // - TopMost works
            // - maximize window and back to normal
            // - TopMost is gone
            //
            // Problem with minimize animation when window is maximized #1528
            // 1. Activate another application (such as Google Chrome).
            // 2. Run the demo and maximize it.
            // 3. Minimize the demo by clicking on the taskbar button.
            // Note that the minimize animation in this case does actually run, but somehow the other
            // application (Google Chrome in this example) is instantly switched to being the top window,
            // and so blocking the animation view.
            AssociatedObject.Topmost = false;
            AssociatedObject.Topmost = AssociatedObject.WindowState == WindowState.Minimized || savedTopMost;

            borderThicknessChangeNotifier.ValueChanged += BorderThicknessChangeNotifierOnValueChanged;
            topMostChangeNotifier.ValueChanged += TopMostChangeNotifierOnValueChanged;
        }

        private void AssociatedObject_SourceInitialized(object sender, EventArgs e)
        {
            handle = new WindowInteropHelper(AssociatedObject).Handle;
            ThrowHelper.IfNullThenThrow(()=>handle);
            
            hwndSource = HwndSource.FromHwnd(handle);
            hwndSource?.AddHook(WindowProc);

            if (AssociatedObject.ResizeMode != ResizeMode.NoResize)
            {
                // handle size to content (thanks @lynnx).
                // This is necessary when ResizeMode != NoResize. Without this workaround,
                // black bars appear at the right and bottom edge of the window.
                var sizeToContent = AssociatedObject.SizeToContent;
                var snapsToDevicePixels = AssociatedObject.SnapsToDevicePixels;
                AssociatedObject.SnapsToDevicePixels = true;
                AssociatedObject.SizeToContent = sizeToContent == SizeToContent.WidthAndHeight ? SizeToContent.Height : SizeToContent.Manual;
                AssociatedObject.SizeToContent = sizeToContent;
                AssociatedObject.SnapsToDevicePixels = snapsToDevicePixels;
            }
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            var window = sender as CushWindow;
            if (window == null)
            {
                return;
            }

            window.SetIsHitTestVisibleInChromeProperty<UIElement>("PART_Icon");
            window.SetIsHitTestVisibleInChromeProperty<UIElement>("PART_TitleBar");
            window.SetIsHitTestVisibleInChromeProperty<ContentPresenter>("PART_LeftWindowCommands");
            window.SetIsHitTestVisibleInChromeProperty<ContentPresenter>("PART_RightWindowCommands");
            window.SetIsHitTestVisibleInChromeProperty<ContentControl>("PART_WindowButtonCommands");

            window.SetWindowChromeResizeGripDirection("WindowResizeGrip", ResizeGripDirection.BottomRight);
        }
    }
}
