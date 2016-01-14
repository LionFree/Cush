using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using Cush.Native.Helpers;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;
// ReSharper disable InconsistentNaming

namespace Cush.Native.Shell
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HIGHCONTRAST
    {
        public int cbSize;
        public HCF dwFlags;
        //[MarshalAs(UnmanagedType.LPWStr, SizeConst=80)]
        //public String lpszDefaultScheme;
        public IntPtr lpszDefaultScheme;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct CREATESTRUCT
    {
        public IntPtr lpCreateParams;
        public IntPtr hInstance;
        public IntPtr hMenu;
        public IntPtr hwndParent;
        public int cy;
        public int cx;
        public int y;
        public int x;
        public WS style;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszClass;
        public WS_EX dwExStyle;
    }

    // New to Vista.
    [StructLayout(LayoutKind.Sequential)]
    internal struct TITLEBARINFOEX
    {
        public int cbSize;
        public RECT rcTitleBar;
        public STATE_SYSTEM rgstate_TitleBar;
        public STATE_SYSTEM rgstate_Reserved;
        public STATE_SYSTEM rgstate_MinimizeButton;
        public STATE_SYSTEM rgstate_MaximizeButton;
        public STATE_SYSTEM rgstate_HelpButton;
        public STATE_SYSTEM rgstate_CloseButton;
        public RECT rgrect_TitleBar;
        public RECT rgrect_Reserved;
        public RECT rgrect_MinimizeButton;
        public RECT rgrect_MaximizeButton;
        public RECT rgrect_HelpButton;
        public RECT rgrect_CloseButton;
    }

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    [StructLayout(LayoutKind.Explicit)]
    internal class PROPVARIANT : IDisposable
    {
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        [FieldOffset(8)]
        private short
            boolVal;

        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        [FieldOffset(8)]
        private byte
            byteVal;

        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        [FieldOffset(8)]
        private long
            longVal;

        [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        [FieldOffset(8)]
        private IntPtr
            pointerVal;

        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        [FieldOffset(0)]
        private ushort vt;

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public VarEnum VarType
        {
            get { return (VarEnum)vt; }
        }

        // Right now only using this for strings.
        [SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public string GetValue()
        {
            if (vt == (ushort)VarEnum.VT_LPWSTR)
            {
                return Marshal.PtrToStringUni(pointerVal);
            }

            return null;
        }

        public void SetValue(bool f)
        {
            Clear();
            vt = (ushort)VarEnum.VT_BOOL;
            boolVal = (short)(f ? -1 : 0);
        }

        [SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public void SetValue(string val)
        {
            Clear();
            vt = (ushort)VarEnum.VT_LPWSTR;
            pointerVal = Marshal.StringToCoTaskMemUni(val);
        }

        public void Clear()
        {
            var hr = NativeMethods.PropVariantClear(this);
            Assert.IsTrue(hr.Succeeded);
        }

        private static class NativeMethods
        {
            [DllImport("ole32.dll")]
            internal static extern HRESULT PropVariantClear(PROPVARIANT pvar);
        }

        #region IDisposable Pattern

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PROPVARIANT()
        {
            Dispose(false);
        }

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "disposing")]
        private void Dispose(bool disposing)
        {
            Clear();
        }

        #endregion
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
        /// <summary>Width of left border that retains its size.</summary>
        public int cxLeftWidth;

        /// <summary>Width of right border that retains its size.</summary>
        public int cxRightWidth;

        /// <summary>Height of top border that retains its size.</summary>
        public int cyTopHeight;

        /// <summary>Height of bottom border that retains its size.</summary>
        public int cyBottomHeight;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class MONITORINFO
    {
        public int cbSize = Marshal.SizeOf(typeof(Cush.Native.MONITORINFO));
        public int dwFlags;
        public RECT rcMonitor;
        public RECT rcWork;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        private int _left;
        private int _top;
        private int _right;
        private int _bottom;

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public void Offset(int dx, int dy)
        {
            _left += dx;
            _top += dy;
            _right += dx;
            _bottom += dy;
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public int Left
        {
            get { return _left; }
            set { _left = value; }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public int Right
        {
            get { return _right; }
            set { _right = value; }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public int Top
        {
            get { return _top; }
            set { _top = value; }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public int Bottom
        {
            get { return _bottom; }
            set { _bottom = value; }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public int Width
        {
            get { return _right - _left; }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public int Height
        {
            get { return _bottom - _top; }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public POINT Position
        {
            get { return new POINT { x = _left, y = _top }; }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public SIZE Size
        {
            get { return new SIZE { cx = Width, cy = Height }; }
        }

        public static RECT Union(RECT rect1, RECT rect2)
        {
            return new RECT
            {
                Left = Math.Min(rect1.Left, rect2.Left),
                Top = Math.Min(rect1.Top, rect2.Top),
                Right = Math.Max(rect1.Right, rect2.Right),
                Bottom = Math.Max(rect1.Bottom, rect2.Bottom)
            };
        }

        public override bool Equals(object obj)
        {
            try
            {
                var rc = (RECT)obj;
                return rc._bottom == _bottom
                       && rc._left == _left
                       && rc._right == _right
                       && rc._top == _top;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return (_left << 16 | Utility.LOWORD(_right)) ^ (_top << 16 | Utility.LOWORD(_bottom));
        }
    }

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    [StructLayout(LayoutKind.Sequential)]
    internal class RefRECT
    {
        private int _bottom;
        private int _left;
        private int _right;
        private int _top;

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public RefRECT(int left, int top, int right, int bottom)
        {
            _left = left;
            _top = top;
            _right = right;
            _bottom = bottom;
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public int Width
        {
            get { return _right - _left; }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public int Height
        {
            get { return _bottom - _top; }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public int Left
        {
            get { return _left; }
            set { _left = value; }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public int Right
        {
            get { return _right; }
            set { _right = value; }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public int Top
        {
            get { return _top; }
            set { _top = value; }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public int Bottom
        {
            get { return _bottom; }
            set { _bottom = value; }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public void Offset(int dx, int dy)
        {
            _left += dx;
            _top += dy;
            _right += dx;
            _bottom += dy;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SIZE
    {
        public int cx;
        public int cy;
    }

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    [BestFitMapping(false)]
    internal class WIN32_FIND_DATAW
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
        public string cAlternateFileName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string cFileName;

        public FileAttributes dwFileAttributes;
        public int dwReserved0;
        public int dwReserved1;
        public FILETIME ftCreationTime;
        public FILETIME ftLastAccessTime;
        public FILETIME ftLastWriteTime;
        public int nFileSizeHigh;
        public int nFileSizeLow;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class WINDOWPLACEMENT
    {
        public int flags;
        public int length = Marshal.SizeOf(typeof(Cush.Native.WINDOWPLACEMENT));
        public POINT ptMaxPosition;
        public POINT ptMinPosition;
        public RECT rcNormalPosition;
        public SW showCmd;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPOS
    {
        public IntPtr hwnd;
        public IntPtr hwndInsertAfter;
        public int x;
        public int y;
        public int cx;
        public int cy;
        public int flags;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct WNDCLASSEX
    {
        public int cbSize;
        public CS style;
        public WndProc lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszMenuName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszClassName;
        public IntPtr hIconSm;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct UNSIGNED_RATIO
    {
        public uint uiNumerator;
        public uint uiDenominator;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct DWM_TIMING_INFO
    {
        public int cbSize;
        public UNSIGNED_RATIO rateRefresh;
        public ulong qpcRefreshPeriod;
        public UNSIGNED_RATIO rateCompose;
        public ulong qpcVBlank;
        public ulong cRefresh;
        public uint cDXRefresh;
        public ulong qpcCompose;
        public ulong cFrame;
        public uint cDXPresent;
        public ulong cRefreshFrame;
        public ulong cFrameSubmitted;
        public uint cDXPresentSubmitted;
        public ulong cFrameConfirmed;
        public uint cDXPresentConfirmed;
        public ulong cRefreshConfirmed;
        public uint cDXRefreshConfirmed;
        public ulong cFramesLate;
        public uint cFramesOutstanding;
        public ulong cFrameDisplayed;
        public ulong qpcFrameDisplayed;
        public ulong cRefreshFrameDisplayed;
        public ulong cFrameComplete;
        public ulong qpcFrameComplete;
        public ulong cFramePending;
        public ulong qpcFramePending;
        public ulong cFramesDisplayed;
        public ulong cFramesComplete;
        public ulong cFramesPending;
        public ulong cFramesAvailable;
        public ulong cFramesDropped;
        public ulong cFramesMissed;
        public ulong cRefreshNextDisplayed;
        public ulong cRefreshNextPresented;
        public ulong cRefreshesDisplayed;
        public ulong cRefreshesPresented;
        public ulong cRefreshStarted;
        public ulong cPixelsReceived;
        public ulong cPixelsDrawn;
        public ulong cBuffersEmpty;
    }
}