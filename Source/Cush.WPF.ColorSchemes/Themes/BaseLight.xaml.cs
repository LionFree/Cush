// -----------------------------------------------------------------------
//      Copyright (c) 2014 Curtis Kaler. All rights reserved.
// -----------------------------------------------------------------------

using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Themes
{
    [Export("Themes", typeof(IResourceExtension))]
    public class BaseLight : ResourceExtension
    {
        public BaseLight() : base(
            "Light",
            "pack://application:,,,/Cush.WPF.ColorSchemes;component/Themes/BaseLight.xaml"
            )
        {
        }
    }
}