using BLL.Models.Dtos;
using BLL.Services;
using Domain.Models;
using KeePass.Core;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KeePass.ViewModel
{
    public class CollectionViewModel : BaseViewModel
    {
        private readonly FolderSerivce _folderSerivce;
        private readonly NoteService _noteService;

        public CollectionViewModel(FolderSerivce folderSerivce, NoteService noteService)
        {
            _folderSerivce = folderSerivce;
            _noteService = noteService;
        }
        private CollectionDto? currentCollection;


        public CollectionDto? CurrentCollection
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

        #region SaveNote
        private ICommand saveCommand;
        public ICommand SaveCommand => saveCommand ??= new AsyncRelayCommand(SaveExecute, SaveCanExecute);

        private bool SaveCanExecute(object arg)
        {
            return (arg as NoteDto)?.IsChanged ?? false;
        }

        private async Task SaveExecute(object arg)
        {
            var noteDto = (NoteDto)arg;
            noteDto.IsPasswordChangingEnable = false;
            noteDto.IsChanged = false;
            if (noteDto.Id == 0)
            {
                var created = await _noteService.AddAsync(noteDto);
                if (created != null)
                {
                    noteDto.Id = created.Id;
                }
            }
            else
            {
                var updated = await _noteService.UpdateAsync(noteDto, noteDto.Id);
                if (updated != null)
                {
                    noteDto.Name = updated.Name;
                    noteDto.SecurePassword = updated.SecurePassword;
                }
            }
        }
        #endregion

        #region EnablePasswordChanging

        private ICommand enablePasswordChangingCommand;
        public ICommand EnablePasswordChangingCommand => enablePasswordChangingCommand ??= new RelayCommand(EnablePasswordChangingExecute);

        private void EnablePasswordChangingExecute(object obj)
        {
            var noteDto = obj as NoteDto;
            if (noteDto != null)
            {
                noteDto.SecurePassword.Password = "";
                noteDto.IsPasswordChangingEnable = true;
            }
        }


        #endregion

        #region CopyToClipBoard

        private ICommand copyToClipBoardCommand;
        public ICommand CopyToClipBoardCommand => copyToClipBoardCommand ??= new RelayCommand(CopyToClipBoardExecute);


        private void CopyToClipBoardExecute(object obj)
        {
            var noteDto = obj as NoteDto;

            if(noteDto != null)
            {
                Clipboard.SetText(noteDto.SecurePassword.Password);
            }
        }


        #endregion

        #region DiscardChanges

        private ICommand discardChangesCommand;
        public ICommand DiscardChangesCommand => discardChangesCommand ??= new AsyncRelayCommand(DiscardChangesExecute, DiscardChangesCanExecute);

        private bool DiscardChangesCanExecute(object arg)
        {
            var noteDto = arg as NoteDto;
            if (noteDto != null && noteDto.IsChanged) 
                return false;
            return true;
        }

        private async Task DiscardChangesExecute(object arg)
        {
            var noteDto = arg as NoteDto;
            if(noteDto != null)
            {
                var clearNote = await _noteService.GetNoteAsync(noteDto.Id);

                if(clearNote != null)
                {
                    noteDto.SecurePassword = clearNote.SecurePassword;
                    noteDto.Name = clearNote.Name;
                    noteDto.IsChanged = false;
                    noteDto.IsPasswordChangingEnable = false;
                }
            }
        }


        #endregion
    }
}
