using System;
using System.Threading.Tasks;
using Cush.Common.Exceptions;
using Cush.Common.Interaction;
using Cush.Common.Logging;

namespace meh.Exceptions
{
    public abstract class ExceptionHandler<TUIExceptionEventArgs>
    {
        public static Type[] UnrecoverableExceptions = {
            typeof (SystemException),
            typeof (OutOfMemoryException),
            typeof (BadImageFormatException)
        };

        protected readonly BindingErrorListener Listener;

        protected ExceptionHandler(ILogger logger, IDialogs dialogs)
            : this(logger, dialogs, BindingErrorListener.GetInstance())
        {
        }

        
        protected ExceptionHandler(ILogger logger, IDialogs dialogs, BindingErrorListener listener)
        {
            Logger = logger;
            Listener = listener;
            Dialogs = dialogs;

            // Add the event handler for handling non-UI thread exceptions to the event. 
            // (On the main thread, this one will happen second.)
            
            // Catch all exceptions on all threads in the appdomain.
            // Instead of e.Handled, use e.SetObserved.
            // Very Unreliable!
            AppDomain.CurrentDomain.UnhandledException += OnNonUIThreadException;

            // Catch any exceptions in the AppDomain that are thrown by TaskScheduler
            TaskScheduler.UnobservedTaskException += OnUnobservedException;
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
        public ILogger Logger {
            //[TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"),
            // __DynamicallyInvokable] 
            get;

            //[TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"),
            // __DynamicallyInvokable] 
            set; }

        /// <summary>
        ///     Forces data binding errors to be shown as exceptions in a user dialog.
        ///     This is very useful when debugging, but is most likely inappropriate for Release code.
        ///     Consider surrounding calls to this code with the #if DEBUG...#endif preprocessor directives.
        /// </summary>
        public void PromoteDataBindingErrors()
        {
            Listener.Listen(Dialogs.ShowError);
        }

        #region Abstract Methods

        /// <summary>
        ///     Catch unhandled exceptions thrown on the main UI thread and allow
        ///     option for user to continue program.
        ///     The <see cref="M:OnNonUIThreadException" /> method will handle all other exceptions thrown by
        ///     any thread (i.e., from AppDomain.UnhandledException).
        /// </summary>
        public abstract void OnMainUIThreadException(object sender, TUIExceptionEventArgs e);

        /// <summary>
        ///     Catch unhandled exceptions not thrown by the main UI thread.
        ///     NOTE: Exceptions caught by this handler cannot be kept from terminating
        ///     the application - this event is specifically designed to allow the application
        ///     to log information about the exception before the system default handler
        ///     reports the exception to the user and terminates the application.
        ///     This method can ONLY log the event, clean up resources, and inform the user
        ///     about the crash.
        /// </summary>
        public abstract void OnNonUIThreadException(object sender, UnhandledExceptionEventArgs e);

        /// <summary>
        ///     Catch unhandled exceptions thrown by tasks.
        ///     Handled or unhandled exceptions caught by this method typically terminate the runtime.
        /// </summary>
        protected abstract void OnUnobservedException(object sender, UnobservedTaskExceptionEventArgs e);

        /// <summary>
        ///     Tries to write a given exception to the log.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        public abstract void LogException(Exception exception);
        
        /// <summary>
        ///     Perform an emergency save of application state,
        ///     in case we need it for a future restart
        ///     (i.e., if e.Handled=true doesn't save the app from crashing.)
        /// </summary>
        public abstract void SaveAppState();

        /// <summary>
        ///     Cleanup from recoverable exception here.
        /// </summary>
        public abstract void RecoverFromException(Exception ex);

        /// <summary>
        ///     Cleanup from unrecoverable exception:
        ///     log the error, do emergency cleanup, prepare to crash gracefully.
        /// </summary>
        public abstract void OnUnrecoverableException(Exception ex);

        #endregion
    }
}