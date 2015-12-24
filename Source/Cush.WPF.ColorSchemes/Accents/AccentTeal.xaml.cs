using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentTeal : ResourceExtension
    {
        public AccentTeal() : base("Teal",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentTeal.xaml")
        {
        }
    }
}