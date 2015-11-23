using System;
using Cush.Testing.RandomObjects;
using NUnit.Framework;

namespace Cush.Testing.Tests.RandomObjects
{
    [TestFixture]
    internal class RogTests
    {
        [TestCase(1000)]
        public void Given_GetRandomChar_When_passedCharArray_Then_Golden(int reps)
        {
            var sut = RandomObjectGenerator.GetInstance();
            const string characterSet = Sets.Symbols;
            for (var i = 0; i < reps; i++)
            {
                var actual = new char[1];
                Assert.DoesNotThrow(() => actual[0] = sut.GetRandomChar(characterSet.ToCharArray()));

                var str = new string(actual);

                Assert.That(str, Is.Not.Null.Or.Empty);
                Assert.That(characterSet.Contains(str));
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomString_When_passedMinMaxAndSet_Then_Golden(int reps)
        {
            const string set = Sets.Symbols;
            var sut = RandomObjectGenerator.GetInstance();

            for (var i = 0; i < reps; i++)
            {
                string actual = null;
                var minLength = GetRandom.UInt(0, 10);
                var maxLength = GetRandom.UInt(minLength, 20);
                Assert.DoesNotThrow(() => actual = sut.GetRandomString(minLength, maxLength, set));

                Assert.IsNotNull(actual, "Actual is null.");
                Assert.That(actual.Length >= minLength, "actual too short: {0} < {1}.", actual.Length, minLength);
                Assert.That(actual.Length <= maxLength, "actual too long:  {0} > {1}.", actual.Length, maxLength);

                foreach (var item in actual)
                {
                    Assert.That(set.Contains(new string(item, 1)), "actual contains invalid characters: {0}", item);
                }
            }
        }

        [TestCase(1000)]
        public void For_ROG_Given_GetRandomDateTimeCalled_When_EarliestEqualsLatest_Then_ReturnEarliest(int reps)
        {
            var sut = RandomObjectGenerator.GetInstance();

            for (var i = 0; i < reps; i++)
            {
                var actual = new DateTime();
                var expected = sut.GetRandomDateTime(new DateTime(1977, 3, 4), DateTime.Now);

                Assert.DoesNotThrow(() => actual = sut.GetRandomDateTime(expected, expected));
                Assert.AreEqual(expected, actual, "Should have returned the same date.  expected{0}:  actual: {1}",
                    expected, actual);
            }
        }

        [TestCase(1000)]
        public void For_ROG_Given_GetRandomByte_When_BothParameters_Then_Golden(int reps)
        {
            var sut = RandomObjectGenerator.GetInstance();
            const byte min = byte.MinValue;
            const byte max = byte.MaxValue;

            for (var i = 0; i < reps; i++)
            {
                var actual = new byte();
                Assert.DoesNotThrow(() => actual = sut.GetRandomByte(min, max));
                Assert.IsNotNull(actual, "Method returned a null actual value.");
                Assert.That(actual.IsAtLeast(byte.MinValue));
                Assert.That(actual.IsAtMost(byte.MaxValue));
            }
        }

        [TestCase(1000)]
        public void For_ROG_Given_Nothing_When_GetRandomBool_Then_Golden(int reps)
        {
            var sut = RandomObjectGenerator.GetInstance();
            var actual = false;
            Assert.DoesNotThrow(() => actual = sut.GetRandomBool());
            Assert.IsNotNull(actual, "Method returned a null actual value.");
        }

        [TestCase(1000, true)]
        [TestCase(1000, false)]
        public void For_ROG_Given_Nothing_When_GetRandomSign_Then_Golden(int reps, bool includeZero)
        {
            var sut = RandomObjectGenerator.GetInstance();

            var actual = 0;
            Assert.DoesNotThrow(() => actual = sut.GetRandomSign(includeZero));
            Assert.IsNotNull(actual, "Method returned a null actual value.");

            if (!includeZero) Assert.AreNotEqual(0, actual);
        }

        [TestCase(1000, Scale.Exponential)]
        [TestCase(1000, Scale.Flat)]
        public void For_ROG_Given_AllParametersArePassed_When_GetRandomScalableNumeric_Then_Golden(int reps, Scale scale)
        {
            var sut = RandomObjectGenerator.GetInstance();

            TestScalableNumeric(reps, scale, long.MinValue, long.MaxValue, sut.GetRandomLong);
            TestScalableNumeric(reps, scale, double.MinValue, double.MaxValue, sut.GetRandomDouble);
            TestScalableNumeric(reps, scale, decimal.MinValue, decimal.MaxValue, sut.GetRandomDecimal);
            TestScalableNumeric(reps, scale, float.MinValue, float.MaxValue, sut.GetRandomFloat);
            TestScalableNumeric(reps, scale, int.MinValue, int.MaxValue, sut.GetRandomInt);
            TestScalableNumeric(reps, scale, short.MinValue, short.MaxValue, sut.GetRandomShort);
        }

        [TestCase(1000)]
        public void For_ROG_Given_AllParametersArePassed_When_GetRandomUnscalableNumeric_Then_Golden(int reps)
        {
            var sut = RandomObjectGenerator.GetInstance();

            TestUnscalableNumeric(reps, ushort.MinValue, ushort.MaxValue, sut.GetRandomUShort);
            TestUnscalableNumeric(reps, ulong.MinValue, ulong.MaxValue, sut.GetRandomULong);
            TestUnscalableNumeric(reps, uint.MinValue, uint.MaxValue, sut.GetRandomUInt);
            TestUnscalableNumeric(reps, sbyte.MinValue, sbyte.MaxValue, sut.GetRandomSByte);
        }

        private void TestUnscalableNumeric<T>(int reps, T min, T max, Func<T, T, T> func)
            where T : IComparable
        {
            for (var i = 0; i < reps; i++)
            {
                var actual = default(T);
                Assert.DoesNotThrow(() => actual = func.Invoke(min, max));
                Assert.IsNotNull(actual, "Method returned a null actual value.");
                Assert.That(actual.IsAtLeast(min));
                Assert.That(actual.IsAtMost(max));
            }
        }

        private void TestScalableNumeric<T>(int reps, Scale scale, T min, T max, Func<T, T, Scale, T> func)
            where T : IComparable
        {
            for (var i = 0; i < reps; i++)
            {
                var actual = default(T);
                Assert.DoesNotThrow(() => actual = func.Invoke(min, max, scale));
                Assert.IsNotNull(actual, "Method returned a null actual value.");
                Assert.That(actual.IsAtLeast(min));
                Assert.That(actual.IsAtMost(max));
            }
        }

        [Test]
        public void For_ROG_Given_ConstructorCalledWithoutSeed_Then_Golden()
        {
            var sut = RandomObjectGenerator.GetInstance();

            Assert.IsNotNull(sut);
        }

        [Test]
        public void For_ROG_Given_GetGlobalSeedGenerator_Then_Golden()
        {
            var sut = RandomObjectGenerator.GlobalSeedGenerator;

            Assert.IsNotNull(sut);
            Assert.IsAssignableFrom<LockedRandom>(sut);
        }

        [Test]
        public void For_ROG_Given_GetRandomCharCalledWithEmptyParameter_Then_ExceptionThrown()
        {
            var sut = RandomObjectGenerator.GetInstance();

            Assert.Throws<ArgumentException>(() => sut.GetRandomChar(string.Empty.ToCharArray()));
        }

        [Test]
        public void For_ROG_Given_GetRandomCharCalledWithoutParameter_Then_ExceptionThrown()
        {
            var sut = RandomObjectGenerator.GetInstance();

            Assert.Throws<ArgumentNullException>(() => sut.GetRandomChar(null));
        }

        [Test]
        public void For_ROG_Given_GetRandomStringCalledWithEmptyParameter_Then_ExceptionThrown()
        {
            var sut = RandomObjectGenerator.GetInstance();

            Assert.Throws<ArgumentException>(() => sut.GetRandomString(12, 15, string.Empty));
        }

        [Test]
        public void For_ROG_Given_GetRandomStringCalledWithMinGreaterThanMax_Then_ExceptionThrown()
        {
            var sut = RandomObjectGenerator.GetInstance();

            Assert.Throws<ArgumentException>(() => sut.GetRandomString(15, 5, Sets.AtomChars));
        }

        [Test]
        public void For_ROG_Given_GetRandomStringCalledWithNullParameter_Then_ExceptionThrown()
        {
            var sut = RandomObjectGenerator.GetInstance();

            Assert.Throws<ArgumentException>(() => sut.GetRandomString(12, 15, null));
        }

        [Test]
        public void For_ROG_Given_PerThreadInstanceCalledButNotFirst_Then_Golden()
        {
            var sut = RandomObjectGenerator.PerThreadInstance;

            Assert.IsNotNull(sut);
            Assert.IsAssignableFrom<Random>(sut);
        }
    }
}