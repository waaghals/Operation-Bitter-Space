using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.Windows.Input;
namespace Hexxagon.Commands
{

    internal class ClickCommand : ICommand
    {
        private CellViewModel cell;

        public ClickCommand(CellViewModel c) {
            cell = c;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return cell.Hex.Available();
        }

        public void Execute(object parameter) {
           
        }
    }
}