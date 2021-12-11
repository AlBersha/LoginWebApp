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
            return shaHash.ComputeHash(Encoding.UTF8.GetBytes(password));
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
            argon.MemorySize = 1024*1024;
            argon.Iterations = 4;
            argon.Salt = salt;

            return argon.GetBytes(64);
        }
        private bool IsRightPassword(string password, byte[] salt, byte[] hash)
        {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash);
        }
    }
}