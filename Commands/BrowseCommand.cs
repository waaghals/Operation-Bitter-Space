using Hexxagon.Controls;
using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace Hexxagon.Commands
{

    internal class BrowseCommand : ICommand
    {
        public MainViewModel ViewModel { get; set; }




        public BrowseCommand(MainViewModel vm)
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
            HexagonGrid grid = new HexagonGrid() { HexagonSideLength = 20, Rows = 20, Columns = 20 };
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

            short hue = 1;

            

            HexButton hex;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    hex = new HexButton();
                    grid.Children.Add(hex);
                    HexagonGrid.SetRow(hex, i);
                    HexagonGrid.SetColumn(hex, j);


                    Hexagon hexCell = hexes[ran.Next(hexes.Length)];
                    hex.DataContext = new Cell(hexCell);
                    hue += 3;
                }
            }

            ViewModel.Content.Clear();
            ViewModel.Content.Add(grid);
        }
    }
}