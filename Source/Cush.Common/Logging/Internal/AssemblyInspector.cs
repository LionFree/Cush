using System.Reflection;

namespace Cush.Common.Logging.Internal
{
    internal static class AssemblyInspector
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

    internal static class AssemblyProxy
    {
        internal static string EntryAssemblyName => Assembly.GetEntryAssembly().GetName().Name;
    }
}