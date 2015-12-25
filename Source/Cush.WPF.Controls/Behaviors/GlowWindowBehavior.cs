using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Interop;
using System.Windows.Threading;
using Cush.Common.Debugging;
using Cush.Native.Shell;
using Cush.WPF.Controls.Native;
using WM = Cush.Native.Win32.WM;
using SystemParameters = System.Windows.SystemParameters;


namespace Cush.WPF.Controls.Behaviors
{
    public class GlowWindowBehavior : Behavior<Window>
    {
        private static readonly TimeSpan GlowTimerDelay = TimeSpan.FromMilliseconds(200);
            //200 ms delay, the same as VS2013

        private WINDOWPOS _previousWp;
        private IntPtr _handle;
        private GlowWindow _left, _right, _top, _bottom;
        private DispatcherTimer _makeGlowVisibleTimer;

        private bool IsGlowDisabled
        {
            get
            {
                var cushWindow = AssociatedObject as CushWindow;
                return cushWindow != null && (cushWindow.UseNoneWindowStyle || cushWindow.GlowBrush == null);
            }
        }

        private bool IsWindowTransitionsEnabled
        {
            get
            {
                var cushWindow = AssociatedObject as CushWindow;
                return cushWindow != null && cushWindow.WindowTransitionsEnabled;
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.SourceInitialized += (o, args) =>
            {
                // No glow effect if UseNoneWindowStyle is true or GlowBrush not set.
                if (IsGlowDisabled)
                {
                    return;
                }
                _handle = new WindowInteropHelper(AssociatedObject).Handle;
                var hwndSource = HwndSource.FromHwnd(_handle);
                if (hwndSource != null)
                {
                    hwndSource.AddHook(AssociatedObjectWindowProc);
                }
            };

            AssociatedObject.Loaded += AssociatedObjectOnLoaded;
            AssociatedObject.Unloaded += AssociatedObjectUnloaded;

            //AssociatedObject.Loaded += (sender, e) =>
            //{
            //    _left = new GlowWindow(AssociatedObject, GlowDirection.Left);
            //    _right = new GlowWindow(AssociatedObject, GlowDirection.Right);
            //    _top = new GlowWindow(AssociatedObject, GlowDirection.Top);
            //    _bottom = new GlowWindow(AssociatedObject, GlowDirection.Bottom);

            //    Show();

            //    _left.Update();
            //    _right.Update();
            //    _top.Update();
            //    _bottom.Update();
            //};

            //AssociatedObject.Closed += (sender, args) =>
            //{
            //    if (_left != null) _left.Close();
            //    if (_right != null) _right.Close();
            //    if (_top != null) _top.Close();
            //    if (_bottom != null) _bottom.Close();
            //};
        }

        void AssociatedObjectStateChanged(object sender, EventArgs e)
        {
            if (_makeGlowVisibleTimer != null)
            {
                _makeGlowVisibleTimer.Stop();
            }
            if (AssociatedObject.WindowState == WindowState.Normal)
            {
                var cushWindow = AssociatedObject as CushWindow;
                var ignoreTaskBar = cushWindow != null && cushWindow.IgnoreTaskbarOnMaximize;
                if (_makeGlowVisibleTimer != null && SystemParameters.MinimizeAnimation && !ignoreTaskBar)
                {
                    _makeGlowVisibleTimer.Start();
                }
                else
                {
                    RestoreGlow();
                }
            }
            else
            {
                HideGlow();
            }
        }

        void AssociatedObjectUnloaded(object sender, RoutedEventArgs e)
        {
            if (_makeGlowVisibleTimer != null)
            {
                _makeGlowVisibleTimer.Stop();
                _makeGlowVisibleTimer.Tick -= makeGlowVisibleTimer_Tick;
                _makeGlowVisibleTimer = null;
            }
        }

        private void makeGlowVisibleTimer_Tick(object sender, EventArgs e)
        {
            if (_makeGlowVisibleTimer != null)
            {
                _makeGlowVisibleTimer.Stop();
            }
            RestoreGlow();
        }

        private void RestoreGlow()
        {
            if (_left != null) _left.IsGlowing = true;
            if (_top != null) _top.IsGlowing = true;
            if (_right != null) _right.IsGlowing = true;
            if (_bottom != null) _bottom.IsGlowing = true;
            Update();
        }

        private void HideGlow()
        {
            if (_left != null) _left.IsGlowing = false;
            if (_top != null) _top.IsGlowing = false;
            if (_right != null) _right.IsGlowing = false;
            if (_bottom != null) _bottom.IsGlowing = false;
            Update();
        }

        private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            // No glow effect if UseNoneWindowStyle is true or GlowBrush not set.
            if (IsGlowDisabled)
            {
                return;
            }

            AssociatedObject.StateChanged -= AssociatedObjectStateChanged;
            AssociatedObject.StateChanged += AssociatedObjectStateChanged;

            if (_makeGlowVisibleTimer == null)
            {
                _makeGlowVisibleTimer = new DispatcherTimer {Interval = GlowTimerDelay};
                _makeGlowVisibleTimer.Tick += makeGlowVisibleTimer_Tick;
            }

            _left = new GlowWindow(AssociatedObject, GlowDirection.Left);
            _right = new GlowWindow(AssociatedObject, GlowDirection.Right);
            _top = new GlowWindow(AssociatedObject, GlowDirection.Top);
            _bottom = new GlowWindow(AssociatedObject, GlowDirection.Bottom);

            Show();
            Update();

            if (!IsWindowTransitionsEnabled)
            {
                // no storyboard so set opacity to 1
                AssociatedObject.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() => SetOpacityTo(1)));
            }
            else
            {
                // start the opacity storyboard 0->1
                StartOpacityStoryboard();
                // hide the glows if window get invisible state
                AssociatedObject.IsVisibleChanged += AssociatedObjectIsVisibleChanged;
                // closing always handled
                AssociatedObject.Closing += (o, args) =>
                {
                    if (!args.Cancel)
                    {
                        AssociatedObject.IsVisibleChanged -= AssociatedObjectIsVisibleChanged;
                    }
                };
            }
        }

        private IntPtr AssociatedObjectWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch ((WM) msg)
            {
                case WM.WINDOWPOSCHANGED:
                case WM.WINDOWPOSCHANGING:
                    Assert.IsNotDefault(lParam);
                    var wp = (WINDOWPOS) Marshal.PtrToStructure(lParam, typeof (WINDOWPOS));
                    if (!wp.Equals(_previousWp))
                    {
                        UpdateCore();
                    }
                    _previousWp = wp;
                    break;
                case WM.SIZE:
                case WM.SIZING:
                    UpdateCore();
                    break;
            }
            return IntPtr.Zero;
        }

        private void AssociatedObjectIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!AssociatedObject.IsVisible)
            {
                // the associated owner got invisible so set opacity to 0 to start the storyboard by 0 for the next visible state
                SetOpacityTo(0);
            }
            else
            {
                StartOpacityStoryboard();
            }
        }

        /// <summary>
        ///     Updates all glow windows (visible, hidden, collapsed)
        /// </summary>
        private void Update()
        {
            if (_left != null) _left.Update();
            if (_right != null) _right.Update();
            if (_top != null) _top.Update();
            if (_bottom != null) _bottom.Update();
        }

        private void UpdateCore()
        {
            Cush.Native.RECT rect;
            if (_handle != IntPtr.Zero && UnsafeNativeMethods.GetWindowRect(_handle, out rect))
            {
                if (_left != null) _left.UpdateCore(rect);
                if (_right != null) _right.UpdateCore(rect);
                if (_top != null) _top.UpdateCore(rect);
                if (_bottom != null) _bottom.UpdateCore(rect);
            }
        }

        /// <summary>
        ///     Sets the opacity to all glow windows
        /// </summary>
        private void SetOpacityTo(double newOpacity)
        {
            if (_left != null) _left.Opacity = newOpacity;
            if (_right != null) _right.Opacity = newOpacity;
            if (_top != null) _top.Opacity = newOpacity;
            if (_bottom != null) _bottom.Opacity = newOpacity;
        }

        /// <summary>
        ///     Starts the opacity storyboard 0 -> 1
        /// </summary>
        private void StartOpacityStoryboard()
        {
            if (_left != null && _left.OpacityStoryboard != null) _left.BeginStoryboard(_left.OpacityStoryboard);
            if (_right != null && _right.OpacityStoryboard != null) _right.BeginStoryboard(_right.OpacityStoryboard);
            if (_top != null && _top.OpacityStoryboard != null) _top.BeginStoryboard(_top.OpacityStoryboard);
            if (_bottom != null && _bottom.OpacityStoryboard != null) _bottom.BeginStoryboard(_bottom.OpacityStoryboard);
        }

        /// <summary>
        ///     Shows all glow windows
        /// </summary>
        private void Show()
        {
            if (_left != null) _left.Show();
            if (_right != null) _right.Show();
            if (_top != null) _top.Show();
            if (_bottom != null) _bottom.Show();
        }
    }
}