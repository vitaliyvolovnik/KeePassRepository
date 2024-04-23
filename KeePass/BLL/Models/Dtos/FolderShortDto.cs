using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Dtos
{
    public class FolderShortDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int UserId { get; set; }

        internal Folder ToEntity()
        {
            return new Folder { Id = Id, Name = Name, UserId = UserId };

        }
    }
}
