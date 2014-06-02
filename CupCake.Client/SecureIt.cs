using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace CupCake.Client
{
    public static class SecureIt
    {
        private static readonly byte[] _entropy = Encoding.Unicode.GetBytes("CupCake");

        public static string EncryptString(this SecureString input)
        {
            if (input == null)
            {
                return null;
            }

            byte[] encryptedData = ProtectedData.Protect(
                Encoding.Unicode.GetBytes(input.ToInsecureString()),
                _entropy,
                DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encryptedData);
        }

        public static SecureString DecryptString(this string encryptedData)
        {
            if (encryptedData == null)
            {
                return null;
            }

            try
            {
                byte[] decryptedData = ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    _entropy,
                    DataProtectionScope.CurrentUser);

                return Encoding.Unicode.GetString(decryptedData).ToSecureString();
            }
            catch
            {
                return new SecureString();
            }
        }

        public static SecureString ToSecureString(this IEnumerable<char> input)
        {
            if (input == null)
            {
                return null;
            }

            var secure = new SecureString();

            foreach (char c in input)
            {
                secure.AppendChar(c);
            }

            secure.MakeReadOnly();
            return secure;
        }

        public static string ToInsecureString(this SecureString input)
        {
            if (input == null)
            {
                return null;
            }

            IntPtr ptr = Marshal.SecureStringToBSTR(input);

            try
            {
                return Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }
        }
    }
}