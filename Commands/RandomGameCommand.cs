using Hexxagon.Controls;
using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace Hexxagon.Commands
{
    internal class RandomGameCommand : ICommand
    {
        public GameViewModel Game { get; set; }
        private MainViewModel mainWindow;
        private ObservableCollection<Player> players;

        public RandomGameCommand(MainViewModel mainWindow, ObservableCollection<Player> players)
        {
            this.mainWindow = mainWindow;
            this.players = players;
            Game = new GameViewModel();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return players.Count >= 2;
        }

        public void Execute(object parameter)
        {
            Game.Map.Clear();
            Random ran = new Random();

            Cell hexCell;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    switch (ran.Next(4))
                    {
                        case 1:
                            hexCell = new AvailableCell()
                            {
                                Owner = new Player()
                                {
                                    Name = "Yorick",
                                    Hue = 110
                                }
                            };
                            break;
                        case 2:
                            hexCell = new AvailableCell()
                            {
                                Owner = new Player()
                                {
                                    Name = "Patrick",
                                    Hue = 349
                                }
                            };
                            break;

                        default:
                            hexCell = new AvailableCell();
                            break;
                    }

                    if (i == 3 && j == 5)
                        hexCell = new UnavailableCell();

                    if (i == 7 && j == 2)
                        hexCell = new UnavailableCell();
                    // hexCell.Name += "X:" + i + " Y:" + j;
                        
                    Game.Map.Add(i, j, new CellViewModel(hexCell, Game));
                    
                }
            }

            Game.Map.MapHexagons();
            Game.AddPlayersFromMap();

            mainWindow.MainContent = Game;
            Console.WriteLine("Random Game Created");
        }
    }
}