
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Collections;

namespace ALauncher.ViewModel;
    public sealed class CommandWrapper {
    private readonly HashSet<RelayCommand> _commands; 

    public CommandWrapper() {
        _commands = new HashSet<RelayCommand>();
    }

    public ICommand GetCommand(Action<object?> execute, Func<object?, bool> canExecute = null) {
        var command = _commands.FirstOrDefault(cachedCommand => cachedCommand.execute == execute);

        if (command != null) {
            return command;
        }
        command = new RelayCommand(execute, canExecute);
        _commands.Add(command);
        return command;
    }
}