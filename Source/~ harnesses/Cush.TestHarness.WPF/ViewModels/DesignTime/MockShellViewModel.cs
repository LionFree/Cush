using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using System.Windows.Input;
using Cush.TestHarness.WPF.ViewModels.Interfaces;

namespace Cush.TestHarness.WPF.ViewModels.DesignTime
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class MockShellViewModel : IShellViewModel
    {
        public ICommand BackButtonCommand { get; }
        public ContentControl Content { get; set; }
    }
}