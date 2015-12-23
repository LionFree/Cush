﻿using System;

// ReSharper disable InconsistentNaming

namespace Cush.Native.Shell
{
    // Some COM interfaces and Win32 structures are already declared in the framework.
    // Interesting ones to remember in System.Runtime.InteropServices.ComTypes are:

    /// <summary>
    ///     HIGHCONTRAST flags
    /// </summary>
    [Flags]
    public enum HCF
    {
        HIGHCONTRASTON = 0x00000001,
        AVAILABLE = 0x00000002,
        HOTKEYACTIVE = 0x00000004,
        CONFIRMHOTKEY = 0x00000008,
        HOTKEYSOUND = 0x00000010,
        INDICATOR = 0x00000020,
        HOTKEYAVAILABLE = 0x00000040
    }

    /// <summary>
    ///     BITMAPINFOHEADER Compression type.  BI_*.
    /// </summary>
    internal enum BI
    {
        RGB = 0
    }

    /// <summary>
    ///     CombingRgn flags.  RGN_*
    /// </summary>
    public enum RGN
    {
        /// <summary>
        ///     Creates the intersection of the two combined regions.
        /// </summary>
        AND = 1,

        /// <summary>
        ///     Creates the union of two combined regions.
        /// </summary>
        OR = 2,

        /// <summary>
        ///     Creates the union of two combined regions except for any overlapping areas.
        /// </summary>
        XOR = 3,

        /// <summary>
        ///     Combines the parts of hrgnSrc1 that are not part of hrgnSrc2.
        /// </summary>
        DIFF = 4,

        /// <summary>
        ///     Creates a copy of the region identified by hrgnSrc1.
        /// </summary>
        COPY = 5
    }

    public enum CombineRgnResult
    {
        ERROR = 0,
        NULLREGION = 1,
        SIMPLEREGION = 2,
        COMPLEXREGION = 3
    }

    /// <summary>
    ///     For IWebBrowser2.  OLECMDEXECOPT_*
    /// </summary>
    internal enum OLECMDEXECOPT
    {
        DODEFAULT = 0,
        PROMPTUSER = 1,
        DONTPROMPTUSER = 2,
        SHOWHELP = 3
    }

    /// <summary>
    ///     For IWebBrowser2.  OLECMDF_*
    /// </summary>
    internal enum OLECMDF
    {
        SUPPORTED = 1,
        ENABLED = 2,
        LATCHED = 4,
        NINCHED = 8,
        INVISIBLE = 16,
        DEFHIDEONCTXTMENU = 32
    }

    /// <summary>
    ///     For IWebBrowser2.  OLECMDID_*
    /// </summary>
    internal enum OLECMDID
    {
        OPEN = 1,
        NEW = 2,
        SAVE = 3,
        SAVEAS = 4,
        SAVECOPYAS = 5,
        PRINT = 6,
        PRINTPREVIEW = 7,
        PAGESETUP = 8,
        SPELL = 9,
        PROPERTIES = 10,
        CUT = 11,
        COPY = 12,
        PASTE = 13,
        PASTESPECIAL = 14,
        UNDO = 15,
        REDO = 16,
        SELECTALL = 17,
        CLEARSELECTION = 18,
        ZOOM = 19,
        GETZOOMRANGE = 20,
        UPDATECOMMANDS = 21,
        REFRESH = 22,
        STOP = 23,
        HIDETOOLBARS = 24,
        SETPROGRESSMAX = 25,
        SETPROGRESSPOS = 26,
        SETPROGRESSTEXT = 27,
        SETTITLE = 28,
        SETDOWNLOADSTATE = 29,
        STOPDOWNLOAD = 30,
        ONTOOLBARACTIVATED = 31,
        FIND = 32,
        DELETE = 33,
        HTTPEQUIV = 34,
        HTTPEQUIV_DONE = 35,
        ENABLE_INTERACTION = 36,
        ONUNLOAD = 37,
        PROPERTYBAG2 = 38,
        PREREFRESH = 39,
        SHOWSCRIPTERROR = 40,
        SHOWMESSAGE = 41,
        SHOWFIND = 42,
        SHOWPAGESETUP = 43,
        SHOWPRINT = 44,
        CLOSE = 45,
        ALLOWUILESSSAVEAS = 46,
        DONTDOWNLOADCSS = 47,
        UPDATEPAGESTATUS = 48,
        PRINT2 = 49,
        PRINTPREVIEW2 = 50,
        SETPRINTTEMPLATE = 51,
        GETPRINTTEMPLATE = 52,
        PAGEACTIONBLOCKED = 55,
        PAGEACTIONUIQUERY = 56,
        FOCUSVIEWCONTROLS = 57,
        FOCUSVIEWCONTROLSQUERY = 58,
        SHOWPAGEACTIONMENU = 59
    }

    /// <summary>
    ///     For IWebBrowser2.  READYSTATE_*
    /// </summary>
    enum READYSTATE
    {
        UNINITIALIZED = 0,
        LOADING = 1,
        LOADED = 2,
        INTERACTIVE = 3,
        COMPLETE = 4
    }


    /// <summary>
    ///     DATAOBJ_GET_ITEM_FLAGS.  DOGIF_*.
    /// </summary>
    internal enum DOGIF
    {
        DEFAULT = 0x0000,
        TRAVERSE_LINK = 0x0001, // if the item is a link get the target
        NO_HDROP = 0x0002, // don't fallback and use CF_HDROP clipboard format
        NO_URL = 0x0004, // don't fallback and use URL clipboard format
        ONLY_IF_ONE = 0x0008 // only return the item if there is one item in the array
    }

    internal enum DWM_SIT
    {
        None,
        DISPLAYFRAME = 1
    }

    [Flags]
    internal enum ErrorModes
    {
        /// <summary>Use the system default, which is to display all error dialog boxes.</summary>
        Default = 0x0,

        /// <summary>
        ///     The system does not display the critical-error-handler message box.
        ///     Instead, the system sends the error to the calling process.
        /// </summary>
        FailCriticalErrors = 0x1,

