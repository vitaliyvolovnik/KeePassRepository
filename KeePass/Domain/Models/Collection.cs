using System.Collections.ObjectModel;

namespace Domain.Models
{
    public class Collection
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public ObservableCollection<Note>? Notes { get; set; } = new ObservableCollection<Note>();

        public Folder? Folder { get; set; }
        public int? FolderId { get; set; }
    }
}
