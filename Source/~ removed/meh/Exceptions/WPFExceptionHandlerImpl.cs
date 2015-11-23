using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Cush.Common.Exceptions;
using Cush.Common.Interaction;
using Cush.Common.Logging;

namespace Cush.TestHarness.WPF.Infrastructure
{
    public sealed class WPFExceptionHandlerImpl : WPFExceptionHandlerImpl
    {
        //private Application _application;

        internal WPFExceptionHandlerImpl(IDialogs dialogs, ILogger logger)
            : base(logger, dialogs)
        {
        }

        public override void AttachExceptionHandler(AppDomain domain, Application application)
        {
            ThrowHelper.IfNullThenThrow(() => domain);
            ThrowHelper.IfNullThenThrow(() => application);


            //_application = application;

            // Add the event handler for handling UI thread exceptions to the event.
            //Application.Current.DispatcherUnhandledException += OnMainUIThreadException;

            // Catch all exceptions on the UI thread.  
            // Takes priority over AppDomain.CurrentDomain.UnhandledException
            Application.Current.DispatcherUnhandledException += OnMainUIThreadException;

            //// Add the event handler for handling non-UI thread exceptions to the event. 
            //// (On the main thread, this one will happen second.)
            //domain.UnhandledException += OnNonUIThreadException;

            //// Catch any exceptions in the AppDomain that are thrown by TaskScheduler
            //TaskScheduler.UnobservedTaskException += OnUnobservedException;
        }

        public override void OnMainUIThreadException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
#if DEBUG
            e.Handled = false;
#else
            // If it's something like OutOfMemory, or whatnot, then crash.
            if (e.Exception == null || UnrecoverableExceptions.Contains(e.Exception.GetType()))
            {
                e.Handled = false;
                return;
            }


            //if (Debugger.IsAttached)
            //{
            //    e.Handled = false;
            //    return;
            //}

            if (!e.Dispatcher.CheckAccess())
            {
                // we are, in fact, running on a dispatcher thread, but better safe than sorry
                e.Dispatcher.Invoke(new Action(() =>
                    OnMainUIThreadException(sender, e)),
                    DispatcherPriority.Normal,
                    null);

                return;
            }

            RecoverFromException(e.Exception);

            var shutdownStarted = e.Dispatcher.HasShutdownStarted;
            Trace.WriteLine("Shutdown started: " + shutdownStarted);

            // Mark exception as handled in order to keep the app alive
            e.Handled = true;
#endif
        }

        public override void OnNonUIThreadException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception) e.ExceptionObject;

            RecoverFromException(exception);
            if (!e.IsTerminating) return;

            OnUnrecoverableException(exception);
            Application.Current.Shutdown();
        }

        protected override void OnUnobservedException(object sender, UnobservedTaskExceptionEventArgs e)
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

        public override void LogException(Exception exception)
        {
            ThrowHelper.IfNullThenThrow(() => exception);

            var message = string.Format("{0}: {1}{2}", exception.GetType(), exception.Message,
                (exception.InnerException == null
                    ? string.Empty
                    : exception.InnerException.Message));

            Logger.Error(exception, message);
        }

        public override void ReportException(Exception exception, Window window)
        {
            var formattedString = string.Format("EXCEPTION TargetSite {0}: {1}{2}{3}",
                exception.TargetSite,
                exception.Message,
                Environment.NewLine,
                exception.StackTrace);

            Dialogs.ShowError(window, formattedString);
        }

        public override void LogAndReportException(Exception exception, Window window)
        {
            LogException(exception);

            //show error
            ReportException(exception, window);
        }

        /// <summary>
        ///     Perform an emergency save of application state,
        ///     in case we need it for a future restart
        ///     (i.e., if e.Handled=true doesn't save the app from crashing.)
        /// </summary>
        public override void SaveAppState()
        {
        }

        /// <summary>
        ///     Cleanup from recoverable exception here.
        /// </summary>
        public override void RecoverFromException(Exception ex)
        {
            LogAndReportException(ex, Application.Current.MainWindow);
            SaveAppState();
        }

        /// <summary>
        ///     Cleanup from unrecoverable exception:
        ///     log the error, do emergency cleanup, prepare to crash gracefully.
        /// </summary>
        public override void OnUnrecoverableException(Exception ex)
        {
            Logger.Fatal("Runtime terminating.");
        }
    }
}