        /// <summary>
        ///     64-bit Windows:  The system automatically fixes memory alignment faults and makes them
        ///     invisible to the application. It does this for the calling process and any descendant processes.
        ///     After this value is set for a process, subsequent attempts to clear the value are ignored.
        /// </summary>
        NoGpFaultErrorBox = 0x2,

        /// <summary>
        ///     The system does not display the general-protection-fault message box.
        ///     This flag should only be set by debugging applications that handle general
        ///     protection (GP) faults themselves with an exception handler.
        /// </summary>
        NoAlignmentFaultExcept = 0x4,

        /// <summary>
        ///     The system does not display a message box when it fails to find a file.
        ///     Instead, the error is returned to the calling process.
        /// </summary>
        NoOpenFileErrorBox = 0x8000
    }

    /// <summary>
    ///     Non-client hit test values, HT*
    /// </summary>
    public enum HT
    {
        ERROR = -2,
        TRANSPARENT = -1,
        NOWHERE = 0,
        CLIENT = 1,
        CAPTION = 2,
        SYSMENU = 3,
        GROWBOX = 4,
        SIZE = GROWBOX,
        MENU = 5,
        HSCROLL = 6,
        VSCROLL = 7,
        MINBUTTON = 8,
        MAXBUTTON = 9,
        LEFT = 10,
        RIGHT = 11,
        TOP = 12,
        TOPLEFT = 13,
        TOPRIGHT = 14,
        BOTTOM = 15,
        BOTTOMLEFT = 16,
        BOTTOMRIGHT = 17,
        BORDER = 18,
        REDUCE = MINBUTTON,
        ZOOM = MAXBUTTON,
        SIZEFIRST = LEFT,
        SIZELAST = BOTTOMRIGHT,
        OBJECT = 19,
        CLOSE = 20,
        HELP = 21
    }

    /// <summary>
    ///     GetClassLongPtr values, GCLP_*
    /// </summary>
    internal enum GCLP
    {
        HBRBACKGROUND = -10
    }

    /// <summary>
    ///     GetWindowLongPtr values, GWL_*
    /// </summary>
    public enum GWL
    {
        WNDPROC = (-4),
        HINSTANCE = (-6),
        HWNDPARENT = (-8),
        STYLE = (-16),
        EXSTYLE = (-20),
        USERDATA = (-21),
        ID = (-12)
    }

    /// <summary>
    ///     SystemMetrics.  SM_*
    /// </summary>
    public enum SM
    {
        CXSCREEN = 0,
        CYSCREEN = 1,
        CXVSCROLL = 2,
        CYHSCROLL = 3,
        CYCAPTION = 4,
        CXBORDER = 5,
        CYBORDER = 6,
        CXFIXEDFRAME = 7,
        CYFIXEDFRAME = 8,
        CYVTHUMB = 9,
        CXHTHUMB = 10,
        CXICON = 11,
        CYICON = 12,
        CXCURSOR = 13,
        CYCURSOR = 14,
        CYMENU = 15,
        CXFULLSCREEN = 16,
        CYFULLSCREEN = 17,
        CYKANJIWINDOW = 18,
        MOUSEPRESENT = 19,
        CYVSCROLL = 20,
        CXHSCROLL = 21,
        DEBUG = 22,
        SWAPBUTTON = 23,
        CXMIN = 28,
        CYMIN = 29,
        CXSIZE = 30,
        CYSIZE = 31,
        CXFRAME = 32,
        CXSIZEFRAME = CXFRAME,
        CYFRAME = 33,
        CYSIZEFRAME = CYFRAME,
        CXMINTRACK = 34,
        CYMINTRACK = 35,
        CXDOUBLECLK = 36,
        CYDOUBLECLK = 37,
        CXICONSPACING = 38,
        CYICONSPACING = 39,
        MENUDROPALIGNMENT = 40,
        PENWINDOWS = 41,
        DBCSENABLED = 42,
        CMOUSEBUTTONS = 43,
        SECURE = 44,
        CXEDGE = 45,
        CYEDGE = 46,
        CXMINSPACING = 47,
        CYMINSPACING = 48,
        CXSMICON = 49,
        CYSMICON = 50,
        CYSMCAPTION = 51,
        CXSMSIZE = 52,
        CYSMSIZE = 53,
        CXMENUSIZE = 54,
        CYMENUSIZE = 55,
        ARRANGE = 56,
        CXMINIMIZED = 57,
        CYMINIMIZED = 58,
        CXMAXTRACK = 59,
        CYMAXTRACK = 60,
        CXMAXIMIZED = 61,
        CYMAXIMIZED = 62,
        NETWORK = 63,
        CLEANBOOT = 67,
        CXDRAG = 68,
        CYDRAG = 69,
        SHOWSOUNDS = 70,
        CXMENUCHECK = 71,
        CYMENUCHECK = 72,
        SLOWMACHINE = 73,
        MIDEASTENABLED = 74,
        MOUSEWHEELPRESENT = 75,
        XVIRTUALSCREEN = 76,
        YVIRTUALSCREEN = 77,
        CXVIRTUALSCREEN = 78,
        CYVIRTUALSCREEN = 79,
        CMONITORS = 80,
        SAMEDISPLAYFORMAT = 81,
        IMMENABLED = 82,
        CXFOCUSBORDER = 83,
        CYFOCUSBORDER = 84,
        TABLETPC = 86,
        MEDIACENTER = 87,
        REMOTESESSION = 0x1000,
        REMOTECONTROL = 0x2001
    }

