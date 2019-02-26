using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lab3
{
    class DelegateCommand : ICommand
    {
        Action _action = null;
        bool _canExecute = true;

        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecuteProperty
        {
            get
            {
                return _canExecute;
            }
            set
            {
                _canExecute = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged;
    }


    class DelegateCommandWithArgs<T> : ICommand
    {
        Action<T> _action = null;
        bool _canExecute = true;

        public DelegateCommandWithArgs(Action<T> action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _action((T)parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
