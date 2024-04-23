using BLL.Models.Dtos;
using Domain.Models;

namespace BLL.Services.Interfaces
{
    public interface IFolderService
    {

        Task<FolderDto?> AddAsync(FolderDto folder);

        Task DeleteFolderAsync(int id);
        Task<FolderDto?> GetFolderAsync(int id);
        Task<IEnumerable<FolderDto>> GetAllFoldersAsync();
        Task<FolderDto?> RenameFolderAsync(int id, string newName);
    }
}