    /// <summary>
    ///     SystemParameterInfo values, SPI_*
    /// </summary>
    internal enum SPI
    {
        GETBEEP = 0x0001,
        SETBEEP = 0x0002,
        GETMOUSE = 0x0003,
        SETMOUSE = 0x0004,
        GETBORDER = 0x0005,
        SETBORDER = 0x0006,
        GETKEYBOARDSPEED = 0x000A,
        SETKEYBOARDSPEED = 0x000B,
        LANGDRIVER = 0x000C,
        ICONHORIZONTALSPACING = 0x000D,
        GETSCREENSAVETIMEOUT = 0x000E,
        SETSCREENSAVETIMEOUT = 0x000F,
        GETSCREENSAVEACTIVE = 0x0010,
        SETSCREENSAVEACTIVE = 0x0011,
        GETGRIDGRANULARITY = 0x0012,
        SETGRIDGRANULARITY = 0x0013,
        SETDESKWALLPAPER = 0x0014,
        SETDESKPATTERN = 0x0015,
        GETKEYBOARDDELAY = 0x0016,
        SETKEYBOARDDELAY = 0x0017,
        ICONVERTICALSPACING = 0x0018,
        GETICONTITLEWRAP = 0x0019,
        SETICONTITLEWRAP = 0x001A,
        GETMENUDROPALIGNMENT = 0x001B,
        SETMENUDROPALIGNMENT = 0x001C,
        SETDOUBLECLKWIDTH = 0x001D,
        SETDOUBLECLKHEIGHT = 0x001E,
        GETICONTITLELOGFONT = 0x001F,
        SETDOUBLECLICKTIME = 0x0020,
        SETMOUSEBUTTONSWAP = 0x0021,
        SETICONTITLELOGFONT = 0x0022,
        GETFASTTASKSWITCH = 0x0023,
        SETFASTTASKSWITCH = 0x0024,

        SETDRAGFULLWINDOWS = 0x0025,
        GETDRAGFULLWINDOWS = 0x0026,
        GETNONCLIENTMETRICS = 0x0029,
        SETNONCLIENTMETRICS = 0x002A,
        GETMINIMIZEDMETRICS = 0x002B,
        SETMINIMIZEDMETRICS = 0x002C,
        GETICONMETRICS = 0x002D,
        SETICONMETRICS = 0x002E,
        SETWORKAREA = 0x002F,
        GETWORKAREA = 0x0030,
        SETPENWINDOWS = 0x0031,
        GETHIGHCONTRAST = 0x0042,
        SETHIGHCONTRAST = 0x0043,
        GETKEYBOARDPREF = 0x0044,
        SETKEYBOARDPREF = 0x0045,
        GETSCREENREADER = 0x0046,
        SETSCREENREADER = 0x0047,
        GETANIMATION = 0x0048,
        SETANIMATION = 0x0049,
        GETFONTSMOOTHING = 0x004A,
        SETFONTSMOOTHING = 0x004B,
        SETDRAGWIDTH = 0x004C,
        SETDRAGHEIGHT = 0x004D,
        SETHANDHELD = 0x004E,
        GETLOWPOWERTIMEOUT = 0x004F,
        GETPOWEROFFTIMEOUT = 0x0050,
        SETLOWPOWERTIMEOUT = 0x0051,
        SETPOWEROFFTIMEOUT = 0x0052,
        GETLOWPOWERACTIVE = 0x0053,
        GETPOWEROFFACTIVE = 0x0054,
        SETLOWPOWERACTIVE = 0x0055,
        SETPOWEROFFACTIVE = 0x0056,
        SETCURSORS = 0x0057,
        SETICONS = 0x0058,
        GETDEFAULTINPUTLANG = 0x0059,
        SETDEFAULTINPUTLANG = 0x005A,
        SETLANGTOGGLE = 0x005B,
        GETWINDOWSEXTENSION = 0x005C,
        SETMOUSETRAILS = 0x005D,
        GETMOUSETRAILS = 0x005E,
        SETSCREENSAVERRUNNING = 0x0061,
        SCREENSAVERRUNNING = SETSCREENSAVERRUNNING,
        GETFILTERKEYS = 0x0032,
        SETFILTERKEYS = 0x0033,
        GETTOGGLEKEYS = 0x0034,
        SETTOGGLEKEYS = 0x0035,
        GETMOUSEKEYS = 0x0036,
        SETMOUSEKEYS = 0x0037,
        GETSHOWSOUNDS = 0x0038,
        SETSHOWSOUNDS = 0x0039,
        GETSTICKYKEYS = 0x003A,
        SETSTICKYKEYS = 0x003B,
        GETACCESSTIMEOUT = 0x003C,
        SETACCESSTIMEOUT = 0x003D,

        GETSERIALKEYS = 0x003E,
        SETSERIALKEYS = 0x003F,
        GETSOUNDSENTRY = 0x0040,
        SETSOUNDSENTRY = 0x0041,
        GETSNAPTODEFBUTTON = 0x005F,
        SETSNAPTODEFBUTTON = 0x0060,
        GETMOUSEHOVERWIDTH = 0x0062,
        SETMOUSEHOVERWIDTH = 0x0063,
        GETMOUSEHOVERHEIGHT = 0x0064,
        SETMOUSEHOVERHEIGHT = 0x0065,
        GETMOUSEHOVERTIME = 0x0066,
        SETMOUSEHOVERTIME = 0x0067,
        GETWHEELSCROLLLINES = 0x0068,
        SETWHEELSCROLLLINES = 0x0069,
        GETMENUSHOWDELAY = 0x006A,
        SETMENUSHOWDELAY = 0x006B,

        GETWHEELSCROLLCHARS = 0x006C,
        SETWHEELSCROLLCHARS = 0x006D,

        GETSHOWIMEUI = 0x006E,
        SETSHOWIMEUI = 0x006F,

        GETMOUSESPEED = 0x0070,
        SETMOUSESPEED = 0x0071,
        GETSCREENSAVERRUNNING = 0x0072,
        GETDESKWALLPAPER = 0x0073,

        GETAUDIODESCRIPTION = 0x0074,
        SETAUDIODESCRIPTION = 0x0075,

        GETSCREENSAVESECURE = 0x0076,
        SETSCREENSAVESECURE = 0x0077,

