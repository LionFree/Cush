using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentCobalt : ResourceExtension
    {
        public AccentCobalt() : base("Cobalt",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentCobalt.xaml")
        {
        }
    }
}