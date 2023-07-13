using Domain.Models;

namespace BLL.Services.Interfaces
{
    public interface ICollectionService
    {

        Task<Collection?> AddAsync(Collection collection);

        Task<Collection?> GetCollectionAsync(int id);
        Task<IEnumerable<Collection>> GetAllCollectionAsync();
        Task<IEnumerable<Collection>> GetByFolderIdAsync(int folderId);

        Task DeleteCollectionAsync(int id);
        Task<Collection?> RenameCollectionAsync(int id, string newName);
    }
}
