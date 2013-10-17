using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hexxagon.Models
{
    class OpenHexagon : Hexagon
    {
        public short highlightHue;
        public Distance highlightDistance;

        public override bool Available()
        {
            return true;
        }

        public override bool Clickable()
        {
            throw new NotImplementedException();
        }

        public override bool OwnedBy(Player p)
        {
            return false;
        }

        public void HighlightFrom(Hexagon from, Distance distance)
        {
            highlightHue = from.Owner.Hue;
            highlightDistance = distance;
            OnPropertyChanged("Highlight");
        }
    }
}
