using System;

namespace Cush.Common.Interaction
{
    [Flags]
    public enum BreadcrumbEllipsisFormat
    {
        // Text is not modified.
        None = 0,
        // Text is trimmed at the end of the string. An ellipsis (...) 
        // is drawn in place of remaining text.
        End = 1,
        // Text is trimmed at the beginning of the string. 
        // An ellipsis (...) is drawn in place of remaining text. 
        Start = 2,
        // Text is trimmed in the middle of the string. 
        // An ellipsis (...) is drawn in place of remaining text.
        Middle = 3
    }
}