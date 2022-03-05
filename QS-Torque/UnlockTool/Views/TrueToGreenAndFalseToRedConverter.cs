using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace UnlockTool.Views
{
    public class TrueToGreenAndFalseToRedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush greenBrush = new SolidColorBrush(Colors.Green);
            Brush redBrush = new SolidColorBrush(Colors.Red);
            if (value is bool b)
            {
                return b ? greenBrush : redBrush;
            }

            return redBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
