using Hexxagon.Commands;
using Hexxagon.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Hexxagon.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand BrowseCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public ObservableCollection<FrameworkElement> Content { get; private set; }

        public MainViewModel()
        {
            Content = new ObservableCollection<FrameworkElement>();
            InitCommands();
        }

        private void InitCommands()
        {
            CloseCommand = new CloseCommand();
            BrowseCommand = new BrowseCommand(this);
        }
    }
}
