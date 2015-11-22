using System;

namespace Cush.Common
{
    public interface IBuildInfo : IVersion, IBuildDate
    {
    }

    public interface IVersion
    {
        /// <summary>
        ///     Gets the major, minor, build, and revision numbers of the object
        ///     assembly.
        /// </summary>
        Version GetVersion();

        /// <summary>
        ///     Gets the major, minor, build, and revision numbers of the object
        ///     at the given path.
        /// </summary>
        /// <param name="imagePath">The path to the assembly.</param>
        Version GetVersion(string imagePath);
    }

    public interface IBuildDate
    {
        /// <summary>
        ///     Gets the date and time of the last time the object was built.
        /// </summary>
        string BuildDate { get; }
    }
}