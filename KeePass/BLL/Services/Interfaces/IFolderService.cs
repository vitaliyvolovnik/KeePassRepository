using Domain.Models;

namespace BLL.Services.Interfaces
{
    public interface IFolderService
    {

        Task<Folder?> AddAsync(Folder folder);

        Task DeleteFolderAsync(int id);
        Task<Folder?> GetFolderAsync(int id);
        Task<IEnumerable<Folder>> GetAllFoldersAsync();
        Task<Folder?> RenameFolderAsync(int id, string newName);
    }
}
