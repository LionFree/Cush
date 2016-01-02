using Cush.TestHarness.WPF.ViewModels.Interfaces;

namespace Cush.TestHarness.WPF.ViewModels
{
    internal class ViewModelProvider
    {
        internal IStartPageViewModel StartPageViewModel { get; set; }
        internal IActivityPageViewModel ActivityPageViewModel { get; set; }
    }
}