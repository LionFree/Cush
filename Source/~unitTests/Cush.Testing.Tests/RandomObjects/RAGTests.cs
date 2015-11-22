using System;
using System.Diagnostics.CodeAnalysis;
using Cush.Testing.RandomObjects;
using NUnit.Framework;

namespace Cush.Testing.Tests.RandomObjects
{
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    internal class RAGTests
    {
        private readonly RandomObjectGenerator _rog = RandomObjectGenerator.GetInstance();

        [TestCase(1000, Scale.Flat)]
        [TestCase(1000, Scale.Exponential)]
        public void Given_AllParametersPassed_When_ScaledNumericCalled_Then_Golden(int reps, Scale scale)
        {
            TestArrayOfScaledNumerics(reps, scale, GetScaledValues<double>(_rog.GetRandomDouble), GetRandomArray.OfDoubles);

            TestArrayOfScaledNumerics(reps, scale, GetScaledValues<float>(_rog.GetRandomFloat), GetRandomArray.OfFloats);

            TestArrayOfScaledNumerics(reps, scale, GetScaledValues<short>(_rog.GetRandomShort), GetRandomArray.OfShorts);

            TestArrayOfScaledNumerics(reps, scale, GetScaledValues<long>(_rog.GetRandomLong), GetRandomArray.OfLongs);

            TestArrayOfScaledNumerics(reps, scale, GetScaledValues<int>(_rog.GetRandomInt), GetRandomArray.OfInts);

            TestArrayOfScaledNumerics(reps, scale, GetScaledValues<decimal>(_rog.GetRandomDecimal), GetRandomArray.OfDecimals);

        }

        [TestCase(100)]
        public void Given_AllParametersPassed_When_UnscaledNumericCalled_Then_Golden(int reps)
        {
            TestArrayOfUnscaledNumerics(reps, GetUnscaledValues<uint>(_rog.GetRandomUInt), GetRandomArray.OfUInts);

            TestArrayOfUnscaledNumerics(reps, GetUnscaledValues<ushort>(_rog.GetRandomUShort), GetRandomArray.OfUShorts);

            TestArrayOfUnscaledNumerics(reps, GetUnscaledValues<ulong>(_rog.GetRandomULong), GetRandomArray.OfULongs);

            TestArrayOfUnscaledNumerics(reps, GetUnscaledValues<byte>(_rog.GetRandomByte), GetRandomArray.OfBytes);

            TestArrayOfUnscaledNumerics(reps, GetUnscaledValues<sbyte>(_rog.GetRandomSByte), GetRandomArray.OfSBytes);
        }

        [TestCase(100, Sets.AtomChars)]
        public void Given_AllParametersPassed_when_StringsCalled_Then_Golden(int reps, string characterSet)
        {
            for (var i = 0; i < reps; i++)

            {
                var lengths = GetLengths();
                
                var actual = default(string[]);
                Assert.DoesNotThrow(() => actual = GetRandomArray.OfStrings(lengths.Item1, lengths.Item2, characterSet));

                ValidateLengths(actual,lengths);
                
                foreach (var str in actual)
                {
                    foreach (var cha in str)
                    {
                        Assert.That(characterSet.Contains(cha.ToString()), "character set doesn't contain character.");
                    }
                }
            }
        }

        [TestCase(100, Sets.AtomChars)]
        public void Given_AllParametersPassed_when_CharsCalled_Then_Golden(int reps, string characterSet)
        {
            for (var i = 0; i < reps; i++)
            {
                var lengths = GetLengths();
                
                var actual = default(char[]);
                Assert.DoesNotThrow(() => actual = GetRandomArray.OfChars(lengths.Item1, lengths.Item2, characterSet));

                ValidateLengths(actual, lengths);
                
                foreach (var cha in actual)
                {
                    Assert.That(characterSet.Contains(cha.ToString()), "character set doesn't contain character.");

                }
            }
        }

        [TestCase(100, Sets.AtomChars)]
        public void Given_AllParametersPassed_when_CharArraysCalled_Then_Golden(int reps, string characterSet)
        {
            for (var i = 0; i < reps; i++)
            {
                var lengths = GetLengths();

                var actual = default(char[][]);
                Assert.DoesNotThrow(() => actual = GetRandomArray.OfCharArrays(lengths.Item1, lengths.Item2, characterSet));

                ValidateLengths(actual, lengths);

                foreach (var charArray in actual)
                {
                    foreach (var cha in charArray)
                    {
                        Assert.That(characterSet.Contains(cha.ToString()), "character set doesn't contain character.");
                    }
                }
            }
        }
        
        [TestCase(1000)]
        public void Given_AllParametersPassed_when_SimpleNonNumericsCalled_Then_Golden(int reps)
        {
            TestSimpleNonNumerics(reps, GetRandomArray.OfExceptions);
            TestSimpleNonNumerics(reps, GetRandomArray.OfRegistryKeys);
            TestSimpleNonNumerics(reps, GetRandomArray.OfBooleans);
        }

        [Test]
        public void Test_PTI_Getter()
        {
            var rog = RandomArrayGenerator.PerThreadInstance;
            Assert.IsNotNull(rog);
        }

        #region Privates

        private Tuple<T, T> GetScaledValues<T>(Func<T, T, Scale, T> func) where T:struct, IComparable<T>
        {
            var mins = GetMins<T>();
            
            var minValue = func.Invoke(mins.Item1, mins.Item2, Scale.Flat);
            var maxValue = func.Invoke(mins.Item2, mins.Item3, Scale.Flat);

            return new Tuple<T, T>(minValue, maxValue);
        }

        private Tuple<T, T> GetUnscaledValues<T>(Func<T, T, T> func) where T : struct, IComparable<T>
        {
            var mins = GetMins<T>();
            
            var minValue = func.Invoke(mins.Item1, mins.Item2);
            var maxValue = func.Invoke(mins.Item2, mins.Item3);

            return new Tuple<T, T>(minValue, maxValue);
        }

        private Tuple<T, T, T> GetMins<T>()
        {
            var min1 = (T)Convert.ChangeType(0, typeof(T));
            var min2 = (T)Convert.ChangeType(5, typeof(T));
            var min3 = (T)Convert.ChangeType(10, typeof(T));
            return new Tuple<T, T, T>(min1,min2,min3);
        }

        private Tuple<uint, uint> GetLengths()
        {
            var minLength = _rog.GetRandomUInt(5, 9);
            var maxLength = _rog.GetRandomUInt(minLength, 10);
            return new Tuple<uint, uint>(minLength, maxLength);
        }

        private void ValidateLengths<T>(T[] array, Tuple<uint, uint> lengths)
        {
            Assert.That(array.Length >= lengths.Item1, "array is too short.");
            Assert.That(array.Length <= lengths.Item2, "array is too long.");
        }

        private void TestSimpleNonNumerics<T>(int reps, Func<uint, uint, T[]> arrayFunc)
        {
            for (var i = 0; i < reps; i++)
            {
                var lengths = GetLengths();

                var actual = default(T[]);
                Assert.DoesNotThrow(() => actual = arrayFunc.Invoke(lengths.Item1, lengths.Item2));
                
                ValidateLengths(actual, lengths);
            }
        }

        private void TestArrayOfScaledNumerics<T>(int reps, Scale scale, Tuple<T,T> extremes,
            Func<uint, uint, T, T, Scale, T[]> arrayFunc) where T : IComparable
        {
            for (var i = 0; i < reps; i++)
            {
                var minLength = _rog.GetRandomUInt(5, 9);
                var maxLength = _rog.GetRandomUInt(minLength, 10);

                var minValue = extremes.Item1;
                var maxValue = extremes.Item2;
                
                var actual = default(T[]);
                Assert.DoesNotThrow(() => actual = arrayFunc.Invoke(minLength, maxLength, minValue, maxValue, scale));
                
                Assert.That(actual.Length <= maxLength, "array is too long.");
                Assert.That(actual.Length >= minLength, "array is too short.");

                foreach (var item in actual)
                {
                    Assert.That(item.IsAtMost(maxValue));
                    Assert.That(item.IsAtLeast(minValue));
                }

            }
        }
        
        private void TestArrayOfUnscaledNumerics<T>(int reps, Tuple<T, T> extremes,
            Func<uint, uint, T, T, T[]> arrayFunc) where T : IComparable
        {
            for (var i = 0; i < reps; i++)
            {
                var minLength = _rog.GetRandomUInt(5, 9);
                var maxLength = _rog.GetRandomUInt(minLength, 10);

                var minValue = extremes.Item1;
                var maxValue = extremes.Item2;

                var actual = default(T[]);
                Assert.DoesNotThrow(() => actual = arrayFunc.Invoke(minLength, maxLength, minValue, maxValue));

                Assert.That(actual.Length <= maxLength, "array is too long.");
                Assert.That(actual.Length >= minLength, "array is too short.");

                foreach (var item in actual)
                {
                    Assert.That(item.IsAtMost(maxValue));
                    Assert.That(item.IsAtLeast(minValue));
                }

            }
        }

        #endregion
    }
}