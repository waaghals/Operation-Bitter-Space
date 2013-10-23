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

        public bool CanExecute(object parameter)
        {
            Player player = game.CurrentPlayer;

            return hex.OwnedBy(player);
        }

        public void Execute(object parameter)
        {
            Console.WriteLine(hex.Name);
            foreach (Hexagon neighbour in hex.Neighbours.Values)
            {
                foreach (Hexagon farNeighbour in neighbour.Neighbours.Values)
                {
                    HighlightNeighbour(hex, farNeighbour, Distance.Far);
                    //Highlight far neighbour
                }

                //Highlight close neighbour
                HighlightNeighbour(hex, neighbour, Distance.Close);
            }
        }

        private void HighlightNeighbour(Hexagon source, Hexagon dest, Distance distance)
        {
            if (distance == Distance.Close)
            Console.WriteLine(distance + " " + dest.Name);
            if (dest.Available())
            {
                OpenHexagon openDest = dest as OpenHexagon;
                openDest.HighlightFrom(source, distance);
            }
        }
    }
}