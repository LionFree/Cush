using System.Diagnostics;
using System.Windows;
using Cush.MVVM;
using Cush.TestHarness.WPF.ViewModels;

namespace Cush.TestHarness.WPF.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainView : IView
    {
        internal MainView(MainViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }

        private void MRUMenu_OnOpenOtherFileClicked(object sender, RoutedEventArgs e)
        {
            //Trace.WriteLine("MRUMenu.Files.Count: " + MRUMenu.Files.Count); 
        }
    }
}