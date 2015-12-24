using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentBlue : ResourceExtension
    {
        public AccentBlue() : base("Blue",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentBlue.xaml")
        {
        }
    }
}