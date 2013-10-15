using Hexxagon.Helpers;
using Hexxagon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Hexxagon
{
    public class Cell
    {
        public Hexagon Hex { get; set; }
        public Brush Gradient { get; set; }

        public Cell(Hexagon h)
        {
            Hex = h;
            if (Hex.Available())
            {
                Gradient = CellGradient.FromHue(0, 0.0);
            }
            else if (Hex.IsOwned())
            {
                PlayerHexagon playerHex = (PlayerHexagon)Hex;
                Gradient = CellGradient.FromHue(playerHex.Owner.Hue, 0.5);
            }
            else
            {
                Gradient = CellGradient.FromHue(0, 0.05);
            }
        }
    }
}
