using System;
using System.Diagnostics;
using System.Linq;
using Cush.Testing.RandomObjects;
using Microsoft.Win32;
using NUnit.Framework;

namespace Cush.Testing.Tests.RandomObjects
{
    [TestFixture]
    internal class NewROGTests
    {
        private readonly RandomObjectGenerator _rog = RandomObjectGenerator.GetInstance();
        private readonly NewRandomObjectGenerator _sut = NewRandomObjectGenerator.GetInstance();

        [TestCase(1000)]
        public void For_NROG_Given_NewRandomExceptionCalled_WithNullParameter_Then_Golden(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var actual = _sut.NewRandomException(null);

                Assert.IsNotNull(actual);
                Assert.IsInstanceOf<Exception>(actual);
            }
        }

        [TestCase(1000)]
        public void For_NROG_Given_NewRandomExceptionCalled_Then_Golden(int reps)
        {
            var oldValue = _rog.GetRandomException();

            for (var i = 0; i < reps; i++)
            {
                var actual = _sut.NewRandomException(oldValue);

                Assert.IsNotNull(actual);
                Assert.AreNotEqual(actual, oldValue);
                Assert.IsInstanceOf<Exception>(actual);
            }
        }

        [TestCase(1000)]
        public void For_NROG_Given_NewRandomStringCalled_WithProperParameters_Then_Golden(int reps)
        {
            const string set = Sets.Symbols;

            for (var i = 0; i < reps; i++)
            {
                var oldValue = _rog.GetRandomString(10, 20, set);
                Test_String(oldValue, Sets.Symbols);
            }
        }

        [TestCase]
        public void For_NROG_Given_NewRandomStringCalled_WithoutOldValue_Then_Golden()
        {
            const string set = Sets.Symbols;
            Test_String(null, set);
        }

        [TestCase]
        public void For_NROG_Given_NewRandomStringCalled_WithEmptyOldValue_Then_Golden()
        {
            const string set = Sets.Symbols;
            Test_String(string.Empty, set);
        }

        [TestCase]
        public void For_NROG_Given_NewRandomStringCalled_WithNullCharSet_Then_Golden()
        {
            var oldValue = _rog.GetRandomString(10, 20, Sets.AlphaNumeric);

            Assert.That(oldValue, Is.Not.Null.Or.Empty);
            Assert.IsInstanceOf<string>(oldValue);

            Assert.Throws<ArgumentException>(() => _sut.NewRandomString(oldValue, null));
        }

        [TestCase]
        public void For_NROG_Given_NewRandomStringCalled_WithTooSmallCharSet_Then_ExceptionIsThrown()
        {
            const string set = "a";
            const string oldValue = "aaaa";

            Assert.Throws<InvalidOperationException>(() => _sut.NewRandomString(oldValue, set));
        }

        [TestCase(1000)]
        public void For_NROG_Given_NewRandomStringCalled_WithVerySmallCharSet_Then_ExceptionIsThrown(int reps)
        {
            const string set = "ab";
            const string oldValue = "aa";

            Test_String(oldValue, set);
        }

        [TestCase(100)]
        public void For_NROG_Given_AllParametersPassed_When_GetUnscalableNumericCalled_Then_Golden(int reps)
        {
            TestNewUnscalableNumeric(reps, byte.MinValue, byte.MaxValue,
                _rog.GetRandomByte(byte.MinValue + 1, byte.MaxValue - 1),
                _rog.GetRandomByte,
                _sut.NewRandomByte);

            TestNewUnscalableNumeric(reps, uint.MinValue, uint.MaxValue,
                _rog.GetRandomUInt(uint.MinValue + 1, uint.MaxValue - 1),
                _rog.GetRandomUInt,
                _sut.NewRandomUInt);

            TestNewUnscalableNumeric(reps, ushort.MinValue, ushort.MaxValue,
                _rog.GetRandomUShort(ushort.MinValue + 1, ushort.MaxValue - 1),
                _rog.GetRandomUShort,
                _sut.NewRandomUShort);

            TestNewUnscalableNumeric(reps, ulong.MinValue, ulong.MaxValue,
                _rog.GetRandomULong(ulong.MinValue + 1, ulong.MaxValue - 1),
                _rog.GetRandomULong,
                _sut.NewRandomULong);

            TestNewUnscalableNumeric(reps, sbyte.MinValue, sbyte.MaxValue,
                _rog.GetRandomSByte(sbyte.MinValue + 1, sbyte.MaxValue - 1),
                _rog.GetRandomSByte,
                _sut.NewRandomSByte);
        }

