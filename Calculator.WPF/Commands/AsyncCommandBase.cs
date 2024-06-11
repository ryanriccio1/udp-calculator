using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.WPF.Commands
{
    public abstract class AsyncCommandBase : ICommand
    {
        // create a base for async commands that we can subscribe to
        private bool _isExecuting;
        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
            set
            {
                _isExecuting = value;
                // let others subscribed to us know that they're allowed to execute now
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {   // prevent the command from executing twice
            return !IsExecuting;
        }

        public async void Execute(object parameter)
        {   // provide the ICommand execute method that
            // just calls the async version
            IsExecuting = true;

            await ExecuteAsync(parameter);

            IsExecuting = false;
        }

        public abstract Task ExecuteAsync(object parameter);
    }
}
