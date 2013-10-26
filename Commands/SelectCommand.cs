using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.Windows.Input;
namespace Hexxagon.Commands
{
    internal class SelectCommand : ICommand
    {
        private Cell hex;
        private GameViewModel game;

        public SelectCommand(Cell h, GameViewModel g)
        {
            hex = h;
            game = g;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            //Player player = game.CurrentPlayer;
            //
            //return hex.OwnedBy(player);
            return true;
        }

        public void Execute(object parameter)
        {
            if (hex.OwnedBy(game.CurrentPlayer))
            {
                ((AvailableCell)hex).HighLightAll(game);
            }
            else if (hex.GetType() == typeof(UnavailableCell))
            {
                return;
            }
            else if (((AvailableCell)hex).Clonable)
            {
                ((AvailableCell)hex).Clone(game.CurrentPlayer);
                ((AvailableCell)hex).TakeOverAll();
                ((AvailableCell)game.SelectedCell).HighLightAll(game);
            }
            else if (((AvailableCell)hex).Targetable)
            {
                ((AvailableCell)game.SelectedCell).Jump();
                ((AvailableCell)hex).Clone(game.CurrentPlayer);
                ((AvailableCell)hex).TakeOverAll();
                ((AvailableCell)game.SelectedCell).HighLightAll(game);
            }

        }

        private void HighlightNeighbour(Cell source, Cell dest, Distance distance)
        {
            if (dest.Available())
            {
                dest.HighlightFrom(source, distance);
            }
        }
    }
}