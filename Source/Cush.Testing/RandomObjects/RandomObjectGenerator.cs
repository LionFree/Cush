using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace Cush.Testing.RandomObjects
{
    /// <summary>
    ///     Internally thread-safe class for obtaining pseudo-random values during (unit-)testing.
    /// </summary>
    public abstract class RandomObjectGenerator
    {
        private static readonly Random LockedInstance = new LockedRandom();

        [ThreadStatic] private static readonly Random PrivatePerThreadRandom;

        static RandomObjectGenerator()
        {
            int perThreadSeed;
            lock (LockedInstance) perThreadSeed = GlobalSeedGenerator.Next();
            PrivatePerThreadRandom = new Random(perThreadSeed);
        }

        /// <summary>
        ///     The (global) seed generator.
        /// </summary>
        public static Random GlobalSeedGenerator
        {
            get { return LockedInstance; }
        }

        /// <summary>
        ///     The per-thread <see cref="T:System.Random" /> pseudo-random number generator.
        /// </summary>
        public static Random PerThreadInstance
        {
            get { return PrivatePerThreadRandom; }
        }

        /// <summary>
        ///     Returns a new instance of the <seealso cref="RandomObjectGenerator" /> class.
        /// </summary>
        /// <returns>
        ///     A new instance of the <seealso cref="RandomObjectGenerator" /> class.
        /// </returns>
        public static RandomObjectGenerator GetInstance()
        {
            return new ROGImplementation();
        }

        private class ROGImplementation : RandomObjectGenerator
        {
            public override char GetRandomChar(char[] characterSet)
            {
                if (characterSet == null)
                    throw new ArgumentNullException("characterSet");

                if (characterSet.Length == 0)
                    throw new ArgumentException("characterSet has zero length.");

                var i = PerThreadInstance.Next(characterSet.Length);
                return characterSet[i];
            }

            public override string GetRandomString(uint minLength, uint maxLength, string characterSet)
            {
                if (string.IsNullOrEmpty(characterSet))
                    throw new ArgumentException("characterSet is null or empty.");

                if (minLength > maxLength)
                    throw new ArgumentException("minLength is greater than maxLength.");

                var actualLength = GetRandomUInt(minLength, maxLength);
                var chars = new char[actualLength];
                var charSetLength = characterSet.Length;
                for (var i = 0; i < actualLength; i++)
                {
                    var pos = GetRandomInt(0, charSetLength, Scale.Flat);
                    chars[i] = characterSet[pos];
                }
                return new string(chars);
            }

            public override Exception GetRandomException()
            {
                var types = new List<Type>
                {
                    typeof (Exception),
                    typeof (AccessViolationException),
                    typeof (AggregateException),
                    typeof (ApplicationException),
                    typeof (ArgumentException),
                    typeof (ArgumentNullException),
                    typeof (ArgumentOutOfRangeException),
                    typeof (ArrayTypeMismatchException),
                    typeof (InvalidOperationException),
                    typeof (InvalidTimeZoneException)
                };

                var type = types[PerThreadInstance.Next(types.Count)];
                return (Exception) Activator.CreateInstance(type);
            }

            public override DateTime GetRandomDateTime(DateTime earliest, DateTime latest)
            {
                if (earliest.Equals(latest))
                    return earliest;

                var dayRange = (latest - earliest).Days;
                var days = dayRange == 0 ? 0 : PerThreadInstance.Next(dayRange);

                var hourRange = (latest - earliest).Hours;
                var hours = hourRange == 0 ? 0 : PerThreadInstance.Next(hourRange);

                var minRange = (latest - earliest).Minutes;
                var minutes = minRange == 0 ? 0 : PerThreadInstance.Next(minRange);

                var secRange = (latest - earliest).Seconds;
                var seconds = secRange == 0 ? 0 : PerThreadInstance.Next(secRange);

                var msRange = (latest - earliest).Milliseconds;
                var milliseconds = msRange == 0 ? 0 : PerThreadInstance.Next(msRange);

                return earliest
                    .AddDays(days)
                    .AddHours(hours)
                    .AddMinutes(minutes)
                    .AddSeconds(seconds)
                    .AddMilliseconds(milliseconds);
            }

            public override RegistryKey GetRandomRegistryKey()
            {
                var list = new List<RegistryKey>
                {
                    Registry.ClassesRoot,
                    Registry.CurrentConfig,
                    Registry.CurrentUser,
                    Registry.LocalMachine,
                    Registry.PerformanceData,
                    Registry.Users
                };

                return list[PerThreadInstance.Next(list.Count)];
            }

            public override byte GetRandomByte(byte minValue, byte maxValue)
            {
                return PerThreadInstance.NextByte(minValue, maxValue);
            }

            public override double GetRandomDouble(double minValue, double maxValue, Scale scale)
            {
                return PerThreadInstance.NextDouble(minValue, maxValue, scale);
            }

            public override decimal GetRandomDecimal(decimal minValue, decimal maxValue, Scale scale)
            {
                return PerThreadInstance.NextDecimal(minValue, maxValue, scale);
            }

            public override float GetRandomFloat(float minValue, float maxValue, Scale scale)
            {
                return PerThreadInstance.NextFloat(minValue, maxValue, scale);
            }

            public override int GetRandomInt(int minValue, int maxValue, Scale scale)
            {
                return PerThreadInstance.NextInt(minValue, maxValue, scale);
            }

            public override long GetRandomLong(long minValue, long maxValue, Scale scale)
            {
                return PerThreadInstance.NextLong(minValue, maxValue, scale);
            }

            public override short GetRandomShort(short minValue, short maxValue, Scale scale)
            {
                return PerThreadInstance.NextShort(minValue, maxValue, scale);
            }

            public override uint GetRandomUInt(uint minValue, uint maxValue)
            {
                return PerThreadInstance.NextUInt32(minValue, maxValue);
            }

            public override ushort GetRandomUShort(ushort minValue, ushort maxValue)
            {
                return PerThreadInstance.NextUInt16(minValue, maxValue);
            }

            public override ulong GetRandomULong(ulong minValue, ulong maxValue)
            {
                return PerThreadInstance.NextUInt64(minValue, maxValue);
            }

            public override sbyte GetRandomSByte(sbyte minValue, sbyte maxValue)
            {
                return PerThreadInstance.NextSByte(minValue, maxValue);
            }

            public override int GetRandomSign(bool includeZero)
            {
                if (!includeZero) return GetRandomBool() ? 1 : -1;
                return Math.Sign(GetRandomInt(-1, 2, Scale.Flat));
            }

            public override bool GetRandomBool()
            {
                return PerThreadInstance.NextBool();
            }

            public override char[] GetRandomCharArray(uint minLength, uint maxLength, char[] characterSet)
            {
                var arrayLength = GetRandomUInt(minLength, maxLength + 1);
                var stringBuilder = new StringBuilder((int) arrayLength);

                for (var i = 0; i < arrayLength; i++)
                {
                    stringBuilder.Append(GetRandomChar(characterSet));
                }

                return stringBuilder.ToString().ToCharArray();
            }
        }

        #region Randomizer Methods

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
        public abstract Exception GetRandomException();

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
        public abstract RegistryKey GetRandomRegistryKey();

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
        public abstract DateTime GetRandomDateTime(DateTime earliest, DateTime latest);

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
        public abstract char[] GetRandomCharArray(uint minLength, uint maxLength, char[] characterSet);

        /// <summary>
        ///     Returns a random boolean value (<c>true</c> or <c>false</c>).
        /// </summary>
        /// <returns>
        ///     A random boolean value (<c>true</c> or <c>false</c>).
        /// </returns>
        public abstract bool GetRandomBool();

        /// <summary>
        ///     Returns a random 128-bit decimal number greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />, using the given <see cref="T:Scale" />.
        /// </summary>
        /// <returns>
        ///     A 128-bit decimal number greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public abstract decimal GetRandomDecimal(decimal minValue, decimal maxValue, Scale scale);

        /// <summary>
        ///     Returns a random 64-bit floating-point number greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />, using the given <see cref="T:Scale" />.
        /// </summary>
        /// <returns>
        ///     A 64-bit floating-point number greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public abstract double GetRandomDouble(double minValue, double maxValue, Scale scale);

        /// <summary>
        ///     Returns a random 32-bit floating-point number greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />, using the given <see cref="T:Scale" />.
        /// </summary>
        /// <returns>
        ///     A 32-bit floating-point number greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public abstract float GetRandomFloat(float minValue, float maxValue, Scale scale);

        /// <summary>
        ///     Returns a random 64-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />, using the given <see cref="T:Scale" />.
        /// </summary>
        /// <returns>
        ///     A 64-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public abstract long GetRandomLong(long minValue, long maxValue, Scale scale);

        /// <summary>
        ///     Returns a random 16-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />, using the given <see cref="T:Scale" />.
        /// </summary>
        /// <returns>
        ///     A 16-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public abstract short GetRandomShort(short minValue, short maxValue, Scale scale);

        /// <summary>
        ///     Returns a random 32-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />, using the given <see cref="T:Scale" />.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public abstract int GetRandomInt(int minValue, int maxValue, Scale scale);

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
        public abstract uint GetRandomUInt(uint minValue, uint maxValue);

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
        public abstract ushort GetRandomUShort(ushort minValue, ushort maxValue);

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
        public abstract ulong GetRandomULong(ulong minValue, ulong maxValue);

        /// <summary>
        ///     Returns an 8-bit signed integer greater than or equal to the given minimum,
        ///     and strictly less than the given maximum.
        /// </summary>
        /// <returns>
        ///     An 8-bit signed integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </returns>
        public abstract sbyte GetRandomSByte(sbyte minValue, sbyte maxValue);

        /// <summary>
        ///     Returns a random 8-bit unsigned integer greater than or equal to <paramref name="minValue" />,
        ///     and strictly less than <paramref name="maxValue" />.
        /// </summary>
        /// <returns>
        ///     An 8-bit unsigned integer greater than or equal to <paramref name="minValue" />,
        ///     and less than <paramref name="maxValue" />; that is,
        ///     the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />.
        /// </returns>
        public abstract byte GetRandomByte(byte minValue, byte maxValue);

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
        public abstract int GetRandomSign(bool includeZero);

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
        public abstract string GetRandomString(uint minLength, uint maxLength, string characterSet);

        /// <summary>
        ///     Returns a random <see cref="T:Char" />,
        ///     from the given character set.
        /// </summary>
        /// <returns>
        ///     A random <see cref="T:Char" />,
        ///     from the given character set.
        /// </returns>
        public abstract char GetRandomChar(char[] characterSet);

        #endregion
    }
}