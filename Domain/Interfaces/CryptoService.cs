using System;
using System.Security.Cryptography;
using System.Text;
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
            
        public string HashPassword(string password)
        {
            var argon = new Argon2i(GetSHA(password));
            argon.DegreeOfParallelism = 16;
            argon.MemorySize = 8192;
            argon.Iterations = 40;
            // argon2.Salt = salt;
            // argon2.AssociatedData = userUuidBytes;

            return Encoding.UTF8.GetString(argon.GetBytes(512));    
        }

        public bool IsRightPassword(string password, string hash)
        {
            return hash == HashPassword(password);
        }
    }
}