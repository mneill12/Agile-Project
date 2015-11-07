using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using CSC3045.Agile.Client.Entities;

namespace Core.Common.UI.Util
{
    [ValueConversion(typeof(List<UserRole>), typeof(string))]
    public class UserRoleListToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new InvalidOperationException("The target must be a String");

            List<string> userRoleNames = new List<string>();

            if (value != null)
            {
                foreach (UserRole user in (IEnumerable<UserRole>) value)
                {
                    userRoleNames.Add(user.UserRoleName);
                }
            }

            if (userRoleNames.Count > 0)
            {
                return String.Join(", ", ((List<string>)userRoleNames).ToArray());
            }
            else
            {
                return "No roles selected!";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
