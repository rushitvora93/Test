using System;
using System.Globalization;
using System.Windows.Data;

namespace FrameworksAndDrivers.Gui.Wpf.View.Converter
{
    public class ShortDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is DateTime dt ? dt.ToShortDateString() : value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}