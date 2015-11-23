using Cush.ResourceSystem;

namespace Cush.Windows.FileSystem
{
    internal sealed class FileSystemFactory : IResourceSystemFactory
    {
        public ILocationInfo LocationInfo(string path)
        {
            return new DirectoryInfoProxy(path);
        }

        public IResourceInfo ResourceInfo(string path)
        {
            return new FileInfoProxy(path);
        }
        
        public IPathFinder PathFinder(IResourceSystem system) 
        {
            return new PathFinder(system);
        }
    }
}