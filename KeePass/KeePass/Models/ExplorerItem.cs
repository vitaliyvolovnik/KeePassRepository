using BLL.Models.Dtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeePass.Models
{
    public class ExplorerItem : INotifyPropertyChanged
    {
        private bool isVisible = false;
        public FolderDto Folder { get; set; }

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    OnPropertyChanged(nameof(IsVisible));
                    OnPropertyChanged(nameof(IsVisibleType));
                }
            }
        }

        public string IsVisibleType
        {
            get { return IsVisible ? "Visible" : "Collapsed"; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
