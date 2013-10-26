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
           // return ViewModel.CurrentPlayer != null;
            return true;
        }

        public void Execute(object parameter)
        {
            if (ViewModel.CurrentPlayer != null)
            {

                Console.WriteLine("Saving");

            }
        }
    }
}
