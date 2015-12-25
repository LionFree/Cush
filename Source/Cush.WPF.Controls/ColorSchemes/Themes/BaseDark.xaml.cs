// -----------------------------------------------------------------------
//      Copyright (c) 2014 Curtis Kaler. All rights reserved.
// -----------------------------------------------------------------------

using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace
namespace Cush.WPF.ColorSchemes.Themes
{
    [Export("Themes", typeof(IResourceExtension))]
    public class BaseDark : ResourceExtension
    {
        public BaseDark() : base("Dark", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Themes/BaseDark.xaml")
        {
        }
    }
}