        GETHUNGAPPTIMEOUT = 0x0078,
        SETHUNGAPPTIMEOUT = 0x0079,
        GETWAITTOKILLTIMEOUT = 0x007A,
        SETWAITTOKILLTIMEOUT = 0x007B,
        GETWAITTOKILLSERVICETIMEOUT = 0x007C,
        SETWAITTOKILLSERVICETIMEOUT = 0x007D,
        GETMOUSEDOCKTHRESHOLD = 0x007E,
        SETMOUSEDOCKTHRESHOLD = 0x007F,
        GETPENDOCKTHRESHOLD = 0x0080,
        SETPENDOCKTHRESHOLD = 0x0081,
        GETWINARRANGING = 0x0082,
        SETWINARRANGING = 0x0083,
        GETMOUSEDRAGOUTTHRESHOLD = 0x0084,
        SETMOUSEDRAGOUTTHRESHOLD = 0x0085,
        GETPENDRAGOUTTHRESHOLD = 0x0086,
        SETPENDRAGOUTTHRESHOLD = 0x0087,
        GETMOUSESIDEMOVETHRESHOLD = 0x0088,
        SETMOUSESIDEMOVETHRESHOLD = 0x0089,
        GETPENSIDEMOVETHRESHOLD = 0x008A,
        SETPENSIDEMOVETHRESHOLD = 0x008B,
        GETDRAGFROMMAXIMIZE = 0x008C,
        SETDRAGFROMMAXIMIZE = 0x008D,
        GETSNAPSIZING = 0x008E,
        SETSNAPSIZING = 0x008F,
        GETDOCKMOVING = 0x0090,
        SETDOCKMOVING = 0x0091,

        GETACTIVEWINDOWTRACKING = 0x1000,
        SETACTIVEWINDOWTRACKING = 0x1001,
        GETMENUANIMATION = 0x1002,
        SETMENUANIMATION = 0x1003,
        GETCOMBOBOXANIMATION = 0x1004,
        SETCOMBOBOXANIMATION = 0x1005,
        GETLISTBOXSMOOTHSCROLLING = 0x1006,
        SETLISTBOXSMOOTHSCROLLING = 0x1007,
        GETGRADIENTCAPTIONS = 0x1008,
        SETGRADIENTCAPTIONS = 0x1009,
        GETKEYBOARDCUES = 0x100A,
        SETKEYBOARDCUES = 0x100B,
        GETMENUUNDERLINES = GETKEYBOARDCUES,
        SETMENUUNDERLINES = SETKEYBOARDCUES,
        GETACTIVEWNDTRKZORDER = 0x100C,
        SETACTIVEWNDTRKZORDER = 0x100D,
        GETHOTTRACKING = 0x100E,
        SETHOTTRACKING = 0x100F,
        GETMENUFADE = 0x1012,
        SETMENUFADE = 0x1013,
        GETSELECTIONFADE = 0x1014,
        SETSELECTIONFADE = 0x1015,
        GETTOOLTIPANIMATION = 0x1016,
        SETTOOLTIPANIMATION = 0x1017,
        GETTOOLTIPFADE = 0x1018,
        SETTOOLTIPFADE = 0x1019,
        GETCURSORSHADOW = 0x101A,
        SETCURSORSHADOW = 0x101B,
        GETMOUSESONAR = 0x101C,
        SETMOUSESONAR = 0x101D,
        GETMOUSECLICKLOCK = 0x101E,
        SETMOUSECLICKLOCK = 0x101F,
        GETMOUSEVANISH = 0x1020,
        SETMOUSEVANISH = 0x1021,
        GETFLATMENU = 0x1022,
        SETFLATMENU = 0x1023,
        GETDROPSHADOW = 0x1024,
        SETDROPSHADOW = 0x1025,
        GETBLOCKSENDINPUTRESETS = 0x1026,
        SETBLOCKSENDINPUTRESETS = 0x1027,

        GETUIEFFECTS = 0x103E,
        SETUIEFFECTS = 0x103F,

        GETDISABLEOVERLAPPEDCONTENT = 0x1040,
        SETDISABLEOVERLAPPEDCONTENT = 0x1041,
        GETCLIENTAREAANIMATION = 0x1042,
        SETCLIENTAREAANIMATION = 0x1043,
        GETCLEARTYPE = 0x1048,
        SETCLEARTYPE = 0x1049,
        GETSPEECHRECOGNITION = 0x104A,
        SETSPEECHRECOGNITION = 0x104B,

        GETFOREGROUNDLOCKTIMEOUT = 0x2000,
        SETFOREGROUNDLOCKTIMEOUT = 0x2001,
        GETACTIVEWNDTRKTIMEOUT = 0x2002,
        SETACTIVEWNDTRKTIMEOUT = 0x2003,
        GETFOREGROUNDFLASHCOUNT = 0x2004,
        SETFOREGROUNDFLASHCOUNT = 0x2005,
        GETCARETWIDTH = 0x2006,
        SETCARETWIDTH = 0x2007,

        GETMOUSECLICKLOCKTIME = 0x2008,
        SETMOUSECLICKLOCKTIME = 0x2009,
        GETFONTSMOOTHINGTYPE = 0x200A,
        SETFONTSMOOTHINGTYPE = 0x200B,

        GETFONTSMOOTHINGCONTRAST = 0x200C,
        SETFONTSMOOTHINGCONTRAST = 0x200D,

        GETFOCUSBORDERWIDTH = 0x200E,
        SETFOCUSBORDERWIDTH = 0x200F,
        GETFOCUSBORDERHEIGHT = 0x2010,
        SETFOCUSBORDERHEIGHT = 0x2011,

        GETFONTSMOOTHINGORIENTATION = 0x2012,
        SETFONTSMOOTHINGORIENTATION = 0x2013,

        GETMINIMUMHITRADIUS = 0x2014,
        SETMINIMUMHITRADIUS = 0x2015,
        GETMESSAGEDURATION = 0x2016,
        SETMESSAGEDURATION = 0x2017
    }

    /// <summary>
    ///     SystemParameterInfo flag values, SPIF_*
    /// </summary>
    [Flags]
    internal enum SPIF
    {
        None = 0,
        UPDATEINIFILE = 0x01,
        SENDCHANGE = 0x02,
        SENDWININICHANGE = SENDCHANGE
    }

