using System.Collections.ObjectModel;
using Cush.TestHarness.WPF.Model;
using Cush.TestHarness.WPF.ViewModels.Interfaces;

namespace Cush.TestHarness.WPF.ViewModels
{
    internal class ActivityPageViewModel : IActivityPageViewModel
    {
        private FileClerk<DataFile> _fileHandler;

        public ActivityPageViewModel(FileClerk<DataFile> clerk)
        {
            _fileHandler = clerk;
            //clerk.FileOpened += OnCollectionChanged;
            //clerk.FileCreated += OnCollectionChanged;
        }
    }
}