using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentLime : ResourceExtension
    {
        public AccentLime() : base("Lime",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentLime.xaml")
        {
        }
    }
}