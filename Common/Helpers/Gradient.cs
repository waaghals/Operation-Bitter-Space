
using Devcorp.Controls.Design;
using System.Windows.Media;

namespace Hexxagon.Helpers
{
    public class CellGradient
    {
        #region Static Methods

        public static Brush FromHue(short hue)
        {
            return FromHue(hue, 0.5);
        }

        public static Brush FromHue(short hue, double saturation)
        {
            LinearGradientBrush g = new LinearGradientBrush();
            g.StartPoint = new System.Windows.Point(0, 0);
            g.EndPoint = new System.Windows.Point(0, 1);

            g.GradientStops.Add(new GradientStop(HueToColor(hue, saturation, 0.95), 0));
            g.GradientStops.Add(new GradientStop(HueToColor(hue, saturation, 0.92), 0.5));
            g.GradientStops.Add(new GradientStop(HueToColor(hue, saturation, 0.87), 0.5));
            g.GradientStops.Add(new GradientStop(HueToColor(hue, saturation, 0.82), 1));

            return g;
        }

        private static Color HueToColor(short hue, double saturation, double brightness)
        {
            Color returnColor = new Color();
            System.Drawing.Color drawingColor = ColorSpaceHelper.HSBtoColor(new HSB(hue, saturation, brightness));

            returnColor.A = drawingColor.A;
            returnColor.R = drawingColor.R;
            returnColor.G = drawingColor.G;
            returnColor.B = drawingColor.B;

            return returnColor;
        }
        #endregion
    }
}
