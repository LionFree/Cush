using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentYellow : ResourceExtension
    {
        public AccentYellow() : base("Yellow", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentYellow.xaml")
        {
        }
    }
}