namespace Domain.Models
{
    public class Folder
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Collection>? Collections { get; set; }

        public User? User { get; set; }
        public int UserId { get; set; }
    }
}
