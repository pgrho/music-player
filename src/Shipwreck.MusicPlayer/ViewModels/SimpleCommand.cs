using System;
using System.Windows.Input;

namespace Shipwreck.MusicPlayer.ViewModels
{
    internal sealed class SimpleCommand : ICommand
    {
        private readonly Action _ExecutionHandler;

        public SimpleCommand(Action executionHandler)
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
            => _ExecutionHandler();
    }
}