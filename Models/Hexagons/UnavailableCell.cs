using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hexxagon.Models
{
    class UnavailableCell : Cell
    {
        //private PlayerHexagon source;
        private Distance sourceDistance;

        public override bool Available()
        {
            return false;
        }

        public override bool OwnedBy(Player p)
        {
            return false;
        }

       // public void HighlightFrom(Cell from, Distance distance)
       // {
       //     source = from as PlayerHexagon;
       //     sourceDistance = distance;
       //     OnPropertyChanged("Highlight");
       // }

        public override bool Clonable
        {
            get { return false; }
        }
      
        public override bool Targetable
        {
            get { return false; }
        }

        public override void HighlightFrom(Cell from, Distance distance)
        {
            return;
        }

        public override bool Owned()
        {
            return false;
        }
    }
}
