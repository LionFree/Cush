// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Diagnostics.CodeAnalysis;

namespace Cush.Testing.Assertion.Exceptions 
{
	/// <summary>
	/// Thrown when an assertion failed.
	/// </summary>
	[Serializable, ExcludeFromCodeCoverage]
	internal class AssertionException : System.Exception
	{
		/// <param name="message">The error message that explains 
		/// the reason for the exception</param>
		internal AssertionException (string message) : base(message) 
		{}

		/// <param name="message">The error message that explains 
		/// the reason for the exception</param>
		/// <param name="inner">The exception that caused the 
		/// current exception</param>
		internal AssertionException(string message, Exception inner) :
			base(message, inner) 
		{}

		/// <summary>
		/// Serialization Constructor
		/// </summary>
		protected AssertionException(System.Runtime.Serialization.SerializationInfo info, 
			System.Runtime.Serialization.StreamingContext context) : base(info,context)
		{}

	}
}
