using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;
namespace Hexxagon.Commands
{

    internal class CloseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            Application.Current.Shutdown();
        }
    }
}