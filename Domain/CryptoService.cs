using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Konscious.Security.Cryptography;

namespace Domain.Interfaces
{
    public class CryptoService
    {
        private byte[] GetSHA(string password)
        {
            using var shaHash = SHA256.Create();
            return shaHash.ComputeHash(Encoding.Default.GetBytes(password));
        } 
        
        public byte[] GetSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }
            
        public byte[] HashPassword(string password, byte[] salt)
        {
            var argon = new Argon2i(GetSHA(password));
            argon.DegreeOfParallelism = 8;
            argon.MemorySize = 1024;
            argon.Iterations = 4;
            argon.Salt = salt;

            return argon.GetBytes(128);
        }
        public bool IsRightPassword(string password, string salt, string hash)
        {
            var saltBytes = Enumerable.Range(0, salt.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(salt.Substring(x, 2), 16))
                .ToArray();

            var hashBytes = Enumerable.Range(0, hash.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hash.Substring(x, 2), 16))
                .ToArray();

            var newHash = HashPassword(password, saltBytes);

            return hashBytes.SequenceEqual(newHash);
        }
    }
}