using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentMauve : ResourceExtension
    {
        public AccentMauve() : base("Mauve", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentMauve.xaml")
        {
        }
    }
}