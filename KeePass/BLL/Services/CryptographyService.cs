using Domain.Models;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services
{
    public class CryptographyService
    {
        private Aes? aes;
        private Aes Aes
        {
            get
            {
                return aes ??= CreateAes();
            }
        }
        private readonly string SALT;
        private readonly User _user;

        public CryptographyService(string salt, User user)
        {
            SALT = salt;
            _user = user;
        }

        private Aes CreateAes()
        {
            Rfc2898DeriveBytes passwordBytes = new Rfc2898DeriveBytes(_user.MasterPassword, Encoding.ASCII.GetBytes(this.SALT));

            var aes = Aes.Create();
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Key = passwordBytes.GetBytes(32);
            aes.IV = passwordBytes.GetBytes(16);

            return aes;
        }

        public string Encrypt(string plaintext)
        {

            byte[] planeTextBytes = Encoding.UTF8.GetBytes(plaintext);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, Aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(planeTextBytes, 0, planeTextBytes.Length);

                }
                var txt = Convert.ToBase64String(ms.ToArray());
                return txt;
            }
        }

        public string Decrypt(string ciphertext)
        {
            byte[] ciphertextBytes = Convert.FromBase64String(ciphertext);


            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, Aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(ciphertextBytes, 0, ciphertextBytes.Length);

                }
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public string HashPassword(string password)
        {
            password += SALT;
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
        }





    }
}


