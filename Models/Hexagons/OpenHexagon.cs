using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hexxagon.Models
{
    class OpenHexagon : Hexagon
    {
        private PlayerHexagon source;
        private Distance sourceDistance;

        public override bool Available()
        {
            return true;
        }

        public override bool OwnedBy(Player p)
        {
            return false;
        }

        public void HighlightFrom(Hexagon from, Distance distance)
        {
            source = from as PlayerHexagon;
            sourceDistance = distance;
            OnPropertyChanged("Highlight");
        }

        public override bool Clonable
        {
            get { return sourceDistance == Distance.Close; }
        }

        public override bool Targetable
        {
            get { return sourceDistance == Distance.Far; }
        }

        public override short Hue
        {
            get
            {
                if (source != null)
                {
                    return source.Owner.Hue;
                }
                return 0; //White
            }
        }
    }
}