        [TestCase(200, Scale.Exponential)]
        [TestCase(200, Scale.Flat)]
        public void For_NROG_Given_AllParametersPassed_When_GetScalableNumericCalled_Then_Golden(int reps, Scale scale)
        {
            var randDouble = _rog.GetRandomDouble(double.MinValue + 1, double.MaxValue - 1, scale);
            var randDecimal = _rog.GetRandomDecimal(decimal.MinValue + 1, decimal.MaxValue - 1, scale);
            var randFloat = _rog.GetRandomFloat(float.MinValue + 1, float.MaxValue - 1, scale);
            var randInt = _rog.GetRandomInt(int.MinValue + 1, int.MaxValue - 1, scale);
            var randLong = _rog.GetRandomLong(long.MinValue + 1, long.MaxValue - 1, scale);
            var randShort = _rog.GetRandomShort(short.MinValue + 1, short.MaxValue - 1, scale);


            TestNewScalableNumeric(reps, double.MinValue, double.MaxValue, randDouble, scale,
                _rog.GetRandomDouble,
                _sut.NewRandomDouble);

            TestNewScalableNumeric(reps, decimal.MinValue, decimal.MaxValue, randDecimal, scale,
                _rog.GetRandomDecimal,
                _sut.NewRandomDecimal);

            TestNewScalableNumeric(reps, float.MinValue, float.MaxValue, randFloat, scale,
                _rog.GetRandomFloat,
                _sut.NewRandomFloat);

            TestNewScalableNumeric(reps, int.MinValue, int.MaxValue, randInt, scale,
                _rog.GetRandomInt,
                _sut.NewRandomInt);

            TestNewScalableNumeric(reps, long.MinValue, long.MaxValue, randLong, scale,
                _rog.GetRandomLong,
                _sut.NewRandomLong);

            TestNewScalableNumeric(reps, short.MinValue, short.MaxValue, randShort, scale,
                _rog.GetRandomShort,
                _sut.NewRandomShort);
        }

        private void TestNewUnscalableNumeric<T>(int reps, T typeMin, T typeMax, T oldValue, Func<T, T, T> randFunc,
            Func<T, T, T, T> newRandFunc)
            where T : IComparable
        {
            for (var i = 0; i < reps; i++)
            {
                var minValue = randFunc.Invoke(typeMin, oldValue);
                var maxValue = randFunc.Invoke(oldValue, typeMax);

                var actual = default(T);

                Assert.DoesNotThrow(() => actual = newRandFunc.Invoke(oldValue, minValue, maxValue), "Threw exception");

                DoNumericAsserts(i, actual, minValue, maxValue, Scale.Unscaled);
            }
        }

        private void TestNewScalableNumeric<T>(int reps, T typeMin, T typeMax, T oldValue, Scale scale,
            Func<T, T, Scale, T> randFunc, Func<T, T, T, Scale, T> newRandFunc)
            where T : IComparable
        {
            for (var i = 0; i < reps; i++)
            {
                var minValue = randFunc.Invoke(typeMin, oldValue, Scale.Flat);
                var maxValue = randFunc.Invoke(oldValue, typeMax, Scale.Flat);

                var actual = default(T);

                Assert.DoesNotThrow(() => actual = newRandFunc.Invoke(oldValue, minValue, maxValue, scale),
                    "Threw exception");

                DoNumericAsserts(i, actual, minValue, maxValue, scale);
            }
        }

        private void DoNumericAsserts<T>(int i, T actual, T minValue, T maxValue, Scale scale) where T : IComparable
        {
            //Debug.WriteLine("{0}({1})", typeof (T), i);
            Debug.WriteLine("{5}|{3}({4})  min: {0}, max: {1}, actual: {2}", minValue, maxValue, actual, typeof (T), i, scale);

            Assert.IsNotNull(actual, "method returned null actual value: {1}|{0}", typeof(T), scale);
            
            Assert.That(actual.IsAtLeast(minValue), "actual value less than min value: {1}|{0}: min={2}, value={3}",
                typeof (T), scale, minValue, actual);

            Assert.That(actual.IsAtMost(maxValue), "actual value exceeds max value: {1}|{0}: max={2}, value={3}",
                typeof (T), scale, minValue, actual);
        }

        [TestCase(100)]
        public void For_NROG_Given_NewRandomCharCalled_WithProperParameters_Then_Golden(int reps)
        {
            const string set = Sets.Symbols;

            for (var i = 0; i < reps; i++)
            {
                var oldValue = _rog.GetRandomChar(set.ToCharArray());
                Test_Char(oldValue, set);
            }
        }

        [TestCase]
        public void For_NROG_Given_NewRandomCharCalled_WithoutOldValue_Then_Golden()
        {
            const string set = Sets.Symbols;
            var array = new char[2];
            Test_Char(array[0], set);
        }

        [TestCase]
        public void For_NROG_Given_NewRandomCharCalled_WithNullCharSet_Then_Golden()
        {
            var oldValue = _rog.GetRandomChar(Sets.AlphaNumeric.ToCharArray());

            Assert.IsNotNull(oldValue);
            Assert.IsInstanceOf<char>(oldValue);

            Assert.Throws<ArgumentException>(() => _sut.NewRandomChar(oldValue, null));
        }

