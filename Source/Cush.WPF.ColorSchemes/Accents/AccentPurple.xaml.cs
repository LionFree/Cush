using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentPurple : ResourceExtension
    {
        public AccentPurple() : base("Purple",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentPurple.xaml")
        {
        }
    }
}