using Hexxagon.Common;
using Hexxagon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Hexxagon.Models
{
    public abstract class Cell : BaseNotifier
    {
        public Dictionary<Neighbour, Cell> Neighbours { get; set; }
        public abstract bool Clonable { get; }
        public abstract bool Targetable { get; }
        public abstract bool Available();
        public abstract bool OwnedBy(Player p);
        public abstract bool Owned();
        public abstract void HighlightFrom(Cell from, Distance distance);

        public Cell()
        {
            Neighbours = new Dictionary<Neighbour, Cell>();
        }

        //These methodes have been added so that code foot print stays readable
        public bool Has(Neighbour n)
        {
            return Neighbours.ContainsKey(n);
        }

        public Cell Get(Neighbour n)
        {
            if (Has(n))
            {
                return Neighbours[n];
            }
            return null;
        }

        public void Set(Neighbour n, Cell h)
        {
            //if (!Has(n))
            //{
            //    throw new ArgumentException("Hexagon already contains a Neighbour in location: " + n);
            //}
            Neighbours.Add(n, h);
        }
    }

    public enum Distance
    {
        OutOfReach,
        Far,
        Close
    }
}
