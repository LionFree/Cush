using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentOrange : ResourceExtension
    {
        public AccentOrange() : base("Orange",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentOrange.xaml")
        {
        }
    }
}