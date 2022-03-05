using System;
using System.Collections.Generic;
using System.Linq;
using Common.Types.Enums;
using Core.Entities;

namespace Client.Core.Entities
{
    public class ClassicProcessTest
    {
        public ClassicProcessTest()
        {
            TestValues = new List<ClassicProcessTestValue>();
        }
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
        public List<ClassicProcessTestValue> TestValues { get; set; }
        public ClassicTestLocation TestLocation { get; set; }
        public TestEquipment TestEquipment { get; set; }
        public ToleranceClass ToleranceClassUnit1 { get; set; }
        public ToleranceClass ToleranceClassUnit2 { get; set; }

        public double NominalValue => ControlledByUnitId == Unit1Id ? NominalValueUnit1 : NominalValueUnit2;
        public double LowerLimit => ControlledByUnitId == Unit1Id ? LowerLimitUnit1 : LowerLimitUnit2;
        public double UpperLimit => ControlledByUnitId == Unit1Id ? UpperLimitUnit1 : UpperLimitUnit2;
        public double LowerInterventionLimit => ControlledByUnitId == Unit1Id ? LowerInterventionLimitUnit1 : LowerInterventionLimitUnit2;
        public double UpperInterventionLimit => ControlledByUnitId == Unit1Id ? UpperInterventionLimitUnit1 : UpperInterventionLimitUnit2;
        public ToleranceClass ToleranceClass => ControlledByUnitId == Unit1Id ? ToleranceClassUnit1 : ToleranceClassUnit2;


        public ClassicProcessTestValue GetTestValueByPosition(long position)
        {
            return TestValues.SingleOrDefault(x => x.Position == position);
        }

        public void SetTestValues(List<ClassicProcessTestValue> values)
        {
           TestValues = values.FindAll(x => x.Id.Equals(Id)).OrderBy(x => x.Position).ToList();
           foreach (var val in TestValues)
           {
               val.ProcessTest = this;
           }
        }
    }
}
