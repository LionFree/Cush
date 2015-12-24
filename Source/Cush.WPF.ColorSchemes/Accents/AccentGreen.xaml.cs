using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentGreen : ResourceExtension
    {
        public AccentGreen() : base("Green",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentGreen.xaml")
        {
        }
    }
}