using System;
using System.Globalization;
using System.Windows.Data;

namespace FrameworksAndDrivers.Gui.Wpf.View.Converter
{
    [ValueConversion(typeof(object), typeof(bool))]
    class IsEqualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
