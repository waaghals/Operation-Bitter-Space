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

namespace Hexxagon.ViewModels
{
    public class GameViewModel
    {
        private Queue<Player> turns;
        public ObservableCollection<Player> Players { get; set; }

        public Player CurrentPlayer
        {
            get
            {
                return turns.Peek();
            }
            private set;
        }
        
        public GameViewModel()
        {
            turns = new Queue<Player>();
        }

        public void DoTurn()
        {
            Player p = turns.Dequeue();
            turns.Enqueue(p);
        }
    }
}
