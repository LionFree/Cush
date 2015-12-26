using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using Cush.TestHarness.WPF.ViewModels.Interfaces;
using Cush.WPF;
using Cush.WPF.Controls;

namespace Cush.TestHarness.WPF.ViewModels.DesignTime
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class MockStartPageViewModel : IStartPageViewModel
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

        public ThreadSafeObservableCollection<MRUEntry> Files { get; set; }
        public ICommand OpenOtherCommand { get; }
        public ICommand OpenRecentCommand { get; }
    }
}