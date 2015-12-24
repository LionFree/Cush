using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentSteel : ResourceExtension
    {
        public AccentSteel() : base("Steel",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentSteel.xaml")
        {
        }
    }
}