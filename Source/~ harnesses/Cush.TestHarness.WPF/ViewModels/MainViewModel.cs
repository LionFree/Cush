using System.Collections.ObjectModel;
using System.Diagnostics;
using Cush.MVVM;
using Cush.WPF;
using Cush.WPF.Interfaces;

namespace Cush.TestHarness.WPF.ViewModels
{
    public interface IMainViewModel : IViewModel
    {
        string TestString { get; set; }
    }

    internal class MockMainViewModel : MainViewModel
    {
        public MockMainViewModel()
        {
            TestString = "Mock Main View Model";
        }

        new public ObservableCollection<IMRUEntry> MRUEntries
        {
            get { return new ObservableCollection<IMRUEntry>(); }
        }

    }

    internal class MainViewModel : BindableBase, IViewModel
    {
        private string _testString;
        private RelayCommand _openOther;

        public MainViewModel()
        {
            TestString = "Real View Model";
        }

        public string TestString
        {
            get { return _testString; }
            set
            {
                if (_testString == value) return;
                _testString = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand OpenOtherCommand
        {
            get { return _openOther ?? (_openOther = new RelayCommand("OpenOtherCommand", param => OpenOther())); }
        }

        private void OpenOther()
        {
            Trace.WriteLine("Fuck Yeah.");
        }

        public RelayCommand OpenRecentCommand
        {
            get { return null; }
        }

        public RelayCommand ImportFileCommand
        {
            get { return null; }
        }

        public RelayCommand MergeFilesCommand
        {
            get { return null; }
        }

        public RelayCommand NewEmptyFileCommand
        {
            get { return null; }
        }

        public ObservableCollection<IMRUEntry> MRUEntries
        {
            get { return new ObservableCollection<IMRUEntry>(); }
        }
    }
}