using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Cush.CLI.Internal;

namespace Cush.CLI
{
    internal abstract class ConsoleInterOp
    {
        internal abstract bool IsConsoleApp { get; }

        internal static ConsoleInterOp GetInstance()
        {
            return new ConsoleInterOpImplementation();
        }

        internal abstract IntPtr ShowFirstConsole();

        private class ConsoleInterOpImplementation : ConsoleInterOp
        {
            private IntPtr _firstHandle;

            private IntPtr FirstConsoleHandle
            {
                get { return _firstHandle != IntPtr.Zero ? _firstHandle : (_firstHandle = GetFirstConsoleHandle()); }
            }

            // The DebuggerNonUserCodeAttribute should prevent the 
            // first-chance IOException from appearing in the debug 
            // window.  See below for more info.
            [DebuggerNonUserCode]
            internal override bool IsConsoleApp
            {
                get { return Console.OpenStandardInput(1) != Stream.Null; }
            }

            private static IntPtr GetFirstConsoleHandle()
            {
                const uint sizeOfArray = 64;
                var processIDs = new uint[sizeOfArray];
                NativeMethods.GetConsoleProcessList(processIDs, sizeOfArray);
                var processID = processIDs.Where(id => id != 0).FirstOrDefault();
                if (processID == 0) return IntPtr.Zero;
                var process = Process.GetProcessById((int) processID);
                return process.MainWindowHandle;
            }

            internal override IntPtr ShowFirstConsole()
            {
                NativeMethods.SetForegroundWindow(FirstConsoleHandle);
                return FirstConsoleHandle;
            }
        }
    }
}