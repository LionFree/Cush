using System.Windows;

namespace Cush.TestHarness.WPF.Infrastructure
{
    /// <summary>
    ///     Interaction logic for EntryPoint.xaml
    /// </summary>
    public partial class EntryPoint
    {
        private void EntryPoint_OnStartup(object sender, StartupEventArgs e)
        {
            Engine.ComposeObjectGraph().Start(e.Args);
        }
    }
}