using Hexxagon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Hexxagon.Models
{
    public abstract class Hexagon
    {
        public Point Location { get; set; }
        public Dictionary<Direction, Hexagon> Neighbours { get; set; }

        public abstract bool Available();
        public abstract bool IsOwned();
    }
}
