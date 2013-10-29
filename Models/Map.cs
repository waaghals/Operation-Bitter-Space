using Hexxagon.Common;
using Hexxagon.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Hexxagon.Models
{
    public class Map : ObservableDictionary<Vector, CellViewModel>
    {
        public void MapHexagons()
        {
            Dictionary<Neighbour, Vector> oddNeighbours = new Dictionary<Neighbour, Vector>();
            Dictionary<Neighbour, Vector> evenNeighbours = new Dictionary<Neighbour, Vector>();
            Dictionary<Neighbour, Vector> neighbours = new Dictionary<Neighbour, Vector>();

            oddNeighbours.Add(Neighbour.North, new Vector(0, -1));
            oddNeighbours.Add(Neighbour.South, new Vector(0, 1));
            oddNeighbours.Add(Neighbour.NorthEast, new Vector(1, 0));
            oddNeighbours.Add(Neighbour.NorthWest, new Vector(-1, 0));
            oddNeighbours.Add(Neighbour.SouthEast, new Vector(1, 1));
            oddNeighbours.Add(Neighbour.SouthWest, new Vector(-1, 1));

            evenNeighbours.Add(Neighbour.North, new Vector(0, -1));
            evenNeighbours.Add(Neighbour.South, new Vector(0, 1));
            evenNeighbours.Add(Neighbour.NorthEast, new Vector(1, -1));
            evenNeighbours.Add(Neighbour.NorthWest, new Vector(-1, -1));
            evenNeighbours.Add(Neighbour.SouthEast, new Vector(1, 0));
            evenNeighbours.Add(Neighbour.SouthWest, new Vector(-1, 0));

            foreach (Vector location in Keys)
            {
                CellViewModel current = this[location];
                if (location.X%2==0)
                {
                    neighbours = evenNeighbours;
                }
                else
                {
                    neighbours = oddNeighbours;
                }
                foreach (KeyValuePair<Neighbour, Vector> neighbour in neighbours)
                {
                    Vector neighbourLocation = location + neighbour.Value;
                    //Vector neighbourLocation = neighbour.Value;

                    if (ContainsKey(neighbourLocation))
                    {
                        CellViewModel newNeighbour = this[neighbourLocation];
                        current.Hex.Set(neighbour.Key, newNeighbour.Hex);
                    }
                }
            }
        }

        public void Add(int x, int y, CellViewModel v)
        {
            Vector p = new Vector(x, y);
            base.Add(p, v);
        }

        public IEnumerable GetPlayers()
        {
            HashSet<Player> tested = new HashSet<Player>();
            foreach (CellViewModel item in Values)
            {
                Player player = null;
                if (item.Hex.IsOwned())
                {
                    player = (item.Hex as AvailableCell).Owner;
                }

                if (player != null && !tested.Contains(player))
                {
                    yield return player;
                    tested.Add(player);
                }
            }
        }

        public int CellCount(Player player)
        {
            int score = 0;
            foreach (CellViewModel cell in Values)
            {
                if (cell.Hex.OwnedBy(player))
                    score++;
            }
            return score;
        }

    }

    public class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, INotifyPropertyChanged, INotifyCollectionChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(object sender, NotifyCollectionChangedAction action, object value)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(sender, new NotifyCollectionChangedEventArgs(action, value));
            }
        }

        private void OnPropertyChanged(object sender, string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            OnCollectionChanged(this, NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));

            OnPropertyChanged(this, "Values");
            OnPropertyChanged(this, "Keys");
            OnPropertyChanged(this, "Count");
        }

        public new bool Remove(TKey key)
        {
            TValue value = base[key];
            var result = base.Remove(key);
            if (result)
            {
                //Won't work, NotifyCollectionChangedEventArgs needs an index which Dictionary doesn't provide
                //OnCollectionChanged(this, NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>(key, value));

                OnPropertyChanged(this, "Values");
                OnPropertyChanged(this, "Keys");
                OnPropertyChanged(this, "Count");
            }
            return result;
        }

        public new void Clear()
        {
            base.Clear();
            OnCollectionChanged(this, NotifyCollectionChangedAction.Reset, null);
            OnPropertyChanged(this, "Values");
            OnPropertyChanged(this, "Keys");
            OnPropertyChanged(this, "Count");
        }

        public new TValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                Remove(key);
                Add(key, value);
                //base[key] = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("Keys"));
                //PropertyChanged(this, new PropertyChangedEventArgs("Values"));
            }
        }
    }
}
