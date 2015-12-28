using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using System.Windows.Input;
using Cush.Common;
using Cush.TestHarness.WPF.ViewModels.Interfaces;

namespace Cush.TestHarness.WPF.ViewModels.DesignTime
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class MockShellViewModel : IShellViewModel
    {
        public ICommand BackButtonClickCommand { get; }
        public ContentControl Content { get; set; }
        public event EventHandler<FileProgressEventArgs> FileProgressStatusChanged;
        public event EventHandler<ProgressChangedEventArgs> FileProgressChanged;
    }
}