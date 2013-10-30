using Hexxagon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hexxagon.Models
{
    public class AvailableCell : Cell
    {
        private AvailableCell source;
        private Distance sourceDistance;
        public Player Owner { get; set; }

        public void ShowNeighbours()
        {
            foreach (Cell neighbour in Neighbours.Values)
            {
                foreach (Cell farNeighbour in neighbour.Neighbours.Values)
                {
                    if (farNeighbour.Available())
                    {
                        farNeighbour.HighlightFrom(this, Distance.Far);
                    }
                }
            }

            foreach (Cell neighbour in Neighbours.Values)
            {
                if (neighbour.Available())
                {
                    neighbour.HighlightFrom(this, Distance.Close);
                }
            }
        }

        public void HideNeighbours()
        {
            foreach (Cell neighbour in Neighbours.Values)
            {
                foreach (Cell farNeighbour in neighbour.Neighbours.Values)
                {
                    if (farNeighbour.Available())
                    {
                        farNeighbour.HighlightFrom(this, Distance.OutOfReach);
                    }
                }
            }
        }

        public override void HighlightFrom(Cell from, Distance distance)
        {
            source = from as AvailableCell;
            sourceDistance = distance;
            OnPropertyChanged("Highlight");
        }

        public short Hue
        {
            get
            {
                if (IsOwned())
                    return Owner.Hue;


                if (source != null)
                    return source.Hue;

                return 60;
            }
        }

        public override bool Clonable
        {
            get { return sourceDistance == Distance.Close && !IsOwned(); }
        }

        public override bool Jumpable
        {
            get { return sourceDistance == Distance.Far && !IsOwned(); }
        }

        public override bool Available()
        {
            return true;
        }

        public override bool OwnedBy(Player p)
        {
            if (Owner == null)
                return false;

            return Owner.Equals(p);
        }

        public override bool IsOwned()
        {
            return Owner != null;
        }

        public void TakeOverNeighbours()
        {
            foreach (Cell neighbour in Neighbours.Values)
            {
                if (neighbour.IsOwned())
                {
                    ((AvailableCell)neighbour).Owner = this.Owner;
                    
                }
            }
            OnPropertyChanged("Hue");
        }

        internal void JumpTo(AvailableCell dest)
        {
            dest.Owner = this.Owner;
            this.Owner = null;
        }

        internal void CloneTo(AvailableCell dest)
        {
            dest.Owner = this.Owner;
        }
    }
}
