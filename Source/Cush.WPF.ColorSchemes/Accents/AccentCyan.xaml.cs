using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof (IResourceExtension))]
    public class AccentCyan : ResourceExtension
    {
        public AccentCyan() : base("Cyan", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentCyan.xaml")
        {
        }
    }
}