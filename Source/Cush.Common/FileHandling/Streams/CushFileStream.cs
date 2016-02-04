using System.IO;

namespace Cush.Common.FileHandling.Streams
{
    class CushFileStream : FileStream
    {
        public CushFileStream(string path) : base(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)
        {
        }
    }
}