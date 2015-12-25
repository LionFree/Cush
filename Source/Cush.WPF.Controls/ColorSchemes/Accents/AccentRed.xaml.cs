using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentRed : ResourceExtension
    {
        public AccentRed() : base("Red", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentRed.xaml")
        {
        }
    }
}