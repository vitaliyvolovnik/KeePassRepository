using BLL.Models.Dtos;
using Domain.Models;

namespace BLL.Services.Interfaces
{
    public interface ICollectionService
    {

        Task<CollectionDto?> AddAsync(CollectionDto collection);

        Task<CollectionDto?> GetCollectionAsync(int id);
        Task<IEnumerable<CollectionDto>> GetAllCollectionAsync();
        Task<IEnumerable<CollectionDto>> GetByFolderIdAsync(int folderId);

        Task DeleteCollectionAsync(int id);
        Task<CollectionDto?> RenameCollectionAsync(int id, string newName);
    }
}
