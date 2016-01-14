﻿using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes.Accents
{
    [Export("Accents", typeof(IResourceExtension))]
    public class AccentEmerald : ResourceExtension
    {
        public AccentEmerald() : base("Emerald", "pack://application:,,,/Cush.WPF.Controls;component/ColorSchemes/Accents/AccentEmerald.xaml")
        {
        }
    }
}