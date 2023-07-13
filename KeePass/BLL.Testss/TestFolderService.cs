using BLL.Services;
using DAL.Context;
using DAL.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BLL.Testss
{
    public class TestFolderService
    {

        private FolderRepository _folderRepository;
        private CollectionRepository _collectionRepository;
        private UserRepository _userRepository;

        private FolderSerivce _folderService;

        KeyPassContext context;

        private User? currentUser;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var options = new DbContextOptionsBuilder<KeyPassContext>()
               .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KeyPassTestDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;")
               .Options;

            context = new(options);
            _userRepository = new(context);
            _folderRepository = new(context);
            _collectionRepository = new(context);
            _folderService = new(_collectionRepository, _folderRepository);
        }

        [SetUp]
        public void SetUp()
        {
            

            _userRepository.DeleteAllAsync().Wait();

            currentUser = _userRepository.CreateAsync(new() { MasterPassword = "password" }).Result;

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        [Test]
        public void CreateFolder_ReturnCreatedFolder()
        {
            //Arrange
            Folder folder = new Folder() { UserId = currentUser.Id, Name = "Folder 1(TestCreate)" };


            //Act
            var result = _folderService.AddAsync(folder);


            //Assert
            Assert.IsNotNull(result.Result, "returned data is NULL");
        }

        [Test]
        public void CreateCollection_ReturnCreatedCollection()
        {
            //Arrange
            Folder folder = new() { UserId = currentUser.Id, Name = "Folder 1.1(TestCreateCollection)" };
            var created = _folderRepository.CreateAsync(folder);
            string name = "Collection 1.1";
            Collection collection = new() { Name = name, FolderId = created.Result.Id };

            //Act
            var result = _folderService.AddAsync(collection);


            //Assert
            Assert.AreEqual(name, result.Result.Name, "the creader and expected name are different");
            Assert.IsNotNull(result.Result, "returned data is NULL");
        }

        [Test]
        public void RenameFolder_ReturnUpdatedFolder()
        {
            //Arrange
            Folder folder = new Folder() { UserId = currentUser.Id, Name = "Folder 1(TestRename)" };
            var created = _folderRepository.CreateAsync(folder);
            string name = "Folder 2(TestRename)";

            //Act
            var result = _folderService.RenameFolderAsync(created.Result.Id, name);


            //Assert
            Assert.IsNotNull(result.Result, "returned data is NULL");
            Assert.AreEqual(name, result.Result.Name, "the exepted and result name are not equal");
        }

        [Test]
        public void RenameCollection_ReturnUpdatedCollection()
        {
            //Arrange
            Folder folder = new Folder() { UserId = currentUser.Id, Name = "Folder 1(TestRenameCollection)" };
            var created = _folderRepository.CreateAsync(folder);

            Collection collection = new() { Name = "", FolderId = created.Result.Id };
            var createdCollection = _collectionRepository.CreateAsync(collection);
            string name = "Collection 1.2(Updated)";

            //Act
            var result = _folderService.RenameCollectionAsync(createdCollection.Result.Id, name);


            //Assert
            Assert.IsNotNull(result.Result, "returned data is NULL");
            Assert.AreEqual(name, result.Result.Name, "the exepted and result name are not equal");
        }

        [Test]
        public void GetFolderAsync_ReturnFolder()
        {
            // Arrange
            string name = "Folder 1(GetFolder)";
            Folder folder = new() { UserId = currentUser.Id, Name = name };
            var created = _folderRepository.CreateAsync(folder);

            // Act
            var result = _folderService.GetFolderAsync(created.Result.Id);

            //Assers
            Assert.AreEqual(name, result.Result.Name);
        }

        [Test]
        public void GetFolderCollections_ReturList()
        {
            // Arrange
            string name = "Folder 1(GetFolder)";
            Folder folder = new()
            {
                UserId = currentUser.Id,
                Name = name,
                Collections = new List<Collection>(){
                    new Collection(){Name = "Collection 1"},
                    new Collection(){Name = "Collection 2"},
                    new Collection(){Name = "Collection 3"}
                }
            };
            var created = _folderRepository.CreateAsync(folder);

            // Act
            var result = _folderService.GetByFolderIdAsync(created.Result.Id);

            //Assers
            Assert.AreEqual(folder.Collections.Count, result.Result.Count());
        }

        [Test]
        public void GetNotExistingFolderCollections_ReturnEmptyList()
        {
            // Arrange
            string name = "Folder 1(GetFolder)";
            Folder folder = new()
            {
                UserId = currentUser.Id,
                Name = name,
                Collections = new List<Collection>(){
                    new Collection(){Name = "Collection 1"},
                    new Collection(){Name = "Collection 2"},
                    new Collection(){Name = "Collection 3"}
                }
            };
            var created = _folderRepository.CreateAsync(folder);

            // Act
            var result = _folderService.GetByFolderIdAsync(created.Result.Id + 1);

            //Assers
            Assert.AreEqual(0, result.Result.Count());
        }

        [Test]
        public void GetEmptyFolderCollections_ReturnEmptyList()
        {
            // Arrange
            string name = "Folder 1(GetFolder)";
            Folder folder = new()
            {
                UserId = currentUser.Id,
                Name = name,

            };
            var created = _folderRepository.CreateAsync(folder);

            // Act
            var result = _folderService.GetByFolderIdAsync(created.Result.Id);

            //Assers
            Assert.AreEqual(0, result.Result.Count());
        }

        [Test]
        public void DelecteExistFolder()
        {
            // Arrange

            _folderRepository.CreateAsync(new() { UserId = currentUser.Id, Name = "Folder 1.1.1" });
            _folderRepository.CreateAsync(new() { UserId = currentUser.Id, Name = "Folder 1.1.2" });
            var created = _folderRepository.CreateAsync(new()
            {
                UserId = currentUser.Id,
                Name = "Folder 1.1.3"
            });

            // Act
            _folderService.DeleteFolderAsync(created.Result.Id);

            //Assers
            var result = _folderRepository.GetAllAsync();
            Assert.AreEqual(2, result.Result.Count());
        }

        

    }
}
