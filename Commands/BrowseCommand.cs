using Hexxagon.Controls;
using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace Hexxagon.Commands
{
    internal class BrowseCommand : ICommand
    {
        public GameViewModel Game { get; set; }

        public BrowseCommand(GameViewModel vm)
        {
            Game = vm;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Game.Map.Clear();
            PlayerHexagon playerHex = new PlayerHexagon()
            {
                Owner = new Player()
                {
                    Name = "Patrick",
                    Hue = 150
                }
            };
            PlayerHexagon opponentHex = new PlayerHexagon()
            {
                Owner = new Player()
                {
                    Name = "Yorick",
                    Hue = 200
                }
            };
            OpenHexagon openHex = new OpenHexagon();
            Hexagon[] hexes = new Hexagon[] { playerHex, opponentHex, openHex };
            Random ran = new Random();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Hexagon hexCell = hexes[ran.Next(hexes.Length)];
                    Game.Map.Add(i, j, new CellViewModel(hexCell, Game));
                }
            }
            Game.Map.MapHexagons();
            Game.AddPlayersFromMap();
        }
    }
}