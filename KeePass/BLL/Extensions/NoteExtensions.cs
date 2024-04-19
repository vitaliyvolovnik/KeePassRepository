using BLL.Models.Dtos;
using BLL.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extensions
{
    public static class NoteExtensions
    {
        public static NoteDto ToDto(this Note note,CryptographyService cryptographyService)
        {
            return new NoteDto()
            {
                Id = note.Id,
                Name = note.Name,
                CollectionId = note.CollectionId,
                SecurePassword = new SecurePassword(cryptographyService) { PasswordHash = note.Password },
            };
        }
    }
}
