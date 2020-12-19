using System;
using System.Windows.Input;

namespace Shipwreck.MusicPlayer.ViewModels
{
    internal sealed class ParameteredCommand : ICommand
    {
        private readonly Action<object> _ExecutionHandler;

        public ParameteredCommand(Action<object> executionHandler)
        {
            _ExecutionHandler = executionHandler;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { }
            remove { }
        }

        bool ICommand.CanExecute(object parameter)
            => true;

        public void Execute(object parameter)
            => _ExecutionHandler(parameter);
    }
}