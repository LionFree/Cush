using System;
using System.Security;

namespace Cush.Common.ResourceSystem
{
    [SecuritySafeCritical]
    public interface ILocationInfo : IResourceSystemInfo, IEquatable<ILocationInfo>
    {
        /// <summary>
        ///     Gets the parent directory of a specified subdirectory.
        /// </summary>
        /// <returns>
        ///     The parent directory, or null if the path is null or if the file path denotes a root (such as "\", "C:", or *
        ///     "\\server\share").
        /// </returns>
        ILocationInfo Parent { get; }

        /// <summary>
        ///     Gets the root portion of a path.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:IDirectoryInfo" /> object representing the root of a path.
        /// </returns>
        ILocationInfo Root { get; }

        /// <summary>
        ///     Creates a subdirectory or subdirectories on the specified path. The specified path can be relative to this instance
        ///     of the <see cref="T:IDirectoryInfo" /> class.
        /// </summary>
        /// <returns>
        ///     The last directory specified in <paramref name="path" />.
        /// </returns>
        /// <param name="path">
        ///     The specified path. This cannot be a different disk volume or Universal Naming Convention (UNC)
        ///     name.
        /// </param>
        ILocationInfo CreateSublocation(string path);

        /// <summary>
        ///     Deletes this instance of an <see cref="T:IDirectoryInfo" />, specifying whether to delete child entities.
        /// </summary>
        /// <param name="recursive">
        ///     true to delete this location and all child entities; otherwise, false.
        /// </param>
        void Delete(bool recursive);

        /// <summary>
        ///     Returns an array of locations in the current
        ///     <see
        ///         cref="T:ILocationInfo" />
        ///     matching the given search criteria and using a value to determine whether to search sublocations.
        /// </summary>
        /// <returns>
        ///     An array of type <see cref="T:ILocationInfo" /> matching <paramref name="searchPattern" />.
        /// </returns>
        /// <param name="searchPattern">
        ///     The search string, such as "System*", used to search for all locations beginning with the word "System".
        /// </param>
        ILocationInfo[] GetLocations(string searchPattern = "*");

        /// <summary>
        ///     Returns an array of resources in the current <see cref="T:ILocationInfo" /> matching the
        ///     given search criteria (ie, "*.txt").
        /// </summary>
        /// <param name="searchPattern">
        ///     The search string to match against the names of resources in the current location.
        /// </param>
        /// <returns>
        ///     An array of type <see cref="T:IResourceInfo" /> of resources in the specified location.
        /// </returns>
        IResourceInfo[] GetResources(string searchPattern = "*");
    }
}