using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace Cush.Testing.Tests
{
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    internal class GetRandomArrayTests
    {

        #region Numerics

        [TestCase(1000)]
        public void Test_ScalableNumericChains(int reps)
        {
            TestNumericArrayChain(reps, GetRandomArray.OfDoubles);

            TestNumericArrayChain(reps, GetRandomArray.OfDecimals);

            TestNumericArrayChain(reps, GetRandomArray.OfFloats);
            TestNumericArrayChain(reps, GetRandomArray.OfSingles);

            TestNumericArrayChain(reps, GetRandomArray.OfInts);
            TestNumericArrayChain(reps, GetRandomArray.OfInt32s);
            TestNumericArrayChain(reps, GetRandomArray.OfShorts);
            TestNumericArrayChain(reps, GetRandomArray.OfInt16s);
            TestNumericArrayChain(reps, GetRandomArray.OfLongs);
            TestNumericArrayChain(reps, GetRandomArray.OfInt64s);

        }

        [TestCase(1000)]
        public void Test_UnscalableNumericChains(int reps)
        {
            TestNumericArrayChain(reps, GetRandomArray.OfUInts);
            TestNumericArrayChain(reps, GetRandomArray.OfUInt32s);

            TestNumericArrayChain(reps, GetRandomArray.OfUShorts);
            TestNumericArrayChain(reps, GetRandomArray.OfUInt16s);

            TestNumericArrayChain(reps, GetRandomArray.OfULongs);
            TestNumericArrayChain(reps, GetRandomArray.OfUInt64s);

            TestNumericArrayChain(reps, GetRandomArray.OfBytes);
            TestNumericArrayChain(reps, GetRandomArray.OfSBytes);
        }

        private void TestNumericArrayChain<T>(int reps, Func<T[]> arrayFunc) where T : IComparable
        {
            for (var i = 0; i < reps; i++)
            {
                var actual = default(T[]);
                Assert.DoesNotThrow(() => actual = arrayFunc.Invoke());

                Assert.That(actual.Length <= 20, "array is too long.");
                Assert.That(actual.Length >= 0, "array is too short.");

                foreach (var item in actual)
                {
                    Assert.That(item.IsAtMost((T)Convert.ChangeType(20, typeof(T))));
                    Assert.That(item.IsAtLeast((T)Convert.ChangeType(0, typeof(T))));
                }
            }
        }

        #endregion

        #region Basic Types

        [TestCase(1000)]
        public void Test_BasicArrayTypes(int reps)
        {
            Test_BasicArrayType(reps, GetRandomArray.OfBooleans);
            Test_BasicArrayType(reps, GetRandomArray.OfExceptions);
            Test_BasicArrayType(reps, GetRandomArray.OfRegistryKeys);
            
        }
        
        private void Test_BasicArrayType<T>(int reps, Func<T[]> func)
        {
            for (var i = 0; i < reps; i++)
            {
                var actual = func.Invoke();
                Assert.That(actual.Length < 20, "array is too long.");

            }
        }

        #endregion

        #region Strings

        [TestCase(100)]
        public void Given_NoParametersPassed_when_StringsCalled_Then_Golden(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var lengths = new Tuple<uint, uint>(0,20);

                var actual = default(string[]);
                Assert.DoesNotThrow(() => actual = GetRandomArray.OfStrings());

                ValidateLengths(actual, lengths);
                ValidateStrings(actual, Sets.AlphaNumeric, lengths);
            }
        }

        [TestCase(100, Sets.AtomChars)]
        public void Given_StringPassed_when_StringsCalled_Then_Golden(int reps, string characterSet)
        {
            for (var i = 0; i < reps; i++)
            {
                var lengths = new Tuple<uint, uint>(0, 20);

                var actual = default(string[]);
                Assert.DoesNotThrow(() => actual = GetRandomArray.OfStrings(characterSet));

                ValidateLengths(actual, lengths);
                ValidateStrings(actual, characterSet, lengths);
            }
        }

        [TestCase(100, Sets.AtomChars)]
        public void Given_CharSetPassed_when_StringsCalled_Then_Golden(int reps, string characterSet)
        {
            var chars = characterSet.ToCharArray();
            for (var i = 0; i < reps; i++)
            {
                var lengths = new Tuple<uint, uint>(0, 20);

                var actual = default(string[]);
                Assert.DoesNotThrow(() => actual = GetRandomArray.OfStrings(chars));

                ValidateLengths(actual, lengths);
                ValidateStrings(actual, characterSet, lengths);
            }
        }

        [TestCase(100)]
        public void Given_MaxLengthPassed_when_StringsCalled_Then_Golden(int reps)
        {
            var maxListLength = GetRandom.UInt(50);
            for (var i = 0; i < reps; i++)
            {
                var listLengths = new Tuple<uint, uint>(0, maxListLength);
                var stringLengths = new Tuple<uint, uint>(0, 20);

                var actual = default(string[]);
                Assert.DoesNotThrow(() => actual = GetRandomArray.OfStrings(maxListLength));

                ValidateLengths(actual, listLengths);
                ValidateStrings(actual, Sets.AlphaNumeric, stringLengths);
            }
        }

        [TestCase(100, Sets.Alpha)]
        public void Given_MaxListLengthAndStringPassed_when_StringsCalled_Then_Golden(int reps, string characterSet)
        {
            var maxListLength = GetRandom.UInt(50);
            for (var i = 0; i < reps; i++)
            {
                var listLengths = new Tuple<uint, uint>(0, maxListLength);
                var stringLengths = new Tuple<uint, uint>(0, 20);

                var actual = default(string[]);
                Assert.DoesNotThrow(() => actual = GetRandomArray.OfStrings(maxListLength, characterSet));

                ValidateLengths(actual, listLengths);
                ValidateStrings(actual, characterSet, stringLengths);
            }
        }

        [TestCase(100, Sets.Alpha)]
        public void Given_MaxListLengthAndCharsPassed_when_StringsCalled_Then_Golden(int reps, string characterSet)
        {
            var chars = characterSet.ToCharArray();
            var maxListLength = GetRandom.UInt(50);
            for (var i = 0; i < reps; i++)
            {
                var listLengths = new Tuple<uint, uint>(0, maxListLength);
                var stringLengths = new Tuple<uint, uint>(0, 20);

                var actual = default(string[]);
                Assert.DoesNotThrow(() => actual = GetRandomArray.OfStrings(maxListLength, chars));

                ValidateLengths(actual, listLengths);
                ValidateStrings(actual, characterSet, stringLengths);
            }
        }

        private void ValidateStrings(string[] array, string characterSet, Tuple<uint, uint> lengths)
        {
            foreach (var str in array)
            {
                Assert.That(str.Length >= lengths.Item1, "string is too short.");
                Assert.That(str.Length <= lengths.Item2, "string is too long.");
                ValidateChars(str.ToCharArray(),characterSet);
            }
        }

        private void ValidateChars(char[] array, string characterSet)
        {
            foreach (var cha in array)
            {
                Assert.That(characterSet.Contains(cha.ToString()), "character set doesn't contain character.");
            }
        }

        private void ValidateLengths<T>(T[] array, Tuple<uint, uint> lengths)
        {
            Assert.That(array.Length >= lengths.Item1, "array is too short.");
            Assert.That(array.Length <= lengths.Item2, "array is too long.");
        }
        
        #endregion

        #region Chars
        
        [TestCase(100, Sets.AlphaNumeric)]
        public void Test_CharArrays(int reps, string characterSet)
        {
            for (var i = 0; i < reps; i++)
            {
                var defaultLengths = new Tuple<uint, uint>(0, 20);
                var customLengths = new Tuple<uint, uint>(0, GetRandom.UInt(0, 50));

                Assert.DoesNotThrow(() => ValidateCharArray(GetRandomArray.OfChars(), defaultLengths, Sets.AtomChars));

                Assert.DoesNotThrow(() => ValidateCharArray(GetRandomArray.OfChars(characterSet), defaultLengths, characterSet));

                Assert.DoesNotThrow(() => ValidateCharArray(GetRandomArray.OfChars(characterSet.ToCharArray()), defaultLengths, characterSet));

                Assert.DoesNotThrow(() => ValidateCharArray(GetRandomArray.OfChars(customLengths.Item2), customLengths, Sets.AtomChars));

                Assert.DoesNotThrow(() => ValidateCharArray(GetRandomArray.OfChars(customLengths.Item2, characterSet), customLengths, characterSet));

                Assert.DoesNotThrow(() => ValidateCharArray(GetRandomArray.OfChars(customLengths.Item2, characterSet.ToCharArray()), customLengths, characterSet));

            }
        }

        private void ValidateCharArray(char[] array, Tuple<uint,uint> lengths, string characterSet)
        {
            Assert.IsNotNull(array);
            ValidateLengths(array, lengths);
            ValidateChars(array, characterSet);
        }

        #endregion

        #region CharArrays

        [TestCase(100, Sets.AlphaNumeric)]
        public void Test_CharArrayArrays(int reps, string characterSet)
        {
            for (var i = 0; i < reps; i++)
            {
                var defaultLengths = new Tuple<uint, uint>(0, 20);
                var customSetLengths = new Tuple<uint, uint>(0, GetRandom.UInt(6, 50));
                var customArrayLengths = new Tuple<uint, uint>(0, GetRandom.UInt(6, 50));

                Assert.DoesNotThrow(() => ValidateCharArraySet(
                    GetRandomArray.OfCharArrays(), 
                    defaultLengths, defaultLengths, Sets.AtomChars));

                Assert.DoesNotThrow(() => ValidateCharArraySet(
                    GetRandomArray.OfCharArrays(characterSet), 
                    defaultLengths, defaultLengths, characterSet));

                Assert.DoesNotThrow(() => ValidateCharArraySet(
                    GetRandomArray.OfCharArrays(characterSet.ToCharArray()), 
                    defaultLengths, defaultLengths, characterSet));

                Assert.DoesNotThrow(() => ValidateCharArraySet(
                    GetRandomArray.OfCharArrays(customSetLengths.Item1, customSetLengths.Item2, customArrayLengths.Item1,
                        customArrayLengths.Item2),
                    customSetLengths, customArrayLengths, Sets.AtomChars));

                Assert.DoesNotThrow(() => ValidateCharArraySet(
                    GetRandomArray.OfCharArrays(customSetLengths.Item2, characterSet),
                    customSetLengths, defaultLengths, characterSet));

                Assert.DoesNotThrow(() => ValidateCharArraySet(
                    GetRandomArray.OfCharArrays(customSetLengths.Item1, customSetLengths.Item2, customArrayLengths.Item1, customArrayLengths.Item2, characterSet.ToCharArray()), 
                    customSetLengths, customArrayLengths, characterSet));


                Assert.DoesNotThrow(() => ValidateCharArraySet(
                    GetRandomArray.OfCharArrays(customSetLengths.Item2),
                    customSetLengths, defaultLengths, Sets.AtomChars));

                Assert.DoesNotThrow(() => ValidateCharArraySet(
                    GetRandomArray.OfCharArrays(customSetLengths.Item2, characterSet.ToCharArray()),
                    customSetLengths, defaultLengths, characterSet));

                Assert.DoesNotThrow(() => ValidateCharArraySet(
                    GetRandomArray.OfCharArrays(customSetLengths.Item1, customSetLengths.Item2, characterSet.ToCharArray()),
                    customSetLengths, defaultLengths, characterSet));

            }
        }

        private void ValidateCharArraySet(char[][] arraySet, Tuple<uint, uint> listLengths,  Tuple<uint, uint> arrayLengths,string characterSet)
        {
            Assert.IsNotNull(arraySet);
            ValidateLengths(arraySet, listLengths);
            foreach (var array in arraySet)
            {
                ValidateLengths(array, arrayLengths);
                ValidateChars(array, characterSet);
            }
        }


#endregion

    }
}