using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentCrimson : ResourceExtension
    {
        public AccentCrimson() : base("Crimson", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentCrimson.xaml")
        {
        }
    }
}