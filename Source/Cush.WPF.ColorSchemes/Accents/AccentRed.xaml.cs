using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentRed : ResourceExtension
    {
        public AccentRed() : base("Red",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentRed.xaml")
        {
        }
    }
}