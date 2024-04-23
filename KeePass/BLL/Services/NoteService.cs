using BLL.Extensions;
using BLL.Models.Dtos;
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

        public async Task<NoteDto?> AddAsync(NoteDto note)
        {
            var dto = await _noteRepository.CreateAsync(note.ToEntity());
            return dto.ToDto(_cryptographyService);
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

        public async Task<IEnumerable<NoteDto>> GetByCollectionIdAsync(int collectionId)
        {
            return (await _noteRepository.FindByConditionAsync(x => x.CollectionId == collectionId)).Select(x=>x.ToDto(_cryptographyService));
        }

        public async Task<NoteDto?> GetNoteAsync(int id)
        {
            return (await _noteRepository.FindFirstAsync(x => x.Id == id))?.ToDto(_cryptographyService);
        }
    }
}
