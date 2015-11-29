using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Cush.Common.Logging.Internal
{
    public abstract class ExceptionWriter
    {
        public static ExceptionWriter Default
        {
            get { return new ExceptionWriterImplementation(ExceptionParser.Default); }
        }

        /// <summary>
        ///     Writes all the details of an exception to the log stream.
        /// </summary>
        /// <param name="exception">The exception to write.</param>
        /// <param name="writer">
        ///     The <see cref="StreamWriter" /> (e.g., file) to write to.
        /// </param>
        public abstract void WriteException(Exception exception, StreamWriter writer);

        /// <summary>
        ///     Writes all the details of an exception to the log stream.
        /// </summary>
        /// <param name="exception">The exception to write.</param>
        /// <param name="writer">
        ///     The <see cref="TextWriter" /> (e.g., file) to write to.
        /// </param>
        public abstract void WriteException(Exception exception, TextWriter writer);


        /// <summary>
        ///     Writes all the details of an exception to the log stream.
        /// </summary>
        /// <param name="exception">The exception to write.</param>
        /// <param name="listeners">
        ///     The collection of <see cref="TraceListener" />s to write to.
        /// </param>
        public abstract void WriteException(Exception exception, TraceListenerCollection listeners);

        private sealed class ExceptionWriterImplementation : ExceptionWriter
        {
            private readonly ExceptionParser _parser;

            internal ExceptionWriterImplementation(ExceptionParser parser)
            {
                _parser = parser;
            }

            public override void WriteException(Exception exception, StreamWriter writer)
            {
                var lines = _parser.Parse(exception);

                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }

            public override void WriteException(Exception exception, TextWriter writer)
            {
                var lines = _parser.Parse(exception);

                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }

            public override void WriteException(Exception exception, TraceListenerCollection listeners)
            {
                foreach (TraceListener listener in listeners)
                {
                    var lines = _parser.Parse(exception);

                    foreach (var line in lines)
                    {
                        listener.WriteLine(line);
                    }
                }
        }
        }
    }

    internal abstract class ExceptionParser
    {
        internal static ExceptionParser Default
        {
            get { return new ExceptionParserImplementation(ExceptionAdder.Default); }
        }

        internal abstract string[] Parse(Exception exception);

        private sealed class ExceptionParserImplementation : ExceptionParser
        {
            private readonly ExceptionAdder _exceptionAdder;

            internal ExceptionParserImplementation(ExceptionAdder exceptionAdder)
            {
                _exceptionAdder = exceptionAdder;
            }

            internal override string[] Parse(Exception exception)
            {
                var lines = new List<string>();

                while (exception != null)
                {
                    lines.Add(string.Empty);
                    _exceptionAdder.AddException(exception, lines);
                    exception = exception.InnerException;
                }

                lines.Add(string.Empty);
                lines.Add(Strings.logDivider);
                lines.Add(string.Empty);

                return lines.ToArray();
            }
        }
    }

    internal abstract class ExceptionAdder
    {
        internal static ExceptionAdder Default
        {
            get { return new ExceptionAdderImplementation(PropertyAdder.Default); }
        }

        internal abstract void AddException(Exception exception, List<string> lines);

        private sealed class ExceptionAdderImplementation : ExceptionAdder
        {
            private readonly PropertyAdder _propertyAdder;

            internal ExceptionAdderImplementation(PropertyAdder propertyAdder)
            {
                _propertyAdder = propertyAdder;
            }

            internal override void AddException(Exception exception, List<string> lines)
            {
                var properties = exception.GetType().GetProperties();

                var exceptionType = exception.GetType().ToString();

                lines.Add(string.Format(Strings.HeaderedProperty,
                                        Strings.Header_Exception,
                                        exceptionType));

                _propertyAdder.AddProperty(lines, Strings.Header_Message, exception.Message);
                _propertyAdder.AddProperty(lines, Strings.Header_StackTrace, exception.StackTrace);

                foreach (var info in properties)
                {
                    if (info.Name != "InnerException")
                    {
                        var value = info.GetValue(exception, null);
                        if (value != null)
                        {
                            if (value is string)
                            {
                                if (string.IsNullOrEmpty(value as string)) continue;
                            }
                            else if (value is IDictionary)
                            {
                                value = _propertyAdder.RenderDictionary(value as IDictionary);
                                if (string.IsNullOrEmpty(value as string)) continue;
                            }
                            else if (value is IEnumerable)
                            {
                                value = _propertyAdder.RenderEnumerable(value as IEnumerable);
                                if (string.IsNullOrEmpty(value as string)) continue;
                            }

                            if (info.Name != "Message" && info.Name != "StackTrace")
                            {
                                _propertyAdder.AddProperty(lines, info.Name, value);
                            }
                        }
                    }
                }
            }
        }
    }

    internal abstract class PropertyAdder
    {
        internal static PropertyAdder Default
        {
            get { return new PropertyAdderImplementation(); }
        }

        internal abstract void AddProperty(List<string> lines, string propertyName, object message);
        internal abstract string RenderDictionary(IDictionary dictionary);
        internal abstract string RenderEnumerable(IEnumerable enumerable);

        private sealed class PropertyAdderImplementation : PropertyAdder
        {
            internal override void AddProperty(List<string> lines, string propertyName, object propertyValue)
            {
                string value;
                if (propertyValue == null)
                {
                    value = "<null>";
                }
                else
                {
                    value = (propertyValue is string) ? propertyValue as string : propertyValue.ToString();
                }

                lines.Add(string.Empty);
                lines.Add(String.Format(Strings.HeaderedProperty, propertyName, value));
            }

            internal override string RenderDictionary(IDictionary dictionary)
            {
                var result = new StringBuilder();
                foreach (var key in dictionary.Keys)
                {
                    if (key != null && dictionary[key] != null)
                    {
                        result.AppendLine(key + " = " + dictionary[key]);
                    }
                }

                if (result.Length > 0) result.Length -= 1;
                return result.ToString();
            }

            internal override string RenderEnumerable(IEnumerable enumerable)
            {
                var result = new StringBuilder();

                foreach (var item in enumerable)
                {
                    result.AppendFormat("{0}\n", item);
                }

                if (result.Length > 0) result.Length -= 1;
                return result.ToString();
            }
        }
    }
}