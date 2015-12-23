using System;
using System.Windows;
using Cush.Windows.SingleInstance;

namespace Cush.TestHarness.WPF.Infrastructure
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public static class SingleInstanceEntryPoint
    {
        [STAThread]
        private static void Main(string[] args)
        {
            WPFApplication.ComposeObjectGraph().StartSingleInstance(args);

            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}