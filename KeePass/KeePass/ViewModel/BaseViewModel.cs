using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeePass.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        protected BaseViewModel()
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        public void Dispose()
        {
            this.OnDispose();
        }
        public virtual void OnDispose()
        {

        }
    }
}
