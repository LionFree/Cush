using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cush.Composition;
using Cush.WPF.Interfaces;

namespace Cush.TestHarness.WPF.Model
{
    class ColorSchemeContainer : ImportContainer, IColorSchemeContainer
    {
        public void OnImportsSatisfied()
        {
            throw new NotImplementedException();
        }

        [ImportMany("ColorScheme", typeof(IColorScheme), AllowRecomposition = false)]
        public List<IColorScheme> ColorSchemes { get; set; }
        public List<IResourceExtension> Accents { get; set; }
        public List<IResourceExtension> Themes { get; set; }
    }
}
