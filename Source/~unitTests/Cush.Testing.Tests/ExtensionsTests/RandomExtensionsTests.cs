using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Cush.Testing.Tests.ExtensionsTests
{
    [TestFixture]
    class RandomExtensionsTests
    {
        private readonly Random _rng = new Random();
        
        [TestCase(100, Scale.Flat)]
        [TestCase(100, Scale.Exponential)]
        public void Test_NumericChains_Decimals(int reps, Scale scale)
        {
            var decimals = Get_Extremes<decimal>();
           
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextDecimal(), decimal.MinValue, decimal.MaxValue));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextDecimal(Scale.Flat), decimal.MinValue, decimal.MaxValue));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextDecimal(decimals.Item2), decimal.MinValue, decimals.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextDecimal(decimals.Item1, decimals.Item2), decimals.Item1, decimals.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextDecimal(decimals.Item1, decimals.Item2, Scale.Exponential), decimals.Item1, decimals.Item2));

        }

        [TestCase(1000, Scale.Flat)]
        [TestCase(1000, Scale.Exponential)]
        public void Test_NumericChains_Doubles(int reps, Scale scale)
        {
            var extremes = Get_Extremes<double>();
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextDouble(scale), double.MinValue, double.MaxValue));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextDouble(extremes.Item2), double.MinValue, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextDouble(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextDouble(extremes.Item1, extremes.Item2, scale), extremes.Item1, extremes.Item2));
        }

        [TestCase(100, Scale.Flat)]
        [TestCase(100, Scale.Exponential)]
        public void Test_NumericChains_Floats(int reps, Scale scale)
        {
            var extremes = Get_Extremes<float>();

            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextFloat(), float.MinValue, float.MaxValue));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextFloat(scale), float.MinValue, float.MaxValue));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextFloat(extremes.Item2), float.MinValue, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextFloat(extremes.Item2, scale), float.MinValue, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextFloat(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextFloat(extremes.Item1, extremes.Item2, scale), extremes.Item1, extremes.Item2));

            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextSingle(), float.MinValue, float.MaxValue));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextSingle(scale), float.MinValue, float.MaxValue));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextSingle(extremes.Item2), float.MinValue, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextSingle(extremes.Item2, scale), float.MinValue, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextSingle(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextSingle(extremes.Item1, extremes.Item2, scale), extremes.Item1, extremes.Item2));
        }
        
        [TestCase(100, Scale.Flat)]
        [TestCase(100, Scale.Exponential)]
        public void Test_NumericChains_Shorts(int reps, Scale scale)
        {
            var extremes = Get_Extremes<short>();

            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextShort(), short.MinValue, short.MaxValue));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextShort(scale), short.MinValue, short.MaxValue));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextShort(extremes.Item2), short.MinValue, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextShort(extremes.Item2, scale), short.MinValue, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextShort(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextShort(extremes.Item1, extremes.Item2, scale), extremes.Item1, extremes.Item2));

            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt16(), short.MinValue, short.MaxValue));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt16(scale), short.MinValue, short.MaxValue));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt16(extremes.Item2), short.MinValue, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt16(extremes.Item2, scale), short.MinValue, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt16(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt16(extremes.Item1, extremes.Item2, scale), extremes.Item1, extremes.Item2));
        }

        [TestCase(100, Scale.Flat)]
        [TestCase(100, Scale.Exponential)]
        public void Test_NumericChains_Ints(int reps, Scale scale)
        {
            var extremes = Get_Extremes<int>();
            const int min = int.MinValue;
            const int max = int.MaxValue;

            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt(), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt(scale), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt(extremes.Item2), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt(extremes.Item2, scale), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt(extremes.Item1, extremes.Item2, scale), extremes.Item1, extremes.Item2));

            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt32(), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt32(scale), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt32(extremes.Item2), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt32(extremes.Item2, scale), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt32(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt32(extremes.Item1, extremes.Item2, scale), extremes.Item1, extremes.Item2));
        }

        [TestCase(100, Scale.Flat)]
        [TestCase(100, Scale.Exponential)]
        public void Test_NumericChains_Longs(int reps, Scale scale)
        {
            var extremes = Get_Extremes<long>();
            const long min = long.MinValue;
            const long max = long.MaxValue;

            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextLong(), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextLong(scale), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextLong(extremes.Item2), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextLong(extremes.Item2, scale), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextLong(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextLong(extremes.Item1, extremes.Item2, scale), extremes.Item1, extremes.Item2));

            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt64(), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt64(scale), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt64(extremes.Item2), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt64(extremes.Item2, scale), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt64(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextInt64(extremes.Item1, extremes.Item2, scale), extremes.Item1, extremes.Item2));
        }
        
        [TestCase(100)]
        public void Test_NumericChains_UShort(int reps)
        {
            var extremes = Get_Extremes<ushort>();
            const ushort min = ushort.MinValue;
            const ushort max = ushort.MaxValue;

            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextUShort(), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextUShort(extremes.Item2), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextUShort(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));

            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextUInt16(), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextUInt16(extremes.Item2), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextUInt16(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));
        }

        [TestCase(100)]
        public void Test_NumericChains_UInt(int reps)
        {
            var extremes = Get_Extremes<uint>();
            const uint min = uint.MinValue;
            const uint max = uint.MaxValue;

            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextUInt(), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextUInt(extremes.Item2), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextUInt(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));

            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextUInt32(), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextUInt32(extremes.Item2), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextUInt32(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));
        }
        
        [TestCase(100)]
        public void Test_NumericChains_ULongs(int reps)
        {
            var extremes = Get_Extremes<ulong>();
            const ulong min = ulong.MinValue;
            const ulong max = ulong.MaxValue;

            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextULong(), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextULong(extremes.Item2), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextULong(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));
            
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextUInt64(), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextUInt64(extremes.Item2), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextUInt64(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));
        }


        [TestCase(100)]
        public void Test_NumericChains_byte(int reps)
        {
            var extremes = Get_Extremes<byte>();
            const byte min = byte.MinValue;
            const byte max = byte.MaxValue;

            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextByte(), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextByte(extremes.Item2), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextByte(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));
        }

        [TestCase(100)]
        public void Test_NumericChains_sbyte(int reps)
        {
            var extremes = Get_Extremes<sbyte>();
            const sbyte min = sbyte.MinValue;
            const sbyte max = sbyte.MaxValue;

            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextSByte(), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextSByte(extremes.Item2), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextSByte(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));

            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextSbyte(), min, max));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextSbyte(extremes.Item2), min, extremes.Item2));
            Assert.DoesNotThrow(() => ValidateNumericChain(_rng.NextSbyte(extremes.Item1, extremes.Item2), extremes.Item1, extremes.Item2));
        }

        [Test]
        public void Test_GetSign_With_Inverted_Extremes()
        {
            int sign = 0;
            Assert.Throws<ArgumentException>(() => sign = _rng.GetSign(7, -1));
        }

        [TestCase(1000, Scale.Flat)]
        [TestCase(1000, Scale.Exponential)]
        public void Test_RandomDouble_With_Infinities(int reps,Scale scale)
        {
            double value = 0;
            
            Assert.DoesNotThrow(
                    () => value = _rng.NextDouble(double.NegativeInfinity, double.NegativeInfinity, scale));
            Assert.DoesNotThrow(
                () => value = _rng.NextDouble(double.PositiveInfinity, double.PositiveInfinity, scale));

            Assert.DoesNotThrow(
                () => value = _rng.NextDouble(double.NegativeInfinity, double.PositiveInfinity, scale));
            
            for (var i = 0; i < reps; i++)
            {
                Assert.DoesNotThrow(() => value = _rng.NextDouble(14, double.PositiveInfinity, scale));
                Assert.DoesNotThrow(() => value = _rng.NextDouble(-100, double.PositiveInfinity, scale));
                Assert.DoesNotThrow(() => value = _rng.NextDouble(double.NegativeInfinity, 14, scale));
                Assert.DoesNotThrow(() => value = _rng.NextDouble(double.NegativeInfinity, -100, scale));
            }
        }
        
        [TestCase(1000, Scale.Flat)]
        [TestCase(1000, Scale.Exponential)]
        public void Test_RandomDouble_With_NaN(int reps, Scale scale)
        {
            double value = 0;
            for (var i = 0; i < reps; i++)
            {
                Assert.DoesNotThrow(() => value = _rng.NextDouble(double.NaN, 0, scale));
                Assert.DoesNotThrow(() => value = _rng.NextDouble(0, double.NaN, scale));
                Assert.DoesNotThrow(() => value = _rng.NextDouble(double.NaN, double.NaN, scale));
            }
        }

        [TestCase(1000, Scale.Flat)]
        [TestCase(1000, Scale.Exponential)]
        public void Test_RandomDouble_With_MinEqualsMax(int reps, Scale scale)
        {
            double value = 0;
            for (var i = 0; i < reps; i++)
            {
                Assert.DoesNotThrow(() => value = _rng.NextDouble(14.0, 14.0, scale));
            }
        }



        [Test]
        public void Test_GetBytes()
        {
            Test_GetBytes<decimal>();
            Test_GetBytes<double>();
            Test_GetBytes<float>();
            Test_GetBytes<int>();
            Test_GetBytes<long>();
            Test_GetBytes<short>();
            

            Test_GetBytes<ushort>();
            Test_GetBytes<uint>();
            Test_GetBytes<ulong>();
            Test_GetBytes<byte>();
            Test_GetBytes<sbyte>();
        }

        private void Test_GetBytes<T>()
        {
            var value = default(byte[]);
            Assert.DoesNotThrow(() => value = _rng.GetBytes<T>());
            Debug.WriteLine("{0} has {1} bytes.", typeof(T), value.Length);
        }

        private void ValidateNumericChain<T>(T value, T minValue, T maxValue) where T : IComparable
        {
            Assert.IsNotNull(value);
            Assert.That(value.IsAtLeast(minValue), "value is less than minimum.");
            Assert.That(value.IsAtMost(maxValue), "value is more than maximum.");
        }

        private Tuple<T,T> Get_Extremes<T>()
        {
            var min = (T)Convert.ChangeType(0, typeof (T));
            var max = (T)Convert.ChangeType(20, typeof(T));
            return new Tuple<T, T>(min, max);
        }
        
    }
}
