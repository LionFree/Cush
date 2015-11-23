// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Cush.Testing.Assertion.Constraints;

namespace Cush.Testing.Assertion
{
	/// <summary>
	/// MessageWriter is the abstract base for classes that write
	/// constraint descriptions and messages in some form. The
	/// class has separate methods for writing various components
	/// of a message, allowing implementations to tailor the
	/// presentation as needed.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal abstract class MessageWriter : StringWriter
	{

		/// <summary>
		/// Construct a MessageWriter given a culture
		/// </summary>
		protected MessageWriter() : base( System.Globalization.CultureInfo.InvariantCulture ) { }

		/// <summary>
		/// Abstract method to get the max line length
		/// </summary>
		internal abstract int MaxLineLength { get; set; }

		/// <summary>
		/// Method to write single line  message with optional args, usually
		/// written to precede the general failure message.
		/// </summary>
		/// <param name="message">The message to be written</param>
		/// <param name="args">Any arguments used in formatting the message</param>
		internal void WriteMessageLine(string message, params object[] args)
		{
			WriteMessageLine(0, message, args);
		}

		/// <summary>
		/// Method to write single line  message with optional args, usually
		/// written to precede the general failure message, at a givel 
		/// indentation level.
		/// </summary>
		/// <param name="level">The indentation level of the message</param>
		/// <param name="message">The message to be written</param>
		/// <param name="args">Any arguments used in formatting the message</param>
		internal abstract void WriteMessageLine(int level, string message, params object[] args);

		/// <summary>
		/// Display Expected and Actual lines for a constraint. This
		/// is called by MessageWriter's default implementation of 
		/// WriteMessageTo and provides the generic two-line display. 
		/// </summary>
		/// <param name="constraint">The constraint that failed</param>
		internal abstract void DisplayDifferences(Constraint constraint);

		/// <summary>
		/// Display Expected and Actual lines for given values. This
		/// method may be called by constraints that need more control over
		/// the display of actual and expected values than is provided
		/// by the default implementation.
		/// </summary>
		/// <param name="expected">The expected value</param>
		/// <param name="actual">The actual value causing the failure</param>
		internal abstract void DisplayDifferences(object expected, object actual);

		/// <summary>
		/// Display the expected and actual string values on separate lines.
		/// If the mismatch parameter is >=0, an additional line is displayed
		/// line containing a caret that points to the mismatch point.
		/// </summary>
		/// <param name="expected">The expected string value</param>
		/// <param name="actual">The actual string value</param>
		/// <param name="mismatch">The point at which the strings don't match or -1</param>
		/// <param name="ignoreCase">If true, case is ignored in locating the point where the strings differ</param>
		/// <param name="clipping">If true, the strings should be clipped to fit the line</param>
		internal abstract void DisplayStringDifferences(string expected, string actual, int mismatch, bool ignoreCase, bool clipping);

		/// <summary>
		/// Writes the text for a connector.
		/// </summary>
		/// <param name="connector">The connector.</param>
		internal abstract void WriteConnector(string connector);

		/// <summary>
		/// Writes the text for a predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		internal abstract void WritePredicate(string predicate);

		/// <summary>
		/// Writes the text for an expected value.
		/// </summary>
		/// <param name="expected">The expected value.</param>
		internal abstract void WriteExpectedValue(object expected);

		/// <summary>
		/// Writes the text for a modifier
		/// </summary>
		/// <param name="modifier">The modifier.</param>
		internal abstract void WriteModifier(string modifier);

		/// <summary>
		/// Writes the text for an actual value.
		/// </summary>
		/// <param name="actual">The actual value.</param>
		internal abstract void WriteActualValue(object actual);

		/// <summary>
		/// Writes the text for a generalized value.
		/// </summary>
		/// <param name="val">The value.</param>
		internal abstract void WriteValue(object val);
	
		/// <summary>
		/// Writes the text for a collection value,
		/// starting at a particular point, to a max length
		/// </summary>
		/// <param name="collection">The collection containing elements to write.</param>
		/// <param name="start">The starting point of the elements to write</param>
		/// <param name="max">The maximum number of elements to write</param>
		internal abstract void WriteCollectionElements(IEnumerable collection, int start, int max);
	}
}
