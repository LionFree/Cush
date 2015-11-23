using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Cush.WPF.Interfaces
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