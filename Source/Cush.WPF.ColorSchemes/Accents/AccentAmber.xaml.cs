using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentAmber : ResourceExtension
    {
        public AccentAmber() : base("Amber",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentAmber.xaml")
        {
        }
    }
}