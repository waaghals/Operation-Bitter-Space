using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.Windows.Input;
namespace Hexxagon.Commands
{
    internal class SelectCommand : ICommand
    {
        #region Fields
        private Cell hex;
        private AvailableCell availableHex;
        private GameViewModel game;
        #endregion
        
        public SelectCommand(Cell h, GameViewModel g)
        {
            hex = h;
            availableHex = h as AvailableCell;
            game = g;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (game.SelectedCell == null || game.SelectedCell.Equals(hex))
            {
                Player player = game.CurrentPlayer;
                return hex.OwnedBy(player);
            }

            return hex.Clonable || hex.Jumpable;
        }

        public void Execute(object parameter)
        {
            if (hex.OwnedBy(game.CurrentPlayer)) //Clicked on player owned cell, highlight possible actions
            {
                if (game.SelectedCell == availableHex) //Deselected previous selection
                {
                    DeselectAction();
                }
                else
                {
                    SelectAction();
                }
            }
            else if (availableHex.Clonable) //Clone target clicked, clone the hex
            {
                CloneAction();
            }
            else if (availableHex.Jumpable) //Jump target clicked, jump to the new location
            {
                JumpAction();
            }
        }

        #region Actions
        private void JumpAction()
        {
            game.SelectedCell.JumpTo(availableHex);
            availableHex.TakeOverNeighbours();
            DeselectAction();
            game.DoTurn();
        }

        private void CloneAction()
        {
            game.SelectedCell.CloneTo(availableHex);
            availableHex.TakeOverNeighbours();
            DeselectAction();
            game.DoTurn();
        }

        private void SelectAction()
        {
            availableHex.ShowNeighbours();
            game.SelectedCell = availableHex;
        }

        private void DeselectAction()
        {
            game.SelectedCell.HideNeighbours();
            game.SelectedCell = null;
        }
        #endregion
    }
}