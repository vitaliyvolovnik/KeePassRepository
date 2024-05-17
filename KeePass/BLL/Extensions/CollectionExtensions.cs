using BLL.Models.Dtos;
using BLL.Services;
using Domain.Models;
using System.Collections.ObjectModel;

namespace BLL.Extensions
{
    public static class CollectionExtensions
    {
        public static CollectionDto ToDto(this Collection collection, CryptographyService cryptography)
        {
            if (collection is null) return null;

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
