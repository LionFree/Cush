using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Cush.Windows
{
    internal class NativeMethods
    {
        [DllImport("shell32.dll", EntryPoint = "CommandLineToArgvW", CharSet = CharSet.Unicode)]
        private static extern IntPtr _CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string cmdLine,
            out int numArgs);

        [DllImport("kernel32.dll", EntryPoint = "LocalFree", SetLastError = true)]
        private static extern IntPtr _LocalFree(IntPtr hMem);

        public static string[] CommandLineToArgvW(string cmdLine)
        {
            var num = IntPtr.Zero;
            try
            {
                int numArgs;
                num = _CommandLineToArgvW(cmdLine, out numArgs);
                if (num == IntPtr.Zero)
                    throw new Win32Exception();
                var strArray = new string[numArgs];
                for (var index = 0; index < numArgs; ++index)
                {
                    var ptr = Marshal.ReadIntPtr(num, index*Marshal.SizeOf(typeof (IntPtr)));
                    strArray[index] = Marshal.PtrToStringUni(ptr);
                }
                return strArray;
            }
            finally
            {
                _LocalFree(num);
            }
        }
    }
}