using BLL.Models.Dtos;
using BLL.Services;
using Domain.Models;
using KeePass.Core;
using KeePass.Models;
using KeePass.View;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KeePass.ViewModel
{


    public class ExplorerViewModel : BaseViewModel
    {

        private readonly FolderSerivce _folderService;
        private readonly User _currentUser;
        



        public ExplorerViewModel(FolderSerivce folderService, User currentUser)
        {
            _folderService = folderService;
            _currentUser = currentUser;
            Initialize();
        }

        private ObservableCollection<FolderDto> folders;
        public ObservableCollection<FolderDto> Folders
        {
            get { return folders ??= new(); }
            set { folders = value; }
        }

        private FolderDto selectedFolder;
        public FolderDto SelectedFolder
        {
            get => selectedFolder;
            set { selectedFolder = value; OnPropertyChanged(nameof(SelectedFolder)); }
        }


        private async Task Initialize()
        {
            foreach (var item in await _folderService.GetAllFoldersAsync())
            {
                Folders.Add(item); 
            };
        }


        #region ChangeCollection
        //TODO: set selected collection
        private Collection currentCollection;

        
        private event Action<int> onCollectionChanged;

        public event Action<int> OnCollectionChanged
        {
            add { onCollectionChanged += value; }
            remove { onCollectionChanged -= value; }
        }

        private ICommand collectionChangeCommand;

        public ICommand CollectionChangeCommand
        {
            get { return collectionChangeCommand ??= new AsyncRelayCommand(changeCollectionAsync); }
        }

        private async Task changeCollectionAsync(object obj)
        {
            if(obj is CollectionDto colleciton)
            {
                await Task.Run(() => onCollectionChanged?.Invoke(colleciton.Id));
            }
        }

        #endregion

        #region AddFolder

        private ICommand addFolderCommand;

        public ICommand AddFolderCommand => addFolderCommand ??= new AsyncRelayCommand(AddFolder);

        private async Task AddFolder(object obj)
        {
            var createWind = new CreateFolderWindow();

            createWind.ShowDialog();

            if (createWind.IsOk)
            {
                var created = await _folderService.AddAsync(new FolderDto() { Name = createWind.Name, UserId = _currentUser.Id });

                if (created != null)
                {
                    this.Folders.Add(created);
                }

            }
        }
        #endregion

        #region DeleteFolder
        public ICommand deleteFolderCommand;

        public ICommand DeleteFolderComand => deleteFolderCommand ??= new AsyncRelayCommand(DeleteFolder);

        private async Task DeleteFolder(object obj)
        {
            if (obj is Control control)
            {
                var id = int.Parse(control.Tag.ToString());
                await _folderService.DeleteFolderAsync(id);

                folders = new ObservableCollection<FolderDto>();
                var all = await _folderService.GetAllFoldersAsync();
                foreach (var item in all)
                    folders.Add(item);
            }
        }
        #endregion

        #region AddCollection
        public ICommand addCollectionCommand;

        public ICommand AddCollectionCommand => addCollectionCommand ??= new AsyncRelayCommand(AddCollection,AddCollectionCanExecute);

        private bool AddCollectionCanExecute(object arg)
        {
            return selectedFolder is not null;
        }

        private async Task AddCollection(object obj)
        {
            
            var createWind = new CreateCollectionWindow();

            createWind.ShowDialog();

            if (createWind.IsOk)
            {
                var created = await _folderService.AddAsync(new CollectionDto() { Name = createWind.Name, FolderId = selectedFolder.Id });

                if (created != null)
                {
                    selectedFolder.Collections?.Add(created);
                    OnPropertyChanged("Folders");
                }
                else
                {
                    MessageBox.Show("Cant create collection");
                }
            }

        }

        #endregion

        #region DelleteCollection
        public ICommand deleteCollectionCommand;

        public ICommand DeleteCollectionComand => deleteCollectionCommand ??= new AsyncRelayCommand(DeleteCollection);

        private async Task DeleteCollection(object obj)
        {
            if (obj is Control control)
            {
                var ids = control.Tag.ToString().Split(":");
                var collectionId = int.Parse(ids[0]);
                var folderId = int.Parse(ids[1]);
                await _folderService.DeleteCollectionAsync(collectionId);

                var collecitons = await _folderService.GetByFolderIdAsync(folderId);

                folders.FirstOrDefault(x => x.Id == folderId).Collections = new ObservableCollection<CollectionDto>(collecitons);

                OnPropertyChanged("Folders");
            }
        }
        #endregion
    }
}


