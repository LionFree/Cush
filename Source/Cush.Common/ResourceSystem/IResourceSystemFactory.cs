namespace Cush.Common.ResourceSystem
{
    public interface IResourceSystemFactory
    {
        IResourceInfo ResourceInfo(string path);
        ILocationInfo LocationInfo(string path);
    }
}