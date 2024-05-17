using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeePass.Domain
{
    public static class PasswordHelper
    {
        public static string GenerateRandomPassword(int length,bool useUppercase, bool useNumbers, bool useSpecialSymbols)
        {
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            const string specialSymbols = "!@#$%&*_+?";

            StringBuilder characterSet = new StringBuilder(lowercase);

            if (useUppercase)
                characterSet.Append(uppercase);

            if (useNumbers)
                characterSet.Append(numbers);

            if (useSpecialSymbols)
                characterSet.Append(specialSymbols);

            StringBuilder password = new StringBuilder(length);
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(characterSet.Length);
                password.Append(characterSet[index]);
            }

            return password.ToString();


        }

    }
}
