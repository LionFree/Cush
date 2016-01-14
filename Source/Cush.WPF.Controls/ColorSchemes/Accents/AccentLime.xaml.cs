using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentLime : ResourceExtension
    {
        public AccentLime() : base("Lime", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentLime.xaml")
        {
        }
    }
}