using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Schedule.UI.Converters
{
    public class AlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TextAlignment alignment = (TextAlignment)value;
            HorizontalAlignment target = Enum.Parse<HorizontalAlignment>(Enum.GetNames<HorizontalAlignment>()
                .FirstOrDefault(h => h == alignment.ToString(), "Stretch")
            );
            return target;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}