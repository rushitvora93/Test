using System;

using UI_TestProjekt.Helper;

namespace UI_TestProjekt.TestModel
{
    public class ShiftManagement
    {
        private DateTime firstShiftFromTime = new DateTime();
        private DateTime firstShiftToTime = new DateTime();

        private bool isSecondShiftActive = false;
        private DateTime secondShiftFromTime = new DateTime();
        private DateTime secondShiftToTime = new DateTime();

        private bool isThirdShiftActive = false;
        private DateTime thirdShiftFromTime = new DateTime();
        private DateTime thirdShiftToTime = new DateTime();

        private DateTime changeDayTime = new DateTime();
        private DayOfWeek firstDayOfWeekDay = DayOfWeek.Monday;

        public DateTime FirstShiftFromTime { get => firstShiftFromTime; set => firstShiftFromTime = value; }
        public DateTime FirstShiftToTime { get => firstShiftToTime; set => firstShiftToTime = value; }
        public bool IsSecondShiftActive { get => isSecondShiftActive; set => isSecondShiftActive = value; }
        public DateTime SecondShiftFromTime { get => secondShiftFromTime; set => secondShiftFromTime = value; }
        public DateTime SecondShiftToTime { get => secondShiftToTime; set => secondShiftToTime = value; }
        public bool IsThirdShiftActive { get => isThirdShiftActive; set => isThirdShiftActive = value; }
        public DateTime ThirdShiftFromTime { get => thirdShiftFromTime; set => thirdShiftFromTime = value; }
        public DateTime ThirdShiftToTime { get => thirdShiftToTime; set => thirdShiftToTime = value; }
        public DateTime ChangeDayTime { get => changeDayTime; set => changeDayTime = value; }
        public DayOfWeek FirstDayOfWeekDay { get => firstDayOfWeekDay; set => firstDayOfWeekDay = value; }

        public ShiftManagement(DateTime firstShiftFromTime, DateTime firstShiftToTime, bool isSecondShiftActive, DateTime secondShiftFromTime, DateTime secondShiftToTime, bool isThirdShiftActive, DateTime thirdShiftFromTime, DateTime thirdShiftToTime, DateTime changeDayTime, DayOfWeek firstDayOfWeekDay)
        {
            FirstShiftFromTime = firstShiftFromTime;
            FirstShiftToTime = firstShiftToTime;
            IsSecondShiftActive = isSecondShiftActive;
            SecondShiftFromTime = secondShiftFromTime;
            SecondShiftToTime = secondShiftToTime;
            IsThirdShiftActive = isThirdShiftActive;
            ThirdShiftFromTime = thirdShiftFromTime;
            ThirdShiftToTime = thirdShiftToTime;
            ChangeDayTime = changeDayTime;
            FirstDayOfWeekDay = firstDayOfWeekDay;
        }

        public enum ShiftManagementTimePickerElements
        {
            FirstShiftFromDropdown,
            FirstShiftToDropdown,
            SecondShiftFromDropdown,
            SecondShiftToDropdown,
            ThirdShiftFromDropdown,
            ThirdShiftToDropdown,
            ChangeDayDropdown,

            FirstShiftFromTextBox,
            FirstShiftToTextBox,
            SecondShiftFromTextBox,
            SecondShiftToTextBox,
            ThirdShiftFromTextBox,
            ThirdShiftToTextBox,
            ChangeDayTextBox
        }

        public string GetSecondShiftIsActiveString()
        {
            return GetIsActiveShiftString(IsSecondShiftActive);
        }
        public string GetThirdShiftIsActiveString()
        {
            return GetIsActiveShiftString(IsThirdShiftActive);
        }
        private static string GetIsActiveShiftString(bool shift)
        {
            if (shift)
            {
                return ValidationStringHelper.GeneralValidationStrings.Ok;
            }
            return ValidationStringHelper.GeneralValidationStrings.Nok;
        }
    }
}
