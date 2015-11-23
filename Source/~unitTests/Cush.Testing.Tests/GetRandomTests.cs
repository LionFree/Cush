using System;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using NUnit.Framework;

namespace Cush.Testing.Tests
{
    [TestFixture]
    internal class GetRandomTests
    {
        [TestCase(1000)]
        public void Test_Numerics_With_NoParams(int reps)
        {
            TestNumeric_NoParams(reps, GetRandom.Byte);
            TestNumeric_NoParams(reps, GetRandom.Sbyte);
            TestNumeric_NoParams(reps, GetRandom.UInt);
            TestNumeric_NoParams(reps, GetRandom.UInt32);
            TestNumeric_NoParams(reps, GetRandom.UInt16);
            TestNumeric_NoParams(reps, GetRandom.UShort);
            TestNumeric_NoParams(reps, GetRandom.ULong);
            TestNumeric_NoParams(reps, GetRandom.UInt64);

            TestNumeric_NoParams(reps, GetRandom.Decimal);
            TestNumeric_NoParams(reps, GetRandom.Double);
            TestNumeric_NoParams(reps, GetRandom.Float);
            TestNumeric_NoParams(reps, GetRandom.Int);
            TestNumeric_NoParams(reps, GetRandom.Long);
            TestNumeric_NoParams(reps, GetRandom.Short);

            TestNumeric_NoParams(reps, GetRandom.Single);
            TestNumeric_NoParams(reps, GetRandom.Int32);
            TestNumeric_NoParams(reps, GetRandom.Int64);
            TestNumeric_NoParams(reps, GetRandom.Int16);
        }

        [TestCase(1000)]
        public void Test_Numerics_With_MaxOnly(int reps)
        {
            TestNumeric_Max<byte>(reps, GetRandom.Byte);
            TestNumeric_Max<sbyte>(reps, GetRandom.Sbyte);
            TestNumeric_Max<uint>(reps, GetRandom.UInt);
            TestNumeric_Max<uint>(reps, GetRandom.UInt32);
            TestNumeric_Max<ushort>(reps, GetRandom.UInt16);
            TestNumeric_Max<ushort>(reps, GetRandom.UShort);
            TestNumeric_Max<ulong>(reps, GetRandom.ULong);
            TestNumeric_Max<ulong>(reps, GetRandom.UInt64);

            TestNumeric_Max<decimal>(reps, GetRandom.Decimal);
            TestNumeric_Max<double>(reps, GetRandom.Double);
            TestNumeric_Max<float>(reps, GetRandom.Float);
            TestNumeric_Max<int>(reps, GetRandom.Int);
            TestNumeric_Max<long>(reps, GetRandom.Long);
            TestNumeric_Max<short>(reps, GetRandom.Short);

            TestNumeric_Max<float>(reps, GetRandom.Single);
            TestNumeric_Max<int>(reps, GetRandom.Int32);
            TestNumeric_Max<long>(reps, GetRandom.Int64);
            TestNumeric_Max<short>(reps, GetRandom.Int16);
        }

        [TestCase(1000)]
        public void Test_Numerics_With_MinAndMaxValues(int reps)
        {
            TestNumeric_MinAndMax<byte>(reps, GetRandom.Byte);
            TestNumeric_MinAndMax<sbyte>(reps, GetRandom.Sbyte);
            TestNumeric_MinAndMax<uint>(reps, GetRandom.UInt);
            TestNumeric_MinAndMax<uint>(reps, GetRandom.UInt32);
            TestNumeric_MinAndMax<ushort>(reps, GetRandom.UInt16);
            TestNumeric_MinAndMax<ushort>(reps, GetRandom.UShort);
            TestNumeric_MinAndMax<ulong>(reps, GetRandom.ULong);
            TestNumeric_MinAndMax<ulong>(reps, GetRandom.UInt64);

            TestNumeric_MinAndMax<decimal>(reps, GetRandom.Decimal);
            TestNumeric_MinAndMax<double>(reps, GetRandom.Double);
            TestNumeric_MinAndMax<float>(reps, GetRandom.Float);
            TestNumeric_MinAndMax<int>(reps, GetRandom.Int);
            TestNumeric_MinAndMax<long>(reps, GetRandom.Long);
            TestNumeric_MinAndMax<short>(reps, GetRandom.Short);

            TestNumeric_MinAndMax<float>(reps, GetRandom.Single);
            TestNumeric_MinAndMax<int>(reps, GetRandom.Int32);
            TestNumeric_MinAndMax<long>(reps, GetRandom.Int64);
            TestNumeric_MinAndMax<short>(reps, GetRandom.Int16);
        }

