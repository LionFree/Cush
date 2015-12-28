using System;
using System.ComponentModel;

namespace Cush.Common.FileHandling
{
    public interface IFileState<T> :INotifyPropertyChanged where T : class
    {
        DateTime DateUpdated { get; set; }
        bool Dirty { get; set; }
        string DisplayName { get; set; }
        bool FileLocked { get; set; }
        string Filename { get; set; }
        string FullPath { get; set; }
        Guid Guid { get; set; }
        bool InUse { get; set; }
        bool Locked { get; set; }
        T Object { get; set; }
        bool OptionsLocked { get; set; }
        string Password { get; set; }
        bool Saved { get; set; }
        bool Shown { get; set; }
    }
}