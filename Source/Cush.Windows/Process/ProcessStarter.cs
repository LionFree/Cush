using System.Diagnostics;

namespace Cush.Windows
{
    internal sealed class ProcessStarter : IProcessStarter
    {
        public IProcess Start(string fileName)
        {
            return new ProcessProxy(Process.Start(fileName));
        }
    }
}