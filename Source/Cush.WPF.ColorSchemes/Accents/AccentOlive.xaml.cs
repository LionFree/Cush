using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentOlive : ResourceExtension
    {
        public AccentOlive() : base("Olive", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentOlive.xaml")
        {
        }
    }
}