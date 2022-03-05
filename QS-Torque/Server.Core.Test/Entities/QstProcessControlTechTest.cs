using System.Collections.Generic;
using Common.Types.Enums;
using NUnit.Framework;
using Server.Core.Entities;
using Server.TestHelper.Factories;
using TestHelper.Checker;

namespace Server.Core.Test.Entities
{
    public class QstProcessControlTechTest
    {
        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<QstProcessControlTech> parameter, EqualityTestHelper<QstProcessControlTech> helper) helperTuple)
        {
            helperTuple.helper.CheckInequalityForParameter(helperTuple.parameter);
        }

        [Test]
        public void EqualsByContentWithRightIsNullMeansInequality()
        {
            var helper = GetEqualsByContentTestHelper();
            helper.CheckInequalityWithRightIsNull();
        }

        [Test]
        public void EqualsByContentWithEqualContentMeansEquality()
        {
            var helper = GetEqualsByContentTestHelper();
            helper.CheckEqualityForParameterList();
        }

        [Test]
        public void SameContentMeansEquality()
        {
            var processControlTech = CreateQstProcessControlTech.Randomized(4354);
            var lhs = processControlTech.CopyDeep();
            var rhs = processControlTech.CopyDeep();
            Assert.IsTrue(lhs.EqualsByContent(rhs));
        }

        [Test]
        public void EqualsWithRightIsNullMeansInequality()
        {
            var processControlTech = CreateQstProcessControlTech.Randomized(4353);
            Assert.IsFalse(processControlTech.EqualsByContent(null));
        }

        [Test]
        public void CopyingTestEquipmentIsEqualButNotSame()
        {
            var qstProcessControlTech = CreateQstProcessControlTech.Randomized(678678);
            var result = qstProcessControlTech.CopyDeep();
            Assert.AreNotSame(qstProcessControlTech, result);
            Assert.IsTrue(result.EqualsByContent(qstProcessControlTech));
        }

        [Test]
        public void CreateQstProcessControlTechSetsManufacturerIdCorrect()
        {
            var qstProcessControlTech = new QstProcessControlTech();
            Assert.AreEqual(ManufacturerIds.ID_QST, qstProcessControlTech.ManufacturerId);
        }

        private static IEnumerable<(EqualityParameter<QstProcessControlTech>, EqualityTestHelper<QstProcessControlTech>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<QstProcessControlTech> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<QstProcessControlTech>(
                (left, right) => left.EqualsByContent(right),
                () => new QstProcessControlTech(),
                new List<EqualityParameter<QstProcessControlTech>>()
                {
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.Id = (ProcessControlTechId)value,
                        CreateParameterValue = () => new ProcessControlTechId(1),
                        CreateOtherParameterValue = () => new ProcessControlTechId(2),
                        ParameterName = nameof(QstProcessControlTech.Id)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.ProcessControlConditionId = (ProcessControlConditionId)value,
                        CreateParameterValue = () => new ProcessControlConditionId(1),
                        CreateOtherParameterValue = () => new ProcessControlConditionId(2),
                        ParameterName = nameof(QstProcessControlTech.ProcessControlConditionId)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.ManufacturerId = (ManufacturerIds)value,
                        CreateParameterValue = () => ManufacturerIds.ID_QST,
                        CreateOtherParameterValue = () => ManufacturerIds.ID_SALTUS,
                        ParameterName = nameof(QstProcessControlTech.ManufacturerId)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.TestMethod = (TestMethod)value,
                        CreateParameterValue = () => TestMethod.QST_MT,
                        CreateOtherParameterValue = () => TestMethod.QST_PA,
                        ParameterName = nameof(QstProcessControlTech.TestMethod)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.Extension = (Extension)value,
                        CreateParameterValue = () => CreateExtension.Randomized(4356),
                        CreateOtherParameterValue = () => CreateExtension.Randomized(756845),
                        ParameterName = nameof(QstProcessControlTech.Extension)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.AngleLimitMt = (long?)value,
                        CreateParameterValue = () => (long?)5,
                        CreateOtherParameterValue = () => (long?)7,
                        ParameterName = nameof(QstProcessControlTech.AngleLimitMt)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.StartMeasurementPeak = (double?)value,
                        CreateParameterValue = () => (double?)6,
                        CreateOtherParameterValue = () => (double?)8,
                        ParameterName = nameof(QstProcessControlTech.StartMeasurementPeak)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.StartAngleCountingPa = (double?)value,
                        CreateParameterValue = () => (double?)9,
                        CreateOtherParameterValue = () => (double?)10,
                        ParameterName = nameof(QstProcessControlTech.StartAngleCountingPa)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.AngleForFurtherTurningPa = (double?)value,
                        CreateParameterValue = () => (double?)41,
                        CreateOtherParameterValue = () => (double?)63,
                        ParameterName = nameof(QstProcessControlTech.AngleForFurtherTurningPa)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.TargetAnglePa = (double?)value,
                        CreateParameterValue = () => (double?)44,
                        CreateOtherParameterValue = () => (double?)56,
                        ParameterName = nameof(QstProcessControlTech.TargetAnglePa)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.StartMeasurementPa = (double?)value,
                        CreateParameterValue = () => (double?)44,
                        CreateOtherParameterValue = () => (double?)56,
                        ParameterName = nameof(QstProcessControlTech.StartMeasurementPa)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.AlarmTorquePa = (double?)value,
                        CreateParameterValue = () => (double?)4,
                        CreateOtherParameterValue = () => (double?)65,
                        ParameterName = nameof(QstProcessControlTech.AlarmTorquePa)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.AlarmAnglePa = (double?)value,
                        CreateParameterValue = () => (double?)4,
                        CreateOtherParameterValue = () => (double?)86,
                        ParameterName = nameof(QstProcessControlTech.AlarmAnglePa)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.MinimumTorqueMt = (double?)value,
                        CreateParameterValue = () => (double?)40,
                        CreateOtherParameterValue = () => (double?)6,
                        ParameterName = nameof(QstProcessControlTech.MinimumTorqueMt)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.StartAngleMt = (double?)value,
                        CreateParameterValue = () => (double?)48,
                        CreateOtherParameterValue = () => (double?)6,
                        ParameterName = nameof(QstProcessControlTech.StartAngleMt)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.StartMeasurementMt = (double?)value,
                        CreateParameterValue = () => (double?)46,
                        CreateOtherParameterValue = () => (double?)86,
                        ParameterName = nameof(QstProcessControlTech.StartMeasurementMt)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.AlarmTorqueMt = (double?)value,
                        CreateParameterValue = () => (double?)4,
                        CreateOtherParameterValue = () => (double?)6,
                        ParameterName = nameof(QstProcessControlTech.AlarmTorqueMt)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.AlarmAngleMt = (double?)value,
                        CreateParameterValue = () => (double?)46,
                        CreateOtherParameterValue = () => (double?)6,
                        ParameterName = nameof(QstProcessControlTech.AlarmAngleMt)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.Alive = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(QstProcessControlTech.Alive)
                    },
                });
        }
    }
}
