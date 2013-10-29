

using System;
using System.Windows.Data;

namespace DebugDataBindings
{
    public class DebugConverter : IValueConverter
    {

        public Object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            //set a breakVector here
            return ((int) value) * 10;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {

            //set a breakVector here
            return value;
        }
    }
}

