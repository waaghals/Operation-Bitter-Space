using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hexxagon.Models
{
    class OpenHexagon : Hexagon
    {

        public override bool Available()
        {
            return true;
        }

        public override bool Selectable()
        {
            throw new NotImplementedException();
        }

        public override bool Clickable()
        {
            throw new NotImplementedException();
        }

        public override bool OwnedBy(Player p)
        {
            return false;
        }
    }
}
