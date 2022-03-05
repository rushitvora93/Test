using System;
using System.Globalization;
using System.Windows.Data;
using InterfaceAdapters.Localization;
using Syncfusion.UI.Xaml.Grid;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel.GridColumns
{
    public class GridAbsoluteOrRelativeColumn : GridTextColumn
    {
        private ILocalizationWrapper _localizationWrapper;
        public GridAbsoluteOrRelativeColumn(ILocalizationWrapper localizationWrapper) : base()
        {
            _localizationWrapper = localizationWrapper;
        }

        protected override void SetDisplayBindingConverter()
        {

            if ((DisplayBinding as Binding).Converter == null)
                (DisplayBinding as Binding).Converter = new AbsolutOrRelativeFormatConverter(_localizationWrapper);
        }
    }

    public class AbsolutOrRelativeFormatConverter : IValueConverter
    {
        private ILocalizationWrapper _localizationWrapper;
        public AbsolutOrRelativeFormatConverter(ILocalizationWrapper localizationWrapper)
        {
            _localizationWrapper = localizationWrapper;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool b))
            {
                return "";
            }

            return _localizationWrapper.Strings.GetParticularString("Tolerance class", b ? "Relative" : "Absolut");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
