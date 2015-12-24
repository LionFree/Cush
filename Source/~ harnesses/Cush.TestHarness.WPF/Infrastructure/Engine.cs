using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Cush.Common.Logging;
using Cush.Composition;
using Cush.TestHarness.WPF.ViewModels;
using Cush.TestHarness.WPF.Views;
using Cush.TestHarness.WPF.Views.Dialogs;
using Cush.TestHarness.WPF.Views.Pages;
using Cush.WPF.ColorSchemes;

namespace Cush.TestHarness.WPF.Infrastructure
{
    internal abstract class Engine
    {
        public static Engine ComposeObjectGraph()
        {
            return ComposeObjectGraph(Loggers.Trace);
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        internal static Engine ComposeObjectGraph(ILogger logger)
        {
            var composer = ComposeExtensions(logger);
            ColorSchemeManager.PopulateSchemes(composer.Container);
            ColorSchemeManager.SetManagedTheme("Dark");

            var shellView = new ShellView(new ShellViewModel(new StartPage(new StartPageViewModel())));

            // dialogs get the resources from ColorSchemeManager.
            var dialogs = new DialogPack(
                new AboutDialog(new AboutViewModel(), shellView, null),
                new SettingsDialog(new SettingsViewModel(), shellView, null)
                );

            shellView.AttachDialogs(dialogs);

            return new Implementation(shellView, composer, logger);
        }

        private static Composer ComposeExtensions(ILogger logger)
        {
            var composer = new Composer(logger);
            using (var collector = new ImportCollector(logger))
            {
                collector.ImportParts();
                composer.ComposeImports(collector);
            }
            return composer;
        }

        internal abstract void Start(params string[] args);
        internal abstract void Shutdown();

        private class Implementation : Engine
        {
            private readonly Composer _composer;
            private readonly ILogger _logger;
            private readonly Window _view;

            public Implementation(Window view, Composer composer, ILogger logger)
            {
                _view = view;
                _composer = composer;
                _logger = logger;
            }

            internal override void Start(params string[] args)
            {
                try
                {
                    _view.Closed += (s, e) => Shutdown();
                    _view.Show();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                    // If it fails here, shut it down.
                    //ErrorHandler.HandleError("AppStartup", ex);

                    Shutdown();
                }
            }
            
            internal override void Shutdown()
            {
                Environment.Exit(0);
            }
        }
    }
}