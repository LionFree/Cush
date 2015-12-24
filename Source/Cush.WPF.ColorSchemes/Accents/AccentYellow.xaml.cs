using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentYellow : ResourceExtension
    {
        public AccentYellow() : base("Yellow",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentYellow.xaml")
        {
        }
    }
}