using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentEmerald : ResourceExtension
    {
        public AccentEmerald() : base("Emerald",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Accents/AccentEmerald.xaml")
        {
        }
    }
}