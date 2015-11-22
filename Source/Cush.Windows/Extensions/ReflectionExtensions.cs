#region License

//   Copyright 2010 John Sheehan
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 

#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Cush.Windows
{
    /// <summary>
    ///     Reflection extensions
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        ///     Retrieves the linker timestamp from the PE header embedded in the assembly.
        /// </summary>
        public static DateTime RetrieveLinkerTimestamp(this Assembly assembly)
        {
            var filePath = assembly.Location;
            const int peHeaderOffset = 60;
            const int linkerTimestampOffset = 8;
            var bytes = new byte[2048];
            Stream stream = null;

            try
            {
                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                stream.Read(bytes, 0, 2048);
            }
            finally
            {
                if (null != stream)
                    stream.Close();
            }

            var startIndex = BitConverter.ToInt32(bytes, peHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(bytes, startIndex + linkerTimestampOffset);
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(secondsSince1970);
            return dateTime.ToLocalTime();
        }

        /// <summary>
        ///     Retrieve an attribute from an assembly
        /// </summary>
        /// <typeparam name="T">Type of attribute to retrieve</typeparam>
        /// <param name="assembly">Assembly to retrieve attribute from</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this Assembly assembly) where T : Attribute
        {
            //var attributes =
            //    assembly.GetCustomAttributes(typeof(T), false);
            //return attributes.Length == 0 ? default(T) : ((T)attributes[0]);
            return Attribute.GetCustomAttribute(assembly, typeof (T)) as T;
        }

        /// <summary>
        ///     Retrieve an attribute from a member (property)
        /// </summary>
        /// <typeparam name="T">Type of attribute to retrieve</typeparam>
        /// <param name="prop">Member to retrieve attribute from</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this MemberInfo prop) where T : Attribute
        {
            return Attribute.GetCustomAttribute(prop, typeof (T)) as T;
        }

        /// <summary>
        ///     Retrieve an attribute from a type
        /// </summary>
        /// <typeparam name="T">Type of attribute to retrieve</typeparam>
        /// <param name="type">Type to retrieve attribute from</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            return Attribute.GetCustomAttribute(type, typeof (T)) as T;
        }

        /// <summary>
        ///     Queries a <see cref="System.Type" /> for a list of all <see cref="Attribute" />s of a specific type.
        /// </summary>
        /// <param name="typeWithAttributes">
        ///     The <see cref="Type" /> to query.
        /// </param>
        /// <typeparam name="T">
        ///     The <see cref="Type" /> of <see cref="Attribute" /> to search for.
        /// </typeparam>
        /// <returns>
        ///     A <see cref="System.Collections.Generic.IEnumerable{T}" /> list of all <see cref="Attribute" />s of the desired
        ///     type on the
        ///     <see
        ///         cref="System.Type" />
        ///     .
        /// </returns>
        public static IEnumerable<T> GetAttributes<T>(this Type typeWithAttributes) where T : Attribute
        {
            // Try to find the configuration attribute for the 
            // default logger if it exists
            var configAttributes = Attribute.GetCustomAttributes(typeWithAttributes, typeof (T), false);
            if (configAttributes.Length <= 0) yield break;

            // get just the first one
            foreach (T attribute in configAttributes)
            {
                yield return attribute;
            }
        }

        ///// <summary>
        /////     Queries a <see cref="System.Type" /> for the first <see cref="Attribute" /> of a specific type.
        ///// </summary>
        ///// <param name="typeWithAttributes">
        /////     The <see cref="Type" /> to query.
        ///// </param>
        ///// <typeparam name="T">
        /////     The <see cref="Type" /> of <see cref="Attribute" /> to search for.
        ///// </typeparam>
        ///// <returns>
        /////     The first <see cref="Attribute" /> of the desired type on the <see cref="System.Type" />.
        ///// </returns>
        //public static T GetAttribute<T>(this Type typeWithAttributes) where T : Attribute
        //{
        //    return GetAttributes<T>(typeWithAttributes).FirstOrDefault();
        //}


        /// <summary>
        ///     Checks a type to see if it derives from a raw generic (e.g. List[[]])
        /// </summary>
        /// <param name="toCheck"></param>
        /// <param name="generic"></param>
        /// <returns></returns>
        public static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic)
        {
            while (toCheck != typeof (object))
            {
                //if (null == toCheck) return false;
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;

                if (generic == cur)
                {
                    return true;
                }

                toCheck = toCheck.BaseType;
            }

            return false;
        }

        public static object ChangeType(this object source, Type newType)
        {
#if FRAMEWORK && !PocketPC
            return Convert.ChangeType(source, newType);
#else
            return Convert.ChangeType(source, newType, null);
#endif
        }

        public static object ChangeType(this object source, Type newType, CultureInfo culture)
        {
#if FRAMEWORK || SILVERLIGHT || WINDOWS_PHONE
            return Convert.ChangeType(source, newType, culture);
#else
            return Convert.ChangeType(source, newType, null);
#endif
        }

        /// <summary>
        ///     Find a value from a System.Enum by trying several possible variants
        ///     of the string value of the enum.
        /// </summary>
        /// <param name="type">Type of enum</param>
        /// <param name="value">Value for which to search</param>
        /// <param name="culture">The culture used to calculate the name variants</param>
        /// <returns></returns>
        public static object FindEnumValue(this Type type, string value, CultureInfo culture)
        {
#if FRAMEWORK && !PocketPC
            var ret = Enum.GetValues(type)
                          .Cast<Enum>()
                          .FirstOrDefault(v => v.ToString()
                                                .GetNameVariants(culture)
                                                .Contains(value, StringComparer.Create(culture, true)));

            if (ret == null)
            {
                var enumValueAsUnderlyingType = Convert.ChangeType(value, Enum.GetUnderlyingType(type), culture);

                if (enumValueAsUnderlyingType != null && Enum.IsDefined(type, enumValueAsUnderlyingType))
                {
                    ret = (Enum)Enum.ToObject(type, enumValueAsUnderlyingType);
                }
            }

            return ret;
#else
            return Enum.Parse(type, value, true);
#endif
        }
    }
}