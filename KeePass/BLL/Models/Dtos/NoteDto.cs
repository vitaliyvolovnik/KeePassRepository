using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Dtos
{
    public class NoteDto : INotifyPropertyChanged
    {
        public int Id { get; set; }



        private string? name;
        public string? Name 
        {
            get { return name; }
            set 
            { 
                name = value;
                IsChanged = true;
                OnPropertyChanged(nameof(Name));
            }
        }

        private SecurePassword? securePassword;
        public SecurePassword? SecurePassword
        {
            get { return securePassword; }
            set
            {
                securePassword = value;
                OnPropertyChanged(nameof(SecurePassword));
            }

        }

        public int? CollectionId { get; set; }

        private bool isChanged;
        public bool IsChanged
        {
            get { return isChanged; }
            set
            {
                if (isChanged != value)
                {
                    isChanged = value;
                    OnPropertyChanged(nameof(IsChanged));
                }
            }
        }


        private bool isPasswordChangingEnable;
        public bool IsPasswordChangingEnable
        {
            get { return isPasswordChangingEnable; }
            set 
            {
                isPasswordChangingEnable = value;
                OnPropertyChanged(nameof(IsPasswordChangingEnable));
                OnPropertyChanged(nameof(IsChangingButtonEnable));
            }
        }
        public bool IsChangingButtonEnable
        {
            get { return !isPasswordChangingEnable; }
        }



        public Note ToEntity()
        {
            return new Note
            {
                Id = Id,
                Name = Name,
                CollectionId = CollectionId,
                Password = SecurePassword?.PasswordHash
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
