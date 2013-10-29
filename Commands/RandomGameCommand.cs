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

        public RandomGameCommand(MainViewModel mainWindow, GameCreatorViewModel gm)
        {
            this.mainWindow = mainWindow;
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
            return gm.Players.Count >= 2;
        }

        public void Execute(object parameter)
        {
            Game.Map.Clear();
            Random ran = new Random();
            int width = gm.StoredWidth;
            int height = gm.StoredHeight;

            if (width < 4)
                width = 4;
            if (height < 4)
                height = 4;

            Cell hexCell;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int random = ran.Next(gm.Players.Count + 1);

                    if (gm.Players.Count > random)
                    {
                        hexCell = new AvailableCell() { Owner = gm.Players[random] };
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