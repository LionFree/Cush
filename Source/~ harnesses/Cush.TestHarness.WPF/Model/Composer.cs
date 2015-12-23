using System.Collections.Generic;
using Cush.Common.Logging;
using Cush.Composition;
using Cush.WPF.ColorSchemes;
using Cush.WPF.Interfaces;

namespace Cush.TestHarness.WPF.Model
{
    public class Composer
    {
        private readonly ILogger _logger;
        private readonly ColorSchemeExtensionContainer _colorSchemes;

        internal Composer(ILogger logger)
            : this(
                logger, new ColorSchemeExtensionContainer())
        {
        }

        // Add extra containers! :)
        internal Composer(ILogger logger, ColorSchemeExtensionContainer colorSchemes)
        {
            _logger = logger;
            _colorSchemes = colorSchemes;
        }

        internal IColorScheme DefaultScheme
        {
            get { return _colorSchemes.DefaultScheme; }
        }

        internal List<IKeyedResourceContainer> Themes
        {
            get { return _colorSchemes.Themes; }
        }

        internal List<IKeyedResourceContainer> Accents
        {
            get { return _colorSchemes.Accents; }
        }

        internal IKeyedResourceContainer BaseTheme
        {
            get { return _colorSchemes.BaseTheme; }
        }

        internal void ComposeImports(ImportCollector collector)
        {
            _logger.Trace("Composing imported parts.");
            _colorSchemes.ComposeImports(collector);
            _logger.Trace("======================================================================");
        }
    }
}