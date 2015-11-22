using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Defaults
{
    [Export("Defaults", typeof (IColorSchemeExtension))]
    internal class DefaultColorScheme : ColorSchemeExtension
    {
        public DefaultColorScheme() : base("Default", new AccentBlue(), new BaseLight())
        {
        }
    }
}