using Hexxagon.Commands;
using Hexxagon.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Hexxagon.ViewModels
{
    public class GameCreatorViewModel : BaseViewModel
    {
        private MainViewModel MainWindow;

        public ObservableCollection<Player> Players { get; set; }
        public ICommand GenerateGameCommand { get; set; }

        public GameCreatorViewModel(MainViewModel ViewModel)
        {
            Players = new ObservableCollection<Player>();
            GenerateGameCommand = new RandomGameCommand(ViewModel, Players);
        }
    }
}
