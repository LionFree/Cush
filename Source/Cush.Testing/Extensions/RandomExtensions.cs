using System;
using System.Runtime.InteropServices;

namespace Cush.Testing
{
    public static class RandomExtensions
    {
        public static byte[] GetBytes<T>(this Random rng)
        {
            var values = new byte[Marshal.SizeOf(typeof (T))];
            rng.NextBytes(values);
            return values;
        }

        public static int NextSign(this Random rng)
        {
            return NextBoolean(rng) ? 1 : -1;
        }

        public static bool NextBoolean(this Random rng)
        {
            return rng.NextDouble() < 0.5;
        }

        public static bool NextBool(this Random rng)
        {
            return NextBoolean(rng);
        }

        #region Decimal

        public static decimal NextDecimal(this Random rnd)
        {
            return NextDecimal(rnd, decimal.MaxValue, Scale.Flat);
        }

        public static decimal NextDecimal(this Random rnd, Scale scale)
        {
            return NextDecimal(rnd, decimal.MaxValue, scale);
        }

        public static decimal NextDecimal(this Random rnd, decimal maxValue)
        {
            return NextDecimal(rnd, decimal.MinValue, maxValue, Scale.Flat);
        }

        public static decimal NextDecimal(this Random rnd, decimal maxValue, Scale scale)
        {
            return NextDecimal(rnd, decimal.MinValue, maxValue, scale);
        }

        public static decimal NextDecimal(this Random rng, decimal minValue, decimal maxValue)
        {
            return NextDecimal(rng, minValue, maxValue, Scale.Flat);
        }

        public static decimal NextDecimal(this Random rng, decimal minValue, decimal maxValue, Scale scale)
        {
            return (decimal)NextDouble(rng, (double)minValue, (double)maxValue, scale);
        }

        #endregion

        #region double

        public static double NextDouble(this Random rng, Scale scale)
        {
            return NextDouble(rng, double.MaxValue, scale);
        }

        public static double NextDouble(this Random rng, double maxValue)
        {
            return NextDouble(rng, maxValue, Scale.Flat);
        }
        
        public static double NextDouble(this Random rng, double maxValue, Scale scale)
        {
            return NextDouble(rng, double.MinValue, maxValue, scale);
        }

        public static double NextDouble(this Random rng, double minValue, double maxValue)
        {
            return NextDouble(rng, minValue, maxValue, Scale.Flat);
        }

        public static double NextDouble(this Random rng, double minValue, double maxValue, Scale scale)
        {
            switch (scale)
            {
                default:
                    lock (rng) return RandomDouble(rng, minValue, maxValue);

                case (Scale.Exponential):
                    while (true)
                    {
                        if (!ValidBounds(minValue, maxValue)) return minValue;
                        if (Math.Abs(minValue - maxValue) < double.Epsilon) return minValue;

                        var minScale = Math.Log10(minValue);
                        var maxScale = Math.Log10(maxValue);
                        
                        if (double.IsNaN(minScale) || double.IsNegativeInfinity(minScale))
                        {
                            minScale = -100;
                        }

                        if (double.IsNaN(maxScale) || double.IsPositiveInfinity(maxScale) || double.IsNegativeInfinity(maxScale))
                        {
                            maxScale = 308;
                        }

                        var sign = GetSign(rng, minValue, maxValue);

                        double exponent;
                        lock (rng) exponent = rng.NextDouble(minScale, maxScale);

                        var output = sign*Math.Pow(10, exponent);


                        if (IsValidValue(minValue, maxValue, output)) return output;
                    }
            }
        }

        internal static bool ValidBounds(double minValue, double maxValue)
        {
            if (double.IsNegativeInfinity(minValue) && double.IsNegativeInfinity(maxValue)) return false;
            if (double.IsPositiveInfinity(minValue) && double.IsPositiveInfinity(maxValue)) return false;
            if (double.IsNaN(minValue) || double.IsNaN(maxValue)) return false;
            return true;
        }

        internal static bool IsValidValue(double minValue, double maxValue, double value)
        {
            if (double.IsNegativeInfinity(minValue) && double.IsPositiveInfinity(maxValue))
                return true;

            if (double.IsNegativeInfinity(minValue) && value <= maxValue)
                return true;

            if (value >= minValue && double.IsPositiveInfinity(maxValue))
                return true;
            
            if (value >= minValue && value <= maxValue)
                return true;

            return false;
        }