    [Flags]
    internal enum STATE_SYSTEM
    {
        UNAVAILABLE = 0x00000001, // Disabled
        SELECTED = 0x00000002,
        FOCUSED = 0x00000004,
        PRESSED = 0x00000008,
        CHECKED = 0x00000010,
        MIXED = 0x00000020, // 3-state checkbox or toolbar button
        INDETERMINATE = MIXED,
        READONLY = 0x00000040,
        HOTTRACKED = 0x00000080,
        DEFAULT = 0x00000100,
        EXPANDED = 0x00000200,
        COLLAPSED = 0x00000400,
        BUSY = 0x00000800,
        FLOATING = 0x00001000, // Children "owned" not "contained" by parent
        MARQUEED = 0x00002000,
        ANIMATED = 0x00004000,
        INVISIBLE = 0x00008000,
        OFFSCREEN = 0x00010000,
        SIZEABLE = 0x00020000,
        MOVEABLE = 0x00040000,
        SELFVOICING = 0x00080000,
        FOCUSABLE = 0x00100000,
        SELECTABLE = 0x00200000,
        LINKED = 0x00400000,
        TRAVERSED = 0x00800000,
        MULTISELECTABLE = 0x01000000, // Supports multiple selection
        EXTSELECTABLE = 0x02000000, // Supports extended selection
        ALERT_LOW = 0x04000000, // This information is of low priority
        ALERT_MEDIUM = 0x08000000, // This information is of medium priority
        ALERT_HIGH = 0x10000000, // This information is of high priority
        PROTECTED = 0x20000000, // access to this is restricted
        VALID = 0x3FFFFFFF
    }

    public enum StockObject
    {
        WHITE_BRUSH = 0,
        LTGRAY_BRUSH = 1,
        GRAY_BRUSH = 2,
        DKGRAY_BRUSH = 3,
        BLACK_BRUSH = 4,
        NULL_BRUSH = 5,
        HOLLOW_BRUSH = NULL_BRUSH,
        WHITE_PEN = 6,
        BLACK_PEN = 7,
        NULL_PEN = 8,
        SYSTEM_FONT = 13,
        DEFAULT_PALETTE = 15
    }

    /// <summary>
    ///     CS_*
    /// </summary>
    [Flags]
    internal enum CS : uint
    {
        VREDRAW = 0x0001,
        HREDRAW = 0x0002,
        DBLCLKS = 0x0008,
        OWNDC = 0x0020,
        CLASSDC = 0x0040,
        PARENTDC = 0x0080,
        NOCLOSE = 0x0200,
        SAVEBITS = 0x0800,
        BYTEALIGNCLIENT = 0x1000,
        BYTEALIGNWINDOW = 0x2000,
        GLOBALCLASS = 0x4000,
        IME = 0x00010000,
        DROPSHADOW = 0x00020000
    }

    /// <summary>
    ///     WindowStyle values, WS_*
    /// </summary>
    [Flags]
    internal enum WS : uint
    {
        OVERLAPPED = 0x00000000,
        POPUP = 0x80000000,
        CHILD = 0x40000000,
        MINIMIZE = 0x20000000,
        VISIBLE = 0x10000000,
        DISABLED = 0x08000000,
        CLIPSIBLINGS = 0x04000000,
        CLIPCHILDREN = 0x02000000,
        MAXIMIZE = 0x01000000,
        BORDER = 0x00800000,
        DLGFRAME = 0x00400000,
        VSCROLL = 0x00200000,
        HSCROLL = 0x00100000,
        SYSMENU = 0x00080000,
        THICKFRAME = 0x00040000,
        GROUP = 0x00020000,
        TABSTOP = 0x00010000,

        MINIMIZEBOX = 0x00020000,
        MAXIMIZEBOX = 0x00010000,

        CAPTION = BORDER | DLGFRAME,
        TILED = OVERLAPPED,
        ICONIC = MINIMIZE,
        SIZEBOX = THICKFRAME,
        TILEDWINDOW = OVERLAPPEDWINDOW,

        OVERLAPPEDWINDOW = OVERLAPPED | CAPTION | SYSMENU | THICKFRAME | MINIMIZEBOX | MAXIMIZEBOX,
        POPUPWINDOW = POPUP | BORDER | SYSMENU,
        CHILDWINDOW = CHILD
    }

    /// <summary>
    ///     Window message values, WM_*
    /// </summary>
    public enum WM
    {
        NULL = 0x0000,
        CREATE = 0x0001,
        DESTROY = 0x0002,
        MOVE = 0x0003,
        SIZE = 0x0005,
        ACTIVATE = 0x0006,
        SETFOCUS = 0x0007,
        KILLFOCUS = 0x0008,
        ENABLE = 0x000A,
        SETREDRAW = 0x000B,
        SETTEXT = 0x000C,
        GETTEXT = 0x000D,
        GETTEXTLENGTH = 0x000E,
        PAINT = 0x000F,
        CLOSE = 0x0010,
        QUERYENDSESSION = 0x0011,
        QUIT = 0x0012,
        QUERYOPEN = 0x0013,
        ERASEBKGND = 0x0014,
        SYSCOLORCHANGE = 0x0015,
        SHOWWINDOW = 0x0018,
        CTLCOLOR = 0x0019,
        WININICHANGE = 0x001A,
        SETTINGCHANGE = 0x001A,
        ACTIVATEAPP = 0x001C,
        SETCURSOR = 0x0020,
        MOUSEACTIVATE = 0x0021,
        CHILDACTIVATE = 0x0022,
        QUEUESYNC = 0x0023,
        GETMINMAXINFO = 0x0024,

        WINDOWPOSCHANGING = 0x0046,
        WINDOWPOSCHANGED = 0x0047,

        CONTEXTMENU = 0x007B,
        STYLECHANGING = 0x007C,
        STYLECHANGED = 0x007D,
        DISPLAYCHANGE = 0x007E,
        GETICON = 0x007F,
        SETICON = 0x0080,
        NCCREATE = 0x0081,
        NCDESTROY = 0x0082,
        NCCALCSIZE = 0x0083,
        NCHITTEST = 0x0084,
        NCPAINT = 0x0085,
        NCACTIVATE = 0x0086,
        GETDLGCODE = 0x0087,
        SYNCPAINT = 0x0088,
        NCMOUSEMOVE = 0x00A0,
        NCLBUTTONDOWN = 0x00A1,
        NCLBUTTONUP = 0x00A2,
        NCLBUTTONDBLCLK = 0x00A3,
        NCRBUTTONDOWN = 0x00A4,
        NCRBUTTONUP = 0x00A5,
        NCRBUTTONDBLCLK = 0x00A6,
        NCMBUTTONDOWN = 0x00A7,
        NCMBUTTONUP = 0x00A8,
        NCMBUTTONDBLCLK = 0x00A9,

