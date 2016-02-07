using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
        private ObservableCollection<MRUEntry> _files;

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        internal StartPageViewModel(ref ObservableCollection<MRUEntry> files)
        {
            _files = files;
        }

        public ObservableCollection<MRUEntry> Files
        {
            get { return _files; }
            set { SetProperty(ref _files, value); }
        }

        public ICommand OnOpenOtherFileRequested => new RelayCommand(() => RaiseEvent(OpenOtherFileRequested));
        public ICommand OnOpenRecentFileRequested => new RelayCommand<object>(OnOpenRecentFile);
        public ICommand OnNewFileRequested=> new RelayCommand(() => RaiseEvent(NewFileRequested));
        public ICommand OnImportFileRequested => new RelayCommand(() => Trace.WriteLine("Import File clicked."));
        public ICommand OnMergeFilesRequested => new RelayCommand(() => Trace.WriteLine("Merge Files clicked."));

        public ICommand OnOpenACopyRequested => new RelayCommand<object>(OpenACopy);

        private void OpenACopy(object obj)
        {
            var file = obj as MRUEntry;
            if (file == null) return;
            Trace.WriteLine($"Opening a copy of {file.FullPath}.");
        }

       

        public event OpenRecentFileEventHandler OpenRecentFileRequested;
        public event EventHandler OpenOtherFileRequested;
        public event EventHandler NewFileRequested;

        private void OnOpenRecentFile(object e)
        {
            var file = e as MRUEntry;
            if (file == null) return;
            OpenRecentFileRequested?.Invoke(this, new FileEventArgs { Fullpath = file.FullPath });
        }
    }
}