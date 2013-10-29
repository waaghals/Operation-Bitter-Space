using Hexxagon.Helpers;
using System;
using System.Windows.Data;

namespace Hexxagon.Common.Helpers
{
    public class GradientConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            return GradientHelper.FromHue((short)value, 1);
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {

            //set a breakVector here
            return value;
        }
    }
}
