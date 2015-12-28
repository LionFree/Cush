using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Cush.Common.FileHandling;

namespace Cush.Common
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [DebuggerStepThrough]
    public class IOEventArgs : EventArgs
    {
        //public delegate void IOEventHandler(object sender, IOEventArgs e);

        /// <summary>
        /// Initializes a new instance of the IOEventArgs class, using the supplied FileAction.
        /// </summary>
        /// <param name="action">The FileAction being taken in this event.</param>
        public IOEventArgs(FileAction action)
        {
            Action = action;
        }

        /// <summary>
        /// Initializes a new instance of the SharedRoutines.IOEventArgs class, using the FileAction,
        /// and providing the opportunity to declare the size and name of the target file.
        /// </summary>
        /// <param name="action">The SharedRoutines.FileAction being taken in this event.</param>
        /// <param name="fileSize">The size of the target file.</param>
        /// <param name="fileName">The name of the target file.</param>
        public IOEventArgs(FileAction action, long fileSize, string fileName)
        {
            Action = action;
            FileSize = fileSize;
            FileName = fileName;
        }

        /// <summary>
        /// Gets the SharedRoutines.FileAction being taken.
        /// </summary>
        public FileAction Action { get; }

        /// <summary>
        /// Gets the size of the file being acted on.
        /// </summary>
        public long FileSize { get; } = -1;

        /// <summary>
        /// Gets the name of the file being acted on.
        /// </summary>
        public string FileName { get; } = string.Empty;
    }
}