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
    public class Map : ObservableDictionary<Point, CellViewModel>
    {
        public void MapHexagons()
        {
            Dictionary<Neighbour, Point3D> neighbours = new Dictionary<Neighbour, Point3D>();
            Dictionary<Neighbour, Point> nn = new Dictionary<Neighbour, Point>();

            foreach (Point key in Keys)
            {
                Point C = key; //Current

                nn.Clear();
                nn.Add(Neighbour.North, new Point(C.X, C.Y - 1));
                nn.Add(Neighbour.South, new Point(C.X, C.Y + 1));
                nn.Add(Neighbour.NorthEast, new Point(C.X + 1, C.Y - 1));
                nn.Add(Neighbour.NorthWest, new Point(C.X - 1, C.Y));
                nn.Add(Neighbour.SouthEast, new Point(C.X - 1, C.Y + 1));
                nn.Add(Neighbour.SouthWest, new Point(C.X + 1, C.Y));

                //neighbours.Clear();
                //neighbours.Add(Neighbour.North, new Point3D(C.X, C.Y + 1, C.Z - 1));
                //neighbours.Add(Neighbour.South, new Point3D(C.X, C.Y - 1, C.Z + 1));
                //neighbours.Add(Neighbour.NorthEast, new Point3D(C.X + 1, C.Y, C.Z - 1));
                //neighbours.Add(Neighbour.NorthWest, new Point3D(C.X - 1, C.Y + 1, C.Z));
                //neighbours.Add(Neighbour.SouthEast, new Point3D(C.X + 1, C.Y - 1, C.Z));
                //neighbours.Add(Neighbour.SouthWest, new Point3D(C.X - 1, C.Y, C.Z + 1));

                foreach (KeyValuePair<Neighbour, Point> neighbour in nn)
                {
                    CellViewModel current = this[key];
                    Point neighbourLocation = neighbour.Value;

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
            Point p = new Point(x, y);
            base.Add(p, v);
        }

        //public void Add(int x, int y, int z, CellViewModel v)
        //{
        //    Point3D p = new Point3D(x, y, z);
        //    Add(p, v);
        //}

        //public void Add(Point3D key, CellViewModel value)
        //{
        //    if (key.X + key.Y + key.Z != 0)
        //    {
        //        throw new ArgumentException("Sum of X, Y and Z should always be zero");
        //    }
        //    base.Add(key, value);
        //}

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
