using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentTaupe : ResourceExtension
    {
        public AccentTaupe() : base("Taupe", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentTaupe.xaml")
        {
        }
    }
}