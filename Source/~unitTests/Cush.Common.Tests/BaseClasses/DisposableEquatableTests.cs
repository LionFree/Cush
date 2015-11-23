using System;
using System.Diagnostics.CodeAnalysis;
using Cush.Testing;
using Moq;
using NUnit.Framework;

namespace Cush.Common.Tests
{
#pragma warning disable 1718
    [TestFixture]
    [SuppressMessage("ReSharper", "EqualExpressionComparison")]
    class DisposableEquatableTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void Test_EqualsOperator_Equals(bool expected)
        {
            var item1 = new TestDisposableClass(expected,12345);
            var item2 = new TestDisposableClass(expected,12345);

            Assert.AreEqual(expected,(item1 == item2));
            Assert.AreEqual(!expected, (item1 != item2));
        }

        [Test]
        public void Test_EqualsOperator_OneNullObject()
        {
            var item1 = new TestDisposableClass();
            TestDisposableClass item2 = null;

            Assert.IsFalse(item1 == item2);
            Assert.IsTrue(item1 != item2);
        }

        [Test]
        public void Test_EqualsOperator_ReferenceEquals()
        {
            var item1 = new TestDisposableClass(true, 12345);
            Assert.IsTrue(item1 == item1);
            Assert.IsFalse(item1 != item1);
        }

        private static TestDisposableClass GetTestObject(IDisposable managed)
        {
            var pointer = new IntPtr(GetRandom.Int(25000, 65535));
            var sut = new TestDisposableClass(managed, pointer);
            Assert.IsFalse(sut.IsDisposed);
            Assert.IsNotNull(sut.ManagedResource);
            Assert.AreNotEqual(IntPtr.Zero, sut.ManagedResource);
            return sut;
        }

        [Test]
        public void Dispose_DoesNotDisposeTwice()
        {
            var inner = new Mock<IDisposable>();
            var sut = GetTestObject(inner.Object);

            sut.Dispose();

            Assert.DoesNotThrow(() => sut.Dispose());
        }

        [Test]
        public void Dispose_ExecutesBothDisposerMethods()
        {
            var inner = new Mock<IDisposable>();
            var sut = GetTestObject(inner.Object);

            sut.Dispose();

            inner.Verify(x => x.Dispose());
            Assert.AreEqual(IntPtr.Zero, sut.UnmanagedResource);
        }

        [Test]
        public void Dispose_SetsIsDisposed()
        {
            var inner = new Mock<IDisposable>();
            var sut = GetTestObject(inner.Object);

            sut.Dispose();

            Assert.IsTrue(sut.IsDisposed);
        }

        [Test]
        public void Finalizer_DoesNotDisposeOfManagedObjects()
        {
            var inner = new Mock<IDisposable>();
            var sut = GetTestObject(inner.Object);

            GC.Collect();
            GC.WaitForPendingFinalizers();

            inner.Verify(x => x.Dispose(), Times.Never);
            Assert.IsNotNull(sut.ManagedResource);
        }
    }
}
