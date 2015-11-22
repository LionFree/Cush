using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cush.Common.Exceptions;
using Cush.Common.Interaction;
using Cush.Common.Logging;

namespace Cush.TestHarness.WinForms
{
    public sealed class WinFormsExceptionHandlerImpl : WinFormsExceptionHandler
    {
        public WinFormsExceptionHandlerImpl(ILogger logger, IDialogs dialogs)
            : base(logger, dialogs)
        {
        }

        public override void AttachExceptionHandler(AppDomain domain, Application application)
        {
            ThrowHelper.IfNullThenThrow(() => domain);

            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += OnMainUIThreadException;

            // Set the unhandled exception mode to force all Windows Forms errors 
            // to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            // (On the main thread, this one will happen second.)
            domain.UnhandledException += OnNonUIThreadException;

            // Catch any exceptions in the AppDomain that are thrown by TaskScheduler
            TaskScheduler.UnobservedTaskException += OnUnobservedException;
        }

        protected override void OnMainUIThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (Debugger.IsAttached) return;

            LogAndReportException(e.Exception, null);

            //mark exception as handled in order to keep the app alive
            //e.Handled = true;
        }

        protected override void OnNonUIThreadException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Error((Exception) e.ExceptionObject, "UNHANDLED EXCEPTION");
            if (e.IsTerminating) Logger.Error("Runtime terminating.");
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

            Logger.Error(exception, "{0}: {1}{2}", exception.GetType(), exception.Message,
                (exception.InnerException == null
                    ? string.Empty
                    : exception.InnerException.Message));
        }

        public override void ReportException(Exception exception, Form owner)
        {
            //show error
            Dialogs.ShowError(owner,
                string.Format("EXCEPTION TargetSite {0}: {1}{2}{3}", exception.TargetSite,
                    exception.Message,
                    Environment.NewLine,
                    exception.StackTrace));
        }

        public override void LogAndReportException(Exception exception, Form window)
        {
            LogException(exception);
            ReportException(exception, null);
        }
    }
}