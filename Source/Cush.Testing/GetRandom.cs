using System;
using Cush.Testing.RandomObjects;
using Microsoft.Win32;

namespace Cush.Testing
{
    /// <summary>
    ///     Convenience class for generating random objects of various types.
    /// </summary>
    public static class GetRandom
    {
        [ThreadStatic] private static readonly RandomObjectGenerator Rand;
        [ThreadStatic] private static readonly FunctionRepository Repo;

        static GetRandom()
        {
            Rand = RandomObjectGenerator.GetInstance();
            Repo = FunctionRepository.GetInstance();
        }

        /// <summary>
        ///     Returns an <see cref="T:System.Exception" /> generated
        ///     randomly from the following list:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>Exception</description>
        ///         </item>
        ///         <item>
        ///             <description>AccessViolationException</description>
        ///         </item>
        ///         <item>
        ///             <description>AggregateException</description>
        ///         </item>
        ///         <item>
        ///             <description>ApplicationException</description>
        ///         </item>
        ///         <item>
        ///             <description>ArgumentException</description>
        ///         </item>
        ///         <item>
        ///             <description>ArgumentNullException</description>
        ///         </item>
        ///         <item>
        ///             <description>ArgumentOutOfRangeException</description>
        ///         </item>
        ///         <item>
        ///             <description>ArrayTypeMismatchException</description>
        ///         </item>
        ///         <item>
        ///             <description>InvalidTimeZoneException</description>
        ///         </item>
        ///     </list>
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Exception" /> generated
        ///     randomly from the following list:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>Exception</description>
        ///         </item>
        ///         <item>
        ///             <description>AccessViolationException</description>
        ///         </item>
        ///         <item>
        ///             <description>AggregateException</description>
        ///         </item>
        ///         <item>
        ///             <description>ApplicationException</description>
        ///         </item>
        ///         <item>
        ///             <description>ArgumentException</description>
        ///         </item>
        ///         <item>
        ///             <description>ArgumentNullException</description>
        ///         </item>
        ///         <item>
        ///             <description>ArgumentOutOfRangeException</description>
        ///         </item>
        ///         <item>
        ///             <description>ArrayTypeMismatchException</description>
        ///         </item>
        ///         <item>
        ///             <description>InvalidTimeZoneException</description>
        ///         </item>
        ///     </list>
        /// </returns>
        public static Exception Exception()
        {
            return Rand.GetRandomException();
        }

        /// <summary>
        ///     Returns a <see cref="Microsoft.Win32.RegistryKey" /> selected
        ///     randomly from the following list.
        ///     <list type="bullet">
        ///         <item>
        ///             <description>Registry.ClassesRoot,</description>
        ///         </item>
        ///         <item>
        ///             <description>Registry.CurrentConfig,</description>
        ///         </item>
        ///         <item>
        ///             <description>Registry.CurrentUser,</description>
        ///         </item>
        ///         <item>
        ///             <description>Registry.LocalMachine,</description>
        ///         </item>
        ///         <item>
        ///             <description>Registry.PerformanceData, and</description>
        ///         </item>
        ///         <item>
        ///             <description>Registry.Users.</description>
        ///         </item>
        ///     </list>
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Microsoft.Win32.RegistryKey" />  selected
        ///     randomly from the following list.
        ///     <list type="bullet">
        ///         <item>
        ///             <description>Registry.ClassesRoot,</description>
        ///         </item>
        ///         <item>
        ///             <description>Registry.CurrentConfig,</description>
        ///         </item>
        ///         <item>
        ///             <description>Registry.CurrentUser,</description>
        ///         </item>
        ///         <item>
        ///             <description>Registry.LocalMachine,</description>
        ///         </item>
        ///         <item>
        ///             <description>Registry.PerformanceData, and</description>
        ///         </item>
        ///         <item>
        ///             <description>Registry.Users.</description>
        ///         </item>
        ///     </list>
        /// </returns>
        public static RegistryKey RegistryKey()
        {
            return Rand.GetRandomRegistryKey();
        }

        /// <summary>
        ///     Returns a randomly-selected entry from the enumeration T.
        /// </summary>
        /// <returns>
        ///     A randomly-selected entry from the enumeration T.
        /// </returns>
        public static T Enum<T>()
        {
            return GetRandomEnum<T>();
        }

        /// <summary>
        ///     Returns a random boolean value (<c>true</c> or <c>false</c>).
        /// </summary>
        /// <returns>
        ///     A random boolean value (<c>true</c> or <c>false</c>).
        /// </returns>
        public static bool Bool()
        {
            return Rand.GetRandomBool();
        }

        /// <summary>
        ///     Returns a random boolean value (<c>true</c> or <c>false</c>).
        /// </summary>
        /// <returns>
        ///     A random boolean value (<c>true</c> or <c>false</c>).
        /// </returns>
        public static bool Boolean()
        {
            return Rand.GetRandomBool();
        }

