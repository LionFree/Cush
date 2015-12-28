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
    public class StartPageViewModel : BindableBase, IStartPageViewModel
    {
        public delegate void OpenRecentFileEventHandler(object sender, FileEventArgs e);

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

        public ICommand OpenOtherCommand
        {
            get { return new RelayCommand(nameof(OpenOtherCommand), param => RaiseEvent(OpenOtherFile)); }
        }

        public ICommand OpenRecentCommand
        {
            get { return new RelayCommand(nameof(OpenRecentCommand), OnOpenRecentFile); }
        }

        public ICommand ImportFileCommand
        {
            get
            {
                return new RelayCommand(nameof(ImportFileCommand), param => Trace.WriteLine("Import File clicked."));
            }
        }

        public ICommand MergeFilesCommand
        {
            get
            {
                return new RelayCommand(nameof(MergeFilesCommand), param => Trace.WriteLine("Merge Files clicked."));
            }
        }

        public ICommand NewEmptyFileCommand
        {
            get
            {
                return new RelayCommand(nameof(NewEmptyFileCommand), param => Trace.WriteLine("New Empty File clicked."));
            }
        }

        internal event OpenRecentFileEventHandler OpenRecentFile;
        internal event EventHandler OpenOtherFile;

        private void OnOpenRecentFile(object e)
        {
            MRUEntry file = null;
            var args = e as SelectionChangedEventArgs;
            if (args != null && args.AddedItems.Count != 0)
                file = (MRUEntry)args.AddedItems[0];

            if (file == null) return;
            OpenRecentFile?.Invoke(this, new FileEventArgs { Fullpath = file.FullPath });
        }

        private void RaiseEvent(EventHandler handler, EventArgs args = null)
        {
            handler?.Invoke(this, args ?? new EventArgs());
        }
    }
}