using Cush.MVVM;
using Cush.WPF;

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
    }

    internal class MainViewModel : BindableBase, IViewModel
    {
        private string _testString;

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
    }
}