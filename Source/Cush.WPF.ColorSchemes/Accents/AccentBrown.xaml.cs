using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentBrown : ResourceExtension
    {
        public AccentBrown() : base("Brown",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentBrown.xaml")
        {
        }
    }
}