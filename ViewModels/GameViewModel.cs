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
        public ObservableDictionary<Player, int> Scores { get; private set; }
        public Map Map { get; set; }

        public Player CurrentPlayer
        {
            get
            {
                if (turns == null)
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
            Scores = new ObservableDictionary<Player, int>();
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
            UpdateScores();

            PrintScores();
        }

        private void PrintScores()
        {
            foreach (KeyValuePair<Player, int> pair in Scores)
            {
                Console.WriteLine(pair.Key.Name + ": " + pair.Value);
            }
        }

        private void UpdateScores()
        {
            Scores.Clear();
            foreach (CellViewModel cell in Map.Values)
            {
                if (!cell.Hex.IsOwned())
                    continue;

                AvailableCell ownedCell = cell.Hex as AvailableCell;
                if (Scores.ContainsKey(ownedCell.Owner))
                {
                    Scores[ownedCell.Owner]++;
                }
                else
                {
                    Scores.Add(ownedCell.Owner, 1);
                }
            }
        }
    }
}
