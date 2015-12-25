using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentViolet : ResourceExtension
    {
        public AccentViolet() : base("Violet", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentViolet.xaml")
        {
        }
    }
}