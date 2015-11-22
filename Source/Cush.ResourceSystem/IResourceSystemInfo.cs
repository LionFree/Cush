using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace Cush.ResourceSystem
{
    public interface IResourceSystemInfo
    {
        /// <summary>
        ///     Gets a value indicating whether the object exists.
        /// </summary>
        /// <returns>
        ///     true if the object exists; otherwise, false.
        /// </returns>
        bool Exists { get; }

        /// <summary>
        ///     For resources, gets the name of the resource. For locations, gets the name of the last location in the hierarchy if
        ///     a hierarchy exists. Otherwise, the Name property gets the name of the location.
        /// </summary>
        /// <returns>
        ///     A string that is the name of the parent location, the name of the last location in the hierarchy, or the name of
        ///     a resource.
        /// </returns>
        string Name { get; }

        /// <summary>
        ///     Gets or sets the <see cref="T:FileAttributes"/> for the current object.
        /// </summary>
        /// <returns>
        ///     <see cref="T:System.IO.FileAttributes" /> of the current <see cref="T:IResourceSystemInfo" />.
        /// </returns>
        FileAttributes Attributes { get; set; }

        /// <summary>
        ///     Gets the full path of the object.
        /// </summary>
        /// <returns>
        ///     A string containing the full path.
        /// </returns>
        string FullName { get; }

        /// <summary>
        ///     Gets or sets the creation time of the current object.
        /// </summary>
        /// <returns>
        ///     The creation date and time of the current <see cref="T:IResourceSystemInfo" /> object.
        /// </returns>
        DateTime CreationTime { get; set; }

        /// <summary>
        ///     Gets or sets the creation time, in coordinated universal time (UTC), of the current object.
        /// </summary>
        /// <returns>
        ///     The creation date and time in UTC format of the current <see cref="T:IResourceSystemInfo" /> object.
        /// </returns>
        [ComVisible(false)]
        DateTime CreationTimeUtc { [SecuritySafeCritical] get; set; }

        /// <summary>
        ///     Gets or sets the time the current object was last accessed.
        /// </summary>
        /// <returns>
        ///     The time that the current object was last accessed.
        /// </returns>
        DateTime LastAccessTime { get; set; }

        /// <summary>
        ///     Gets or sets the time, in coordinated universal time (UTC), that the current object was last accessed.
        /// </summary>
        /// <returns>
        ///     The UTC time that the current object was last accessed.
        /// </returns>
        [ComVisible(false)]
        DateTime LastAccessTimeUtc { [SecuritySafeCritical] get; set; }

        /// <summary>
        ///     Gets or sets the time when the current object was last written to.
        /// </summary>
        /// <returns>
        ///     The time the current file was last written.
        /// </returns>
        DateTime LastWriteTime { get; set; }

        /// <summary>
        ///     Gets or sets the time, in coordinated universal time (UTC), when the current object was last written to.
        /// </summary>
        /// <returns>
        ///     The UTC time when the current file was last written to.
        /// </returns>
        [ComVisible(false)]
        DateTime LastWriteTimeUtc { [SecuritySafeCritical] get; set; }

        /// <summary>
        ///     Deletes the object.
        /// </summary>
        [SecuritySafeCritical]
        void Delete();

        /// <summary>
        ///     Refreshes the state of the object.
        /// </summary>
        [SecuritySafeCritical]
        void Refresh();
    }
}