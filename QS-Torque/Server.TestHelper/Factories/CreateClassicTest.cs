using System;
using Common.Types.Enums;
using Core.Enums;
using Server.Core.Entities;

namespace Server.TestHelper.Factories
{
    public class CreateClassicTest
    {
        public static ClassicChkTest CreateClassicChkTest(long id, int result, double average,
           MeaUnit controlledBy, double lowerLimit1, MeaUnit unit1, double lowerLimitUnit2, double nomainalValueUnit1, double nominalValueUnit2,
           int numberOfTests, string sensorSerialNumber, double testValueMaximum, double testValueMinimum, double thresholdTorque, long toleranceClassUnit1,
           long toleranceCLassUnit2, MeaUnit unit2, double upperLimitUnit1, double upperLimitUnit2, DateTime timestamp, TestEquipment testEquipment,
           double? standardDeviation, ClassicTestLocation location, long locationToolAssignmentId)
        {
            return new ClassicChkTest()
            {
                Id = new GlobalHistoryId(id),
                Result = new TestResult(result),
                Timestamp = timestamp,
                Average = average,
                ControlledByUnitId = controlledBy,
                LowerLimitUnit1 = lowerLimit1,
                Unit1Id = unit1,
                LowerLimitUnit2 = lowerLimitUnit2,
                NominalValueUnit1 = nomainalValueUnit1,
                NominalValueUnit2 = nominalValueUnit2,
                NumberOfTests = numberOfTests,
                SensorSerialNumber = sensorSerialNumber,
                TestValueMaximum = testValueMaximum,
                TestValueMinimum = testValueMinimum,
                ThresholdTorque = thresholdTorque,
                ToleranceClassUnit1 = new ToleranceClass(){Id = new ToleranceClassId(toleranceClassUnit1)},
                ToleranceClassUnit2 = new ToleranceClass(){Id = new ToleranceClassId(toleranceCLassUnit2)},
                Unit2Id = unit2,
                UpperLimitUnit1 = upperLimitUnit1,
                UpperLimitUnit2 = upperLimitUnit2,
                TestEquipment = testEquipment,
                StandardDeviation = standardDeviation,
                TestLocation = location,
                LocationToolAssignmentId = new LocationToolAssignmentId(locationToolAssignmentId)
            };
        }

        public static ClassicMfuTest CreateClassicMfuTest(long id, int result, double average,
            MeaUnit controlledBy, double lowerLimit1, MeaUnit unit1, double lowerLimitUnit2, double nomainalValueUnit1, double nominalValueUnit2,
            int numberOfTests, string sensorSerialNumber, double testValueMaximum, double testValueMinimum, double thresholdTorque, long toleranceClassUnit1,
            long toleranceCLassUnit2, MeaUnit unit2, double upperLimitUnit1, double upperLimitUnit2, DateTime timestamp,
            double cm, double cmk, TestEquipment testEquipment, double? standardDeviation, ClassicTestLocation location, long locationToolAssignmentId, double limitCm, double limitCmk)
        {
            return new ClassicMfuTest()
            {
                Id = new GlobalHistoryId(id),
                Result = new TestResult(result),
                Timestamp = timestamp,
                Average = average,
                ControlledByUnitId = controlledBy,
                LowerLimitUnit1 = lowerLimit1,
                Unit1Id = unit1,
                LowerLimitUnit2 = lowerLimitUnit2,
                NominalValueUnit1 = nomainalValueUnit1,
                NominalValueUnit2 = nominalValueUnit2,
                NumberOfTests = numberOfTests,
                SensorSerialNumber = sensorSerialNumber,
                TestValueMaximum = testValueMaximum,
                TestValueMinimum = testValueMinimum,
                ThresholdTorque = thresholdTorque,
                ToleranceClassUnit1 = new ToleranceClass() { Id = new ToleranceClassId(toleranceClassUnit1) },
                ToleranceClassUnit2 = new ToleranceClass() { Id = new ToleranceClassId(toleranceCLassUnit2) },
                Unit2Id = unit2,
                UpperLimitUnit1 = upperLimitUnit1,
                UpperLimitUnit2 = upperLimitUnit2,
                Cmk = cmk,
                Cm = cm,
                LimitCm = limitCm,
                LimitCmk = limitCmk,
                TestEquipment = testEquipment,
                StandardDeviation = standardDeviation,
                TestLocation = location,
                LocationToolAssignmentId = new LocationToolAssignmentId(locationToolAssignmentId)
            };
        }

        public static ClassicTestLocation CreateClassicTestLocation(long locationId, long locationDirectoryId)
        {
            return new ClassicTestLocation()
            {
                LocationId = new LocationId(locationId),
                LocationDirectoryId = new LocationDirectoryId(locationDirectoryId)
            };
        }
    }
}
