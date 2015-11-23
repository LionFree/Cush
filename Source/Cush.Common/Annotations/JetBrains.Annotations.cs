using System;

#pragma warning disable 1591
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

namespace JetBrains.Annotations
{
    /// <summary>
    ///     Indicates that the value of the marked element could never be <c>null</c>.
    /// </summary>
    /// <example>
    ///     <code>
    /// [NotNull] public object Foo() {
    ///   return null; // Warning: Possible 'null' assignment
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(
        AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
        AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event)]
    public sealed class NotNullAttribute : Attribute
    {
    }

    /// <summary>
    ///     Describes dependency between method input and output.
    /// </summary>
    /// <syntax>
    ///     <p>Function Definition Table syntax:</p>
    ///     <list>
    ///         <item>FDT      ::= FDTRow [;FDTRow]*</item>
    ///         <item>FDTRow   ::= Input =&gt; Output | Output &lt;= Input</item>
    ///         <item>Input    ::= ParameterName: Value [, Input]*</item>
    ///         <item>Output   ::= [ParameterName: Value]* {halt|stop|void|nothing|Value}</item>
    ///         <item>Value    ::= true | false | null | notnull | canbenull</item>
    ///     </list>
    ///     If method has single input parameter, it's name could be omitted.<br />
    ///     Using <c>halt</c> (or <c>void</c>/<c>nothing</c>, which is the same)
    ///     for method output means that the methos doesn't return normally.<br />
    ///     <c>canbenull</c> annotation is only applicable for output parameters.<br />
    ///     You can use multiple <c>[ContractAnnotation]</c> for each FDT row,
    ///     or use single attribute with rows separated by semicolon.<br />
    /// </syntax>
    /// <examples>
    ///     <list>
    ///         <item>
    ///             <code>
    /// [ContractAnnotation("=> halt")]
    /// public void TerminationMethod()
    /// </code>
    ///         </item>
    ///         <item>
    ///             <code>
    /// [ContractAnnotation("halt &lt;= condition: false")]
    /// public void Assert(bool condition, string text) // regular assertion method
    /// </code>
    ///         </item>
    ///         <item>
    ///             <code>
    /// [ContractAnnotation("s:null => true")]
    /// public bool IsNullOrEmpty(string s) // string.IsNullOrEmpty()
    /// </code>
    ///         </item>
    ///         <item>
    ///             <code>
    /// // A method that returns null if the parameter is null,
    /// // and not null if the parameter is not null
    /// [ContractAnnotation("null => null; notnull => notnull")]
    /// public object Transform(object data) 
    /// </code>
    ///         </item>
    ///         <item>
    ///             <code>
    /// [ContractAnnotation("s:null=>false; =>true,result:notnull; =>false, result:null")]
    /// public bool TryParse(string s, out Person result)
    /// </code>
    ///         </item>
    ///     </list>
    /// </examples>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class ContractAnnotationAttribute : Attribute
    {
        public ContractAnnotationAttribute([NotNull] string contract)
            : this(contract, false)
        {
        }

        public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
        {
            Contract = contract;
            ForceFullStates = forceFullStates;
        }

        public string Contract { get; private set; }
        public bool ForceFullStates { get; private set; }
    }


    /// <summary>
    ///     Indicates that the marked method is assertion method, i.e. it halts control flow if
    ///     one of the conditions is satisfied. To set the condition, mark one of the parameters with
    ///     <see cref="AssertionConditionAttribute" /> attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AssertionMethodAttribute : Attribute
    {
    }

    /// <summary>
    ///     Indicates the condition parameter of the assertion method. The method itself should be
    ///     marked by <see cref="AssertionMethodAttribute" /> attribute. The mandatory argument of
    ///     the attribute is the assertion type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AssertionConditionAttribute : Attribute
    {
        public AssertionConditionAttribute(AssertionConditionType conditionType)
        {
            ConditionType = conditionType;
        }

        public AssertionConditionType ConditionType { get; private set; }
    }

    /// <summary>
    ///     Specifies assertion type. If the assertion method argument satisfies the condition,
    ///     then the execution continues. Otherwise, execution is assumed to be halted.
    /// </summary>
    public enum AssertionConditionType
    {
        /// <summary>Marked parameter should be evaluated to true.</summary>
        IS_TRUE = 0,

        /// <summary>Marked parameter should be evaluated to false.</summary>
        IS_FALSE = 1,

        /// <summary>Marked parameter should be evaluated to null value.</summary>
        IS_NULL = 2,

        /// <summary>Marked parameter should be evaluated to not null value.</summary>
        IS_NOT_NULL = 3
    }

    ///// <summary>
    /////     Indicates that the marked method builds string by format pattern and (optional) arguments.
    /////     Parameter, which contains format string, should be given in constructor. The format string
    /////     should be in <see cref="string.Format(IFormatProvider,string,object[])" />-like form
    ///// </summary>
    ///// <example>
    /////     <code>
    ///// [StringFormatMethod("message")]
    ///// public void ShowError(string message, params object[] args) { /* do something */ }
    ///// public void Foo() {
    /////   ShowError("Failed: {0}"); // Warning: Non-existing argument in format string
    ///// }
    ///// </code>
    ///// </example>
    //[AttributeUsage(
    //    AttributeTargets.Constructor | AttributeTargets.Method)]
    //internal sealed class StringFormatMethodAttribute : Attribute
    //{
    //    /// <param name="formatParameterName">
    //    ///     Specifies which parameter of an annotated method should be treated as format-string
    //    /// </param>
    //    public StringFormatMethodAttribute(string formatParameterName)
    //    {
    //        FormatParameterName = formatParameterName;
    //    }

    //    public string FormatParameterName { get; private set; }
    //}
}