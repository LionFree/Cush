﻿using System.Windows;
using Cush.TestHarness.WPF.ViewModels.Interfaces;
using Cush.TestHarness.WPF.Views.Dialogs;
using Cush.WPF.Controls;

namespace Cush.TestHarness.WPF.Views
{
    /// <summary>
    ///     Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView:CushWindow
    {
        private DialogPack _dialogs;

        public ShellView(IShellViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }

        internal void AttachDialogs(DialogPack dialogs)
        {
            _dialogs = dialogs;
        }

        private async void Settings_OnClick(object sender, RoutedEventArgs e)
        {
            await this.ShowDialogAsync(_dialogs.Settings);
        }

        private async void About_OnClick(object sender, RoutedEventArgs e)
        {
            await this.ShowDialogAsync(_dialogs.About);
        }
    }
}