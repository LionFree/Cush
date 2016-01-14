﻿using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentSteel : ResourceExtension
    {
        public AccentSteel() : base("Steel", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentSteel.xaml")
        {
        }
    }
}