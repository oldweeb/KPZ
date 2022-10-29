using System;
using System.Globalization;
using System.Windows.Data;
using Schedule.UI.ViewModel;

namespace Schedule.UI.Converters
{
    public class UserNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return "Unga bunga";
            }
            var user = value as UserViewModel;
            return String.Concat(user.LastName, " ", user.FirstName ?? "", " ", user.MiddleName ?? "");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
