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
        private GameCreatorViewModel gm;
        private ObservableCollection<Player> players;

        public RandomGameCommand(MainViewModel mainWindow, ObservableCollection<Player> players, GameCreatorViewModel gm)
        {
            this.mainWindow = mainWindow;
            this.players = players;
            this.gm = gm;
            Game = new GameViewModel();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return players.Count >= 0;
        }

        public void Execute(object parameter)
        {
            Game.Map.Clear();
            Random ran = new Random();

            Cell hexCell;
            for (int i = 0; i < gm.StoredWidth; i++)
            {
                for (int j = 0; j < gm.StoredHeight; j++)
                {
                    int random = ran.Next(gm.Players.Count + 1);

                    if (gm.Players.Count > random)
                    {
                        hexCell = new AvailableCell() { Owner = players[random] };
                    }
                    else
                    {
                        hexCell = new AvailableCell();
                    }

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