using System.Collections.Generic;
using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes
{
    public interface IColorSchemeExtensionContainer : IPartImportsSatisfiedNotification
    {
        [Import("BaseTheme", typeof (IResourceExtension), AllowRecomposition = false)]
        IKeyedResourceContainer BaseTheme { get; set; }

        List<IKeyedResourceContainer> Themes { get; set; }

        List<IKeyedResourceContainer> Accents { get; set; }

        IColorScheme DefaultScheme { get; set; }
    }
}