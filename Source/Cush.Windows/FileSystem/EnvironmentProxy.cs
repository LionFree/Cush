using System;
using JetBrains.Annotations;

namespace Cush.Windows.FileSystem
{
    [Serializable]
    public sealed class EnvironmentProxy
    {
        /// <summary>
        ///     Gets or sets the fully qualified path of the current working directory.
        /// </summary>
        /// <exception cref="ArgumentException">Attempted to set to an empty string ("").</exception>
        /// <exception cref="ArgumentNullException">Attempted to set to null.</exception>
        /// <exception cref="System.IO.IOException">An I/O error occurred.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Attempted to set a local path that cannot be found.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the appropriate permission.</exception>
        /// <remarks>
        ///     By definition, if this process starts in the root directory of a local or network drive, the value of this
        ///     property is the drive name followed by a trailing slash (for example, "C:\"). If this process starts in a
        ///     subdirectory, the value of this property is the drive and subdirectory path, without a trailing slash (for example,
        ///     "C:\mySubDirectory").
        /// </remarks>
        public string CurrentDirectory
        {
            get { return Environment.CurrentDirectory; }
            set { Environment.CurrentDirectory = value; }
        }

        /// <summary>
        ///     Replaces the name of each environment variable embedded
        ///     in the specified string with the string equivalent of the
        ///     value of the variable, then returns the resulting string.
        /// </summary>
        /// <param name="name">
        ///     A string containing the names of zero or more environment variables.
        ///     Each environment variable is quoted with the percent sign character (%).
        /// </param>
        /// <returns>A string with each environment variable replaced by its value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name" /> is null.</exception>
        /// <remarks>
        ///     COM interop is used to retrieve the environment variables from the operating system. If the environment variables
        ///     cannot be retrieved due to a COM error, the HRESULT that explains the cause of the failure is used to generate one
        ///     of several possible exceptions; that is, the exception depends on the HRESULT. For more information about how the
        ///     HRESULT is processed, see the Remarks section of the Marshal.ThrowExceptionForHR method.
        ///     Replacement only occurs for environment variables that are set. For example, suppose name is "MyENV = %MyENV%". If
        ///     the environment variable, MyENV, is set to 42, this method returns "MyENV = 42". If MyENV is not set, no change
        ///     occurs; this method returns "MyENV = %MyENV%".
        ///     The size of the return value is limited to 32K on Windows NT 4.0 and earlier, and Windows 2000 and later.
        /// </remarks>
        public string ExpandEnvironmentVariables([NotNull] string name)
        {
            return Environment.ExpandEnvironmentVariables(name);
        }
    }
}