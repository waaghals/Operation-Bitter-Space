using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hexxagon.Models
{
    class PlayerHexagon : Hexagon
    {
        public Player Owner { get; set; }

        public override bool Available()
        {
            return false;
        }

        public override bool IsOwned()
        {
            return Owner != null;
        }
    }
}
