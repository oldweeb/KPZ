using System;
using System.Globalization;
using System.Windows.Data;

namespace Schedule.UI.Converters
{
    public class SelectedDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DayOfWeek day = (DayOfWeek) value;
            return $"Schedule: {day}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
