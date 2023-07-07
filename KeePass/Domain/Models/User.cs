namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? MasterPassword { get; set; }

        public List<Folder>? Folders { get; set; }
    }

    
}
