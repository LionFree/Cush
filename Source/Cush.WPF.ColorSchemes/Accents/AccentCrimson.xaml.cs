using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentCrimson : ResourceExtension
    {
        public AccentCrimson() : base("Crimson",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentCrimson.xaml")
        {
        }
    }
}