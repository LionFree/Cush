using System;
using System.Diagnostics.CodeAnalysis;

namespace Cush.Common
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class FileProgressEventArgs : EventArgs
    {
        public FileProgressEventArgs(FileProgressStatus status, Action cancelCallback=null)
        {
            Status = status;
            CancelCallback = cancelCallback;
        }

        public FileProgressStatus Status { get; private set; }
        public Action CancelCallback { get; private set; }

    }
}