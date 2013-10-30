using Hexxagon.Common;
using Hexxagon.Common.Helpers;
using Hexxagon.Controls;
using Hexxagon.Models;
using Hexxagon.Commands;
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
            Player player = turns.Dequeue();
            turns.Enqueue(player);

            RemoveUnmovablePlayers();
            UpdateScores();

            if (IsEndOfGame())
            {
                WinCommand win = new WinCommand();
                String name = "";
                int score = 0;
                foreach (var item in Scores)
                {
                    if (item.Value > score)
                    {
                        score = item.Value;
                        name = item.Key.Name;
                    }
                }
                win.Execute(name + " Wins with " + score + " Points!!!!");
                ResetGame();
            }
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
            Scores.Clear();
            Map.Clear();
            Players.Clear();
            turns.Clear();
        }

        private bool IsEndOfGame()
        {
            return turns.Count() < 2;
        }

        private void UpdateScores()
        {
            Scores.Clear();
            foreach (Player p in Players)
            {
                int count = Map.CellCount(p);
                Scores.Add(p, count);
            }
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

        public void CalculateScore()
        {

        }
    }
}