        internal static int GetSign(this Random rng, double minValue, double maxValue)
        {
            var signMax = Math.Sign(maxValue);
            var signMin = Math.Sign(minValue);
            var difference = signMax - signMin;
            var sum = signMax + signMin;

            switch (difference)
            {
                case 0: // same sign
                    return signMin;
                case 2: // different signs
                    return NextSign(rng);
                case 1: // one is zero
                    return sum;
                default:
                    throw new ArgumentException("maxValue is less than minValue.");
            }
        }

        #endregion

        #region single/float

        public static float NextFloat(this Random rng)
        {
            return NextSingle(rng, float.MaxValue, Scale.Flat);
        }

        public static float NextFloat(this Random rng, Scale scale)
        {
            return NextSingle(rng, float.MaxValue, scale);
        }

        public static float NextFloat(this Random rng, float maxValue)
        {
            return NextSingle(rng, float.MinValue, maxValue, Scale.Flat);
        }

        public static float NextFloat(this Random rng, float maxValue, Scale scale)
        {
            return NextSingle(rng, float.MinValue, maxValue, scale);
        }

        public static float NextFloat(this Random rng, float minValue, float maxValue)
        {
            return NextSingle(rng, minValue, maxValue, Scale.Flat);
        }

        public static float NextFloat(this Random rng, float minValue, float maxValue, Scale scale)
        {
            return NextSingle(rng, minValue, maxValue, scale);
        }

        public static float NextSingle(this Random rng)
        {
            return NextSingle(rng, float.MaxValue, Scale.Flat);
        }

        public static float NextSingle(this Random rng, Scale scale)
        {
            return NextSingle(rng, float.MaxValue, scale);
        }

        public static float NextSingle(this Random rng, float maxValue)
        {
            return NextSingle(rng, float.MinValue, maxValue, Scale.Flat);
        }

        public static float NextSingle(this Random rng, float maxValue, Scale scale)
        {
            return NextSingle(rng, float.MinValue, maxValue, scale);
        }

        public static float NextSingle(this Random rng, float minValue, float maxValue)
        {
            return NextSingle(rng, minValue, maxValue, Scale.Flat);
        }

        public static float NextSingle(this Random rng, float minValue, float maxValue, Scale scale)
        {
            return (float) NextDouble(rng, minValue, maxValue, scale);
        }

        #endregion

        #region int16/short

        public static short NextInt16(this Random rng)
        {
            return NextInt16(rng, short.MaxValue, Scale.Flat);
        }

        public static short NextInt16(this Random rng, Scale scale)
        {
            return NextInt16(rng, short.MaxValue, scale);
        }

        public static short NextInt16(this Random rng, short maxValue)
        {
            return NextInt16(rng, short.MinValue, maxValue, Scale.Flat);
        }

        public static short NextInt16(this Random rng, short maxValue, Scale scale)
        {
            return NextInt16(rng, short.MinValue, maxValue, scale);
        }

        public static short NextInt16(this Random rng, short minValue, short maxValue)
        {
            return NextInt16(rng, minValue, maxValue, Scale.Flat);
        }

        public static short NextInt16(this Random rng, short minValue, short maxValue, Scale scale)
        {
            return (short) NextDouble(rng, minValue, maxValue, scale);
        }

        public static short NextShort(this Random rng)
        {
            return NextInt16(rng, short.MaxValue, Scale.Flat);
        }

        public static short NextShort(this Random rng, short maxValue)
        {
            return NextInt16(rng, short.MinValue, maxValue, Scale.Flat);
        }

        public static short NextShort(this Random rng, short minValue, short maxValue)
        {
            return NextInt16(rng, minValue, maxValue, Scale.Flat);
        }

        public static short NextShort(this Random rng, Scale scale)
        {
            return NextInt16(rng, short.MaxValue, scale);
        }

        public static short NextShort(this Random rng, short maxValue, Scale scale)
        {
            return NextInt16(rng, short.MinValue, maxValue, scale);
        }

        public static short NextShort(this Random rng, short minValue, short maxValue, Scale scale)
        {
            return NextInt16(rng, minValue, maxValue, scale);
        }

        #endregion

        #region int32/int

        public static int NextInt32(this Random rng)
        {
            return NextInt(rng, int.MaxValue, Scale.Flat);
        }

