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
            Player player = game.CurrentPlayer;

            return hex.OwnedBy(player);
        }

        public void Execute(object parameter)
        {
            if (game.SelectedCell != null)
            foreach (Cell neighbour in game.SelectedCell.Neighbours.Values)
            {
                foreach (Cell farNeighbour in neighbour.Neighbours.Values)
                {
                    //Highlight far neighbour
                    HighlightNeighbour(hex, farNeighbour, Distance.OutOfReach);
                }
            }

            game.SelectedCell = hex;

            foreach (Cell neighbour in hex.Neighbours.Values)
            {
                foreach (Cell farNeighbour in neighbour.Neighbours.Values)
                {
                    //Highlight far neighbour
                    HighlightNeighbour(hex, farNeighbour, Distance.Far);
                }
            }

            foreach (Cell neighbour in hex.Neighbours.Values)
            {
                //Highlight close neighbour
                HighlightNeighbour(hex, neighbour, Distance.Close);
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