using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hexxagon.Models
{
    public class Player : BaseModel
    {
        public string Name { get; set; }
        private short hue;
        public short Hue
        {
            get
            {
                return hue;
            }
            set
            {
                if (value > 349 || value < 0)
                    throw new ArgumentException("Player.Hue needs to be between 0 and 349");

                hue = value;
                OnPropertyChanged("hue");
            }
        }
    }
}
