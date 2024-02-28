

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Converters;

namespace ALauncher.ViewModel;
    public class CommandWrapper {
    private readonly List<RelayCommand> _commands; 

    public CommandWrapper() {
        _commands = new List<RelayCommand>();
    }

    public ICommand GetCommand(string Name, Action<object?> executeMethod, Func<object?, bool> canExecuteMethod = null) {
        var command = _commands.FirstOrDefault(cachedCommand => cachedCommand.Name == Name);

        if (command != null) {
            return command;
        }

        command = new RelayCommand(executeMethod, canExecuteMethod);
        _commands.Add(command);
        return command;
    }
}