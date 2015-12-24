﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cush.TestHarness.WPF.Views.Pages;
using Cush.WPF;

namespace Cush.TestHarness.WPF.ViewModels
{
    public interface IShellViewModel
    {
        string TestString { get; set; }
    }

    public class MockShellViewModel : ShellViewModel
    {
        public MockShellViewModel() : base(new StartPage(new StartPageViewModel()))
        {
        }


    }

    public class ShellViewModel : BindableBase
    {
        private readonly StartPage _fileView;
        private ICommand _backButtonCommand;

        public ShellViewModel(StartPage fileView)
        {
            _fileView = fileView;
            _fileView.FileViewButtonPressed += content_FileViewButtonPressed;

            Content = _fileView;
        }

        public object BackButtonCommand => _backButtonCommand ??
                                           (_backButtonCommand = new RelayCommand("BackButtonCommand", OnBackButtonClick));

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

                    case FileViewEvent.OpenRecentFile:
                        MessageBox.Show("ShellWindow:  OpenRecentFile: " + e.Filename);
                        break;


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



        private ContentControl _content;

        public ContentControl Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }
    }
}