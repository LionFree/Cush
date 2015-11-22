//using System;
//using System.ComponentModel;
//using JetBrains.Annotations;

//namespace meh.meh
//{
//    internal class Logger_NeverBrowsable
//    {
//        #region Trace() overloads

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level.
//        /// </summary>
//        /// <param name="value">A <see langword="object" /> to be written.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Trace(object value)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, "{0}", new[] {value});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="value">A <see langword="object" /> to be written.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Trace(IFormatProvider formatProvider, object value)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, formatProvider, "{0}", new[] {value});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified parameters.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing format items.</param>
//        /// <param name="arg1">First argument to format.</param>
//        /// <param name="arg2">Second argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(string message, object arg1, object arg2)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, message, new[] {arg1, arg2});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified parameters.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing format items.</param>
//        /// <param name="arg1">First argument to format.</param>
//        /// <param name="arg2">Second argument to format.</param>
//        /// <param name="arg3">Third argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(string message, object arg1, object arg2, object arg3)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, message, new[] {arg1, arg2, arg3});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(IFormatProvider formatProvider, string message, bool argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Trace(string message, bool argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(IFormatProvider formatProvider, string message, char argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Trace(string message, char argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(IFormatProvider formatProvider, string message, byte argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Trace(string message, byte argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(IFormatProvider formatProvider, string message, string argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Trace(string message, string argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(IFormatProvider formatProvider, string message, int argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Trace(string message, int argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(IFormatProvider formatProvider, string message, long argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Trace(string message, long argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(IFormatProvider formatProvider, string message, float argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Trace(string message, float argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(IFormatProvider formatProvider, string message, double argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Trace(string message, double argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(IFormatProvider formatProvider, string message, decimal argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Trace(string message, decimal argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(IFormatProvider formatProvider, string message, object argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, formatProvider, message, new[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Trace(string message, object argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, message, new[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(IFormatProvider formatProvider, string message, sbyte argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(string message, sbyte argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(IFormatProvider formatProvider, string message, uint argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(string message, uint argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(IFormatProvider formatProvider, string message, ulong argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Trace(string message, ulong argument)
//        {
//            if (IsTraceEnabled)
//            {
//                WriteToTargets(LogLevel.Trace, message, new object[] {argument});
//            }
//        }

//        #endregion

//        #region Debug() overloads

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level.
//        /// </summary>
//        /// <param name="value">A <see langword="object" /> to be written.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Debug(object value)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, "{0}", new[] {value});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="value">A <see langword="object" /> to be written.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Debug(IFormatProvider formatProvider, object value)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, formatProvider, "{0}", new[] {value});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified parameters.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing format items.</param>
//        /// <param name="arg1">First argument to format.</param>
//        /// <param name="arg2">Second argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(string message, object arg1, object arg2)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, message, new[] {arg1, arg2});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified parameters.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing format items.</param>
//        /// <param name="arg1">First argument to format.</param>
//        /// <param name="arg2">Second argument to format.</param>
//        /// <param name="arg3">Third argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(string message, object arg1, object arg2, object arg3)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, message, new[] {arg1, arg2, arg3});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(IFormatProvider formatProvider, string message, bool argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Debug(string message, bool argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(IFormatProvider formatProvider, string message, char argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Debug(string message, char argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(IFormatProvider formatProvider, string message, byte argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Debug(string message, byte argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(IFormatProvider formatProvider, string message, string argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Debug(string message, string argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(IFormatProvider formatProvider, string message, int argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Debug(string message, int argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(IFormatProvider formatProvider, string message, long argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Debug(string message, long argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(IFormatProvider formatProvider, string message, float argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Debug(string message, float argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(IFormatProvider formatProvider, string message, double argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Debug(string message, double argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(IFormatProvider formatProvider, string message, decimal argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Debug(string message, decimal argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(IFormatProvider formatProvider, string message, object argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, formatProvider, message, new[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Debug(string message, object argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, message, new[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(IFormatProvider formatProvider, string message, sbyte argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(string message, sbyte argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(IFormatProvider formatProvider, string message, uint argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(string message, uint argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(IFormatProvider formatProvider, string message, ulong argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Debug(string message, ulong argument)
//        {
//            if (IsDebugEnabled)
//            {
//                WriteToTargets(LogLevel.Debug, message, new object[] {argument});
//            }
//        }

