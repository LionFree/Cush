using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentCyan : ResourceExtension
    {
        public AccentCyan() : base("Cyan",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentCyan.xaml")
        {
        }
    }
}