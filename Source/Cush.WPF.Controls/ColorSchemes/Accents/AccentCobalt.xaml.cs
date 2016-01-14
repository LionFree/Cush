using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentCobalt : ResourceExtension
    {
        public AccentCobalt() : base("Cobalt", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentCobalt.xaml")
        {
        }
    }
}