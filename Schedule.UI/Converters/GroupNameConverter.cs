using System;
using System.Globalization;
using System.Windows.Data;
using Schedule.UI.ViewModel;

namespace Schedule.UI.Converters
{
    internal class GroupNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var group = value as GroupViewModel;
            return group.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
