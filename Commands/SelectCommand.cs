using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.Windows.Input;
namespace Hexxagon.Commands
{
    internal class SelectCommand : BaseCommand
    {
        private Cell hex;
        private GameViewModel game;

        public SelectCommand(Cell h, GameViewModel g)
        {
            hex = h;
            game = g;
        }
        
        public bool CanExecute(object parameter)
        {
            if (game.SelectedCell == null || game.SelectedCell.Equals(hex))
            {
                Player player = game.CurrentPlayer;
                return hex.OwnedBy(player);
            }

            return hex.Clonable || hex.Targetable;
        }

        public void Execute(object parameter)
        {
            AvailableCell availableHex = hex as AvailableCell;
            if (hex.OwnedBy(game.CurrentPlayer))
            {
                availableHex.HighLightAll(game);
            }
            else if (hex.GetType() == typeof(UnavailableCell))
            {
                return;
            }
            else if (availableHex.Clonable)
            {
                availableHex.Clone(game.CurrentPlayer);
                availableHex.TakeOverNeighbours();
                ((AvailableCell)game.SelectedCell).HighLightAll(game);
                game.DoTurn();
            }
            else if (availableHex.Targetable)
            {
                ((AvailableCell)game.SelectedCell).Jump();
                availableHex.Clone(game.CurrentPlayer);
                availableHex.TakeOverNeighbours();
                ((AvailableCell)game.SelectedCell).HighLightAll(game);
                game.DoTurn();
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