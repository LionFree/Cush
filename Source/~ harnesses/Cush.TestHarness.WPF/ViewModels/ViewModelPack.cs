using Cush.TestHarness.WPF.ViewModels.Interfaces;

namespace Cush.TestHarness.WPF.ViewModels
{
    internal class ViewModelPack
    {
        internal IStartPageViewModel StartPageViewModel { get; set; }
        internal IActivityPageViewModel ActivityPageViewModel { get; set; }
    }
}