using System;
using System.Windows.Input;
namespace Hexxagon.Commands
{

    internal class ClickCommand : ICommand
    {
        private Cell cell;

        public ClickCommand(Cell c) {
            cell = c;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return cell.Hex.Available();
        }

        public void Execute(object parameter) {
            //Doe iets
        }
    }
}