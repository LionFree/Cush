﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cush.Common;
using Cush.Common.Configuration;
using Cush.Common.FileHandling;
using Cush.Common.Logging;
using Cush.TestHarness.WPF.Model;
using Cush.TestHarness.WPF.ViewModels.Interfaces;
using Cush.TestHarness.WPF.Views;
using Cush.TestHarness.WPF.Views.Events;
using Cush.TestHarness.WPF.Views.Pages;
using Cush.WPF;

namespace Cush.TestHarness.WPF.ViewModels
{
    public class ShellViewModel : BindableBase, IShellViewModel
    {
        private readonly ActivityPage _activityPage;
        private readonly FileClerk<DataFile> _fileHandler;
        private readonly ILogger _logger;

        private readonly ThreadSafeObservableCollection<MRUEntry> _mruList;
        private readonly StartPage _startPage;
        private ICommand _backButtonCommand;
        private ContentControl _content;

        internal ShellViewModel(ILogger logger, FileClerk<DataFile> fileHandler, PagePack pages)
        {
            _mruList = new ThreadSafeObservableCollection<MRUEntry>();
            _logger = logger;
            _fileHandler = fileHandler;
            _startPage = pages.StartPage;
            _activityPage = pages.ActivityPage;

            _fileHandler.FileOpenedEvent += FileOpenedEvent;
            _fileHandler.FileClosedEvent += FileClosedEvent;
            _fileHandler.FileProgressChanged += OnFileProgressChanged;
            _fileHandler.FileProgressStatusChanged += OnFileProgressStatusChanged;

            _startPage.FileViewButtonPressed += content_FileViewButtonPressed;
            _startPage.OpenRecentFile += _startPage_OpenRecentFile;
            _startPage.OpenOtherFile += _startPage_OpenOtherFile;
            Content = _startPage;
        }


        public ICommand BackButtonClickCommand => _backButtonCommand ??
                                                  (_backButtonCommand =
                                                      new RelayCommand("BackButtonCommand", OnBackButtonClick));

        public ContentControl Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        private void OnFileProgressStatusChanged(object sender, FileProgressEventArgs e)
        {
            FileProgressStatusChanged?.Invoke(this, e);
        }

        public event EventHandler<FileProgressEventArgs> FileProgressStatusChanged;
        public event EventHandler<ProgressChangedEventArgs> FileProgressChanged;

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
            Content = _activityPage;

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
            var openedFile = new MRUEntry { FullPath = fullPath, Pinned = pinned };
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
                Views.Strings.TEXT_ApplicationName,
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
            // TODO: Move to viewModel
            //ShellWindow.UpdateLayout();
            //// Get the MRU list from the MRU control.
            //ObservableCollection<MRUEntry> mruList = ShellWindow.FileMenuContent.MRUMenu.Files;

            try
            {
                // Get the app.config section.
                var mruListSection = MRUListSection.Open();

                // Clear the section's MRU List.
                mruListSection.MRUList.Clear();

                // Populate the section with MRUEntries.
                foreach (var item in mruList)
                {
                    var temp = new MRUEntryElement { FullPath = item.FullPath, Pinned = item.Pinned };
                    mruListSection.MRUList.Add(temp);
                }

                // Save the full configuration file and force save even if the file was not modified.
                mruListSection.Save();
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

        internal void content_FileViewButtonPressed(object sender, FileViewEventArgs e)
        {
            try
            {
                switch (e.PageEvent)
                {
                    case FileViewEvent.ToggleRecentFilePin:
                        break;

                    case FileViewEvent.OpenOther:
                        RaiseOpenEvent();
                        break;

                    //case FileViewEvent.OpenRecentFile:
                    //    MessageBox.Show("ShellWindow:  OpenRecentFile: " + e.Filename);
                    //    break;


                    case FileViewEvent.OpenACopy:
                        MessageBox.Show("ShellWindow:  OpenACopy: " + e.Filename);
                        break;

                    case FileViewEvent.NewFromTemplate:
                        MessageBox.Show("NewFromTemplate: " + e.Filename);
                        break;

                    case FileViewEvent.AppBarCommand:
                        MessageBox.Show("AppbarCommand: " + e.TargetCommand);
                        break;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("content_FileViewButtonPressed.Exception: " + ex.Message);
            }
        }

        // This method raises the OpenClicked event 
        private void RaiseOpenEvent()
        {
            //var newEventArgs = new RoutedEventArgs(OpenClickedEvent);
            //RaiseEvent(newEventArgs);
        }
    }
}