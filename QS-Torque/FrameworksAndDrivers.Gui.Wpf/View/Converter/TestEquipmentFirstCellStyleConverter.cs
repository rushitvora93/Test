using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FrameworksAndDrivers.Gui.Wpf.View.Converter
{
    public class TestEquipmentFirstCellStyleConverter : IValueConverter
    {
        public object Convert(object isIo, Type targetType, object parameter, CultureInfo culture)
        {
            if (isIo is bool value)
            {
                return value ? new SolidColorBrush(Colors.Transparent) : new SolidColorBrush(Colors.Red);
            }

            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}