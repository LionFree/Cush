using System;
using System.Security;
using System.Security.Cryptography;
// ReSharper disable UnusedMember.Global

namespace Cush.Common.Crypto
{
    public class CryptoHasher
    {
        // The following constants may be changed without breaking existing hashes.
        private const int SaltByteSize = 256;
        private const int HashByteSize = 256;

        private readonly ICryptoAlgorithm _algorithm;

        public CryptoHasher() : this(CryptoAlgorithm.GetInstance(new SecureStringHelper()))
        {
        }
        
        public CryptoHasher(ICryptoAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        public string Hash(SecureString password, string salt)
        {
            // Hash the password and encode the parameters
            var hash = _algorithm.Hash(password, salt, HashByteSize);
            return Convert.ToBase64String(hash);
        }

        public string CreateNewSalt()
        {
            var provider = new RNGCryptoServiceProvider();
            var salt = new byte[SaltByteSize];
            provider.GetNonZeroBytes(salt);

            var output = Convert.ToBase64String(salt);
            return output;
        }
    }
}