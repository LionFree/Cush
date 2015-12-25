using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentAmber : ResourceExtension
    {
        public AccentAmber() : base("Amber", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentAmber.xaml")
        {
        }
    }
}