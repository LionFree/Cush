using System.Diagnostics;
using System.Windows.Input;
using Cush.TestHarness.WPF.ViewModels.Interfaces;
using Cush.WPF;
using Cush.WPF.Controls;

namespace Cush.TestHarness.WPF.ViewModels
{
    public class StartPageViewModel : BindableBase, IStartPageViewModel
    {
        private ThreadSafeObservableCollection<MRUEntry> _files;

        public StartPageViewModel() : this(new ThreadSafeObservableCollection<MRUEntry>())
        {
        }

        public StartPageViewModel(ThreadSafeObservableCollection<MRUEntry> files)
        {
            _files = files;
        }

        public ThreadSafeObservableCollection<MRUEntry> Files
        {
            get { return _files; }
            set { SetProperty(ref _files, value); }
        }

        public ICommand OpenOtherCommand
        {
            get { return new RelayCommand("OpenOtherCommand", param => Trace.WriteLine("Open Other clicked.")); }
        }

        public ICommand OpenRecentCommand
        {
            get { return new RelayCommand("OpenRecentCommand", param => Trace.WriteLine("Recent File clicked.")); }
        }
    }
}