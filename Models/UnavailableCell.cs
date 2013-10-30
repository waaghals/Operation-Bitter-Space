using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hexxagon.Models
{
    class UnavailableCell : Cell
    {
        public override bool Available()
        {
            return false;
        }

        public override bool OwnedBy(Player p)
        {
            return false;
        }

        public override bool Clonable
        {
            get { return false; }
        }
      
        public override bool Jumpable
        {
            get { return false; }
        }

        public override void HighlightFrom(Cell from, Distance distance)
        {
            return;
        }

        public override bool IsOwned()
        {
            return false;
        }
    }
}
