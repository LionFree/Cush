using System;
using Microsoft.Win32;

namespace Cush.Testing.RandomObjects
{
    /// <summary>
    ///     Generates arrays of various types, and populates them with items
    ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
    /// </summary>
    /// <returns>
    ///     Arrays of various types, and populates them with items
    ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
    /// </returns>
    public abstract class RandomArrayGenerator
    {

        private class RagImplementation : RandomArrayGenerator
        {
            public RagImplementation ()
            {
                _perThreadInstance = RandomObjectGenerator.GetInstance();
            }

            public override string[] GetRandomArrayOfStrings(uint minListLength, uint maxListLength,
                uint minStringLength, uint maxStringLength, string characterSet)
            {
                return PopulatedList<string>(GetListLength(minListLength, maxListLength),
                    (Func<uint, uint, string, string>)_perThreadInstance.GetRandomString,
                    minStringLength, maxStringLength, characterSet);
            }

            public override char[] GetRandomArrayOfChars(uint minListLength, uint maxListLength, char[] characterSet)
            {
                return PopulatedList<char>(GetListLength(minListLength, maxListLength),
                    (Func<char[],char>) _perThreadInstance.GetRandomChar,
                    characterSet);
            }

            public override char[][] GetRandomArrayOfCharArrays(uint minListLength, uint maxListLength, uint minLength,
                uint maxLength, char[] characterSet)
            {
                return PopulatedList<char[]>(GetListLength(minListLength, maxListLength),
                    (Func<uint, uint, char[], char[]>) _perThreadInstance.GetRandomCharArray,
                    minLength, maxLength, characterSet);
            }

            public override RegistryKey[] GetRandomArrayOfRegistryKeys(uint minListLength, uint maxListLength)
            {
                return PopulatedList<RegistryKey>(GetListLength(minListLength, maxListLength),
                    (Func<RegistryKey>) _perThreadInstance.GetRandomRegistryKey,
                    null);
            }

            public override Exception[] GetRandomArrayOfExceptions(uint minListLength, uint maxListLength)
            {
                return PopulatedList<Exception>(GetListLength(minListLength, maxListLength),
                    (Func<Exception>) _perThreadInstance.GetRandomException,
                    null);
            }

            public override bool[] GetRandomArrayOfBooleans(uint minListLength, uint maxListLength)
            {
                return PopulatedList<bool>(GetListLength(minListLength, maxListLength),
                    (Func<bool>) _perThreadInstance.GetRandomBool,
                    null);
            }

            public override byte[] GetRandomArrayOfBytes(uint minLength, uint maxLength, byte minValue, byte maxValue)
            {
                return PopulatedList<byte>(GetListLength(minLength, maxLength),
                    (Func<byte,byte,byte>)_perThreadInstance.GetRandomByte,
                    minValue, maxValue);
            }

            public override double[] GetRandomArrayOfDoubles(uint minLength, uint maxLength, double minValue,
                double maxValue, Scale scale)
            {
                return PopulatedList<double>(GetListLength(minLength, maxLength),
                    (Func<double, double, Scale, double>) _perThreadInstance.GetRandomDouble,
                    minValue, maxValue, scale);
            }

            public override decimal[] GetRandomArrayOfDecimals(uint minListLength, uint maxListLength, decimal minValue,
                decimal maxValue, Scale scale)
            {
                return PopulatedList<decimal>(GetListLength(minListLength, maxListLength),
                    (Func<decimal, decimal, Scale, decimal>)_perThreadInstance.GetRandomDecimal,
                    minValue, maxValue, scale);
            }

            public override float[] GetRandomArrayOfFloats(uint minListLength, uint maxListLength, float minValue,
                float maxValue, Scale scale)
            {
                return PopulatedList<float>(GetListLength(minListLength, maxListLength),
                    (Func<float, float, Scale, float>)_perThreadInstance.GetRandomFloat,
                    minValue, maxValue, scale);
            }

            public override long[] GetRandomArrayOfLongs(uint minListLength, uint maxListLength, long minValue,
                long maxValue, Scale scale)
            {
                return PopulatedList<long>(GetListLength(minListLength, maxListLength),
                    (Func<long,long,Scale,long>) _perThreadInstance.GetRandomLong,
                    minValue, maxValue, scale);
            }

            public override int[] GetRandomArrayOfInts(uint minLength, uint maxLength, int minValue, int maxValue,
                Scale scale)
            {
                return PopulatedList<int>(GetListLength(minLength, maxLength),
                    (Func<int, int, Scale, int>)_perThreadInstance.GetRandomInt,
                    minValue, maxValue, scale);
            }

