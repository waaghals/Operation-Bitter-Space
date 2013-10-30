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
            //Dictionary<Neighbour, Vector3D> neighbours = new Dictionary<Neighbour, Vector3D>();
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

                //neighbours.Clear();
                //neighbours.Add(Neighbour.North, new Vector(C.X, C.Y - 1));
                //neighbours.Add(Neighbour.South, new Vector(C.X, C.Y + 1));
                //neighbours.Add(Neighbour.NorthEast, new Vector(C.X + 1, C.Y - 1));
                //neighbours.Add(Neighbour.NorthWest, new Vector(C.X - 1, C.Y));
                //neighbours.Add(Neighbour.SouthEast, new Vector(C.X - 1, C.Y + 1));
                //neighbours.Add(Neighbour.SouthWest, new Vector(C.X + 1, C.Y));

                //neighbours.Clear();
                //neighbours.Add(Neighbour.North, new Vector3D(C.X, C.Y + 1, C.Z - 1));
                //neighbours.Add(Neighbour.South, new Vector3D(C.X, C.Y - 1, C.Z + 1));
                //neighbours.Add(Neighbour.NorthEast, new Vector3D(C.X + 1, C.Y, C.Z - 1));
                //neighbours.Add(Neighbour.NorthWest, new Vector3D(C.X - 1, C.Y + 1, C.Z));
                //neighbours.Add(Neighbour.SouthEast, new Vector3D(C.X + 1, C.Y - 1, C.Z));
                //neighbours.Add(Neighbour.SouthWest, new Vector3D(C.X - 1, C.Y, C.Z + 1));

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
            //x = q
            //z = r - (q - q&1) / 2
            //y = -x-z

            //int z = x - (y - y % 2) / 2;
            //y = -x - z;
            Vector p = new Vector(x, y);
            base.Add(p, v);
        }

        //public void Add(int x, int y, int z, CellViewModel v)
        //{
        //    Vector3D p = new Vector3D(x, y, z);
        //    Add(p, v);
        //}

        //public void Add(Vector3D key, CellViewModel value)
        //{
        //    if (key.X + key.Y + key.Z != 0)
        //    {
        //        throw new ArgumentException("Sum of X, Y and Z should always be zero");
        //    }
        //    base.Add(key, value);
        //}

        public IEnumerable GetPlayers()
        {
            HashSet<Player> tested = new HashSet<Player>();
            foreach (CellViewModel item in Values)
            {
                Player player = null;
                if (item.Hex.Owned())
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
    }

    public class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, INotifyPropertyChanged, INotifyCollectionChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public event NotifyCollectionChangedEventHandler CollectionChanged = delegate { };

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

            //OnPropertyChanged(this, "Values");
            //OnPropertyChanged(this, "Keys");
            //OnPropertyChanged(this, "Count");
        }

        public new bool Remove(TKey key)
        {
            var kvp = base[key];
            var result = base.Remove(key);
            if (result)
            {
                OnCollectionChanged(this, NotifyCollectionChangedAction.Remove, kvp);

                //OnPropertyChanged(this, "Values");
                //OnPropertyChanged(this, "Keys");
                //OnPropertyChanged(this, "Count");
            }
            return result;
        }

        public new TValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Keys"));
            }
        }
    }
}
