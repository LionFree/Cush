using System.IO;
using System.Security.AccessControl;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;

namespace Cush.Common.FileHandling
{
    class CushFileStream : FileStream
    {
        public CushFileStream(string path) : base(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)
        {
        }

        public CushFileStream(string path, FileMode mode) : base(path, mode, FileAccess.ReadWrite, FileShare.ReadWrite)
        {
        }

        public CushFileStream(string path, FileMode mode, FileAccess access) : base(path, mode, access)
        {
        }

        public CushFileStream(string path, FileMode mode, FileAccess access, FileShare share)
            : base(path, mode, access, share)
        {
        }

        public CushFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
            : base(path, mode, access, share, bufferSize)
        {
        }

        public CushFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize,
            FileOptions options) : base(path, mode, access, share, bufferSize, options)
        {
        }

        public CushFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize,
            bool useAsync) : base(path, mode, access, share, bufferSize, useAsync)
        {
        }

        public CushFileStream(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize,
            FileOptions options, FileSecurity fileSecurity)
            : base(path, mode, rights, share, bufferSize, options, fileSecurity)
        {
        }

        public CushFileStream(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize,
            FileOptions options) : base(path, mode, rights, share, bufferSize, options)
        {
        }

        public CushFileStream([NotNull] SafeFileHandle handle, FileAccess access) : base(handle, access)
        {
        }

        public CushFileStream([NotNull] SafeFileHandle handle, FileAccess access, int bufferSize)
            : base(handle, access, bufferSize)
        {
        }

        public CushFileStream([NotNull] SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync)
            : base(handle, access, bufferSize, isAsync)
        {
        }
    }
}