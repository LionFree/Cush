using System;
using System.Threading;
using System.Windows.Forms;
using Cush.Common.Exceptions;
using Cush.Common.Interaction;
using Cush.Common.Logging;

namespace meh.Exceptions
{
    public abstract class WinFormsExceptionHandler : ExceptionHandler<ThreadExceptionEventArgs>
    {
        protected WinFormsExceptionHandler(ILogger logger, IDialogs dialogs) : base(logger, dialogs)
        {
        }

        protected WinFormsExceptionHandler(ILogger logger, IDialogs dialogs, BindingErrorListener listener)
            : base(logger, dialogs, listener)
        {
            // Set the unhandled exception mode to force all Windows Forms errors to go through
            // our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        }

        /// <summary>
        ///     Attaches the exception handler to the given <see cref="AppDomain" /> and <see cref="Application" />.
        /// </summary>
        public abstract void AttachExceptionHandler(AppDomain domain, Application application);

        /// <summary>
        ///     Displays an error dialog containing an error message which is
        ///     formatted with the submitted exception's <see cref="Exception.Message" />.
        /// </summary>
        /// <param name="exception">Exception to be displayed.</param>
        /// <param name="owner">The <see cref="Form" /> that will own the error dialog.</param>
        public abstract void ReportException(Exception exception, Form owner);

        /// <summary>
        ///     Displays an error dialog containing an error message which is
        ///     formatted with the submitted exception's <see cref="Exception.Message" />,
        ///     and logs the exception to the application log.
        /// </summary>
        /// <param name="exception">Exception to be logged.</param>
        /// <param name="owner">The <see cref="Form" /> that will own the error dialog.</param>
        public abstract void LogAndReportException(Exception exception, Form owner);
    }
}