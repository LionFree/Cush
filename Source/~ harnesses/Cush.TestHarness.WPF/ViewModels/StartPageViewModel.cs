using System.Diagnostics;
using System.Windows.Input;
using Cush.Common;
using Cush.TestHarness.WPF.ViewModels.Interfaces;
using Cush.WPF;
using Cush.WPF.Controls;

namespace Cush.TestHarness.WPF.ViewModels
{
    public class StartPageViewModel : BindableBase, IStartPageViewModel
    {
        private Cush.WPF.ThreadSafeObservableCollection<MRUEntry> _files;

        public StartPageViewModel() : this(new Cush.WPF.ThreadSafeObservableCollection<MRUEntry>())
        {
        }

        public StartPageViewModel(Cush.WPF.ThreadSafeObservableCollection<MRUEntry> files)
        {
            _files = files;
        }

        public Cush.WPF.ThreadSafeObservableCollection<MRUEntry> Files
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