        SYSKEYDOWN = 0x0104,
        SYSKEYUP = 0x0105,
        SYSCHAR = 0x0106,
        SYSDEADCHAR = 0x0107,
        COMMAND = 0x0111,
        SYSCOMMAND = 0x0112,

        MOUSEMOVE = 0x0200,
        LBUTTONDOWN = 0x0201,
        LBUTTONUP = 0x0202,
        LBUTTONDBLCLK = 0x0203,
        RBUTTONDOWN = 0x0204,
        RBUTTONUP = 0x0205,
        RBUTTONDBLCLK = 0x0206,
        MBUTTONDOWN = 0x0207,
        MBUTTONUP = 0x0208,
        MBUTTONDBLCLK = 0x0209,
        MOUSEWHEEL = 0x020A,
        XBUTTONDOWN = 0x020B,
        XBUTTONUP = 0x020C,
        XBUTTONDBLCLK = 0x020D,
        MOUSEHWHEEL = 0x020E,
        PARENTNOTIFY = 0x0210,

        CAPTURECHANGED = 0x0215,
        POWERBROADCAST = 0x0218,
        DEVICECHANGE = 0x0219,

        ENTERSIZEMOVE = 0x0231,
        EXITSIZEMOVE = 0x0232,

        IME_SETCONTEXT = 0x0281,
        IME_NOTIFY = 0x0282,
        IME_CONTROL = 0x0283,
        IME_COMPOSITIONFULL = 0x0284,
        IME_SELECT = 0x0285,
        IME_CHAR = 0x0286,
        IME_REQUEST = 0x0288,
        IME_KEYDOWN = 0x0290,
        IME_KEYUP = 0x0291,

        NCMOUSELEAVE = 0x02A2,

        TABLET_DEFBASE = 0x02C0,
        //WM_TABLET_MAXOFFSET = 0x20,

        TABLET_ADDED = TABLET_DEFBASE + 8,
        TABLET_DELETED = TABLET_DEFBASE + 9,
        TABLET_FLICK = TABLET_DEFBASE + 11,
        TABLET_QUERYSYSTEMGESTURESTATUS = TABLET_DEFBASE + 12,

        CUT = 0x0300,
        COPY = 0x0301,
        PASTE = 0x0302,
        CLEAR = 0x0303,
        UNDO = 0x0304,
        RENDERFORMAT = 0x0305,
        RENDERALLFORMATS = 0x0306,
        DESTROYCLIPBOARD = 0x0307,
        DRAWCLIPBOARD = 0x0308,
        PAINTCLIPBOARD = 0x0309,
        VSCROLLCLIPBOARD = 0x030A,
        SIZECLIPBOARD = 0x030B,
        ASKCBFORMATNAME = 0x030C,
        CHANGECBCHAIN = 0x030D,
        HSCROLLCLIPBOARD = 0x030E,
        QUERYNEWPALETTE = 0x030F,
        PALETTEISCHANGING = 0x0310,
        PALETTECHANGED = 0x0311,
        HOTKEY = 0x0312,
        PRINT = 0x0317,
        PRINTCLIENT = 0x0318,
        APPCOMMAND = 0x0319,
        THEMECHANGED = 0x031A,

        DWMCOMPOSITIONCHANGED = 0x031E,
        DWMNCRENDERINGCHANGED = 0x031F,
        DWMCOLORIZATIONCOLORCHANGED = 0x0320,
        DWMWINDOWMAXIMIZEDCHANGE = 0x0321,

        GETTITLEBARINFOEX = 0x033F,

        #region Windows 7

        DWMSENDICONICTHUMBNAIL = 0x0323,
        DWMSENDICONICLIVEPREVIEWBITMAP = 0x0326,

        #endregion

        USER = 0x0400,

        // This is the hard-coded message value used by WinForms for Shell_NotifyIcon.
        // It's relatively safe to reuse.
        TRAYMOUSEMESSAGE = 0x800, //WM_USER + 1024
        APP = 0x8000
    }

    /// <summary>
    ///     Window style extended values, WS_EX_*
    /// </summary>
    [Flags]
    internal enum WS_EX : uint
    {
        None = 0,
        DLGMODALFRAME = 0x00000001,
        NOPARENTNOTIFY = 0x00000004,
        TOPMOST = 0x00000008,
        ACCEPTFILES = 0x00000010,
        TRANSPARENT = 0x00000020,
        MDICHILD = 0x00000040,
        TOOLWINDOW = 0x00000080,
        WINDOWEDGE = 0x00000100,
        CLIENTEDGE = 0x00000200,
        CONTEXTHELP = 0x00000400,
        RIGHT = 0x00001000,
        LEFT = 0x00000000,
        RTLREADING = 0x00002000,
        LTRREADING = 0x00000000,
        LEFTSCROLLBAR = 0x00004000,
        RIGHTSCROLLBAR = 0x00000000,
        CONTROLPARENT = 0x00010000,
        STATICEDGE = 0x00020000,
        APPWINDOW = 0x00040000,
        LAYERED = 0x00080000,
        NOINHERITLAYOUT = 0x00100000, // Disable inheritence of mirroring by children
        LAYOUTRTL = 0x00400000, // Right to left mirroring
        COMPOSITED = 0x02000000,
        NOACTIVATE = 0x08000000,
        OVERLAPPEDWINDOW = (WINDOWEDGE | CLIENTEDGE),
        PALETTEWINDOW = (WINDOWEDGE | TOOLWINDOW | TOPMOST)
    }

