using Cush.Common.ResourceSystem;

namespace Cush.Windows.FileSystem
{
    public interface IObjectRetriever
    {
        TOut[] GetObjects<TOut>(string searchPattern = "*") where TOut : IResourceSystemInfo;
    }
}