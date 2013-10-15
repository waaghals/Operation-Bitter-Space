using Devcorp.Controls.Design;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Hexxagon
{
    class Tile
    {
        public System.Windows.Media.Brush Gradient { get; set; }

        private System.Windows.Media.Brush BuildGradient(short hue)
        {
            LinearGradientBrush g = new LinearGradientBrush();
            g.StartPoint = new System.Windows.Point(0, 0);
            g.EndPoint = new System.Windows.Point(0, 1);

            g.GradientStops.Add(new GradientStop(HueToColor(hue, 0.95), 0));
            g.GradientStops.Add(new GradientStop(HueToColor(hue, 0.92), 0.5));
            g.GradientStops.Add(new GradientStop(HueToColor(hue, 0.87), 0.5));
            g.GradientStops.Add(new GradientStop(HueToColor(hue, 0.82), 1));

            return g;
        }

        private System.Windows.Media.Color HueToColor(short hue, double brightness)
        {
            System.Windows.Media.Color returnColor = new System.Windows.Media.Color();
            System.Drawing.Color drawingColor = ColorSpaceHelper.HSBtoColor(new HSB(hue, 0.50, brightness));

            returnColor.A = drawingColor.A;
            returnColor.R = drawingColor.R;
            returnColor.G = drawingColor.G;
            returnColor.B = drawingColor.B;

            return returnColor;
        }

        public Tile(short h)
        {
            if (h > 359 || h < 0)
            {
                throw new Exception("Hue moet tussen de 0 en 359 liggen!");
            }
            Gradient = BuildGradient(h);
        }
    }
}
