using System;
using System.Collections.Generic;
using System.Linq;
using Common.Types.Enums;
using Core.Enums;
using Core.UseCases.Communication;
using NUnit.Framework;
using TestHelper.Factories;

namespace Core.Test.Entities
{
    public class TestEquipmentTestResultTest
    {
        [TestCase(10, LocationControlledBy.Angle, 4, 10)]
        [TestCase(55, LocationControlledBy.Torque, 55, 4)]
        public void GetNominalValueReturnsCorrectValue(double resultNominalValue, LocationControlledBy controlledBy, double nom1, double nom2)
        {
            var testEquipmentTestResult = new TestEquipmentTestResult()
            {
                LocationToolAssignment = CreateLocationToolAssignment.WithTestParametersControlledBy(controlledBy),
                ResultFromDataGate = new DataGateResult()
                {
                    Nom1 = nom1,
                    Nom2 = nom2
                }
            };
            Assert.AreEqual(resultNominalValue, testEquipmentTestResult.NominalValue);
        }

        [TestCase(78, LocationControlledBy.Angle, 4, 78)]
        [TestCase(38, LocationControlledBy.Torque, 38, 4)]
        public void GetLowerLimitReturnsCorrectValue(double resultLowerLimit, LocationControlledBy controlledBy, double min1, double min2)
        {
            var testEquipmentTestResult = new TestEquipmentTestResult()
            {
                LocationToolAssignment = CreateLocationToolAssignment.WithTestParametersControlledBy(controlledBy),
                ResultFromDataGate = new DataGateResult()
                {
                    Min1 = min1,
                    Min2 = min2
                }
            };
            Assert.AreEqual(resultLowerLimit, testEquipmentTestResult.LowerToleranceLimit);
        }

        [TestCase(39, LocationControlledBy.Angle, 4, 39)]
        [TestCase(22, LocationControlledBy.Torque, 22, 4)]
        public void GetUpperLimitReturnsCorrectValue(double resultUpperLimit, LocationControlledBy controlledBy, double max1, double max2)
        {
            var testEquipmentTestResult = new TestEquipmentTestResult()
            {
                LocationToolAssignment = CreateLocationToolAssignment.WithTestParametersControlledBy(controlledBy),
                ResultFromDataGate = new DataGateResult()
                {
                    Max1 = max1,
                    Max2 = max2
                }
            };
            Assert.AreEqual(resultUpperLimit, testEquipmentTestResult.UpperToleranceLimit);
        }


        private static IEnumerable<(List<double>, LocationControlledBy, List<DataGateResultValue>)>
            GetValuesReturnsCorrectValueData =
                new List<(List<double>, LocationControlledBy, List<DataGateResultValue>)>()
                {
                    (
                        new List<double>(){1, 2, 3, 4},
                        LocationControlledBy.Angle, 
                        new List<DataGateResultValue>()
                        {
                            new DataGateResultValue(){Timestamp = DateTime.Now,Value1 = 55, Value2 = 1},
                            new DataGateResultValue(){Timestamp = DateTime.Now,Value1 = 66, Value2 = 2},
                            new DataGateResultValue(){Timestamp = DateTime.Now,Value1 = 77, Value2 = 3},
                            new DataGateResultValue(){Timestamp = DateTime.Now,Value1 = 88, Value2 = 4}
                        }
                    ),
                    (
                        new List<double>(){10, 20, 30, 40},
                        LocationControlledBy.Torque,
                        new List<DataGateResultValue>()
                        {
                            new DataGateResultValue(){Timestamp = DateTime.Now,Value1 = 10, Value2 = 1},
                            new DataGateResultValue(){Timestamp = DateTime.Now,Value1 = 20, Value2 = 2},
                            new DataGateResultValue(){Timestamp = DateTime.Now,Value1 = 30, Value2 = 3},
                            new DataGateResultValue(){Timestamp = DateTime.Now,Value1 = 40, Value2 = 4}
                        }
                    )
                };

