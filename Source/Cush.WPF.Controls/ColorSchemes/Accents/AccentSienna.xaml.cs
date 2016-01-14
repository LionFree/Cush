using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentSienna : ResourceExtension
    {
        public AccentSienna() : base("Sienna", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentSienna.xaml")
        {
        }
    }
}