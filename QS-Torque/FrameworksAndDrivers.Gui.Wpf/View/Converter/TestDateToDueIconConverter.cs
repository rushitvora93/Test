using Client.Core.Entities;
using Core.Enums;
using InterfaceAdapters.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FrameworksAndDrivers.Gui.Wpf.View.Converter
{
    public enum DueIcon
    {
        RedPast,
        YellowPresence,
        GreenFuture
    }

    public class TestDateToDueIconConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values[0] == null || values[1] == null || values[1] == DependencyProperty.UnsetValue)
            {
                return DueIcon.RedPast;
            }

            var now = DateTime.Now;
            var today = now.Date;
            var currentShift = new ShiftCalculator((values[1] as ShiftManagementModel).Entity).GetShiftForDateTime(now);

            var locToolAssignment = (values[0] as LocationToolAssignmentModel)?.Entity;
            if (locToolAssignment != null && parameter != null)
            {
                var model = new LocationToolAssignmentModelWithTestType(locToolAssignment, null) { TestType = (TestType)parameter };
                if (today < model.NextTestDateForTestType)
                {
                    return DueIcon.GreenFuture;
                }
                else if (today == model.NextTestDateForTestType)
                {
                    if (model.NextTestShiftForTestType == null)
                    {
                        return DueIcon.YellowPresence;
                    }
                    else if (currentShift < model.NextTestShiftForTestType)
                    {
                        return DueIcon.GreenFuture;
                    }
                    else if (currentShift == model.NextTestShiftForTestType)
                    {
                        return DueIcon.YellowPresence;
                    }
                    else
                    {
                        return DueIcon.RedPast;
                    }
                }
                else
                {
                    return DueIcon.RedPast;
                }
            }

            var processControlCondition = (values[0] as ProcessControlConditionHumbleModel)?.Entity;
            if(processControlCondition != null)
            {
                if (today < processControlCondition.NextTestDate)
                {
                    return DueIcon.GreenFuture;
                }
                else if (today == processControlCondition.NextTestDate)
                {
                    if (processControlCondition.NextTestShift == null)
                    {
                        return DueIcon.YellowPresence;
                    }
                    else if (currentShift < processControlCondition.NextTestShift)
                    {
                        return DueIcon.GreenFuture;
                    }
                    else if (currentShift == processControlCondition.NextTestShift)
                    {
                        return DueIcon.YellowPresence;
                    }
                    else
                    {
                        return DueIcon.RedPast;
                    }
                }
                else
                {
                    return DueIcon.RedPast;
                }
            }

            return DueIcon.RedPast;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}