        [TestCaseSource(nameof(GetValuesReturnsCorrectValueData))]
        public void GetValuesReturnsCorrectValue((List<double> resultValues, LocationControlledBy controlledBy, List<DataGateResultValue> values) data)
        {
            var testEquipmentTestResult = new TestEquipmentTestResult()
            {
                LocationToolAssignment = CreateLocationToolAssignment.WithTestParametersControlledBy(data.controlledBy),
                ResultFromDataGate = new DataGateResult()
                {
                    Values = data.values
                }
            };
            CollectionAssert.AreEqual(data.resultValues, testEquipmentTestResult.Values.ToList());
        }

        static List<(double, double, List<DataGateResultValue>, LocationControlledBy)> TestEquipmentTestData = new List<(double, double, List<DataGateResultValue>, LocationControlledBy)>
        {
            (1.24,0.09, new List<DataGateResultValue>
            {
                new DataGateResultValue {Timestamp = DateTime.Now, Value1 = 1.2},
                new DataGateResultValue {Timestamp = DateTime.Now, Value1 = 1.4},
                new DataGateResultValue {Timestamp = DateTime.Now, Value1 = 1.3},
                new DataGateResultValue {Timestamp = DateTime.Now, Value1 = 1.2},
                new DataGateResultValue {Timestamp = DateTime.Now, Value1 = 1.2},
                new DataGateResultValue {Timestamp = DateTime.Now, Value1 = 1.3},
                new DataGateResultValue {Timestamp = DateTime.Now, Value1 = 1.1},
                new DataGateResultValue {Timestamp = DateTime.Now, Value1 = 1.2},
            }, LocationControlledBy.Torque),
            (3.26,1.32, new List<DataGateResultValue>
            {
                new DataGateResultValue {Timestamp = DateTime.Now, Value2 = 3.2},
                new DataGateResultValue {Timestamp = DateTime.Now, Value2 = 5.4},
                new DataGateResultValue {Timestamp = DateTime.Now, Value2 = 1.3},
                new DataGateResultValue {Timestamp = DateTime.Now, Value2 = 4.2},
                new DataGateResultValue {Timestamp = DateTime.Now, Value2 = 3.5},
                new DataGateResultValue {Timestamp = DateTime.Now, Value2 = 2.6},
                new DataGateResultValue {Timestamp = DateTime.Now, Value2 = 1.9},
                new DataGateResultValue {Timestamp = DateTime.Now, Value2 = 4.0},
            }, LocationControlledBy.Angle)
        };

        [Test]
        [TestCaseSource(nameof(TestEquipmentTestData))]
        public void TestEquipmentTestResultCalculatesAverageCorrectly((double average, double standDev, List<DataGateResultValue> values, LocationControlledBy controlledBy) testData)
        {
            var testEquipmentTestResult = new TestEquipmentTestResult
            {
                LocationToolAssignment = CreateLocationToolAssignment.WithTestParametersControlledBy(testData.controlledBy),
                ResultFromDataGate = new DataGateResult
                {
                    Values = testData.values
                }
            };
            Assert.AreEqual(testData.average, Math.Round(testEquipmentTestResult.Average, 2));
        }

        [Test]
        [TestCaseSource(nameof(TestEquipmentTestData))]
        public void TestEquipmentTestResultCalculatesStandardDeviationCorrectly((double average, double standDev, List<DataGateResultValue> values, LocationControlledBy controlledBy) testData)
        {
            var testEquipmentTestResult = new TestEquipmentTestResult
            {
                LocationToolAssignment = CreateLocationToolAssignment.WithTestParametersControlledBy(testData.controlledBy),
                ResultFromDataGate = new DataGateResult
                {
                    Values = testData.values
                }
            };

            Assert.AreEqual(testData.standDev, Math.Round((decimal)testEquipmentTestResult.StandardDeviation, 2));
        }

