using Hexxagon.Common;
using Hexxagon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Hexxagon.Models
{
    public abstract class Hexagon : BaseNotifier
    {
        public Dictionary<Neighbour, Hexagon> Neighbours { get; set; }
        public Player Owner { get; set; }
        public string Name { get; set; }

        public abstract bool Available();
        public abstract bool OwnedBy(Player p);
        public abstract bool Clickable();


        public Hexagon()
        {
            Neighbours = new Dictionary<Neighbour, Hexagon>();
        }

        //These methodes have been added so that code foot print stays readable
        public bool Has(Neighbour n)
        {
            return Neighbours.ContainsKey(n);
        }

        public Hexagon Get(Neighbour n)
        {
            if (Has(n))
            {
                return Neighbours[n];
            }
            return null;
        }

        public void Set(Neighbour n, Hexagon h)
        {
            if (!Has(n))
            {
                Neighbours.Add(n, h);
                //throw new ArgumentException("Hexagon already contains a Neighbour in location: " + n);
            }
            //Neighbours.Add(n, h);
        }
    }

    public enum Distance
    {
        Far,
        Close,
        OutOfReach
    }
}
