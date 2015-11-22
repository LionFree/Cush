using System.Diagnostics;
using Cush.Common;

namespace Cush.Windows
{
    /// <summary>
    ///     Wraps a <see cref="System.Diagnostics.Process" />, providing access to local and remote processes and enables you
    ///     to start and stop local system processes.
    /// </summary>
    public class ProcessProxy : DisposableBase, IProcess
    {
        private Process _process;

        /// <summary> 
        ///       Initializes a new instance of the <see cref='ProcessProxy'/> class.
        /// </summary> 
        public ProcessProxy(Process process)
        {
            _process = process;
        }

        /// <summary>
        ///     Gets a new <see cref="IProcess" /> component and associates it with the currently active process.
        /// </summary>
        public IProcess GetCurrentProcess()
        {
            return new ProcessProxy(Process.GetCurrentProcess());
        }

        /// <summary>
        ///     Starts a process resource by specifying the name of a document or application file and associates the resource with
        ///     a new <see cref="T:System.Diagnostics.Process" /> component.
        /// </summary>
        /// <returns>
        ///     A new <see cref="T:System.Diagnostics.Process" /> component that is associated with the process resource, or null,
        ///     if no process resource is started (for example, if an existing process is reused).
        /// </returns>
        /// <param name="fileName">The name of a document or application file to run in the process. </param>
        /// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The PATH environment variable has a string containing quotes.</exception>
        /// <filterpriority>1</filterpriority>
        public IProcess Start(string fileName)
        {
            return new ProcessProxy(Process.Start(fileName));
        }

        /// <summary>
        /// Stops the associated process immediately.
        /// </summary>
        public void Kill()
        {
            _process.Kill();
        }

        /// <summary>
        /// Frees any resources associated with this component.
        /// </summary>
        public void Close()
        {
            _process.Close();
        }

        /// <summary>
        ///     Gets the name of the process.
        /// </summary>
        public string ProcessName
        {
            get { return _process.ProcessName; }
        }

        protected override void DisposeOfManagedResources()
        {
            _process.Dispose();
        }

        protected override void DisposeOfUnManagedObjects()
        {
            _process = null;
        }
    }
}