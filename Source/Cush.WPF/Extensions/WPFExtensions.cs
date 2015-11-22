using System.Windows;
using Cush.Common.Exceptions;


namespace Cush.WPF
{
    public static class WPFExtensions
    {
        /// <summary>
        ///     Brings the window to the front of the z-order,
        ///     even if the window is a child of another application.
        /// </summary>
        public static Window SetOnTop(this Window window)
        {
            ThrowHelper.IfNullThenThrow(() => window);

            var oldValue = window.Topmost;
            window.Topmost = true;
            window.Activate();
            window.Topmost = oldValue;
            return window;
        }
    }
}