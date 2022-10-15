using System;
using System.Globalization;
using System.Windows.Data;
using Schedule.UI.ViewModel;

namespace Schedule.UI.Converters
{
    public class EventNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ev = value as EventViewModel;
            return String.Concat(ev.Name, ", ", ev.Type.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
