using System.Collections.Generic;
using Client.Core.Entities;
using Client.TestHelper.Factories;
using Common.Types.Enums;
using Core.Entities;
using NUnit.Framework;
using TestHelper.Factories;

namespace Client.Core.Test.Entities
{
    public class ClassicPfuDataTest
    {
        private static IEnumerable<(List<ClassicProcessTest>, MeaUnit)> ControlledByUnitIdReturnsCorrectValueData =
            new List<(List<ClassicProcessTest>, MeaUnit)>()
            {
                (
                    new List<ClassicProcessTest>()
                    {
                        new ClassicProcessTest(){ControlledByUnitId = MeaUnit.Deg},
                        new ClassicProcessTest(){ControlledByUnitId = MeaUnit.Nm},
                    },
                    MeaUnit.Nm
                 ),
                (
                    new List<ClassicProcessTest>()
                    {
                        new ClassicProcessTest(){ControlledByUnitId = MeaUnit.Deg},
                        new ClassicProcessTest(){ControlledByUnitId = MeaUnit.Nm},
                        new ClassicProcessTest(){ControlledByUnitId = MeaUnit.Deg},
                    },
                    MeaUnit.Deg
                )
            };

        [TestCaseSource(nameof(ControlledByUnitIdReturnsCorrectValueData))]
        public void ControlledByUnitIdReturnsCorrectValue((List<ClassicProcessTest> tests, MeaUnit controlledBy) data)
        {
            var pfuData = new ClassicPfuData(data.tests, 3);
            Assert.AreEqual(data.controlledBy, pfuData.ControlledByUnitId);
        }

        private static IEnumerable<(List<ClassicProcessTest>, double, double, double, double, double)> MinMaxAverageRangeVarianceData =
            new List<(List<ClassicProcessTest>, double, double, double, double, double)>()
            {
                (
                    new List<ClassicProcessTest>()
                    {
                        CreateClassicProcessTest.WithUnitsAndValues(MeaUnit.Deg, MeaUnit.Deg, MeaUnit.Nm, new List<(double, double)>()
                        {
                            (10, 5),
                            (11, 13),
                            (15, 16),
                            (17, 20),
                        }),
                        CreateClassicProcessTest.WithUnitsAndValues(MeaUnit.Deg, MeaUnit.Deg, MeaUnit.Nm, new List<(double, double)>()
                        {
                            (100, 50),
                            (110, 130),
                            (150, 160),
                            (170, 200),
                        })
                    },
                    10, 170, 72.875, 160, 4535.5535714285716
                ),
                (
                    new List<ClassicProcessTest>()
                    {
                        CreateClassicProcessTest.WithUnitsAndValues(MeaUnit.Nm, MeaUnit.Deg, MeaUnit.Nm, new List<(double, double)>()
                        {
                            (8, 4),
                            (9, 12),
                            (15, 16),
                            (18, 21),
                        }),
                        CreateClassicProcessTest.WithUnitsAndValues(MeaUnit.Nm, MeaUnit.Deg, MeaUnit.Nm, new List<(double, double)>()
                        {
                            (1001, 501),
                            (1101, 1301),
                            (1501, 1601),
                            (1701, 2001),
                        })
                    },
                    4, 2001, 682.125, 1997, 684186.41071428568
                )
            };

        [TestCaseSource(nameof(MinMaxAverageRangeVarianceData))]
        public void TestValueMinimumReturnsCorrectValue((List<ClassicProcessTest> tests, double min, double max, double average, double range, double variance) data)
        {
            var pfuData = new ClassicPfuData(data.tests, 3);
            Assert.AreEqual(data.min, pfuData.TestValueMinimum);
        }

        [TestCaseSource(nameof(MinMaxAverageRangeVarianceData))]
        public void TestValueMaximumReturnsCorrectValue((List<ClassicProcessTest> tests, double min, double max, double average, double range, double variance) data)
        {
            var pfuData = new ClassicPfuData(data.tests, 3);
            Assert.AreEqual(data.max, pfuData.TestValueMaximum);
        }

        [TestCaseSource(nameof(MinMaxAverageRangeVarianceData))]
        public void AverageReturnsCorrectValue((List<ClassicProcessTest> tests, double min, double max, double average, double range, double variance) data)
        {
            var pfuData = new ClassicPfuData(data.tests, 3);
            Assert.AreEqual(data.average, pfuData.Average);
        }

        [TestCaseSource(nameof(MinMaxAverageRangeVarianceData))]
        public void RangeReturnsCorrectValue((List<ClassicProcessTest> tests, double min, double max, double average, double range, double variance) data)
        {
            var pfuData = new ClassicPfuData(data.tests, 3);
            Assert.AreEqual(data.range, pfuData.Range);
        }

        [TestCaseSource(nameof(MinMaxAverageRangeVarianceData))]
        public void VarianceReturnsCorrectValue((List<ClassicProcessTest> tests, double min, double max, double average, double range, double variance) data)
        {
            var pfuData = new ClassicPfuData(data.tests, 3);
            Assert.AreEqual(data.variance, pfuData.Variance);
        }

