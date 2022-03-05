using System;
using System.Globalization;
using System.Windows.Data;
using Core.Entities;

namespace FrameworksAndDrivers.Gui.Wpf.View.Converter
{
    public class IntervalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{(value as Interval)?.IntervalValue ?? 0} ({parameter as string ?? ""})";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}