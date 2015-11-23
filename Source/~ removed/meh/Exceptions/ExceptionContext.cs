using System;
using System.Collections.Generic;
using Cush.Common.Interaction;
using Cush.Common.Logging;

namespace Cush.Common
{
    public class ExceptionContext
    {
        private static readonly string GenericErrorMessage =
            "We are really sorry, but something unexpected happened. " +
            "Please refresh the page and try again. " +
            "If the problem persists, please report it to your administrator.";

        private readonly IDialogs _dialogs;
        private readonly ILogger _logger;

        public ExceptionContext(ILogger logger, IDialogs dialogs)
        {
            _logger = logger;
            _dialogs = dialogs;
        }

        public Exception Exception { get; set; }
        public bool ExceptionHandled { get; set; }

        public void OnException(ExceptionContext filterContext)
        {
            _logger.Error(filterContext.Exception);

            _dialogs.ShowError(filterContext, GenericErrorMessage);

            filterContext.ExceptionHandled = true;
        }
    }

    public static class ExceptionExtensions
    {
        public static IEnumerable<Exception> AndInnerExceptions(this Exception exception)
        {
            while (exception != null)
            {
                yield return exception;
                exception = exception.InnerException;
            }
        }
    }
}