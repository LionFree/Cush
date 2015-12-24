using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentMauve : ResourceExtension
    {
        public AccentMauve() : base("Mauve",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentMauve.xaml")
        {
        }
    }
}