using BLL.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeePass.ViewModel
{
    public class CollectionViewModel:BaseViewModel
    {
        private readonly FolderSerivce _folderSerivce;
        private readonly NoteService _noteService;

        public CollectionViewModel(FolderSerivce folderSerivce, NoteService noteService)
        {
            _folderSerivce = folderSerivce;
            _noteService = noteService;
        }
        private Collection? currentCollection;


        public Collection? CurrentCollection
        {
            get { return currentCollection; }
        }


        #region SetCollection
            
        public async void ChangeCollection(int collecionId)
        {
            var collecion = await _folderSerivce.GetCollectionAsync(collecionId);

            currentCollection = collecion;
            OnPropertyChanged("CurrentCollection");
        }


        #endregion

        #region AddNote
            
        #endregion
    }
}
