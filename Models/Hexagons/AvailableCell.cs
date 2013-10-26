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

        public void HighLightAll(Hexxagon.ViewModels.GameViewModel game)
        {
            if (game.SelectedCell == this)
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

                game.SelectedCell = null;
            }
            else
            {
                if (game.SelectedCell != null)
                {
                    ((AvailableCell)game.SelectedCell).HighLightAll(game);
                }
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
                game.SelectedCell = this;
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
                if (Owned())
                    return Owner.Hue;


                if (source != null)
                    return source.Hue;

                return 60;
            }
        }

        public override bool Clonable
        {
            get { return sourceDistance == Distance.Close; }
        }

        public override bool Targetable
        {
            get { return sourceDistance == Distance.Far; }
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

        public override bool Owned()
        {
            return Owner != null;
        }

        public void Clone(Player newPlayer)
        {
            Owner = newPlayer;
            OnPropertyChanged("Hue");
        }

        public void Jump()
        {
            Owner = null;
        }

        public void TakeOverAll()
        {
            foreach (Cell neighbour in Neighbours.Values)
            {
                if (neighbour.Owned())
                {
                    ((AvailableCell)neighbour).TakeOver(Owner);
                }
            }
        }

        private void TakeOver(Player NewOwner)
        {
            Owner = NewOwner;
            OnPropertyChanged("Hue");
        }
    }
}
