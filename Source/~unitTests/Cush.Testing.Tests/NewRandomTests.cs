using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace Cush.Testing.Tests
{
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    internal class NewRandomTests
    {
        private void TestUnscalableNumericChain<T>(Func<T, T, T> oldFunc,
            Func<T, T> newFunc) where T : IComparable
        {
            var minValue = (T) Convert.ChangeType(0, typeof (T));
            var maxValue = (T) Convert.ChangeType(100, typeof (T));

            var oldValue = oldFunc.Invoke(minValue, maxValue);
            var newValue = newFunc(oldValue);

            Assert.AreNotEqual(oldValue, newValue, "NewValue is equal to OldValue.");
        }

        private void TestScalableNumericChain<T>(Func<T, T, Scale, T> oldFunc,
            Func<T, T> newFunc) where T : IComparable
        {
            var minValue = (T) Convert.ChangeType(-100, typeof (T));
            var maxValue = (T) Convert.ChangeType(100, typeof (T));

            var oldValue = oldFunc.Invoke(minValue, maxValue, Scale.Flat);
            var newValue = newFunc(oldValue);

            Assert.AreNotEqual(oldValue, newValue, "NewValue is equal to OldValue.");
        }

        private void Test_NewCharArray(string characterSet, char[] oldValue, char[] newValue)
        {
            Assert.AreNotEqual(oldValue, newValue);

            foreach (var item in newValue)
            {
                Assert.That(characterSet.Contains(item.ToString()));
            }
        }

        private void Test_NewChar(string characterSet, char oldValue, char newValue)
        {
            Assert.AreNotEqual(oldValue, newValue);

            Assert.That(characterSet.Contains(newValue.ToString()));
        }

        private void Test_NewString(string characterSet, string oldValue, string newValue)
        {
            Assert.AreNotEqual(oldValue, newValue);

            foreach (var item in newValue)
            {
                Assert.That(characterSet.Contains(item.ToString()));
            }
        }

        private void Test_SimpleType<T>(Func<T> oldFunc, Func<T, T> newFunc)
        {
            var oldValue = oldFunc.Invoke();
            var newValue = newFunc.Invoke(oldValue);
            Assert.AreNotEqual(oldValue, newValue, "Old value ({0}) is equal to new Value ({1})", oldValue, newValue);
        }

        [Test]
        public void NR_Double_withOld_MaxValue_Test()
        {
            var oldValue = GetRandom.Double(-100, 100);
            var newValue = NewRandom.Double(oldValue, oldValue);
            Assert.AreNotEqual(oldValue, newValue);
        }

        [Test]
        public void Test_NewChar_WithoutSet()
        {
            var set = Sets.AtomChars;
            var oldValue = GetRandom.Char(set);
            var newValue = NewRandom.Char(oldValue);
            Test_NewChar(set, oldValue, newValue);
        }

        [Test]
        public void Test_NewChar_WithSet()
        {
            var set = Sets.Alpha;
            var oldValue = GetRandom.Char(set);
            var newValue = NewRandom.Char(oldValue, set);
            Test_NewChar(set, oldValue, newValue);
        }

        [Test]
        public void Test_NewCharArray_WithoutSet()
        {
            var set = Sets.AtomChars;
            var oldValue = GetRandom.CharArray(set);
            var newValue = NewRandom.CharArray(oldValue);
            Test_NewCharArray(set, oldValue, newValue);
        }

        [Test]
        public void Test_NewCharArray_WithSet()
        {
            var set = Sets.Alpha;
            var oldValue = GetRandom.CharArray(set);
            var newValue = NewRandom.CharArray(oldValue, set);
            Test_NewCharArray(set, oldValue, newValue);
        }

        [Test]
        public void Test_NewDateTime()
        {
            var oldValue = GetRandom.DateTime(new DateTime(1977, 3, 4), DateTime.Now);
            var newValue = NewRandom.DateTime(oldValue);
            Assert.AreNotEqual(oldValue, newValue);
        }

        [Test]
        public void Test_ScalableNumericChains()
        {
            TestScalableNumericChain<decimal>(GetRandom.Decimal, NewRandom.Decimal);

            TestScalableNumericChain<double>(GetRandom.Double, NewRandom.Double);

            TestScalableNumericChain<float>(GetRandom.Single, NewRandom.Single);
            TestScalableNumericChain<float>(GetRandom.Float, NewRandom.Float);

            TestScalableNumericChain<short>(GetRandom.Short, NewRandom.Short);
            TestScalableNumericChain<short>(GetRandom.Int16, NewRandom.Int16);

            TestScalableNumericChain<long>(GetRandom.Long, NewRandom.Long);
            TestScalableNumericChain<long>(GetRandom.Int64, NewRandom.Int64);

            TestScalableNumericChain<int>(GetRandom.Int, NewRandom.Int);
            TestScalableNumericChain<int>(GetRandom.Int32, NewRandom.Int32);
        }

        [Test]
        public void Test_SimpleTypes()
        {
            Test_SimpleType(GetRandom.Exception, NewRandom.Exception);
            Test_SimpleType(GetRandom.RegistryKey, NewRandom.RegistryKey);
        }

        [Test]
        public void Test_String_WithoutSet()
        {
            var set = Sets.AtomChars;
            var oldValue = GetRandom.String(set);
            var newValue = NewRandom.String(oldValue);
            Test_NewString(set, oldValue, newValue);
        }

        [Test]
        public void Test_String_WithSet()
        {
            var set = Sets.Alpha;
            var oldValue = GetRandom.String(set);
            var newValue = NewRandom.String(oldValue, set);
            Test_NewString(set, oldValue, newValue);
        }

        [Test]
        public void Test_UnscalableNumericChains()
        {
            TestUnscalableNumericChain<byte>(GetRandom.Byte, NewRandom.Byte);
            TestUnscalableNumericChain<sbyte>(GetRandom.Sbyte, NewRandom.Sbyte);
            TestUnscalableNumericChain<uint>(GetRandom.UInt, NewRandom.UInt);
            TestUnscalableNumericChain<uint>(GetRandom.UInt32, NewRandom.UInt32);
            TestUnscalableNumericChain<ushort>(GetRandom.UInt16, NewRandom.UInt16);
            TestUnscalableNumericChain<ushort>(GetRandom.UShort, NewRandom.UShort);
            TestUnscalableNumericChain<ulong>(GetRandom.UInt64, NewRandom.UInt64);
            TestUnscalableNumericChain<ulong>(GetRandom.ULong, NewRandom.ULong);
        }
    }
}