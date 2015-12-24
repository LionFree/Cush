using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentIndigo : ResourceExtension
    {
        public AccentIndigo() : base("Indigo",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentIndigo.xaml")
        {
        }
    }
}