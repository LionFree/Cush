using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using System.Windows.Input;
using Cush.Common;
using Cush.Common.FileHandling;
using Cush.TestHarness.WPF.ViewModels.Interfaces;
using Cush.WPF;

namespace Cush.TestHarness.WPF.ViewModels
{
    public delegate void OpenRecentFileEventHandler(object sender, FileEventArgs e);

    public class StartPageViewModel : BindableBase, IStartPageViewModel
    {
        private ThreadSafeObservableCollection<MRUEntry> _files;

        public StartPageViewModel() : this(new ThreadSafeObservableCollection<MRUEntry>())
        {
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        internal StartPageViewModel(ThreadSafeObservableCollection<MRUEntry> files)
        {
            _files = files;
        }

        public ThreadSafeObservableCollection<MRUEntry> Files
        {
            get { return _files; }
            set { SetProperty(ref _files, value); }
        }

        public ICommand OnOpenOtherFileRequested=> new RelayCommand(nameof(OpenOtherFileRequested), param => RaiseEvent(OpenOtherFileRequested));
        public ICommand OnOpenRecentFileRequested => new RelayCommand(nameof(OpenRecentFileRequested), OnOpenRecentFile);
        public ICommand OnNewFileRequested=> new RelayCommand(nameof(OnNewFileRequested), param => RaiseEvent(NewFileRequested));
        public ICommand OnImportFileRequested=> new RelayCommand(nameof(OnImportFileRequested), param => Trace.WriteLine("Import File clicked."));
        public ICommand OnMergeFilesRequested=> new RelayCommand(nameof(OnMergeFilesRequested), param => Trace.WriteLine("Merge Files clicked."));
        
        public event OpenRecentFileEventHandler OpenRecentFileRequested;
        public event EventHandler OpenOtherFileRequested;
        public event EventHandler NewFileRequested;

        private void OnOpenRecentFile(object e)
        {
            MRUEntry file = null;
            var args = e as SelectionChangedEventArgs;
            if (args != null && args.AddedItems.Count != 0)
                file = (MRUEntry)args.AddedItems[0];

            if (file == null) return;
            OpenRecentFileRequested?.Invoke(this, new FileEventArgs { Fullpath = file.FullPath });
        }

        private void RaiseEvent(EventHandler handler, EventArgs args = null)
        {
            handler?.Invoke(this, args ?? new EventArgs());
        }
    }
}