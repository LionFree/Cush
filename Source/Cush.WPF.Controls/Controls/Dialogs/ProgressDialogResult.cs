using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Cush.WPF.Controls
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class ProgressDialogResult
    {
        public ProgressDialogResult(RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                Cancelled = true;
            else if (e.Error != null)
                Error = e.Error;
            else
                Result = e.Result;
        }

        public object Result { get; private set; }
        public bool Cancelled { get; private set; }
        public Exception Error { get; }

        public bool OperationFailed => Error != null;
    }
}