﻿// ReSharper disable InconsistentNaming

using System;

namespace Cush.Native
{
    public static class Constants
    {
        public const int MONITOR_DEFAULTTONEAREST = 0x00000002;
        public const int WM_NCLBUTTONDOWN = 0x00A1;
        public const int WM_NCCALCSIZE = 0x83;
        public const int WM_NCPAINT = 0x85;
        public const int WM_NCACTIVATE = 0x86;
        public const int WM_GETMINMAXINFO = 0x24;
        public const int WM_CREATE = 0x0001;
        public const long WS_MAXIMIZE = 0x01000000;
        public const int GCLP_HBRBACKGROUND = -0x0A;
        public const int WM_NCHITTEST = 0x84;
        public const int HT_CAPTION = 0x2;
        public const int HTLEFT = 0x0A;
        public const int HTRIGHT = 0x0B;
        public const int HTTOP = 0x0C;
        public const int HTTOPLEFT = 0x0D;
        public const int HTTOPRIGHT = 0x0E;
        public const int HTBOTTOM = 0x0F;
        public const int HTBOTTOMLEFT = 0x10;
        public const int HTBOTTOMRIGHT = 0x11;
        //public const uint TPM_RETURNCMD = 0x0100;
        //public const uint TPM_LEFTBUTTON = 0x0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        //public const uint SYSCOMMAND = 0x0112;
        public const int WM_INITMENU = 0x116;

        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_RESTORE = 0xF120;
        public const int SC_MOVE = 0xF010;
        public const int MF_GRAYED = 0x00000001;
        public const int MF_BYCOMMAND = 0x00000000;
        public const int MF_ENABLED = 0x00000000;
    }

    [Flags()]
    internal enum RedrawWindowFlags : uint
    {
        /// <summary>
        /// Invalidates the rectangle or region that you specify in lprcUpdate or hrgnUpdate.
        /// You can set only one of these parameters to a non-NULL value. If both are NULL, RDW_INVALIDATE invalidates the entire window.
        /// </summary>
        Invalidate = 0x1,

        /// <summary>Causes the OS to post a WM_PAINT message to the window regardless of whether a portion of the window is invalid.</summary>
        InternalPaint = 0x2,

        /// <summary>
        /// Causes the window to receive a WM_ERASEBKGND message when the window is repainted.
        /// Specify this value in combination with the RDW_INVALIDATE value; otherwise, RDW_ERASE has no effect.
        /// </summary>
        Erase = 0x4,

        /// <summary>
        /// Validates the rectangle or region that you specify in lprcUpdate or hrgnUpdate.
        /// You can set only one of these parameters to a non-NULL value. If both are NULL, RDW_VALIDATE validates the entire window.
        /// This value does not affect internal WM_PAINT messages.
        /// </summary>
        Validate = 0x8,

        NoInternalPaint = 0x10,

        /// <summary>Suppresses any pending WM_ERASEBKGND messages.</summary>
        NoErase = 0x20,

        /// <summary>Excludes child windows, if any, from the repainting operation.</summary>
        NoChildren = 0x40,

        /// <summary>Includes child windows, if any, in the repainting operation.</summary>
        AllChildren = 0x80,

        /// <summary>Causes the affected windows, which you specify by setting the RDW_ALLCHILDREN and RDW_NOCHILDREN values, to receive WM_ERASEBKGND and WM_PAINT messages before the RedrawWindow returns, if necessary.</summary>
        UpdateNow = 0x100,

        /// <summary>
        /// Causes the affected windows, which you specify by setting the RDW_ALLCHILDREN and RDW_NOCHILDREN values, to receive WM_ERASEBKGND messages before RedrawWindow returns, if necessary.
        /// The affected windows receive WM_PAINT messages at the ordinary time.
        /// </summary>
        EraseNow = 0x200,

        Frame = 0x400,

        NoFrame = 0x800
    }

}