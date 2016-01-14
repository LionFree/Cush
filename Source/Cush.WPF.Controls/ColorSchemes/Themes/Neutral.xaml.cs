// -----------------------------------------------------------------------
//      Copyright (c) 2014 Curtis Kaler. All rights reserved.
// -----------------------------------------------------------------------

using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace
namespace Cush.WPF.ColorSchemes.Themes
{
    
    [Export("BaseTheme", typeof(IResourceExtension))]
    public class Neutral : ResourceExtension
    {
        public Neutral() : base("Base Theme", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Themes/Neutral.xaml")
        {
        }
    }
}