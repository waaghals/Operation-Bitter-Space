using Hexxagon.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexxagon.ViewModels
{
    public abstract class BaseViewModel : BaseNotifier
    {
        protected void SubscribeTo(INotifyPropertyChanged source)
        {
            source.PropertyChanged += ChangeHandler;
        }

        protected virtual void ChangeHandler(object sender, PropertyChangedEventArgs e)
        {
            //Just pass it forth to the views
            OnPropertyChanged(e.PropertyName);
        }
    }
}
