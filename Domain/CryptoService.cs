using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Domain.Models;
using Konscious.Security.Cryptography;
using Sodium;
using static System.String;

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
            argon.MemorySize = 2028;
            argon.Iterations = 15;
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
        
        private byte[] GetKey()
        {
            var key = new byte[32];
            RandomNumberGenerator.Create().GetBytes(key);
            return key;
        }

        private byte[] GetNonce()
        {
            var nonce = new byte[24];
            RandomNumberGenerator.Create().GetBytes(nonce);
            return nonce;
        }

        public (byte[], byte[]) EncryptData(string data)
        {
            var nonce = GetNonce();
            // var bytes = Enumerable.Range(0, data.Length)
            //     .Where(x => x % 2 == 0)
            //     .Select(x => Convert.ToByte(data.Substring(x, 2), 16))
            //     .ToArray();
            var bytes = Encoding.Default.GetBytes(data);
            return (nonce, SecretAeadXChaCha20Poly1305.Encrypt(bytes, nonce, new byte[32]));
        }
        
        public byte[] DecryptData(string data, byte[]nonce)
        {
            var bytes = Enumerable.Range(0, data.Length)
                                        .Where(x => x % 2 == 0)
                                        .Select(x => Convert.ToByte(data.Substring(x, 2), 16))
                                        .ToArray();
            var tmp = SecretAeadXChaCha20Poly1305.Decrypt(bytes, nonce, new byte[32]);
            return tmp;
        }
    }
}