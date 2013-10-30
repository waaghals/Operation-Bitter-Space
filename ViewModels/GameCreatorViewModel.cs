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
        private int storedHeight;
        public int StoredHeight
        {
            get
            {
                return storedHeight;
            }
            set
            {
                SetProperty(ref storedHeight, value);
            }
        }

        private int storedWidth;
        public int StoredWidth
        {
            get
            {
                return storedWidth;
            }
            set
            {
                SetProperty(ref storedWidth, value);
            }
        }

        private int holes;
        public int Holes
        {
            get
            {
                return holes;
            }
            set
            {
                SetProperty(ref holes, value);
            }
        }

        private int density;
        public int Density
        {
            get
            {
                return density;
            }
            set
            {
                SetProperty(ref density, value);
            }
        }
        public ICommand GenerateGameCommand { get; set; }

        public GameCreatorViewModel(MainViewModel ViewModel)
        {
            Players = new ObservableCollection<Player>();
            GenerateGameCommand = new RandomGameCommand(ViewModel, this);
        }
    }
}
