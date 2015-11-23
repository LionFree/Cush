using System.Reflection;

namespace Cush.Testing
{
    public static class AssemblyInspector
    {
        public static string GetClrRuntimeVersion(Assembly assembly)
        {
            return ImageRuntimeVersion(assembly);
        }

        public static string ImageRuntimeVersion(Assembly assembly)
        {
            return assembly.ImageRuntimeVersion;
        }
    }
}