//        #endregion

//        #region Info() overloads

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level.
//        /// </summary>
//        /// <param name="value">A <see langword="object" /> to be written.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Info(object value)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, "{0}", new[] {value});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="value">A <see langword="object" /> to be written.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Info(IFormatProvider formatProvider, object value)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, formatProvider, "{0}", new[] {value});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified parameters.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing format items.</param>
//        /// <param name="arg1">First argument to format.</param>
//        /// <param name="arg2">Second argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(string message, object arg1, object arg2)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, message, new[] {arg1, arg2});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified parameters.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing format items.</param>
//        /// <param name="arg1">First argument to format.</param>
//        /// <param name="arg2">Second argument to format.</param>
//        /// <param name="arg3">Third argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(string message, object arg1, object arg2, object arg3)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, message, new[] {arg1, arg2, arg3});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(IFormatProvider formatProvider, string message, bool argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Info(string message, bool argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(IFormatProvider formatProvider, string message, char argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Info(string message, char argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(IFormatProvider formatProvider, string message, byte argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Info(string message, byte argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(IFormatProvider formatProvider, string message, string argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Info(string message, string argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(IFormatProvider formatProvider, string message, int argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Info(string message, int argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(IFormatProvider formatProvider, string message, long argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Info(string message, long argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(IFormatProvider formatProvider, string message, float argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Info(string message, float argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(IFormatProvider formatProvider, string message, double argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Info(string message, double argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(IFormatProvider formatProvider, string message, decimal argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Info(string message, decimal argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(IFormatProvider formatProvider, string message, object argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, formatProvider, message, new[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Info(string message, object argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, message, new[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(IFormatProvider formatProvider, string message, sbyte argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(string message, sbyte argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(IFormatProvider formatProvider, string message, uint argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(string message, uint argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(IFormatProvider formatProvider, string message, ulong argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Info(string message, ulong argument)
//        {
//            if (IsInfoEnabled)
//            {
//                WriteToTargets(LogLevel.Info, message, new object[] {argument});
//            }
//        }

//        #endregion

//        #region Warn() overloads

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level.
//        /// </summary>
//        /// <param name="value">A <see langword="object" /> to be written.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Warn(object value)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, "{0}", new[] {value});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="value">A <see langword="object" /> to be written.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Warn(IFormatProvider formatProvider, object value)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, formatProvider, "{0}", new[] {value});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified parameters.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing format items.</param>
//        /// <param name="arg1">First argument to format.</param>
//        /// <param name="arg2">Second argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(string message, object arg1, object arg2)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, message, new[] {arg1, arg2});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified parameters.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing format items.</param>
//        /// <param name="arg1">First argument to format.</param>
//        /// <param name="arg2">Second argument to format.</param>
//        /// <param name="arg3">Third argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(string message, object arg1, object arg2, object arg3)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, message, new[] {arg1, arg2, arg3});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(IFormatProvider formatProvider, string message, bool argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Warn(string message, bool argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(IFormatProvider formatProvider, string message, char argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Warn(string message, char argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(IFormatProvider formatProvider, string message, byte argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Warn(string message, byte argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(IFormatProvider formatProvider, string message, string argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Warn(string message, string argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(IFormatProvider formatProvider, string message, int argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Warn(string message, int argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(IFormatProvider formatProvider, string message, long argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Warn(string message, long argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(IFormatProvider formatProvider, string message, float argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Warn(string message, float argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(IFormatProvider formatProvider, string message, double argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Warn(string message, double argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(IFormatProvider formatProvider, string message, decimal argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Warn(string message, decimal argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(IFormatProvider formatProvider, string message, object argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, formatProvider, message, new[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Warn(string message, object argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, message, new[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(IFormatProvider formatProvider, string message, sbyte argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(string message, sbyte argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(IFormatProvider formatProvider, string message, uint argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(string message, uint argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(IFormatProvider formatProvider, string message, ulong argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Warn(string message, ulong argument)
//        {
//            if (IsWarnEnabled)
//            {
//                WriteToTargets(LogLevel.Warn, message, new object[] {argument});
//            }
//        }

