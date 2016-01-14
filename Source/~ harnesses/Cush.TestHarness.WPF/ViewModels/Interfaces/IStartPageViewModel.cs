using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Cush.Common;
using Cush.Common.FileHandling;

namespace Cush.TestHarness.WPF.ViewModels.Interfaces
{
    public interface IStartPageViewModel
    {
        ObservableCollection<MRUEntry> Files { get; set; }
        ICommand OnOpenOtherFileRequested { get; }
        ICommand OnOpenRecentFileRequested { get; }
        ICommand OnNewFileRequested { get; }
        ICommand OnMergeFilesRequested { get; }
        ICommand OnImportFileRequested { get; }

        event OpenRecentFileEventHandler OpenRecentFileRequested;
        event EventHandler OpenOtherFileRequested;
        event EventHandler NewFileRequested;
    }
}