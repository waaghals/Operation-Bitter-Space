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
            int density = gm.Density;

            if (width < 6)
                width = 6;
            if (height < 6)
                height = 6;
            if (density < 30)
                density = 30;

            int maxStart = Convert.ToInt32((height * width * ((double)density/100)) / gm.Players.Count);
            Console.WriteLine(height);
            Console.WriteLine(width);
            Console.WriteLine(gm.Players.Count);
            Console.WriteLine(density);
            Console.WriteLine(maxStart);
            if (maxStart < 2)
                maxStart = 2;


            Dictionary<Vector, AvailableCell> cells = new Dictionary<Vector, AvailableCell>();

            foreach (Player player in gm.Players)
                for (int i = 0; i < maxStart; i++)
                {
                    Vector vector = new Vector(ran.Next(width), ran.Next(height));
                    while (cells.ContainsKey(vector))
                    {
                        vector = new Vector(ran.Next(width), ran.Next(height));
                    }
                    cells.Add(vector, new AvailableCell() { Owner = player });
                }

            Cell hexCell;
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    if (cells.ContainsKey(new Vector(i, j)))
                        hexCell = cells[new Vector(i, j)];
                    else
                        hexCell = new AvailableCell();
                    Game.Map.Add(i, j, new CellViewModel(hexCell, Game));
                }

            Game.Map.MapHexagons();
            Game.AddPlayersFromMap();


            mainWindow.MainContent = Game;
            Console.WriteLine("Random Game Created");
        }
    }
}