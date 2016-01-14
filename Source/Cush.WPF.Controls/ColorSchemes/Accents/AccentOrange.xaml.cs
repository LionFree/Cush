using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentOrange : ResourceExtension
    {
        public AccentOrange() : base("Orange", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentOrange.xaml")
        {
        }
    }
}