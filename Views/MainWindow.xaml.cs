using Hexxagon.Controls;
using Hexxagon.Models;
using Hexxagon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hexxagon
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GridWindow : Window
    {

        public GridWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();

        }
    }
}
