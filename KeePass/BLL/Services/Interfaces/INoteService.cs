using Domain.Models;

namespace BLL.Services.Interfaces
{
    public interface INoteService
    {
        Task<Note?> AddAsync(Note note);

        Task<Note?> GetNoteAsync(int id);
        Task<IEnumerable<Note>> GetByCollectionIdAsync(int collectionId);

        Task ChangeNoteNameAsync(int id, string newName);
        Task ChangeNotePassword(int id, string newPassword);

        Task DeleteNoteAsync(int id);

    }
}
