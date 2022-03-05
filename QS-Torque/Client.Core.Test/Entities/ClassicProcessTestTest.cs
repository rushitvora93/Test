using System.Collections.Generic;
using System.Linq;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.Entities;
using NUnit.Framework;
using TestHelper.Factories;

namespace Client.Core.Test.Entities
{
    public class ClassicProcessTestTest
    {
        [TestCase(MeaUnit.Nm, MeaUnit.Nm, MeaUnit.Deg, 5.0, 6.0, 5.0)]
        [TestCase(MeaUnit.Deg, MeaUnit.Nm, MeaUnit.Deg, 15.0, 16.0, 16.0)]
        public void GetLowerLimitGetsCorrectValue(MeaUnit controledByUnitId, MeaUnit unit1Id, MeaUnit unit2Id, double lowerlimit1, double lowerLimit2, double resultLowerLimit)
        {
            var classicProcessTest = new ClassicProcessTest
            {
                ControlledByUnitId = controledByUnitId,
                Unit1Id = unit1Id,
                Unit2Id = unit2Id,
                LowerLimitUnit1 = lowerlimit1,
                LowerLimitUnit2 = lowerLimit2,
            };

            Assert.AreEqual(resultLowerLimit, classicProcessTest.LowerLimit);
        }

        [TestCase(MeaUnit.Nm, MeaUnit.Nm, MeaUnit.Deg, 5.0, 6.0, 5.0)]
        [TestCase(MeaUnit.Deg, MeaUnit.Nm, MeaUnit.Deg, 15.0, 16.0, 16.0)]
        public void GetUpperLimitGetsCorrectValue(MeaUnit controledByUnitId, MeaUnit unit1Id, MeaUnit unit2Id, double upperlimit1, double upperLimit2, double resultUpperLimit)
        {
            var classicProcessTest = new ClassicProcessTest
            {
                ControlledByUnitId = controledByUnitId,
                Unit1Id = unit1Id,
                Unit2Id = unit2Id,
                UpperLimitUnit1 = upperlimit1,
                UpperLimitUnit2 = upperLimit2,
            };

            Assert.AreEqual(resultUpperLimit, classicProcessTest.UpperLimit);
        }

        [TestCase(MeaUnit.Nm, MeaUnit.Nm, MeaUnit.Deg, 5.0, 6.0, 5.0)]
        [TestCase(MeaUnit.Deg, MeaUnit.Nm, MeaUnit.Deg, 15.0, 16.0, 16.0)]
        public void GetLowerInterventionLimitGetsCorrectValue(MeaUnit controledByUnitId, MeaUnit unit1Id, MeaUnit unit2Id, double lowerlimit1, double lowerLimit2, double resultLowerLimit)
        {
            var classicProcessTest = new ClassicProcessTest
            {
                ControlledByUnitId = controledByUnitId,
                Unit1Id = unit1Id,
                Unit2Id = unit2Id,
                LowerInterventionLimitUnit1 = lowerlimit1,
                LowerInterventionLimitUnit2 = lowerLimit2,
            };

            Assert.AreEqual(resultLowerLimit, classicProcessTest.LowerInterventionLimit);
        }

        [TestCase(MeaUnit.Nm, MeaUnit.Nm, MeaUnit.Deg, 5.0, 6.0, 5.0)]
        [TestCase(MeaUnit.Deg, MeaUnit.Nm, MeaUnit.Deg, 15.0, 16.0, 16.0)]
        public void GetUpperInterventionLimitGetsCorrectValue(MeaUnit controledByUnitId, MeaUnit unit1Id, MeaUnit unit2Id, double upperlimit1, double upperLimit2, double resultUpperLimit)
        {
            var classicProcessTest = new ClassicProcessTest
            {
                ControlledByUnitId = controledByUnitId,
                Unit1Id = unit1Id,
                Unit2Id = unit2Id,
                UpperInterventionLimitUnit1 = upperlimit1,
                UpperInterventionLimitUnit2 = upperLimit2,
            };

            Assert.AreEqual(resultUpperLimit, classicProcessTest.UpperInterventionLimit);
        }

        [TestCase(MeaUnit.Nm, MeaUnit.Nm, MeaUnit.Deg, 5.0, 6.0, 5.0)]
        [TestCase(MeaUnit.Deg, MeaUnit.Nm, MeaUnit.Deg, 15.0, 16.0, 16.0)]
        public void GetNominalValueGetsCorrectValue(MeaUnit controledByUnitId, MeaUnit unit1Id, MeaUnit unit2Id, double nominal1, double nominal2, double nominalResult)
        {
            var classicProcessTest = new ClassicProcessTest
            {
                ControlledByUnitId = controledByUnitId,
                Unit1Id = unit1Id,
                Unit2Id = unit2Id,
                NominalValueUnit1 = nominal1,
                NominalValueUnit2 = nominal2,
            };

            Assert.AreEqual(nominalResult, classicProcessTest.NominalValue);
        }

