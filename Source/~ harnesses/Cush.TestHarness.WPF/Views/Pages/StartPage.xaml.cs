using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Cush.Common;
using Cush.TestHarness.WPF.ViewModels.Interfaces;
using Cush.TestHarness.WPF.Views.Events;
using Cush.WPF.ColorSchemes;
using Cush.WPF.Controls;

namespace Cush.TestHarness.WPF.Views.Pages
{
    /// <summary>
    ///     Interaction logic for FileView.xaml
    /// </summary>
    public partial class StartPage : ISchemedElement
    {
        public delegate void StartViewEventHandler(object sender, FileViewEventArgs e);

        public StartPage(IStartPageViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
            ColorSchemeManager.Register(this);
        }

        public IColorScheme ColorScheme { get; set; }

        private void PinRecentFile(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var b = sender as Rectangle;
                DependencyObject item = b;
                while (item is ListBoxItem == false)
                {
                    if (item != null) item = VisualTreeHelper.GetParent(item);
                }
                var lbi = (ListBoxItem) item;
                var rfb = (MRUEntry) lbi.Content;

                // Toggle the pin.
                rfb.Pinned = !rfb.Pinned;

                // Create the event arguments to pass to the ShellWindow.
                var d = new FileViewEventArgs
                {
                    PageEvent = FileViewEvent.ToggleRecentFilePin,
                    Filename = rfb.FullPath,
                    Pinned = rfb.Pinned
                };

                // Raise an event to the ShellWindow to open a file and display the content page.
                OnFileViewButtonPressed(d);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception: " + ex.Message);
            }
            e.Handled = true;
        }

        public event StartViewEventHandler FileViewButtonPressed;

        protected virtual void OnFileViewButtonPressed(FileViewEventArgs e)
        {
            // Invoke the delegate.
            FileViewButtonPressed?.Invoke(this, e);
        }

        private void NewFromTemplate(object sender, RoutedEventArgs e)
        {
            // Get the Template filename from the button
            var b = sender as Button;
            var filename = (string) b.Content;

            // Create the event arguments to pass to the ShellWindow.
            var d = new FileViewEventArgs();
            d.PageEvent = FileViewEvent.NewFromTemplate;
            d.Description = "New Restaurant Worksheet";
            d.Filename = filename;

            // Raise an event to the ShellWindow to open a file and display the content page.
            OnFileViewButtonPressed(d);
        }

        private void AppBarCommand(object sender, RoutedEventArgs e)
        {
            // Get the Template filename from the button
            var b = sender as Button;
            var filename = (string) b.Content;

            // Create the event arguments to pass to the ShellWindow.
            var d = new FileViewEventArgs();
            d.PageEvent = FileViewEvent.AppBarCommand;
            d.TargetCommand = "Multi-Unit Comparison";

            // Raise an event to the ShellWindow to open a file and display the content page.
            OnFileViewButtonPressed(d);
        }

        //private void MRUMenu_OnOpenOtherSheet(object sender, RoutedEventArgs e)
        //{
        //    //MessageBox.Show("MainWindow: Opening other sheet!");

        //    // Raise an event to the ShellWindow to open a file and display the content page.
        //    OnFileViewButtonPressed(new FileViewEventArgs {PageEvent = FileViewEvent.OpenOther});
        //}

        private void MRUMenu_OnRecentFileSelected(object sender, SelectionChangedEventArgs e)
        {
            MRUEntry file = null;

            var args = e;

            if (args.AddedItems.Count != 0)
                file = (MRUEntry) args.AddedItems[0];

            if (file != null)
            {
                // Create the event arguments to pass to the ShellWindow.
                var d = new FileViewEventArgs
                {
                    PageEvent = FileViewEvent.OpenRecentFile,
                    Description = file.FullPath,
                    Filename = file.FileName,
                    Pinned = file.Pinned
                };

                // Move the item to the top.
                //    RecentFiles.Items.MoveCurrentToFirst();

                // Raise an event to the ShellWindow to open a file and display the content page.
                OnFileViewButtonPressed(d);
            }
        }

        private void MRUMenu_OnOpenACopy(object sender, SelectionChangedEventArgs e)
        {
            MRUEntry file = null;

            var args = e;

            if (args.AddedItems.Count != 0)
                file = (MRUEntry) args.AddedItems[0];

            if (file != null)
            {
                //MessageBox.Show("MainWindow: OpenRecentSheet: " + fileName);

                // Create the event arguments to pass to the ShellWindow.
                var d = new FileViewEventArgs
                {
                    PageEvent = FileViewEvent.OpenACopy,
                    Description = file.FullPath,
                    Filename = file.FileName,
                    Pinned = file.Pinned
                };

                // Move the item to the top.
                //    RecentFiles.Items.MoveCurrentToFirst();

                // Raise an event to the ShellWindow to open a file and display the content page.
                OnFileViewButtonPressed(d);
            }
        }

        private void MRUMenu_OnOpenOtherFileClicked(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("MainWindow: Opening other sheet!");

            // Raise an event to the ShellWindow to open a file and display the content page.
            OnFileViewButtonPressed(new FileViewEventArgs {PageEvent = FileViewEvent.OpenOther});
        }
    }
}