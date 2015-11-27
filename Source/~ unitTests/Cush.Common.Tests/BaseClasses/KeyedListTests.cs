using System;
using System.Collections;
using System.Collections.Generic;
using Cush.Testing;
using NUnit.Framework;

namespace Cush.Common.Tests
{
    [TestFixture]
    internal class KeyedListTests
    {
        private const string FirstValue = "firstValue";
        private const string ExpectedKey = "FIRST";

        private string Convert()
        {
            return FirstValue.ToLower();
        }

        private string JunkMethod()
        {
            return FirstValue.ToUpper();
        }


        public KeyedList<Func<string>> SetupKeyedList(string expectedKey)
        {
            Func<string> expectedFunc = Convert;

            var sut = new KeyedList<Func<string>>();
            sut.Add(expectedKey, expectedFunc);
            return sut;
        }

        [TestCase(null)]
        [TestCase("")]
        public void Add_NullOrEmptyStringThrows(string value)
        {
            object actual = null;
            var sut = SetupKeyedList(ExpectedKey);

            Assert.Throws<ArgumentException>(() => actual = sut.Add(value, Convert));
            var func = actual as Func<string>;
            Assert.IsNull(func, "Should not have returned a value.");
        }

        [TestCase(null)]
        [TestCase("")]
        public void Remove_EmptyKeyStringDoesNothing(string value)
        {
            var sut = new KeyedList<Func<string>> {{ExpectedKey, Convert}};
            Assert.IsTrue(sut.Contains(ExpectedKey));
            Assert.DoesNotThrow(() => sut.Remove(value));
            Assert.IsTrue(sut.Contains(ExpectedKey));
        }

        public void Test_GetEnumerator_ReturnsEnumerator<T>(KeyedList<T> sut, Func<IEnumerator> del)
        {
            object actual = null;
            var expected = sut.Items.GetEnumerator();

            Assert.DoesNotThrow(() => actual = del.DynamicInvoke());
            Assert.AreEqual(expected, actual, "Returns wrong enumerator.");
        }

        [Test]
        public void Add_ReturnsExistingValue()
        {
            object actual = null;
            var sut = SetupKeyedList(ExpectedKey);

            Assert.DoesNotThrow(() => sut.Add(ExpectedKey, Convert));
            Assert.DoesNotThrow(() => actual = sut.Add(ExpectedKey, Convert));
            var func = actual as Func<string>;
            Assert.IsNotNull(func);
            Assert.AreEqual((Func<string>) Convert, func);
        }

        [Test]
        public void Add_ReturnsNewValue()
        {
            object actual = null;
            var sut = SetupKeyedList(ExpectedKey);

            Assert.DoesNotThrow(() => actual = sut.Add(ExpectedKey, Convert));
            var func = actual as Func<string>;
            Assert.IsNotNull(func);
            Assert.AreEqual((Func<string>) Convert, func);
        }

        [Test]
        public void Contains_ReturnsFalse()
        {
            object actual = null;

            var sut = SetupKeyedList(ExpectedKey);

            Assert.DoesNotThrow(() => actual = sut.Contains(ExpectedKey));
            Assert.IsNotNull(actual);
            Assert.IsTrue((bool) actual);
        }

        [Test]
        public void Contains_ReturnsTrue()
        {
            object actual = null;
            var sut = SetupKeyedList(ExpectedKey);

            Assert.DoesNotThrow(() => actual = sut.Contains(ExpectedKey));
            Assert.IsNotNull(actual);
            Assert.IsTrue((bool) actual);
        }

        [Test]
        public void GetEnumerator_ReturnsEnumerator()
        {
            var sut = new KeyedList<Func<string>> {{ExpectedKey, Convert}};
            Test_GetEnumerator_ReturnsEnumerator(sut, sut.GetEnumerator);
        }

        [Test]
        public void IEnumerable_GetEnumerator_ReturnsEnumerator()
        {
            var sut = new KeyedList<Func<string>> {{ExpectedKey, Convert}};
            Test_GetEnumerator_ReturnsEnumerator(sut, ((IEnumerable) sut).GetEnumerator);
        }

