using System.Windows.Controls;
using System.Windows.Input;
using Cush.TestHarness.WPF.Views.Pages;

namespace Cush.TestHarness.WPF.ViewModels.Interfaces
{
    public interface IShellViewModel
    {
        ICommand BackButtonCommand { get; }
        ContentControl Content { get; set; }
    }
}