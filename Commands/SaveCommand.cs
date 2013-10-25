using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;
namespace Hexxagon.Commands
{
    class SaveCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private GameViewModel ViewModel;

        public SaveCommand(GameViewModel ViewModel)
        {
            this.ViewModel = ViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return ViewModel.HasStarted;
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("Saving");
        }
    }
}