        [Test]
        public void GetTestValueByPositionReturnsNullWhenPositionNotFound()
        {
            var classicProcessTest = new ClassicProcessTest();
            classicProcessTest.TestValues.Add(new ClassicProcessTestValue() { Position = 1 });
            classicProcessTest.TestValues.Add(new ClassicProcessTestValue() { Position = 2 });
            classicProcessTest.TestValues.Add(new ClassicProcessTestValue() { Position = 3 });

            var processTestValue = classicProcessTest.GetTestValueByPosition(10);
            Assert.IsNull(processTestValue);
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetTestValueByPositionReturnsCorrectChkTestValue(long position)
        {
            var classicProcessTest = new ClassicProcessTest();
            classicProcessTest.TestValues.Add(new ClassicProcessTestValue() { Position = 1 });
            classicProcessTest.TestValues.Add(new ClassicProcessTestValue() { Position = 2 });
            classicProcessTest.TestValues.Add(new ClassicProcessTestValue() { Position = 3 });

            var processTestValue = classicProcessTest.GetTestValueByPosition(position);
            Assert.AreEqual(position, processTestValue.Position);
        }

        [TestCase(MeaUnit.Nm, MeaUnit.Nm, MeaUnit.Deg, 99, 66, 99)]
        [TestCase(MeaUnit.Deg, MeaUnit.Nm, MeaUnit.Deg, 1, 12, 12)]
        public void GetToleranceClassGetsCorrectValue(MeaUnit controledByUnitId, MeaUnit unit1Id, MeaUnit unit2Id, long toleranceUnit1Id, long toleranceUnit2Id, long toleranceClassIdResult)
        {
            var classicMfuTest = new ClassicProcessTest
            {
                ControlledByUnitId = controledByUnitId,
                Unit1Id = unit1Id,
                Unit2Id = unit2Id,
                ToleranceClassUnit1 = CreateToleranceClass.WithId(toleranceUnit1Id),
                ToleranceClassUnit2 = CreateToleranceClass.WithId(toleranceUnit2Id)
            };

            Assert.AreEqual(toleranceClassIdResult, classicMfuTest.ToleranceClass.Id.ToLong());
        }

        static IEnumerable<(long, List<ClassicProcessTestValue>, List<ClassicProcessTestValue>)> SetTestValuesSetsCorrectValueData = new List<(long, List<ClassicProcessTestValue>, List<ClassicProcessTestValue>)>()
        {
            (
                1,
                new List<ClassicProcessTestValue>()
                {
                    new ClassicProcessTestValue()
                    {
                        Id = new GlobalHistoryId(5),
                        Position = 2,
                    },
                    new ClassicProcessTestValue()
                    {
                        Id = new GlobalHistoryId(1),
                        Position = 2,
                    },
                    new ClassicProcessTestValue()
                    {
                        Id = new GlobalHistoryId(1),
                        Position = 1,
                    }
                }, 
                new List<ClassicProcessTestValue>()
                {
                    new ClassicProcessTestValue()
                    {
                        Id = new GlobalHistoryId(1),
                        Position = 1,
                    },
                    new ClassicProcessTestValue()
                    {
                        Id = new GlobalHistoryId(1),
                        Position = 2,
                    }
                }
            ),
            (
                5,
                new List<ClassicProcessTestValue>()
                {
                    new ClassicProcessTestValue()
                    {
                        Id = new GlobalHistoryId(5),
                        Position = 2,
                    },
                    new ClassicProcessTestValue()
                    {
                        Id = new GlobalHistoryId(1),
                        Position = 2,
                    },
                    new ClassicProcessTestValue()
                    {
                        Id = new GlobalHistoryId(5),
                        Position = 1,
                    }
                },
                new List<ClassicProcessTestValue>()
                {
                    new ClassicProcessTestValue()
                    {
                        Id = new GlobalHistoryId(5),
                        Position = 1,
                    },
                    new ClassicProcessTestValue()
                    {
                        Id = new GlobalHistoryId(5),
                        Position = 2,
                    }
                }),

        };

        [TestCaseSource(nameof(SetTestValuesSetsCorrectValueData))]
        public void SetTestValuesSetsCorrectValue((long testid, List<ClassicProcessTestValue> values, List<ClassicProcessTestValue> expectedValues) data)
        {
            var classicTest = new ClassicProcessTest() {Id = new GlobalHistoryId(data.testid)};
            classicTest.SetTestValues(data.values);

            Assert.AreEqual(data.expectedValues.Select(x => x.Id.ToLong()).ToList(), classicTest.TestValues.Select(x => x.Id.ToLong()).ToList());
            Assert.AreEqual(data.expectedValues.Select(x => x.Position).ToList(), classicTest.TestValues.Select(x => x.Position).ToList());

            foreach (var val in classicTest.TestValues)
            {
                Assert.AreSame(classicTest,val.ProcessTest);
            }
        }

    }
}
