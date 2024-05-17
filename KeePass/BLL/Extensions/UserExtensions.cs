using BLL.Models.Dtos;
using BLL.Services;
using Domain.Models;

namespace BLL.Extensions
{
    public static class UserExtensions
    {
        public static UserDto ToDto(this User user, CryptographyService cryptography)
        {
            return new UserDto()
            {
                Id = user.Id,
                MasterPassword = user.MasterPassword,
                Folders = user.Folders?.Select(x => x.ToDto(cryptography)).ToList(),

            };
        }
    }
}
