using System;
using System.Runtime.InteropServices;
using System.Security;

namespace ModelBindingSecureString.Helpers
{
    public static class SecureStringHelper
    {
        /// <summary>
        /// 把 SecureString 轉回 PlainText
        /// </summary>
        public static string ToString(this SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
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
    }
}