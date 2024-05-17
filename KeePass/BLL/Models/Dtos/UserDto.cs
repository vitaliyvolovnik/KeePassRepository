using Domain.Models;

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
