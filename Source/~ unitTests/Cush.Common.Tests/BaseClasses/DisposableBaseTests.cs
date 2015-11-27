using System;
using Cush.Testing;
using Moq;
using NUnit.Framework;

namespace Cush.Common.Tests
{
    [TestFixture]
    public class DisposableBaseTests
    {
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