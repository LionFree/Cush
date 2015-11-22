using Cush.MVVM;

namespace Cush.WPF.Exceptions
{
    public interface IExceptionViewerViewModel : IViewModel
    {
        string ViewTitle { get; }
    }
}