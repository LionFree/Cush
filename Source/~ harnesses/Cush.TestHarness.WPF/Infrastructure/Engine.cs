using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Windows;
using Cush.Common.Configuration;
using Cush.Common.FileHandling;
using Cush.Common.Logging;
using Cush.TestHarness.WPF.Model;
using Cush.TestHarness.WPF.ViewModels;
using Cush.TestHarness.WPF.Views;
using Cush.TestHarness.WPF.Views.Dialogs;
using Cush.TestHarness.WPF.Views.Pages;
using Cush.WPF;
using Cush.WPF.ColorSchemes;
using Cush.WPF.Controls;

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
            TestThing(logger);

            ColorSchemeManager.ComposeColorSchemeExtensions(logger);

            var mruEntries = PopulateDebugEntries();
            var mruSettings = new MRUUserSettingsHandler(logger);
            
            var fileClerk = new FileClerk<DataFile>(logger,
                new FileHandler<DataFile>(logger)
                {
                    Filter = "Test Data Files (*.dat)|*.dat|All Files (*.*)|*.*",
                    DefaultExt = "dat",
                    DefaultFileName = string.Empty
                }
                );

            
            var viewModels = new ViewModelProvider
            {
                ActivityPageViewModel = new ActivityPageViewModel(fileClerk),
                StartPageViewModel = new StartPageViewModel(ref mruEntries),
            };

            var pages = new PageProvider
            {
                ActivityPage = new ActivityPage(viewModels.ActivityPageViewModel),
                StartPage = new StartPage(viewModels.StartPageViewModel)
            };

            var shellView = new ShellView(new ShellViewModel(logger, fileClerk, ref mruSettings,
                pages, viewModels));

            var dialogs = new DialogPack { 
                AboutDialog = new AboutDialog(new AboutViewModel(), shellView, null),
                SettingsDialog=new SettingsDialog(new SettingsViewModel(), shellView, null),
                ProgressDialog = new ProgressDialog(shellView, ProgressDialogSettings.Cancellable),
                };

            shellView.SetDialogs(dialogs);

            return new Implementation(shellView, logger);
        }

        private static void TestThing(ILogger logger)
        {
            var mruEntries = PopulateDebugEntries();

            var mruHandler = new MRUUserSettingsHandler(logger);
            var existing = mruHandler.Read();
            Trace.WriteLine($"Existing count (before save): {existing.Count}");

            mruHandler.Save(mruEntries);
            existing = mruHandler.Read();
            Trace.WriteLine($"Existing count (after save): {existing.Count}");

            //mruHandler.Clear();
            //existing = mruHandler.Read();
            //Trace.WriteLine($"Existing count (after save and clear): {existing.Count}");

            mruHandler.Remove(mruEntries[3]);
            mruHandler.Save();
            existing = mruHandler.Read();
            Trace.WriteLine($"Existing count (after removing one): {existing.Count}");



            Environment.Exit(0);

        }

        private static void TestWithNoParameters()
        {
            Trace.WriteLine(nameof(TestWithNoParameters));
        }

        private static ObservableCollection<MRUEntry> PopulateDebugEntries()
        {
            return new ObservableCollection<MRUEntry>
            {
                new MRUEntry {FullPath = "1+" + Path.GetRandomFileName(), Pinned = true},
                new MRUEntry {FullPath = "2+" + Path.GetRandomFileName(), Pinned = true},
                new MRUEntry {FullPath = "3+" + Path.GetRandomFileName(), Pinned = true},
                new MRUEntry {FullPath = "4+" + Path.GetRandomFileName(), Pinned = false},
                new MRUEntry {FullPath = "5+" + Path.GetRandomFileName(), Pinned = false},
                new MRUEntry {FullPath = "6+" + Path.GetRandomFileName(), Pinned = false}
            };
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