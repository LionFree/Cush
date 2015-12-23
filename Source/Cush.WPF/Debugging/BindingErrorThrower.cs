/*
 * This has been inpired by  
 * http://tech.pro/tutorial/940/wpf-snippet-detecting-binding-errors
 */

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Cush.WPF.Debugging
{
    /// <summary>
    /// Converts WPF binding error into BindingException
    /// </summary>
    public static class BindingExceptionThrower
    {
        private static BindingErrorListener _errorListener;

        /// <summary>
        /// Start listening WPF binding error
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static void Attach()
        {
            _errorListener = new BindingErrorListener();
            _errorListener.ErrorCaught += OnErrorCaught;
        }

        /// <summary>
        /// Stop listening WPF binding error
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static void Detach()
        {
            _errorListener.ErrorCaught -= OnErrorCaught;
            _errorListener.Dispose();
            _errorListener = null;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static bool IsAttached
        {
            get { return _errorListener != null; }
        }

        [DebuggerStepThrough]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        static void OnErrorCaught(string message)
        {
            throw new BindingException(message);
        }
    }
}
