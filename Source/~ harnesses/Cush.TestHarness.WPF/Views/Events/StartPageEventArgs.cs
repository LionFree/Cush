using System;

namespace Cush.TestHarness.WPF.Views.Events
{
    public enum FileViewEvent
    {
        OpenACopy,
        OpenRecentFile,
        ToggleRecentFilePin,
        OpenOther,
        NewFromTemplate,
        AppBarCommand
    }

    public class FileViewEventArgs : EventArgs
    {
        public string Description;
        public string Filename;
        public FileViewEvent PageEvent;
        public bool Pinned;
        public string TargetCommand;
    }
}