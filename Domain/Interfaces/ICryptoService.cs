namespace Domain.Interfaces
{
    public interface ICryptoService
    {
        byte[] HashPassword(string password, byte[] salt);
        bool IsRightPassword(string password, string salt, string hash);
        (byte[], byte[]) EncryptData(string data);
        byte[] DecryptData(string data, byte[] nonce);
        byte[] GetSalt();
    }
}