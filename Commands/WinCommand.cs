using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace Hexxagon.Commands
{
    class WinCommand: ICommand
    {
        public event EventHandler CanExecuteChanged;
        private string text;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MessageBox.Show((string)parameter);
        }
    }
}