            public override short[] GetRandomArrayOfShorts(uint minLength, uint maxLength, short minValue,
                short maxValue, Scale scale)
            {
                return PopulatedList<short>(GetListLength(minLength, maxLength),
                    (Func<short, short, Scale, short>)_perThreadInstance.GetRandomShort,
                    minValue, maxValue, scale);
            }

            public override uint[] GetRandomArrayOfUInts(uint minLength, uint maxLength, uint minValue, uint maxValue)
            {
                return PopulatedList<uint>(GetListLength(minLength, maxLength),
                    (Func<uint, uint, uint>)_perThreadInstance.GetRandomUInt,
                    minValue, maxValue);
            }

            public override ulong[] GetRandomArrayOfULongs(uint minLength, uint maxLength, ulong minValue,
                ulong maxValue)
            {
                return PopulatedList<ulong>(GetListLength(minLength, maxLength),
                    (Func<ulong, ulong, ulong>)_perThreadInstance.GetRandomULong,
                    minValue, maxValue);
            }

            public override ushort[] GetRandomArrayOfUShorts(uint minLength, uint maxLength, ushort minValue,
                ushort maxValue)
            {
                return PopulatedList<ushort>(GetListLength(minLength, maxLength),
                    (Func<ushort, ushort, ushort>)_perThreadInstance.GetRandomUShort,
                    minValue, maxValue);
            }

            public override sbyte[] GetRandomArrayOfSBytes(uint minLength, uint maxLength, sbyte minValue,
                sbyte maxValue)
            {
                return PopulatedList<sbyte>(GetListLength(minLength, maxLength),
                    (Func<sbyte, sbyte, sbyte>)_perThreadInstance.GetRandomSByte,
                    minValue, maxValue);
            }

            #region Reduce Repetition

            private uint GetListLength(uint minListLength, uint maxListLength)
            {
                return _perThreadInstance.GetRandomUInt(minListLength, maxListLength);
            }

            private T[] PopulatedList<T>(uint listLength, Delegate method, params object[] args)
            {
                var output = new T[listLength];
                for (var i = 0; i < listLength; i++)
                {
                    output[i] = (GetValueFromMethod<T>(method, args));
                }
                return output;
            }

            private T GetValueFromMethod<T>(Delegate method, object[] args)
            {
                var invokeable = method.Method;
                return (T) invokeable.Invoke(_perThreadInstance, args);
            }

            #endregion
        }