        [Test]
        public void Items_ReturnsDictionary()
        {
            object actual = null;
            object actualValue = null;

            Func<string> expectedFunc = () => FirstValue.ToLower();
            var expectedKey = "first".ToUpper();
            var expectedDictionary = new Dictionary<string, Func<string>> {{expectedKey, expectedFunc}};
            var sut = new KeyedList<Func<string>>();
            sut.Add(expectedKey, expectedDictionary[expectedKey]);

            Assert.DoesNotThrow(() => actual = sut.Items);

            var dictionary = actual as Dictionary<string, Func<string>>;
            Assert.IsNotNull(dictionary);
            Assert.AreEqual(sut.Count, dictionary.Count);

            GAssert.AreEqual(dictionary, expectedDictionary);

            Assert.DoesNotThrow(() => actualValue = sut[expectedKey]);
            Assert.AreEqual(expectedFunc, actualValue);
        }

        [Test]
        public void Remove_BadKeyStringDoesNothing()
        {
            var badName = NewRandom.String(ExpectedKey);
            var sut = new KeyedList<Func<string>> {{ExpectedKey, Convert}};
            Assert.IsTrue(sut.Contains(ExpectedKey));
            Assert.DoesNotThrow(() => sut.Remove(badName));
            Assert.IsTrue(sut.Contains(ExpectedKey));
        }

        [Test]
        public void Remove_RemovesItem()
        {
            object actual = null;
            var sut = new KeyedList<Func<string>> {{ExpectedKey, Convert}};

            Assert.IsTrue(sut.Contains(ExpectedKey));
            Assert.DoesNotThrow(() => sut.Remove(ExpectedKey));
            Assert.IsFalse(sut.Contains(ExpectedKey));

            Assert.DoesNotThrow(() => actual = sut.Add(ExpectedKey, Convert));
            var func = actual as Func<string>;
            Assert.IsNotNull(func);
            Assert.AreEqual((Func<string>) Convert, func);
        }


        [Test]
        public void Clear_Works()
        {
            object actual = null;
            var expectedKey2 = NewRandom.String(ExpectedKey);

            var sut = new KeyedList<Func<string>>
            {
                {ExpectedKey, Convert},
                {expectedKey2, Convert},
            };
            Assert.AreEqual(2, sut.Count);
            Assert.DoesNotThrow(() => sut.Clear());
            Assert.AreEqual(0, sut.Count);

            Assert.Throws<KeyNotFoundException>(() => actual = sut[ExpectedKey]);
            Assert.IsNull(actual);
        }


        [Test]
        public void Item_SetterAddsNewItem()
        {
            object actual = null;
            var sut = new KeyedList<Func<string>>();

            Assert.DoesNotThrow(() => sut[ExpectedKey] = Convert);
            Assert.AreEqual(1, sut.Count, "Value was not added.");
            actual = sut[ExpectedKey];
            Assert.IsNotNull(actual, "Value was set, but returns as null.");
            Assert.AreEqual((Func<string>) Convert, actual);
        }

        [Test]
        public void Item_SetterReplacesExistingItem()
        {
            object actual = null;
            var sut = new KeyedList<Func<string>>();

            Assert.DoesNotThrow(() => sut[ExpectedKey] = JunkMethod);
            Assert.AreEqual(1, sut.Count, "Value was not added.");
            actual = sut[ExpectedKey];
            Assert.IsNotNull(actual, "Value was set, but returns as null.");
            Assert.AreEqual((Func<string>)JunkMethod, actual);

            Assert.DoesNotThrow(() => sut[ExpectedKey] = Convert);
            Assert.AreEqual(1, sut.Count, "Count is incorrect after replace operation.");
            actual = sut[ExpectedKey];
            Assert.IsNotNull(actual, "Value was set, but returns as null.");
            Assert.AreEqual((Func<string>)Convert, actual);
        }
    }
}