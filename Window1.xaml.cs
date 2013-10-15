using Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HexagonGridTest
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            short hue = 1;
            HexButton hex;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    hex = new HexButton();
                    grid.Children.Add(hex);
                    HexagonGrid.SetRow(hex, i);
                    HexagonGrid.SetColumn(hex, j);
                    hex.DataContext = new Tile(hue);
                    hue += 3;
                }
            }
        }
    }
}
