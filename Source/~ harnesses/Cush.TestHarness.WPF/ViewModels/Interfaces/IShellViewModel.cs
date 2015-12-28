using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Cush.Common;

namespace Cush.TestHarness.WPF.ViewModels.Interfaces
{
    public interface IShellViewModel
    {
        ICommand BackButtonClickCommand { get; }
        ContentControl Content { get; set; }
        event EventHandler<FileProgressEventArgs> FileProgressStatusChanged;
        event EventHandler<ProgressChangedEventArgs> FileProgressChanged;
    }
}