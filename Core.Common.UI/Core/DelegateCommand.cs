using System;
using System.Windows.Input;

namespace Core.Common.UI.Core
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Predicate<T> _CanExecute;

        private readonly Action<T> _Execute;

        public DelegateCommand(Action<T> execute) : this(execute, null)
        {
        }

        public DelegateCommand(Action<T> execute, Predicate<T> canExecute) : this(execute, canExecute, "")
        {
        }

        public DelegateCommand(Action<T> execute, Predicate<T> canExecute, string label)
        {
            _Execute = execute;
            _CanExecute = canExecute;

            Label = label;
        }

        public string Label { get; set; }

        public void Execute(object parameter)
        {
            _Execute((T) parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _CanExecute == null ? true : _CanExecute((T) parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_CanExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_CanExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }
    }
}