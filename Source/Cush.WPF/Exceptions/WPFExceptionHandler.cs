using System.Windows.Threading;
using Cush.Common.Exceptions;
using Cush.Common.Interaction;
using Cush.Common.Logging;

namespace Cush.WPF.Exceptions
{
    public abstract class WPFExceptionHandler : Common.ExceptionHandler<DispatcherUnhandledExceptionEventArgs>
    {
        protected WPFExceptionHandler(IDialogs dialogs, ILogger logger)
            : this(BindingErrorListener.GetInstance(), dialogs, logger)
        {
        }

        protected WPFExceptionHandler(BindingErrorListener listener, IDialogs dialogs, ILogger logger)
            : base(listener, dialogs, logger)
        {
            // Add the event handler for handling UI thread exceptions to the event.
            //System.Windows.Application.Current.DispatcherUnhandledException += OnMainUIThreadException;
        }
    }
}