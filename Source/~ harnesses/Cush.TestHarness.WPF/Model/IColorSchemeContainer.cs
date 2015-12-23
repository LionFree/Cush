using System.Collections.Generic;
using System.ComponentModel.Composition;
using Cush.WPF.Interfaces;

namespace Cush.TestHarness.WPF.Model
{
    public interface IColorSchemeContainer : IPartImportsSatisfiedNotification
    {
        List<IColorScheme> ColorSchemes { get; set; }
        List<IResourceExtension> Accents { get; set; }
        List<IResourceExtension> Themes { get; set; }
    }
}