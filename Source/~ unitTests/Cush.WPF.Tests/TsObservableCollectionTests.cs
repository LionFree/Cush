using System.Collections.Specialized;
using System.Diagnostics;
using Cush.Common;
using NUnit.Framework;

namespace Cush.WPF.Tests
{
    [TestFixture]
    internal class TsObservableCollectionTests
    {
        internal class TestPCB : PropertyChangedBase
        {
            private string _thing;

            internal string Thing
            {
                get { return _thing; }
                set { SetProperty(ref _thing, value, () => Thing); }
            }

            [Test]
            public void Constructor_WithNoParameters()
            {
                object sut = null;
                Assert.DoesNotThrow(() => sut = new ThreadSafeObservableCollection<TestPCB>());
                var actual = sut as ThreadSafeObservableCollection<TestPCB>;
                Assert.IsNotNull(actual, "Object is null");
            }

            [Test]
            public void Test_PCB_CollectionChanged()
            {
                var collectionChanged = false;
                var sut = new ThreadSafeObservableCollection<TestPCB>();
                sut.CollectionChanged += (s, e) => collectionChanged = sut_CollectionChanged(e);
                var item = new TestPCB();

                sut.Add(item);

                Assert.IsTrue(collectionChanged);
            }

            private static bool sut_CollectionChanged(NotifyCollectionChangedEventArgs e)
            {
                Debug.WriteLine("Collection changed: " + e.NewItems.Count);
                return true;
            }

            [Test]
            public void Test_PCB_WithoutChange()
            {
                var called = false;


                var item = new TestPCB();
                item.PropertyChanged += (s, e) =>
                {
                    called = true;
                    Debug.WriteLine(e.PropertyName);
                };

                var sut = new ThreadSafeObservableCollection<TestPCB> {item};
                
                Assert.IsFalse(called);
            }

            [Test]
            public void Test_NonPCB_WithChange()
            {
                object temp = null;
                var item = 10.0d;

                Assert.DoesNotThrow(() => temp = new ThreadSafeObservableCollection<double>());
                
                var sut = temp as ThreadSafeObservableCollection<double>;
                Assert.IsNotNull(sut, "Could not cast sut to proper type.");

                Assert.DoesNotThrow(() => sut.Add(item), "Exception adding item.");
                Assert.DoesNotThrow(() => item = 4.0d, "Exception changing item.");
            }
        }
    }
}