//        #endregion

//        #region Error() overloads

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level.
//        /// </summary>
//        /// <param name="value">A <see langword="object" /> to be written.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Error(object value)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, "{0}", new[] {value});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="value">A <see langword="object" /> to be written.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Error(IFormatProvider formatProvider, object value)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, formatProvider, "{0}", new[] {value});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified parameters.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing format items.</param>
//        /// <param name="arg1">First argument to format.</param>
//        /// <param name="arg2">Second argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(string message, object arg1, object arg2)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, message, new[] {arg1, arg2});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified parameters.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing format items.</param>
//        /// <param name="arg1">First argument to format.</param>
//        /// <param name="arg2">Second argument to format.</param>
//        /// <param name="arg3">Third argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(string message, object arg1, object arg2, object arg3)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, message, new[] {arg1, arg2, arg3});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(IFormatProvider formatProvider, string message, bool argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Error(string message, bool argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(IFormatProvider formatProvider, string message, char argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Error(string message, char argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(IFormatProvider formatProvider, string message, byte argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Error(string message, byte argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(IFormatProvider formatProvider, string message, string argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Error(string message, string argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(IFormatProvider formatProvider, string message, int argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Error(string message, int argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(IFormatProvider formatProvider, string message, long argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Error(string message, long argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(IFormatProvider formatProvider, string message, float argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Error(string message, float argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(IFormatProvider formatProvider, string message, double argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Error(string message, double argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(IFormatProvider formatProvider, string message, decimal argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Error(string message, decimal argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(IFormatProvider formatProvider, string message, object argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, formatProvider, message, new[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Error(string message, object argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, message, new[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(IFormatProvider formatProvider, string message, sbyte argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(string message, sbyte argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(IFormatProvider formatProvider, string message, uint argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(string message, uint argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(IFormatProvider formatProvider, string message, ulong argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Error(string message, ulong argument)
//        {
//            if (IsErrorEnabled)
//            {
//                WriteToTargets(LogLevel.Error, message, new object[] {argument});
//            }
//        }

//        #endregion

//        #region Fatal() overloads

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level.
//        /// </summary>
//        /// <param name="value">A <see langword="object" /> to be written.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Fatal(object value)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, "{0}", new[] {value});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="value">A <see langword="object" /> to be written.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Fatal(IFormatProvider formatProvider, object value)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, formatProvider, "{0}", new[] {value});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified parameters.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing format items.</param>
//        /// <param name="arg1">First argument to format.</param>
//        /// <param name="arg2">Second argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(string message, object arg1, object arg2)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, message, new[] {arg1, arg2});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified parameters.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing format items.</param>
//        /// <param name="arg1">First argument to format.</param>
//        /// <param name="arg2">Second argument to format.</param>
//        /// <param name="arg3">Third argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(string message, object arg1, object arg2, object arg3)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, message, new[] {arg1, arg2, arg3});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(IFormatProvider formatProvider, string message, bool argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Fatal(string message, bool argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(IFormatProvider formatProvider, string message, char argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Fatal(string message, char argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(IFormatProvider formatProvider, string message, byte argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Fatal(string message, byte argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(IFormatProvider formatProvider, string message, string argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Fatal(string message, string argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(IFormatProvider formatProvider, string message, int argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Fatal(string message, int argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(IFormatProvider formatProvider, string message, long argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Fatal(string message, long argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(IFormatProvider formatProvider, string message, float argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Fatal(string message, float argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(IFormatProvider formatProvider, string message, double argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Fatal(string message, double argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(IFormatProvider formatProvider, string message, decimal argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Fatal(string message, decimal argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(IFormatProvider formatProvider, string message, object argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, formatProvider, message, new[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Fatal(string message, object argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, message, new[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(IFormatProvider formatProvider, string message, sbyte argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(string message, sbyte argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(IFormatProvider formatProvider, string message, uint argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(string message, uint argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(IFormatProvider formatProvider, string message, ulong argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] {argument});
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Fatal(string message, ulong argument)
//        {
//            if (IsFatalEnabled)
//            {
//                WriteToTargets(LogLevel.Fatal, message, new object[] {argument});
//            }
//        }

