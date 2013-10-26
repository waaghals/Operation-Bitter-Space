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
        public ICommand TestCommand { get; set; }
        public GameViewModel ViewModel { get; private set; }

        public MainViewModel()
        {
            ViewModel = new GameViewModel();
            InitCommands();
        }

        private void InitCommands()
        {
            SaveCommand = new SaveCommand(ViewModel);
            CloseCommand = new CloseCommand();
            BrowseCommand = new BrowseCommand(ViewModel);
            TestCommand = new TestCommand(ViewModel);
        }
    }
}
