using Cush.Common;

namespace Cush.WPF.Exceptions
{
    public class ExceptionViewerViewModel : BindableBase, IExceptionViewerViewModel
    {
        private readonly string _applicationName;

        internal ExceptionViewerViewModel(string applicationName)
        {
            _applicationName = applicationName;
        }

        public string ViewTitle
        {
            get { return _applicationName; }
        }
    }

    
}