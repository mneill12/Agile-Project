using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using CSC3045.Agile.Client.Entities;

namespace Core.Common.UI.Util
{
    [ValueConversion(typeof (List<UserRole>), typeof (string))]
    public class UserRoleListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof (string))
                throw new InvalidOperationException("The target must be a String");

            var userRoleNames = new List<string>();

            if (value != null)
            {
                foreach (var user in (IEnumerable<UserRole>) value)
                {
                    userRoleNames.Add(user.UserRoleName);
                }
            }

            if (userRoleNames.Count > 0)
            {
                return string.Join(", ", userRoleNames.ToArray());
            }
            return "No roles selected!";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}