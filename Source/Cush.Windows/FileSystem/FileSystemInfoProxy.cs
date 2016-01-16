using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using Cush.Common.ResourceSystem;

namespace Cush.Windows.FileSystem
{
    public abstract class FileSystemInfoProxy : MarshalByRefObject, IResourceSystemInfo
    {
        protected FileSystemInfo WrappedObject;

        /// <summary>
        ///     Gets a value indicating whether the file or directory exists.
        /// </summary>
        /// <returns>
        ///     true if the directory exists; otherwise, false.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public bool Exists
        {
            [SecuritySafeCritical] get { return WrappedObject.Exists; }
        }

        /// <summary>
        ///     Gets the name of this <see cref="T:FileSystemInfoProxy" /> instance.
        /// </summary>
        /// <returns>
        ///     The directory name.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public string Name
        {
            [SecuritySafeCritical] get { return WrappedObject.Name; }
        }

        /// <summary>
        ///     Gets or sets the attributes for the current directory.
        /// </summary>
        /// <returns>
        ///     <see cref="T:System.IO.FileAttributes" /> of the current <see cref="T:FileSystemInfoProxy" />.
        /// </returns>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified file does not exist. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified path is invalid; for example,
        ///     it is on an unmapped drive.
        /// </exception>
        /// <exception cref="T:System.Security.SecurityException">
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The caller attempts to set an invalid file attribute.
        ///     -or- The user attempts to set an attribute value but does not have write permission.
        /// </exception>
        /// <exception cref="T:System.IO.IOException">
        ///     <see cref="M:Refresh" />
        ///     cannot initialize the data.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public FileAttributes Attributes
        {
            [SecuritySafeCritical] get { return WrappedObject.Attributes; }
            [SecuritySafeCritical] set { WrappedObject.Attributes = value; }
        }

        /// <summary>
        ///     Gets the full path of the <see cref="T:FileSystemInfoProxy" />.
        /// </summary>
        /// <returns>
        ///     A string containing the full path.
        /// </returns>
        /// <exception cref="T:System.IO.PathTooLongException">The fully qualified path and file name is 260 or more characters.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        public string FullName
        {
            [SecuritySafeCritical] get { return WrappedObject.FullName; }
        }

        /// <summary>
        ///     Gets or sets the time when the current file or directory was last written to.
        /// </summary>
        /// <returns>
        ///     The time the current file or directory was last written to.
        /// </returns>
        /// <exception cref="T:System.IO.IOException">
        ///     <see cref="M:Refresh" /> cannot initialize the data.
        /// </exception>
        /// <exception cref="T:System.PlatformNotSupportedException">
        ///     The current operating system is not Microsoft Windows NT or
        ///     later.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        [ComVisible(false)]
        public DateTime LastWriteTime
        {
            [SecuritySafeCritical]
            get
            {
                WrappedObject.Refresh();
                return WrappedObject.LastWriteTime;
            }
            set { WrappedObject.LastWriteTime = value; }
        }

        /// <summary>
        ///     Gets or sets the time, in coordinated universal time (UTC), when the current file or directory was last written to.
        /// </summary>
        /// <returns>
        ///     The UTC time when the current file or directory was last written to.
        /// </returns>
        /// <exception cref="T:System.IO.IOException"><see cref="M:Refresh" /> cannot initialize the data. </exception>
        /// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        [ComVisible(false)]
        public DateTime LastWriteTimeUtc
        {
            get
            {
                WrappedObject.Refresh();
                return WrappedObject.LastWriteTimeUtc;
            }
            set { WrappedObject.LastWriteTimeUtc = value; }
        }

        /// <summary>
        ///     Gets or sets the creation time of the current file or directory.
        /// </summary>
        /// <returns>
        ///     The creation date and time of the current <see cref="T:FileSystemInfoProxy" /> object.
        /// </returns>
        /// <exception cref="T:System.IO.IOException"><see cref="M:Refresh" /> cannot initialize the data. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified path is invalid; for example, it is on an
        ///     unmapped drive.
        /// </exception>
        /// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid creation time.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public DateTime CreationTime
        {
            get { return WrappedObject.CreationTime; }
            set { WrappedObject.CreationTime = value; }
        }

