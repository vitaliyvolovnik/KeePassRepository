using Domain.Models;
using System.Collections.ObjectModel;

namespace BLL.Models.Dtos
{
    public class CollectionDto
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public ObservableCollection<NoteDto>? Notes { get; set; } = new ObservableCollection<NoteDto>();

        public FolderShortDto? Folder { get; set; }
        public int? FolderId { get; set; }

        public Collection ToEntity()
        {
            return new Collection()
            {
                Id = Id,
                Name = Name,
                Notes = new ObservableCollection<Note>(Notes.Select(x => x.ToEntity())),
                Folder = Folder?.ToEntity(),
                FolderId = FolderId
            };
        }

    }
}
