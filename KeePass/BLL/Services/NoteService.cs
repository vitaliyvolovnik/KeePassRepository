using BLL.Services.Interfaces;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Domain.Models;

namespace BLL.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly CryptographyService _cryptographyService;


        public NoteService(INoteRepository noteRepository, CryptographyService cryptography)
        {
            _noteRepository = noteRepository;
            _cryptographyService = cryptography;
        }

        public async Task<Note?> AddAsync(Note note)
        {
            note.Password = _cryptographyService.Encrypt(note.Password);
            return await _noteRepository.CreateAsync(note);
        }

        public async Task ChangeNoteNameAsync(int id, string newName)
        {
            await _noteRepository.UpdateAsync(id, new Note() { Name = newName });
        }

        public async Task ChangeNotePassword(int id, string newPassword)
        {
            await _noteRepository.UpdateAsync(id, new Note() 
            { Password = _cryptographyService.Encrypt(newPassword)});
        }

        public async Task DeleteNoteAsync(int id)
        {
            await _noteRepository.DeleteAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Note>> GetByCollectionIdAsync(int collectionId)
        {
            return await _noteRepository.FindByConditionAsync(x => x.CollectionId == collectionId);
        }

        public async Task<Note?> GetNoteAsync(int id)
        {
            return await _noteRepository.FindFirstAsync(x => x.Id == id);
        }
    }
}
