using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FrameworksAndDrivers.Gui.Wpf.View.Converter
{
    public class TestEquipmentCellStyleConverter : IValueConverter
    {
        public object Convert(object isIo, Type targetType, object parameter, CultureInfo culture)
        {
            if (isIo is bool value)
            {
                return value ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.Red);
            }

            return new SolidColorBrush(Colors.Green);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
