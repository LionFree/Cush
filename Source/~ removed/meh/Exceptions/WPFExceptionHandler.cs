﻿using System;
using System.Windows;
using System.Windows.Threading;
using Cush.Common.Exceptions;
using Cush.Common.Interaction;
using Cush.Common.Logging;

namespace meh.Exceptions
{
    public abstract class WPFExceptionHandler : ExceptionHandler<DispatcherUnhandledExceptionEventArgs>
    {
        protected WPFExceptionHandler(ILogger logger, IDialogs dialogs)
            : base(logger, dialogs)
        {
        }

        protected WPFExceptionHandler(ILogger logger, IDialogs dialogs, BindingErrorListener listener)
            : base(logger, dialogs, listener)
        {
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
        /// <param name="window">The <see cref="Window" /> that will own the error dialog.</param>
        public abstract void ReportException(Exception exception, Window window);

        /// <summary>
        ///     Displays an error dialog containing an error message which is
        ///     formatted with the submitted exception's <see cref="Exception.Message" />,
        ///     and logs the exception to the application log.
        /// </summary>
        /// <param name="exception">Exception to be logged.</param>
        /// <param name="window">The <see cref="Window" /> that will own the error dialog.</param>
        public abstract void LogAndReportException(Exception exception, Window window);
    }
}