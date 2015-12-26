using System.Windows.Input;
using Cush.TestHarness.WPF.ViewModels.Interfaces;
using Cush.WPF.Controls;

namespace Cush.TestHarness.WPF.Views.Dialogs
{
    /// <summary>
    ///     Interaction logic for AboutDialog.xaml
    /// </summary>
    public partial class AboutDialog
    {
        public AboutDialog(IAboutViewModel vm, CushWindow owningWindow, DialogSettings settings)
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