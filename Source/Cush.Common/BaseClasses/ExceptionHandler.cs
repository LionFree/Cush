using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Cush.Common.Interaction;
using Cush.Common.Logging;

namespace Cush.Common
{
    /// <summary>
    ///     Provides the abstract base class for the handlers who monitor
    ///     unhandled exceptions.
    /// </summary>
    public abstract class ExceptionHandler<TUIExceptionEventArgs>
    {
        /// <summary>
        ///     A listener that monitors trace and debug output.
        /// </summary>
        protected readonly TraceListener Listener;

        /// <summary>
        ///     The types of exceptions from which there is no recovery.
        /// </summary>
        protected Type[] UnrecoverableExceptions =
        {
            typeof (SystemException),
            typeof (OutOfMemoryException),
            typeof (BadImageFormatException)
        };

        protected ExceptionHandler(TraceListener listener, IDialogs dialogs, ILogger logger)
        {
            Listener = listener;
            Dialogs = dialogs;
            Logger = logger;

            // Catch all exceptions on all threads in the appdomain.
            // Instead of e.Handled, use e.SetObserved.
            // Very Unreliable!
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            // Catch any exceptions in the AppDomain that are thrown by TaskScheduler
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }

        /// <summary>
        ///     The dialogs to use for user interaction.
        /// </summary>
        protected IDialogs Dialogs {
            //[TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"),
            // __DynamicallyInvokable] 
            get;

            //[TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"),
            // __DynamicallyInvokable] 
            set; }

        /// <summary>
        ///     The logger to use when logging messages and exceptions.
        /// </summary>
        protected ILogger Logger {
            //[TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"),
            // __DynamicallyInvokable] 
            get;

            //[TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"),
            // __DynamicallyInvokable] 
            set; }

        /// <summary>
        ///     Catch unhandled exceptions not thrown by the main UI thread.
        ///     NOTE: Exceptions caught by this handler cannot be kept from terminating
        ///     the application - this event is specifically designed to allow the application
        ///     to log information about the exception before the system default handler
        ///     reports the exception to the user and terminates the application.
        ///     This method can ONLY log the event, clean up resources, and inform the user
        ///     about the crash.
        /// </summary>
        protected abstract void OnUnhandledException(object sender, UnhandledExceptionEventArgs e);

        /// <summary>
        ///     Catch unhandled exceptions thrown by tasks.
        ///     Handled or unhandled exceptions caught by this method typically terminate the runtime.
        /// </summary>
        protected abstract void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e);

        /// <summary>
        ///     Catch unhandled exceptions thrown on the main UI thread and allow
        ///     option for user to continue program.
        ///     The <see cref="M:OnUnhandledException" /> method will handle all other exceptions thrown by
        ///     any thread (i.e., from AppDomain.UnhandledException).
        /// </summary>
        protected abstract void OnMainUIThreadException(object sender, TUIExceptionEventArgs e);
    }
}