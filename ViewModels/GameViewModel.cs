using Hexxagon.Common;
using Hexxagon.Common.Helpers;
using Hexxagon.Controls;
using Hexxagon.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                if (turns == null || turns.Count == 0)
                {
                    return null;
                }
                return turns.Peek();
            }
        }

        public AvailableCell SelectedCell { get; set; }
        
        public GameViewModel()
        {
            Map = new Map();
            Players = new ObservableCollection<Player>();
            SubscribeTo(Map);
        }

        public void AddPlayersFromMap()
        {
            foreach (Player p in Map.GetPlayers())
            {
                if (!Players.Contains(p))
                Players.Add(p);
            }
            IList<Player> playerList = Players.ToList();
            playerList.Shuffle();

            turns = new Queue<Player>(playerList);
        }

        public void DoTurn()
        {
            Player p = turns.Dequeue();
            turns.Enqueue(p);

            RemoveUnmovablePlayers();

            if (IsEndOfGame())
            {
                Console.WriteLine("Game Over");
                Console.WriteLine("");
                if (turns.Count == 1)
                    Console.WriteLine(turns.Peek().Name + " is Victorious");
                else
                    Console.WriteLine("Mutual Assured Destruction has been achieved...");
                Application.Current.Windows[0].Close();

                Console.Read();
            }
        }

        private bool IsEndOfGame()
        {
            if (turns.Count <= 1)
            {
                return true;
            }
            return false;
        }

        public void CalculateScore()
        {

        }

        private bool IsGameOver(Player p)
        {
            foreach (CellViewModel cell in Map.Values)
            {
                if ((cell.Hex.OwnedBy(p) && cell.Hex.CanMove()))
                {
                    return false;
                }
            }
            return true;
        }

        private void RemoveUnmovablePlayers()
        {
            while (IsGameOver(CurrentPlayer))
            {
                turns.Dequeue();
                if (turns.Count() == 0)
                    break;
            }
        }

        private void ResetGame()
        {
            //Scores.Clear();
            Map.Clear();
            Players.Clear();
            turns.Clear();
        }
    }
}
