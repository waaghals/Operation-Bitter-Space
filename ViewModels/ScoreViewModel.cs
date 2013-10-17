using Hexxagon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Hexxagon.ViewModels
{
    public class ScoreViewModel : BaseViewModel
    {
        public Brush Hue { get; private set; }

        public ScoreViewModel(Player p)
        {
            short h = p.Hue;
        }

    }
}
