using BLL.Models.Dtos;
using BLL.Services;
using Domain.Models;
using System.Collections.ObjectModel;

namespace BLL.Extensions
{
    public static class FolderExtensions
    {
        public static FolderDto ToDto(this Folder folder, CryptographyService cryptography)
        {
            if (folder == null) return null;
            return new FolderDto()
            {
                Id = folder.Id,
                Name = folder.Name,
                Collections = new ObservableCollection<CollectionDto>(folder.Collections.Select(x => x.ToDto(cryptography))),
                UserId = folder.UserId,
            };
        }
        public static FolderShortDto ToShortDto(this Folder folder)
        {
            if (folder is null) return null;
            return new FolderShortDto()
            {
                Id = folder.Id,
                Name = folder.Name,
                UserId = folder.UserId,
            };
        }
    }
}
