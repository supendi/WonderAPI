using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace WonderAPI.Controllers.Account
{
    /// <summary>
    /// Interface to hash a password
    /// </summary>
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string hashedPassword);
    }

    /// <summary>
    /// Implements IPasswordHasher and it use Pbkdf2 Algorithm
    /// </summary>
    public class Pbkdf2Hasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        public bool Verify(string password, string hashedPassword)
        {
            return Hash(password) == hashedPassword;
        } 
    }
}
