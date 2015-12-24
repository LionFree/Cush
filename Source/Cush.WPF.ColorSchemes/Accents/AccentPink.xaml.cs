using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentPink : ResourceExtension
    {
        public AccentPink() : base("Pink",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentPink.xaml")
        {
        }
    }
}