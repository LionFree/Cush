using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Cush.Common;
using Cush.Common.Exceptions;
using Cush.Common.FileHandling;
using Cush.Common.Helpers;
using Cush.Common.Logging;

namespace Cush.TestHarness.WPF.Model
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class FileClerk<T> where T : class
    {
        private readonly FileHandler<T> _fileHandler;
        private readonly ILogger _logger;

        public FileClerk(ILogger logger, FileReader reader, FileWriter writer)
            : this(logger, new FileHandler<T>(logger, reader, writer))
        {
        }

        public FileClerk(ILogger logger, FileHandler<T> handler)
        {
            _logger = logger;
            _fileHandler = handler;
        }

        private int CurrentFileIndex
        {
            get { return _fileHandler.CurrentFileIndex; }
            set
            {
                if (_fileHandler != null && _fileHandler.CurrentFileIndex != value)
                {
                    _fileHandler.CurrentFileIndex = value;
                }
            }
        }

        internal T CurrentFile => _fileHandler?.CurrentFile;

        private void OpenFile(string fileName)
        {
            ThrowHelper.IfNullOrEmptyThenThrow(() => fileName);
            OnFileProgressStatusChanged(FileProgressStatus.Loading);

            try
            {
                // Attempt to open the file.
                var fileNumber = _fileHandler.Open(fileName, 0, FileProgressChanged);

                // If the load failed completely, get out.
                if (fileNumber != 0)
                {
                    OnFileProgressStatusChanged(FileProgressStatus.Done);
                    return;
                }

                // If the file isn't already shown...
                if (!_fileHandler.Files[fileNumber].Shown)
                {
                    // Update the CurrentFileIndex to the most recent file.
                    CurrentFileIndex = _fileHandler.Files.Count - 1;

                    // Add the propertychanged handler so that we can update the save button appropriately.
                    _fileHandler.Files[fileNumber].PropertyChanged += OnFileChanged;
                }

                // Set the brand new file as not dirty.
                _fileHandler.SetDirtyBit(false);

                // Show the file to whoever's asking.
                _fileHandler.Files[fileNumber].Shown = true;
                RaiseEvent(FileOpenedEvent, new FileEventArgs { Fullpath = fileName, Pinned = false });
            }
            catch (Exception ex)
            {
                // remove file from MRU list
                //_mruHandler.Remove(fileName);

                _logger.Error(ex);
            }
            OnFileProgressStatusChanged(FileProgressStatus.Done);
        }

        private void OnFileChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Shown" || e.PropertyName == "Dirty") return;
            _fileHandler.SetDirtyBit(true);
        }

        internal void CloseFile()
        {
            _fileHandler.Remove(CurrentFile);
            RaiseEvent(FileClosedEvent, new FileEventArgs { FileCount = _fileHandler.Files.Count });

            //if (num == 0)
            //{
            //    ShellWindow.SetPage(Page.FileMenu, Visibility.Hidden);
            //}
        }

        private void RaiseEvent(EventHandler<FileEventArgs> handler, FileEventArgs args)
        {
            handler?.Invoke(this, args);
        }

        internal event EventHandler<FileEventArgs> FileClosedEvent;
        internal event EventHandler<FileEventArgs> FileOpenedEvent;
        internal event ProgressChangedEventHandler FileProgressChanged;
        internal event EventHandler<FileProgressEventArgs> FileProgressStatusChanged;

        private void OnFileProgressStatusChanged(FileProgressStatus status)
        {
            var handler = FileProgressStatusChanged;
            if (handler == null) return;
            var args = new FileProgressEventArgs(status, Cancel);
            handler(this, args);
        }

        private void Cancel()
        {
            _fileHandler.CancelFileOperation();
            CloseFile();
        }

        public void OpenOtherFile(AskUserIfOkayToSaveChangesCallback okayToSaveChangesCallback)
        {
            if (!OkayToCreateNew(okayToSaveChangesCallback)) return;
            try
            {
                var fileName = _fileHandler.GetFileName(FileAction.Open);
                if (string.IsNullOrEmpty(fileName)) return;
                OpenFile(fileName);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private bool OkayToCreateNew(AskUserIfOkayToSaveChangesCallback okayToSaveChangesCallback)
        {
            var number = _fileHandler?.Files?.Count;
            if (!(number > 0)) return true;

            if (!OkayToCloseFile(okayToSaveChangesCallback)) return false;

            CloseFile();
            return true;
        }

        private bool OkayToCloseFile(AskUserIfOkayToSaveChangesCallback okayToSaveChangesCallback)
        {
            var index = CurrentFileIndex;
            if (_fileHandler.Files.Count <= index) return false;

            var fileState = _fileHandler.Files[index];
            if (!fileState.Dirty) return true;

            var okToSave = okayToSaveChangesCallback(GetShortFileName(fileState));
            if (okToSave)
            {
                _fileHandler.Save(CurrentFile, SaveType.Save);
            }
            return okToSave;
        }

        private string GetShortFileName(IFileState<T> fileState)
        {
            var file = FileNameHelper.StripFileName(fileState.Filename);
            file = FileNameHelper.StripFileExtension(file);
            return file;
        }
    }

    /// <summary>
    ///     A callback method (return type bool) that asks the user if it's okay to save changes.
    /// </summary>
    /// <param name="fileName">The filename in question.</param>
    /// <returns></returns>
    public delegate bool AskUserIfOkayToSaveChangesCallback(string fileName);
}