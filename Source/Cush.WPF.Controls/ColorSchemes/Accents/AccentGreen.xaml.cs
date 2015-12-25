using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentGreen : ResourceExtension
    {
        public AccentGreen() : base("Green", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentGreen.xaml")
        {
        }
    }
}