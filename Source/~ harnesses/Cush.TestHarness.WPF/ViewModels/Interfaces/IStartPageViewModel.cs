using System.Windows.Input;
using Cush.WPF;
using Cush.WPF.Controls;

namespace Cush.TestHarness.WPF.ViewModels.Interfaces
{
    public interface IStartPageViewModel
    {
        ThreadSafeObservableCollection<MRUEntry> Files { get; set; }
        ICommand OpenOtherCommand { get; }
        ICommand OpenRecentCommand { get; }
    }
}