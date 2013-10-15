using Hexxagon.Common;
using Hexxagon.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hexxagon.Models
{
    class Game : BaseNotifier
    {
        public HashSet<Player> Players { get; set; }
        public Queue Turns { get; set; }
        public Map Map { get; set; }
    }
}
