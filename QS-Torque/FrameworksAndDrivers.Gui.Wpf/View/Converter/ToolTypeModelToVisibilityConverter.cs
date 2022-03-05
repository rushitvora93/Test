using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Core.Entities.ToolTypes;
using InterfaceAdapters.Models;
using Syncfusion.Data.Extensions;

namespace FrameworksAndDrivers.Gui.Wpf.View.Converter
{
    public class ToolTypeModelToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a ToolType with Parameter to Visibility
        /// </summary>
        /// <param name="value">ToolType</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">String wit names of ToolType seperated by comma</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is AbstractToolTypeModel))
            {
                return Visibility.Collapsed;
            }

            List<string> parameters = (parameter as string).Split(',').ToList();
            Visibility returnVisibility = Visibility.Collapsed;
            switch (value)
            {
                case ClickWrenchModel clickWrenchModel:
                    if (parameters.Contains(nameof(ClickWrench)))
                    {
                        returnVisibility = Visibility.Visible;
                    }
                    break;
                case ECDriverModel ecDriverModel:
                    if (parameters.Contains(nameof(ECDriver)))
                    {
                        returnVisibility = Visibility.Visible;
                    }
                    break;
                case GeneralModel generalModel:
                    if (parameters.Contains(nameof(General)))
                    {
                        returnVisibility = Visibility.Visible;
                    }
                    break;
                case MDWrenchModel mdWrenchModel:
                    if (parameters.Contains(nameof(MDWrench)))
                    {
                        returnVisibility = Visibility.Visible;
                    }
                    break;
                case ProductionWrenchModel productionWrenchModel:
                    if (parameters.Contains(nameof(ProductionWrench)))
                    {
                        returnVisibility = Visibility.Visible;
                    }
                    break;
                case PulseDriverModel pulseDriverModel:
                    if (parameters.Contains(nameof(PulseDriver)))
                    {
                        returnVisibility = Visibility.Visible;
                    }
                    break;
                case PulseDriverShutOffModel pulseDriverShutOffModel:
                    if (parameters.Contains(nameof(PulseDriverShutOff)))
                    {
                        returnVisibility = Visibility.Visible;
                    }
                    break;
            }
            return returnVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}