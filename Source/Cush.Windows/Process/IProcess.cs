using System;
using Cush.Common;

namespace Cush.Windows
{
    /// <summary>
    ///     Provides interface methods for access to local and remote processes and enables you to start and stop local system
    ///     processes.
    /// </summary>
    public interface IProcess : IDisposable, IProcessStarter
    {
        /// <summary>
        ///     Gets the name of the process.
        /// </summary>
        string ProcessName { get; }

        /// <summary>
        ///     Gets a new <see cref="IProcess" /> component and associates it with the currently active process.
        /// </summary>
        IProcess GetCurrentProcess();

        /// <summary>
        /// Stops the associated process immediately.
        /// </summary>
        void Kill();

        /// <summary>
        /// Frees any resources associated with this component.
        /// </summary>
        void Close();
    }
}