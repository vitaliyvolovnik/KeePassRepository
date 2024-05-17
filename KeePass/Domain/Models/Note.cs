namespace Domain.Models
{
    public class Note
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }

        public Collection? Collrction { get; set; }
        public int? CollectionId { get; set; }
    }
}
