using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Schedule.Model;

namespace Schedule.UI.Converters
{
    public class PositionToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IServiceProvider services = App.Services!;
            var position = (Position) value;
            if (position == Position.Student)
            {
                return @"D:\studying\Labs\KPZ\Schedule.UI\Assets\student.jpeg";
            }

            return @"D:\studying\Labs\KPZ\Schedule.UI\Assets\teacher.jpeg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
