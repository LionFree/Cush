using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentSienna : ResourceExtension
    {
        public AccentSienna() : base("Sienna",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentSienna.xaml")
        {
        }
    }
}