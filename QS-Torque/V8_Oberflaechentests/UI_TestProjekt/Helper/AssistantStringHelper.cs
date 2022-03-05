namespace UI_TestProjekt.Helper
{
    public class AssistantStringHelper
    {
        public static class MpStrings
        {
            public const string Number = "Number";
            public const string Description = "Description";
            public const string ControlledBy = "Controlled by";
            public const string SetPointTorque = "Setpoint torque";
            public const string ToleranceClassTorque = "Tolerance class torque";
            public const string MinimumTorque = "Minimum torque";
            public const string MaximumTorque = "Maximum torque";
            public const string ThresholdTorque = "Threshold torque";
            public const string SetPointAngle = "Setpoint angle";
            public const string ToleranceClassAngle = "Tolerance class angle";
            public const string MinimumAngle = "Minimum angle";
            public const string MaximumAngle = "Maximum angle";
            public const string ConfigurableField1 = "Configurable field 1";
            public const string ConfigurableField2 = "Configurable field 2";
            public const string ConfigurableField3 = "Configurable field 3";
        }
        public static class ToolModelStrings
        {
            public const string Description = "Description";
            public const string Manufacturer = "Manufacturer";
            public const string ToolModelType = "Tool model type";
            public const string ToolModelClass = "Tool model class";
            public const string AirPressure = "Air pressure";
            public const string AirConsumption = "Air consumption";
            public const string BatteryVoltage = "Battery voltage";
            public const string MaxRotSpeed = "Max. rotation speed";
            public const string MinPower = "Min. power";
            public const string MaxPower = "Max. power";
            public const string ToolType = "Tool type";
            public const string Weight = "Weight";
            public const string SwitchOff = "Switch off";
            public const string ShutOff = "Shut off";
            public const string DriveSize = "Drive size";
            public const string DriveType = "Drive type";
            public const string ConstructionType = "Construction type";
        }
        public static class ToolStrings
        {
            public const string SerialNumber = "Serial number";
            public const string InventoryNumber = "Inventory number";
            public const string ToolModel = "Tool model";
            public const string Status = "Status";
            public const string Accessory = "Accessory";
            public const string ConfigurableField = "Customer";
            public const string CostCenter = "Cost center";
            public const string ConfigurableField1 = "Configurable field 1";
            public const string ConfigurableField2 = "Configurable field 2";
            public const string ConfigurableField3 = "Configurable field 3";
        }
        public static class Testequipment
        {
            public const string SerialNumber = "Serial number";
            public const string InventoryNumber = "Inventory number";
            public const string TestequipmentType = "Test equipment type";
            public const string TestequipmentModel = "Test equipment model";
            public const string LastCalibrationDate = "Last Calibration date";
            public const string Interval = "Interval";
            public const string CalibrationNorm = "Calibration norm";
            public const string FirmwareVersion = "Firmware version";
            public const string CapacityMinimum = "Capacity minimum";
            public const string CapacityMaximum = "Capacity maximum";
            public const string UseProcess = "Use for process testing";
            public const string UseRotating = "Use for rotating testing";
        }
        public static class WorkingCalendar
        {
            public const string Date = "Date";
            public const string Description = "Description";
            public const string Repetition = "Repetition";
            public const string Type = "Type";
        }
        public static class MpToolAllocation
        {
            public const string ToolUsage = "Tool usage";
            public const string StatusForTool = "Status for tool";

            public const string ControlledBy = "Controlled by";
            public const string SetpointTorque = "Setpoint torque";
            public const string ToleranceClassTorque = "Tolerance class torque";
            public const string MininumTorque = "Minimum torque";
            public const string MaximumTorque = "Maximum torque";

            public const string ThresholdTorque = "Threshold torque";
            public const string SetpointAngle = "Setpoint angle";
            public const string ToleranceClassAngle = "Tolerance class angle";
            public const string MininumAngle = "Minimum angle";
            public const string MaximumAngle = "Maximum angle";

            public const string TestLevelSetChk = "Test level set monitoring";
            //TODO "Tool level monitoring" -> "Test level monitoring" ausbessern sobald Schreibfehler gefixed ist
            public const string TestLevelChk = "Tool level monitoring";
            public const string StartDateChk = "Start date monitoring";
            public const string TestModeActiveChk = "Test mode active monitoring";
            public const string TestLevelSetMca = "Test level set MCA";
            public const string TestLevelMca = "Test level MCA";
            public const string StartDateMca = "Start date MCA";
            public const string TestModeActiveMCA = "Test mode active MCA";

            //Testtechnique
            public const string EndCycleTime = "End cycle time";
            public const string FilterFrequency = "Filter frequency";
            public const string CycleComplete = "Cycle complete";
            public const string MeasureDelay = "Measure delay";
            public const string ResetTime = "Reset time";
            public const string TorqueAndAngleBetweenLimits = "Torque and angle must be within the limits";
            public const string CycleStart = "Cycle start";
            public const string StartFinalAngle = "Start final angle";

            public const string SlipTorque = "Slip torque";
            public const string TorqueCoefficient = "Torque coefficient";
            public const string MinimumPulse = "Minimum pulse";
            public const string MaximumPulse = "Maximum pulse";
            public const string Threshold = "Threshold";
        }
        public static class ProcessControl
        {
            public const string LowerMeasuringLimit = "Lower measuring limit";
            public const string UpperMeasuringLimit = "Upper measuring limit";
            public const string LowerInterventionLimit = "Lower intervention limit";
            public const string UpperInterventionLimit = "Upper intervention limit";
            public const string TestLevelSet = "Test level set";
            public const string TestLevel = "Test level";
            public const string StartDate = "Start date";
            public const string TestModeActive = "Test mode active";
            public const string TestMethod = "Test method";
            public const string MinimumTorque = "Minimum torque";
            public const string StartAngleCount = "Start angle count";
            public const string AngleLimit = "Angle limit";
            public const string StartMeasurement = "Start measurement";
            public const string AlarmLimitTorque = "Alarm limit - torque";
            public const string AlarmLimitAngle = "Alarm limit - angle";
            public const string AngleForPrevail = "Angle for prevail torque";
            public const string TargetAngle = "Target angle";
        }
        public static class UnitStrings
        {
            public const string Nm = "Nm";
            public const string Deg = "°";
            public const string Bar = "bar";
            public const string Volt = "V";
            public const string RevolutionPerMin = "U/min";
            public const string Kg = "kg";

            public const string Sec = "s";
            public const string Hertz = "Hz";
        }
    }

    public enum HistoButton
    {
        Apply,
        Reset,
        Cancel
    }
}