        private static IEnumerable<(List<ClassicProcessTest>, double, double, double, ToleranceClass)> LowerLimitReturnsCorrectValueData =
            new List<(List<ClassicProcessTest>, double, double, double, ToleranceClass)>()
            {
                (
                    new List<ClassicProcessTest>()
                    {
                        new ClassicProcessTest()
                        {
                            ControlledByUnitId = MeaUnit.Deg, Unit1Id = MeaUnit.Deg, Unit2Id = MeaUnit.Nm,
                            LowerLimitUnit1 = 5, LowerLimitUnit2 = 1,
                            UpperLimitUnit1 = 50, UpperLimitUnit2 = 40,
                            NominalValueUnit1 = 40, NominalValueUnit2 = 30,
                            ToleranceClassUnit1 = CreateToleranceClass.WithId(1),
                            ToleranceClassUnit2 = CreateToleranceClass.WithId(2),
                        },
                        new ClassicProcessTest()
                        {
                            ControlledByUnitId = MeaUnit.Deg, Unit1Id = MeaUnit.Deg, Unit2Id = MeaUnit.Nm,
                            LowerLimitUnit1 = 50, LowerLimitUnit2 = 10,
                            UpperLimitUnit1 = 500, UpperLimitUnit2 = 400,
                            NominalValueUnit1 = 400, NominalValueUnit2 = 300,
                            ToleranceClassUnit1 = CreateToleranceClass.WithId(3),
                            ToleranceClassUnit2 = CreateToleranceClass.WithId(4),
                        }
                    },
                    50, 500, 400, CreateToleranceClass.WithId(3)
                ),
                (
                    new List<ClassicProcessTest>()
                    {
                        new ClassicProcessTest()
                        {
                            ControlledByUnitId = MeaUnit.Nm, Unit1Id = MeaUnit.Deg, Unit2Id = MeaUnit.Nm,
                            LowerLimitUnit1 = 51, LowerLimitUnit2 = 11,
                            UpperLimitUnit1 = 501, UpperLimitUnit2 = 401,
                            NominalValueUnit1 = 500, NominalValueUnit2 = 400,
                            ToleranceClassUnit1 = CreateToleranceClass.WithId(5),
                            ToleranceClassUnit2 = CreateToleranceClass.WithId(6)
                        },
                        new ClassicProcessTest()
                        {
                            ControlledByUnitId = MeaUnit.Nm, Unit1Id = MeaUnit.Deg, Unit2Id = MeaUnit.Nm,
                            LowerLimitUnit1 = 501, LowerLimitUnit2 = 101,
                            UpperLimitUnit1 = 5001, UpperLimitUnit2 = 4001,
                            NominalValueUnit1 = 5000, NominalValueUnit2 = 4000,
                            ToleranceClassUnit1 = CreateToleranceClass.WithId(7),
                            ToleranceClassUnit2 = CreateToleranceClass.WithId(8)
                        }
                    },
                    101, 4001, 4000, CreateToleranceClass.WithId(8)
                )
            };

        [TestCaseSource(nameof(LowerLimitReturnsCorrectValueData))]
        public void LowerLimitReturnsCorrectValue((List<ClassicProcessTest> tests, double lowerLimit, double upperLimit, double nominalValue, ToleranceClass tolClass) data)
        {
            var pfuData = new ClassicPfuData(data.tests, 3);
            Assert.AreEqual(data.lowerLimit, pfuData.LowerLimit);
        }

        [TestCaseSource(nameof(LowerLimitReturnsCorrectValueData))]
        public void UpperLimitReturnsCorrectValue((List<ClassicProcessTest> tests, double lowerLimit, double upperLimit, double nominalValue, ToleranceClass tolClass) data)
        {
            var pfuData = new ClassicPfuData(data.tests, 3);
            Assert.AreEqual(data.upperLimit, pfuData.UpperLimit);
        }

        [TestCaseSource(nameof(LowerLimitReturnsCorrectValueData))]
        public void NominalValueReturnsCorrectValue((List<ClassicProcessTest> tests, double lowerLimit, double upperLimit, double nominalValue, ToleranceClass tolClass) data)
        {
            var pfuData = new ClassicPfuData(data.tests, 3);
            Assert.AreEqual(data.nominalValue, pfuData.NominalValue);
        }

        [TestCaseSource(nameof(LowerLimitReturnsCorrectValueData))]
        public void ToleranceClassReturnsCorrectValue((List<ClassicProcessTest> tests, double lowerLimit, double upperLimit, double nominalValue, ToleranceClass tolClass) data)
        {
            var pfuData = new ClassicPfuData(data.tests, 3);
            Assert.AreEqual(data.tolClass.Id.ToLong(), pfuData.ToleranceClass.Id.ToLong());
        }

        [Test]
        public void CmkValueReturnsNullWhenUnder10Values()
        {
            var tests = new List<ClassicProcessTest>() {new ClassicProcessTest()};
            var pfuData = new ClassicPfuData(tests, 3);
            Assert.IsNull(pfuData.CmkValue);
        }

        [Test]
        public void CmValueReturnsNullWhenUnder10Values()
        {
            var tests = new List<ClassicProcessTest>() { new ClassicProcessTest() };
            var pfuData = new ClassicPfuData(tests, 3);
            Assert.IsNull(pfuData.CmValue);
        }

        [Test]
        public void VarianceReturnsNullWhenException()
        {
            var tests = new List<ClassicProcessTest>() { new ClassicProcessTest() };
            var pfuData = new ClassicPfuData(tests, 3);
            Assert.IsNull(pfuData.Variance);
        }

        [Test]
        public void StandardDeviationReturnsNullWhenException()
        {
            var tests = new List<ClassicProcessTest>() { new ClassicProcessTest() };
            var pfuData = new ClassicPfuData(tests, 3);
            Assert.IsNull(pfuData.StandardDeviation);
        }

    }
}
