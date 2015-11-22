// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Cush.Testing.Assertion.Constraints;

namespace Cush.Testing.Assertion
{
    /// <summary>
    ///     TextMessageWriter writes constraint descriptions and messages
    ///     in displayable form as a text stream. It tailors the display
    ///     of individual message components to form the standard message
    ///     format of NUnit assertion failure messages.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class TextMessageWriter : MessageWriter
    {
        private int _maxLineLength = DEFAULT_LINE_LENGTH;

        #region Properties

        /// <summary>
        ///     Gets or sets the maximum line length for this writer
        /// </summary>
        internal override int MaxLineLength
        {
            get { return _maxLineLength; }
            set { _maxLineLength = value; }
        }

        #endregion

        #region Message Formats and Constants

        private static readonly int DEFAULT_LINE_LENGTH = 78;

        // Prefixes used in all failure messages. All must be the same
        // length, which is held in the PrefixLength field. Should not
        // contain any tabs or newline characters.
        /// <summary>
        ///     Prefix used for the expected value line of a message
        /// </summary>
        internal static readonly string PfxExpected = "  Expected: ";

        /// <summary>
        ///     Prefix used for the actual value line of a message
        /// </summary>
        internal static readonly string PfxActual = "  But was:  ";

        /// <summary>
        ///     Length of a message prefix
        /// </summary>
        internal static readonly int PrefixLength = PfxExpected.Length;

        private static readonly string FmtConnector = " {0} ";
        private static readonly string FmtPredicate = "{0} ";
        //private static readonly string Fmt_Label = "{0}";
        private static readonly string Fmt_Modifier = ", {0}";

        private static readonly string Fmt_Null = "null";
        private static readonly string Fmt_EmptyString = "<string.Empty>";
        private static readonly string Fmt_EmptyCollection = "<empty>";

        private static readonly string Fmt_String = "\"{0}\"";
        private static readonly string Fmt_Char = "'{0}'";
        private static readonly string Fmt_DateTime = "yyyy-MM-dd HH:mm:ss.fff";
        private static readonly string Fmt_ValueType = "{0}";
        private static readonly string Fmt_Default = "<{0}>";

        #endregion

        #region Constructors

        /// <summary>
        ///     Construct a TextMessageWriter
        /// </summary>
        internal TextMessageWriter()
        {
        }

        /// <summary>
        ///     Construct a TextMessageWriter, specifying a user message
        ///     and optional formatting arguments.
        /// </summary>
        /// <param name="userMessage"></param>
        /// <param name="args"></param>
        internal TextMessageWriter(string userMessage, params object[] args)
        {
            if (!string.IsNullOrEmpty(userMessage))
                WriteMessageLine(userMessage, args);
        }

        #endregion

        #region internal Methods - High Level

        /// <summary>
        ///     Method to write single line  message with optional args, usually
        ///     written to precede the general failure message, at a givel
        ///     indentation level.
        /// </summary>
        /// <param name="level">The indentation level of the message</param>
        /// <param name="message">The message to be written</param>
        /// <param name="args">Any arguments used in formatting the message</param>
        internal override void WriteMessageLine(int level, string message, params object[] args)
        {
            if (message != null)
            {
                while (level-- >= 0) Write("  ");

                if (args != null && args.Length > 0)
                    message = string.Format(message, args);

                WriteLine(message);
            }
        }

        /// <summary>
        ///     Display Expected and Actual lines for a constraint. This
        ///     is called by MessageWriter's default implementation of
        ///     WriteMessageTo and provides the generic two-line display.
        /// </summary>
        /// <param name="constraint">The constraint that failed</param>
        internal override void DisplayDifferences(Constraint constraint)
        {
            WriteExpectedLine(constraint);
            WriteActualLine(constraint);
        }

        /// <summary>
        ///     Display Expected and Actual lines for given values. This
        ///     method may be called by constraints that need more control over
        ///     the display of actual and expected values than is provided
        ///     by the default implementation.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value causing the failure</param>
        internal override void DisplayDifferences(object expected, object actual)
        {
            WriteExpectedLine(expected);
            WriteActualLine(actual);
        }

        ///// <summary>
        ///// Display Expected and Actual lines for given values, including
        ///// a tolerance value on the expected line.
        ///// </summary>
        ///// <param name="expected">The expected value</param>
        ///// <param name="actual">The actual value causing the failure</param>
        ///// <param name="tolerance">The tolerance within which the test was made</param>
        //internal override void DisplayDifferences(object expected, object actual, Tolerance tolerance)
        //{
        //    WriteExpectedLine(expected, tolerance);
        //    WriteActualLine(actual);
        //}

        /// <summary>
        ///     Display the expected and actual string values on separate lines.
        ///     If the mismatch parameter is >=0, an additional line is displayed
        ///     line containing a caret that points to the mismatch point.
        /// </summary>
        /// <param name="expected">The expected string value</param>
        /// <param name="actual">The actual string value</param>
        /// <param name="mismatch">The point at which the strings don't match or -1</param>
        /// <param name="ignoreCase">If true, case is ignored in string comparisons</param>
        /// <param name="clipping">If true, clip the strings to fit the max line length</param>
        internal override void DisplayStringDifferences(string expected, string actual, int mismatch, bool ignoreCase,
            bool clipping)
        {
            // Maximum string we can display without truncating
            var maxDisplayLength = MaxLineLength
                                   - PrefixLength // Allow for prefix
                                   - 2; // 2 quotation marks

            if (clipping)
                MsgUtils.ClipExpectedAndActual(ref expected, ref actual, maxDisplayLength, mismatch);

            expected = MsgUtils.EscapeControlChars(expected);
            actual = MsgUtils.EscapeControlChars(actual);

            // The mismatch position may have changed due to clipping or white space conversion
            mismatch = MsgUtils.FindMismatchPosition(expected, actual, 0, ignoreCase);

            Write(PfxExpected);
            WriteExpectedValue(expected);
            if (ignoreCase)
                WriteModifier("ignoring case");
            WriteLine();
            WriteActualLine(actual);
            //DisplayDifferences(expected, actual);
            if (mismatch >= 0)
                WriteCaretLine(mismatch);
        }

        #endregion

        #region internal Methods - Low Level

        /// <summary>
        ///     Writes the text for a connector.
        /// </summary>
        /// <param name="connector">The connector.</param>
        internal override void WriteConnector(string connector)
        {
            Write(FmtConnector, connector);
        }

        /// <summary>
        ///     Writes the text for a predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        internal override void WritePredicate(string predicate)
        {
            Write(FmtPredicate, predicate);
        }

        //internal override void WriteLabel(string label)
        //{
        //    Write(Fmt_Label, label);
        //}

        /// <summary>
        ///     Write the text for a modifier.
        /// </summary>
        /// <param name="modifier">The modifier.</param>
        internal override void WriteModifier(string modifier)
        {
            Write(Fmt_Modifier, modifier);
        }


        /// <summary>
        ///     Writes the text for an expected value.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        internal override void WriteExpectedValue(object expected)
        {
            WriteValue(expected);
        }

        /// <summary>
        ///     Writes the text for an actual value.
        /// </summary>
        /// <param name="actual">The actual value.</param>
        internal override void WriteActualValue(object actual)
        {
            WriteValue(actual);
        }

        /// <summary>
        ///     Writes the text for a generalized value.
        /// </summary>
        /// <param name="val">The value.</param>
        internal override void WriteValue(object val)
        {
            if (val == null)
                Write(Fmt_Null);
            else if (val.GetType().IsArray)
                WriteArray((Array) val);
            else if (val is string)
                WriteString((string) val);
            else if (val is IEnumerable)
                WriteCollectionElements((IEnumerable) val, 0, 10);
            else if (val is char)
                WriteChar((char) val);
            else if (val is double)
                WriteDouble((double) val);
            else if (val is float)
                WriteFloat((float) val);
            else if (val is decimal)
                WriteDecimal((decimal) val);
            else if (val is DateTime)
                WriteDateTime((DateTime) val);
            else if (val.GetType().IsValueType)
                Write(Fmt_ValueType, val);
            else
                Write(Fmt_Default, val);
        }

        /// <summary>
        ///     Writes the text for a collection value,
        ///     starting at a particular point, to a max length
        /// </summary>
        /// <param name="collection">The collection containing elements to write.</param>
        /// <param name="start">The starting point of the elements to write</param>
        /// <param name="max">The maximum number of elements to write</param>
        internal override void WriteCollectionElements(IEnumerable collection, int start, int max)
        {
            var count = 0;
            var index = 0;

            foreach (var obj in collection)
            {
                if (index++ >= start)
                {
                    if (++count > max)
                        break;
                    Write(count == 1 ? "< " : ", ");
                    WriteValue(obj);
                }
            }

            if (count == 0)
            {
                Write(Fmt_EmptyCollection);
                return;
            }

            if (count > max)
                Write("...");

            Write(" >");
        }

        private void WriteArray(Array array)
        {
            if (array.Length == 0)
            {
                Write(Fmt_EmptyCollection);
                return;
            }

            var rank = array.Rank;
            var products = new int[rank];

            for (int product = 1, r = rank; --r >= 0;)
                products[r] = product *= array.GetLength(r);

            var count = 0;
            foreach (var obj in array)
            {
                if (count > 0)
                    Write(", ");

                var startSegment = false;
                for (var r = 0; r < rank; r++)
                {
                    startSegment = startSegment || count%products[r] == 0;
                    if (startSegment) Write("< ");
                }

                WriteValue(obj);

                ++count;

                var nextSegment = false;
                for (var r = 0; r < rank; r++)
                {
                    nextSegment = nextSegment || count%products[r] == 0;
                    if (nextSegment) Write(" >");
                }
            }
        }

        private void WriteString(string s)
        {
            if (s == string.Empty)
                Write(Fmt_EmptyString);
            else
                Write(Fmt_String, s);
        }

        private void WriteChar(char c)
        {
            Write(Fmt_Char, c);
        }

        private void WriteDouble(double d)
        {
            if (double.IsNaN(d) || double.IsInfinity(d))
                Write(d);
            else
            {
                var s = d.ToString("G17", CultureInfo.InvariantCulture);

                if (s.IndexOf('.') > 0)
                    Write(s + "d");
                else
                    Write(s + ".0d");
            }
        }

        private void WriteFloat(float f)
        {
            if (float.IsNaN(f) || float.IsInfinity(f))
                Write(f);
            else
            {
                var s = f.ToString("G9", CultureInfo.InvariantCulture);

                if (s.IndexOf('.') > 0)
                    Write(s + "f");
                else
                    Write(s + ".0f");
            }
        }

        private void WriteDecimal(decimal d)
        {
            Write(d.ToString("G29", CultureInfo.InvariantCulture) + "m");
        }

        private void WriteDateTime(DateTime dt)
        {
            Write(dt.ToString(Fmt_DateTime, CultureInfo.InvariantCulture));
        }

        #endregion

        #region Helper Methods

        /// <summary>
        ///     Write the generic 'Expected' line for a constraint
        /// </summary>
        /// <param name="constraint">The constraint that failed</param>
        private void WriteExpectedLine(Constraint constraint)
        {
            Write(PfxExpected);
            constraint.WriteDescriptionTo(this);
            WriteLine();
        }

        /// <summary>
        ///     Write the generic 'Expected' line for a given value
        /// </summary>
        /// <param name="expected">The expected value</param>
        private void WriteExpectedLine(object expected)
        {
            Write(PfxExpected);
            WriteExpectedValue(expected);
            WriteLine();
        }

        /// <summary>
        ///     Write the generic 'Actual' line for a constraint
        /// </summary>
        /// <param name="constraint">The constraint for which the actual value is to be written</param>
        private void WriteActualLine(Constraint constraint)
        {
            Write(PfxActual);
            constraint.WriteActualValueTo(this);
            WriteLine();
        }

        /// <summary>
        ///     Write the generic 'Actual' line for a given value
        /// </summary>
        /// <param name="actual">The actual value causing a failure</param>
        private void WriteActualLine(object actual)
        {
            Write(PfxActual);
            WriteActualValue(actual);
            WriteLine();
        }

        private void WriteCaretLine(int mismatch)
        {
            // We subtract 2 for the initial 2 blanks and add back 1 for the initial quote
            WriteLine("  {0}^", new string('-', PrefixLength + mismatch - 2 + 1));
        }

        #endregion
    }
}