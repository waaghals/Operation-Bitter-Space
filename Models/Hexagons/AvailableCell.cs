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
    }
}
