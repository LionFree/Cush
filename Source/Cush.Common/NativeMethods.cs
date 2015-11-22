﻿using System;
//using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

namespace Cush.Common
{
    [SuppressUnmanagedCodeSecurity]
    internal static class NativeMethods
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindowByCaption(IntPtr zeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern
            bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern
            bool IsIconic(IntPtr hWnd);

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        //public delegate IntPtr MessageHandler(WindowsMessage uMsg, IntPtr wParam, IntPtr lParam, out bool handled);
    }

    //[SuppressMessage("ReSharper", "InconsistentNaming")]
    //internal enum WindowsMessage
    //{
    //    NULL = 0,
    //    CREATE = 1,
    //    DESTROY = 2,
    //    MOVE = 3,
    //    SIZE = 5,
    //    ACTIVATE = 6,
    //    SETFOCUS = 7,
    //    KILLFOCUS = 8,
    //    ENABLE = 10,
    //    SETREDRAW = 11,
    //    SETTEXT = 12,
    //    GETTEXT = 13,
    //    GETTEXTLENGTH = 14,
    //    PAINT = 15,
    //    CLOSE = 16,
    //    QUERYENDSESSION = 17,
    //    QUIT = 18,
    //    QUERYOPEN = 19,
    //    ERASEBKGND = 20,
    //    SYSCOLORCHANGE = 21,
    //    SHOWWINDOW = 24,
    //    ACTIVATEAPP = 28,
    //    SETCURSOR = 32,
    //    MOUSEACTIVATE = 33,
    //    CHILDACTIVATE = 34,
    //    QUEUESYNC = 35,
    //    GETMINMAXINFO = 36,
    //    WINDOWPOSCHANGING = 70,
    //    WINDOWPOSCHANGED = 71,
    //    CONTEXTMENU = 123,
    //    STYLECHANGING = 124,
    //    STYLECHANGED = 125,
    //    DISPLAYCHANGE = 126,
    //    GETICON = 127,
    //    SETICON = 128,
    //    NCCREATE = 129,
    //    NCDESTROY = 130,
    //    NCCALCSIZE = 131,
    //    NCHITTEST = 132,
    //    NCPAINT = 133,
    //    NCACTIVATE = 134,
    //    GETDLGCODE = 135,
    //    SYNCPAINT = 136,
    //    NCMOUSEMOVE = 160,
    //    NCLBUTTONDOWN = 161,
    //    NCLBUTTONUP = 162,
    //    NCLBUTTONDBLCLK = 163,
    //    NCRBUTTONDOWN = 164,
    //    NCRBUTTONUP = 165,
    //    NCRBUTTONDBLCLK = 166,
    //    NCMBUTTONDOWN = 167,
    //    NCMBUTTONUP = 168,
    //    NCMBUTTONDBLCLK = 169,
    //    SYSKEYDOWN = 260,
    //    SYSKEYUP = 261,
    //    SYSCHAR = 262,
    //    SYSDEADCHAR = 263,
    //    COMMAND = 273,
    //    SYSCOMMAND = 274,
    //    MOUSEMOVE = 512,
    //    LBUTTONDOWN = 513,
    //    LBUTTONUP = 514,
    //    LBUTTONDBLCLK = 515,
    //    RBUTTONDOWN = 516,
    //    RBUTTONUP = 517,
    //    RBUTTONDBLCLK = 518,
    //    MBUTTONDOWN = 519,
    //    MBUTTONUP = 520,
    //    MBUTTONDBLCLK = 521,
    //    MOUSEWHEEL = 522,
    //    XBUTTONDOWN = 523,
    //    XBUTTONUP = 524,
    //    XBUTTONDBLCLK = 525,
    //    MOUSEHWHEEL = 526,
    //    CAPTURECHANGED = 533,
    //    ENTERSIZEMOVE = 561,
    //    EXITSIZEMOVE = 562,
    //    IME_SETCONTEXT = 641,
    //    IME_NOTIFY = 642,
    //    IME_CONTROL = 643,
    //    IME_COMPOSITIONFULL = 644,
    //    IME_SELECT = 645,
    //    IME_CHAR = 646,
    //    IME_REQUEST = 648,
    //    IME_KEYDOWN = 656,
    //    IME_KEYUP = 657,
    //    NCMOUSELEAVE = 674,
    //    DWMCOMPOSITIONCHANGED = 798,
    //    DWMNCRENDERINGCHANGED = 799,
    //    DWMCOLORIZATIONCOLORCHANGED = 800,
    //    DWMWINDOWMAXIMIZEDCHANGE = 801,
    //    DWMSENDICONICTHUMBNAIL = 803,
    //    DWMSENDICONICLIVEPREVIEWBITMAP = 806,
    //    USER = 1024,
    //    TRAYMOUSEMESSAGE = 2048,
    //    APP = 32768
    //}
}