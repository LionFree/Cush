using System;
using System.ComponentModel;
using Cush.Common;

namespace Cush.TestHarness.WPF.ViewModels.Interfaces
{
    public interface IShellViewModel
    {
        event EventHandler<FileProgressEventArgs> FileProgressStatusChanged;
        event EventHandler<ProgressChangedEventArgs> FileProgressChanged;
    }
}