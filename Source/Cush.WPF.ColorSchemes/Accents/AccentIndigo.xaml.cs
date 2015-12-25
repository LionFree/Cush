using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentIndigo : ResourceExtension
    {
        public AccentIndigo() : base("Indigo", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentIndigo.xaml")
        {
        }
    }
}