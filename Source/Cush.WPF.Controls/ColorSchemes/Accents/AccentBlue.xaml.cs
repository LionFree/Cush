using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentBlue : ResourceExtension
    {
        public AccentBlue() : base("Blue", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentBlue.xaml")
        {
        }
    }
}