using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

namespace Cush.Common.Crypto
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public sealed class SecureStringHelper
    {
        /// <summary>
        ///     Gets the CLEAR-TEXT version of the SecureString.
        ///     Do NOT store the output of this method in a variable!
        /// </summary>
        [DebuggerStepThrough]
        public string SecureStringToString(SecureString value)
        {
            var valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        /// <summary>
        ///     Compares two SecureStrings in length-constant time. This comparison
        ///     method is used so that password hashes cannot be extracted from
        ///     on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        /// <param name="ss1">The first SecureString.</param>
        /// <param name="ss2">The second SecureString.</param>
        /// <returns>True if both SecureStrings are equal. False otherwise.</returns>
        [DebuggerStepThrough]
        public bool AreEqual(SecureString ss1, SecureString ss2)
        {
            var bstr1 = IntPtr.Zero;
            var bstr2 = IntPtr.Zero;
            try
            {
                bstr1 = Marshal.SecureStringToBSTR(ss1);
                bstr2 = Marshal.SecureStringToBSTR(ss2);
                var length1 = Marshal.ReadInt32(bstr1, -4);
                var length2 = Marshal.ReadInt32(bstr2, -4);

                long diff = length1 ^ length2;
                for (var x = 0; x < length1; ++x)
                {
                    var b1 = Marshal.ReadByte(bstr1, x);
                    var b2 = Marshal.ReadByte(bstr2, x);

                    diff |= (uint) (b1 ^ b2);
                    if (b1 != b2) return false;
                }

                return diff == 0;
            }
            finally
            {
                if (bstr2 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr2);
                if (bstr1 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr1);
            }
        }

        /// <summary>
        ///     Creates a new SecureString, full of *'s, of a certain length.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public SecureString GetRandomString(int length)
        {
            var output = new SecureString();
            for (var i = 0; i < length; i++)
            {
                output.AppendChar('*');
            }
            return output;
        }
    }
}