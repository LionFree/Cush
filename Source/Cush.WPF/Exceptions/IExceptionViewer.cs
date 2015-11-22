using System;

namespace Cush.WPF.Exceptions
{
    /// <summary>
    ///     Interaction logic for ExceptionViewer.xaml
    /// </summary>
    public interface IExceptionViewer
    {
        void ShowException(Exception ex);
    }
}