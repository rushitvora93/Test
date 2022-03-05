using System;
using System.Globalization;
using Common.Types.Enums;
using Core.Enums;
using Core.UseCases.Communication;
using InterfaceAdapters;
using InterfaceAdapters.Localization;

namespace FrameworksAndDrivers.Gui.Wpf.Model
{
    public class TestEquipmentTestResultHumbleModel : BindableBase
    {
        public readonly TestEquipmentTestResult TestEquipmentTestResult;
        private readonly ILocalizationWrapper _localization;
        private readonly string _doubleformat = "0.000";
        private readonly CultureInfo _culture = CultureInfo.InvariantCulture;
        private readonly string _degreeUnitString;
        private readonly string _nmUnitString;

        public TestEquipmentTestResultHumbleModel(TestEquipmentTestResult testEquipmentTestResult, ILocalizationWrapper localization)
        {
            TestEquipmentTestResult = testEquipmentTestResult;
            _localization = localization;
            _degreeUnitString = _localization.Strings.GetParticularString("Unit", "°");
            _nmUnitString = _localization.Strings.GetParticularString("Unit", "Nm");
        }

        private string GetFormattedValue(double value)
        {
            var unitStr = TestEquipmentTestResult.ControlUnit() == MeaUnit.Nm ? _nmUnitString :
                TestEquipmentTestResult.ControlUnit() == MeaUnit.Deg ? _degreeUnitString : "";

            return value.ToString(_doubleformat, _culture) + " " + unitStr;
        }

        private string GetFormattedValue(double? value)
        {
            return value == null ? "-" : GetFormattedValue(value.Value);
        }

        public string LocationNumber => TestEquipmentTestResult.LocationNumber.ToDefaultString();
        public string LocationDescription => TestEquipmentTestResult.LocationDescription.ToDefaultString();
        public string InventoryNumber => TestEquipmentTestResult.ToolInventoryNumber;
        public string SerialNumber => TestEquipmentTestResult.ToolSerialNumber;
        public string NominalValue => GetFormattedValue(TestEquipmentTestResult.NominalValue);
        public string LowerToleranceLimit => GetFormattedValue(TestEquipmentTestResult.LowerToleranceLimit);
        public string UpperToleranceLimit => GetFormattedValue(TestEquipmentTestResult.UpperToleranceLimit);
        public int SampleCount => TestEquipmentTestResult.SampleCount;
        public DateTime TestTimestamp => TestEquipmentTestResult.TestTimestamp;
        public string Average => GetFormattedValue(TestEquipmentTestResult.Average);
        public string StandardDeviation => GetFormattedValue(TestEquipmentTestResult.StandardDeviation);
        public bool IsIo => TestEquipmentTestResult.TestResult.IsIo;
        public string NioSign => TestEquipmentTestResult.TestResult.IsIo ? "" : "!";
    }
}
