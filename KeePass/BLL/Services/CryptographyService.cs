using System.Security.Cryptography;
using System.Text;

namespace BLL.Services
{
    public class CryptographyService
    {
        private readonly Aes aes;
        private readonly string SALT;

        public CryptographyService(string aesKey, string salt)
        {
            SALT = salt;

            aes = Aes.Create();
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Key = Encoding.UTF8.GetBytes(aesKey);
        }

        public string Encrypt(string plaintext)
        {
            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            {
                byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
                return Convert.ToBase64String(encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length))  ;
            }
        }

        public string Decrypt(string ciphertext)
        {   
            byte[] ciphertextBytes = Encoding.UTF8.GetBytes(ciphertext);

            using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(ciphertextBytes, 0, ciphertext.Length);
                return  Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        public string HashPassword(string password)
        {
            password += SALT;
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); 
            }
        }




    }
}
