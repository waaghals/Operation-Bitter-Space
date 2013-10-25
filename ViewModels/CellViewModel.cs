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

        private Cell hex;
        public Cell Hex
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

        public CellViewModel(Cell h, GameViewModel currentGame)
        {
            Hex = h;
            ClickCommand = new SelectCommand(h, currentGame);
            Gradient = UpdateGradient();

            if (Hex.Owned())
            {
                SubscribeTo((Hex as AvailableCell).Owner);
            }
            SubscribeTo(Hex);
        }

        private Brush UpdateGradient()
        {
            if (Hex.Owned())
            {
                return GradientHelper.FromHue((Hex as AvailableCell).Hue, 0.9);
            }
            else if (Hex.Clonable)
            {
                return GradientHelper.FromHue((Hex as AvailableCell).Hue, 0.4);
            }
            else if (Hex.Targetable)
            {
                return GradientHelper.FromHue((Hex as AvailableCell).Hue, 0.1);
            }
            else if (Hex.Available())
            {
                return GradientHelper.FromHue(0, 0.0);
            }
            return Brushes.White;
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