//        #endregion



//        #region Log() overloads

//        // the following code has been automatically generated by a PERL script

//        #region Log() overloads



//        /// <summary>
//        ///     Writes the diagnostic message at the specified level.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="value">A <see langword="object" /> to be written.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Log(LogLevel level, object value)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, "{0}", new[] { value });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="value">A <see langword="object" /> to be written.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Log(LogLevel level, IFormatProvider formatProvider, object value)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, formatProvider, "{0}", new[] { value });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified parameters.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="message">A <see langword="string" /> containing format items.</param>
//        /// <param name="arg1">First argument to format.</param>
//        /// <param name="arg2">Second argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, string message, object arg1, object arg2)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, message, new[] { arg1, arg2 });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified parameters.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="message">A <see langword="string" /> containing format items.</param>
//        /// <param name="arg1">First argument to format.</param>
//        /// <param name="arg2">Second argument to format.</param>
//        /// <param name="arg3">Third argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, string message, object arg1, object arg2, object arg3)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, message, new[] { arg1, arg2, arg3 });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, IFormatProvider formatProvider, string message, bool argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, formatProvider, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Log(LogLevel level, string message, bool argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, IFormatProvider formatProvider, string message, char argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, formatProvider, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Log(LogLevel level, string message, char argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, IFormatProvider formatProvider, string message, byte argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, formatProvider, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Log(LogLevel level, string message, byte argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, IFormatProvider formatProvider, string message, string argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, formatProvider, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Log(LogLevel level, string message, string argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, IFormatProvider formatProvider, string message, int argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, formatProvider, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Log(LogLevel level, string message, int argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, IFormatProvider formatProvider, string message, long argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, formatProvider, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Log(LogLevel level, string message, long argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, IFormatProvider formatProvider, string message, float argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, formatProvider, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Log(LogLevel level, string message, float argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, IFormatProvider formatProvider, string message, double argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, formatProvider, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Log(LogLevel level, string message, double argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, IFormatProvider formatProvider, string message, decimal argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, formatProvider, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Log(LogLevel level, string message, decimal argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, IFormatProvider formatProvider, string message, object argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, formatProvider, message, new[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public void Log(LogLevel level, string message, object argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, message, new[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, IFormatProvider formatProvider, string message, sbyte argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, formatProvider, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, string message, sbyte argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, IFormatProvider formatProvider, string message, uint argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, formatProvider, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, string message, uint argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it
//        ///     with the supplied format provider.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, IFormatProvider formatProvider, string message, ulong argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, formatProvider, message, new object[] { argument });
//            }
//        }

//        /// <summary>
//        ///     Writes the diagnostic message at the specified level using the specified value as a parameter.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        /// <param name="message">A <see langword="string" /> containing one format item.</param>
//        /// <param name="argument">The argument to format.</param>
//        [CLSCompliant(false)]
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        [StringFormatMethod("message")]
//        public void Log(LogLevel level, string message, ulong argument)
//        {
//            if (IsEnabled(level))
//            {
//                WriteToTargets(level, message, new object[] { argument });
//            }
//        }

//        #endregion

//        #endregion
//    }
//}