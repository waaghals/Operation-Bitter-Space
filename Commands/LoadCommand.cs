using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Hexxagon.Commands
{
    class LoadCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private GameViewModel ViewModel;

        public LoadCommand(GameViewModel ViewModel)
        {
            this.ViewModel = ViewModel;
        }

        public bool CanExecute(object parameter)
        {
            // return ViewModel.CurrentPlayer != null;
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                ViewModel.Map.Clear();
                using (StreamReader sr = new StreamReader("SaveFile.txt"))
                {
                    if (sr.ReadLine() != "HexGrid")
                        return;

                    String[] newPlayer1 = sr.ReadLine().Split(' ');
                    String[] newPlayer2 = sr.ReadLine().Split(' ');

                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        String[] cell = line.Split(',');

                        int i = Convert.ToInt16(cell[0]);
                        int j = Convert.ToInt16(cell[1]);

                        Cell hexCell;

                        if (cell[2]=="1")
                        {
                            hexCell = new AvailableCell()
                            {
                                Owner = new Player()
                                {
                                    Name = newPlayer1[0],
                                    Hue = Convert.ToInt16(newPlayer1[1])
                                }
                            }; 
                        }
                        else if (cell[2] == "2")
                        {
                            hexCell = new AvailableCell()
                            {
                                Owner = new Player()
                                {
                                    Name = newPlayer2[0],
                                    Hue = Convert.ToInt16(newPlayer2[1])
                                }
                            }; 
                        }
                        else if (cell[2] == "3")
                        {
                            hexCell = new AvailableCell();
                        }
                        else
                        {
                            hexCell = new UnavailableCell();
                        }

                        ViewModel.Map.Add(i, j, new CellViewModel(hexCell, ViewModel));
                    }

                    ViewModel.Map.MapHexagons();
                    ViewModel.AddPlayersFromMap();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }
    }
}
