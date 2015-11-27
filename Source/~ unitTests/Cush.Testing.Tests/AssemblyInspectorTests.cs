using System.Reflection;
using NUnit.Framework;

namespace Cush.Testing.Tests
{
    [TestFixture]
    internal class AssemblyInspectorTests
    {
        [Test]
        public void Test_GetAssemblyCLRVersion()
        {
            var assembly = Assembly.GetAssembly(typeof (AssemblyInspector));
            var expected = assembly.ImageRuntimeVersion;

            var actual = AssemblyInspector.GetClrRuntimeVersion(assembly);
            
            Assert.AreEqual(expected, actual, "Method reporting incorrect value:");
        }
    }
}