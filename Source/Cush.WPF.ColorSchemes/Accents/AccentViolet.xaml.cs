using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentViolet : ResourceExtension
    {
        public AccentViolet() : base("Violet",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentViolet.xaml")
        {
        }
    }
}