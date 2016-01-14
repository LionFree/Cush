using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Cush.Common")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Cush.Common")]
[assembly: AssemblyCopyright("Copyright ©  2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

[assembly: CLSCompliant(true)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("d04013c8-ecfd-4b73-89bd-20c220fcb538")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: InternalsVisibleTo("Cush.Common.Tests")]


// In many cases, using compiled Expression trees instead of 'hard coded' code results 
// in a performance regression, with the profiler indicating that the additional time 
// is spent in JIT_MethodAccessCheck. 
// It is possible to make runtime-generated code run twice as fast as 'hard coded' code 
// (half the time to complete). By decorating the assembly with the following attributes, 
// the whole security check can be eliminated:
//[assembly: AllowPartiallyTrustedCallers]
//[assembly: SecurityTransparent]
//[assembly: SecurityRules(SecurityRuleSet.Level2, SkipVerificationInFullTrust = true)]