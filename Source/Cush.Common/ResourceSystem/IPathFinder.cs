using System.Collections.Generic;

namespace Cush.Common.ResourceSystem
{
    public interface IPathFinder
    {
        List<string> GetExistingPathsFromList(string basePath, IList<string> possiblePaths,
            bool isLocation,
            bool mostRecent);

        string GetPathFromUri(string uriString);
        bool UriExists(string uriPath, bool isLocation);
    }
}