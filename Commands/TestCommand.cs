using Hexxagon.Controls;
using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
namespace Hexxagon.Commands
{
    internal class TestCommand : ICommand
    {
        public GameViewModel ViewModel { get; set; }

        public TestCommand(GameViewModel vm)
        {
            ViewModel = vm;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
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
            ClosedHexagon closedHex = new ClosedHexagon();
            Hexagon[] hexes = new Hexagon[] { playerHex, opponentHex, openHex, closedHex };
            Random ran = new Random();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Hexagon hexCell = hexes[ran.Next(hexes.Length)];
                    ViewModel.Map.Add(i, j, new CellViewModel(hexCell));
                }
            }
        }
    }
}