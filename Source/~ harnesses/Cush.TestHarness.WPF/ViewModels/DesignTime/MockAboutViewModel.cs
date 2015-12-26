using System.Diagnostics.CodeAnalysis;
using Cush.TestHarness.WPF.ViewModels.Interfaces;

namespace Cush.TestHarness.WPF.ViewModels.DesignTime
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class MockAboutViewModel : IAboutViewModel
    {
        public string CopyrightString => "Copyright String!";
    }
}