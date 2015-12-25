using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentMagenta : ResourceExtension
    {
        public AccentMagenta() : base("Magenta", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentMagenta.xaml")
        {
        }
    }
}