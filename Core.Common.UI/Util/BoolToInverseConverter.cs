﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Core.Common.UI.Util
{
    /// <summary>
    ///     Converts a bool to inverse.
    /// </summary>
    public class BoolToInverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is bool && (bool) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is bool && (bool) value);
        }
    }
}