        [TestCase(1000, Scale.Flat)]
        [TestCase(1000, Scale.Exponential)]
        public void Test_Numerics_With_MinMaxAndScale(int reps, Scale scale)
        {
            TestNumeric_MaxMinAndScale<decimal>(reps, scale, GetRandom.Decimal);
            TestNumeric_MaxMinAndScale<double>(reps, scale, GetRandom.Double);
            TestNumeric_MaxMinAndScale<float>(reps, scale, GetRandom.Float);
            TestNumeric_MaxMinAndScale<int>(reps, scale, GetRandom.Int);
            TestNumeric_MaxMinAndScale<long>(reps, scale, GetRandom.Long);
            TestNumeric_MaxMinAndScale<short>(reps, scale, GetRandom.Short);

            TestNumeric_MaxMinAndScale<float>(reps, scale, GetRandom.Single);
            TestNumeric_MaxMinAndScale<int>(reps, scale, GetRandom.Int32);
            TestNumeric_MaxMinAndScale<long>(reps, scale, GetRandom.Int64);
            TestNumeric_MaxMinAndScale<short>(reps, scale, GetRandom.Int16);
        }

        [TestCase(1000)]
        public void Given_GetRandomException_When_noParameters_Then_Golden(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                Exception actual = null;
                Assert.DoesNotThrow(() => actual = GetRandom.Exception());
                Assert.IsNotNull(actual);
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomRegistryKey_When_noParameters_Then_Golden(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                RegistryKey actual = null;
                Assert.DoesNotThrow(() => actual = GetRandom.RegistryKey());
                Assert.IsNotNull(actual);
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomBool_When_noParameters_Then_Golden(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var actual = false;
                Assert.DoesNotThrow(() => actual = GetRandom.Bool());
                Assert.IsNotNull(actual);
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomBoolean_When_noParameters_Then_Golden(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var actual = false;
                Assert.DoesNotThrow(() => actual = GetRandom.Boolean());
                Assert.IsNotNull(actual);
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomDateTime_When_noParameters_Then_Golden(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var actual = DateTime.Now;
                Assert.DoesNotThrow(() => actual = GetRandom.DateTime());
                Assert.IsNotNull(actual);
                Assert.AreNotEqual(DateTime.Now, actual);
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomDateTime_When_PassedParameters_Then_Golden(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var actual = DateTime.Now;
                var early = DateTime.Now;
                var late = DateTime.Now;

                Assert.DoesNotThrow(() => early = GetRandom.DateTime(), "{0}: Parameterless threw an exception.", i);
                Assert.DoesNotThrow(() => late = GetRandom.DateTime(early, DateTime.Now),
                    "{0}: Late threw an exception.", i);

                Assert.That(early <= late, "{0}: early doesn't come before late: {3} {1} >= {2}", i, early, late,
                    Environment.NewLine);

                Assert.DoesNotThrow(() => actual = GetRandom.DateTime(early, late), "{0}: Actual threw an exception.", i);
                Assert.That(early <= actual, "{0}: early doesn't come before actual:{4} {1} >= {2} < {3}", i, early,
                    actual,
                    late, Environment.NewLine);
                Assert.That(actual <= late, "{0}: actual doesn't come before late:{4} {1} < {2} >= {3}", i, early,
                    actual,
                    late, Environment.NewLine);
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomDateTime_When_PassedSameDates_Then_GetSameDateBack(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var actual = DateTime.Now;
                var expected = DateTime.Now;

                Assert.DoesNotThrow(() => expected = GetRandom.DateTime(), "{0}: Parameterless threw an exception.", i);
                Assert.DoesNotThrow(() => actual = GetRandom.DateTime(expected, expected),
                    "{0}: Late threw an exception.", i);

                Assert.AreEqual(expected, actual, "expected != actual : {0} != {1}", expected, actual);
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomSign_When_noParameters_Then_Golden(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var actual = 0;
                Assert.DoesNotThrow(() => actual = GetRandom.Sign());
                Assert.That(-1 <= actual, "actual ({0}) is less than -1", actual);
                Assert.That(actual <= 1, "actual ({0}) is greater than 1.", actual);

                var polar = (actual == -1) || (actual == 1);
                var zero = actual == 0;
                var triState = polar || zero;
                var twoState = polar && !zero;

                Assert.IsTrue(triState, "{0}: actual is not an acceptable value: {1}", i, actual);
                Assert.IsTrue(twoState, "{0}: actual should not be zero, but is.", i);
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomSign_When_NoZero_Then_Golden(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var actual = 0;
                var includeZero = GetRandom.Bool();


                Assert.DoesNotThrow(() => actual = GetRandom.Sign(includeZero));


                var polar = (actual == -1) || (actual == 1);
                var zero = actual == 0;
                var triState = polar || zero;
                var twoState = polar && !zero;

                Assert.IsTrue(triState, "{0}: actual is not an acceptable value: {1}", i, actual);
                if (!includeZero) Assert.IsTrue(twoState, "{0}: actual should not be zero, but is.", i);
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomChar_When_noParameters_Then_Golden(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var actual = new char[1];
                Assert.DoesNotThrow(() => actual[0] = GetRandom.Char());

                var str = new string(actual);

                Assert.That(str, Is.Not.Null.Or.Empty);
                Assert.That(Sets.AtomChars.Contains(str));
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomChar_When_passedCharArray_Then_Golden(int reps)
        {
            const string characterSet = Sets.Symbols;
            for (var i = 0; i < reps; i++)
            {
                var actual = new char[1];
                Assert.DoesNotThrow(() => actual[0] = GetRandom.Char(characterSet.ToCharArray()));

                var str = new string(actual);

                Assert.That(str, Is.Not.Null.Or.Empty);
                Assert.That(characterSet.Contains(str));
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomChar_When_passedString_Then_Golden(int reps)
        {
            const string characterSet = Sets.Alpha;
            for (var i = 0; i < reps; i++)
            {
                var actual = new char[1];
                Assert.DoesNotThrow(() => actual[0] = GetRandom.Char(characterSet));

                var str = new string(actual);

                Assert.That(str, Is.Not.Null.Or.Empty);
                Assert.That(characterSet.Contains(str));
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomCharArray_When_noParameters_Then_Golden(int reps)
        {
            const string set = Sets.AtomChars;
            for (var i = 0; i < reps; i++)
            {
                char[] actual = null;
                Assert.DoesNotThrow(() => actual = GetRandom.CharArray());

                Assert.IsNotNull(actual, "Actual is null.");
                Assert.That(actual.Length <= 20, "actual length {0} > 20.", actual.Length);

                foreach (var item in actual)
                {
                    Assert.That(set.Contains(new string(item, 1)), "actual contains invalid characters: {0}", item);
                }
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomCharArray_When_passedCharArray_Then_Golden(int reps)
        {
            const string set = Sets.Alpha;
            for (var i = 0; i < reps; i++)
            {
                char[] actual = null;
                Assert.DoesNotThrow(() => actual = GetRandom.CharArray(set.ToCharArray()));

                Assert.IsNotNull(actual, "Actual is null.");
                Assert.That(actual.Length <= 20, "actual length {0} > 20.", actual.Length);

                foreach (var item in actual)
                {
                    Assert.That(set.Contains(new string(item, 1)), "actual contains invalid characters: {0}", item);
                }
            }
        }


        [TestCase(1000)]
        public void Given_GetRandomCharArray_When_passedString_Then_Golden(int reps)
        {
            const string set = Sets.AlphaNumeric;
            for (var i = 0; i < reps; i++)
            {
                char[] actual = null;
                Assert.DoesNotThrow(() => actual = GetRandom.CharArray(set));

                Assert.IsNotNull(actual, "Actual is null.");
                Assert.That(actual.Length <= 20, "actual length {0} > 20.", actual.Length);

                foreach (var item in actual)
                {
                    Assert.That(set.Contains(new string(item, 1)), "actual contains invalid characters: {0}", item);
                }
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomCharArray_When_passedLength_Then_Golden(int reps)
        {
            const string set = Sets.AtomChars;

            for (var i = 0; i < reps; i++)
            {
                char[] actual = null;
                var len = GetRandom.UInt(20);
                Assert.DoesNotThrow(() => actual = GetRandom.CharArray(len));

                Assert.IsNotNull(actual, "Actual is null.");
                Assert.That(actual.Length == len, "actual incorrect length: {0} > {1}.", actual.Length, len);

                foreach (var item in actual)
                {
                    Assert.That(set.Contains(new string(item, 1)), "actual contains invalid characters: {0}", item);
                }
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomCharArray_When_passedLengthAndChars_Then_Golden(int reps)
        {
            const string set = Sets.Symbols;

            for (var i = 0; i < reps; i++)
            {
                char[] actual = null;
                var len = GetRandom.UInt(20);
                Assert.DoesNotThrow(() => actual = GetRandom.CharArray(len, set.ToCharArray()));

                Assert.IsNotNull(actual, "Actual is null.");
                Assert.That(actual.Length == len, "actual incorrect length: {0} > {1}.", actual.Length, len);

                foreach (var item in actual)
                {
                    Assert.That(set.Contains(new string(item, 1)), "actual contains invalid characters: {0}", item);
                }
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomCharArray_When_passedLengthAndString_Then_Golden(int reps)
        {
            const string set = Sets.Symbols;

            for (var i = 0; i < reps; i++)
            {
                char[] actual = null;
                var len = GetRandom.UInt(20);
                Assert.DoesNotThrow(() => actual = GetRandom.CharArray(len, set));

                Assert.IsNotNull(actual, "Actual is null.");
                Assert.That(actual.Length == len, "actual incorrect length: {0} > {1}.", actual.Length, len);

                foreach (var item in actual)
                {
                    Assert.That(set.Contains(new string(item, 1)), "actual contains invalid characters: {0}", item);
                }
            }
        }


        [TestCase(1000)]
        public void Given_GetRandomCharArray_When_passedMinMax_Then_Golden(int reps)
        {
            const string set = Sets.AtomChars;

            for (var i = 0; i < reps; i++)
            {
                char[] actual = null;
                var minLength = GetRandom.UInt(0, 10);
                var maxLength = GetRandom.UInt(minLength, 20);
                Assert.DoesNotThrow(() => actual = GetRandom.CharArray(minLength, maxLength));

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
        public void Given_GetRandomCharArray_When_passedMinMaxChars_Then_Golden(int reps)
        {
            const string set = Sets.AlphaNumeric;

            for (var i = 0; i < reps; i++)
            {
                char[] actual = null;
                var minLength = GetRandom.UInt(0, 10);
                var maxLength = GetRandom.UInt(minLength, 20);
                Assert.DoesNotThrow(() => actual = GetRandom.CharArray(minLength, maxLength, set.ToCharArray()));

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
        public void Given_GetRandomCharArray_When_passedMinMaxString_Then_Golden(int reps)
        {
            const string set = Sets.Alpha;

            for (var i = 0; i < reps; i++)
            {
                char[] actual = null;
                var minLength = GetRandom.UInt(0, 10);
                var maxLength = GetRandom.UInt(minLength, 20);
                Assert.DoesNotThrow(() => actual = GetRandom.CharArray(minLength, maxLength, set));

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
        public void Given_GetRandomString_When_noParameters_Then_Golden(int reps)
        {
            const string set = Sets.AlphaNumeric;
            var minLength = 0;
            var maxLength = 20;

            for (var i = 0; i < reps; i++)
            {
                string actual = null;
                Assert.DoesNotThrow(() => actual = GetRandom.String());

                Assert.IsNotNull(actual, "Actual is null.");
                Assert.That(actual.Length >= minLength, "actual too short: {0} < {1}", actual.Length, minLength);
                Assert.That(actual.Length <= maxLength, "actual too long: {0} > {1}", actual.Length, maxLength);

                foreach (var item in actual)
                {
                    Assert.That(set.Contains(new string(item, 1)), "actual contains invalid characters: {0}", item);
                }
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomString_When_passedString_Then_Golden(int reps)
        {
            const string set = Sets.AlphaNumeric;
            uint minLength = 0;
            uint maxLength = 20;

            for (var i = 0; i < reps; i++)
            {
                string actual = null;
                Assert.DoesNotThrow(() => actual = GetRandom.String(set));

                Assert.IsNotNull(actual, "Actual is null.");
                Assert.That(actual.Length >= minLength, "actual too short: {0} < {1}", actual.Length, minLength);
                Assert.That(actual.Length <= maxLength, "actual too long: {0} > {1}", actual.Length, maxLength);

                foreach (var item in actual)
                {
                    Assert.That(set.Contains(new string(item, 1)), "actual contains invalid characters: {0}", item);
                }
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomString_When_passedSetAndLength_Then_Golden(int reps)
        {
            const string expectedSet = Sets.Symbols;
            const uint minLength = 0;
            var maxLength = GetRandom.UInt(0, 20);

            for (var i = 0; i < reps; i++)
            {
                string actual = null;
                Assert.DoesNotThrow(() => actual = GetRandom.String(maxLength, expectedSet));

                Assert.IsNotNull(actual, "{0}: Actual is null.", i);
                Assert.That(actual.Length >= minLength, "{2}: actual too short: {0} < {1}", actual.Length, minLength, i);
                Assert.That(actual.Length <= maxLength, "{2}: actual too long: {0} > {1}", actual.Length, maxLength, i);

                foreach (var item in actual)
                {
                    Assert.That(expectedSet.Contains(new string(item, 1)),
                        "{1}: actual contains invalid characters: {0}", item, i);
                }
            }
        }

        [TestCase(1000)]
        public void Given_GetRandomString_When_passedMinMax_Then_Golden(int reps)
        {
            const string set = Sets.AlphaNumeric;

            for (var i = 0; i < reps; i++)
            {
                string actual = null;
                var minLength = GetRandom.UInt(0, 10);
                var maxLength = GetRandom.UInt(minLength, 20);
                Assert.DoesNotThrow(() => actual = GetRandom.String(minLength, maxLength));

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
        public void Given_GetRandomString_When_passedMinMaxAndSet_Then_Golden(int reps)
        {
            const string set = Sets.Symbols;

            for (var i = 0; i < reps; i++)
            {
                string actual = null;
                var minLength = GetRandom.UInt(0, 10);
                var maxLength = GetRandom.UInt(minLength, 20);
                Assert.DoesNotThrow(() => actual = GetRandom.String(minLength, maxLength, set));

                Assert.IsNotNull(actual, "Actual is null.");
                Assert.That(actual.Length >= minLength, "actual too short: {0} < {1}.", actual.Length, minLength);
                Assert.That(actual.Length <= maxLength, "actual too long:  {0} > {1}.", actual.Length, maxLength);

                foreach (var item in actual)
                {
                    Assert.That(set.Contains(new string(item, 1)), "actual contains invalid characters: {0}", item);
                }
            }
        }

        private void ValidateRandomGenerics<T>(object thing1, object thing2)
        {
            Assert.IsNotNull(thing1, "random object is null");
            Assert.IsNotNull(thing2, "random object is null");
            Assert.That(thing1.GetType() == typeof (T), "random object is not the correct type");
            Assert.That(thing2.GetType() == typeof (T), "random object is not the correct type");
            Assert.AreNotEqual(thing1, thing2, "two random objects are identical");
        }


        private void TestNumeric_NoParams<T>(int reps, Func<T> func) where T : IComparable
        {
            var min = (T) typeof (T).InvokeMember("MinValue", BindingFlags.GetField, null, default(T), null);
            var max = (T) typeof (T).InvokeMember("MaxValue", BindingFlags.GetField, null, default(T), null);

            for (var i = 0; i < reps; i++)
            {
                TestMethod(func, min, max);
            }
        }

        private void TestNumeric_Max<T>(int reps, Func<T, T> func) where T : IComparable
        {
            var min = (T) typeof (T).InvokeMember("MinValue", BindingFlags.GetField, null, default(T), null);
            var max = (T) typeof (T).InvokeMember("MaxValue", BindingFlags.GetField, null, default(T), null);
            for (var i = 0; i < reps; i++)
            {
                TestMethod(func, min, max, max);
            }
        }

        private void TestNumeric_MinAndMax<T>(int reps, Func<T, T, T> func) where T : IComparable
        {
            var min = (T) typeof (T).InvokeMember("MinValue", BindingFlags.GetField, null, default(T), null);
            var max = (T) typeof (T).InvokeMember("MaxValue", BindingFlags.GetField, null, default(T), null);
            for (var i = 0; i < reps; i++)
            {
                TestMethod(func, min, max, min, max);
            }
        }

        private void TestNumeric_MaxMinAndScale<T>(int reps, Scale scale, Func<T, T, Scale, T> func)
            where T : IComparable
        {
            var min = (T) typeof (T).InvokeMember("MinValue", BindingFlags.GetField, null, default(T), null);
            var max = (T) typeof (T).InvokeMember("MaxValue", BindingFlags.GetField, null, default(T), null);

            for (var i = 0; i < reps; i++)
            {
                TestMethod(func, min, max, min, max, scale);
            }
        }


        private void TestMethod<T>(Delegate del, T min, T max, params object[] args) where T : IComparable
        {
            var actual = default(T);
            Assert.DoesNotThrow(() => actual = (T) del.DynamicInvoke(args));
            Assert.That(actual.IsAtLeast(min));
            Assert.That(actual.IsLessThan(max));
        }

        internal class Patient
        {
            public string Identifier { get; set; }
            public string Name { get; set; }
            public int Number1 { get; set; }
            public int Number2 { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        [Test]
        public void Test_AddMethod_With0Parameters()
        {
            GetRandom.AddType(
                () => new Patient {Name = Path.GetRandomFileName(), Identifier = GetRandom.Int().ToString()});

            ValidateRandomGenerics<Patient>(
                GetRandom.Object<Patient>(),
                GetRandom.Object<Patient>());
        }

        [Test]
        public void Test_AddMethod_With1Parameter()
        {
            GetRandom.AddType<string, Patient>(
                x => new Patient
                {
                    Name = x,
                    Identifier = GetRandom.Int().ToString()
                });

            ValidateRandomGenerics<Patient>(
                GetRandom.Object<Patient>("dingus"),
                GetRandom.Object<Patient>("dongle"));
        }

        [Test]
        public void Test_AddMethod_With2Parameters()
        {
            GetRandom.AddType<string, string, Patient>(
                (x, y) => new Patient
                {
                    Name = x,
                    Identifier = y
                });

            ValidateRandomGenerics<Patient>(
                GetRandom.Object<Patient>("dingus", "id1"),
                GetRandom.Object<Patient>("dongle", "id2"));
        }

        [Test]
        public void Test_AddMethod_With3Parameters()
        {
            GetRandom.AddType<string, string, int, Patient>(
                (x, y, z) => new Patient
                {
                    Name = x,
                    Identifier = y,
                    Number1 = z
                });

            ValidateRandomGenerics<Patient>(
                GetRandom.Object<Patient>("dingus", "id1", 1),
                GetRandom.Object<Patient>("dongle", "id2", 2));
        }

        [Test]
        public void Test_AddMethod_With4Parameters()
        {
            GetRandom.AddType<string, string, int, int, Patient>(
                (w, x, y, z) => new Patient
                {
                    Name = w,
                    Identifier = x,
                    Number1 = y,
                    Number2 = z
                });

            ValidateRandomGenerics<Patient>(
                GetRandom.Object<Patient>("dingus", "id1", 1, 10),
                GetRandom.Object<Patient>("dongle", "id2", 2, 20));
        }

        [Test]
        public void Test_AddMethod_With5Parameters()
        {
            GetRandom.AddType<string, string, int, int, DateTime, Patient>(
                (v, w, x, y, z) => new Patient
                {
                    Name = v,
                    Identifier = w,
                    Number1 = x,
                    Number2 = y,
                    CreatedDate = z
                });

            ValidateRandomGenerics<Patient>(
                GetRandom.Object<Patient>("dingus", "id1", 1, 10, new DateTime(1999, 4, 4)),
                GetRandom.Object<Patient>("dongle", "id1", 1, 10, new DateTime(1996, 3, 9)));
        }


        // Generics
        [Test]
        public void Test_Enum_With_Enum()
        {
            var actual = new ConsoleColor();
            Assert.DoesNotThrow(() => { actual = GetRandom.Enum<ConsoleColor>(); });
            Assert.IsNotNull(actual, "Method returns null value.");
        }

        [Test]
        public void Test_Enum_With_OtherType()
        {
            var actual = new double();
            Assert.Throws<ArgumentException>(() => { actual = GetRandom.Enum<double>(); });
            Assert.IsNotNull(actual, "Method returns null value.");
        }

        //        Assert.IsNotNull(actual, "Actual is null.");
        //        Assert.DoesNotThrow(() => actual = GetRandom.String(minLength, maxLength, set.ToString()));
        //        var maxLength = GetRandom.UInt(minLength, 20);
        //        var minLength = GetRandom.UInt(0, 10);
        //        string actual = null;
        //    {

        //    for (var i = 0; i < reps; i++)
        //    const string set = Sets.AlphaNumeric;
        //{
        //public void Given_GetRandomString_When_passedMinMaxChars_Then_Golden(int reps)

        //[TestCase(1000)]
        //}
        //    }
        //        }
        //            Assert.That(set.Contains(new string(item, 1)), "actual contains invalid characters: {0}", item);
        //        {

        //        foreach (var item in actual)
        //        Assert.That(actual.Length <= maxLength, "actual too long:  {0} > {1}.", actual.Length, maxLength);
        //        Assert.That(actual.Length >= minLength, "actual too short: {0} < {1}.", actual.Length, minLength);

        //        Assert.IsNotNull(actual, "Actual is null.");
        //        Assert.DoesNotThrow(() => actual = GetRandom.String(minLength, maxLength));
        //        var maxLength = GetRandom.UInt(minLength, 20);
        //        var minLength = GetRandom.UInt(0, 10);
        //        string actual = null;
        //    {

        //    for (var i = 0; i < reps; i++)
        //    const string set = Sets.AtomChars;
        //{
        //public void Given_GetRandomString_When_passedMinMax_Then_Golden(int reps)


        //[TestCase(1000)]
        //        Assert.That(actual.Length >= minLength, "actual too short: {0} < {1}.", actual.Length, minLength);
        //        Assert.That(actual.Length <= maxLength, "actual too long:  {0} > {1}.", actual.Length, maxLength);

        //        foreach (var item in actual)
        //        {
        //            Assert.That(set.Contains(new string(item, 1)), "actual contains invalid characters: {0}", item);
        //        }
        //    }
        //}

        //[TestCase(1000)]
        //public void Given_GetRandomString_When_passedMinMaxString_Then_Golden(int reps)
        //{
        //    const string set = Sets.Alpha;

        //    for (var i = 0; i < reps; i++)
        //    {
        //        string actual = null;
        //        var minLength = GetRandom.UInt(0, 10);
        //        var maxLength = GetRandom.UInt(minLength, 20);
        //        Assert.DoesNotThrow(() => actual = GetRandom.String(minLength, maxLength, set));

        //        Assert.IsNotNull(actual, "Actual is null.");
        //        Assert.That(actual.Length >= minLength, "actual too short: {0} < {1}.", actual.Length, minLength);
        //        Assert.That(actual.Length <= maxLength, "actual too long:  {0} > {1}.", actual.Length, maxLength);

        //        foreach (var item in actual)
        //        {
        //            Assert.That(set.Contains(new string(item, 1)), "actual contains invalid characters: {0}", item);
        //        }
        //    }
        //}
    }
}