using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Hexxagon.Commands
{
    class OpenGameCreatorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private MainViewModel ViewModel;

        public OpenGameCreatorCommand(MainViewModel vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.MainContent = new GameCreatorViewModel(ViewModel);
        }
    }
}
