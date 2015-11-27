using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cush.Testing;
using NUnit.Framework;

using ThrowHelper = Cush.Common.Exceptions.ThrowHelper;

namespace Cush.Common.Tests
{
    [TestFixture]
    public class BoundedListTests
    {
        [Test]
        public void Test_Constructor_With_NoParameters()
        {
            object temp = null;
            Assert.DoesNotThrow(() => temp = new BoundedList<double>());
            Assert.IsNotNull(temp, "BoundedList constructor returned null object.");
            var sut = temp as BoundedList<double>;
            Assert.IsNotNull(sut, "BoundedList constructor returned wrong type.");
        }

        [TestCase(1000)]
        public void Test_Constructor_With_CapacityParameter(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var capacity = GetRandom.Int(1, 100);
                object temp = null;

                Assert.DoesNotThrow(() => temp = new BoundedList<double>(capacity));
                Assert.IsNotNull(temp, "BoundedList constructor returned null object.");
                var sut = temp as BoundedList<double>;
                Assert.IsNotNull(sut, "BoundedList constructor returned wrong type.");
                Assert.AreEqual(capacity, sut.Capacity, "Capacity should be {0}, but is {1}.", capacity, sut.Capacity);
            }
        }

        [Test]
        public void Test_Constructor_With_ZeroCapacity()
        {
            object temp = null;
            var enumerable = GetRandomArray.OfDoubles(5, 10).ToArray();
            Assert.Throws<ArgumentOutOfRangeException>(() => temp = new BoundedList<double>(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => temp = new BoundedList<double>(enumerable, 0));
            Assert.IsNull(temp);
        }

        [TestCase(1000)]
        public void Test_Constructor_With_ICollectionParameter(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                // ICollection is a superset of IEnumerable
                object temp = null;
                var enumerable = GetRandomArray.OfDoubles().ToList();

                Assert.DoesNotThrow(() => temp = new BoundedList<double>(enumerable));


                Assert.IsNotNull(temp, "BoundedList constructor returned null object.");
                var sut = temp as BoundedList<double>;
                Assert.IsNotNull(sut, "BoundedList constructor returned wrong type.");
                Assert.AreEqual(Math.Max(enumerable.Count, 1), sut.Capacity,
                    "i = {2} : Capacity should be {0}, but is {1}.",
                    enumerable.Count, sut.Capacity, i);

                foreach (var expected in enumerable)
                {
                    Assert.Contains(expected, sut, "BoundedList should contain '{0}', but does not.", expected);
                }
            }
        }
        
        [Test]
        public void Test_Constructor_With_nullObject()
        {
            object temp = null;
            Assert.Throws<ArgumentNullException>(() => temp = new BoundedList<double>(null, 1));
            Assert.IsNull(temp, "Exception thrown, but constructed object is not null");
        }

        [Test]
        public void Test_Constructor_With_EmptyEnumerable()
        {
            var enumerable = new List<double>();

            object temp = null;
            Assert.DoesNotThrow(() => temp = new BoundedList<double>(enumerable));
            Assert.IsNotNull(temp, "BoundedList constructor returned null object.");
            var sut = temp as BoundedList<double>;
            Assert.IsNotNull(sut, "BoundedList constructor returned wrong type.");
            Assert.AreEqual(1, sut.Capacity, "Capacity should be 1, but is {0}.", sut.Capacity);
        }

        [Test]
        public void Test_Constructor_With_BadCapacity()
        {
            var enumerable = GetRandomArray.OfDoubles(5, 10);

            object temp = null;
            Assert.Throws<ArgumentOutOfRangeException>(() => temp = new BoundedList<double>(enumerable, 4));
            Assert.IsNull(temp, "Exception thrown, but constructed object is not null");
        }
        

        [Test]
        public void Test_Capacity_With_OriginalValue()
        {
            var expected = GetRandom.Int(1, 20);
            object actual = null;
            var sut = new BoundedList<double>(expected);

            Assert.DoesNotThrow(() => actual = sut.Capacity);
            Assert.That(actual is int);
            Assert.AreEqual(expected, (int) actual, "Capacity should be {0}, but is {1}.", expected, actual);
        }

        [Test]
        public void Test_Capacity_With_SameValue()
        {
            var expected = GetRandom.Int(1, 20);
            object actual = null;
            var sut = new BoundedList<double>(expected);

            Assert.DoesNotThrow(() => actual = sut.Capacity);
            Assert.That(actual is int);
            Assert.AreEqual(expected, (int) actual, "Capacity should be {0}, but is {1}.", expected, actual);

            // Same value
            actual = null;
            Assert.DoesNotThrow(() => sut.Capacity = expected);
            Assert.DoesNotThrow(() => actual = sut.Capacity);
            Assert.That(actual is int);
            Assert.AreEqual(expected, (int) actual, "Capacity should be {0}, but is {1}.", expected, actual);
        }

