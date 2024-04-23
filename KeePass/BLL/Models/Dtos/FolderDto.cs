using Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Dtos
{
    public class FolderDto
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public ObservableCollection<CollectionDto>? Collections { get; set; } = new ObservableCollection<CollectionDto>();

        public int UserId { get; set; }

        public Folder ToEnitty()
        {
            return new Folder()
            {
                Id = Id,
                Name = Name,
                Collections = new ObservableCollection<Collection>(Collections?.Select(x=>x.ToEntity())),
                UserId = UserId
            };
        }
    }
}
