using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using Schedule.UI.ViewModel;

namespace Schedule.UI.Converters
{
    internal class GroupMembersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var members = value as IEnumerable<UserViewModel>;
            if (members is null)
            {
                return "";
            }

            return String.Join(", ", members);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
