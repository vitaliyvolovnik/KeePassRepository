using BLL.Services;

namespace BLL.Models.Dtos
{
    public class SecurePassword
    {
        private readonly CryptographyService _cryptographyService;

        public SecurePassword(CryptographyService cryptographyService)
        {
            this._cryptographyService = cryptographyService;
        }

        public string PasswordHash { set; get; } = string.Empty;


        public string Password
        {
            get { return _cryptographyService.Decrypt(PasswordHash); }
            set { PasswordHash = _cryptographyService.Encrypt(value); }
        }
    }
}
