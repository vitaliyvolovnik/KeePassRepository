namespace Domain.Models
{
    public class Collection
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Note>? Notes { get; set; }

        public Folder? Folder { get; set; }
        public int? FolderId { get; set; }
    }
}
