using OpenQA.Selenium.Appium;

namespace UI_TestProjekt.Helper
{
    public class ShiftManagementUiHelper
    {
        public AppiumWebElement testPlanningMasterDataView;
        public AppiumWebElement shiftManagementTab;
        public AppiumWebElement save;

        public AppiumWebElement firstShiftFromText;
        public AppiumWebElement firstShiftToText;
        public AppiumWebElement secondShiftFromText;
        public AppiumWebElement secondShiftToText;
        public AppiumWebElement thirdShiftFromText;
        public AppiumWebElement thirdShiftToText;
        public AppiumWebElement changeOfDayText;

        public AppiumWebElement firstShiftFromDropdown;
        public AppiumWebElement firstShiftFromTextBox;
        public AppiumWebElement firstShiftToDropdown;
        public AppiumWebElement firstShiftToTextBox;

        public AppiumWebElement secondShiftActive;
        public AppiumWebElement secondShiftFromDropdown;
        public AppiumWebElement secondShiftFromTextBox;
        public AppiumWebElement secondShiftToDropdown;
        public AppiumWebElement secondShiftToTextBox;

        public AppiumWebElement thirdShiftActive;
        public AppiumWebElement thirdShiftFromDropdown;
        public AppiumWebElement thirdShiftFromTextBox;
        public AppiumWebElement thirdShiftToDropdown;
        public AppiumWebElement thirdShiftToTextBox;

        public AppiumWebElement changeDayDropdown;
        public AppiumWebElement changeDayTextBox;

        public AppiumWebElement firstDayOfWeek;

        public ShiftManagementUiHelper() { }
    }
}
