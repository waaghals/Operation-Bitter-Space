using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
namespace Hexxagon.Commands
{
    class SaveCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private GameViewModel ViewModel;

        public SaveCommand(GameViewModel ViewModel)
        {
            this.ViewModel = ViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (ViewModel.CurrentPlayer != null)
            {
                try
                {
                    using (StreamWriter file = new StreamWriter("SaveFile.txt", true))
                    {
                        File.WriteAllText("SaveFile.txt", String.Empty);

                        file.WriteLine("HexGrid");
                        foreach (Player player in ViewModel.Players)
                        {
                            file.WriteLine(player.Name + " " + player.Hue);
                        }

                        foreach (var cell in ViewModel.Map.Values)
                        {
                            if (cell.Hex.GetType() == typeof(AvailableCell))
                            {
                                //((AvailableCell)cell.Hex).Owner
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be found:");
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}
