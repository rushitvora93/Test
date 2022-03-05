using System;
using System.Collections.Generic;
using Common.Types.Enums;
using Core.Enums;
using Server.Core.Enums;

namespace Server.Core.Entities
{

    public class ClassicMfuTest
    {
        public GlobalHistoryId Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Shift Shift { get; set; }
        public long NumberOfTests { get; set; }
        public double LowerLimitUnit1 { get; set; }
        public double NominalValueUnit1 { get; set; }
        public double UpperLimitUnit1 { get; set; }
        public MeaUnit Unit1Id { get; set; }
        public double LowerLimitUnit2 { get; set; }
        public double NominalValueUnit2 { get; set; }
        public double UpperLimitUnit2 { get; set; }
        public MeaUnit Unit2Id { get; set; }
        public double TestValueMinimum { get; set; }
        public double TestValueMaximum { get; set; }
        public double Average { get; set; }
        public double? StandardDeviation { get; set; }
        public MeaUnit ControlledByUnitId { get; set; }
        public double ThresholdTorque { get; set; }
        public string SensorSerialNumber { get; set; }
        public double Cm { get; set; }
        public double Cmk { get; set; }
        public double LimitCm { get; set; }
        public double LimitCmk { get; set; }
        public TestResult Result { get; set; }
        public TestEquipment TestEquipment { get; set; }
        public ToleranceClass ToleranceClassUnit1 { get; set; }
        public ToleranceClass ToleranceClassUnit2 { get; set; }
        public ClassicTestLocation TestLocation { get; set; }
        public User User { get; set; }
        public long ToolId { get; set; }
        public List<ClassicMfuTestValue> TestValues { get; set; }
        public LocationToolAssignmentId LocationToolAssignmentId { get; set; }
    }
}
