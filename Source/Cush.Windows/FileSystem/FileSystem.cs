using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Cush.ResourceSystem;
using Cush.Common.Exceptions;

namespace Cush.Windows.FileSystem
{
    /// <summary>
    ///     Exposes instance methods for creating, moving, and enumerating through directories and subdirectories.
    ///     This class cannot be inherited.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    [ComVisible(true)]
    [Serializable]
    public sealed class FileSystem : IResourceSystem
    {
        private readonly FileSystemFactory _factory;
        private readonly string _path;
        private readonly IPathFinder _pathFinder;
        private readonly IProcessStarter _starter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:FileSystem" /> class on the specified path.
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
        public FileSystem(string path) : this(new FileSystemFactory(), new ProcessStarter(), path)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:FileSystem" /> class on the specified path, using the specified
        ///     factory.
        /// </summary>
        /// <param name="starter">The <see cref="T:IProcessStarter" /> to use to open a resource or location.</param>
        /// <param name="path">A string specifying the path on which to create the DirectoryInfo. </param>
        /// <param name="factory">The <see cref="T:IResourceSystemFactory" /> to use.</param>
        [DebuggerStepThrough]
        internal FileSystem(FileSystemFactory factory, IProcessStarter starter, string path)
        {
            ThrowHelper.IfNullThenThrow(() => factory);
            ThrowHelper.IfNullOrEmptyThenThrow(() => path);

            _factory = factory;
            _path = path;
            _pathFinder = _factory.PathFinder(this);
            _starter = starter;
        }

        /// <summary>
        ///     Gets a value indicating whether the specified directory exists.
        /// </summary>
        /// <returns>
        ///     true if the specified directory exists; otherwise, false.
        /// </returns>
        public bool LocationExists(string directory)
        {
            return _factory.LocationInfo(directory).Exists;
        }

        /// <summary>
        ///     Deletes a directory.
        /// </summary>
        /// <param name="directory">
        ///     The directory to delete.
        /// </param>
        public void DeleteLocation(string directory)
        {
            _factory.LocationInfo(directory).Delete();
        }

        /// <summary>
        ///     Moves the specified file.
        /// </summary>
        public void MoveResource(string resource, string newResource)
        {
            _factory.ResourceInfo(resource).MoveTo(newResource);
        }

        /// <summary>
        ///     Deletes a directory, specifying whether to delete subdirectories and files.
        /// </summary>
        /// <param name="directory">The directory to delete.</param>
        /// <param name="recursive">
        ///     true to delete this directory, its subdirectories, and all files; otherwise, false.
        /// </param>
        public void DeleteLocation(string directory, bool recursive)
        {
            _factory.LocationInfo(directory).Delete(recursive);
        }

        /// <summary>
        ///     Opens a file or directory using the default editor.
        /// </summary>
        /// <param name="uriPath">The path of the file or directory to open.</param>
        /// <param name="isLocation">A value that determines whether the object to open is a directory or not.</param>
        /// <returns></returns>
        public void OpenUri(string uriPath, bool isLocation = false)
        {
            ThrowHelper.IfNullOrEmptyThenThrow(() => uriPath);

            var path = _pathFinder.GetPathFromURI(uriPath);
            ThrowHelper.IfNullOrEmptyThenThrow(() => path, Strings.EXCEPTION_InvalidPath);

            var exists = _pathFinder.UriExists(path, isLocation);
            if (!exists) ThrowHelper.ThrowPathDoesNotExistException(() => path, isLocation);

            // It's a resource or location. Open it using Process.Start(string path).
            _starter.Start(path);
        }

        /// <summary>
        ///     Gets a value indicating whether the specified file exists.
        /// </summary>
        /// <returns>
        ///     true if the file exists; otherwise, false.
        /// </returns>
        public bool ResourceExists(string resource)
        {
            return _factory.ResourceInfo(resource).Exists;
        }

        /// <summary>
        ///     Deletes the specified file.
        /// </summary>
        public void DeleteResource(string resource)
        {
            _factory.ResourceInfo(resource).Delete();
        }