        /// <summary>
        ///     Returns a random 8-bit unsigned integer greater than or equal to 0,
        ///     and strictly less than 256.
        /// </summary>
        /// <returns>
        ///     Returns a random 8-bit unsigned integer greater than or equal to 0,
        ///     and strictly less than 256; that is,
        ///     the range of return values includes 0 but not 256.
        /// </returns>
        public static byte Byte()
        {
            return Byte(byte.MaxValue);
        }

        /// <summary>
        ///     Returns a random 8-bit unsigned integer greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     Returns a random 8-bit unsigned integer greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes 0 but not <paramref name="maxValue" />.
        /// </returns>
        public static byte Byte(byte maxValue)
        {
            return Byte(0, maxValue);
        }

        /// <summary>
        ///     Returns a random 8-bit unsigned integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     An 8-bit unsigned integer greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static byte Byte(byte minValue, byte maxValue)
        {
            return Rand.GetRandomByte(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random 128-bit decimal number greater than or equal to 0,
        ///     and strictly less than <see cref="P:decimal.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     Returns a random 64-bit floating-point number greater than or equal to 0,
        ///     and strictly less than <see cref="P:decimal.MaxValue" />; that is,
        ///     the range of return values includes zero,
        ///     but not <see cref="P:decimal.MaxValue" />.
        /// </returns>
        public static decimal Decimal()
        {
            return Decimal(decimal.MaxValue);
        }

        /// <summary>
        ///     Returns a random 128-bit decimal number greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 128-bit decimal number greater than or equal to 0,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes 0 but not <paramref name="maxValue" />.
        /// </returns>
        public static decimal Decimal(decimal maxValue)
        {
            return Decimal(0, maxValue);
        }

        /// <summary>
        ///     Returns a random 128-bit decimal number greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 128-bit decimal number greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static decimal Decimal(decimal minValue, decimal maxValue)
        {
            return Decimal(minValue, maxValue, Scale.Flat);
        }

        /// <summary>
        ///     Returns a random 128-bit decimal number greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />, using the given <see cref="T:Scale" />.
        /// </summary>
        /// <returns>
        ///     A 128-bit decimal number greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static decimal Decimal(decimal minValue, decimal maxValue, Scale scale)
        {
            return Rand.GetRandomDecimal(minValue, maxValue, scale);
        }

        /// <summary>
        ///     Returns a random 64-bit floating-point number greater than or equal to 0,
        ///     and strictly less than <see cref="P:double.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     Returns a random 64-bit floating-point number greater than or equal to 0,
        ///     and strictly less than <see cref="P:double.MaxValue" />; that is,
        ///     the range of return values includes zero,
        ///     but not <see cref="P:double.MaxValue" />.
        /// </returns>
        public static double Double()
        {
            return Double(double.MaxValue);
        }

        /// <summary>
        ///     Returns a random 64-bit floating-point number greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 64-bit floating-point number greater than or equal to 0,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes 0 but not <paramref name="maxValue" />.
        /// </returns>
        public static double Double(double maxValue)
        {
            return Double(0, maxValue);
        }

        /// <summary>
        ///     Returns a random 64-bit floating-point number greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 64-bit floating-point number greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static double Double(double minValue, double maxValue)
        {
            return Double(minValue, maxValue, Scale.Flat);
        }

        /// <summary>
        ///     Returns a random 64-bit floating-point number greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />, using the given <see cref="T:Scale" />.
        /// </summary>
        /// <returns>
        ///     A 64-bit floating-point number greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static double Double(double minValue, double maxValue, Scale scale)
        {
            return Rand.GetRandomDouble(minValue, maxValue, scale);
        }

        /// <summary>
        ///     Returns a random 32-bit floating-point number greater than or equal to 0,
        ///     and strictly less than <see cref="T:float.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     Returns a random 32-bit floating-point number greater than or equal to 0,
        ///     and strictly less than <see cref="T:float.MaxValue" />; that is,
        ///     the range of return values includes zero,
        ///     but not <see cref="T:float.MaxValue" />.
        /// </returns>
        public static float Float()
        {
            return Float(float.MaxValue);
        }

        /// <summary>
        ///     Returns a random 32-bit floating-point number greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 32-bit floating-point number greater than or equal to 0,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes zero,
        ///     but not <paramref name="maxValue" />.
        /// </returns>
        public static float Float(float maxValue)
        {
            return Float(0, maxValue);
        }

        /// <summary>
        ///     Returns a random 32-bit floating-point number greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 32-bit floating-point number greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static float Float(float minValue, float maxValue)
        {
            return Float(minValue, maxValue, Scale.Flat);
        }

        /// <summary>
        ///     Returns a random 32-bit floating-point number greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />, using the given <see cref="T:Scale" />.
        /// </summary>
        /// <returns>
        ///     A 32-bit floating-point number greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static float Float(float minValue, float maxValue, Scale scale)
        {
            return Rand.GetRandomFloat(minValue, maxValue, scale);
        }

        /// <summary>
        ///     Returns a random 32-bit floating-point number greater than or equal to 0,
        ///     and strictly less than <see cref="T:Single.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     Returns a random 32-bit floating-point number greater than or equal to 0,
        ///     and strictly less than <see cref="T:Single.MaxValue" />; that is,
        ///     the range of return values includes zero,
        ///     but not <see cref="T:Single.MaxValue" />.
        /// </returns>
        public static float Single()
        {
            return Float();
        }

        /// <summary>
        ///     Returns a random 32-bit floating-point number greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 32-bit floating-point number greater than or equal to 0,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes 0 but not <paramref name="maxValue" />.
        /// </returns>
        public static float Single(float maxValue)
        {
            return Float(maxValue);
        }

        /// <summary>
        ///     Returns a random 32-bit floating-point number greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 32-bit floating-point number greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static float Single(float minValue, float maxValue)
        {
            return Float(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random 32-bit floating-point number greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />, using the given <see cref="T:Scale" />.
        /// </summary>
        /// <returns>
        ///     A 32-bit floating-point number greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static float Single(float minValue, float maxValue, Scale scale)
        {
            return Float(minValue, maxValue, scale);
        }

        /// <summary>
        ///     Returns a random 32-bit signed integer greater than or equal to 0,
        ///     and strictly less than <see cref="T:Int32.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     Returns a random 32-bit signed integer greater than or equal to 0,
        ///     and strictly less than <see cref="T:Int32.MaxValue" />; that is,
        ///     the range of return values includes zero,
        ///     but not <see cref="T:Int32.MaxValue" />.
        /// </returns>
        public static int Int()
        {
            return Int(int.MaxValue);
        }

        /// <summary>
        ///     Returns a random 32-bit signed integer greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer greater than or equal to 0,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes 0 but not <paramref name="maxValue" />.
        /// </returns>
        public static int Int(int maxValue)
        {
            return Int(0, maxValue);
        }

        /// <summary>
        ///     Returns a random 32-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static int Int(int minValue, int maxValue)
        {
            return Int(minValue, maxValue, Scale.Flat);
        }

        /// <summary>
        ///     Returns a random 32-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />, using the given <see cref="T:Scale" />.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static int Int(int minValue, int maxValue, Scale scale)
        {
            return Rand.GetRandomInt(minValue, maxValue, scale);
        }

        /// <summary>
        ///     Returns a random 32-bit signed integer greater than or equal to 0,
        ///     and strictly less than <see cref="T:Int32.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     Returns a random 32-bit signed integer greater than or equal to 0,
        ///     and strictly less than <see cref="T:Int32.MaxValue" />; that is,
        ///     the range of return values includes zero,
        ///     but not <see cref="T:Int32.MaxValue" />.
        /// </returns>
        public static int Int32()
        {
            return Int();
        }

        /// <summary>
        ///     Returns a random 32-bit signed integer greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer greater than or equal to 0,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes 0 but not <paramref name="maxValue" />.
        /// </returns>
        public static int Int32(int maxValue)
        {
            return Int(maxValue);
        }

        /// <summary>
        ///     Returns a random 32-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static int Int32(int minValue, int maxValue)
        {
            return Int(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random 32-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />, using the given <see cref="T:Scale" />.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static int Int32(int minValue, int maxValue, Scale scale)
        {
            return Int(minValue, maxValue, scale);
        }

        /// <summary>
        ///     Returns a random 64-bit signed integer greater than or equal to 0,
        ///     and strictly less than <see cref="T:Long.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     A 64-bit signed integer greater than or equal to 0,
        ///     and strictlyless than <see cref="T:Long.MaxValue" />; that is,
        ///     the range of return values includes zero,
        ///     but not <see cref="T:Long.MaxValue" />.
        /// </returns>
        public static long Long()
        {
            return Long(long.MaxValue);
        }

        /// <summary>
        ///     Returns a random 64-bit signed integer greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 64-bit signed integer greater than or equal to 0, and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes 0 but not <paramref name="maxValue" />.
        /// </returns>
        public static long Long(long maxValue)
        {
            return Long(0, maxValue);
        }

        /// <summary>
        ///     Returns a random 64-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 64-bit signed integer greater than or equal to <paramref name="minValue" />, and less than
        ///     <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static long Long(long minValue, long maxValue)
        {
            return Long(minValue, maxValue, Scale.Flat);
        }

        /// <summary>
        ///     Returns a random 64-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />, using the given <see cref="T:Scale" />.
        /// </summary>
        /// <returns>
        ///     A 64-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static long Long(long minValue, long maxValue, Scale scale)
        {
            return Rand.GetRandomLong(minValue, maxValue, scale);
        }

        /// <summary>
        ///     Returns a random 64-bit signed integer greater than or equal to 0,
        ///     and strictly less than <see cref="T:Int64.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     A 64-bit signed integer greater than or equal to 0,
        ///     and strictly less than <see cref="T:Int64.MaxValue" />; that is,
        ///     the range of return values includes zero,
        ///     but not <see cref="T:Int64.MaxValue" />.
        /// </returns>
        public static long Int64()
        {
            return Long();
        }

        /// <summary>
        ///     Returns a random 64-bit signed integer greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 64-bit signed integer greater than or equal to 0, and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes 0 but not <paramref name="maxValue" />.
        /// </returns>
        public static long Int64(long maxValue)
        {
            return Long(maxValue);
        }

        /// <summary>
        ///     Returns a random 64-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 64-bit signed integer greater than or equal to <paramref name="minValue" />, and less than
        ///     <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static long Int64(long minValue, long maxValue)
        {
            return Long(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random 64-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />, using the given <see cref="T:Scale" />.
        /// </summary>
        /// <returns>
        ///     A 64-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static long Int64(long minValue, long maxValue, Scale scale)
        {
            return Long(minValue, maxValue, scale);
        }

        /// <summary>
        ///     Returns an 8-bit signed integer greater than or equal to -128, and strictly less than 128.
        /// </summary>
        /// <returns>
        ///     An 8-bit signed integer greater than or equal to -128, and strictly less than 128.
        /// </returns>
        public static sbyte Sbyte()
        {
            return Sbyte(sbyte.MaxValue);
        }

        /// <summary>
        ///     Returns an 8-bit signed integer greater than or equal to 0,
        ///     and strictly less than a given maximum value.
        /// </summary>
        /// <returns>
        ///     An 8-bit signed integer greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </returns>
        public static sbyte Sbyte(sbyte maxValue)
        {
            return Sbyte(0, maxValue);
        }

        /// <summary>
        ///     Returns an 8-bit signed integer greater than or equal to the given minimum,
        ///     and strictly less than the given maximum.
        /// </summary>
        /// <returns>
        ///     An 8-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </returns>
        public static sbyte Sbyte(sbyte minValue, sbyte maxValue)
        {
            return Rand.GetRandomSByte(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random 16-bit signed integer greater than or equal to 0,
        ///     and strictly less than <see cref="P:short.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     A 16-bit signed integer greater than or equal to 0,
        ///     and strictly less than <see cref="P:short.MaxValue" />; that is,
        ///     the range of return values includes zero,
        ///     but not <see cref="P:short.MaxValue" />.
        /// </returns>
        public static short Short()
        {
            return Short(short.MaxValue);
        }

        /// <summary>
        ///     Returns a random 16-bit signed integer greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 16-bit signed integer greater than or equal to 0,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes 0 but not <paramref name="maxValue" />.
        /// </returns>
        public static short Short(short maxValue)
        {
            return Short(0, maxValue);
        }

        /// <summary>
        ///     Returns a random 16-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 16-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static short Short(short minValue, short maxValue)
        {
            return Short(minValue, maxValue, Scale.Flat);
        }

        /// <summary>
        ///     Returns a random 16-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />, using the given <see cref="T:Scale" />.
        /// </summary>
        /// <returns>
        ///     A 16-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static short Short(short minValue, short maxValue, Scale scale)
        {
            return Rand.GetRandomShort(minValue, maxValue, scale);
        }

        /// <summary>
        ///     Returns a random 16-bit signed integer greater than or equal to 0,
        ///     and strictly less than <see cref="P:Int16.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     A 16-bit signed integer greater than or equal to 0,
        ///     and strictly less than <see cref="P:Int16.MaxValue" />; that is,
        ///     the range of return values includes zero,
        ///     but not <see cref="P:Int16.MaxValue" />.
        /// </returns>
        public static short Int16()
        {
            return Short();
        }

        /// <summary>
        ///     Returns a random 16-bit signed integer greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 16-bit signed integer greater than or equal to 0,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes 0 but not <paramref name="maxValue" />.
        /// </returns>
        public static short Int16(short maxValue)
        {
            return Short(maxValue);
        }

        /// <summary>
        ///     Returns a random 16-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     A 16-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static short Int16(short minValue, short maxValue)
        {
            return Short(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random 16-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />, using the given <see cref="T:Scale" />.
        /// </summary>
        /// <returns>
        ///     A 16-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public static short Int16(short minValue, short maxValue, Scale scale)
        {
            return Short(minValue, maxValue, scale);
        }

        /// <summary>
        ///     Returns a number that indicates a sign, either -1 or 1, chosen at random.
        /// </summary>
        /// <returns>
        ///     A value indicating the sign of a number, either -1 or 1.
        /// </returns>
        public static int Sign()
        {
            return Sign(false);
        }

        /// <summary>
        ///     Returns a value (chosen at random) indicating the sign of a number.
        ///     <list type="table">
        ///         <listheader>
        ///             <term>value</term>
        ///             <description>Meaning</description>
        ///         </listheader>
        ///         <item>
        ///             <term>-1</term>
        ///             <description>value is less than zero.</description>
        ///         </item>
        ///         <item>
        ///             <term>0</term>
        ///             <description>value is zero.</description>
        ///         </item>
        ///         <item>
        ///             <term>1</term>
        ///             <description>value is greater than zero.</description>
        ///         </item>
        ///     </list>
        ///     <para> </para>
        /// </summary>
        /// <returns>
        ///     A value (chosen at random) indicating the sign of a number.
        ///     <list type="table">
        ///         <listheader>
        ///             <term>value</term>
        ///             <description>Meaning</description>
        ///         </listheader>
        ///         <item>
        ///             <term>-1</term>
        ///             <description>value is less than zero.</description>
        ///         </item>
        ///         <item>
        ///             <term>0</term>
        ///             <description>value is zero.</description>
        ///         </item>
        ///         <item>
        ///             <term>1</term>
        ///             <description>value is greater than zero.</description>
        ///         </item>
        ///     </list>
        /// </returns>
        /// <param name="includeZero">
        ///     A parameter indicating whether the return values should 0.
        ///     <para>
        ///         Set this to <c>true</c> (or simply use the
        ///         <see cref="M:Sign" /> method without
        ///         the <paramref name="includeZero" /> parameter)
        ///         if you only want -1 or 1.
        ///     </para>
        /// </param>
        public static int Sign(bool includeZero)
        {
            return Rand.GetRandomSign(includeZero);
        }

        /// <summary>
        ///     Returns a random unsigned 32-bit integer greater than or equal to 0,
        ///     and strictly less than <see cref="P:uint.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 32-bit integer than or equal to 0,
        ///     and strictly less than <see cref="P:uint.MaxValue" />; that is,
        ///     the range of return values includes 0
        ///     but not <see cref="P:uint.MaxValue" />.
        /// </returns>
        public static uint UInt()
        {
            return UInt(uint.MaxValue);
        }

        /// <summary>
        ///     Returns a random unsigned 32-bit integer greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 32-bit integer than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes 0
        ///     but not <paramref name="maxValue" />.
        /// </returns>
        public static uint UInt(uint maxValue)
        {
            return UInt(0, maxValue);
        }

        /// <summary>
        ///     Returns a random unsigned 32-bit integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 32-bit integer than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" />
        ///     but not <paramref name="maxValue" />.
        /// </returns>
        public static uint UInt(uint minValue, uint maxValue)
        {
            return Rand.GetRandomUInt(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random unsigned 32-bit integer greater than or equal to 0,
        ///     and strictly less than <see cref="P:uint.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 32-bit integer than or equal to 0,
        ///     and strictly less than <see cref="P:uint.MaxValue" />; that is,
        ///     the range of return values includes 0
        ///     but not <see cref="P:uint.MaxValue" />.
        /// </returns>
        public static uint UInt32()
        {
            return UInt();
        }

        /// <summary>
        ///     Returns a random unsigned 32-bit integer greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 32-bit integer than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes 0
        ///     but not <paramref name="maxValue" />.
        /// </returns>
        public static uint UInt32(uint maxValue)
        {
            return UInt(maxValue);
        }

        /// <summary>
        ///     Returns a random unsigned 32-bit integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 32-bit integer than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" />
        ///     but not <paramref name="maxValue" />.
        /// </returns>
        public static uint UInt32(uint minValue, uint maxValue)
        {
            return UInt(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random unsigned 64-bit integer greater than or equal to 0,
        ///     and strictly less than <see cref="P:ulong.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 64-bit integer than or equal to 0,
        ///     and strictly less than <see cref="P:ulong.MaxValue" />; that is,
        ///     the range of return values includes 0
        ///     but not <see cref="P:ulong.MaxValue" />.
        /// </returns>
        public static ulong ULong()
        {
            return ULong(ulong.MaxValue);
        }

        /// <summary>
        ///     Returns a random unsigned 64-bit integer greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 64-bit integer than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes 0
        ///     but not <paramref name="maxValue" />.
        /// </returns>
        public static ulong ULong(ulong maxValue)
        {
            return ULong(0, maxValue);
        }

        /// <summary>
        ///     Returns a random unsigned 64-bit integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 64-bit integer than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" />
        ///     but not <paramref name="maxValue" />.
        /// </returns>
        public static ulong ULong(ulong minValue, ulong maxValue)
        {
            return Rand.GetRandomULong(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random unsigned 64-bit integer greater than or equal to 0,
        ///     and strictly less than <see cref="P:UInt64.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 64-bit integer than or equal to 0,
        ///     and strictly less than <see cref="P:UInt64.MaxValue" />; that is,
        ///     the range of return values includes 0
        ///     but not <see cref="P:UInt64.MaxValue" />.
        /// </returns>
        public static ulong UInt64()
        {
            return ULong();
        }

        /// <summary>
        ///     Returns a random unsigned 64-bit integer greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 64-bit integer than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes 0
        ///     but not <paramref name="maxValue" />.
        /// </returns>
        public static ulong UInt64(ulong maxValue)
        {
            return ULong(maxValue);
        }

        /// <summary>
        ///     Returns a random unsigned 64-bit integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 64-bit integer than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" />
        ///     but not <paramref name="maxValue" />.
        /// </returns>
        public static ulong UInt64(ulong minValue, ulong maxValue)
        {
            return ULong(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random unsigned 16-bit integer greater than or equal to 0,
        ///     and strictly less than <see cref="P:ushort.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 16-bit integer than or equal to 0,
        ///     and strictly less than <see cref="P:ushort.MaxValue" />; that is,
        ///     the range of return values includes 0
        ///     but not <see cref="P:ushort.MaxValue" />.
        /// </returns>
        public static ushort UShort()
        {
            return UShort(ushort.MaxValue);
        }

        /// <summary>
        ///     Returns a random unsigned 16-bit integer greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 16-bit integer than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes 0
        ///     but not <paramref name="maxValue" />.
        /// </returns>
        public static ushort UShort(ushort maxValue)
        {
            return UShort(0, maxValue);
        }

        /// <summary>
        ///     Returns a random unsigned 16-bit integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 16-bit integer than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" />
        ///     but not <paramref name="maxValue" />.
        /// </returns>
        public static ushort UShort(ushort minValue, ushort maxValue)
        {
            return Rand.GetRandomUShort(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random unsigned 16-bit integer greater than or equal to 0,
        ///     and strictly less than <see cref="P:UInt16.MaxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 16-bit integer than or equal to 0,
        ///     and strictly less than <see cref="P:UInt16.MaxValue" />; that is,
        ///     the range of return values includes 0
        ///     but not <see cref="P:UInt16.MaxValue" />.
        /// </returns>
        public static ushort UInt16()
        {
            return UShort();
        }

        /// <summary>
        ///     Returns a random unsigned 16-bit integer greater than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 16-bit integer than or equal to 0,
        ///     and strictly less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes 0
        ///     but not <paramref name="maxValue" />.
        /// </returns>
        public static ushort UInt16(ushort maxValue)
        {
            return UShort(maxValue);
        }

        /// <summary>
        ///     Returns a random unsigned 16-bit integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     An unsigned 16-bit integer than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" />
        ///     but not <paramref name="maxValue" />.
        /// </returns>
        public static ushort UInt16(ushort minValue, ushort maxValue)
        {
            return UShort(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="T:DateTime" />, within the
        ///     range of 1 January 1970 to present day.
        /// </summary>
        /// <returns>
        ///     A random <see cref="T:DateTime" />, within the
        ///     range of 1 January 1995 to present day.
        /// </returns>
        public static DateTime DateTime()
        {
            return Rand.GetRandomDateTime(new DateTime(1970, 1, 1), System.DateTime.Now);
        }

        /// <summary>
        ///     Returns a random <see cref="T:DateTime" />, within the
        ///     date range defined by <paramref name="earliest" />
        ///     to <paramref name="latest" />.
        /// </summary>
        /// <returns>
        ///     A random <see cref="T:DateTime" />, within the
        ///     date range defined by <paramref name="earliest" />
        ///     to <paramref name="latest" />.
        /// </returns>
        public static DateTime DateTime(DateTime earliest, DateTime latest)
        {
            return Rand.GetRandomDateTime(earliest, latest);
        }

        /// <summary>
        ///     Returns a random alphanumeric
        ///     <see cref="T:String" />, between 0 and 20 digits long.
        /// </summary>
        /// <returns>
        ///     An alphanumeric <see cref="T:String" />, between 0 and 20 digits long.
        /// </returns>
        public static string String()
        {
            return String(20);
        }

        /// <summary>
        ///     Returns
        ///     a <see cref="T:String" /> of random length
        ///     (between 0 and 20 digits long),
        ///     generated from the given set of characters.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:String" /> of random length
        ///     (between 0 and 20 digits long),
        ///     generated from the given set of characters.
        /// </returns>
        public static string String(string characterSet)
        {
            return String(20, characterSet);
        }

        /// <summary>
        ///     Returns
        ///     an alphanumeric <see cref="T:String" /> of random length
        ///     (between 0 and <paramref name="maxLength" /> letters).
        /// </summary>
        /// <returns>
        ///     An alphanumeric <see cref="T:String" /> of random length
        ///     (between 0 and <paramref name="maxLength" /> letters).
        /// </returns>
        public static string String(uint maxLength)
        {
            return String(0, maxLength, Sets.AlphaNumeric);
        }

        /// <summary>
        ///     Returns
        ///     a <see cref="T:String" /> of random length
        ///     (between 0 and <paramref name="maxLength" /> letters),
        ///     generated from the given set of characters.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:String" /> of random length
        ///     (between 0 and <paramref name="maxLength" /> letters),
        ///     generated from the given set of characters.
        /// </returns>
        public static string String(uint maxLength, string characterSet)
        {
            return String(0, maxLength, characterSet);
        }

        /// <summary>
        ///     Returns an  alphanumeric <see cref="T:String" /> of random length
        ///     (between <paramref name="minLength" /> and
        ///     <paramref name="maxLength" /> letters long).
        /// </summary>
        /// <returns>
        ///     An alphanumeric <see cref="T:String" /> of random length
        ///     (between <paramref name="minLength" /> and
        ///     <paramref name="maxLength" /> letters long).
        /// </returns>
        public static string String(uint minLength, uint maxLength)
        {
            return String(minLength, maxLength, Sets.AlphaNumeric);
        }

        /// <summary>
        ///     Returns a <see cref="T:String" /> of random length
        ///     (between <paramref name="minLength" /> and
        ///     <paramref name="maxLength" /> letters long),
        ///     generated from the given set of characters.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:String" /> of random length
        ///     (between <paramref name="minLength" /> and
        ///     <paramref name="maxLength" /> letters long).
        ///     generated from the given set of characters.
        /// </returns>
        public static string String(uint minLength, uint maxLength, string characterSet)
        {
            return Rand.GetRandomString(minLength, maxLength, characterSet);
        }

        /// <summary>
        ///     Returns a random <see cref="T:Char" />,
        ///     from the <see cref="P:Cush.Testing.Sets.AtomChars" /> character set.
        /// </summary>
        /// <returns>
        ///     A random <see cref="T:Char" />,
        ///     from the <see cref="P:Cush.Testing.Sets.AtomChars" /> character set.
        /// </returns>
        public static char Char()
        {
            return Char(Sets.AtomChars);
        }

        /// <summary>
        ///     Returns a random <see cref="T:Char" />,
        ///     from the given character set.
        /// </summary>
        /// <returns>
        ///     A random <see cref="T:Char" />,
        ///     from the given character set.
        /// </returns>
        public static char Char(char[] characterSet)
        {
            return Rand.GetRandomChar(characterSet);
        }

        /// <summary>
        ///     Returns a random <see cref="T:Char" />,
        ///     from the given character set.
        /// </summary>
        /// <returns>
        ///     A random <see cref="T:Char" />,
        ///     from the given character set.
        /// </returns>
        public static char Char(string characterSet)
        {
            return Char(characterSet.ToCharArray());
        }

        /// <summary>
        ///     Returns a random <see cref="T:char[]" />,
        ///     between 0 and 20 characters long,
        ///     where the elements are taken from the
        ///     <see cref="P:Cush.Testing.Sets.AtomChars" /> character set.
        /// </summary>
        /// <returns>
        ///     A random <see cref="T:char[]" />,
        ///     between 0 and 20 characters long,
        ///     where the elements are taken from the
        ///     <see cref="P:Cush.Testing.Sets.AtomChars" /> character set.
        /// </returns>
        public static char[] CharArray()
        {
            return CharArray(0, 20, Sets.AtomChars.ToCharArray());
        }

        /// <summary>
        ///     Returns a random <see cref="T:char[]" />,
        ///     between 0 and 20 characters long,
        ///     where the elements are taken from the
        ///     given character set.
        /// </summary>
        /// <returns>
        ///     A random <see cref="T:char[]" />,
        ///     between 0 and 20 characters long,
        ///     where the elements are taken from the
        ///     given character set.
        /// </returns>
        public static char[] CharArray(char[] characterSet)
        {
            return CharArray(0, 20, characterSet);
        }

        /// <summary>
        ///     Returns a random <see cref="T:char[]" />,
        ///     between 0 and 20 characters long,
        ///     where the elements are taken from the
        ///     given character set.
        /// </summary>
        /// <returns>
        ///     A random <see cref="T:char[]" />,
        ///     between 0 and 20 characters long,
        ///     where the elements are taken from the
        ///     given character set.
        /// </returns>
        public static char[] CharArray(string characterSet)
        {
            return CharArray(0, 20, characterSet.ToCharArray());
        }

        /// <summary>
        ///     Returns a random <see cref="T:char[]" />
        ///     of the given length,
        ///     where the elements are taken from the
        ///     <see cref="P:Cush.Testing.Sets.AtomChars" /> character set.
        /// </summary>
        /// <returns>
        ///     A random <see cref="T:char[]" />
        ///     of the given length,
        ///     where the elements are taken from the
        ///     <see cref="P:Cush.Testing.Sets.AtomChars" /> character set.
        /// </returns>
        public static char[] CharArray(uint length)
        {
            return CharArray(length, length, Sets.AtomChars.ToCharArray());
        }

        /// <summary>
        ///     Returns a random <see cref="T:char[]" />
        ///     of the given length,
        ///     where the elements are taken from the
        ///     given character set.
        /// </summary>
        /// <returns>
        ///     A random <see cref="T:char[]" />
        ///     of the given length,
        ///     where the elements are taken from the
        ///     given character set.
        /// </returns>
        public static char[] CharArray(uint length, string characterSet)
        {
            return CharArray(length, length, characterSet.ToCharArray());
        }

        /// <summary>
        ///     Returns a random <see cref="T:char[]" />
        ///     of the given length,
        ///     where the elements are taken from the
        ///     given character set.
        /// </summary>
        /// <returns>
        ///     A random <see cref="T:char[]" />
        ///     of the given length,
        ///     where the elements are taken from the
        ///     given character set.
        /// </returns>
        public static char[] CharArray(uint length, char[] characterSet)
        {
            return CharArray(length, length, characterSet);
        }

        /// <summary>
        ///     Returns a random <see cref="T:char[]" />,
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" /> characters long,
        ///     where the elements are taken from the
        ///     <see cref="P:Cush.Testing.Sets.AtomChars" /> character set.
        /// </summary>
        /// <returns>
        ///     A random <see cref="T:char[]" />,
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" /> characters long,
        ///     where the elements are taken from the
        ///     <see cref="P:Cush.Testing.Sets.AtomChars" /> character set.
        /// </returns>
        public static char[] CharArray(uint minLength, uint maxLength)
        {
            return CharArray(minLength, maxLength, Sets.AtomChars.ToCharArray());
        }

        /// <summary>
        ///     Returns a random <see cref="T:char[]" />,
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" /> characters long,
        ///     where the elements are taken from the
        ///     given character set.
        /// </summary>
        /// <returns>
        ///     A random <see cref="T:char[]" />,
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" /> characters long,
        ///     where the elements are taken from the
        ///     given character set.
        /// </returns>
        public static char[] CharArray(uint minLength, uint maxLength, char[] characterSet)
        {
            return Rand.GetRandomCharArray(minLength, maxLength, characterSet);
        }

        /// <summary>
        ///     Returns a random <see cref="T:char[]" />,
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" /> characters long,
        ///     where the elements are taken from the
        ///     given character set.
        /// </summary>
        /// <returns>
        ///     A random <see cref="T:char[]" />,
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" /> characters long,
        ///     where the elements are taken from the
        ///     given character set.
        /// </returns>
        public static char[] CharArray(uint minLength, uint maxLength, string characterSet)
        {
            return CharArray(minLength, maxLength, characterSet.ToCharArray());
        }

        #region Generic Random Objects

        /// <summary>
        ///     Adds a new type to the random function repository.
        /// </summary>
        /// <typeparam name="TResult">The type to add.</typeparam>
        /// <param name="method">The method to add.</param>
        public static void AddType<TResult>(Func<TResult> method)
        {
            Repo.AddMethod(method);
        }

        /// <summary>
        ///     Adds a new type to the random function repository.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TResult">The type to add.</typeparam>
        /// <param name="method">The method to add.</param>
        public static void AddType<T1, TResult>(Func<T1, TResult> method)
        {
            Repo.AddMethod(method);
        }

        /// <summary>
        ///     Adds a new type to the random function repository.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult">The type to add.</typeparam>
        /// <param name="method">The method to add.</param>
        public static void AddType<T1, T2, TResult>(Func<T1, T2, TResult> method)
        {
            Repo.AddMethod(method);
        }

        /// <summary>
        ///     Adds a new type to the random function repository.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult">The type to add.</typeparam>
        /// <param name="method">The method to add.</param>
        public static void AddType<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> method)
        {
            Repo.AddMethod(method);
        }

        /// <summary>
        ///     Adds a new type to the random function repository.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult">The type to add.</typeparam>
        /// <param name="method">The method to add.</param>
        public static void AddType<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> method)
        {
            Repo.AddMethod(method);
        }

        /// <summary>
        ///     Adds a new type to the random function repository.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TResult">The type to add.</typeparam>
        /// <param name="method">The method to add.</param>
        public static void AddType<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> method)
        {
            Repo.AddMethod(method);
        }


        /// <summary>
        ///     Get a random object of type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type of object to retrieve.</typeparam>
        /// <param name="args">The arguments to pass to the generating method.</param>
        /// <exception cref="ArgumentException">
        ///     Will throw an ArgumentException if the repository does not contain a generating method for the
        ///     given type (add one using the <see cref="M:AddType" /> method), or if the arguments passed do
        ///     not match the signature of the generating method.
        /// </exception>
        public static T Object<T>(params object[] args)
        {
            return (T) Repo.GetValue<T>(args);
        }


        private static T GetRandomEnum<T>()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Given type T is not an enum.");

            var values = System.Enum.GetValues(typeof(T));
            return (T)values.GetValue(Int(values.Length));
        }

        #endregion
    }
}