using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Defaults
{
    /// <summary>
    ///     Interaction logic for AccentBlue.xaml
    /// </summary>
    [Export("Accents", typeof (IResourceExtension))]
    public class AccentBlue : ResourceExtension
    {
        public AccentBlue() : base("Blue",
            "pack://application:,,,/Cush.WPF;component/ColorSchemes/Defaults/AccentBlue.xaml")
        {
        }
    }
}