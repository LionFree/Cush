using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cush.Testing;
using NUnit.Framework;

namespace Cush.Common.Tests
{
    [TestFixture]
    class CollectionDebugViewTests
    {
        [Test]
        public void Test_Constructor_WithParameter()
        {
            var doubles = GetRandomArray.OfDoubles(5, 20);
            object temp = null;

            Assert.DoesNotThrow(()=>temp = new CollectionDebugView<double>(doubles));
            var sut = temp as CollectionDebugView<double>;
            Assert.IsNotNull(sut, "Cannot cast to the expected type.");
        }

        [Test]
        public void Test_Constructor_NullParameter()
        {
            object temp = null;

            Assert.Throws<ArgumentNullException>(() => temp = new CollectionDebugView<double>(null));
            var sut = temp as CollectionDebugView<double>;
            Assert.IsNull(sut, "Object should be null.");
        }

        [Test]
        public void Test_Items()
        {
            var expected = GetRandomArray.OfDoubles(5, 20);

            var sut = new CollectionDebugView<double>(expected);

            Assert.AreEqual(expected, sut.Items, "Arrays should match, but do not.");
        }
    }
}