        /// <summary>
        ///     Gets or sets the creation time, in coordinated universal time (UTC), of the current file or directory.
        /// </summary>
        /// <returns>
        ///     The creation date and time in UTC format of the current file or directory.
        /// </returns>
        /// <exception cref="T:System.IO.IOException"><see cref="M:Refresh" /> cannot initialize the data. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified path is invalid; for example, it is on an
        ///     unmapped drive.
        /// </exception>
        /// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        [ComVisible(false)]
        public DateTime CreationTimeUtc
        {
            [SecuritySafeCritical]
            get
            {
                WrappedObject.Refresh();
                return WrappedObject.CreationTimeUtc;
            }
            set { WrappedObject.CreationTimeUtc = value; }
        }

        /// <summary>
        ///     Gets or sets the time the current file or directory was last accessed.
        /// </summary>
        /// <returns>
        ///     The time that the current file or directory was last accessed.
        /// </returns>
        /// <exception cref="T:System.IO.IOException">
        ///     <see cref="M:Refresh" /> cannot initialize the data.
        /// </exception>
        /// <exception cref="T:System.PlatformNotSupportedException">
        ///     The current operating system is not Windows NT or later.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     The caller attempts to set an invalid access time
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public DateTime LastAccessTime
        {
            get
            {
                WrappedObject.Refresh();
                return WrappedObject.LastAccessTime;
            }
            set { WrappedObject.LastAccessTime = value; }
        }

        /// <summary>
        ///     Gets or sets the time, in coordinated universal time (UTC), that the current file or directory was last accessed.
        /// </summary>
        /// <returns>
        ///     The UTC time that the current file or directory was last accessed.
        /// </returns>
        /// <exception cref="T:System.IO.IOException"><see cref="M:Refresh" /> cannot initialize the data. </exception>
        /// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        [ComVisible(false)]
        public DateTime LastAccessTimeUtc
        {
            [SecuritySafeCritical]
            get
            {
                WrappedObject.Refresh();
                return WrappedObject.LastAccessTimeUtc;
            }
            set { WrappedObject.LastAccessTimeUtc = value; }
        }

        /// <summary>
        ///     Deletes a file or directory.
        /// </summary>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified path is invalid; for example, it is on an
        ///     unmapped drive.
        /// </exception>
        /// <exception cref="T:System.IO.IOException">
        ///     There is an open handle on the file or directory, and the operating system is
        ///     Windows XP or earlier. This open handle can result from enumerating directories and files. For more information,
        ///     see How to: Enumerate Directories and Files.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        public void Delete()
        {
            WrappedObject.Delete();
        }

        /// <summary>
        ///     Refreshes the state of the object.
        /// </summary>
        /// <exception cref="T:System.IO.IOException">A device such as a disk drive is not ready. </exception>
        /// <filterpriority>1</filterpriority>
        [SecuritySafeCritical]
        public void Refresh()
        {
            WrappedObject.Refresh();
        }

        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            if (ReferenceEquals(this, obj)) return true;

            var p = (FileSystemInfoProxy) obj;
            var same1 = (Name == p.Name);
            var same2 = (FullName == p.FullName);
            var same3 = (Exists == p.Exists);

            var same = same1 && same2 && same3;
            return same;
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap.
            {
                // pick two prime numbers
                const int seed = 7;
                var hash = 11;

                // be sure to check for nullity, etc.
                hash *= seed + (Name != null ? Name.GetHashCode() : 0);
                hash *= seed + (FullName != null ? FullName.GetHashCode() : 0);
                hash *= seed + Exists.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(FileSystemInfoProxy left, FileSystemInfoProxy right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (((object) left) == null || ((object) right) == null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(FileSystemInfoProxy left, FileSystemInfoProxy right)
        {
            return !(left == right);
        }
    }
}