        [Test]
        public void TestEquipmentTestResultStandardDeviationIsNullWithNotEnoughValues()
        {
            var testEquipmentTestResult = new TestEquipmentTestResult
            {
                LocationToolAssignment = CreateLocationToolAssignment.Anonymous(),
                ResultFromDataGate = new DataGateResult
                {
                    Values = new List<DataGateResultValue>
                    {
                        new DataGateResultValue {Value1 = 5, Value2 = 10}
                    }
                }
            };
            Assert.IsNull(testEquipmentTestResult.StandardDeviation);
        }


        private static IEnumerable<(double, double, TestEquipmentTestResult)> TestEquipmentCmCmkTestData =
            new List<(double, double, TestEquipmentTestResult)>()
            {
                (
                    0.7, 0.54,
                    new TestEquipmentTestResult()
                    {
                        LocationToolAssignment = CreateLocationToolAssignment.WithTestParametersControlledBy(LocationControlledBy.Torque),
                        ResultFromDataGate = new DataGateResult()
                        {
                            Min1 = 20,
                            Max1 = 40,
                            Values = new List<DataGateResultValue>()
                            {
                                new DataGateResultValue(){Value1 = 30.1},
                                new DataGateResultValue(){Value1 = 30.8},
                                new DataGateResultValue(){Value1 = 30.9},
                                new DataGateResultValue(){Value1 = 32.1},
                                new DataGateResultValue(){Value1 = 30},
                                new DataGateResultValue(){Value1 = 33},
                                new DataGateResultValue(){Value1 = 45.6},
                                new DataGateResultValue(){Value1 = 30.1},
                                new DataGateResultValue(){Value1 = 30.6},
                                new DataGateResultValue(){Value1 = 30},
                            }
                        }
                    }
                ),
                (
                    1.42, 0.89,
                    new TestEquipmentTestResult()
                    {
                        LocationToolAssignment = CreateLocationToolAssignment.WithTestParametersControlledBy(LocationControlledBy.Angle),
                        ResultFromDataGate = new DataGateResult()
                        {
                            Min2 = 10,
                            Max2 = 50,
                            Values = new List<DataGateResultValue>()
                            {
                                new DataGateResultValue(){Value2 = 36.1},
                                new DataGateResultValue(){Value2 = 38.8},
                                new DataGateResultValue(){Value2 = 32.9},
                                new DataGateResultValue(){Value2 = 33.1},
                                new DataGateResultValue(){Value2 = 37},
                                new DataGateResultValue(){Value2 = 38},
                                new DataGateResultValue(){Value2 = 49.6},
                                new DataGateResultValue(){Value2 = 38.1},
                                new DataGateResultValue(){Value2 = 36.6},
                                new DataGateResultValue(){Value2 = 35},
                            }
                        }
                    }
                ),

            };

        [TestCaseSource(nameof(TestEquipmentCmCmkTestData))]
        public void TestEquipmentTestResultCalculatesCmCmkCorrectly((double expectedCm, double expectedCmk, TestEquipmentTestResult testResult) data)
        {
            Assert.AreEqual(data.expectedCm, Math.Round(data.testResult.Cm, 2));
            Assert.AreEqual(data.expectedCmk, Math.Round(data.testResult.Cmk, 2));
        }

        [TestCase(MeaUnit.Nm, MeaUnit.Nm, MeaUnit.Deg, LocationControlledBy.Torque)]
        [TestCase(MeaUnit.Deg, MeaUnit.Nm, MeaUnit.Deg, LocationControlledBy.Angle)]
        public void GetControlUnitReturnsCorrectValue(MeaUnit expectedUnit, MeaUnit unit1, MeaUnit unit2, LocationControlledBy controlledBy)
        {
            var testEquipmentTestResult = new TestEquipmentTestResult()
            {
                LocationToolAssignment = CreateLocationToolAssignment.WithTestParametersControlledBy(controlledBy),
                ResultFromDataGate = new DataGateResult()
                {
                    Unit1Id = (long) unit1,
                    Unit2Id = (long) unit2
                }
            };
            Assert.AreEqual(expectedUnit, testEquipmentTestResult.ControlUnit());
        }
    }
}
