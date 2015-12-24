using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentOlive : ResourceExtension
    {
        public AccentOlive() : base("Olive",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentOlive.xaml")
        {
        }
    }
}