        #region done
        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:double" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:double" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        ///     The distribution of the <see cref="T:double" />s is modified by the <paramref name="scale" />.
        /// </summary>
        /// <seealso cref="Scale" />
        /// <returns>
        ///     An array of <see cref="T:double" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:double" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        ///     The distribution of the <see cref="T:double" />s is modified by the <paramref name="scale" />.
        /// </returns>
        public abstract double[] GetRandomArrayOfDoubles(uint minLength, uint maxLength, double minValue,
            double maxValue, Scale scale);

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:float" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:float" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        ///     The distribution of the <see cref="T:float" />s is modified by the <paramref name="scale" />.
        /// </summary>
        /// <seealso cref="Scale" />
        /// <returns>
        ///     An array of <see cref="T:float" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:float" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        ///     The distribution of the <see cref="T:float" />s is modified by the <paramref name="scale" />.
        /// </returns>
        public abstract float[] GetRandomArrayOfFloats(uint minLength, uint maxLength, float minValue, float maxValue,
            Scale scale);

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:int" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        ///     The distribution of the <see cref="T:int" />s is modified by the <paramref name="scale" />.
        /// </summary>
        /// <seealso cref="Scale" />
        /// <returns>
        ///     An array of <see cref="T:int" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        ///     The distribution of the <see cref="T:int" />s is modified by the <paramref name="scale" />.
        /// </returns>
        public abstract int[] GetRandomArrayOfInts(uint minLength, uint maxLength, int minValue, int maxValue,
            Scale scale);

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:long" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        ///     The distribution of the <see cref="T:long" />s is modified by the <paramref name="scale" />.
        /// </summary>
        /// <seealso cref="Scale" />
        /// <returns>
        ///     An array of <see cref="T:long" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        ///     The distribution of the <see cref="T:long" />s is modified by the <paramref name="scale" />.
        /// </returns>
        public abstract long[] GetRandomArrayOfLongs(uint minLength, uint maxLength, long minValue, long maxValue,
            Scale scale);

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:short" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        ///     The distribution of the <see cref="T:short" />s is modified by the <paramref name="scale" />.
        /// </summary>
        /// <seealso cref="Scale" />
        /// <returns>
        ///     An array of <see cref="T:short" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        ///     The distribution of the <see cref="T:short" />s is modified by the <paramref name="scale" />.
        /// </returns>
        public abstract short[] GetRandomArrayOfShorts(uint minLength, uint maxLength, short minValue, short maxValue,
            Scale scale);

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:uint" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:uint" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:uint" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:uint" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public abstract uint[] GetRandomArrayOfUInts(uint minLength, uint maxLength, uint minValue, uint maxValue);

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:ulong" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ulong" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:ulong" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ulong" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public abstract ulong[] GetRandomArrayOfULongs(uint minLength, uint maxLength, ulong minValue, ulong maxValue);

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:ushort" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ushort" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:ushort" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ushort" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public abstract ushort[] GetRandomArrayOfUShorts(uint minLength, uint maxLength, ushort minValue,
            ushort maxValue);

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:sbyte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:sbyte" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:sbyte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:sbyte" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public abstract sbyte[] GetRandomArrayOfSBytes(uint minLength, uint maxLength, sbyte minValue, sbyte maxValue);

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between <paramref name="minStringLength" /> and <paramref name="maxStringLength" /> characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     Each <see cref="T:String" /> has a random length
        ///     between <paramref name="minStringLength" /> and <paramref name="maxStringLength" /> characters,
        ///     and is generated from the given set of characters.
        /// </returns>
        public abstract string[] GetRandomArrayOfStrings(uint minListLength, uint maxListLength, uint minStringLength,
            uint maxStringLength, string characterSet);

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:Microsoft.Win32.RegistryKey" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:Microsoft.Win32.RegistryKey" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        /// </returns>
        public abstract RegistryKey[] GetRandomArrayOfRegistryKeys(uint minLength, uint maxLength);

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:System.Exception" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:System.Exception" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        /// </returns>
        public abstract Exception[] GetRandomArrayOfExceptions(uint minLength, uint maxLength);

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:Boolean" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:Boolean" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        /// </returns>
        public abstract bool[] GetRandomArrayOfBooleans(uint minLength, uint maxLength);

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:Byte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:Byte" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:Byte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:Byte" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public abstract byte[] GetRandomArrayOfBytes(uint minLength, uint maxLength, byte minValue, byte maxValue);

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:decimal" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:decimal" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        ///     The distribution of the <see cref="T:decimal" />s is modified by the <paramref name="scale" />.
        /// </summary>
        /// <seealso cref="Scale" />
        /// <returns>
        ///     An array of <see cref="T:decimal" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:decimal" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        ///     The distribution of the <see cref="T:decimal" />s is modified by the <paramref name="scale" />.
        /// </returns>
        public abstract decimal[] GetRandomArrayOfDecimals(uint minLength, uint maxLength, decimal minValue,
            decimal maxValue, Scale scale);

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:char" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     Each <see cref="T:char" /> is chosen from the given character set.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:char" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     Each <see cref="T:char" /> is chosen from the given character set.
        /// </returns>
        public abstract char[] GetRandomArrayOfChars(uint minLength, uint maxLength, char[] characterSet);

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of array-type elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     Each <seealso cref="T:char[]" /> includes a number of <see cref="T:char" />s
        ///     between <paramref name="minInnerLength" /> and <paramref name="maxInnerLength" />.
        ///     Each <see cref="T:char" /> is chosen from the given character set.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of array-type elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     Each <seealso cref="T:char[]" /> includes a number of <see cref="T:char" />s
        ///     between <paramref name="minInnerLength" /> and <paramref name="maxInnerLength" />.
        ///     Each <see cref="T:char" /> is chosen from the given character set.
        /// </returns>
        public abstract char[][] GetRandomArrayOfCharArrays(uint minLength, uint maxLength, uint minInnerLength,
            uint maxInnerLength, char[] characterSet);

        #endregion

        #region Standard Methods

        [ThreadStatic] private static RandomObjectGenerator _perThreadInstance;

        /// <summary>
        ///     The per-thread <see cref="T:System.Random" /> pseudo-random number generator.
        /// </summary>
        public static RandomObjectGenerator PerThreadInstance
        {
            get
            {
                lock (_perThreadInstance)
                {
                    return _perThreadInstance ?? (_perThreadInstance = RandomObjectGenerator.GetInstance());
                }
            }
        }


        /// <summary>
        ///     Returns a new instance of the <seealso cref="RandomArrayGenerator" /> class.
        /// </summary>
        /// <returns>
        ///     A new instance of the <seealso cref="RandomArrayGenerator" /> class.
        /// </returns>
        public static RandomArrayGenerator GetInstance()
        {
            return new RagImplementation();
        }

        static RandomArrayGenerator()
        {
            _perThreadInstance = RandomObjectGenerator.GetInstance();
        }

        #endregion
    }
}