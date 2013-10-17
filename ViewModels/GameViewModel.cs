using Hexxagon.Common;
using Hexxagon.Controls;
using Hexxagon.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace Hexxagon.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private Queue<Player> turns;
        public ObservableCollection<Player> Players { get; set; }
        public ObservableCollection<Score> Scores { get; private set; }
        public Map Map { get; set; }

        public Player CurrentPlayer
        {
            get
            {
                return turns.Peek();
            }
        }
        
        public GameViewModel()
        {
            Map = new Map();
            SubscribeTo(Map);
            turns = new Queue<Player>();
        }

        public void DoTurn()
        {
            Player p = turns.Dequeue();
            turns.Enqueue(p);
        }
    }
}
