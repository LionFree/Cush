using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows;
using Cush.Common.Exceptions;
using Cush.Common.Helpers;
using Cush.Common.Logging;
using Microsoft.Win32;

// ReSharper disable InconsistentNaming

namespace Cush.Common.FileHandling
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
    public sealed class FileHandler<T> : DependencyObject where T : class
    {
        public delegate void IOEventHandler(object sender, IOEventArgs e);

        private readonly FileReader _fileReader;
        private readonly FileWriter _fileWriter;

        private readonly ILogger _logger;

        private readonly OpenFileDialog _openDialog = new OpenFileDialog {FileName = ""};
        private readonly SaveFileDialog _saveDialog = new SaveFileDialog {FileName = ""};

        public readonly DependencyProperty FileProgressProperty =
            DependencyProperty.Register("FileProgress", typeof (int), typeof (FileHandler<T>),
                new PropertyMetadata(0));

        public readonly DependencyProperty FilesProperty =
            DependencyProperty.Register("Files", typeof (ObservableCollection<IFileState<T>>), typeof (FileHandler<T>),
                new FrameworkPropertyMetadata(null, OnFileCollectionChanged));

        public FileHandler(FileReader reader, FileWriter writer) : this(Loggers.Trace, reader, writer)
        {
        }

        public FileHandler(ILogger logger, FileReader reader, FileWriter writer):this(logger, reader, writer, new ObservableCollection<IFileState<T>>())
        {
        }

        internal FileHandler(ILogger logger, FileReader reader, FileWriter writer, ObservableCollection<IFileState<T>> files)
        {
            _logger = logger;
            Files = files;
            _fileReader = reader;
            _fileWriter = writer;
            CancelFileOperations += _fileReader.CancelFileOperations;
        }

        public string DefaultExt
        {
            get { return _openDialog.DefaultExt; }
            set
            {
                _openDialog.DefaultExt = value;
                _saveDialog.DefaultExt = value;
            }
        }

        public string Filter
        {
            get { return _openDialog.Filter; }
            set
            {
                _openDialog.Filter = value;
                _saveDialog.Filter = value;
            }
        }

        public string FileName
        {
            get { return _openDialog.FileName; }
            set
            {
                if (_openDialog.FileName != value)
                {
                    _openDialog.FileName = value;
                    _saveDialog.FileName = value;
                }
            }
        }

        public bool ShowReadOnly
        {
            get { return _openDialog.ShowReadOnly; }
            set { _openDialog.ShowReadOnly = value; }
        }

        public bool OverwritePrompt
        {
            get { return _saveDialog.OverwritePrompt; }
            set { _saveDialog.OverwritePrompt = value; }
        }

        public string SaveFileName
        {
            get { return _saveDialog.FileName; }
            set { _saveDialog.FileName = value; }
        }

        public string OpenFileName
        {
            get { return _openDialog.FileName; }
            set { _openDialog.FileName = value; }
        }

        [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
        public string DefaultFileName { get; set; } = string.Empty;

        public int Count => Files.Count;
        public int CurrentFileIndex { get; set; }

        public T CurrentFile
        {
            get
            {
                if (Count <= CurrentFileIndex || Count == 0 || CurrentFileIndex == -1)
                {
                    return null;
                }
                return Files[CurrentFileIndex].Object;
            }
            set { Files[CurrentFileIndex].Object = value; }
        }

        public IFileState<T> CurrentFileState
        {
            get
            {
                if (Count <= CurrentFileIndex || Count == 0)
                {
                    return null;
                }

                return Files[CurrentFileIndex];
            }
            set { Files[CurrentFileIndex] = value; }
        }

        /// <summary>
        ///     Gets or sets the FileProgress of the controls.
        /// </summary>
        public int FileProgress
        {
            get { return (int) GetValue(FileProgressProperty); }
            set { SetValue(FileProgressProperty, value); }
        }


        public ObservableCollection<IFileState<T>> Files
        {
            get { return (ObservableCollection<IFileState<T>>) GetValue(FilesProperty); }
            set { SetValue(FilesProperty, value); }
        }

        /// <summary>
        ///     Occurs when an element of the Files collection changes.
        /// </summary>
        [SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
        public event PropertyChangedEventHandler FileChanged;

        private static void OnFileCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as FileHandler<T>;

            var old = e.OldValue as ObservableCollection<IFileState<T>>;

            if (old != null)
                if (me != null) old.CollectionChanged -= me.OnFilesCollectionChanged;

            var n = e.NewValue as ObservableCollection<IFileState<T>>;

            if (n != null)
                if (me != null) n.CollectionChanged += me.OnFilesCollectionChanged;
        }

        private void OnFilesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    // Clear and update entire collection
                }

                if (e.NewItems != null)
                {
                    foreach (IFileState<T> item in e.NewItems)
                    {
                        // Subscribe for changes on item
                        item.PropertyChanged += OnFileChanged;

                        // Add item to internal collection
                    }
                }

                if (e.OldItems != null)
                {
                    foreach (IFileState<T> item in e.OldItems)
                    {
                        // Unsubscribe for changes on item
                        item.PropertyChanged -= OnFileChanged;

                        // Remove item from internal collection
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in FileHandler OnFilesCollectionChanged: " + ex.Message);
            }
        }

        /// <summary>
        ///     When overridden in a derived class, is invoked whenever application code or internal processes
        ///     change an element of the Files collection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFileChanged(object sender, PropertyChangedEventArgs e)
        {
            FileChanged?.Invoke(this, e);
        }

        /// <summary>
        ///     Occurs whenever the FileHandler is done reading or saving a file.
        /// </summary>
        public event IOEventHandler ReadWriteDone;

        /// <summary>
        ///     Is invoked whenever application code or internal processes call
        ///     <see cref="M:Read" /> or <see cref="M:Save" />.
        /// </summary>
        private void OnReadWriteDone(IOEventArgs e)
        {
            ReadWriteDone?.Invoke(this, e);
        }

        /// <summary>
        ///     Occurs whenever the FileHandler starts reading or saving a file.
        /// </summary>
        public event IOEventHandler ReadWriteStarting;

        /// <summary>
        ///     Is invoked whenever application code or internal processes finish
        ///     <see cref="M:Read" /> or <see cref="M:Save" />.
        /// </summary>
        private void OnReadWriteStarting(IOEventArgs e)
        {
            ReadWriteStarting?.Invoke(this, e);
        }

        ///// <summary>
        /////     Occurs when the file read/write progress changes.
        ///// </summary>
        //[SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
        //public event ProgressChangedEventHandler ProgressChanged;

        ///// <summary>
        /////     Is invoked whenever application code or internal processes call <see cref="M:Read" /> or <see cref="M:Save" />.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    ProgressChanged?.Invoke(this, e);
        //}

        /// <summary>
        ///     Sets the dirtybit of the current file.
        /// </summary>
        /// <param name="status">true if the file has unsaved changes; otherwise, false.</param>
        public void SetDirtyBit(bool status)
        {
            SetDirtyBit(status, CurrentFileIndex);
        }

        /// <summary>
        ///     Sets the dirtybit of the designated file.
        /// </summary>
        /// <param name="fileNumber">The index of the DataFile.</param>
        /// <param name="status">true if the file has unsaved changes; otherwise, false.</param>
        public void SetDirtyBit(bool status, int fileNumber)
        {
            //Trace.WriteLine("Setting dirty bit.");
            if (Files.Count > fileNumber && fileNumber > -1)
            {
                Files[fileNumber].Dirty = status;
            }
        }


        private string AttemptSave(object dataFile,
            string saveName,
            ProgressChangedEventHandler callback)
        {
            try
            {
                if (saveName != null)
                {
                    if (saveName == string.Empty)
                    {
                        const string header = "Failed to Save File";
                        const string message = "File must have a valid filename.";
                        MessageBox.Show(message, header, MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        return AttemptSave(dataFile, null, callback);
                    }

                    if (saveName == DefaultFileName)
                    {
                        return AttemptSave(dataFile, null, callback);
                    }

                    // Fire the Writing event so that the app can 
                    // show progress dialog, if desired.
                    OnReadWriteStarting(new IOEventArgs(FileAction.Save, -1, saveName));

                    // Attempt to save the data.
                    var goodSave = _fileWriter.Write(dataFile, saveName, callback);

                    // Fire the WriteDone event so that the app can
                    // close the progress dialog.
                    OnReadWriteDone(new IOEventArgs(FileAction.Save));

                    if (goodSave)
                    {
                        foreach (var item in Files.Where(item => item.Object == dataFile))
                        {
                            item.Dirty = false;
                        }

                        _saveDialog.FileName = saveName;
                        return saveName;
                    }
                }
                else
                {
                    //SaveName == null 
                    saveName = GetFileName(FileAction.Save);

                    if (saveName != null)
                    {
                        return AttemptSave(dataFile, saveName, callback);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                // Fire the WriteDone event so that the consumer can close the progress dialog.
                OnReadWriteDone(new IOEventArgs(FileAction.Save));

                _logger.Error(ex);
                return null;
            }
        }

        private bool LinkFile(T targetFile, int fileIndex, string fullPath)
        {
            ThrowHelper.IfNullThenThrow(() => fullPath);
            try
            {
                //Trace.WriteLine("Linking File.");
                var newFState = new FileState<T>();

                if (targetFile == null) return false;

                // Record the Path and filename in the FileState object.
                newFState.Filename = FileNameHelper.GetFileName(fullPath);
                newFState.FullPath = fullPath;
                newFState.Object = targetFile;
                newFState.Dirty = false;

                //Console.WriteLine("Adding file: " + input.Guid.ToString());
                Files.Add(newFState);

                // Put the file where it belongs.
                if ((fileIndex > -1) && (Files.Count - 1 != fileIndex))
                {
                    Trace.WriteLine("Moving File to designated index...");
                    Files.RemoveAt(fileIndex);
                    Files.Move(Files.Count - 1, fileIndex);
                }

                //Trace.WriteLine("Linking Done.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }

        public string GetFileName(FileAction action)
        {
            try
            {
                FileDialog dialog = null;
                switch (action)
                {
                    case FileAction.Save:
                        dialog = _saveDialog;
                        break;

                    case FileAction.Open:
                        dialog = _openDialog;
                        break;
                }

                var result = dialog?.ShowDialog();

                return result == true ? dialog.FileName : null;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return string.Empty;
            }
        }

        public int IndexOf(T targetFile)
        {
            foreach (var item in Files.Where(item => item.Object == targetFile))
            {
                return Files.IndexOf(item);
            }
            return -1;
        }

        public T Lookup(Guid guid)
        {
            return (from item in Files where item.Guid == guid select item.Object).FirstOrDefault();
        }


        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private int NewFile(string defaultName, int fileIndex, bool sentByUser)
        {
            if (sentByUser && (fileIndex < 0)) throw new ArgumentException("fileIndex cannot be negative.");

            try
            {
                var targetFile = Activator.CreateInstance<T>();
                var success = LinkFile(targetFile, fileIndex, defaultName);
                if (!success) return -1;

                var output = IndexOf(targetFile);
                CurrentFileIndex = output;
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return -1;
            }
        }

        public int NewFile(string defaultName, int fileIndex)
        {
            return NewFile(defaultName, fileIndex, true);
        }

        public int NewFile(string defaultName)
        {
            return NewFile(defaultName, -1, false);
        }

        public bool FileAlreadyOpen(string fileName)
        {
            return Files.Count > 0 && Files.Any(t => t.FullPath == fileName);
        }

        // This routine makes sure that the filename is legit, then opens it
        public int Open(string fileName)
        {
            return Open(fileName, -1, false, null);
        }

        public int Open(string fileName, ProgressChangedEventHandler callback)
        {
            return Open(fileName, -1, false, callback);
        }

        public int Open(string fileName, int fileIndex)
        {
            return Open(fileName, fileIndex, true, null);
        }

        public int Open(string fileName, int fileIndex, ProgressChangedEventHandler callback)
        {
            return Open(fileName, fileIndex, true, callback);
        }

        internal event EventHandler CancelFileOperations;

        public void CancelFileOperation()
        {
            CancelFileOperations?.Invoke(this, new EventArgs());
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private int Open(string fileName, int fileIndex, bool userSentIndex, ProgressChangedEventHandler callback)
        {
            ThrowHelper.IfNullOrEmptyThenThrow(() => fileName);
            if ((fileIndex < 0) && (userSentIndex)) throw new ArgumentException("fileIndex cannot be negative.");

            // ...and if the file isn't already loaded...
            if (!FileAlreadyOpen(fileName))
            {
                // Load it.
                var result = ReadFile(fileName, fileIndex, callback);

                if (result)
                {
                    SaveFileName = fileName;
                    if (fileIndex > -1) return fileIndex;
                }
            }

            // Return the index of that document.
            if (Files.Count > 0)
            {
                // Get the filename of the object.
                for (var i = 0; i < Files.Count; i++)
                {
                    if (Files[i].FullPath == fileName)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }


        // This routine actually reads the file

        public bool ReadFile(string fileName, int fileIndex = -1, ProgressChangedEventHandler callback = null)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException();

            var result = false;
            try
            {
                var file = new FileInfo(fileName);
                if (file.Exists)
                {
                    OnReadWriteStarting(new IOEventArgs(FileAction.Open, file.Length, fileName));

                    var input = _fileReader.Read<T>(fileName, callback);

                    if (input != null)
                    {
                        LinkFile(input, fileIndex, fileName);
                        result = true;
                    }
                    else
                    {
                        Trace.WriteLine("DataFile is null.");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                result = false;
            }
            catch (FileNotFoundException)
            {
                result = false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                result = false;
            }
            finally
            {
                OnReadWriteDone(new IOEventArgs(FileAction.Open, -1, fileName));
            }
            return result;
        }


        public string Save(T dataFile, SaveType saveType, ProgressChangedEventHandler callback = null)
        {
            try
            {
                // Set the filename based on what kind of save this is.
                string fileName = null;
                if (saveType == SaveType.Save)
                {
                    if (Files.Count > 0)
                    {
                        // Get the filename of the object.
                        foreach (var item in Files.Where(item => item.Object.Equals(dataFile)))
                        {
                            fileName = item.FullPath;
                        }
                    }
                }

                // Attempt the save.
                var newName = AttemptSave(dataFile, fileName, callback);


                // Write the new name, if it changed.
                if (fileName != newName)
                {
                    if (Files.Count > 0)
                    {
                        // Get the filename of the object.
                        foreach (var item in Files.Where(item => item.Object.Equals(dataFile)))
                        {
                            item.FullPath = newName;
                        }
                    }
                }

                return newName;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        public bool Remove(Guid targetGuid)
        {
            return RemoveOn(fileState => fileState.Guid == targetGuid);
        }

        public bool Remove(T targetFile)
        {
            return RemoveOn(fileState => fileState.Object == targetFile);
        }

        private bool RemoveOn(Func<IFileState<T>, bool> condition)
        {
            foreach (var item in Files.Where(condition.Invoke))
            {
                Files.Remove(item);
                return true;
            }
            return false;
        }
    }
}