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
    public static class FolderExtensions
    {
        public static FolderDto  ToDto(this Folder folder, CryptographyService cryptography)
        {
            return new FolderDto()
            {
                Id = folder.Id,
                Name = folder.Name,
                Collections = new ObservableCollection<CollectionDto>(folder.Collections.Select(x => x.ToDto(cryptography)))
            };
        }
        public static FolderShortDto ToShortDto(this Folder folder) 
        {
            return new FolderShortDto()
            {
                Id = folder.Id,
                Name = folder.Name,
            };
        }
    }
}
