using System.IO;
using Cush.Testing;
using Cush.TestHarness.WPF.Model;

namespace Cush.TestHarness.WPF.Infrastructure
{
    internal class RandomMethods
    {
        internal static Patient RandomPatient()
        {
            var obj = new Patient
            {
                Name = Path.GetRandomFileName(),
                Identifier = GetRandom.Int().ToString()
            };
            return obj;
        }
    }
}