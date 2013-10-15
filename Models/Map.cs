using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hexxagon.Models
{
    class Map : IEnumerable
    {
        private List<Hexagon> hexagons;

        public Map()
        {
            hexagons = new List<Hexagon>();
        }

        public IEnumerator GetEnumerator()
        {
            foreach (Hexagon hex in hexagons)
            {
                yield return hex;
            }
        }

        public IEnumerator GetLocations()
        {
            foreach (Hexagon hex in hexagons)
            {
                yield return hex.Location;
            }
        }

        public IEnumerator GetEnumerator(Player p)
        {
            foreach (Hexagon hex in hexagons)
            {
                if (hex.IsOwned())
                {
                    PlayerHexagon pHex = (PlayerHexagon)hex;
                    if (pHex.Owner == p)
                    {
                        yield return hex;
                    }
                }
            }
        }

        public IEnumerator GetLocations(Player p)
        {
            foreach (Hexagon hex in hexagons)
            {
                if (hex.IsOwned())
                {
                    PlayerHexagon pHex = (PlayerHexagon)hex;
                    if (pHex.Owner == p)
                    {
                        yield return hex.Location;
                    }
                }
            }
        }
    }
}
