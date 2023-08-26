using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KeePass.Core
{
    public class RelayCommand:ICommand
    {

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            this._execute = execute;
            this._canExecute = canExecute ?? (o => true);
        }
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;


        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return this._canExecute.Invoke(parameter);
        }

        public void Execute(object? parameter)
        {
            this._execute.Invoke(parameter);
        }
    }
}
