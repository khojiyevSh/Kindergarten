using Kindergarten.Application.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace Kindergarten.Infrastucture.Services
{
    public class HashServise : IHashService
    {
        public string GetHash(string key)
        {
            const int keySize = 64;
            const int iterations = 320000;

            HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA512;

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(key),
                new byte[0],
                iterations,
                hashAlgorithmName,
                keySize
                );

            return Convert.ToHexString(hash);
        }
    }
}
