using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FrameworksAndDrivers.Gui.Wpf.View.Converter
{
    public class TransferToTestEquipmentCellStyleConverter : IValueConverter
    {
        public object Convert(object hasError, Type targetType, object parameter, CultureInfo culture)
        {
            if (hasError is bool value)
            {
                return value ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Black);
            }

            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
