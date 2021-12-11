using System;
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
        
        private byte[] GetSalt()
        {
            var buffer = new byte[32];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }
            
        public (string, string) HashPassword(string password)
        {
            var argon = new Argon2i(GetSHA(password));
            argon.DegreeOfParallelism = 16;
            argon.MemorySize = 8192;
            argon.Iterations = 40;
            argon.Salt = GetSalt();
            // argon2.AssociatedData = userUuidBytes;

            var hashout = Regex.Replace(BitConverter.ToString(argon.GetBytes(512)).ToLower(), "-", "");
            var saltout = Regex.Replace(BitConverter.ToString(argon.Salt).ToLower(), "-", "");

            return (saltout, hashout);
        }

        // public bool IsRightPassword(string password, string hash)
        // {
        //     return hash == HashPassword(password);
        // }
    }
}