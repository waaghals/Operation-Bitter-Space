﻿using Hexxagon.Common;
using Hexxagon.Helpers;
using Hexxagon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Hexxagon.Models
{
    public class Player : BaseNotifier
    {
        #region Properties
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetProperty(ref name, value);
            }
        }

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

                SetProperty(ref hue, value);
            }
        }
        #endregion
    }
}
