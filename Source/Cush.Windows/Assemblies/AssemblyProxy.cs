using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Cush.Windows
{
    /// <summary>
    ///     Represents an assembly, which is a reusable, versionable, and
    ///     self-describing block of a common runtime language application.
    /// </summary>
    public abstract class AssemblyProxy : IAssembly
    {
        private class AssemblyProxyImplementation : AssemblyProxy
        {
            private readonly Assembly _assembly;

            internal AssemblyProxyImplementation(Assembly entryAssembly)
            {
                _assembly = entryAssembly;
            }

            public override string AssemblyTitle
            {
                get
                {
                    var attributes =
                        _assembly.GetCustomAttributes(typeof (AssemblyTitleAttribute), false);

                    if (attributes.Length <= 0) return Path.GetFileNameWithoutExtension(_assembly.CodeBase);

                    var titleAttribute = (AssemblyTitleAttribute) attributes[0];
                    return titleAttribute.Title != ""
                        ? titleAttribute.Title
                        : Path.GetFileNameWithoutExtension(_assembly.CodeBase);
                }
            }

            public override string AssemblyVersion
            {
                get { return "Version " + _assembly.GetName().Version; }
            }

            public override string AssemblyDescription
            {
                get { return _assembly.GetAttribute<AssemblyDescriptionAttribute>().Description; }
            }

            public override string AssemblyProduct
            {
                get { return _assembly.GetAttribute<AssemblyProductAttribute>().Product; }
            }

            public override string AssemblyCopyright
            {
                get { return _assembly.GetAttribute<AssemblyCopyrightAttribute>().Copyright; }
            }

            public override string AssemblyCompany
            {
                get { return _assembly.GetAttribute<AssemblyCompanyAttribute>().Company; }
            }

            public override string BuildDate
            {
                get
                {
                    return _assembly.RetrieveLinkerTimestamp().ToString(CultureInfo.InvariantCulture);
                    //var version = _assembly.GetName().Version;
                    //return version.ToDateTime().ToString(CultureInfo.InvariantCulture);
                }
            }

            public override string Location
            {
                get { return _assembly.Location; }
            }

            public override Assembly GetEntryAssembly()
            {
                return Assembly.GetEntryAssembly();
            }

            public override Version GetVersion(Assembly assembly)
            {
                return assembly.GetName().Version;
            }

            public override Version GetVersion()
            {
                return GetVersion(_assembly);
            }

            public override Version GetVersion(string imagePath)
            {
                return GetVersion(Assembly.ReflectionOnlyLoadFrom(imagePath));
            }
        }

        #region Constructors

        /// <summary>
        ///     Generates a new instance of this class.
        /// </summary>
        public static AssemblyProxy Default
        {
            get
            {
                return AssemblyInspector.HasEntryAssembly
                    ? WrapAssembly(Assembly.GetEntryAssembly())
                    : Null;
            }
        }

        /// <summary>
        ///     Generates a new instance of this class, using no EntryAssembly.
        /// </summary>
        public static AssemblyProxy Null
        {
            get { return WrapAssembly(null); }
        }

        /// <summary>
        ///     Wraps an <see cref="T:Assembly" />.
        /// </summary>
        public static AssemblyProxy WrapAssembly(Assembly entryAssembly)
        {
            return GetInstance(entryAssembly);
        }

        internal static AssemblyProxy GetInstance(Assembly entryAssembly)
        {
            return new AssemblyProxyImplementation(entryAssembly);
        }

        #endregion

        #region Abstract members

        /// <summary>
        ///     Gets assembly title information.
        /// </summary>
        public abstract string AssemblyTitle { get; }

        /// <summary>
        ///     Gets the major, minor, build, and revision numbers of the assembly.
        /// </summary>
        public abstract string AssemblyVersion { get; }

        /// <summary>
        ///     Gets assembly description information.
        /// </summary>
        public abstract string AssemblyDescription { get; }

        /// <summary>
        ///     Gets product name information.
        /// </summary>
        public abstract string AssemblyProduct { get; }

        /// <summary>
        ///     Gets copyright information.
        /// </summary>
        public abstract string AssemblyCopyright { get; }

        /// <summary>
        ///     Gets company information.
        /// </summary>
        public abstract string AssemblyCompany { get; }

        /// <summary>
        ///     Gets the date and time of the last time the assembly was compiled.
        /// </summary>
        public abstract string BuildDate { get; }

        /// <summary>
        ///     Gets the location of the assembly as specified originally, for example, in an
        ///     <see cref="T:System.Reflection.AssemblyName" /> object.
        /// </summary>
        /// <returns>
        ///     The location of the assembly as specified originally.
        /// </returns>
        public abstract string Location { get; }

        /// <summary>
        ///     Gets the process executable in the default application domain.
        ///     In other application domains, this is the first executable
        ///     that was executed by AppDomain.ExecuteAssembly.
        /// </summary>
        public abstract Assembly GetEntryAssembly();

        /// <summary>
        ///     Gets the major, minor, build, and revision numbers of the given
        ///     assembly.
        /// </summary>
        public abstract Version GetVersion(Assembly assembly);

        /// <summary>
        ///     Gets the major, minor, build, and revision numbers of the current
        ///     assembly.
        /// </summary>
        public abstract Version GetVersion();

        /// <summary>
        ///     Gets the major, minor, build, and revision numbers of the assembly
        ///     at the given path.
        /// </summary>
        /// <param name="imagePath">The path to the assembly.</param>
        public abstract Version GetVersion(string imagePath);

        #endregion
    }
}