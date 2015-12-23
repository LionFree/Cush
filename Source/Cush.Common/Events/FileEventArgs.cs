namespace Cush.Common
{
    public class FileEventArgs : System.EventArgs
    {
        public FileEventArgs(FileStatus status)
        {
            Status = status;
        }

        public FileStatus Status { get; private set; }
    }

    public enum FileStatus
    {
        Done = 0,
        Loading = 1,
        Saving = 2
    }
}