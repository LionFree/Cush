using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;

using Cush.ResourceSystem;
using Cush.Common.Exceptions;

namespace Cush.Windows.FileSystem
{
    /// <summary>
    ///     Exposes instance methods for creating, moving, and enumerating through directories and subdirectories. This class
    ///     cannot be inherited.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    [ComVisible(true)]
    [Serializable]
    public sealed class DirectoryInfoProxy : FileSystemInfoProxy, ILocationInfo
    {
        private readonly DirectoryInfo _directoryInfo;
        private readonly IObjectRetriever _retriever;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:DirectoryInfoProxy" /> class on the specified path.
        /// </summary>
        /// <param name="path">A string specifying the path on which to create the DirectoryInfo. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is an empty string. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="path" /> contains invalid characters such as ", &lt;, &gt;
        ///     , or |.
        /// </exception>
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined
        ///     maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names
        ///     must be less than 260 characters. The specified path, file name, or both are too long.
        /// </exception>
        [DebuggerStepThrough]
        [SecuritySafeCritical]
        public DirectoryInfoProxy(string path) : this(new DirectoryInfo(path), new FileSystemObjectRetriever(path))
        {
            ThrowHelper.IfNullThenThrow(() => path);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:DirectoryInfoProxy" /> class on the specified path.
        /// </summary>
        /// <param name="directoryInfo">A <see cref="T:System.IO.DirectoryInfo" /> representing the DirectoryInfo. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="directoryInfo" /> is null. </exception>
        [DebuggerStepThrough]
        [SecuritySafeCritical]
        public DirectoryInfoProxy(DirectoryInfo directoryInfo)
            : this(directoryInfo, new FileSystemObjectRetriever(directoryInfo.FullName))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:DirectoryInfoProxy" /> class on the specified path.
        /// </summary>
        /// <param name="directoryInfo">A <see cref="T:System.IO.DirectoryInfo" /> representing the DirectoryInfo. </param>
        /// <param name="retriever">A <see cref="T:ObjectRetriever" /> used to GET files or directories.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="directoryInfo" /> is null. </exception>
        [DebuggerStepThrough]
        [SecuritySafeCritical]
        internal DirectoryInfoProxy(DirectoryInfo directoryInfo, IObjectRetriever retriever)
        {
            ThrowHelper.IfNullThenThrow(() => directoryInfo);
            _directoryInfo = directoryInfo;
            WrappedObject = directoryInfo;
            _retriever = retriever;
        }

        /// <summary>
        ///     Gets the parent directory of a specified subdirectory.
        /// </summary>
        /// <returns>
        ///     The parent directory, or null if the path is null or if the file path denotes a root (such as "\", "C:", or *
        ///     "\\server\share").
        /// </returns>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public DirectoryInfoProxy Parent
        {
            get
            {
                var parent = _directoryInfo.Parent;
                ThrowHelper.IfNullThenThrow(() => parent);
                return new DirectoryInfoProxy(parent.FullName);
            }
        }

        /// <summary>
        ///     Gets the root portion of a path.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:DirectoryInfoProxy" /> object representing the root of a path.
        /// </returns>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public DirectoryInfoProxy Root
        {
            [SecuritySafeCritical]
            get
            {
                var root = _directoryInfo.Root;
                ThrowHelper.IfNullThenThrow(() => root);
                return new DirectoryInfoProxy(root.FullName);
            }
        }

        ILocationInfo ILocationInfo.Parent
        {
            [SecuritySafeCritical] get { return Parent; }
        }

        ILocationInfo ILocationInfo.Root
        {
            [SecuritySafeCritical] get { return Root; }
        }

        /// <summary>
        ///     Deletes this instance of a <see cref="T:DirectoryInfoProxy" />, specifying whether to delete subdirectories and
        ///     files.
        /// </summary>
        /// <param name="recursive">true to delete this directory, its subdirectories, and all files; otherwise, false. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The directory contains a read-only file.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The directory described by this
        ///     <see cref="T:System.IO.DirectoryInfo" /> object does not exist or could not be found.
        /// </exception>
        /// <exception cref="T:System.IO.IOException">
        ///     The directory is read-only.-or- The directory contains one or more files or
        ///     subdirectories and <paramref name="recursive" /> is false.-or-The directory is the application's current working
        ///     directory. -or-There is an open handle on the directory or on one of its files, and the operating system is Windows
        ///     XP or earlier. This open handle can result from enumerating directories and files. For more information, see How
        ///     to: Enumerate Directories and Files.
        /// </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        [SecuritySafeCritical]
        public void Delete(bool recursive)
        {
            _directoryInfo.Delete(recursive);
        }

        ILocationInfo ILocationInfo.CreateSublocation(string path)
        {
            return CreateSublocation(path);
        }

        bool IEquatable<ILocationInfo>.Equals(ILocationInfo obj)
        {
            return Equals(obj);
        }

        IResourceInfo[] ILocationInfo.GetResources(string searchPattern)
        {
            return GetResources(searchPattern).ToArray<IResourceInfo>();
        }

        ILocationInfo[] ILocationInfo.GetLocations(string searchPattern)
        {
            return GetLocations(searchPattern).ToArray<ILocationInfo>();
        }

        /// <summary>
        ///     Creates a subdirectory or subdirectories on the specified path.
        ///     The specified path can be relative to this instance of the
        ///     <see cref="T:DirectoryInfoProxy" /> class.
        /// </summary>
        /// <returns>
        ///     The last directory specified in <paramref name="path" />.
        /// </returns>
        /// <param name="path">
        ///     The specified path. This cannot be a different disk volume or
        ///     Universal Naming Convention (UNC) name.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="path" /> does not specify a valid file path or contains invalid DirectoryInfo characters.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified path is invalid, such as being on an unmapped
        ///     drive.
        /// </exception>
        /// <exception cref="T:System.IO.IOException">
        ///     The subdirectory cannot be created.-or- A file or directory already has the
        ///     name specified by <paramref name="path" />.
        /// </exception>
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined
        ///     maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names
        ///     must be less than 260 characters. The specified path, file name, or both are too long.
        /// </exception>
        /// <exception cref="T:System.Security.SecurityException">
        ///     The caller does not have code access permission to create the
        ///     directory.-or-The caller does not have code access permission to read the directory described by the returned
        ///     <see cref="T:DirectoryInfoProxy" /> object.  This can occur when the <paramref name="path" /> parameter
        ///     describes an existing directory.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="path" /> contains a colon character (:) that is not
        ///     part of a drive label ("C:\").
        /// </exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        [SecuritySafeCritical]
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public DirectoryInfoProxy CreateSublocation(string path)
        {
            ThrowHelper.IfNullThenThrow(() => path);

            if (Path.IsPathRooted(path))
                ThrowHelper.ThrowArgumentException(() => path, Strings.EXCEPTION_RootedSubdirectory);

            return new DirectoryInfoProxy(_directoryInfo.CreateSubdirectory(path));
        }

        /// <summary>
        ///     Returns an array of Files in the current <see cref="T:DirectoryInfoProxy" /> matching the
        ///     given search criteria (ie, "*.txt").
        /// </summary>
        /// <param name="searchPattern">
        ///     The search string to match against the names of files in the current directory.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="searchPattern " />contains invalid characters. To determine the invalid
        ///     characters, use the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified path is invalid (for example, it is on an
        ///     unmapped drive).
        /// </exception>
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined
        ///     maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must
        ///     be less than 260 characters.
        /// </exception>
        /// <exception cref="T:System.IO.IOException">A network error has occurred. </exception>
        /// <returns>
        ///     An array of type <see cref="T:IFileInfo" /> of files in the specified directory.
        /// </returns>
        public FileInfoProxy[] GetResources(string searchPattern = "*")
        {
            return _retriever.GetObjects<FileInfoProxy>(searchPattern);
        }

        /// <summary>
        ///     Returns an array of directories in the current
        ///     <see
        ///         cref="T:DirectoryInfoProxy" />
        ///     matching the given search criteria and using a value to determine whether to search subdirectories.
        /// </summary>
        /// <returns>
        ///     An array of type <see cref="T:DirectoryInfoProxy" /> matching <paramref name="searchPattern" />.
        /// </returns>
        /// <param name="searchPattern">
        ///     The search string, such as "System*", used to search for all directories beginning with the word "System".
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="searchPattern " />contains invalid characters. To determine the invalid characters, use the
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     method.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The path encapsulated in the
        ///     <see cref="T:DirectoryInfoProxy" /> object is invalid, such as being on an unmapped drive.
        /// </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        public DirectoryInfoProxy[] GetLocations(string searchPattern = "*")
        {
            return _retriever.GetObjects<DirectoryInfoProxy>(searchPattern);
        }
    }
}