using System;
using System.ComponentModel;
using System.Windows;
using Cush.Common;
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
        private AboutDialog _aboutDialog;
        private ProgressDialog _progressDialog;
        private SettingsDialog _settingsDialog;

        public ShellView(IShellViewModel vm)
        {
            vm.FileProgressStatusChanged += OnFileProgressStatusChanged;
            vm.FileProgressChanged += OnFileProgressChanged;

            DataContext = vm;
            InitializeComponent();
        }

        private async void OnFileProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _progressDialog.Progress = e.ProgressPercentage;
            if (e.ProgressPercentage < 100) return;

            //if (!_progressDialog.IsVisible)
            //    await _progressDialog.WaitForLoadAsync();

            await _progressDialog.Close();
            _progressDialog.Progress = 0;
        }

        private async void OnFileProgressStatusChanged(object sender, FileProgressEventArgs e)
        {
            EventHandler cancelDelegate = delegate { e.CancelCallback(); };

            if (e.Status == FileProgressStatus.Done)
            {
                if (!_progressDialog.IsVisible)
                    await _progressDialog.WaitForLoadAsync();

                await _progressDialog.Close();
                _progressDialog.Cancel -= cancelDelegate;
                return;
            }

            _progressDialog.Reset();
            _progressDialog.Title = e.Status == FileProgressStatus.Loading ? "Loading Data..." : "Saving Data...";
            _progressDialog.Cancel += cancelDelegate;

            await this.ShowDialogAsync(_progressDialog);
        }

        internal void SetDialogs(DialogPack dialogs)
        {
            _settingsDialog = dialogs.SettingsDialog;
            _aboutDialog = dialogs.AboutDialog;
            _progressDialog = dialogs.ProgressDialog;
        }

        private async void Settings_OnClick(object sender, RoutedEventArgs e)
        {
            await this.ShowDialogAsync(_settingsDialog);
        }

        private async void About_OnClick(object sender, RoutedEventArgs e)
        {
            await this.ShowDialogAsync(_aboutDialog);
        }
    }
}