using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using Cush.Native.Helpers;

// ReSharper disable InconsistentNaming

namespace Cush.Native.Shell
{
    internal static class Utility
    {
        private static readonly Version OsVersion = Environment.OSVersion.Version;

        private static readonly Version PresentationFrameworkVersion =
            Assembly.GetAssembly(typeof(Window)).GetName().Version;

        // This can be cached.  It's not going to change under reasonable circumstances.
        //private static int s_bitDepth; // = 0;

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static bool IsOSVistaOrNewer
        {
            get { return OsVersion >= new Version(6, 0); }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static bool IsOSWindows7OrNewer
        {
            get { return OsVersion >= new Version(6, 1); }
        }

        /// <summary>
        ///     Is this using WPF4?
        /// </summary>
        /// <remarks>
        ///     There are a few specific bugs in Window in 3.5SP1 and below that require workarounds
        ///     when handling WM_NCCALCSIZE on the HWND.
        /// </remarks>
        public static bool IsPresentationFrameworkVersionLessThan4
        {
            get { return PresentationFrameworkVersion < new Version(4, 0); }
        }

        /// <summary>Convert a native integer that represent a color with an alpha channel into a Color struct.</summary>
        /// <param name="color">The integer that represents the color.  Its bits are of the format 0xAARRGGBB.</param>
        /// <returns>A Color representation of the parameter.</returns>
        public static Color ColorFromArgbDword(uint color)
        {
            return Color.FromArgb(
                (byte)((color & 0xFF000000) >> 24),
                (byte)((color & 0x00FF0000) >> 16),
                (byte)((color & 0x0000FF00) >> 8),
                (byte)((color & 0x000000FF) >> 0));
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static int GET_X_LPARAM(IntPtr lParam)
        {
            return LOWORD(lParam.ToInt32());
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static int GET_Y_LPARAM(IntPtr lParam)
        {
            return HIWORD(lParam.ToInt32());
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private static int HIWORD(int i)
        {
            return (short)(i >> 16);
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static int LOWORD(int i)
        {
            return (short)(i & 0xFFFF);
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static bool IsFlagSet(int value, int mask)
        {
            return 0 != (value & mask);
        }

        /// <summary>GDI's DeleteObject</summary>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static void SafeDeleteObject(ref IntPtr gdiObject)
        {
            var p = gdiObject;
            gdiObject = IntPtr.Zero;
            if (IntPtr.Zero != p)
            {
                NativeMethods.DeleteObject(p);
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static void SafeDestroyWindow(ref IntPtr hwnd)
        {
            var p = hwnd;
            hwnd = IntPtr.Zero;
            if (NativeMethods.IsWindow(p))
            {
                NativeMethods.DestroyWindow(p);
            }
        }


        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static void SafeDispose<T>(ref T disposable) where T : IDisposable
        {
            // Dispose can safely be called on an object multiple times.
            IDisposable t = disposable;
            disposable = default(T);
            if (null != t)
            {
                t.Dispose();
            }
        }

        ///// <summary>GDI+'s DisposeImage</summary>
        ///// <param name="gdipImage"></param>
        //[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        //public static void SafeDisposeImage(ref IntPtr gdipImage)
        //{
        //    var p = gdipImage;
        //    gdipImage = IntPtr.Zero;
        //    if (IntPtr.Zero != p)
        //    {
        //        NativeMethods.GdipDisposeImage(p);
        //    }
        //}

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static void SafeFreeHGlobal(ref IntPtr hglobal)
        {
            var p = hglobal;
            hglobal = IntPtr.Zero;
            if (IntPtr.Zero != p)
            {
                Marshal.FreeHGlobal(p);
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static void SafeRelease<T>(ref T comObject) where T : class
        {
            var t = comObject;
            comObject = default(T);
            if (null != t)
            {
                Assert.IsTrue(Marshal.IsComObject(t));
                Marshal.ReleaseComObject(t);
            }
        }

        public static void AddDependencyPropertyChangeListener(object component, DependencyProperty property,
            EventHandler listener)
        {
            if (component == null)
            {
                return;
            }
            Assert.IsNotNull(property);
            Assert.IsNotNull(listener);

            var dpd = DependencyPropertyDescriptor.FromProperty(property, component.GetType());
            dpd.AddValueChanged(component, listener);
        }

        public static void RemoveDependencyPropertyChangeListener(object component, DependencyProperty property,
            EventHandler listener)
        {
            if (component == null)
            {
                return;
            }
            Assert.IsNotNull(property);
            Assert.IsNotNull(listener);

            var dpd = DependencyPropertyDescriptor.FromProperty(property, component.GetType());
            dpd.RemoveValueChanged(component, listener);
        }

        #region Extension Methods

        public static bool IsThicknessNonNegative(Thickness thickness)
        {
            if (!IsDoubleFiniteAndNonNegative(thickness.Top))
            {
                return false;
            }

            if (!IsDoubleFiniteAndNonNegative(thickness.Left))
            {
                return false;
            }

            if (!IsDoubleFiniteAndNonNegative(thickness.Bottom))
            {
                return false;
            }

            if (!IsDoubleFiniteAndNonNegative(thickness.Right))
            {
                return false;
            }

            return true;
        }

        public static bool IsCornerRadiusValid(CornerRadius cornerRadius)
        {
            if (!IsDoubleFiniteAndNonNegative(cornerRadius.TopLeft))
            {
                return false;
            }

            if (!IsDoubleFiniteAndNonNegative(cornerRadius.TopRight))
            {
                return false;
            }

            if (!IsDoubleFiniteAndNonNegative(cornerRadius.BottomLeft))
            {
                return false;
            }

            if (!IsDoubleFiniteAndNonNegative(cornerRadius.BottomRight))
            {
                return false;
            }

            return true;
        }

        private static bool IsDoubleFiniteAndNonNegative(double d)
        {
            return !double.IsNaN(d) && !double.IsInfinity(d) && !(d < 0);
        }

        #endregion
    }
}