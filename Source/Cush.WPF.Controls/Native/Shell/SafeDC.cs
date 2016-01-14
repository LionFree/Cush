using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Cush.Native.Helpers;
using Microsoft.Win32.SafeHandles;

// ReSharper disable InconsistentNaming

namespace Cush.Native.Shell
{
    public sealed class SafeDC : SafeHandleZeroOrMinusOneIsInvalid
    {
        private bool _created;

        private IntPtr? _hwnd;

        private SafeDC() : base(true)
        {
        }

        public IntPtr Hwnd
        {
            set
            {
                Assert.NullableIsNull(_hwnd);
                _hwnd = value;
            }
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        protected override bool ReleaseHandle()
        {
            if (_created)
            {
                return NativeMethods.DeleteDC(handle);
            }

            if (!_hwnd.HasValue || _hwnd.Value == IntPtr.Zero)
            {
                return true;
            }

            return NativeMethods.ReleaseDC(_hwnd.Value, handle) == 1;
        }

        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"),
         SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static SafeDC CreateDC(string deviceName)
        {
            SafeDC dc = null;
            try
            {
                // Should this really be on the driver parameter?
                dc = NativeMethods.CreateDC(deviceName, null, IntPtr.Zero, IntPtr.Zero);
            }
            finally
            {
                if (dc != null)
                {
                    dc._created = true;
                }
            }

            if (dc.IsInvalid)
            {
                dc.Dispose();
                throw new SystemException("Unable to create a device context from the specified device information.");
            }

            return dc;
        }

        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"),
         SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static SafeDC CreateCompatibleDC(SafeDC hdc)
        {
            SafeDC dc = null;
            try
            {
                var hPtr = IntPtr.Zero;
                if (hdc != null)
                {
                    hPtr = hdc.handle;
                }
                dc = NativeMethods.CreateCompatibleDC(hPtr);
                if (dc == null)
                {
                    HRESULT.ThrowLastError();
                }
            }
            finally
            {
                if (dc != null)
                {
                    dc._created = true;
                }
            }

            if (dc.IsInvalid)
            {
                dc.Dispose();
                throw new SystemException("Unable to create a device context from the specified device information.");
            }

            return dc;
        }

        public static SafeDC GetDC(IntPtr hwnd)
        {
            SafeDC dc = null;
            try
            {
                dc = NativeMethods.GetDC(hwnd);
            }
            finally
            {
                if (dc != null)
                {
                    dc.Hwnd = hwnd;
                }
            }

            if (dc.IsInvalid)
            {
                // GetDC does not set the last error...
                HRESULT.E_FAIL.ThrowIfFailed();
            }

            return dc;
        }

        public static SafeDC GetDesktop()
        {
            return GetDC(IntPtr.Zero);
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static SafeDC WrapDC(IntPtr hdc)
        {
            // This won't actually get released by the class, but it allows an IntPtr to be converted for signatures.
            return new SafeDC
            {
                handle = hdc,
                _created = false,
                _hwnd = IntPtr.Zero
            };
        }

        private static class NativeMethods
        {
            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [DllImport("user32.dll")]
            public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [DllImport("user32.dll")]
            public static extern SafeDC GetDC(IntPtr hwnd);

            // Weird legacy function, documentation is unclear about how to use it...
            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
            public static extern SafeDC CreateDC([MarshalAs(UnmanagedType.LPWStr)] string lpszDriver,
                [MarshalAs(UnmanagedType.LPWStr)] string lpszDevice, IntPtr lpszOutput, IntPtr lpInitData);

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern SafeDC CreateCompatibleDC(IntPtr hdc);

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [DllImport("gdi32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DeleteDC(IntPtr hdc);
        }
    }
}