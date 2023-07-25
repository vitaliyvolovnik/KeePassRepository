using BLL.Services;
using Domain.Models;
using KeePass.Core;
using KeePass.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace KeePass.ViewModel
{

    
    public class ExplorerViewModel:BaseViewModel
    {

        private readonly FolderSerivce _folderService;
        private readonly User _currentUser;

        public ExplorerViewModel(FolderSerivce folderService,User currentUser)
        {
            _folderService = folderService;
            _currentUser = currentUser;
        }

        private List<Tuple<Folder,bool>> folders;
        public List<Tuple<Folder,bool>> Folders
        {
            get { return folders; }
            set { folders = value; OnPropertyChanged("folders"); }
        }



        #region ChangeCollection
        private Collection currentCollection;

        public delegate void SellectCollection(int collectionId);
        private event SellectCollection onCollectionChanged;

        public event SellectCollection OnCollectionChanged
        {
            add { onCollectionChanged += value ; }
            remove { onCollectionChanged -= value ; }
        }

        private ICommand collectionChangeCommand;

        public ICommand CollectionChangeCommand
        {
            get { return collectionChangeCommand ??=new AsyncRelayCommand(changeCollectionAsync); }
        }

        async Task changeCollectionAsync(object obj)
        {
            if(obj is Button button)
            {
                var id = int.Parse(button.Tag.ToString());


                await Task.Run(() => onCollectionChanged?.Invoke(id));
            }
        }

        #endregion

        #region AddFolder

        public ICommand addFolderCommand;

        public ICommand AddFolderCommand => addFolderCommand ??= new AsyncRelayCommand(AddFolder);

        private async Task AddFolder(object obj)
        {
            var createWind = new CreateFolderWindow();

            var res = createWind.ShowDialog();

            if (res == true)
            {
                var created = await _folderService.AddAsync(new Folder() { Name = createWind.Name,UserId = _currentUser.Id});

                if(created != null)
                {
                    this.folders.Add(new Tuple<Folder, bool>(created,false));
                    OnPropertyChanged("folders");
                }
                else
                {
                    throw new NotImplementedException();
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


                folders = new List<Tuple<Folder, bool>>();
                var all = await _folderService.GetAllFoldersAsync();
                foreach (var item in all)
                    folders.Add(new Tuple<Folder, bool>(item, false));

                OnPropertyChanged("folders");
            }
        }
        #endregion

        #region AddCollection
        public ICommand addCollectionCommand;

        public ICommand AddCollectionCommand => addCollectionCommand ??= new AsyncRelayCommand(AddCollection);

        private async Task AddCollection(object obj)
        {
            if (obj is Control control)
            {
                var id = int.Parse(control.Tag.ToString());
                var createWind = new CreateFolderWindow();

                var res = createWind.ShowDialog();

                if (res == true)
                {
                    var created = await _folderService.AddAsync(new Collection() { Name = createWind.Name, FolderId = id });

                    if (created != null)
                    {
                        folders.FirstOrDefault(x=>x.Item1.Id == id)?.Item1.Collections.Add(created);
                        OnPropertyChanged("folders");
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }
            
        }

        #endregion

        #region
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

                folders.FirstOrDefault(x => x.Item1.Id == folderId).Item1.Collections = collecitons.ToList();

                OnPropertyChanged("folders");
            }
        }
        #endregion
    }
}


