using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Cush.Common.Logging;

namespace Cush.Common.Exceptions
{
    public abstract class ExceptionHandler
    {
        protected ILogger Logger;

        protected ExceptionHandler() : this(NullLogger.GetInstance)
        {
        }

        protected ExceptionHandler(ILogger logger)
        {
            Logger = logger;

            // Add the event handler for handling non-UI thread exceptions to the event. 
            // (On the main thread, this one will happen second.)
            AppDomain.CurrentDomain.UnhandledException += NonUIThreadExceptionHandler;


            // Catch any exceptions in the AppDomain that are thrown by TaskScheduler
            TaskScheduler.UnobservedTaskException += UnobservedExceptionHandler;
        }

        public abstract void HookMainUIThread();

        /// <summary>
        ///     Catch unhandled exceptions thrown by tasks.
        ///     Handled or unhandled exceptions caught by this method typically terminate the runtime.
        /// </summary>
        protected virtual void UnobservedExceptionHandler(object sender, UnobservedTaskExceptionEventArgs e)
        {
            // get the aggregate exception and process the contents
            e.Exception.Handle(ex =>
            {
                // write the type of the exception to the console
                Logger.Error(ex.InnerException, "TASK EXCEPTION");
                return true;
            });

            // mark the exception as being handled
            e.SetObserved();
        }

        /// <summary>
        ///     Catch unhandled exceptions not thrown by the main UI thread.
        ///     NOTE: Exceptions caught by this handler cannot be kept from terminating
        ///     the application - this event is specifically designed to allow the application
        ///     to log information about the exception before the system default handler
        ///     reports the exception to the user and terminates the application.
        ///     This method can only log the event, clean up resources, and inform the user
        ///     about the crash.
        /// </summary>
        protected virtual void NonUIThreadExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Error((Exception) e.ExceptionObject, "UNHANDLED EXCEPTION");
            if (e.IsTerminating) Logger.Error("Runtime terminating.");
        }
    }
}