using System.IO;
using System.Security;
using JetBrains.Annotations;

namespace Cush.Common.ResourceSystem
{
    /// <summary>
    ///     Provides instance methods for the creation, copying, deletion,
    ///     moving, and opening of files, and aids in the creation of objects.
    /// </summary>
    [SecuritySafeCritical]
    public interface IResourceInfo : IResourceSystemInfo
    {
        /// <summary>
        ///     Creates an instance of the wrapped parent location.
        /// </summary>
        ILocationInfo Location { get; }

        /// <summary>
        ///     Gets the length of a resource, in bytes.
        /// </summary>
        long Length { get; }

        /// <summary>
        ///     Gets a value indicating whether the resource is locked or in use.
        /// </summary>
        /// <returns>
        ///     true if the resource is locked or in use; otherwise, false.
        /// </returns>
        bool IsResourceLocked { get; }

        /// <summary>
        ///     Creates a resource.
        /// </summary>
        object Create();

        /// <summary>
        ///     Moves a given resource to a new location and potentially a new resource name.
        ///     The caller must have the necessary permissions
        ///     (e.g., Read/Write permissions on the source and Write permission to the destination).
        /// </summary>
        /// <param name="destination">
        ///     The location to move the resource to, which can specify a different resource name.
        /// </param>
        void MoveTo([NotNull] string destination);

        /// <summary>
        ///     Copies an existing resource to a new location and potentially a new resource name, disallowing the overwriting of
        ///     an existing resource.
        ///     The caller must have the necessary permissions
        ///     (e.g., Read permissions on the source and Write permission to the destination).
        /// </summary>
        /// <param name="destination">
        ///     The location to copy the resource to, which can specify a different resource name.
        /// </param>
        void CopyTo([NotNull] string destination);

        /// <summary>
        ///     Copies an existing resource to a new location and potentially a new resource name, allowing the overwriting of an
        ///     existing resource.
        ///     The caller must have the necessary permissions
        ///     (e.g., Read permissions on the source and Write permission to the destination).
        /// </summary>
        /// <param name="destination">
        ///     The location to copy the resource to, which can specify a different resource name.
        /// </param>
        /// <param name="overWrite">true to allow an existing resource to be overwritten, otherwise false.</param>
        void CopyTo([NotNull] string destination, bool overWrite);

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
        ///     access FileStream objects have to this object.
        /// </param>
        object Open(FileMode mode, FileAccess access, FileShare share);
    }
}