using System;
using System.Reflection;
using Cush.Common;

namespace Cush.Windows
{
    /// <summary>
    ///     Represents an assembly, which is a reusable, versionable, and
    ///     self-describing block of a common runtime language application.
    /// </summary>
    public interface IAssembly: IBuildInfo
    {
        /// <summary>
        ///     Gets assembly title information.
        /// </summary>
        string AssemblyTitle { get; }
        
        /// <summary>
        ///     Gets the major, minor, build, and revision numbers of the assembly.
        /// </summary>
        string AssemblyVersion { get; }

        /// <summary>
        ///     Gets assembly description information.
        /// </summary>
        string AssemblyDescription { get; }

        /// <summary>
        ///     Gets product name information.
        /// </summary>
        string AssemblyProduct { get; }

        /// <summary>
        ///     Gets copyright information.
        /// </summary>
        string AssemblyCopyright { get; }

        /// <summary>
        ///     Gets company information.
        /// </summary>
        string AssemblyCompany { get; }
        
        /// <summary>
        ///     Gets the location of the assembly.
        /// </summary>
        string Location { get; }

        /// <summary>
        ///     Gets the process executable in the default application domain.
        ///     In other application domains, this is the first executable
        ///     that was executed by AppDomain.ExecuteAssembly.
        /// </summary>
        Assembly GetEntryAssembly();

        /// <summary>
        ///     Gets the major, minor, build, and revision numbers of the given
        ///     assembly.
        /// </summary>
        Version GetVersion(Assembly assembly);
    }
}