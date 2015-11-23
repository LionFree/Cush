// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Cush.Testing.Assertion.Constraints
{
	/// <summary>
	///     Delegate used to delay evaluation of the actual value
	///     to be used in evaluating a constraint
	/// </summary>
#if CLR_2_0 || CLR_4_0
	internal delegate T ActualValueDelegate<T>();
#else
	internal delegate object ActualValueDelegate();
#endif

	/// <summary>
	///     The Constraint class is the base of all built-in constraints
	///     within NUnit. It provides the operator overloads used to combine
	///     constraints.
	/// </summary>
	[ExcludeFromCodeCoverage, DebuggerStepThrough]
	internal abstract class Constraint : IResolveConstraint
	{
		#region UnsetObject Class

		/// <summary>
		///     Class used to detect any derived constraints
		///     that fail to set the actual value in their
		///     Matches override.
		/// </summary>
		private class UnsetObject
		{
			public override string ToString()
			{
				return "UNSET";
			}
		}

		#endregion

		#region Static and Instance Fields

		/// <summary>
		///     Static UnsetObject used to detect derived constraints
		///     failing to set the actual value.
		/// </summary>
		protected static object Unset = new UnsetObject();

		/// <summary>
		///     The actual value being tested against a constraint
		/// </summary>
		protected object Actual = Unset;

		/// <summary>
		///     The display name of this Constraint for use by ToString()
		/// </summary>
		private string _displayName;

		/// <summary>
		///     Argument fields used by ToString();
		/// </summary>
		private readonly int _argcnt;

		private readonly object _arg1;
		private readonly object _arg2;

		/// <summary>
		///     The builder holding this constraint
		/// </summary>
		private ConstraintBuilder _builder;

		#endregion

		#region Constructors

		/// <summary>
		///     Construct a constraint with no arguments
		/// </summary>
		protected Constraint()
		{
			_argcnt = 0;
		}

		/// <summary>
		///     Construct a constraint with one argument
		/// </summary>
		protected Constraint(object arg)
		{
			_argcnt = 1;
			_arg1 = arg;
		}

		/// <summary>
		///     Construct a constraint with two arguments
		/// </summary>
		protected Constraint(object arg1, object arg2)
		{
			_argcnt = 2;
			_arg1 = arg1;
			_arg2 = arg2;
		}

		#endregion

		#region Set Containing ConstraintBuilder

		/// <summary>
		///     Sets the ConstraintBuilder holding this constraint
		/// </summary>
		internal void SetBuilder(ConstraintBuilder builder)
		{
			_builder = builder;
		}

		#endregion

		#region Properties

		/// <summary>
		///     The display name of this Constraint for use by ToString().
		///     The default value is the name of the constraint with
		///     trailing "Constraint" removed. Derived classes may set
		///     this to another name in their constructors.
		/// </summary>
		protected string DisplayName
		{
			get
			{
				if (_displayName == null)
				{
					_displayName = GetType().Name.ToLower();
					if (_displayName.EndsWith("`1") || _displayName.EndsWith("`2"))
						_displayName = _displayName.Substring(0, _displayName.Length - 2);
					if (_displayName.EndsWith("constraint"))
						_displayName = _displayName.Substring(0, _displayName.Length - 10);
				}

				return _displayName;
			}

			set { _displayName = value; }
		}

		#endregion

		#region Abstract and Virtual Methods

		/// <summary>
		///     Write the failure message to the MessageWriter provided
		///     as an argument. The default implementation simply passes
		///     the constraint and the actual value to the writer, which
		///     then displays the constraint description and the value.
		///     Constraints that need to provide additional details,
		///     such as where the error occured can override this.
		/// </summary>
		/// <param name="writer">The MessageWriter on which to display the message</param>
		internal virtual void WriteMessageTo(MessageWriter writer)
		{
			writer.DisplayDifferences(this);
		}

		/// <summary>
		///     Test whether the constraint is satisfied by a given value
		/// </summary>
		/// <param name="actual">The value to be tested</param>
		/// <returns>True for success, false for failure</returns>
		internal abstract bool Matches(object actual);

#if CLR_2_0 || CLR_4_0
	/// <summary>
	/// Test whether the constraint is satisfied by an
	/// ActualValueDelegate that returns the value to be tested.
	/// The default implementation simply evaluates the delegate
	/// but derived classes may override it to provide for delayed 
	/// processing.
	/// </summary>
	/// <param name="del">An <see cref="ActualValueDelegate{T}"/></param>
	/// <returns>True for success, false for failure</returns>
		internal virtual bool Matches<T>(ActualValueDelegate<T> del)
		{
			if(AsyncInvocationRegion.IsAsyncOperation(del))
				using (var region = AsyncInvocationRegion.Create(del))
					return Matches(region.WaitForPendingOperationsToComplete(del()));

			return Matches(del());
		}
#else
		/// <summary>
		///     Test whether the constraint is satisfied by an
		///     ActualValueDelegate that returns the value to be tested.
		///     The default implementation simply evaluates the delegate
		///     but derived classes may override it to provide for delayed
		///     processing.
		/// </summary>
		/// <param name="del">An <see cref="ActualValueDelegate" /></param>
		/// <returns>True for success, false for failure</returns>
		internal virtual bool Matches(ActualValueDelegate del)
		{
			return Matches(del());
		}
#endif

		/// <summary>
		///     Test whether the constraint is satisfied by a given reference.
		///     The default implementation simply dereferences the value but
		///     derived classes may override it to provide for delayed processing.
		/// </summary>
		/// <param name="actual">A reference to the value to be tested</param>
		/// <returns>True for success, false for failure</returns>
#if CLR_2_0 || CLR_4_0
		internal virtual bool Matches<T>(ref T actual)
#else
		internal virtual bool Matches(ref bool actual)
#endif
		{
			return Matches(actual);
		}

		/// <summary>
		///     Write the constraint description to a MessageWriter
		/// </summary>
		/// <param name="writer">The writer on which the description is displayed</param>
		internal abstract void WriteDescriptionTo(MessageWriter writer);

		/// <summary>
		///     Write the actual value for a failing constraint test to a
		///     MessageWriter. The default implementation simply writes
		///     the raw value of actual, leaving it to the writer to
		///     perform any formatting.
		/// </summary>
		/// <param name="writer">The writer on which the actual value is displayed</param>
		internal virtual void WriteActualValueTo(MessageWriter writer)
		{
			writer.WriteActualValue(Actual);
		}

		#endregion

		#region ToString Override

		/// <summary>
		///     Default override of ToString returns the constraint DisplayName
		///     followed by any arguments within angle brackets.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			var rep = GetStringRepresentation();

			return _builder == null ? rep : string.Format("<unresolved {0}>", rep);
		}

		/// <summary>
		///     Returns the string representation of this constraint
		/// </summary>
		protected virtual string GetStringRepresentation()
		{
			switch (_argcnt)
			{
				default:
					return string.Format("<{0}>", DisplayName);
				case 1:
					return string.Format("<{0} {1}>", DisplayName, _displayable(_arg1));
				case 2:
					return string.Format("<{0} {1} {2}>", DisplayName, _displayable(_arg1), _displayable(_arg2));
			}
		}

		private static string _displayable(object o)
		{
			if (o == null) return "null";

			var fmt = o is string ? "\"{0}\"" : "{0}";
			return string.Format(CultureInfo.InvariantCulture, fmt, o);
		}

		#endregion
		
		#region IResolveConstraint Members

		Constraint IResolveConstraint.Resolve()
		{
			return _builder == null ? this : _builder.Resolve();
		}

		#endregion
	}
}