using Hexxagon.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexxagon.Models
{
    public class Score : BaseNotifier
    {
        public Player Player { get; set; }
        public int Vectors { get; set; }
    }
}
