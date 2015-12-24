using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentMagenta : ResourceExtension
    {
        public AccentMagenta() : base("Magenta",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentMagenta.xaml")
        {
        }
    }
}