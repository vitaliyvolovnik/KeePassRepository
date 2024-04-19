using BLL.Services;
using Domain.Models;
using KeePass.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
            OnPropertyChanged(nameof(CurrentCollection));
        }


        #endregion


        #region AddNote

        private ICommand addNoteCommand;
        public ICommand AddNoteCommand => addNoteCommand ??= new RelayCommand(AddNoteExecute, AddNoteCanExecute);

        private bool AddNoteCanExecute(object arg)
        {
            return currentCollection != null;
        }

        private void AddNoteExecute(object obj)
        {
            CurrentCollection.Notes.Add(new());
        }

        #endregion

        #region RemoveNote

        private ICommand removeNoteCommand;
        public ICommand RemoveNoteCommand => addNoteCommand ??= new AsyncRelayCommand(RemoveNoteExecute);

        private async Task RemoveNoteExecute(object obj)
        {
            int noteId = (int)obj;
            await _noteService.DeleteNoteAsync(noteId);
            CurrentCollection.Notes.Remove(CurrentCollection.Notes.First(x => x.Id == noteId));
        }
        #endregion

        #region SaveData
        private ICommand saveCommand;
        public ICommand SaveCommand => saveCommand ??= new AsyncRelayCommand(SaveExecute);

        private async Task SaveExecute(object arg)
        {
            await _folderSerivce.SaveAsync();
        }
        #endregion
    }
}
