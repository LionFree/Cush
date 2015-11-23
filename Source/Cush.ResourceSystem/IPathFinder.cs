using System.Collections.Generic;

namespace Cush.ResourceSystem
{
    public interface IPathFinder
    {
        List<string> GetExistingPathsFromList(IList<string> possiblePaths,
            bool isLocation,
            bool mostRecent);

        string GetPathFromURI(string uriString);
        bool UriExists(string uriPath, bool isLocation);
    }
}