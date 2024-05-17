using System.Collections.ObjectModel;

namespace Domain.Models
{
    public class Folder
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public ObservableCollection<Collection>? Collections { get; set; } = new ObservableCollection<Collection>();

        public User? User { get; set; }
        public int UserId { get; set; }
    }
}
