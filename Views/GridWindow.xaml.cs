using Hexxagon.Controls;
using Hexxagon.Models;
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

            PlayerHexagon playerHex = new PlayerHexagon()
            {
                Owner = new Player()
                {
                    Name = "Patrick",
                    Hue = 150                    
                }
            };
            PlayerHexagon opponentHex = new PlayerHexagon()
            {
                Owner = new Player()
                {
                    Name = "Yorick",
                    Hue = 200                    
                }
            };
            OpenHexagon openHex = new OpenHexagon();
            ClosedHexagon closedHex = new ClosedHexagon();
            Hexagon[] hexes = new Hexagon[] {playerHex, opponentHex, openHex, closedHex};
            Random ran = new Random();

            //short hue = 1;
            HexButton hex;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    hex = new HexButton();
                    grid.Children.Add(hex);
                    HexagonGrid.SetRow(hex, i);
                    HexagonGrid.SetColumn(hex, j);


                    Hexagon hexCell = hexes[ran.Next(hexes.Length)];
                    hex.DataContext = new Cell(hexCell);
                    //hue += 3;
                }
            }
        }
    }
}