        [TestCase(1000)]
        public void Test_Capacity_WhenExpanding(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var expected1 = GetRandom.Int(1, 10);
                var expected2 = NewRandom.Int(expected1, expected1 + 1, 20);
                object actual = null;
                var sut = new BoundedList<double>(expected1);

                Assert.DoesNotThrow(() => actual = sut.Capacity);
                Assert.That(actual is int);
                Assert.AreEqual(expected1, (int) actual, "Capacity should be {0}, but is {1}.", expected1, actual);

                // New value
                actual = null;
                Assert.DoesNotThrow(() => sut.Capacity = expected2);
                Assert.DoesNotThrow(() => actual = sut.Capacity);
                Assert.That(actual is int);
                Assert.AreEqual(expected2, (int) actual, "Capacity should be {0}, but is {1}.", expected2, actual);
            }
        }

        [TestCase(1000)]
        public void Test_Capacity_WhenContracting(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var expected1 = GetRandom.Int(1, 10);
                var expected2 = NewRandom.Int(expected1, expected1 + 1, 20);
                object actual = null;
                var sut = new BoundedList<double>(expected2);
                Assert.DoesNotThrow(() => actual = sut.Capacity);
                Assert.That(actual is int);
                Assert.AreEqual(expected2, (int)actual, "Capacity should be {0}, but is {1}.", expected1, actual);

                Assert.Throws<ArgumentOutOfRangeException>(() => sut.Capacity = expected1, 
                    "Capacity should throw exception when trying to set capacity less than current size.");
            }
        }


        [Test]
        public void Test_IList_IsFixedSize()
        {
            object actual = null;
            var sut = new BoundedList<double>();

            Assert.DoesNotThrow(() => actual = ((IList)sut).IsFixedSize);
            Assert.That(actual is bool);
            Assert.IsFalse((bool)actual);
        }

        [Test]
        public void Test_IList_IsReadOnly()
        {
            object actual = null;
            var sut = new BoundedList<double>();

            Assert.DoesNotThrow(() => actual = ((IList)sut).IsReadOnly);
            Assert.That(actual is bool);
            Assert.IsFalse((bool)actual);
        }

        [Test]
        public void Test_ICollection_IsSynchronized()
        {
            object actual = null;
            var sut = new BoundedList<double>();

            Assert.DoesNotThrow(() => actual = ((ICollection)sut).IsSynchronized);
            Assert.That(actual is bool);
            Assert.IsFalse((bool)actual);
        }

        [Test]
        public void Test_ICollection_SyncRoot()
        {
            object actual = null;
            var sut = new BoundedList<double>();

            Assert.DoesNotThrow(() => actual = ((ICollection)sut).SyncRoot);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void Test_Count()
        {
            var array = GetRandomArray.OfDoubles(1, 1000);
            var expected = array.Count();
            object actual = null;

            var sut = new BoundedList<double>(array);

            Assert.DoesNotThrow(() => actual = sut.Count);
            Assert.That(actual is int);
            Assert.AreEqual(expected, (int) actual, "Count should be {0}, but is {1}.", expected, actual);
        }

        [TestCase(1000)]
        public void Test_Count_AfterAddingUnderCapacity(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var array = GetRandomArray.OfDoubles(5, 10);
                var expected = array.Count();
                object actual = null;

                var sut = new BoundedList<double>(array, 1000) { GetRandom.Double() };
                
                Assert.DoesNotThrow(() => actual = sut.Count);
                Assert.That(actual is int);
                Assert.AreEqual(expected + 1, (int) actual,
                    "Count did not increment when an item was added: should be {0}, but is {1}.", expected + 1, actual);
            }
        }

        [Test]
        public void Test_Count_AfterAddingAtCapacity()
        {
            var array = GetRandomArray.OfDoubles(5, 10);
            var expected = array.Count();
            object actual = null;

            var sut = new BoundedList<double>(array) {GetRandom.Double()};

            Assert.DoesNotThrow(() => actual = sut.Count);
            Assert.That(actual is int);
            Assert.AreEqual(expected, (int) actual,
                "Count did not increment when an item was added: should be {0}, but is {1}.", expected, actual);
        }

        [Test]
        public void Test_Items_AfterAddingAtCapacity()
        {
            object actual = null;
            var array = GetRandomArray.OfDoubles(5, 10);
            var expectedSize = array.Count();
            var newItem = GetRandom.Double();
            var expected = new double[expectedSize];
            for (var i = 0; i < array.Length - 1; i++)
            {
                expected[i] = array[i + 1];
            }
            expected[expected.Length - 1] = newItem;
            var sut = new BoundedList<double>(array) {newItem};
            
            Assert.DoesNotThrow(() => actual = sut.Count);
            Assert.That(actual is int);
            Assert.AreEqual(expectedSize, (int)actual,
                "Count did not increment when an item was added: should be {0}, but is {1}.", expected, actual);

            for (var i = 0; i < sut.Count; i++)
            {
                Assert.AreEqual(expected[i], sut[i], "Items not correct.");
            }
        }

        [TestCase(1000)]
        public void Test_Items_AfterAddingUnderCapacity(int reps)
        {
            for (var j = 0; j < reps; j++)
            {
                object actual = null;
                var array = GetRandomArray.OfDoubles(5, 10);
                var expectedSize = array.Length + 1;
                var newItem = GetRandom.Double();
                var expected = new double[expectedSize];
                for (var i = 0; i < array.Length; i++)
                {
                    expected[i] = array[i];
                }
                expected[array.Length] = newItem;

                var sut = new BoundedList<double>(array, 20) {newItem};

                Assert.DoesNotThrow(() => actual = sut.Count);
                Assert.That(actual is int);
                Assert.AreEqual(expectedSize, (int) actual,
                    "Count did not increment when an item was added: should be {0}, but is {1}.", expectedSize, actual);

                for (var i = 0; i < sut.Count; i++)
                {
                    Assert.AreEqual(expected[i], sut[i], "Item({0}) not correct.", i);
                }
            }
        }

        [Test]
        public void Test_IsReadOnly()
        {
            var sut = new BoundedList<double>();
            var ro = (ICollection<double>)sut;
            Assert.IsFalse(ro.IsReadOnly);
        }

        [Test]
        public void Test_Item_Setter()
        {
            var array = GetRandomArray.OfDoubles(20, 30);
            var target = GetRandom.Int(0, array.Length);
            var expected = GetRandom.Double();
            var sut = new BoundedList<double>(array);
            

            Assert.DoesNotThrow(() => sut[target] = expected);
            Assert.AreEqual(expected, sut[target], "Setter didn't set value.");
        }


        [Test]
        public void Test_IList_Item_Setter()
        {
            var array = GetRandomArray.OfDoubles(20, 30);
            var target = GetRandom.Int(0, array.Length);
            var expected = GetRandom.Double();
            var sut = new BoundedList<double>(array);


            Assert.DoesNotThrow(() => ((IList) sut)[target] = expected);
            Assert.AreEqual(expected, ((IList) sut)[target], "Setter didn't set value.");
        }

        [Test]
        public void Test_IList_Item_Setter_with_InvalidCast()
        {
            var array = GetRandomArray.OfDoubles(20, 30);
            var target = GetRandom.Int(0, array.Length);
            var item = GetRandom.String();
            var sut = new BoundedList<double>(array);
            var expected = sut.ToArray();


            Assert.Throws<ArgumentException>(() => ((IList)sut)[target] = item);
            Assert.AreEqual(expected, sut.ToArray(), "Setter set value, but shouldn't have.");
        }

        [Test]
        public void Test_Add_nullObject()
        {
            var sut = new BoundedList<object>(10);
            Assert.Throws<ArgumentNullException>(() => sut.Add(null));         
        }

        [Test]
        public void Test_Add_ExistingItem()
        {
            var item = GetRandom.Double();
            object actualLocation = null;
            object actualCount = null;

            var sut = new BoundedList<double>(new[] {item});

            Assert.DoesNotThrow(() => actualLocation = sut.Add(item));
            Assert.DoesNotThrow(() => actualCount = sut.Count);

            Assert.IsNotNull(actualLocation, "No exception was thrown, but method returned null value.");
            Assert.AreEqual(0, actualLocation, "Should have returned 0, but returned {0}.", actualLocation);
            Assert.AreEqual(1, actualCount, "Count should be 1, but is {0}.", actualCount);
        }

        [Test]
        public void Test_Add_NewItem()
        {
            var item = GetRandom.Double();
            var sut = new BoundedList<double>();
            object actualLocation = null;
            object actualCount = null;

            Assert.DoesNotThrow(() => actualLocation = sut.Add(item));
            Assert.DoesNotThrow(() => actualCount = sut.Count);

            Assert.IsNotNull(actualLocation, "No exception was thrown, but method returned null value.");
            Assert.IsNotNull(actualCount, "No exception was thrown, but property returned null value.");
            Assert.AreEqual(0, actualLocation, "Should have returned 0, but returned {0}.", actualLocation);
            Assert.AreEqual(1, actualCount, "Count should be 1, but is {0}.", actualCount);
            Assert.AreEqual(item, sut[0], "Item wasn't added correctly.");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Test_Contains(bool shouldContain)
        {
            double item;
            object actual = null;
            var sut = new BoundedList<double>();
            var array = sut.ToArray();
            do
            {
                item = GetRandom.Double();
            } while (array.Contains(item));

            if (shouldContain) sut.Add(item);

            Assert.DoesNotThrow(() => actual = sut.Contains(item));
            Assert.IsNotNull(actual);
            Assert.That(actual is bool);
            Assert.AreEqual(shouldContain, (bool) actual, "List should{0} contain the item, but it does{1}.",
                shouldContain ? "" : " not", shouldContain ? " not" : "");
        }


        [TestCase(true)]
        [TestCase(false)]
        public void Test_IList_Contains(bool shouldContain)
        {
            double item;
            object actual = null;
            var sut = new BoundedList<double>();
            var array = sut.ToArray();
            do
            {
                item = GetRandom.Double();
            } while (array.Contains(item));

            if (shouldContain) sut.Add(item);

            Assert.DoesNotThrow(() => actual = ((IList) sut).Contains(item));
            Assert.IsNotNull(actual);
            Assert.That(actual is bool);
            Assert.AreEqual(shouldContain, (bool)actual, "List should{0} contain the item, but it does{1}.",
                shouldContain ? "" : " not", shouldContain ? " not" : "");
        }


        [Test]
        public void Test_ICollectionAdd()
        {
            var item = GetRandom.Double();
            var sut = new BoundedList<double>();
            object actualCount = null;

            Assert.DoesNotThrow(() => ((ICollection<double>)sut).Add(item));
            Assert.DoesNotThrow(() => actualCount = sut.Count);

            Assert.IsNotNull(actualCount, "No exception was thrown, but property returned null value.");
            Assert.AreEqual(1, actualCount, "Count should be 1, but is {0}.", actualCount);
            Assert.AreEqual(item, sut[0], "Item wasn't added correctly.");
        }


        [Test]
        public void Test_AsReadOnly()
        {

            var array = GetRandomArray.OfDoubles(5, 10).ToList();
            var expected = array.AsReadOnly();
            object actual = null;

            var sut = new BoundedList<double>(array);

            Assert.DoesNotThrow(() => actual = sut.AsReadOnly());
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_GetEnumerator()
        {
            var array = GetRandomArray.OfDoubles(5, 10).ToList();
            object actual = null;
            var sut = new BoundedList<double>(array);
            var expected = new BoundedList<double>.Enumerator(sut);

            Assert.DoesNotThrow(() => actual = sut.GetEnumerator());
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_IEnumerableEnumerator()
        {
            var array = GetRandomArray.OfDoubles(5, 10).ToList();
            object actual = null;
            var sut = new BoundedList<double>(array);
            var expected = new BoundedList<double>.Enumerator(sut);

            Assert.DoesNotThrow(() => actual = ((IEnumerable<double>) sut).GetEnumerator());
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Test_CopyTo()
        {
            var list = GetRandomArray.OfDoubles(20, 20).ToList();
            var sut = new BoundedList<double>(list);

            TestCopyTo(list, sut.CopyTo);
        }

        [Test]
        public void Test_IList_CopyTo()
        {
            var list = GetRandomArray.OfDoubles(20, 20).ToList();
            var sut = new BoundedList<double>(list);
            
            TestCopyTo(list, ((IList)sut).CopyTo);
        }

        private static void TestCopyTo<T>(ICollection<T> collection, Action<T[],int> action)
        {
            var totalCount = collection.Count;
            var copyToIndex = GetRandom.Int(totalCount);
            var expected = new T[totalCount + copyToIndex];
            var actual = new T[totalCount + copyToIndex];
            collection.CopyTo(expected, copyToIndex);

            Assert.DoesNotThrow(() => action.Invoke(actual, copyToIndex));
            Assert.AreEqual(expected, actual);
        }



        [Test]
        public void Test_IndexOf()
        {
            var array = GetRandomArray.OfDoubles(10, 20).ToList();
            var expected = GetRandom.Int(5, 10);
            var item = array[expected];
            object actual = null;
            var sut = new BoundedList<double>(array);
            
            Assert.DoesNotThrow(() => actual = sut.IndexOf(item));
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Clear()
        {
            var array = GetRandomArray.OfDoubles(10, 20).ToList();
            var sut = new BoundedList<double>(array);
            var expectedCapacity = sut.Capacity;
            object actualCount = null;
            object actualCapacity = null;

            Assert.DoesNotThrow(() => sut.Clear());
            Assert.DoesNotThrow(() => actualCount = sut.Count);
            Assert.DoesNotThrow(() => actualCapacity = sut.Capacity);
            Assert.AreEqual(0, actualCount, "Count should be zero, but is {0}.", actualCount);
            Assert.AreEqual(expectedCapacity, actualCapacity, "Capacity should not have changed.");
        }

        [Test]
        public void Test_ToArray()
        {
            var array = GetRandomArray.OfDoubles(10, 20).ToList();
            var expected = array.ToArray();
            object actual = null;
            
            var sut = new BoundedList<double>(array);
            
            Assert.DoesNotThrow(() => actual = sut.ToArray());
            Assert.AreEqual(expected, actual, "Returned object is wrong.");
        }


        [Test]
        public void Test_Remove_ItemFromList()
        {
            object actual = null;
            BoundedList<double> sut;
            double item;
            do
            {
                sut = new BoundedList<double>().Populated();
                var index = GetRandom.Int(0, sut.Count);
                item = sut[index];
            } while (!sut.ContainsOnlyOne(item));
            
            
            Assert.That(sut.ContainsOnlyOne(item));

            Assert.DoesNotThrow(() => actual = sut.Remove(item));
            Assert.IsFalse(sut.Contains(item));
            Assert.IsNotNull(actual);
            Assert.IsTrue((bool)actual);
        }

        [Test]
        public void Test_Remove_ItemThatDoesNotExist()
        {
            object actual = null;
            BoundedList<double> sut;
            var item = GetRandom.Double();
            do
            {
                sut = new BoundedList<double>().Populated();
            } while (sut.Contains(item));

            Assert.DoesNotThrow(() => actual = sut.Remove(item));
            Assert.IsFalse(sut.Contains(item));
            Assert.IsNotNull(actual);
            Assert.IsFalse((bool)actual);
        }

        [Test]
        public void Test_Remove_nullObject_ThrowsException()
        {
            var sut = new BoundedList<string>().Populated();
            Assert.Throws<ArgumentNullException>(() => sut.Remove(null));
        }





        [Test]
        public void Test_IList_Remove_IncompatibleType()
        {
            var sut = new BoundedList<double>().Populated();
            var expected = sut.ToArray();
            object item = "Robble";

            Assert.DoesNotThrow(() => ((IList)sut).Remove(item));
            Assert.AreEqual(expected, sut.ToArray(),"Array changed when it should not have.");
        }


        [Test]
        public void Test_IList_Remove_ItemFromList()
        {
            BoundedList<double> sut;
            double item;
            do
            {
                sut = new BoundedList<double>().Populated();
                var index = GetRandom.Int(0, sut.Count);
                item = sut[index];
            } while (!sut.ContainsOnlyOne(item));


            Assert.That(sut.ContainsOnlyOne(item));

            Assert.DoesNotThrow(() => ((IList)sut).Remove(item));
            Assert.IsFalse(sut.Contains(item));
        }

        [Test]
        public void Test_IList_Remove_ItemThatDoesNotExist()
        {
            BoundedList<double> sut;
            var item = GetRandom.Double();
            do
            {
                sut = new BoundedList<double>().Populated();
            } while (sut.Contains(item));

            Assert.DoesNotThrow(() => ((IList)sut).Remove(item));
            Assert.IsFalse(sut.Contains(item));
        }

        [Test]
        public void Test_IList_Remove_nullObject_ThrowsException()
        {
            var sut = new BoundedList<string>().Populated();
            Assert.Throws<ArgumentNullException>(() => ((IList)sut).Remove(null));
        }




        
        [TestCase(true)]
        [TestCase(false)]
        public void Test_IList_Insert(bool isAtCapacity)
        {
            var item = GetRandom.Double();
            var sut = new BoundedList<double>().Populated();
            if (isAtCapacity) sut.Capacity = 50;
            var count = sut.Count;

            for (var i = 0; i < count; i++)
            {
                var expected = sut.ConvertToListAndInsertItem(item, i);
                
                Assert.DoesNotThrow(() => ((IList)sut).Insert(i, item));
                
                var actual = sut.ToArray();
                Assert.AreEqual(expected, actual, "Arrays do not match. i={0}", i);
            }
        }

        [Test]
        public void Test_IList_Insert_nullObject()
        {
            var sut = new BoundedList<double>().Populated();
            var expected2 = sut.ToArray();
            
            Assert.Throws<ArgumentNullException>(() => ((IList)sut).Insert(1, _nullObject));
            
            var actual2 = sut.ToArray();
            Assert.AreEqual(expected2, actual2, "Array changed after adding null object. i={0}");
        }

        [Test]
        public void Test_IList_Insert_badType()
        {
            object item = "robble";
            var sut = new BoundedList<double>().Populated();
            var expected2 = sut.ToArray();

            Assert.Throws<ArgumentException>(() => ((IList)sut).Insert(1, item));
            
            var actual2 = sut.ToArray();
            Assert.AreEqual(expected2, actual2, "Array changed after adding null object. i={0}");
        }
       
        private readonly object _nullObject = null;



        [Test]
        public void Test_AddRange_nullObject()
        {
            var sut = new BoundedList<object>(10);
            Assert.Throws<NullReferenceException>(() => sut.AddRange(null));
        }

        
        [TestCase(false)]//"Add a range such that the new list does not exceed capacity.")
        [TestCase(true)] //"Add a range such that the new list DOES exceed capacity."
        public void Test_AddRange(bool shouldOverflowCapacity)
        {
            object actual = null;
            var sut = new BoundedList<double>().Populated();
            if (shouldOverflowCapacity) sut.Capacity = 50;
            var range = GetRandomArray.OfDoubles(5, 10);

            var expected = sut.ConvertToListAndAddRange(range);

            Assert.DoesNotThrow(() => sut.AddRange(range));
            Assert.DoesNotThrow(() => actual = sut.ToArray());
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual, "Method returned incorrect results.");
        }

        [Test]
        public void Test_RemoveAt_Golden()
        {
            var index = GetRandom.Int(10);
            var sut = new BoundedList<double>().Populated(10);
            var copy = sut.ToList();
            copy.RemoveAt(index);
            var expected = copy.ToList();

            Assert.DoesNotThrow(() => sut.RemoveAt(index));
            var actual = sut.ToList();
            Assert.AreEqual(expected, actual, "Method returned incorrect results.");
        }



        [Test]
        public void Test_IList_Add_nullObject()
        {
            var sut = new BoundedList<object>(10);
            Assert.Throws<ArgumentNullException>(() => ((IList)sut).Add(null));
        }

        [Test]
        public void Test_IList_Add_ExistingItem()
        {
            var item = GetRandom.Double();
            object actualLocation = null;
            object actualCount = null;

            var sut = new BoundedList<double>(new[] { item });

            Assert.DoesNotThrow(() => actualLocation = ((IList)sut).Add(item));
            Assert.DoesNotThrow(() => actualCount = ((IList)sut).Count);

            Assert.IsNotNull(actualLocation, "No exception was thrown, but method returned null value.");
            Assert.AreEqual(0, actualLocation, "Should have returned 0, but returned {0}.", actualLocation);
            Assert.AreEqual(1, actualCount, "Count should be 1, but is {0}.", actualCount);
        }

        [Test]
        public void Test_IList_Add_NewItem()
        {
            var item = GetRandom.Double();
            var sut = new BoundedList<double>();
            object actualLocation = null;
            object actualCount = null;

            Assert.DoesNotThrow(() => actualLocation = ((IList)sut).Add(item));
            Assert.DoesNotThrow(() => actualCount = ((IList)sut).Count);

            Assert.IsNotNull(actualLocation, "No exception was thrown, but method returned null value.");
            Assert.IsNotNull(actualCount, "No exception was thrown, but property returned null value.");
            Assert.AreEqual(0, actualLocation, "Should have returned 0, but returned {0}.", actualLocation);
            Assert.AreEqual(1, actualCount, "Count should be 1, but is {0}.", actualCount);
            Assert.AreEqual(item, sut[0], "Item wasn't added correctly.");
        }

        [TestCase(1)]
        public void Test_IList_Add_BadItem(int reps)
        {
            var str = GetRandom.String();

            for (var i = 0; i < reps; i++)
            {
                var sut = new BoundedList<double>();
                object actualLocation = null;
                object actualCount = null;
                var initialCount = sut.Count;

                Assert.Throws<ArgumentException>(() => actualLocation = ((IList)sut).Add(str));
                Assert.IsNull(actualLocation, "Exception was thrown, but method returned a value.");
                
                Assert.DoesNotThrow(() => actualCount = ((IList) sut).Count);
                Assert.AreEqual(initialCount, actualCount, "Count should be {0}, but is {1}.", initialCount, actualCount);
            }
        }

        [Test]
        public void Test_IndexOf_With_ExistingItem()
        {
            var sut = new BoundedList<double>().Populated(50);
            var expected = GetRandom.Int(50);
            var item = sut[expected];

            Test_IndexOf(sut.IndexOf, item, expected);
        }

        [Test]
        public void Test_IList_IndexOf_With_ExistingItem()
        {
            var sut = new BoundedList<double>().Populated(50);
            var expected = GetRandom.Int(50);
            var item = sut[expected];

            Test_IndexOf<object>(((IList)sut).IndexOf, item, expected);
        }

        private void Test_IndexOf<T>(Func<T, int> func, T item, int expected)
        {
            object actual = null;
            
            Assert.DoesNotThrow(() => actual = func.Invoke(item));
            Assert.That(actual is int);
            Assert.AreEqual(expected, (int)actual, "Index should be {0}, but is {1}.", expected, actual);
        }

        [Test]
        public void Test_IndexOf_WithIndex()
        {
            object actual = null;
            var sut = new BoundedList<double>().Populated(500);
            var expected = GetRandom.Int(300, 500);
            var item = sut[expected];

            Assert.DoesNotThrow(() => actual = sut.IndexOf(item, 300));
            Assert.That(actual is int);
            Assert.AreEqual(expected, (int)actual, "Index should be {0}, but is {1}.", expected, actual);
        }

        [Test]
        public void Test_IndexOf_WithBadIndex()
        {
            object actual = null;
            var sut = new BoundedList<double>().Populated(500);
            var badIndex = GetRandom.Int(501, 1000);
            var item = sut[GetRandom.Int(300, 500)];

            Assert.Throws<ArgumentOutOfRangeException>(() => actual = sut.IndexOf(item, badIndex));
            Assert.IsNull(actual);
        }

        [Test]
        public void Test_IndexOf_UsingIndexBeyondIndexOfItem()
        {
            object actual = null;
            var sut = new BoundedList<double>().Populated(500);
            var expected = GetRandom.Int(100);
            var item = sut[expected];

            Assert.DoesNotThrow(() => actual = sut.IndexOf(item, 300));
            Assert.That(actual is int);
            Assert.AreEqual(-1, (int)actual, "Index should be {0}, but is {1}.", expected, actual);
        }
        
        [Test]
        public void Test_IndexOf_WithIndexAndBadCount()
        {
            object actual = null;
            var sut = new BoundedList<double>().Populated(500);
            var expected = GetRandom.Int(300, 500);
            var item = sut[expected];

            Assert.Throws<ArgumentOutOfRangeException>(() => actual = sut.IndexOf(item, 300, 5000));
            Assert.IsNull(actual);
        }

        [TestCase(1000)]
        public void Test_IndexOf_WithIndexAndGoodCount(int reps)
        {
            object actual = null;
            for (var i = 0; i < reps; i++)
            {
                var sut = new BoundedList<double>().Populated(500);
                var expected = GetRandom.Int(301, 500);
                var item = sut[expected];

                Assert.DoesNotThrow(() => actual = sut.IndexOf(item, 300, expected - 299));
                Assert.That(actual is int);
                Assert.AreEqual(expected, (int) actual, "Index should be {0}, but is {1}.", expected, actual);
            }
        }

        [TestCase(1000)]
        public void Test_IndexOf_WithIndexAndBadRange(int reps)
        {
            object actual = null;
            for (var i = 0; i < reps; i++)
            {
                var sut = new BoundedList<double>().Populated(500);
                var expected = GetRandom.Int(301, 500);
                var item = sut[expected];

                Assert.DoesNotThrow(() => actual = sut.IndexOf(item, 300, expected - 301));
                Assert.That(actual is int);
                Assert.AreEqual(-1, (int)actual, "Index should be {0}, but is {1}.", expected, actual);
            }
        }


        [Test]
        public void Test_ToList()
        {
            object actual = null;
            var sut = new BoundedList<string>().Populated(500);
            var expected = sut.ToArray().ToList();

            Assert.DoesNotThrow(() => actual = sut.ToList());
            Assert.AreEqual(expected, actual, "Lists are not identical.");
        }

        [Test]
        public void Test_Sort()
        {
            var sut = new BoundedList<string>().Populated(500);
            var expected = sut.ToList();
            expected.Sort();
                
            Assert.DoesNotThrow(() => sut.Sort());
            var actual = sut.ToList();
            Assert.AreEqual(expected, actual, "Lists are not identical.");
            
        }

        [Test]
        public void Test_Sort_WithComparison()
        {
            var sut = new BoundedList<string>().Populated(500);
            var expected = sut.ToList();
            expected.Sort(CompareDinosByLength);

            Assert.DoesNotThrow(() => sut.Sort(CompareDinosByLength));
            var actual = sut.ToList();
            Assert.AreEqual(expected, actual, "Lists are not identical.");
        }

        [Test]
        public void Test_Sort_WithIComparer()
        {
            var sut = new BoundedList<string>().Populated(500);
            var expected = sut.ToList();
            expected.Sort(new DinoComparer());

            Assert.DoesNotThrow(() => sut.Sort(new DinoComparer()));
            var actual = sut.ToList();
            Assert.AreEqual(expected, actual, "Lists are not identical.");
        }

        [Test]
        public void Test_Enumerator_Reset()
        {
            var list = new BoundedList<double>().Populated(50);
            var sut = list.GetEnumerator();

            Assert.DoesNotThrow(() => sut.Reset());
        }

        private static int CompareDinosByLength(string x, string y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're 
                    // equal.  
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y 
                    // is greater.  
                    return -1;
                }
            }
            else
            {
                // If x is not null... 
                // 
                if (y == null)
                    // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the  
                    // lengths of the two strings. 
                    // 
                    int retval = x.Length.CompareTo(y.Length);

                    if (retval != 0)
                    {
                        // If the strings are not of equal length, 
                        // the longer string is greater. 
                        // 
                        return retval;
                    }
                    // If the strings are of equal length, 
                    // sort them with ordinary string comparison. 
                    // 
                    return string.Compare(x, y, StringComparison.Ordinal);

                }
            }
        }

    }


    internal class DinoComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're 
                    // equal.  
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y 
                    // is greater.  
                    return -1;
                }
            }
            else
            {
                // If x is not null... 
                // 
                if (y == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the  
                    // lengths of the two strings. 
                    // 
                    int retval = x.Length.CompareTo(y.Length);

                    if (retval != 0)
                    {
                        // If the strings are not of equal length, 
                        // the longer string is greater. 
                        // 
                        return retval;
                    }
                    
                        // If the strings are of equal length, 
                        // sort them with ordinary string comparison. 
                        // 
                        return string.Compare(x, y, StringComparison.Ordinal);
                    
                }
            }
        }
    }

    internal static class BoundedListTestingExtensions
    {
        internal static T[] ConvertToListAndAddRange<T>(this BoundedList<T> list, T[] range)
        {
            var newList = list.ToArray().ToList();

            newList.AddRange(range);

            while (newList.Count > list.Capacity)
            {
                newList.RemoveAt(0);
            }

            return newList.ToArray();
        }

        internal static T[] ConvertToListAndInsertItem<T>(this BoundedList<T> list, T item, int index)
        {
            if (index >= list.Count) ThrowHelper.ThrowArgumentOutOfRangeException(()=>index);

            var newList = list.ToArray().ToList();

            newList.Insert(index, item);

            if (newList.Count >= list.Capacity)
            {
                newList.RemoveAt(0);
            }

            return newList.ToArray();
        }

        internal static BoundedList<double> Populated(this BoundedList<double> list, uint? capacity = null)
        {
            var totalCount = capacity ?? GetRandom.UInt(10, 20);
            var item = GetRandomArray.OfDoubles(totalCount, totalCount).ToList();
            return new BoundedList<double>(item);
        }

        internal static List<double> Populated(this List<double> list, uint? capacity = null)
        {
            var totalCount = capacity ?? GetRandom.UInt(10, 20);

            var item = GetRandomArray.OfDoubles(totalCount, totalCount).ToList();
            return new List<double>(item);
        }


        internal static BoundedList<string> Populated(this BoundedList<string> list, uint? capacity = null)
        {
            var totalCount = capacity ?? GetRandom.UInt(10, 20);
            var item = GetRandomArray.OfStrings(totalCount, totalCount).ToList();
            return new BoundedList<string>(item);
        }

        internal static bool ContainsOnlyOne<T>(this BoundedList<T> list, T itemToFind)
        {
            var count = 0;
            for (var i=0;i<list.Count;i++)
            {
                if (list[i].Equals(itemToFind))
                {
                    ++count;
                }
            }
            return count == 1;
        }




    }

   
}
