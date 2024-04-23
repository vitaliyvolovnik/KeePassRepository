using BLL.Extensions;
using BLL.Models.Dtos;
using BLL.Services.Interfaces;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Domain.Models;

namespace BLL.Services
{
    public class FolderSerivce : IFolderService, ICollectionService
    {

        private readonly IFolderRepository _folderRepository;
        private readonly ICollectionRepository _collectionRepository;
        private readonly CryptographyService _cryptographyService;


        public FolderSerivce(
            ICollectionRepository collectionRepository, 
            IFolderRepository folderRepository,
            CryptographyService cryptographyService)
        {
            _collectionRepository = collectionRepository;
            _folderRepository = folderRepository;
            _cryptographyService = cryptographyService;
        }

        public async Task<FolderDto?> AddAsync(FolderDto folder)
        {
            if (await _folderRepository.IsExistAsync(folder.Name))
                return null;

            var createdFolder = await _folderRepository.CreateAsync(folder.ToEnitty());
            return createdFolder?.ToDto(_cryptographyService);
        }

        public async Task<CollectionDto?> AddAsync(CollectionDto collection)
        {
            if (await _collectionRepository.IsExistAsync(collection.Name))
                return null;

            var dto = await _collectionRepository.CreateAsync(collection.ToEntity());
            return dto.ToDto(_cryptographyService);
        }

        public async Task DeleteCollectionAsync(int id) => await _collectionRepository.DeleteAsync(x => x.Id == id);

        public async Task DeleteFolderAsync(int id) => await _folderRepository.DeleteAsync(x => x.Id == id);

        public async Task<IEnumerable<CollectionDto>> GetAllCollectionAsync()
            => (await _collectionRepository.GetAllAsync()).Select(x => x.ToDto(_cryptographyService));

        public async Task<IEnumerable<FolderDto>> GetAllFoldersAsync()
            => (await _folderRepository.GetAllAsync()).Select(x => x.ToDto(_cryptographyService));

        public async Task<CollectionDto?> GetCollectionAsync(int id)
            => (await _collectionRepository.FindFirstAsync(x => x.Id == id)).ToDto(_cryptographyService);

        public async Task<FolderDto?> GetFolderAsync(int id)
            => (await _folderRepository.FindFirstAsync(x => x.Id == id)).ToDto(_cryptographyService);

        public async Task<IEnumerable<CollectionDto>> GetByFolderIdAsync(int folderId)
            => (await _collectionRepository.FindByConditionAsync(x => x.FolderId == folderId)).Select(x => x.ToDto(_cryptographyService));

        public async Task<CollectionDto?> RenameCollectionAsync(int id, string newName)
        {
            var colleciton = await _collectionRepository.UpdateAsync(id, new Collection()
            {
                Name = newName
            });
            return colleciton.ToDto(_cryptographyService);
        }

        public async Task<FolderDto?> RenameFolderAsync(int id, string newName)
        {
            var folder =  await _folderRepository.UpdateAsync(id, new Folder()
            {
                Name = newName
            });
            return folder.ToDto(_cryptographyService);
        }

        public async Task SaveAsync()
        {
            await _collectionRepository.SaveChangesAsync();
        }
    }
}