        /// <summary>
        ///     Gets the length of a file, in bytes.
        /// </summary>
        public long ResourceLength(string resource)
        {
            return _factory.ResourceInfo(resource).Length;
        }

        /// <summary>
        ///     Gets the time when the file was last written to.
        /// </summary>
        public DateTime LastWriteTime(string resource)
        {
            return _factory.ResourceInfo(resource).LastWriteTime;
        }

        /// <summary>
        ///     Searches through a list of possible file paths, and returns a list of paths to existing files.
        /// </summary>
        public IList<string> GetExistingResourcesFromList(IList<string> possiblePaths)
        {
            return _pathFinder.GetExistingPathsFromList(possiblePaths, false, false);
        }

        /// <summary>
        ///     Searches through a list of possible folder paths, and returns a list of paths to existing folders.
        /// </summary>
        public IList<string> GetExistingLocationsFromList(IList<string> possibleLocations)
        {
            return _pathFinder.GetExistingPathsFromList(possibleLocations, true, false);
        }

        /// <summary>
        ///     Searches through a list of possible file paths, and returns the path of the first existing file found.
        /// </summary>
        public string GetExistingResourceFromList(IList<string> possiblePaths)
        {
            var output = GetExistingResourcesFromList(possiblePaths);
            return (output.Count == 0) ? string.Empty : output[0];
        }

        /// <summary>
        ///     Searches through a list of possible folder paths, and returns the path of the first existing folder found.
        /// </summary>
        public string GetExistingLocationFromList(IList<string> possibleLocations)
        {
            var output = GetExistingLocationsFromList(possibleLocations);
            return (output.Count == 0) ? string.Empty : output[0];
        }

        /// <summary>
        ///     Searches through a list of possible file paths, and returns a list of paths to the most recently modified files.
        /// </summary>
        public IList<string> GetMostRecentlyModifiedResourcesFromList(IList<string> possiblePaths)
        {
            return _pathFinder.GetExistingPathsFromList(possiblePaths, false, true);
        }

        /// <summary>
        ///     Searches through a list of possible file paths, and returns the path to the most recently modified file.
        /// </summary>
        public string GetMostRecentlyModifiedResourceFromList(IList<string> possiblePaths)
        {
            var output = GetMostRecentlyModifiedResourcesFromList(possiblePaths);
            return (output.Count == 0) ? string.Empty : output[0];
        }

        /// <summary>
        ///     Searches for all files with the given search pattern.
        /// </summary>
        /// <returns>An array of <see cref="T:IResourceInfo" />s representing any found files.</returns>
        public IResourceInfo[] GetResources(string searchPattern)
        {
            if (string.IsNullOrEmpty(searchPattern)) searchPattern = "*";
            return _factory.LocationInfo(_path).GetResources(searchPattern);
        }

        /// <summary>
        ///     Searches for all files with the given search pattern.
        /// </summary>
        /// <returns>An array of <see cref="T:IResourceInfo" />s representing any found files.</returns>
        public IResourceInfo[] GetResources(string searchPattern, bool mostRecent)
        {
            if (string.IsNullOrEmpty(searchPattern)) searchPattern = "*";

            var list = _factory.LocationInfo(_path).GetResources(searchPattern);

            if (!mostRecent || list.Length < 2) return list;

            var newestFile = list[0];
            foreach (var item in list)
            {
                if (item.LastWriteTime > newestFile.LastWriteTime) newestFile = item;
            }
            var output = new[] {newestFile};
            return output;
        }

        /// <summary>
        ///     Searches for all folders with the given search pattern.
        /// </summary>
        /// <returns>An array of <see cref="T:ILocationInfo" />s representing any found folders.</returns>
        public ILocationInfo[] GetLocations(string searchPattern)
        {
            if (string.IsNullOrEmpty(searchPattern)) searchPattern = "*";
            return _factory.LocationInfo(_path).GetLocations(searchPattern);
        }
    }
}