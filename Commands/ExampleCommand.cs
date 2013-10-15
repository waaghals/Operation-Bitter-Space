using System;
using System.Windows.Input;
namespace Hexxagon.Commands
{

    internal class ExampleCommand : ICommand
    {
        private int viewModel;

        /// <summary>
        /// Initializes a new instance of the UpdateLevelCommand class.
        /// </summary>
        public ExampleCommand(int viewModel) {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
        }
    }
}