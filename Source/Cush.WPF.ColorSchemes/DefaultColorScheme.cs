using System.ComponentModel.Composition;
using Cush.WPF.ColorSchemes.Accents;
using Cush.WPF.ColorSchemes.Themes;

// ReSharper disable once CheckNamespace
namespace Cush.WPF.ColorSchemes
{
    [Export("Defaults", typeof (IColorSchemeExtension))]
    internal class DefaultColorScheme : ColorSchemeExtension
    {
        public DefaultColorScheme() : base("Default", new Neutral(), new BaseLight(), new AccentBlue())
        {
        }
    }
}