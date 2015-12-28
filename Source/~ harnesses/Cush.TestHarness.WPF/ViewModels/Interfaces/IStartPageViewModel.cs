using System.Windows.Input;
using Cush.Common;
using Cush.Common.FileHandling;

namespace Cush.TestHarness.WPF.ViewModels.Interfaces
{
    public interface IStartPageViewModel
    {
        ThreadSafeObservableCollection<MRUEntry> Files { get; set; }
        ICommand OpenOtherCommand { get; }
        ICommand OpenRecentCommand { get; }
    }
}