        [TestCase]
        public void For_NROG_Given_NewRandomCharCalled_WithTooSmallCharSet_Then_ExceptionIsThrown()
        {
            var set = new[] {'a'};
            const char oldValue = 'a';

            Assert.Throws<ArgumentException>(() => _sut.NewRandomChar(oldValue, set));
        }

        [TestCase(1000)]
        public void For_NROG_Given_NewRandomCharCalled_WithVerySmallCharSet_Then_ExceptionIsThrown(int reps)
        {
            const string set = "ab";
            const char oldValue = 'a';

            Test_Char(oldValue, set);
        }

        [TestCase(1000)]
        public void For_NROG_Given_NewRandomRegistryKeyCalled_WithNullParameter_Then_Golden(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var actual = _sut.NewRandomRegistryKey(null);

                Assert.IsNotNull(actual);
                Assert.IsInstanceOf<RegistryKey>(actual);
            }
        }

        [TestCase(1000)]
        public void For_NROG_Given_NewRandomRegistryKeyCalled_Then_Golden(int reps)
        {
            var oldValue = _rog.GetRandomRegistryKey();

            for (var i = 0; i < reps; i++)
            {
                var actual = _sut.NewRandomRegistryKey(oldValue);

                Assert.IsNotNull(actual);
                Assert.AreNotEqual(actual, oldValue);
                Assert.IsInstanceOf<RegistryKey>(actual);
            }
        }

        [TestCase(1000)]
        public void For_NROG_Given_NewRandomDateTimeCalled_WithNullParameter_Then_Golden(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var earliest = _rog.GetRandomDateTime(new DateTime(1970, 1, 1), DateTime.Today);
                var latest = _rog.GetRandomDateTime(earliest, DateTime.Now);

                var actual = _sut.NewRandomDateTime(new DateTime(), earliest, latest);

                Assert.IsNotNull(actual);
                Assert.IsInstanceOf<DateTime>(actual);
                Assert.That(actual > earliest);
                Assert.That(actual < latest);
            }
        }

        [TestCase(1000)]
        public void For_NROG_Given_NewRandomDateTimeCalled_WithCloseInterval_Then_Golden(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var earliest = _rog.GetRandomDateTime(new DateTime(1970, 1, 1), DateTime.Now);
                var latest = earliest.AddSeconds(2);

                var actual = _sut.NewRandomDateTime(earliest, earliest, latest);

                Assert.IsNotNull(actual);
                Assert.IsInstanceOf<DateTime>(actual);
                Assert.That(actual > earliest);
                Assert.That(actual < latest);
            }
        }

        [TestCase(1000)]
        public void For_NROG_Given_NewRandomCharArrayCalled_With_SmallSet_Then_Golden(int reps)
        {
            const string set = "ab";
            var oldValue = new[] {'a'};

            for (var i = 0; i < reps; i++)
            {
                Test_CharArray(oldValue, set);
            }
        }

        private void Test_CharArray(char[] oldValue, string set)
        {
            var newValue = _sut.NewRandomCharArray(oldValue, set.ToCharArray());

            Assert.IsNotNull(newValue);
            Assert.That(!oldValue.SequenceEqual(newValue));
            Assert.IsInstanceOf<char[]>(newValue);
            foreach (var item in newValue)
            {
                Assert.That(set.Contains(item.ToString()));
            }
        }

        private void Test_Char(char oldValue, string set)
        {
            var newValue = _sut.NewRandomChar(oldValue, set.ToCharArray());

            Assert.IsNotNull(newValue);
            Assert.AreNotEqual(newValue, oldValue);
            Assert.IsInstanceOf<char>(newValue);
            Assert.That(set.Contains(newValue.ToString()));
        }

        private void Test_String(string oldValue, string set)
        {
            var actual = _sut.NewRandomString(oldValue, set);

            Assert.IsNotNull(actual);
            Assert.AreNotEqual(actual, oldValue);
            Assert.IsInstanceOf<string>(actual);

            foreach (var character in actual)
            {
                Assert.That(set.Contains(character.ToString()));
            }
        }

        [Test]
        public void For_NROG_Given_ConstructorCalledWithoutSeed_Then_Golden()
        {
            Assert.IsNotNull(_sut);
        }

        [Test]
        public void For_NROG_Given_PTICalledButNotFirst_Then_Golden()
        {
            var sut = NewRandomObjectGenerator.PerThreadInstance;

            Assert.IsNotNull(sut);
            Assert.IsInstanceOf<RandomObjectGenerator>(sut);
        }

        [Test, OneTimeSetUp, Description("Must go first for full coverage.")]
        public void For_NROG_Given_PTICalledFirst_Then_Golden()
        {
            var sut = NewRandomObjectGenerator.PerThreadInstance;

            Assert.IsNotNull(sut);
            Assert.IsInstanceOf<RandomObjectGenerator>(sut);
        }
    }
}