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
        
        public FileClerk(ILogger logger)
            : this(logger, new FileHandler<T>(logger))
        {
        }

        public FileClerk(ILogger logger, FileHandler<T> handler)
        {
            _logger = logger;
            _fileHandler = handler;
            //_defaultFileName = Strings.TEXT_DefaultFileName;
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

        public bool FileIsDirty => CurrentFileState.Dirty;

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public IFileState<T> CurrentFileState
            => _fileHandler.Files.Count == 0 ? new FileState<T>() : _fileHandler.Files[0];

        private void OpenFile(string fileName)
        {
            ThrowHelper.IfNullOrEmptyThenThrow(() => fileName);
            OnFileProgressStatusChanged(FileProgressStatus.Loading);

            try
            {
                // Attempt to open the file.
                var fileNumber = _fileHandler.Open<XmlFileOperator>(fileName, 0, FileProgressChanged);

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
                RaiseEvent(FileOpenedEvent, new FileEventArgs {Fullpath = fileName, Pinned = false});
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
            RaiseEvent(FileClosedEvent, new FileEventArgs {FileCount = _fileHandler.Files.Count});

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
                _fileHandler.Save<XmlFileOperator>(CurrentFile, SaveType.Save);
            }
            return okToSave;
        }

        private string GetShortFileName(IFileState<T> fileState)
        {
            var file = FileNameHelper.StripFileName(fileState.Filename);
            file = FileNameHelper.StripFileExtension(file);
            return file;
        }

        public bool RequestNewFile(AskUserIfOkayToSaveChangesCallback okayToSaveChanges,
            T aNewFile)
        {
            // If a file already exists, make sure we want to close the first one.
            if (!OkayToCreateNew(okayToSaveChanges)) return false;

            // Create a new file holder.
            var fileNumber = _fileHandler.NewFile("Untitled", 0);

            // If the load failed completely, get out.
            if (fileNumber != 0) return false;

            // Load the default values into the new file.
            _fileHandler.Files[fileNumber].Object = aNewFile;

            // Set the dirty bit to false.
            _fileHandler.SetDirtyBit(false);

            // If the file isn't already shown...
            if (!_fileHandler.Files[fileNumber].Shown)
            {
                // Update the CurrentFileIndex to the most recent file.
                CurrentFileIndex = _fileHandler.Count - 1;
            }

            RaiseEvent(FileCreated, new FileEventArgs());
            return true;
        }

        internal event EventHandler<FileEventArgs> FileCreated;

        internal string Save(SaveType saveType)
        {
            return _fileHandler.Save<XmlFileOperator>(CurrentFile, CurrentFileState.FullPath == "Untitled" ? SaveType.SaveAs : saveType);
        }

        public string Save()
        {
            return Save(SaveType.Save);
        }

        public string SaveAs()
        {
            return Save(SaveType.SaveAs);
        }
    }

    /// <summary>
    ///     A callback method (return type bool) that asks the user if it's okay to save changes.
    /// </summary>
    /// <param name="fileName">The filename in question.</param>
    /// <returns></returns>
    public delegate bool AskUserIfOkayToSaveChangesCallback(string fileName);
}