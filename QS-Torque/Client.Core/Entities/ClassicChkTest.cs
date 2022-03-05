using System;
using System.Collections.Generic;
using System.Linq;
using Common.Types.Enums;
using Core.Enums;

namespace Core.Entities
{
    public class ClassicChkTest
    {
        public ClassicChkTest()
        {
            TestValues = new List<ClassicChkTestValue>();
        }
        public GlobalHistoryId Id { get; set; }
        public DateTime Timestamp { get; set; }
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
        public TestResult Result { get; set; }
        public TestEquipment TestEquipment { get; set; }
        public ToleranceClass ToleranceClassUnit1 { get; set; }
        public ToleranceClass ToleranceClassUnit2 { get; set; }
        public List<ClassicChkTestValue> TestValues { get; set; }
        public ClassicTestLocation TestLocation { get; set; }

        public double LowerLimit => ControlledByUnitId == Unit1Id ? LowerLimitUnit1 : LowerLimitUnit2;
        public double UpperLimit => ControlledByUnitId == Unit1Id ? UpperLimitUnit1 : UpperLimitUnit2;
        public double NominalValue => ControlledByUnitId == Unit1Id ? NominalValueUnit1 : NominalValueUnit2;
        public double NominalTorque => ControlledByUnitId == Unit1Id ? NominalValueUnit1 : NominalValueUnit2;
        public double NominalAngle => ControlledByUnitId == Unit1Id ? NominalValueUnit2 : NominalValueUnit1;
        public ToleranceClass ToleranceClass => ControlledByUnitId == Unit1Id ? ToleranceClassUnit1 : ToleranceClassUnit2;

        public double GetNominalValueByMeaUnit(MeaUnit meaUnit)
        {
            if (meaUnit != Unit1Id && meaUnit != Unit2Id)
            {
                throw new ArgumentException("No Unit for meaUnit available");
            }

            return meaUnit == Unit1Id ? NominalValueUnit1 : NominalValueUnit2;
        }

        public ClassicChkTestValue GetTestValueByPosition(long position)
        {
            return TestValues.SingleOrDefault(x => x.Position == position);
        }
    }
}
