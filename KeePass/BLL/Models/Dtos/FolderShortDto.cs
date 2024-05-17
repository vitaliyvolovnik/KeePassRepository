using Domain.Models;

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
