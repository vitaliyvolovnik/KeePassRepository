using BLL.Models.Dtos;
using BLL.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
