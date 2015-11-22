using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace Cush.ResourceSystem
{
    /// <summary>
    ///     Exposes instance methods for creating, moving, and enumerating through locations and resources.
    /// </summary>
    [ComVisible(true)]
    [SecuritySafeCritical]
    public interface IResourceSystem
    {
        /// <summary>
        ///     Gets a value indicating whether the specified location exists.
        /// </summary>
        /// <returns>
        ///     true if the specified location exists; otherwise, false.
        /// </returns>
        [SecuritySafeCritical]
        bool LocationExists(string location);

        /// <summary>
        ///     Deletes the specified location.
        /// </summary>
        [SecuritySafeCritical]
        void DeleteLocation(string location);

        /// <summary>
        ///     Moves the specified resource.
        /// </summary>
        [SecuritySafeCritical]
        void MoveResource(string resource, string newLocation);

        /// <summary>
        ///     Deletes a location, specifying whether to recursively delete child elements.
        /// </summary>
        /// <param name="location">The location to delete.</param>
        /// <param name="recursive">
        ///     true to delete this location and all child elements; otherwise, false.
        /// </param>
        [SecuritySafeCritical]
        void DeleteLocation(string location, bool recursive);

        /// <summary>
        ///     Opens the object at the specified URI using the default editor.
        /// </summary>
        /// <param name="uriString">The URI of the object to open.</param>
        /// <param name="isLocation">A value that determines whether the object to open is a location or not.</param>
        [SecuritySafeCritical]
        void OpenUri(string uriString, bool isLocation = false);

        /// <summary>
        ///     Gets a value indicating whether the specified resource exists.
        /// </summary>
        /// <returns>
        ///     true if the resource exists; otherwise, false.
        /// </returns>
        [SecuritySafeCritical]
        bool ResourceExists(string resource);

        /// <summary>
        ///     Deletes the specified resource.
        /// </summary>
        [SecuritySafeCritical]
        void DeleteResource(string resource);

        /// <summary>
        ///     Gets the length of a resource, in bytes.
        /// </summary>
        [SecuritySafeCritical]
        long ResourceLength(string resource);

        /// <summary>
        ///     Gets the time when the resource was last written to.
        /// </summary>
        [SecuritySafeCritical]
        DateTime LastWriteTime(string resource);

        /// <summary>
        ///     Searches through a list of possible resource URIs, and returns a list of URIs to existing resources.
        /// </summary>
        IList<string> GetExistingResourcesFromList(IList<string> possiblePaths);

        /// <summary>
        ///     Searches through a list of possible location URIs, and returns a list of URIs to existing locations.
        /// </summary>
        IList<string> GetExistingLocationsFromList(IList<string> possibleLocations);

        /// <summary>
        ///     Searches through a list of possible resource URIs, and returns the URI of the first existing resource found.
        /// </summary>
        string GetExistingResourceFromList(IList<string> possibleResources);

        /// <summary>
        ///     Searches through a list of possible location URIs, and returns the URI of the first existing location found.
        /// </summary>
        string GetExistingLocationFromList(IList<string> possibleLocations);

        /// <summary>
        ///     Searches through a list of possible resource URIs, and returns a list of URIs to the most recently modified
        ///     resources.
        /// </summary>
        IList<string> GetMostRecentlyModifiedResourcesFromList(IList<string> possibleResources);

        /// <summary>
        ///     Searches through a list of possible resource URIs, and returns the URI to the most recently modified resource.
        /// </summary>
        string GetMostRecentlyModifiedResourceFromList(IList<string> possiblePaths);

        /// <summary>
        ///     Searches for all resources with the given search pattern.
        /// </summary>
        /// <returns>An array of <see cref="T:IResourceInfo" />s representing any found resources.</returns>
        IResourceInfo[] GetResources(string searchPattern);

        /// <summary>
        ///     Searches for all resources with the given search pattern.
        /// </summary>
        /// <returns>An array of <see cref="T:IResourceInfo" />s representing any found resources.</returns>
        IResourceInfo[] GetResources(string searchPattern, bool mostRecent);

        /// <summary>
        ///     Searches for all locations with the given search pattern.
        /// </summary>
        /// <returns>An array of <see cref="T:ILocationInfo" />s representing any found locations.</returns>
        ILocationInfo[] GetLocations(string searchPattern);
    }

    ///// <summary>
    /////     Exposes instance methods for creating, moving, and enumerating through locations and resources.
    ///// </summary>
    //[ComVisible(true)]
    //[SecuritySafeCritical]
    //public interface IResourceSystem<out T>
    //{
    //    /// <summary>
    //    ///     Searches for all resources with the given search pattern.
    //    /// </summary>
    //    /// <returns>An array of <see cref="T:IResourceInfo" />s representing any found resources.</returns>
    //    IResourceInfo<T>[] GetResources(string searchPattern);

    //    /// <summary>
    //    ///     Searches for all resources with the given search pattern.
    //    /// </summary>
    //    /// <returns>An array of <see cref="T:IResourceInfo" />s representing any found resources.</returns>
    //    IResourceInfo<T>[] GetResources(string searchPattern, bool mostRecent);

    //}
}