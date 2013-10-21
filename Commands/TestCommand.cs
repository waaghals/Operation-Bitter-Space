using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
namespace Hexxagon.Commands
{

    internal class TestCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        GameViewModel vm;
        public TestCommand(GameViewModel m)
        {
            vm = m;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
           
        }
    }
}