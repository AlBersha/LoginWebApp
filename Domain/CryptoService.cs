using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
using Konscious.Security.Cryptography;
using Sodium;

namespace Domain.Interfaces
{
    public class CryptoService: ICryptoService
    {
        private readonly byte[] Key;
        private const string KeyId = "af016287-b1ec-46f5-85f7-43228ad72caa";
        private const string KeyARN = "arn:aws:kms:us-east-2:662983123043:key/af016287-b1ec-46f5-85f7-43228ad72caa";
        private const string AccessKeyId = "AKIAZUXG7UBRXWWJHNNZ";
        private const string AWSSecretKey = "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";
        private const string ServiceUrl = "https://kms.us-east-2.amazonaws.com";
        
        private AmazonKeyManagementServiceClient kmsClient;
        public CryptoService()
        {
            kmsClient = GetClient();
            var dataKeyRequest = new GenerateDataKeyRequest()
            {
                KeyId = KeyId,
                KeySpec = DataKeySpec.AES_128
            };

            // var resp = kmsClient.GetPublicKeyAsync(new GetPublicKeyRequest {KeyId = KeyId});
            var dataKeyResponse = kmsClient.GenerateDataKeyAsync(dataKeyRequest).Result;
        
            
            Key = dataKeyResponse.Plaintext.ToArray();
            // var encryptedKey = dataKeyResponse.CiphertextBlob;
        }
        
        private AmazonKeyManagementServiceClient GetClient()
        {
            if (kmsClient == null)
            {
                try
                {
                    var clientConfig = new AmazonKeyManagementServiceConfig
                    {
                        // Set the endpoint URL
                        ServiceURL = ServiceUrl
                    };
                    kmsClient = new AmazonKeyManagementServiceClient(AccessKeyId, AWSSecretKey, clientConfig);
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine(ex.Message);
                    throw;
                }
                
            }
            return kmsClient;
        }
        
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
        
        private byte[] GetNonce()
        {
            var nonce = new byte[24];
            RandomNumberGenerator.Create().GetBytes(nonce);
            return nonce;
        }

        public (byte[], byte[]) EncryptData(string data)
        {
            var nonce = GetNonce();
            var bytes = Encoding.Default.GetBytes(data);
            return (nonce, SecretAeadXChaCha20Poly1305.Encrypt(bytes, nonce, new byte[32]));
        }
        
        public byte[] DecryptData(string data, byte[]nonce)
        {
            var bytes = Enumerable.Range(0, data.Length)
                                        .Where(x => x % 2 == 0)
                                        .Select(x => Convert.ToByte(data.Substring(x, 2), 16))
                                        .ToArray();
            return SecretAeadXChaCha20Poly1305.Decrypt(bytes, nonce, new byte[32]);
        }
    }
}