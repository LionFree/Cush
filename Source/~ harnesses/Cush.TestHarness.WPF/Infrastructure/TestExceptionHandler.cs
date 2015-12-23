using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Cush.Common.Exceptions;
using Cush.Common.Interaction;
using Cush.Common.Logging;
using Cush.WPF.Exceptions;

namespace Cush.TestHarness.WPF.Infrastructure
{
    internal class TestExceptionHandler : WPFExceptionHandler
    {
        public TestExceptionHandler(IDialogs<Window> dialogs, ILogger logger)
            : base(dialogs, logger)
        {
        }

        internal void AttachToApplication()
        {
            // Add the event handler for handling UI thread exceptions to the event.
            Application.Current.DispatcherUnhandledException += OnMainUIThreadException;
        }

        internal void RecoverFromException(Exception ex)
        {
            LogAndReportException(ex, Application.Current.MainWindow);
            SaveAppState();
        }

        internal void OnUnrecoverableException(Exception ex)
        {
            Logger.Fatal("Runtime terminating.");
        }

        internal void LogAndReportException(Exception exception, Window window)
        {
            LogException(exception);

            //show error
            ReportException(exception, window);
        }

        internal void LogException(Exception exception)
        {
            ThrowHelper.IfNullThenThrow(() => exception);

            var message =
                string.Format("{0}: {1}{2}", exception.GetType(), exception.Message,
                    null == exception.InnerException ? string.Empty : exception.InnerException.Message);

            Logger.Error(exception, message);
        }

        internal void ReportException(Exception exception, Window window)
        {
            var formattedString =
                string.Format("EXCEPTION TargetSite {0}: {1}{2}{3}", exception.TargetSite, exception.Message,
                    Environment.NewLine, exception.StackTrace);

            Dialogs.ShowError(window, formattedString);
        }

        internal void SaveAppState()
        {
        }

        protected override void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception) e.ExceptionObject;

            RecoverFromException(exception);
            if (!e.IsTerminating) return;

            OnUnrecoverableException(exception);
            Application.Current.Shutdown();
        }

        protected override void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
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

        protected override void OnMainUIThreadException(object sender, DispatcherUnhandledExceptionEventArgs e)
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
            Logger.Fatal("Shutdown started: " + shutdownStarted);

            // Mark exception as handled in order to keep the app alive
            e.Handled = true;
#endif
        }
    }
}