using BLL.Services.Interfaces;
using DAL.Repositories;
using Domain.Models;

namespace BLL.Services
{
    public class FolderSerivce : IFolderService, ICollectionService
    {

        private readonly FolderRepository _folderRepository;
        private readonly CollectionRepository _collectionRepository;


        public FolderSerivce(CollectionRepository collectionRepository, FolderRepository folderRepository)
        {
            _collectionRepository = collectionRepository;
            _folderRepository = folderRepository;
        }

        public async Task<Folder?> AddAsync(Folder folder)
        {
            if (await _folderRepository.IsExistAsync(folder.Name))
                return null;

            return await _folderRepository.CreateAsync(folder);
        }

        public async Task<Collection?> AddAsync(Collection collection)
        {
            if (await _collectionRepository.IsExistAsync(collection.Name))
                return null;

            return await _collectionRepository.CreateAsync(collection);
        }

        public async Task DeleteCollectionAsync(int id) => await _collectionRepository.DeleteAsync(x => x.Id == id);

        public async Task DeleteFolderAsync(int id) => await _folderRepository.DeleteAsync(x => x.Id == id);

        public async Task<IEnumerable<Collection>> GetAllCollectionAsync() => await _collectionRepository.GetAllAsync();

        public async Task<IEnumerable<Folder>> GetAllFoldersAsync() => await _folderRepository.GetAllAsync();

        public async Task<Collection?> GetCollectionAsync(int id)=> await _collectionRepository.FindFirstAsync(x=>x.Id==id);

        public async Task<Folder?> GetFolderAsync(int id) => await _folderRepository.FindFirstAsync(x => x.Id == id);

        public Task<IEnumerable<Collection>> GetByFolderIdAsync(int folderId)=> _collectionRepository.FindByConditionAsync(x => x.FolderId == folderId);

        public async Task<Collection?> RenameCollectionAsync(int id, string newName)
        {
            return await _collectionRepository.UpdateAsync(id, new Collection()
            {
                Name = newName
            });
        }

        public async Task<Folder?> RenameFolderAsync(int id, string newName)
        {
            return await _folderRepository.UpdateAsync(id, new Folder()
            {
                Name = newName
            });
        }
    }
}