        public static int NextInt32(this Random rng, Scale scale)
        {
            return NextInt(rng, int.MaxValue, scale);
        }

        public static int NextInt32(this Random rng, int maxValue)
        {
            return NextInt(rng, int.MinValue, maxValue, Scale.Flat);
        }

        public static int NextInt32(this Random rng, int maxValue, Scale scale)
        {
            return NextInt(rng, int.MinValue, maxValue, scale);
        }

        public static int NextInt32(this Random rng, int minValue, int maxValue)
        {
            return NextInt(rng, minValue, maxValue, Scale.Flat);
        }

        public static int NextInt32(this Random rng, int minValue, int maxValue, Scale scale)
        {
            return NextInt(rng, minValue, maxValue, scale);
        }

        public static int NextInt(this Random rng)
        {
            return NextInt(rng, int.MaxValue, Scale.Flat);
        }

        public static int NextInt(this Random rng, Scale scale)
        {
            return NextInt(rng, int.MaxValue, scale);
        }

        public static int NextInt(this Random rng, int maxValue)
        {
            return NextInt(rng, int.MinValue, maxValue, Scale.Flat);
        }

        public static int NextInt(this Random rng, int maxValue, Scale scale)
        {
            return NextInt(rng, int.MinValue, maxValue, scale);
        }

        public static int NextInt(this Random rng, int minValue, int maxValue)
        {
            return NextInt(rng, minValue, maxValue, Scale.Flat);
        }

        public static int NextInt(this Random rng, int minValue, int maxValue, Scale scale)
        {
            return (int) NextDouble(rng, minValue, maxValue, scale);
        }

        #endregion

        #region int64/long

        public static long NextInt64(this Random rng)
        {
            return NextLong(rng, long.MaxValue, Scale.Flat);
        }

        public static long NextInt64(this Random rng, Scale scale)
        {
            return NextLong(rng, long.MaxValue, scale);
        }

        public static long NextInt64(this Random rng, long maxValue)
        {
            return NextLong(rng, long.MinValue, maxValue, Scale.Flat);
        }

        public static long NextInt64(this Random rng, long maxValue, Scale scale)
        {
            return NextLong(rng, long.MinValue, maxValue, scale);
        }

        public static long NextInt64(this Random rng, long minValue, long maxValue)
        {
            return NextLong(rng, minValue, maxValue, Scale.Flat);
        }

        public static long NextInt64(this Random rng, long minValue, long maxValue, Scale scale)
        {
            return NextLong(rng, minValue, maxValue, scale);
        }

        public static long NextLong(this Random rng)
        {
            return NextInt64(rng, long.MaxValue, Scale.Flat);
        }

        public static long NextLong(this Random rng, Scale scale)
        {
            return NextInt64(rng, long.MaxValue, scale);
        }

        public static long NextLong(this Random rng, long maxValue)
        {
            return NextInt64(rng, long.MinValue, maxValue, Scale.Flat);
        }

        public static long NextLong(this Random rng, long maxValue, Scale scale)
        {
            return NextInt64(rng, long.MinValue, maxValue, scale);
        }

        public static long NextLong(this Random rng, long minValue, long maxValue)
        {
            return NextInt64(rng, minValue, maxValue, Scale.Flat);
        }

        public static long NextLong(this Random rng, long minValue, long maxValue, Scale scale)
        {
            return (long)NextDouble(rng, minValue, maxValue, scale);
        }

        #endregion

        #region sbyte

        public static sbyte NextSByte(this Random rng)
        {
            return NextSByte(rng, sbyte.MaxValue);
        }

        public static sbyte NextSByte(this Random rng, sbyte maxValue)
        {
            return NextSByte(rng, sbyte.MinValue, maxValue);
        }

        public static sbyte NextSByte(this Random rng, sbyte minValue, sbyte maxValue)
        {
            while (true)
            {
                var doubleValue = rng.NextDouble();
                return (sbyte) (minValue + (doubleValue*(maxValue - minValue)));
            }
        }

        public static sbyte NextSbyte(this Random rng)
        {
            return NextSByte(rng, sbyte.MaxValue);
        }

        public static sbyte NextSbyte(this Random rng, sbyte maxValue)
        {
            return NextSByte(rng, sbyte.MinValue, maxValue);
        }

        public static sbyte NextSbyte(this Random rng, sbyte minValue, sbyte maxValue)
        {
            return NextSByte(rng, minValue, maxValue);
        }

