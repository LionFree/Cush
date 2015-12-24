using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentTaupe : ResourceExtension
    {
        public AccentTaupe() : base("Taupe",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentTaupe.xaml")
        {
        }
    }
}