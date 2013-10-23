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
            //For testing purpeses now, a browse and load function needs to be implemented here
            Game.Map.Clear();
            Random ran = new Random();

            Hexagon hexCell;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 3 && j == 5)
                        continue;

                    if (i == 7 && j == 2)
                        continue;

                    switch (ran.Next(3))
                    {
                        case 1:
                            hexCell = new PlayerHexagon()
                            {
                                Owner = new Player()
                                {
                                    Name = "Yorick",
                                    Hue = 200
                                }
                            };
                            break;
                        case 2:
                            hexCell = new PlayerHexagon()
                            {
                                Owner = new Player()
                                {
                                    Name = "Patrick",
                                    Hue = 150
                                }
                            };
                            break;

                        default:
                            hexCell = new OpenHexagon();
                            break;
                    }
                    hexCell.Name += "X:" + i + " Y:" + j;
                    Game.Map.Add(i, j, new CellViewModel(hexCell, Game));
                }
            }
            Game.Map.MapHexagons();
            Game.AddPlayersFromMap();
        }
    }
}