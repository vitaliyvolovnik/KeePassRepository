using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Dtos
{
    public class SecurePassword
    {
        private readonly CryptographyService _cryptographyService;

        public SecurePassword(CryptographyService cryptographyService)
        {
            this._cryptographyService = cryptographyService;
        }

        public string PasswordHash { set; get; }


        public string Password
        {
            get { return _cryptographyService.Decrypt(PasswordHash); }
            set { PasswordHash = _cryptographyService.Encrypt(value); }
        }
    }
}