    /// <summary>
    ///     GetDeviceCaps nIndex values.
    /// </summary>
    public enum DeviceCap
    {
        /// <summary>
        ///     Number of bits per pixel
        /// </summary>
        BITSPIXEL = 12,

        /// <summary>
        ///     Number of planes
        /// </summary>
        PLANES = 14,

        /// <summary>
        ///     Logical pixels inch in X
        /// </summary>
        LOGPIXELSX = 88,

        /// <summary>
        ///     Logical pixels inch in Y
        /// </summary>
        LOGPIXELSY = 90
    }

    internal enum FO
    {
        MOVE = 0x0001,
        COPY = 0x0002,
        DELETE = 0x0003,
        RENAME = 0x0004
    }

    /// <summary>
    ///     "FILEOP_FLAGS", FOF_*.
    /// </summary>
    internal enum FOF : ushort
    {
        MULTIDESTFILES = 0x0001,
        CONFIRMMOUSE = 0x0002,
        SILENT = 0x0004,
        RENAMEONCOLLISION = 0x0008,
        NOCONFIRMATION = 0x0010,
        WANTMAPPINGHANDLE = 0x0020,
        ALLOWUNDO = 0x0040,
        FILESONLY = 0x0080,
        SIMPLEPROGRESS = 0x0100,
        NOCONFIRMMKDIR = 0x0200,
        NOERRORUI = 0x0400,
        NOCOPYSECURITYATTRIBS = 0x0800,
        NORECURSION = 0x1000,
        NO_CONNECTED_ELEMENTS = 0x2000,
        WANTNUKEWARNING = 0x4000,
        NORECURSEREPARSE = 0x8000
    }

    /// <summary>
    ///     EnableMenuItem uEnable values, MF_*
    /// </summary>
    [Flags]
    internal enum MF : uint
    {
        /// <summary>
        ///     Possible return value for EnableMenuItem
        /// </summary>
        DOES_NOT_EXIST = unchecked((uint)-1),
        ENABLED = 0,
        BYCOMMAND = 0,
        GRAYED = 1,
        DISABLED = 2
    }

    /// <summary>Specifies the type of visual style attribute to set on a window.</summary>
    internal enum WINDOWTHEMEATTRIBUTETYPE : uint
    {
        /// <summary>Non-client area window attributes will be set.</summary>
        WTA_NONCLIENT = 1
    }

    /// <summary>
    ///     DWMFLIP3DWINDOWPOLICY.  DWMFLIP3D_*
    /// </summary>
    internal enum DWMFLIP3D
    {
        DEFAULT,
        EXCLUDEBELOW,
        EXCLUDEABOVE
        //LAST
    }

    /// <summary>
    ///     DWMNCRENDERINGPOLICY. DWMNCRP_*
    /// </summary>
    internal enum DWMNCRP
    {
        USEWINDOWSTYLE,
        DISABLED,
        ENABLED
        //LAST
    }

    /// <summary>
    ///     DWMWINDOWATTRIBUTE.  DWMWA_*
    /// </summary>
    internal enum DWMWA
    {
        NCRENDERING_ENABLED = 1,
        NCRENDERING_POLICY,
        TRANSITIONS_FORCEDISABLED,
        ALLOW_NCPAINT,
        CAPTION_BUTTON_BOUNDS,
        NONCLIENT_RTL_LAYOUT,
        FORCE_ICONIC_REPRESENTATION,
        FLIP3D_POLICY,
        EXTENDED_FRAME_BOUNDS,

        // New to Windows 7:

        HAS_ICONIC_BITMAP,
        DISALLOW_PEEK,
        EXCLUDED_FROM_PEEK

        // LAST
    }

    /// <summary>
    ///     WindowThemeNonClientAttributes
    /// </summary>
    [Flags]
    internal enum WTNCA : uint
    {
        /// <summary>Prevents the window caption from being drawn.</summary>
        NODRAWCAPTION = 0x00000001,

        /// <summary>Prevents the system icon from being drawn.</summary>
        NODRAWICON = 0x00000002,

        /// <summary>Prevents the system icon menu from appearing.</summary>
        NOSYSMENU = 0x00000004,

        /// <summary>Prevents mirroring of the question mark, even in right-to-left (RTL) layout.</summary>
        NOMIRRORHELP = 0x00000008,

        /// <summary> A mask that contains all the valid bits.</summary>
        VALIDBITS = NODRAWCAPTION | NODRAWICON | NOMIRRORHELP | NOSYSMENU
    }

    /// <summary>
    ///     SetWindowPos options
    /// </summary>
    [Flags]
    public enum SWP
    {
        ASYNCWINDOWPOS = 0x4000,
        DEFERERASE = 0x2000,
        DRAWFRAME = 0x0020,
        FRAMECHANGED = 0x0020,
        HIDEWINDOW = 0x0080,
        NOACTIVATE = 0x0010,
        NOCOPYBITS = 0x0100,
        NOMOVE = 0x0002,
        NOOWNERZORDER = 0x0200,
        NOREDRAW = 0x0008,
        NOREPOSITION = 0x0200,
        NOSENDCHANGING = 0x0400,
        NOSIZE = 0x0001,
        NOZORDER = 0x0004,
        SHOWWINDOW = 0x0040
    }

    /// <summary>
    ///     ShowWindow options
    /// </summary>
    public enum SW
    {
        HIDE = 0,
        SHOWNORMAL = 1,
        NORMAL = 1,
        SHOWMINIMIZED = 2,
        SHOWMAXIMIZED = 3,
        MAXIMIZE = 3,
        SHOWNOACTIVATE = 4,
        SHOW = 5,
        MINIMIZE = 6,
        SHOWMINNOACTIVE = 7,
        SHOWNA = 8,
        RESTORE = 9,
        SHOWDEFAULT = 10,
        FORCEMINIMIZE = 11
    }

