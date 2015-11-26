using System;
using System.Globalization;
using System.Windows.Data;

namespace Core.Common.UI.Util
{
    /// <summary>
    /// Takes an array of multiple values and converts between a shallow copy to be passed to the VM as a single object.
    /// </summary>
    public class MultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return (object[]) value;
        }
    }
}
