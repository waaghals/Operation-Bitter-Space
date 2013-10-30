using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexxagon.Common
{
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
