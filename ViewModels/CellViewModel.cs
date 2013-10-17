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
        }

        private Brush UpdateGradient()
        {
            if (Hex.Owner != null)
            {
                return GradientHelper.FromHue(Hex.Owner.Hue, 0.7);
            } return GradientHelper.FromHue(0, 0.0);
        }

        private Brush UpdateGradient(short hue, Distance distance)
        {
            if (distance == Distance.Close)
            {
                return GradientHelper.FromHue(hue, 0.3);
            }
            else
            {
                return GradientHelper.FromHue(hue, 0.1);
            }
            
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
                OpenHexagon openHex = hex as OpenHexagon;
                Action action = new Action(() => Gradient = UpdateGradient(openHex.highlightHue, openHex.highlightDistance));
                Application.Current.Dispatcher.Invoke(action);
            }
        }
    }
}
