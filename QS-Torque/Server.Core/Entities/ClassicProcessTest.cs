using System;
using Common.Types.Enums;

namespace Server.Core.Entities
{
    public class ClassicProcessTest
    {
        public GlobalHistoryId Id { get; set; }
        public DateTime Timestamp { get; set; }
        public long NumberOfTests { get; set; }
        public MeaUnit ControlledByUnitId { get; set; }
        public MeaUnit Unit1Id { get; set; }
        public double LowerLimitUnit1 { get; set; }
        public double NominalValueUnit1 { get; set; }
        public double UpperLimitUnit1 { get; set; }
        public double LowerLimitUnit2 { get; set; }
        public double NominalValueUnit2 { get; set; }
        public double UpperLimitUnit2 { get; set; }
        public double LowerInterventionLimitUnit1 { get; set; }
        public double UpperInterventionLimitUnit1 { get; set; }
        public double LowerInterventionLimitUnit2 { get; set; }
        public double UpperInterventionLimitUnit2 { get; set; }
        public TestResult Result { get; set; }
        public MeaUnit Unit2Id { get; set; }
        public double TestValueMinimum { get; set; }
        public double TestValueMaximum { get; set; }
        public double Average { get; set; }
        public double? StandardDeviation { get; set; }
        public ClassicTestLocation TestLocation { get; set; }
        public TestEquipment TestEquipment { get; set; }
        public ToleranceClass ToleranceClassUnit1 { get; set; }
        public ToleranceClass ToleranceClassUnit2 { get; set; }
    }
}
