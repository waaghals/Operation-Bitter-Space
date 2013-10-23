using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hexxagon.Models
{
    class PlayerHexagon : Hexagon
    {

        public override bool Available()
        {
            return false;
        }


        public override bool OwnedBy(Player p)
        {
            return Owner == p;
        }

        public override bool Clonable
        {
            get { return false; }
        }

        public override bool Targetable
        {
            get { return false; }
        }

        public override short Hue
        {
            get { return Owner.Hue; }
        }
    }
}
