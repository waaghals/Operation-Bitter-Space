using Hexxagon.Commands;
using Hexxagon.Models;
using Hexxagon.Helpers;
using Hexxagon.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Hexxagon.ViewModels
{
    public class CellViewModel : BaseViewModel
    {
        #region Properties
        public ICommand ClickCommand { get; set; }
        public ICommand SecondClickCommand { get; set; }

        private Hexagon hex;
        public Hexagon Hex
        {
            get
            {
                return hex;
            }
            set
            {
                SetProperty(ref hex, value);
            }
        }

        private Brush gradient;
        public Brush Gradient
        {
            get
            {
                return gradient;
            }
            set
            {
                SetProperty(ref gradient, value);
            }
        }

        #endregion

        public CellViewModel(Hexagon h, GameViewModel currentGame)
        {
            Hex = h;
            ClickCommand = new SelectCommand(h, currentGame);
            Gradient = UpdateGradient();


            if (Hex.Owner != null)
            {
                SubscribeTo(Hex.Owner);
            }
            SubscribeTo(Hex);
        }

        private Brush UpdateGradient()
        {
            if (Hex.Owner != null)
            {
                return GradientHelper.FromHue(Hex.Hue, 0.9);
            }
            else if (Hex.Clonable)
            {
                return GradientHelper.FromHue(Hex.Hue, 0.6);
            }
            else if (Hex.Targetable)
            {
                return GradientHelper.FromHue(Hex.Hue, 0.2);
            }

            //white
            return GradientHelper.FromHue(0, 0.0);
        }


        protected override void ChangeHandler(object sender, PropertyChangedEventArgs e)
        {
            //base.ChangeHandler(sender, e);

            //Player hue updated, redraw the Gradient
            if (e.PropertyName == "Hue")
            {
                //Updating the Colors directly wil result in modifing the UI from a non-UI thread, 
                //which can't be done in WPF
                Application.Current.Dispatcher.Invoke(new Action(() => Gradient = UpdateGradient()));
            }

            if (e.PropertyName == "Highlight")
            {
                if (sender.Equals(Hex))
                {
                    Application.Current.Dispatcher.Invoke(() => Gradient = UpdateGradient());
                }
                    
            }
        }
    }
}
