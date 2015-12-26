using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Cush.Common.Logging;
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
            ColorSchemeManager.ComposeColorSchemeExtensions(logger);

            var shellView = new ShellView(new ShellViewModel(new StartPage(new StartPageViewModel())));

            var dialogs = new DialogPack(
                new AboutDialog(new AboutViewModel(), shellView, null),
                new SettingsDialog(new SettingsViewModel(), shellView, null)
                );

            shellView.AttachDialogs(dialogs);

            return new Implementation(shellView, logger);
        }

        internal abstract void Start(params string[] args);
        internal abstract void Shutdown();

        private class Implementation : Engine
        {
            private readonly ILogger _logger;
            private readonly Window _view;

            public Implementation(Window view, ILogger logger)
            {
                _view = view;
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
                    _logger.Error(ex.Message);
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