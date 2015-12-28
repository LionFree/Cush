using Cush.TestHarness.WPF.ViewModels.Interfaces;

namespace Cush.TestHarness.WPF.Views.Pages
{
    /// <summary>
    ///     Interaction logic for ActivityView.xaml
    /// </summary>
    public partial class ActivityPage
    {
        //private ProgressView _progressView;

        public ActivityPage(IActivityPageViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}