        #endregion

        #region uint16/ushort

        public static ushort NextUInt16(this Random rng)
        {
            return NextUInt16(rng, ushort.MaxValue);
        }

        public static ushort NextUInt16(this Random rng, ushort maxValue)
        {
            return NextUInt16(rng, ushort.MinValue, maxValue);
        }

        public static ushort NextUInt16(this Random rng, ushort minValue, ushort maxValue)
        {
            while (true)
            {
                var doubleValue = rng.NextDouble();
                return (ushort) (minValue + doubleValue*(maxValue - minValue));
            }
        }

        public static ushort NextUShort(this Random rng)
        {
            return NextUInt16(rng, ushort.MaxValue);
        }

        public static ushort NextUShort(this Random rng, ushort maxValue)
        {
            return NextUInt16(rng, ushort.MinValue, maxValue);
        }

        public static ushort NextUShort(this Random rng, ushort minValue, ushort maxValue)
        {
            return NextUInt16(rng, minValue, maxValue);
        }

        #endregion

        #region uint32/uint

        public static uint NextUInt32(this Random rng)
        {
            return NextUInt32(rng, uint.MaxValue);
        }

        public static uint NextUInt32(this Random rng, uint maxValue)
        {
            return NextUInt32(rng, uint.MinValue, maxValue);
        }

        public static uint NextUInt32(this Random rng, uint minValue, uint maxValue)
        {
            while (true)
            {
                var doubleValue = rng.NextDouble();
                return (uint) (minValue + doubleValue*(maxValue - minValue));
            }
        }

        public static uint NextUInt(this Random rng)
        {
            return NextUInt32(rng, uint.MaxValue);
        }

        public static uint NextUInt(this Random rng, uint maxValue)
        {
            return NextUInt32(rng, uint.MinValue, maxValue);
        }

        public static uint NextUInt(this Random rng, uint minValue, uint maxValue)
        {
            return NextUInt32(rng, minValue, maxValue);
        }

        #endregion

        #region uint64/ulong

        public static ulong NextUInt64(this Random rng)
        {
            return NextUInt64(rng, ulong.MaxValue);
        }

        public static ulong NextUInt64(this Random rng, ulong maxValue)
        {
            return NextUInt64(rng, ulong.MinValue, maxValue);
        }

        public static ulong NextUInt64(this Random rng, ulong minValue, ulong maxValue)
        {
            while (true)
            {
                var doubleValue = rng.NextDouble();
                return (ulong) (minValue + doubleValue*(maxValue - minValue));
            }
        }

        public static ulong NextULong(this Random rng)
        {
            return NextUInt64(rng, ulong.MaxValue);
        }

        public static ulong NextULong(this Random rng, ulong maxValue)
        {
            return NextUInt64(rng, ulong.MinValue, maxValue);
        }

        public static ulong NextULong(this Random rng, ulong minValue, ulong maxValue)
        {
            return NextUInt64(rng, minValue, maxValue);
        }

        #endregion

        #region byte

        public static byte NextByte(this Random rng)
        {
            return NextByte(rng, byte.MaxValue);
        }

        public static byte NextByte(this Random rng, byte maxValue)
        {
            return NextByte(rng, byte.MinValue, maxValue);
        }

        public static byte NextByte(this Random rng, byte minValue, byte maxValue)
        {
            while (true)
            {
                var doubleValue = rng.NextDouble();
                return (byte) (minValue + (doubleValue*(maxValue - minValue)));
            }
        }

        #endregion

        #region Private methods

        internal static double RandomDouble(Random rng, double minValue, double maxValue)
        {
            if (double.IsNegativeInfinity(minValue)) minValue = double.MinValue;
            if (double.IsNegativeInfinity(maxValue)) maxValue = double.MinValue;
            if (double.IsPositiveInfinity(minValue)) minValue = double.MaxValue;
            if (double.IsPositiveInfinity(maxValue)) maxValue = double.MaxValue;

            var scalar = rng.NextDouble();
            var p1 = scalar*maxValue;
            var p2 = scalar*minValue;

            var output = minValue + p1 - p2;

            if (double.IsNegativeInfinity(output)) output = double.MinValue;
            if (double.IsPositiveInfinity(output)) output = double.MaxValue;
            
            return output;
        }

        #endregion
    }
}