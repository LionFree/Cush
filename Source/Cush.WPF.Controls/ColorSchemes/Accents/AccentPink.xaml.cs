using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentPink : ResourceExtension
    {
        public AccentPink() : base("Pink", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentPink.xaml")
        {
        }
    }
}