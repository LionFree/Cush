using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentPurple : ResourceExtension
    {
        public AccentPurple() : base("Purple", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentPurple.xaml")
        {
        }
    }
}