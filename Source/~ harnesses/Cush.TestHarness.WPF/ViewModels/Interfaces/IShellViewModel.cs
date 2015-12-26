using System.Windows.Controls;
using System.Windows.Input;

namespace Cush.TestHarness.WPF.ViewModels.Interfaces
{
    public interface IShellViewModel
    {
        ICommand BackButtonCommand { get; }
        ContentControl Content { get; set; }
    }
}