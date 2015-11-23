using NUnit.Framework;

namespace Cush.Testing.Tests.ExtensionsTests
{
    [TestFixture]
    internal class CollectionExtensionsTests
    {
        [Test]
        public void TestNormalizedMean_OfEmptyArray()
        {
            var array = new double[] {};
            Assert.AreEqual(double.NaN, array.NormalizedMean());
        }


        [Test]
        public void TestDeviations_OfEmptyArray()
        {
            var array = new double[] { };
            var sut = array.Deviations();
            Assert.IsNotNull(sut);
            Assert.IsEmpty(sut);
        }
    }
}