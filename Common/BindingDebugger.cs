﻿

using System;
using System.Windows.Data;

namespace DebugDataBindings
{
    public class DebugConverter : IValueConverter
    {

        public Object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {

            //set a breakpoint here
            return value;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {

            //set a breakpoint here
            return value;
        }
    }
}

