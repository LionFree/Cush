using System;
using System.Runtime.InteropServices;

namespace Cush.CLI.Internal
{
    internal class NativeMethods
    {
        [DllImport("USER32.DLL")]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        // Fills an array of type uint with the process ID's of all processes attached to this console.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern uint GetConsoleProcessList(uint[] processList, uint processCount);
    }
}