using System;
using System.Globalization;
using System.Windows.Data;

namespace FrameworksAndDrivers.Gui.Wpf.View.Converter
{
    [ValueConversion(typeof(object), typeof(bool))]
    class IsGreaterThanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IComparable val && parameter is IComparable param)
            {
                return val.CompareTo(param) > 0;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
