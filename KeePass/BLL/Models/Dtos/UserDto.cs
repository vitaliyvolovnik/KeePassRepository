using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Dtos
{
    public class UserDto
    {

        public int Id { get; set; }

        public string? MasterPassword { get; set; }

        public List<FolderDto>? Folders { get; set; }

        public User ToEntity()
        {
            return new User()
            {
                Id = Id,
                MasterPassword = MasterPassword,
                Folders = Folders?.Select(x => x.ToEnitty()).ToList(),
            };
            
        }

    }
}
