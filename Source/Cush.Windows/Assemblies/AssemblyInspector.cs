using System.Reflection;

namespace Cush.Windows
{
    public static class AssemblyInspector
    {
        public static bool HasEntryAssembly
        {
            get
            {
                try
                {
                    var assembly = Assembly.GetEntryAssembly();
                    return (assembly != null);
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}