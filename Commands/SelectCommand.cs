using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.Windows.Input;
namespace Hexxagon.Commands
{
    internal class SelectCommand : ICommand
    {
        private Hexagon hex;
        private GameViewModel game;

        public SelectCommand(Hexagon h, GameViewModel g)
        {
            hex = h;
            game = g;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            Player player = game.CurrentPlayer;

            return hex.OwnedBy(player);
        }

        public void Execute(object parameter) {
           
        }
    }
}