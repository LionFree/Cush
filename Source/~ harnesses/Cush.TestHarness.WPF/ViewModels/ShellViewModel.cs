using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Cush.Common;
using Cush.Common.Configuration;
using Cush.Common.FileHandling;
using Cush.Common.Logging;
using Cush.TestHarness.WPF.Model;
using Cush.TestHarness.WPF.ViewModels.Interfaces;
using Cush.WPF;
using Strings = Cush.TestHarness.WPF.Views.Strings;

namespace Cush.TestHarness.WPF.ViewModels
{
    public class ShellViewModel : BindableBase, IShellViewModel
    {
        private readonly FileClerk<DataFile> _fileHandler;
        private readonly ILogger _logger;
        private readonly ObservableCollection<MRUEntry> _mruList;
        private readonly PageProvider _pages;
        private readonly MRUUserSettingsHandler _mruHandler;

        private ContentControl _content;
        
        internal ShellViewModel(ILogger logger, FileClerk<DataFile> fileHandler,
            ref MRUUserSettingsHandler mruSettingsHandler,
            PageProvider pages, ViewModelProvider viewModels)
        {
            _logger = logger;
            _mruHandler = mruSettingsHandler;
            _mruList = mruSettingsHandler.Read();//new ObservableCollection<MRUEntry>();

            fileHandler.FileOpenedEvent += FileOpenedEvent;
            fileHandler.FileClosedEvent += FileClosedEvent;
            fileHandler.FileProgressChanged += OnFileProgressChanged;
            fileHandler.FileProgressStatusChanged += OnFileProgressStatusChanged;

            viewModels.StartPageViewModel.OpenRecentFileRequested += _startPage_OpenRecentFile;
            viewModels.StartPageViewModel.OpenOtherFileRequested += _startPage_OpenOtherFile;
            viewModels.StartPageViewModel.NewFileRequested += _startPage_NewFileRequested;
            
            _fileHandler = fileHandler;
            _pages = pages;

            Content = _pages.StartPage;
        }

        private void _startPage_NewFileRequested(object sender, EventArgs e)
        {
            var success = _fileHandler.RequestNewFile(OkayToSaveChanges, DataFile.Default);

            if (success)
            {
                // Switch to the Activity editor page.
                Content = _pages.ActivityPage;
            }
        }


        public bool IsFileMenuVisible => !Equals(Content, _pages.StartPage);

        public bool IsForwardButtonVisible => (!IsFileMenuVisible) && (_fileHandler.CurrentFile != null);

        public object PageSwapRequested => new RelayCommand(() =>
        {
            Content = (IsForwardButtonVisible ? (ContentControl)_pages.ActivityPage : (ContentControl)_pages.StartPage);
        });

        public ContentControl Content
        {
            get { return _content; }
            set
            {
                SetProperty(ref _content, value);
                OnPropertyChanged(nameof(IsFileMenuVisible));
                OnPropertyChanged(nameof(IsForwardButtonVisible));
            }
        }

        

        public event EventHandler<FileProgressEventArgs> FileProgressStatusChanged;
        public event EventHandler<ProgressChangedEventArgs> FileProgressChanged;


        private void OnFileProgressStatusChanged(object sender, FileProgressEventArgs e)
        {
            FileProgressStatusChanged?.Invoke(this, e);
        }

        private void OnFileProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FileProgressChanged?.Invoke(this, e);
        }


        private void FileClosedEvent(object sender, FileEventArgs e)
        {
            _logger.Trace("File Closed!");
        }

        private void FileOpenedEvent(object sender, FileEventArgs e)
        {
            _logger.Trace("File Opened!");

            if (!Authenticate(_fileHandler.CurrentFile))
            {
                _fileHandler.CloseFile();
                return;
            }

            FileValidator.ValidateAllActivities(_fileHandler.CurrentFile);

            UpdateMRUList(e.Fullpath, e.Pinned);
            Content = _pages.ActivityPage;

            //ShellWindow.SetPage(Page.ActivityEditor);
            //ShellWindow.UpdateLayout();
            //// Update buttons.
            //ShellWindow.ActivityContent.UpdateSaveButton();
            //ShellWindow.ActivityContent.UpdateValidateButton();
        }

        private bool Authenticate(DataFile currentFile)
        {
            var ep = currentFile.Settings.EncryptedPassword;
            if (string.IsNullOrEmpty(ep)) return true;

            //var loginDialog = new LoginDialog(ep, titleText, okString) {Owner = this};
            //loginDialog.Show();
            //return loginDialog.DialogResult;
            return true;
        }

        private void UpdateMRUList(string fullPath, bool pinned)
        {
            var openedFile = new MRUEntry {FullPath = fullPath, Pinned = pinned};
            //ShellWindow.FileMenuContent.MRUMenu.Add(openedFile);
            _mruList.Add(openedFile);

            // Write the MRU list.
            WriteMRUList(_mruList);
        }

        private void _startPage_OpenOtherFile(object sender, EventArgs e)
        {
            _fileHandler.OpenOtherFile(OkayToSaveChanges);
        }

        private bool OkayToSaveChanges(string shortFileName)
        {
            var okay = MessageBox.Show($"Do you want to save the changes to {shortFileName}?",
                Strings.TEXT_ApplicationName,
                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            switch (okay)
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return true;
            }
            return false;
        }

        internal void WriteMRUList(IEnumerable<MRUEntry> mruList)
        {
            try
            {
                //// Get the app.config section.
                //var mruListSection = MRUListSection.Open();

                //// Clear the section's MRU List.
                //mruListSection.MRUList.Clear();

                //// Populate the section with MRUEntries.
                //foreach (var item in mruList)
                //{
                //    var temp = new MRUEntryElement {FullPath = item.FullPath, Pinned = item.Pinned};
                //    mruListSection.MRUList.Add(temp);
                //}

                // Save the full configuration file and force save even if the file was not modified.
                _mruHandler.Save(mruList);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }


        private void _startPage_OpenRecentFile(object sender, FileEventArgs e)
        {
            Trace.WriteLine("ShellViewModel:  OpenRecentFile: " + e.Filename);
        }

        private void OnBackButtonClick(object obj)
        {
            Trace.WriteLine("Back Button Clicked");
        }
    }
}