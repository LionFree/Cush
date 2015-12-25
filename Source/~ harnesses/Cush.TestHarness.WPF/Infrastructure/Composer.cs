using System.Collections.Generic;
using System.Diagnostics;
using Cush.Common;
using Cush.Common.Logging;
using Cush.Composition;
using Cush.WPF.ColorSchemes;
using Cush.WPF.Interfaces;

namespace Cush.TestHarness.WPF.Infrastructure
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

        internal IColorSchemeExtensionContainer Container => _colorSchemes;

        internal IColorScheme DefaultScheme => _colorSchemes.DefaultScheme;

        internal List<IKeyedResourceContainer> Themes => _colorSchemes.Themes;

        internal List<IKeyedResourceContainer> Accents => _colorSchemes.Accents;

        internal IKeyedResourceContainer BaseTheme => _colorSchemes.BaseTheme;

        internal void ComposeImports(ImportCollector collector)
        {
            _logger.Trace("Composing imported parts.");
            _colorSchemes.ComposeImports(collector);
            Trace.WriteLine(Common.Strings.logDivider);
        }
    }
}