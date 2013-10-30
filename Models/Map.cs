using Hexxagon.Common;
using Hexxagon.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Hexxagon.Models
{
    public class Map : ObservableDictionary<Vector, CellViewModel>
    {
        public void MapHexagons()
        {
            Dictionary<Neighbour, Vector> oddNeighbours = new Dictionary<Neighbour, Vector>();
            Dictionary<Neighbour, Vector> evenNeighbours = new Dictionary<Neighbour, Vector>();
            Dictionary<Neighbour, Vector> neighbours = new Dictionary<Neighbour, Vector>();

            oddNeighbours.Add(Neighbour.North, new Vector(0, -1));
            oddNeighbours.Add(Neighbour.South, new Vector(0, 1));
            oddNeighbours.Add(Neighbour.NorthEast, new Vector(1, 0));
            oddNeighbours.Add(Neighbour.NorthWest, new Vector(-1, 0));
            oddNeighbours.Add(Neighbour.SouthEast, new Vector(1, 1));
            oddNeighbours.Add(Neighbour.SouthWest, new Vector(-1, 1));

            evenNeighbours.Add(Neighbour.North, new Vector(0, -1));
            evenNeighbours.Add(Neighbour.South, new Vector(0, 1));
            evenNeighbours.Add(Neighbour.NorthEast, new Vector(1, -1));
            evenNeighbours.Add(Neighbour.NorthWest, new Vector(-1, -1));
            evenNeighbours.Add(Neighbour.SouthEast, new Vector(1, 0));
            evenNeighbours.Add(Neighbour.SouthWest, new Vector(-1, 0));

            foreach (Vector location in Keys)
            {
                CellViewModel current = this[location];
                if (location.X % 2 == 0)
                {
                    neighbours = evenNeighbours;
                }
                else
                {
                    neighbours = oddNeighbours;
                }
                foreach (KeyValuePair<Neighbour, Vector> neighbour in neighbours)
                {
                    Vector neighbourLocation = location + neighbour.Value;
                    //Vector neighbourLocation = neighbour.Value;

                    if (ContainsKey(neighbourLocation))
                    {
                        CellViewModel newNeighbour = this[neighbourLocation];
                        current.Hex.Set(neighbour.Key, newNeighbour.Hex);
                    }
                }
            }
        }

        public void Add(int x, int y, CellViewModel v)
        {
            Vector p = new Vector(x, y);
            base.Add(p, v);
        }

        public IEnumerable GetPlayers()
        {
            HashSet<Player> tested = new HashSet<Player>();
            foreach (CellViewModel item in Values)
            {
                Player player = null;
                if (item.Hex.IsOwned())
                {
                    player = (item.Hex as AvailableCell).Owner;
                }

                if (player != null && !tested.Contains(player))
                {
                    yield return player;
                    tested.Add(player);
                }
            }
        }

        public int CellCount(Player player)
        {
            int score = 0;
            foreach (CellViewModel cell in Values)
            {
                if (cell.Hex.OwnedBy(player))
                    score++;
            }
            return score;
        }

    }
}
