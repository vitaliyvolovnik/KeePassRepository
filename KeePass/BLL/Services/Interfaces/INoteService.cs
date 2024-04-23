using BLL.Models.Dtos;
using Domain.Models;

namespace BLL.Services.Interfaces
{
    public interface INoteService
    {
        Task<NoteDto?> AddAsync(NoteDto note);

        Task<NoteDto?> GetNoteAsync(int id);
        Task<IEnumerable<NoteDto>> GetByCollectionIdAsync(int collectionId);

        Task ChangeNoteNameAsync(int id, string newName);
        Task ChangeNotePassword(int id, string newPassword);

        Task DeleteNoteAsync(int id);

    }
}