    public enum SC
    {
        SIZE = 0xF000,
        MOVE = 0xF010,
        MOUSEMOVE = 0xF012,
        MINIMIZE = 0xF020,
        MAXIMIZE = 0xF030,
        NEXTWINDOW = 0xF040,
        PREVWINDOW = 0xF050,
        CLOSE = 0xF060,
        VSCROLL = 0xF070,
        HSCROLL = 0xF080,
        MOUSEMENU = 0xF090,
        KEYMENU = 0xF100,
        ARRANGE = 0xF110,
        RESTORE = 0xF120,
        TASKLIST = 0xF130,
        SCREENSAVE = 0xF140,
        HOTKEY = 0xF150,
        DEFAULT = 0xF160,
        MONITORPOWER = 0xF170,
        CONTEXTHELP = 0xF180,
        SEPARATOR = 0xF00F,
        /// <summary>
        /// SCF_ISSECURE
        /// </summary>
        F_ISSECURE = 0x00000001,
        ICON = MINIMIZE,
        ZOOM = MAXIMIZE,
    }

    /// <summary>
    ///     GDI+ Status codes
    /// </summary>
    internal enum Status
    {
        Ok = 0,
        GenericError = 1,
        InvalidParameter = 2,
        OutOfMemory = 3,
        ObjectBusy = 4,
        InsufficientBuffer = 5,
        NotImplemented = 6,
        Win32Error = 7,
        WrongState = 8,
        Aborted = 9,
        FileNotFound = 10,
        ValueOverflow = 11,
        AccessDenied = 12,
        UnknownImageFormat = 13,
        FontFamilyNotFound = 14,
        FontStyleNotFound = 15,
        NotTrueTypeFont = 16,
        UnsupportedGdiplusVersion = 17,
        GdiplusNotInitialized = 18,
        PropertyNotFound = 19,
        PropertyNotSupported = 20,
        ProfileNotFound = 21
    }

    internal enum MOUSEEVENTF
    {
        //mouse event constants
        LEFTDOWN = 2,
        LEFTUP = 4
    }

    /// <summary>
    ///     MSGFLT_*.  New in Vista.  Realiased in Windows 7.
    /// </summary>
    internal enum MSGFLT
    {
        // Win7 versions of this enum:
        RESET = 0,
        ALLOW = 1,
        DISALLOW = 2

        // Vista versions of this enum:
        // ADD = 1,
        // REMOVE = 2,
    }

    internal enum MSGFLTINFO
    {
        NONE = 0,
        ALREADYALLOWED_FORWND = 1,
        ALREADYDISALLOWED_FORWND = 2,
        ALLOWED_HIGHER = 3
    }

    internal enum INPUT_TYPE : uint
    {
        MOUSE = 0
    }

    /// <summary>
    ///     Shell_NotifyIcon messages.  NIM_*
    /// </summary>
    internal enum NIM : uint
    {
        ADD = 0,
        MODIFY = 1,
        DELETE = 2,
        SETFOCUS = 3,
        SETVERSION = 4
    }

    /// <summary>
    ///     SHAddToRecentDocuments flags.  SHARD_*
    /// </summary>
    internal enum SHARD
    {
        PIDL = 0x00000001,
        PATHA = 0x00000002,
        PATHW = 0x00000003,
        APPIDINFO = 0x00000004, // indicates the data type is a pointer to a SHARDAPPIDINFO structure
        APPIDINFOIDLIST = 0x00000005, // indicates the data type is a pointer to a SHARDAPPIDINFOIDLIST structure
        LINK = 0x00000006, // indicates the data type is a pointer to an IShellLink instance
        APPIDINFOLINK = 0x00000007 // indicates the data type is a pointer to a SHARDAPPIDINFOLINK structure 
    }

    [Flags]
    enum SLGP
    {
        SHORTPATH = 0x1,
        UNCPRIORITY = 0x2,
        RAWPATH = 0x4
    }

    /// <summary>
    ///     Shell_NotifyIcon flags.  NIF_*
    /// </summary>
    [Flags]
    internal enum NIF : uint
    {
        MESSAGE = 0x0001,
        ICON = 0x0002,
        TIP = 0x0004,
        STATE = 0x0008,
        INFO = 0x0010,
        GUID = 0x0020,

        /// <summary>
        ///     Vista only.
        /// </summary>
        REALTIME = 0x0040,

        /// <summary>
        ///     Vista only.
        /// </summary>
        SHOWTIP = 0x0080,

        XP_MASK = MESSAGE | ICON | STATE | INFO | GUID,
        VISTA_MASK = XP_MASK | REALTIME | SHOWTIP
    }

    /// <summary>
    ///     Shell_NotifyIcon info flags.  NIIF_*
    /// </summary>
    internal enum NIIF
    {
        NONE = 0x00000000,
        INFO = 0x00000001,
        WARNING = 0x00000002,
        ERROR = 0x00000003,

        /// <summary>XP SP2 and later.</summary>
        USER = 0x00000004,

        /// <summary>XP and later.</summary>
        NOSOUND = 0x00000010,

        /// <summary>Vista and later.</summary>
        LARGE_ICON = 0x00000020,

        /// <summary>Windows 7 and later</summary>
        NIIF_RESPECT_QUIET_TIME = 0x00000080,

        /// <summary>XP and later.  Native version called NIIF_ICON_MASK.</summary>
        XP_ICON_MASK = 0x0000000F
    }

    /// <summary>
    ///     AC_*
    /// </summary>
    internal enum AC : byte
    {
        SRC_OVER = 0,
        SRC_ALPHA = 1
    }

    internal enum ULW
    {
        ALPHA = 2,
        COLORKEY = 1,
        OPAQUE = 4
    }

    internal enum WVR
    {
        ALIGNTOP = 0x0010,
        ALIGNLEFT = 0x0020,
        ALIGNBOTTOM = 0x0040,
        ALIGNRIGHT = 0x0080,
        HREDRAW = 0x0100,
        VREDRAW = 0x0200,
        VALIDRECTS = 0x0400,
        REDRAW = HREDRAW | VREDRAW
    }

    internal static class Win32Value
    {
        public const uint MAX_PATH = 260;
        public const uint INFOTIPSIZE = 1024;
        public const uint TRUE = 1;
        public const uint FALSE = 0;
        public const uint sizeof_WCHAR = 2;
        public const uint sizeof_CHAR = 1;
        public const uint sizeof_BOOL = 4;
    }
}