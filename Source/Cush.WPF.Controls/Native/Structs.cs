using System;
using System.Runtime.InteropServices;
using System.Windows;

// ReSharper disable InconsistentNaming

namespace Cush.Native
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPLACEMENT
    {
        public int length;
        public int flags;
        public int showCmd;
        public POINT minPosition;
        public POINT maxPosition;
        public RECT normalPosition;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct RECT
    {
        public readonly int left;
        public int top;
        public readonly int right;
        public int bottom;

        public static readonly RECT Empty = new RECT();

        public int Width
        {
            get { return Math.Abs(right - left); }  // Abs needed for BIDI OS
        }

        public int Height
        {
            get { return bottom - top; }
        }

        public RECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public RECT(RECT rcSrc)
        {
            left = rcSrc.left;
            top = rcSrc.top;
            right = rcSrc.right;
            bottom = rcSrc.bottom;
        }

        public bool IsEmpty
        {
            get
            {
                // BUGBUG : On Bidi OS (hebrew arabic) left > right
                return left >= right || top >= bottom;
            }
        }

        public override string ToString()
        {
            if (this == Empty)
                return "RECT {Empty}";
            return "RECT { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }";
        }

        /// <summary> Determine if 2 RECT are equal (deep compare) </summary>
        public override bool Equals(object obj)
        {
            if (!(obj is Rect)) { return false; }
            return (this == (RECT)obj);
        }

        /// <summary>Return the HashCode for this struct (not garanteed to be unique)</summary>
        public override int GetHashCode()
        {
            return left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode();
        }

        public static bool operator ==(RECT rect1, RECT rect2)
        {
            return (rect1.left == rect2.left && rect1.top == rect2.top && rect1.right == rect2.right && rect1.bottom == rect2.bottom);
        }

        public static bool operator !=(RECT rect1, RECT rect2)
        {
            return !(rect1 == rect2);
        }


    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        private int _x;
        private int _y;

        public POINT(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj is POINT)
            {
                var point = (POINT)obj;

                return point._x == _x && point._y == _y;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return _x.GetHashCode() ^ _y.GetHashCode();
        }

        public static bool operator ==(POINT a, POINT b)
        {
            return a._x == b._x && a._y == b._y;
        }

        public static bool operator !=(POINT a, POINT b)
        {
            return !(a == b);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class MONITORINFO
    {
        public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
        public RECT rcMonitor = new RECT();
        public RECT rcWork = new RECT();
        public int dwFlags = 0;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MINMAXINFO
    {
        public POINT ptReserved;
        public POINT ptMaxSize;
        public POINT ptMaxPosition;
        public POINT ptMinTrackSize;
        public POINT ptMaxTrackSize;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
        public int leftWidth;
        public int rightWidth;
        public int topHeight;
        public int bottomHeight;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct APPBARDATA
    {
        public int cbSize;
        public IntPtr hWnd;
        public int uCallbackMessage;
        public int uEdge;
        public RECT rc;
        public bool lParam;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
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
        public int style;
        public string lpszName;
        public string lpszClass;
        public int dwExStyle;
    }
}