using System;
using System.Runtime.InteropServices;

namespace Cush.Native.Win32
{
    public static class NativeMethods
    {
        public static WS GetWindowLong(this IntPtr hWnd)
        {
            return (WS) GetWindowLong(hWnd, (int) GWL.STYLE);
        }

        public static WSEX GetWindowLongEx(this IntPtr hWnd)
        {
            return (WSEX) GetWindowLong(hWnd, (int) GWL.EXSTYLE);
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLongA", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public static WS SetWindowLong(this IntPtr hWnd, WS dwNewLong)
        {
            return (WS) SetWindowLong(hWnd, (int) GWL.STYLE, (int) dwNewLong);
        }

        public static WSEX SetWindowLongEx(this IntPtr hWnd, WSEX dwNewLong)
        {
            return (WSEX) SetWindowLong(hWnd, (int) GWL.EXSTYLE, (int) dwNewLong);
        }

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy,
            SWP flags);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool PostMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);


        public static bool PostMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            return PostMessage(hwnd, (uint) msg, wParam, lParam);
        }
    }
}