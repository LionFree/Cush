using System.Windows.Input;
using Cush.TestHarness.WPF.ViewModels;
using Cush.WPF.Controls;

namespace Cush.TestHarness.WPF.Views.Dialogs
{
    /// <summary>
    ///     Interaction logic for AboutDialog.xaml
    /// </summary>
    public partial class AboutDialog
    {
        public AboutDialog(AboutViewModel vm, CushWindow owningWindow, DialogSettings settings)
            : base(owningWindow, settings)
        {
            DataContext = vm;
            InitializeComponent();
        }

        private void AboutView_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Close();
            }
        }
    }
}