using System;
using System.Windows.Input;

namespace GameWPF.ViewModel
{
    public class CommandDelegate : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }

        public CommandDelegate(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            if (this.execute != null)
                this.execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute != null ? this.canExecute(parameter) : true;
        }
    }

    public class CommandDelegate<T> : ICommand
    {
        private Action<T> execute;
        private Func<T, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }

        public CommandDelegate(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            if (this.execute != null)
                this.execute((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute != null ? this.canExecute((T)parameter) : true;
        }
    }
}
