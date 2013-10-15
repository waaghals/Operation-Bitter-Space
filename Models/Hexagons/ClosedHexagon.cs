using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hexxagon.Models
{
    class ClosedHexagon : Hexagon
    {
        public override bool Available()
        {
            return false;
        }

        public override bool IsOwned()
        {
            return false;
        }
    }
}
