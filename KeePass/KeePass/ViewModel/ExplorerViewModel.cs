using BLL.Services;
using Domain.Models;
using KeePass.Core;
using KeePass.Models;
using KeePass.View;
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

        private ObservableCollection<ExplorerItem> folders;
        public ObservableCollection<ExplorerItem> Folders
        {
            get { return folders ??= new(); }
            set { folders = value; }
        }

        private async Task Initialize()
        {
            (await _folderService.GetAllFoldersAsync()).Select(x => new ExplorerItem() { Folder = x, IsVisible = false }).ToList().ForEach(x => Folders.Add(x));
        }



        #region ChangeVisibility 

        private ICommand changeFolderVisibility;

        public ICommand ChangeFolderVisibility => changeFolderVisibility ??= new RelayCommand(ChangeVisibility);
        
        private void ChangeVisibility(object arg)
        {
            int id = (int)arg;
            var item = this.Folders.First(x => x.Folder.Id == id);
            item.IsVisible = !item.IsVisible;
        }


        #endregion

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
            var id = 0;

            if (int.TryParse(obj.ToString(), out id))
            {

                await Task.Run(() => onCollectionChanged?.Invoke(id));
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
                var created = await _folderService.AddAsync(new Folder() { Name = createWind.Name, UserId = _currentUser.Id });

                if (created != null)
                {
                    this.Folders.Add(new ExplorerItem() { Folder = created });
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

                folders = new ObservableCollection<ExplorerItem>();
                var all = await _folderService.GetAllFoldersAsync();
                foreach (var item in all)
                    folders.Add(new ExplorerItem() { Folder = item });
            }
        }
        #endregion

        #region AddCollection
        public ICommand addCollectionCommand;

        public ICommand AddCollectionCommand => addCollectionCommand ??= new AsyncRelayCommand(AddCollection);

        private async Task AddCollection(object obj)
        {
            var id = int.Parse(obj.ToString());
            var createWind = new CreateCollectionWindow();

            createWind.ShowDialog();

            if (createWind.IsOk)
            {
                var created = await _folderService.AddAsync(new Collection() { Name = createWind.Name, FolderId = id });

                if (created != null)
                {
                    folders.FirstOrDefault(x => x.Folder.Id == id)?.Folder.Collections.Add(created);
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

                folders.FirstOrDefault(x => x.Folder.Id == folderId).Folder.Collections = new ObservableCollection<Collection>(collecitons);

                OnPropertyChanged("Folders");
            }
        }
        #endregion
    }
}


