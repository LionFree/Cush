using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Cush.Testing.Tests
{
    [TestFixture]
    internal class ShuffleExtensionTests
    {
        private static T[] CopyArray<T>(T[] array)
        {
            var output = new T[array.Length];
            array.CopyTo(output, 0);
            return output;
        }

        private static IList<T> CopyList<T>(IEnumerable<T> list)
        {
            return CopyArray(list.ToArray()).ToList();
        }


        [Test]
        public void ShuffleArrayTest()
        {
            var array1 = GetRandomArray.OfStrings(10, 20);
            var array2 = CopyArray(array1);

            Assert.AreEqual(array1, array2);
            Assert.AreNotSame(array1, array2);

            array2.Shuffle();

            foreach (var item in array1)
            {
                Assert.That(array2.Contains(item));
            }

            Assert.AreNotEqual(array1, array2);
            Assert.AreNotSame(array1, array2);
        }

        [Test]
        public void ShuffleListTest()
        {
            var array1 = GetRandomArray.OfStrings(10, 20).ToList();
            var array2 = CopyList(array1);

            Assert.AreEqual(array1, array2);
            Assert.AreNotSame(array1, array2);

            array2.Shuffle();

            foreach (var item in array1)
            {
                Assert.That(array2.Contains(item));
            }

            Assert.AreNotEqual(array1, array2);
            Assert.AreNotSame(array1, array2);
        }
    }
}