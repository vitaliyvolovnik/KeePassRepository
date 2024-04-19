using BLL.Models.Dtos;
using BLL.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extensions
{
    public static class CollectionExtensions
    {
        public static CollectionDto ToDto(this Collection collection,CryptographyService cryptography)
        {
            return new CollectionDto()
            {
                Id = collection.Id,
                Name = collection.Name,
                Folder = collection.Folder.ToShortDto(),
                FolderId = collection.FolderId,
                Notes = new ObservableCollection<NoteDto>(collection.Notes.Select(x => x.ToDto(cryptography)))
            };
            
        }
    }
}
