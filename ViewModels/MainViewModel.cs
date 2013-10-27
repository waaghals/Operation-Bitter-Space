using Hexxagon.Commands;
using Hexxagon.Controls;
using Hexxagon.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Hexxagon.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand BrowseCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand NewRandomGame { get; set; }

        private BaseViewModel mainContent;
        public BaseViewModel MainContent
        {
            get
            {
                return mainContent;
            }
            set
            {
                SetProperty(ref mainContent, value);
            }
        }

        public MainViewModel()
        {
            MainContent = new GameViewModel();
            InitCommands();
        }

        private void InitCommands()
        {
            SaveCommand = new SaveCommand((GameViewModel)MainContent);
            LoadCommand = new LoadCommand((GameViewModel)MainContent);
            CloseCommand = new CloseCommand();
            NewRandomGame = new OpenGameCreatorCommand(this);
        }
    }
}
