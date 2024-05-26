
using System.Windows.Input;
using System;

namespace ALauncher.WPF.Common
{
    public sealed class RelayCommand : ICommand
    {
        public Action<object?> execute { get; private set; }
        public Func<object?, bool> canExecute { get; private set; }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object?> execute, Func<object?, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            this.execute(parameter);
        }

    }
}