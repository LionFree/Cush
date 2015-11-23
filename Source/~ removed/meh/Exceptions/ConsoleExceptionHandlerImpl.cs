using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Cush.Common.Exceptions;
using Cush.Common.Interaction;
using Cush.Common.Logging;

namespace Cush.TestHarness.ConsoleApp
{
    public sealed class ConsoleExceptionHandlerImpl : ExceptionHandler
    {
        public ConsoleExceptionHandlerImpl(ILogger logger, IDialogs dialogs) : base(logger, dialogs)
        {
        }

        protected override void OnMainUIThreadException(object sender, EventArgs e)
        {
            Trace.WriteLine("MainUIThreadExceptionHandler");

            var args = (ThreadExceptionEventArgs) e;
            Logger.Error(args.Exception, "EXCEPTION");
        }

        protected override void OnNonUIThreadException(object sender, UnhandledExceptionEventArgs e)
        {
            Trace.WriteLine("NonUIThreadExceptionHandler");

            Logger.Error((Exception) e.ExceptionObject, "UNHANDLED EXCEPTION");
            if (e.IsTerminating) Logger.Error("Runtime terminating.");
        }

        protected override void OnUnobservedException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Trace.WriteLine("UnobservedExceptionHandler");

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

        public override void HandleException(Exception ex)
        {
            Trace.WriteLine("HandleException");
            Logger.Error(ex, "UNHANDLED EXCEPTION");
        }

        public override void LogException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override void ReportException(Exception exception, object window)
        {
            throw new NotImplementedException();
        }

        public override void LogAndReportException(Exception exception, object window)
        {
            throw new NotImplementedException();
        }
    }
}