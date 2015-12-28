using System;
using System.Diagnostics.CodeAnalysis;

namespace Cush.Common.FileHandling
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class FileState<T> : BindableBase, IKeyedItem, IFileState<T> where T : class
    {
        private DateTime _dateUpdated;
        private bool _dirty;
        private string _displayName;
        private bool _fileLocked;
        private string _filename;
        private string _fullPath;
        private Guid _guid;
        private bool _inUse;
        private bool _locked;
        private T _object;
        private bool _optionsLocked;
        private string _password;
        private bool _saved;
        private bool _shown;

        public DateTime DateUpdated
        {
            get { return _dateUpdated; }
            set { SetProperty(ref _dateUpdated, value); }
        }

        public bool InUse
        {
            get { return _inUse; }
            set { SetProperty(ref _inUse, value); }
        }

        public string Filename
        {
            get { return _filename; }
            set { SetProperty(ref _filename, value); }
        }

        public string FullPath
        {
            get { return _fullPath; }
            set { SetProperty(ref _fullPath, value); }
        }

        public bool Shown
        {
            get { return _shown; }
            set { SetProperty(ref _shown, value); }
        }

        public bool Dirty
        {
            get { return _dirty; }
            set { SetProperty(ref _dirty, value); }
        }

        public bool Saved
        {
            get { return _saved; }
            set { SetProperty(ref _saved, value); }
        }

        public bool Locked
        {
            get { return _locked; }
            set { SetProperty(ref _locked, value); }
        }

        public T Object
        {
            get { return _object; }
            set { SetProperty(ref _object, value); }
        }

        public bool FileLocked
        {
            get { return _fileLocked; }
            set { SetProperty(ref _fileLocked, value); }
        }

        public bool OptionsLocked
        {
            get { return _optionsLocked; }
            set { SetProperty(ref _optionsLocked, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public string DisplayName
        {
            get { return _displayName; }
            set { SetProperty(ref _displayName, value); }
        }

        public Guid Guid
        {
            get { return _guid; }
            set { SetProperty(ref _guid, value); }
        }
    }
}