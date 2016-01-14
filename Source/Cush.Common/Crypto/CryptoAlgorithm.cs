using System;
using System.Security;
using System.Security.Cryptography;

namespace Cush.Common.Crypto
{
    public interface ICryptoAlgorithm
    {
        byte[] Hash(SecureString password, string saltString, int outputBytes);
    }

    internal abstract class CryptoAlgorithm: ICryptoAlgorithm
    {
        private const int Iterations = 1000;

        internal static CryptoAlgorithm GetInstance(SecureStringHelper helper)
        {
            return new Pbkdf2Algorithm(helper);
        }

        /// <summary>
        ///     Computes the hash of a password.
        /// </summary>
        /// <param name="password">The password to hash (as a SecureString).</param>
        /// <param name="saltString">The salt (as a Base64String).</param>
        /// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
        /// <returns>A hash of the password.</returns>
        public abstract byte[] Hash(SecureString password, string saltString, int outputBytes);

        private sealed class Pbkdf2Algorithm : CryptoAlgorithm
        {
            private readonly SecureStringHelper _helper;

            internal Pbkdf2Algorithm(SecureStringHelper helper)
            {
                _helper = helper;
            }

            public override byte[] Hash(SecureString password, string saltString, int outputBytes)
            {
                var salt = Convert.FromBase64String(saltString);
                var pbkdf2 = new Rfc2898DeriveBytes(_helper.SecureStringToString(password), salt)
                {
                    IterationCount = Iterations
                };
                return pbkdf2.GetBytes(outputBytes);
            }
        }
    }
}