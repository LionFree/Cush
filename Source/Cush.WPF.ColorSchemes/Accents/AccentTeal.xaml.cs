using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentTeal : ResourceExtension
    {
        public AccentTeal() : base("Teal", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentTeal.xaml")
        {
        }
    }
}