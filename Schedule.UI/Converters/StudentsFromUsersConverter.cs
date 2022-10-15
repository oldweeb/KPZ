using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Schedule.Model;
using Schedule.UI.ViewModel;

namespace Schedule.UI.Converters
{
    public class StudentsFromUsersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var users = value as IEnumerable<UserViewModel>;
            return users.Where(u => u.Position is Position.Student);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
