using System;
using System.Collections.Generic;
using System.Linq;
using Cush.Common.Exceptions;
using Cush.Common.ResourceSystem;

namespace Cush.Windows.FileSystem
{
    internal sealed class PathFinder : IPathFinder
    {
        private readonly PathFinderHelper _helper;

        internal PathFinder(IResourceSystem fileSystem)
        {
            _helper = PathFinderHelper.GetInstance(fileSystem);
        }

        public bool UriExists(string path, bool isLocation = false)
        {
            return _helper.UriExists(path, isLocation);
        }
        
        public List<string> GetExistingPathsFromList(string basePath, IList<string> possiblePaths, bool isFolder,
            bool mostRecent)
        {
            ThrowHelper.IfNullThenThrow(() => possiblePaths);

            if (possiblePaths.Count == 0) return new List<string>();

            var output = new List<string>();

            var getter = _helper.GetExistingPaths(basePath, isFolder, mostRecent);

            foreach (var pathToFind in possiblePaths)
            {
                output.AddRange(getter(pathToFind));
            }

            if (output.Count == 0) output.Add(string.Empty);
            return output;
        }

        public string GetPathFromUri(string uriString)
        {
            Uri result;
            var success = Uri.TryCreate(uriString, UriKind.Absolute, out result);
            return success ? result.LocalPath : null;
        }

        internal abstract class PathFinderHelper
        {
            internal static PathFinderHelper GetInstance(IResourceSystem resourceSystem)
            {
                return new PfhImplementation(resourceSystem);
            }

            internal abstract Func<string, IEnumerable<string>> GetExistingPaths(string location, bool isFolder, bool mostRecent);

            internal abstract bool UriExists(string path, bool isLocation = false);
            
            private sealed class PfhImplementation : PathFinderHelper
            {
                private readonly IResourceSystem _resourceSystem;

                internal PfhImplementation(IResourceSystem resourceSystem)
                {
                    _resourceSystem = resourceSystem;
                }

                internal override Func<string, IEnumerable<string>> GetExistingPaths(string location, bool isFolder, bool mostRecent)
                {
                    if (isFolder)
                        return
                            searchPattern =>
                                _resourceSystem.GetLocations(location, searchPattern)
                                    .Select(i => i.FullName);

                    return
                        searchPattern =>
                            _resourceSystem.GetResources(location, searchPattern, mostRecent).Select(i => i.FullName);
                }

                internal override bool UriExists(string path, bool isLocation = false)
                {
                    {
                        return isLocation ? _resourceSystem.LocationExists(path)
                            : _resourceSystem.ResourceExists(path);
                    }

                }
            }
        }
    }
}