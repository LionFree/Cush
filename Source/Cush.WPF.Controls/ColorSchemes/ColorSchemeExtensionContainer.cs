using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using Cush.Composition;
using Cush.WPF.Interfaces;
// ReSharper disable once CheckNamespace
namespace Cush.WPF.ColorSchemes
{
    public class ColorSchemeExtensionContainer : ImportContainer, IColorSchemeExtensionContainer
    {
        [Import("BaseTheme", typeof (IResourceExtension), AllowRecomposition = false)]
        public IKeyedResourceContainer BaseTheme { get; set; }

        [ImportMany("Themes", typeof (IResourceExtension), AllowRecomposition = true)]
        public List<IKeyedResourceContainer> Themes { get; set; }

        [ImportMany("Accents", typeof (IResourceExtension), AllowRecomposition = true)]
        public List<IKeyedResourceContainer> Accents { get; set; }

        [Import("Defaults", typeof (IColorSchemeExtension), AllowRecomposition = true)]
        public IColorScheme DefaultScheme { get; set; }

        public void OnImportsSatisfied()
        {
            Trace.WriteLine("    ColorSchemeExtensionContainer Satisfied.");
            Themes = GetUniqueExtensions(Themes);
            Accents = GetUniqueExtensions(Accents);
            Trace.WriteLine("    Themes and Accents are Unique.");
        }

        private List<IKeyedResourceContainer> GetUniqueExtensions(List<IKeyedResourceContainer> list)
        {
            var distinctItemGuids = list.Select(x => x.Guid).Distinct().ToList();
            return distinctItemGuids.Count == list.Count
                ? list
                : distinctItemGuids.Select(guid => list.FirstOrDefault(x => x.Guid == guid)).ToList();
        }
    }
}