using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Dtos
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public SecurePassword? SecurePassword { get; set; }

        public int? CollectionId { get; set; }

        public Note ToEntity()
        {
            return new Note
            {
                Id = Id,
                Name = Name,
                CollectionId = CollectionId,
                Password = SecurePassword?.PasswordHash
            };
        }
    }
}
