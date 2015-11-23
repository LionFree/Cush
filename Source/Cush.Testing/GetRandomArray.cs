using System;
using Cush.Testing.RandomObjects;
using Microsoft.Win32;
// ReSharper disable InconsistentNaming

namespace Cush.Testing
{
    public static class GetRandomArray
    {
        [ThreadStatic] private static readonly RandomArrayGenerator Rand;

        static GetRandomArray()
        {
            Rand = RandomArrayGenerator.GetInstance();
        }
        
        #region Decimals

        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:decimal"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:decimal" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:decimal"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:decimal" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static decimal[] OfDecimals()
        {
            return OfDecimals(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:decimal" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:decimal" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:decimal" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:decimal" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static decimal[] OfDecimals(uint maxLength)
        {
            return OfDecimals(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:double" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:double" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:decimal" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:decimal" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static decimal[] OfDecimals(uint minLength, uint maxLength)
        {
            return OfDecimals(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:decimal" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:decimal" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:decimal" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:decimal" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static decimal[] OfDecimals(uint minLength, uint maxLength, decimal maxValue)
        {
            return OfDecimals(minLength, maxLength, 0, maxValue);
        }

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
        /// </summary>
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
        /// </returns>
        public static decimal[] OfDecimals(uint minLength, uint maxLength, decimal minValue, decimal maxValue)
        {
            return OfDecimals(minLength, maxLength, minValue, maxValue, Scale.Flat);
        }

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
        public static decimal[] OfDecimals(uint minLength, uint maxLength, decimal minValue, decimal maxValue, Scale scale)
        {
            return Rand.GetRandomArrayOfDecimals(minLength, maxLength, minValue, maxValue, scale);
        }
        #endregion
        
        #region Doubles

        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:double"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:double" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:double"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:double" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static double[] OfDoubles()
        {
            return OfDoubles(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:double" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:double" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:double" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:double" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static double[] OfDoubles(uint maxLength)
        {
            return OfDoubles(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:double" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:double" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:double" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:double" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static double[] OfDoubles(uint minLength, uint maxLength)
        {
            return OfDoubles(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:double" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:double" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:double" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:double" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static double[] OfDoubles(uint minLength, uint maxLength, double maxValue)
        {
            return OfDoubles(minLength, maxLength, 0, maxValue);
        }

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
        /// </summary>
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
        /// </returns>
        public static double[] OfDoubles(uint minLength, uint maxLength, double minValue, double maxValue)
        {
            return OfDoubles(minLength, maxLength, minValue, maxValue, Scale.Flat);
        }

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
        public static double[] OfDoubles(uint minLength, uint maxLength, double minValue, double maxValue, Scale scale)
        {
            return Rand.GetRandomArrayOfDoubles(minLength, maxLength, minValue, maxValue, scale);
        }
#endregion

        #region Floats

        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:float"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:float" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:float"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:float" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static float[] OfFloats()
        {
            return OfFloats(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:float" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:float" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:float" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:float" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static float[] OfFloats(uint maxLength)
        {
            return OfFloats(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:float" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:float" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:float" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:float" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static float[] OfFloats(uint minLength, uint maxLength)
        {
            return OfFloats(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:float" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:float" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:float" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:float" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static float[] OfFloats(uint minLength, uint maxLength, float maxValue)
        {
            return OfFloats(minLength, maxLength, 0, maxValue);
        }

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
        /// </summary>
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
        /// </returns>
        public static float[] OfFloats(uint minLength, uint maxLength, float minValue, float maxValue)
        {
            return OfFloats(minLength, maxLength, minValue, maxValue, Scale.Flat);
        }

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
        public static float[] OfFloats(uint minLength, uint maxLength, float minValue, float maxValue, Scale scale)
        {
            return Rand.GetRandomArrayOfFloats(minLength, maxLength, minValue, maxValue, scale);
        }




        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:Single"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:Single" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:Single"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:Single" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static float[] OfSingles()
        {
            return OfSingles(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:Single" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:Single" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:Single" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:Single" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static float[] OfSingles(uint maxLength)
        {
            return OfSingles(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:Single" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:Single" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:Single" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:Single" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static float[] OfSingles(uint minLength, uint maxLength)
        {
            return OfSingles(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:Single" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:Single" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:Single" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:Single" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static float[] OfSingles(uint minLength, uint maxLength, float maxValue)
        {
            return OfSingles(minLength, maxLength, 0, maxValue);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:Single" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:Single" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:Single" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:Single" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static float[] OfSingles(uint minLength, uint maxLength, float minValue, float maxValue)
        {
            return OfSingles(minLength, maxLength, minValue, maxValue, Scale.Flat);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:Single" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:Single" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        ///     The distribution of the <see cref="T:Single" />s is modified by the <paramref name="scale" />.
        /// </summary>
        /// <seealso cref="Scale" />
        /// <returns>
        ///     An array of <see cref="T:Single" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:Single" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        ///     The distribution of the <see cref="T:Single" />s is modified by the <paramref name="scale" />.
        /// </returns>
        public static float[] OfSingles(uint minLength, uint maxLength, float minValue, float maxValue, Scale scale)
        {
            return OfFloats(minLength, maxLength, minValue, maxValue, scale);
        }
        
        #endregion

        #region Shorts

        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:short"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:short"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static short[] OfShorts()
        {
            return OfShorts(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:short" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:short" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static short[] OfShorts(uint maxLength)
        {
            return OfShorts(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:short" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:short" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static short[] OfShorts(uint minLength, uint maxLength)
        {
            return OfShorts(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:short" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:short" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static short[] OfShorts(uint minLength, uint maxLength, short maxValue)
        {
            return OfShorts(minLength, maxLength, 0, maxValue);
        }

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
        /// </summary>
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
        /// </returns>
        public static short[] OfShorts(uint minLength, uint maxLength, short minValue, short maxValue)
        {
            return OfShorts(minLength, maxLength, minValue, maxValue, Scale.Flat);
        }

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
        public static short[] OfShorts(uint minLength, uint maxLength, short minValue, short maxValue, Scale scale)
        {
            return Rand.GetRandomArrayOfShorts(minLength, maxLength, minValue, maxValue, scale);
        }


        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:short"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:short"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static short[] OfInt16s()
        {
            return OfInt16s(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:short" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:short" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static short[] OfInt16s(uint maxLength)
        {
            return OfInt16s(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:short" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:short" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static short[] OfInt16s(uint minLength, uint maxLength)
        {
            return OfInt16s(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:short" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:short" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:short" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static short[] OfInt16s(uint minLength, uint maxLength, short maxValue)
        {
            return OfInt16s(minLength, maxLength, 0, maxValue);
        }

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
        /// </summary>
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
        /// </returns>
        public static short[] OfInt16s(uint minLength, uint maxLength, short minValue, short maxValue)
        {
            return OfInt16s(minLength, maxLength, minValue, maxValue, Scale.Flat);
        }

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
        public static short[] OfInt16s(uint minLength, uint maxLength, short minValue, short maxValue, Scale scale)
        {
            return OfShorts(minLength, maxLength, minValue, maxValue, scale);
        }
        #endregion

        #region Ints

        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:int"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:int"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static int[] OfInts()
        {
            return OfInts(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:int" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:int" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static int[] OfInts(uint maxLength)
        {
            return OfInts(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:int" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:int" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static int[] OfInts(uint minLength, uint maxLength)
        {
            return OfInts(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:int" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:int" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static int[] OfInts(uint minLength, uint maxLength, int maxValue)
        {
            return OfInts(minLength, maxLength, 0, maxValue);
        }

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
        /// </summary>
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
        /// </returns>
        public static int[] OfInts(uint minLength, uint maxLength, int minValue, int maxValue)
        {
            return OfInts(minLength, maxLength, minValue, maxValue, Scale.Flat);
        }

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
        public static int[] OfInts(uint minLength, uint maxLength, int minValue, int maxValue, Scale scale)
        {
            return Rand.GetRandomArrayOfInts(minLength, maxLength, minValue, maxValue, scale);
        }


        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:int"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:int"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static int[] OfInt32s()
        {
            return OfInt32s(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:int" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:int" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static int[] OfInt32s(uint maxLength)
        {
            return OfInt32s(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:int" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:int" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static int[] OfInt32s(uint minLength, uint maxLength)
        {
            return OfInt32s(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:int" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:int" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:int" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static int[] OfInt32s(uint minLength, uint maxLength, int maxValue)
        {
            return OfInt32s(minLength, maxLength, 0, maxValue);
        }

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
        /// </summary>
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
        /// </returns>
        public static int[] OfInt32s(uint minLength, uint maxLength, int minValue, int maxValue)
        {
            return OfInt32s(minLength, maxLength, minValue, maxValue, Scale.Flat);
        }

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
        public static int[] OfInt32s(uint minLength, uint maxLength, int minValue, int maxValue, Scale scale)
        {
            return OfInts(minLength, maxLength, minValue, maxValue, scale);
        }
        #endregion

        #region Longs

        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:long"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:long"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static long[] OfLongs()
        {
            return OfLongs(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:long" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:long" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static long[] OfLongs(uint maxLength)
        {
            return OfLongs(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:long" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:long" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static long[] OfLongs(uint minLength, uint maxLength)
        {
            return OfLongs(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:long" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:long" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static long[] OfLongs(uint minLength, uint maxLength, long maxValue)
        {
            return OfLongs(minLength, maxLength, 0, maxValue);
        }

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
        /// </summary>
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
        /// </returns>
        public static long[] OfLongs(uint minLength, uint maxLength, long minValue, long maxValue)
        {
            return OfLongs(minLength, maxLength, minValue, maxValue, Scale.Flat);
        }

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
        public static long[] OfLongs(uint minLength, uint maxLength, long minValue, long maxValue, Scale scale)
        {
            return Rand.GetRandomArrayOfLongs(minLength, maxLength, minValue, maxValue, scale);
        }

        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:long"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:long"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static long[] OfInt64s()
        {
            return OfInt64s(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:long" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:long" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static long[] OfInt64s(uint maxLength)
        {
            return OfInt64s(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:long" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:long" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static long[] OfInt64s(uint minLength, uint maxLength)
        {
            return OfInt64s(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:long" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:long" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:long" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static long[] OfInt64s(uint minLength, uint maxLength, long maxValue)
        {
            return OfInt64s(minLength, maxLength, 0, maxValue);
        }

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
        /// </summary>
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
        /// </returns>
        public static long[] OfInt64s(uint minLength, uint maxLength, long minValue, long maxValue)
        {
            return OfInt64s(minLength, maxLength, minValue, maxValue, Scale.Flat);
        }

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
        public static long[] OfInt64s(uint minLength, uint maxLength, long minValue, long maxValue, Scale scale)
        {
            return OfLongs(minLength, maxLength, minValue, maxValue, scale);
        }

        #endregion

        #region UInts

        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:uint"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:uint" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:uint"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:uint" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static uint[] OfUInt32s()
        {
            return OfUInt32s(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:uint" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:uint" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:uint" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:uint" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static uint[] OfUInt32s(uint maxLength)
        {
            return OfUInt32s(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:uint" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:uint" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:uint" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:uint" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static uint[] OfUInt32s(uint minLength, uint maxLength)
        {
            return OfUInt32s(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:uint" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:uint" /> has a random value
        ///         greater than or equal to 0,
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
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static uint[] OfUInt32s(uint minLength, uint maxLength, uint maxValue)
        {
            return OfUInt32s(minLength, maxLength, 0, maxValue);
        }

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
        public static uint[] OfUInt32s(uint minLength, uint maxLength, uint minValue, uint maxValue)
        {
            return OfUInts(minLength, maxLength, minValue, maxValue);
        }

        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:uint"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:uint" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:uint"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:uint" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static uint[] OfUInts()
        {
            return OfUInts(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:uint" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:uint" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:uint" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:uint" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static uint[] OfUInts(uint maxLength)
        {
            return OfUInts(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:uint" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:uint" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:uint" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:uint" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static uint[] OfUInts(uint minLength, uint maxLength)
        {
            return OfUInts(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:uint" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:uint" /> has a random value
        ///         greater than or equal to 0,
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
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static uint[] OfUInts(uint minLength, uint maxLength, uint maxValue)
        {
            return OfUInts(minLength, maxLength, 0, maxValue);
        }

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
        public static uint[] OfUInts(uint minLength, uint maxLength, uint minValue, uint maxValue)
        {
            return Rand.GetRandomArrayOfUInts(minLength, maxLength, minValue, maxValue);
        }

        #endregion

        #region UShorts

        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:ushort"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:ushort" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:ushort"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:ushort" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static ushort[] OfUInt16s()
        {
            return OfUInt16s(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:ushort" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ushort" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:ushort" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ushort" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static ushort[] OfUInt16s(uint maxLength)
        {
            return OfUInt16s(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:ushort" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ushort" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:ushort" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ushort" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static ushort[] OfUInt16s(uint minLength, uint maxLength)
        {
            return OfUInt16s(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:ushort" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ushort" /> has a random value
        ///         greater than or equal to 0,
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
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static ushort[] OfUInt16s(uint minLength, uint maxLength, ushort maxValue)
        {
            return OfUInt16s(minLength, maxLength, 0, maxValue);
        }

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
        public static ushort[] OfUInt16s(uint minLength, uint maxLength, ushort minValue, ushort maxValue)
        {
            return OfUShorts(minLength, maxLength, minValue, maxValue);
        }

        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:ushort"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:ushort" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:ushort"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:ushort" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static ushort[] OfUShorts()
        {
            return OfUShorts(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:ushort" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ushort" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:ushort" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ushort" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static ushort[] OfUShorts(uint maxLength)
        {
            return OfUShorts(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:ushort" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ushort" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:ushort" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ushort" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static ushort[] OfUShorts(uint minLength, uint maxLength)
        {
            return OfUShorts(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:ushort" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ushort" /> has a random value
        ///         greater than or equal to 0,
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
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static ushort[] OfUShorts(uint minLength, uint maxLength, ushort maxValue)
        {
            return OfUShorts(minLength, maxLength, 0, maxValue);
        }

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
        public static ushort[] OfUShorts(uint minLength, uint maxLength, ushort minValue, ushort maxValue)
        {
            return Rand.GetRandomArrayOfUShorts(minLength, maxLength, minValue, maxValue);
        }

        #endregion

        #region ULongs

        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:ulong"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:ulong" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:ulong"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:ulong" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static ulong[] OfULongs()
        {
            return OfULongs(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:ulong" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ulong" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:ulong" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ulong" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static ulong[] OfULongs(uint maxLength)
        {
            return OfULongs(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:ulong" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ulong" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:ulong" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ulong" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static ulong[] OfULongs(uint minLength, uint maxLength)
        {
            return OfULongs(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:ulong" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ulong" /> has a random value
        ///         greater than or equal to 0,
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
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static ulong[] OfULongs(uint minLength, uint maxLength, ulong maxValue)
        {
            return OfULongs(minLength, maxLength, 0, maxValue);
        }

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
        public static ulong[] OfULongs(uint minLength, uint maxLength, ulong minValue, ulong maxValue)
        {
            return Rand.GetRandomArrayOfULongs(minLength, maxLength, minValue, maxValue);
        }



        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:ulong"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:ulong" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:ulong"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:ulong" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static ulong[] OfUInt64s()
        {
            return OfUInt64s(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:ulong" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ulong" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:ulong" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ulong" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static ulong[] OfUInt64s(uint maxLength)
        {
            return OfUInt64s(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:ulong" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ulong" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:ulong" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ulong" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static ulong[] OfUInt64s(uint minLength, uint maxLength)
        {
            return OfUInt64s(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:ulong" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:ulong" /> has a random value
        ///         greater than or equal to 0,
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
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static ulong[] OfUInt64s(uint minLength, uint maxLength, ulong maxValue)
        {
            return OfUInt64s(minLength, maxLength, 0, maxValue);
        }

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
        public static ulong[] OfUInt64s(uint minLength, uint maxLength, ulong minValue, ulong maxValue)
        {
            return OfULongs(minLength, maxLength, minValue, maxValue);
        }

        #endregion

        #region Bytes

        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:byte"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:byte" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:byte"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:byte" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static byte[] OfBytes()
        {
            return OfBytes(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:byte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:byte" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:byte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:byte" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static byte[] OfBytes(uint maxLength)
        {
            return OfBytes(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:byte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:byte" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:byte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:byte" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static byte[] OfBytes(uint minLength, uint maxLength)
        {
            return OfBytes(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:byte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:byte" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:byte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:byte" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static byte[] OfBytes(uint minLength, uint maxLength, byte maxValue)
        {
            return OfBytes(minLength, maxLength, 0, maxValue);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:byte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:byte" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:byte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:byte" /> has a random value
        ///         greater than or equal to <paramref name="minValue" />,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static byte[] OfBytes(uint minLength, uint maxLength, byte minValue, byte maxValue)
        {
            return Rand.GetRandomArrayOfBytes(minLength, maxLength, minValue, maxValue);
        }

        #endregion

        #region SBytes

        /// <summary>
        ///     Returns
        ///     an array of between 
        ///     0 and 20 randomly chosen <see cref="T:sbyte"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:sbyte" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of between 
        ///     0 and 20 randomly chosen <see cref="T:sbyte"/>s,
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />.
        ///     <para>
        ///         Each <see cref="T:sbyte" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static sbyte[] OfSBytes()
        {
            return OfSBytes(20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:sbyte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:sbyte" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:sbyte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between 0 and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:sbyte" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static sbyte[] OfSBytes(uint maxLength)
        {
            return OfSBytes(0, maxLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:sbyte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:sbyte" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:sbyte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:sbyte" /> has a random value
        ///         greater than or equal to 0,
        ///         and strictly less than 20.
        ///     </para>
        /// </returns>
        public static sbyte[] OfSBytes(uint minLength, uint maxLength)
        {
            return OfSBytes(minLength, maxLength, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:sbyte" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minLength" /> and <paramref name="maxLength" />.
        ///     <para>
        ///         Each <see cref="T:sbyte" /> has a random value
        ///         greater than or equal to 0,
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
        ///         greater than or equal to 0,
        ///         and strictly less than <paramref name="maxValue" />.
        ///     </para>
        /// </returns>
        public static sbyte[] OfSBytes(uint minLength, uint maxLength, sbyte maxValue)
        {
            return OfSBytes(minLength, maxLength, 0, maxValue);
        }

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
        public static sbyte[] OfSBytes(uint minLength, uint maxLength, sbyte minValue, sbyte maxValue)
        {
            return Rand.GetRandomArrayOfSBytes(minLength, maxLength, minValue, maxValue);
        }

        #endregion

        #region Booleans

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:Boolean" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:Boolean" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        /// </returns>
        public static bool[] OfBooleans()
        {
            return OfBooleans(20);
        }

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:Boolean" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxLength" /> randomly chosen elements.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:Boolean" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxLength" /> randomly chosen 
        ///     <see cref="T:char" />s from the given character set.
        /// </returns>
        public static bool[] OfBooleans(uint maxLength)
        {
            return OfBooleans(0, maxLength);
        }

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:Boolean" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between <paramref name="minLength" /> and <paramref name="maxLength" />
        ///     randomly chosen elements.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:Boolean" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between <paramref name="minLength" /> and <paramref name="maxLength" />
        ///     randomly chosen elements.
        /// </returns>
        public static bool[] OfBooleans(uint minLength, uint maxLength)
        {
            return Rand.GetRandomArrayOfBooleans(minLength, maxLength);
        }

        #endregion

        #region RegistryKeys

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:Microsoft.Win32.RegistryKey" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:Microsoft.Win32.RegistryKey" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        /// </returns>
        public static RegistryKey[] OfRegistryKeys()
        {
            return OfRegistryKeys(20);
        }

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:Microsoft.Win32.RegistryKey" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxLength" /> randomly chosen elements.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:Microsoft.Win32.RegistryKey" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxLength" /> randomly chosen elements.
        /// </returns>
        public static RegistryKey[] OfRegistryKeys(uint maxLength)
        {
            return OfRegistryKeys(0, maxLength);
        }

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:Microsoft.Win32.RegistryKey" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between <paramref name="minLength" /> and <paramref name="maxLength" />
        ///     randomly chosen elements.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:Microsoft.Win32.RegistryKey" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between <paramref name="minLength" /> and <paramref name="maxLength" />
        ///     randomly chosen elements.
        /// </returns>
        public static RegistryKey[] OfRegistryKeys(uint minLength, uint maxLength)
        {
            return Rand.GetRandomArrayOfRegistryKeys(minLength, maxLength);
        }

        #endregion

        #region Exceptions

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:System.Exception" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:System.Exception" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        /// </returns>
        public static Exception[] OfExceptions()
        {
            return OfExceptions(20);
        }

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:System.Exception" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxLength" /> randomly chosen elements.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:System.Exception" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxLength" /> randomly chosen elements.
        /// </returns>
        public static Exception[] OfExceptions(uint maxLength)
        {
            return OfExceptions(0, maxLength);
        }

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:System.Exception" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between <paramref name="minLength" /> and <paramref name="maxLength" />
        ///     randomly chosen elements.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:System.Exception" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between <paramref name="minLength" /> and <paramref name="maxLength" />
        ///     randomly chosen elements.
        /// </returns>
        public static Exception[] OfExceptions(uint minLength, uint maxLength)
        {
            return Rand.GetRandomArrayOfExceptions(minLength, maxLength);
        }

        #endregion

        #region Strings

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the <see cref="P:Cush.Testing.Sets.AlphaNumeric" /> set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the <see cref="P:Cush.Testing.Sets.AlphaNumeric" /> set of characters.
        ///     </para>
        /// </returns>
        public static string[] OfStrings()
        {
            return OfStrings(Sets.AlphaNumeric);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </returns>
        public static string[] OfStrings(char[] characterSet)
        {
            return OfStrings(new string(characterSet));
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </returns>
        public static string[] OfStrings(string characterSet)
        {
            return Rand.GetRandomArrayOfStrings(0, 20, 0, 20, characterSet);
        }


        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxListLength" /> randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxListLength" /> randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the <see cref="P:Cush.Testing.Sets.AlphaNumeric" /> set of characters.
        ///     </para>
        /// </returns>
        public static string[] OfStrings(uint maxListLength)
        {
            return OfStrings(0, maxListLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxListLength" /> randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxListLength" /> randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </returns>
        public static string[] OfStrings(uint maxListLength, string characterSet)
        {
            return OfStrings(0, maxListLength, characterSet);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxListLength" /> randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxListLength" /> randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </returns>
        public static string[] OfStrings(uint maxListLength, char[] characterSet)
        {
            return OfStrings(0, maxListLength, characterSet);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the <see cref="P:Cush.Testing.Sets.AlphaNumeric" /> set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the <see cref="P:Cush.Testing.Sets.AlphaNumeric" /> set of characters.
        ///     </para>
        /// </returns>
        public static string[] OfStrings(uint minListLength, uint maxListLength)
        {
            return OfStrings(minListLength, maxListLength, 0, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </returns>
        public static string[] OfStrings(uint minListLength, uint maxListLength, char[] characterSet)
        {
            return OfStrings(minListLength, maxListLength, 0, 20, characterSet);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </returns>
        public static string[] OfStrings(uint minListLength, uint maxListLength, string characterSet)
        {
            return OfStrings(minListLength, maxListLength, 0, 20, characterSet);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between <paramref name="minStringLength" /> and <paramref name="maxStringLength" /> characters,
        ///         and is generated from the <see cref="P:Cush.Testing.Sets.AlphaNumeric" /> set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:String" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between <paramref name="minStringLength" /> and <paramref name="maxStringLength" /> characters,
        ///         and is generated from the <see cref="P:Cush.Testing.Sets.AlphaNumeric" /> set of characters.
        ///     </para>
        /// </returns>
        public static string[] OfStrings(uint minListLength, uint maxListLength, uint minStringLength,
            uint maxStringLength)
        {
            return OfStrings(minListLength, maxListLength, minStringLength, maxStringLength,
                Sets.AlphaNumeric);
        }

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
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between <paramref name="minStringLength" /> and <paramref name="maxStringLength" /> characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </returns>
        public static string[] OfStrings(uint minListLength, uint maxListLength, uint minStringLength,
            uint maxStringLength, char[] characterSet)
        {
            return OfStrings(minListLength, maxListLength, minStringLength, maxStringLength,
                new string(characterSet));
        }

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
        ///     <para>
        ///         Each <see cref="T:String" /> has a random length
        ///         between <paramref name="minStringLength" /> and <paramref name="maxStringLength" /> characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </returns>
        public static string[] OfStrings(uint minListLength, uint maxListLength, uint minStringLength,
            uint maxStringLength, string characterSet)
        {
            return Rand.GetRandomArrayOfStrings(minListLength, maxListLength, minStringLength, maxStringLength,
                characterSet);
        }

        #endregion

        #region CharArrays

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the <see cref="P:Cush.Testing.Sets.AlphaNumeric" /> set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the <see cref="P:Cush.Testing.Sets.AlphaNumeric" /> set of characters.
        ///     </para>
        /// </returns>
        public static char[][] OfCharArrays()
        {
            return OfCharArrays(Sets.AlphaNumeric);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </returns>
        public static char[][] OfCharArrays(char[] characterSet)
        {
            return OfCharArrays(new string(characterSet));
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </returns>
        public static char[][] OfCharArrays(string characterSet)
        {
            return OfCharArrays(20, characterSet);
        }


        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxListLength" /> randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxListLength" /> randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the <see cref="P:Cush.Testing.Sets.AlphaNumeric" /> set of characters.
        ///     </para>
        /// </returns>
        public static char[][] OfCharArrays(uint maxListLength)
        {
            return OfCharArrays(0, maxListLength);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxListLength" /> randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxListLength" /> randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </returns>
        public static char[][] OfCharArrays(uint maxListLength, string characterSet)
        {
            return OfCharArrays(0, maxListLength, characterSet);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxListLength" /> randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxListLength" /> randomly chosen elements.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </returns>
        public static char[][] OfCharArrays(uint maxListLength, char[] characterSet)
        {
            return OfCharArrays(0, maxListLength, characterSet);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the <see cref="P:Cush.Testing.Sets.AlphaNumeric" /> set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the <see cref="P:Cush.Testing.Sets.AlphaNumeric" /> set of characters.
        ///     </para>
        /// </returns>
        public static char[][] OfCharArrays(uint minListLength, uint maxListLength)
        {
            return OfCharArrays(minListLength, maxListLength, 0, 20);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </returns>
        public static char[][] OfCharArrays(uint minListLength, uint maxListLength, char[] characterSet)
        {
            return OfCharArrays(minListLength, maxListLength, 0, 20, characterSet);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between 0 and 20 characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </returns>
        public static char[][] OfCharArrays(uint minListLength, uint maxListLength, string characterSet)
        {
            return OfCharArrays(minListLength, maxListLength, 0, 20, characterSet);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between <paramref name="minStringLength" /> and <paramref name="maxStringLength" /> characters,
        ///         and is generated from the <see cref="P:Cush.Testing.Sets.AlphaNumeric" /> set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between <paramref name="minStringLength" /> and <paramref name="maxStringLength" /> characters,
        ///         and is generated from the <see cref="P:Cush.Testing.Sets.AlphaNumeric" /> set of characters.
        ///     </para>
        /// </returns>
        public static char[][] OfCharArrays(uint minListLength, uint maxListLength, uint minStringLength,
            uint maxStringLength)
        {
            return OfCharArrays(minListLength, maxListLength, minStringLength, maxStringLength,
                Sets.AlphaNumeric);
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between <paramref name="minStringLength" /> and <paramref name="maxStringLength" /> characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between <paramref name="minStringLength" /> and <paramref name="maxStringLength" /> characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </returns>
        public static char[][] OfCharArrays(uint minListLength, uint maxListLength, uint minStringLength,
            uint maxStringLength, string characterSet)
        {
            return OfCharArrays(minListLength, maxListLength, minStringLength, maxStringLength,
                characterSet.ToCharArray());
        }

        /// <summary>
        ///     Returns
        ///     an array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between <paramref name="minStringLength" /> and <paramref name="maxStringLength" /> characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:char[]" />s
        ///     generated randomly by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with a number of elements randomly chosen
        ///     between <paramref name="minListLength" /> and <paramref name="maxListLength" />.
        ///     <para>
        ///         Each <see cref="T:char[]" /> has a random length
        ///         between <paramref name="minStringLength" /> and <paramref name="maxStringLength" /> characters,
        ///         and is generated from the given set of characters.
        ///     </para>
        /// </returns>
        public static char[][] OfCharArrays(uint minListLength, uint maxListLength, uint minStringLength,
            uint maxStringLength, char[] characterSet)
        {
            return Rand.GetRandomArrayOfCharArrays(minListLength, maxListLength, minStringLength, maxStringLength,
                characterSet);
        }

        #endregion

        #region Chars

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20
        ///     randomly chosen <see cref="T:char" />s
        ///     from the <see cref="P:Sets.AtomChars" /> character set.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20
        ///     randomly chosen <see cref="T:char" />s
        ///     from the <see cref="P:Sets.AtomChars" /> character set.
        /// </returns>
        public static char[] OfChars()
        {
            return OfChars(Sets.AtomChars);
        }

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20
        ///     randomly chosen <see cref="T:char" />s
        ///     from the <see cref="P:Sets.AtomChars" /> character set.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20
        ///     randomly chosen <see cref="T:char" />s
        ///     from the given character set.
        /// </returns>
        public static char[] OfChars(string characterSet)
        {
            return OfChars(20, characterSet);
        }


        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20
        ///     randomly chosen <see cref="T:char" />s
        ///     from the given character set.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and 20 
        ///     randomly chosen <see cref="T:char" />s
        ///     from the given character set.
        /// </returns>
        public static char[] OfChars(char[] characterSet)
        {
            return OfChars(20, characterSet);
        }

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxLength" /> 
        ///     randomly chosen <see cref="T:char" />s
        ///     from the <see cref="P:Sets.AtomChars" /> character set.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxLength" /> 
        ///     randomly chosen <see cref="T:char" />s
        ///     from the <see cref="P:Sets.AtomChars" /> character set.
        /// </returns>
        public static char[] OfChars(uint maxLength)
        {
            return OfChars(0, maxLength);
        }


        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxLength" /> 
        ///     randomly chosen <see cref="T:char" />s
        ///     from the given character set.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxLength" /> 
        ///     randomly chosen <see cref="T:char" />s
        ///     from the given character set.
        /// </returns>
        public static char[] OfChars(uint maxLength, string characterSet)
        {
            return OfChars(0, maxLength, characterSet);
        }

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxLength" /> 
        ///     randomly chosen <see cref="T:char" />s
        ///     from the given character set.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between 0 and <paramref name="maxLength" />
        ///     randomly chosen <see cref="T:char" />s
        ///     from the given character set.
        /// </returns>
        public static char[] OfChars(uint maxLength, char[] characterSet)
        {
            return OfChars(0, maxLength, characterSet);
        }


        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between <paramref name="minLength" /> and <paramref name="maxLength" />
        ///     randomly chosen <see cref="T:char" />s
        ///     from the <see cref="P:Sets.AtomChars" /> character set.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:char" />s generated randomly by the
        ///     <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" /> class,
        ///     with between <paramref name="minLength" /> and <paramref name="maxLength" />
        ///     randomly chosen <see cref="T:char" />s 
        ///     from the <see cref="P:Sets.AtomChars" /> character set.
        /// </returns>
        public static char[] OfChars(uint minLength, uint maxLength)
        {
            return OfChars(minLength, maxLength, Sets.AtomChars.ToCharArray());
        }

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between <paramref name="minLength" /> and <paramref name="maxLength" />
        ///     randomly chosen <see cref="T:char" />s
        ///     from the given character set.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between <paramref name="minLength" /> and <paramref name="maxLength" />
        ///     randomly chosen <see cref="T:char" />s
        ///     from the given character set.
        /// </returns>
        public static char[] OfChars(uint minLength, uint maxLength, string characterSet)
        {
            return OfChars(minLength, maxLength, characterSet.ToCharArray());
        }

        /// <summary>
        ///     Returns an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between <paramref name="minLength" /> and <paramref name="maxLength" />
        ///     randomly chosen <see cref="T:char" />s
        ///     from the given character set.
        /// </summary>
        /// <returns>
        ///     an array of
        ///     <see cref="T:char" />s generated randomly
        ///     by the <seealso cref="M:Cush.Testing.RandomObjects.RandomObjectGenerator" />,
        ///     with between <paramref name="minLength" /> and <paramref name="maxLength" />
        ///     randomly chosen <see cref="T:char" />s
        ///     from the given character set.
        /// </returns>
        public static char[] OfChars(uint minLength, uint maxLength, char[] characterSet)
        {
            return Rand.GetRandomArrayOfChars(minLength, maxLength, characterSet);
        }

        #endregion
    }
}