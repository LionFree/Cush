using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using Cush.Common.Exceptions;
using Cush.Common.ResourceSystem;

namespace Cush.Windows.FileSystem
{
    /// <summary>
    ///     Provides instance methods for the creation, copying, deletion,
    ///     moving, and opening of files, and aids in the creation of
    ///     <see cref="T:System.IO.FileStream" /> objects.
    /// </summary>
    [ComVisible(true)]
    [Serializable]
    [FileIOPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public sealed class FileInfoProxy : FileSystemInfoProxy, IResourceInfo
    {
        private readonly FileInfo _fileInfo;
        private DirectoryInfoProxy _directoryInfo;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Cush.Windows.FileSystem.FileInfoProxy" />
        ///     class, which acts as a wrapper for a file path.
        /// </summary>
        /// <param name="fileName">
        ///     The fully-qualified name of the new file, or the relative file name.
        /// </param>
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public FileInfoProxy(string fileName) : this(new FileInfo(fileName))
        {
            ThrowHelper.IfNullThenThrow(() => fileName);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:FileInfoProxy" /> class on the specified path.
        /// </summary>
        /// <param name="fileInfo">A <see cref="T:System.IO.FileInfo" /> representing the FileInfo. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="fileInfo" /> is null. </exception>
        [SecuritySafeCritical]
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        internal FileInfoProxy(FileInfo fileInfo)
        {
            ThrowHelper.IfNullThenThrow(() => fileInfo);
            _fileInfo = fileInfo;
            WrappedObject = fileInfo;
        }

        /// <summary>
        ///     Creates an instance of the wrapped parent directory.
        /// </summary>
        public DirectoryInfoProxy Directory
        {
            get
            {
                return _directoryInfo ??
                       (_directoryInfo = new DirectoryInfoProxy(Path.GetDirectoryName(_fileInfo.FullName)));
            }
        }

        ILocationInfo IResourceInfo.Location
        {
            get { return Directory; }
        }

        /// <summary>
        ///     Gets the length of a file, in bytes.
        /// </summary>
        public long Length
        {
            get { return _fileInfo.Length; }
        }

        object IResourceInfo.Create()
        {
            return Create();
        }

        /// <summary>
        ///     Moves the specified file to a new location and potentially a new file name.
        ///     The caller must have the necessary permissions (e.g., Read/Write permissions on the source and Write permission to
        ///     the destination).
        /// </summary>
        /// <param name="destination">
        ///     The path to move the file to, which can specify a different file name.
        /// </param>
        public void MoveTo(string destination)
        {
            _fileInfo.MoveTo(destination);
        }

        /// <summary>
        ///     Copies an existing file, disallowing the overwriting of an existing file.
        ///     The caller must have the necessary permissions
        ///     (e.g., Read permissions on the source and Write permission to the destination).
        /// </summary>
        /// <param name="destination">
        ///     The location to copy the file to, which can specify a different file name.
        /// </param>
        public void CopyTo(string destination)
        {
            CopyTo(destination, false);
        }

        /// <summary>
        ///     Copies an existing file, allowing the overwriting of an existing file.
        ///     The caller must have the necessary permissions
        ///     (e.g., Read permissions on the source and Write permission to the destination).
        /// </summary>
        /// <param name="destination">
        ///     The location to copy the file to, which can specify a different file name.
        /// </param>
        /// <param name="overWrite">true to allow an existing file to be overwritten, otherwise false.</param>
        public void CopyTo(string destination, bool overWrite)
        {
            _fileInfo.CopyTo(destination, overWrite);
        }

        object IResourceInfo.Open(FileMode mode, FileAccess access, FileShare share)
        {
            return Open(mode, access, share);
        }

        /// <summary>
        ///     Gets a value indicating whether the file is locked or in use.
        /// </summary>
        /// <returns>
        ///     true if the file is locked or in use; otherwise, false.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public bool IsResourceLocked
        {
            get
            {
                FileStream stream = null;

                try
                {
                    stream = Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                }
                catch (FileNotFoundException)
                {
                    return false;
                }
                catch (IOException)
                {
                    return true;
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }
                return false;
            }
        }

        /// <summary>
        ///     Creates a resource.
        /// </summary>
        public FileStream Create()
        {
            return _fileInfo.Create();
        }

        /// <summary>
        ///     Opens a file in the specified mode with read, write,
        ///     or read/write access and the specified sharing options.
        /// </summary>
        /// <param name="mode">
        ///     A <see cref="FileMode" /> constant specifying the
        ///     mode (for example, Open or Append) in which to open the file.
        /// </param>
        /// <param name="access">
        ///     A <see cref="FileAccess" /> constant specifying whether to
        ///     open the file with Read, Write, or ReadWrite access.
        /// </param>
        /// <param name="share">
        ///     A <see cref="FileShare" /> constant specifying the type of
        ///     access other FileStream objects have to this object.
        /// </param>
        public FileStream Open(FileMode mode, FileAccess access, FileShare share)
        {
            return _fileInfo.Open(mode, access, share);
        }

        /// <summary>
        ///     Returns a string that represents the current <see cref="FileInfoProxy" />.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}