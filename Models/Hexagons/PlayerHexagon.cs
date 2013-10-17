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

        public override bool Clickable()
        {
            throw new NotImplementedException();
        }

        public override bool OwnedBy(Player p)
        {
            return Owner == p;
        }
    }
}
