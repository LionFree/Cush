// -----------------------------------------------------------------------
//      Copyright (c) 2014 Curtis Kaler. All rights reserved.
// -----------------------------------------------------------------------

using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Defaults
{
    [Export("Themes", typeof(IResourceExtension))]
    [Export("BaseTheme", typeof(IResourceExtension))]
    public class BaseLight : ResourceExtension
    {
        public BaseLight() : base(
            "Light",
            "pack://application:,,,/Cush.WPF;component/ColorSchemes/Defaults/BaseLight.xaml"
            )
        {
        }
    }
}