using System.Diagnostics;
using System.Windows.Input;
using Cush.WPF;
using Cush.WPF.Controls;

namespace Cush.TestHarness.WPF.ViewModels
{
    public class MockStartPageViewModel : StartPageViewModel
    {
        public MockStartPageViewModel()
        {
            PopulateFakeFiles();
        }


        void PopulateFakeFiles()
        {
            Trace.WriteLine("InitializeDebug: Initial count: " + Files.Count);

            var ne0 = new MRUEntry
            {
                FullPath =
                    "D:\\Users\\Curt\\Documents\\tnt mining.txt",
                Pinned = true
            };
            Files.Add(ne0);

            var ne1 = new MRUEntry
            {
                FullPath =
                    "E:\\Curt\\Development\\My Projects\\Archive\\PAWS\\PAWS 2012 - No Reports\\PAWS\\SharedRoutines\\FileHandling\\Helpers\\Entry1.cs",
                Pinned = false
            };
            Files.Add(ne1);

            Trace.WriteLine("Count after adding item: " + Files.Count);
        }
    }

    public class StartPageViewModel : BindableBase
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