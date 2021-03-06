﻿using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentBrown : ResourceExtension
    {
        public AccentBrown() : base("Brown", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentBrown.xaml")
        {
        }
    }
}