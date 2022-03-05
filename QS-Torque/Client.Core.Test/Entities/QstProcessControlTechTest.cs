using System.Collections.Generic;
using Client.Core.Entities;
using Common.Types.Enums;
using NUnit.Framework;
using TestHelper.Checker;
using Client.TestHelper.Factories;
using Core.Entities;
using Core.PhysicalValueTypes;

namespace Client.Core.Test.Entities
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

        [TestCase(0.4, true)]
        [TestCase(0.5, false)]
        [TestCase(10, false)]
        [TestCase(999, false)]
        [TestCase(1000, true)]
        public void ValidateReturnsMinimumTorqueMtNotBetween0Point5And999(double minimumTorque, bool result)
        {
            var entity = CreateQstProcessControlTech.Anonymous();
            entity.MinimumTorqueMt = Torque.FromNm(minimumTorque);
            Assert.AreEqual(result, entity.Validate(nameof(QstProcessControlTech.MinimumTorqueMt)) == ProcessControlTechValidationError.MinimumTorqueMtNotBetween0Point5And999);
        }

        [TestCase(0.4, true)]
        [TestCase(0.5, false)]
        [TestCase(10, false)]
        [TestCase(999, false)]
        [TestCase(1000, true)]
        public void ValidateReturnsStartAngleMtNotBetween0Point5And999(double startAngleMt, bool result)
        {
            var entity = CreateQstProcessControlTech.Anonymous();
            entity.StartAngleMt = Torque.FromNm(startAngleMt);
            Assert.AreEqual(result, entity.Validate(nameof(QstProcessControlTech.StartAngleMt)) == ProcessControlTechValidationError.StartAngleMtNotBetween0Point5And999);
        }

        [TestCase(0.4, 0.4, true)]
        [TestCase(0.5, 0.5, false)]
        [TestCase(10, 10, false)]
        [TestCase(999, 999, false)]
        [TestCase(1000, 1000, true)]
        [TestCase(100, 10, true)]
        [TestCase(10, 100, false)]
        public void ValidateReturnsStartMeasurementMtNotBetween0Point5And999AndGreaterThanStartAngleMt(double startMeasurementMt, double startAngleMt, bool result)
        {
            var entity = CreateQstProcessControlTech.Anonymous();
            entity.StartAngleMt = Torque.FromNm(startAngleMt);
            entity.StartMeasurementMt = Torque.FromNm(startMeasurementMt);
            Assert.AreEqual(result, entity.Validate(nameof(QstProcessControlTech.StartMeasurementMt)) == ProcessControlTechValidationError.StartMeasurementMtNotBetween0Point5And999AndGreaterThanStartAngleMt);
        }

        [TestCase(-1, -1, -1, -1, true)]
        [TestCase(0, 10, 10, 10, false)]
        [TestCase(10, 10, 10, 10, false)]
        [TestCase(9999, 9999, 9999, 9999, false)]
        [TestCase(10000, 10000, 10000, 10000, true)]
        [TestCase(5, 6, 4, 4, true)]
        [TestCase(5, 4, 6, 4, true)]
        [TestCase(5, 4, 4, 7, true)]
        [TestCase(55, 10, 15, 13, false)]
        public void ValidateReturnsAlarmTorqueMtNotBetween0And9999AndLessThanMinimumTorqueMtOrStartAngleMtOrStartMeasurementMt(double alarmTorqueMt, double startMeasurementMt, double startAngleMt, double minimumTorqueMt, bool result)
        {
            var entity = CreateQstProcessControlTech.Anonymous();
            entity.AlarmTorqueMt = Torque.FromNm(alarmTorqueMt);
            entity.MinimumTorqueMt = Torque.FromNm(minimumTorqueMt);
            entity.StartAngleMt = Torque.FromNm(startAngleMt);
            entity.StartMeasurementMt = Torque.FromNm(startMeasurementMt);
            Assert.AreEqual(result, entity.Validate(nameof(QstProcessControlTech.AlarmTorqueMt)) == ProcessControlTechValidationError.AlarmTorqueMtNotBetween0And9999AndLessThanMinimumTorqueMtOrStartAngleMtOrStartMeasurementMt);
        }

        [TestCase(0.4, true)]
        [TestCase(0.5, false)]
        [TestCase(10, false)]
        [TestCase(999, false)]
        [TestCase(1000, true)]
        public void ValidateReturnsStartAngleCountingPaNotBetween0Point5And999(double startAngleCountingPa, bool result)
        {
            var entity = CreateQstProcessControlTech.Anonymous();
            entity.StartAngleCountingPa = Torque.FromNm(startAngleCountingPa);
            Assert.AreEqual(result, entity.Validate(nameof(QstProcessControlTech.StartAngleCountingPa)) == ProcessControlTechValidationError.StartAngleCountingPaNotBetween0Point5And999);
        }

        [TestCase(0.4, 0.4, true)]
        [TestCase(0.5, 0.5, false)]
        [TestCase(10, 10, false)]
        [TestCase(999, 999, false)]
        [TestCase(1000, 1000, true)]
        [TestCase(100, 10, true)]
        [TestCase(10, 100, false)]
        public void ValidateReturnsStartMeasurementPaNotBetween0Point5And999OrGreaterThanStartAngleCountingPa(double startMeasurementPa, double startAngleCounting, bool result)
        {
            var entity = CreateQstProcessControlTech.Anonymous();
            entity.StartAngleCountingPa = Torque.FromNm(startAngleCounting);
            entity.StartMeasurementPa = Torque.FromNm(startMeasurementPa);
            Assert.AreEqual(result, entity.Validate(nameof(QstProcessControlTech.StartMeasurementPa)) == ProcessControlTechValidationError.StartMeasurementPaNotBetween0Point5And999OrGreaterThanStartAngleCountingPa);
        }

        [TestCase(-1, -1, -1, true)]
        [TestCase(0, 10, 10, false)]
        [TestCase(10, 10, 10, false)]
        [TestCase(9999, 9999, 9999, false)]
        [TestCase(10000, 10000, 10000, true)]
        [TestCase(5, 6, 4, true)]
        [TestCase(5, 4, 6, true)]
        [TestCase(55, 10, 15, false)]
        public void ValidateReturnsAlarmTorquePaNotBetween0And9999AndLessThanStartAngleCountingPaOrStartMeasurementPa(double alarmTorquePa, double startMeasurementPa, double startAngleCounting, bool result)
        {
            var entity = CreateQstProcessControlTech.Anonymous();
            entity.AlarmTorquePa = Torque.FromNm(alarmTorquePa);
            entity.StartAngleCountingPa = Torque.FromNm(startAngleCounting);
            entity.StartMeasurementPa = Torque.FromNm(startMeasurementPa);
            Assert.AreEqual(result, entity.Validate(nameof(QstProcessControlTech.AlarmTorquePa)) == ProcessControlTechValidationError.AlarmTorquePaNotBetween0And9999AndLessThanStartAngleCountingPaOrStartMeasurementPa);
        }

        [TestCase(-0.4, true)]
        [TestCase(0, false)]
        [TestCase(10, false)]
        [TestCase(99, false)]
        [TestCase(100, true)]
        public void ValidateReturnsAngleForFurtherTurningPaNotBetween0And99(double turningPa, bool result)
        {
            var entity = CreateQstProcessControlTech.Anonymous();
            entity.AngleForFurtherTurningPa = Angle.FromDegree(turningPa);
            Assert.AreEqual(result, entity.Validate(nameof(QstProcessControlTech.AngleForFurtherTurningPa)) == ProcessControlTechValidationError.AngleForFurtherTurningPaNotBetween0And99);
        }

        [TestCase(-0.4, true)]
        [TestCase(0, false)]
        [TestCase(10, false)]
        [TestCase(20, false)]
        [TestCase(21, true)]
        public void ValidateReturnsTargetAnglePaNotBetween0And20(double targetPa, bool result)
        {
            var entity = CreateQstProcessControlTech.Anonymous();
            entity.TargetAnglePa = Angle.FromDegree(targetPa);
            Assert.AreEqual(result, entity.Validate(nameof(QstProcessControlTech.TargetAnglePa)) == ProcessControlTechValidationError.TargetAnglePaNotBetween0And20);
        }

        [TestCase(-1, true)]
        [TestCase(0, false)]
        [TestCase(100, false)]
        [TestCase(9999, false)]
        [TestCase(10000, true)]
        public void ValidateReturnsAlarmAngleMtNotBetween0And9999(double alarmAngleMt, bool result)
        {
            var entity = CreateQstProcessControlTech.Anonymous();
            entity.AlarmAngleMt = Angle.FromDegree(alarmAngleMt);
            Assert.AreEqual(result, entity.Validate(nameof(QstProcessControlTech.AlarmAngleMt)) == ProcessControlTechValidationError.AlarmAngleMtNotBetween0And9999);
        }

        [TestCase(0, true)]
        [TestCase(0.5, false)]
        [TestCase(100, false)]
        [TestCase(999, false)]
        [TestCase(1000, true)]
        public void ValidateReturnsStartMeasurementPeakNotBetween0Point5And999(double startMea, bool result)
        {
            var entity = CreateQstProcessControlTech.Anonymous();
            entity.StartMeasurementPeak = Torque.FromNm(startMea);
            Assert.AreEqual(result, entity.Validate(nameof(QstProcessControlTech.StartMeasurementPeak)) == ProcessControlTechValidationError.StartMeasurementPeakNotBetween0Point5And999);
        }

        [TestCase(0.9, true)]
        [TestCase(1, false)]
        [TestCase(20, false)]
        [TestCase(99, false)]
        [TestCase(100, true)]
        public void ValidateReturnsAngleLimitMtNotBetween1And99(double angleLimitMt, bool result)
        {
            var entity = CreateQstProcessControlTech.Anonymous();
            entity.AngleLimitMt = Angle.FromDegree(angleLimitMt);
            Assert.AreEqual(result, entity.Validate(nameof(QstProcessControlTech.AngleLimitMt)) == ProcessControlTechValidationError.AngleLimitMtNotBetween1And99);
        }

        [TestCase(-1, -1, -1, true)]
        [TestCase(0, 10, 10, false)]
        [TestCase(10, 10, 10, false)]
        [TestCase(9999, 9999, 9999, false)]
        [TestCase(10000, 10000, 10000, true)]
        [TestCase(5, 6, 4, true)]
        [TestCase(5, 4, 6, true)]
        [TestCase(55, 10, 15, false)]
        public void ValidateReturnsAlarmAnglePaNotBetween0And9999OrLessThanAngleForFurtherTurningPaOrTargetAnglePa(double alarmAnglePa, double angleForFurtherTurningPa, double targetAnglePa, bool result)
        {
            var entity = CreateQstProcessControlTech.Anonymous();
            entity.AlarmAnglePa = Angle.FromDegree(alarmAnglePa);
            entity.TargetAnglePa = Angle.FromDegree(targetAnglePa);
            entity.AngleForFurtherTurningPa = Angle.FromDegree(angleForFurtherTurningPa);
            Assert.AreEqual(result, entity.Validate(nameof(QstProcessControlTech.AlarmAnglePa)) == ProcessControlTechValidationError.AlarmAnglePaNotBetween0And9999OrLessThanAngleForFurtherTurningPaOrTargetAnglePa);
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
                        CreateParameterValue = () => CreateExtension.Randomized(32556),
                        CreateOtherParameterValue = () => CreateExtension.Randomized(343),
                        ParameterName = nameof(QstProcessControlTech.Extension)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.AngleLimitMt = (Angle)value,
                        CreateParameterValue = () => Angle.FromDegree(1),
                        CreateOtherParameterValue = () => Angle.FromDegree(7),
                        ParameterName = nameof(QstProcessControlTech.AngleLimitMt)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.StartMeasurementPeak = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(6),
                        CreateOtherParameterValue = () => Torque.FromNm(8),
                        ParameterName = nameof(QstProcessControlTech.StartMeasurementPeak)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.StartAngleCountingPa = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(10),
                        CreateOtherParameterValue = () => Torque.FromNm(9),
                        ParameterName = nameof(QstProcessControlTech.StartAngleCountingPa)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.AngleForFurtherTurningPa = (Angle)value,
                        CreateParameterValue = () => Angle.FromDegree(41),
                        CreateOtherParameterValue = () => Angle.FromDegree(53),
                        ParameterName = nameof(QstProcessControlTech.AngleForFurtherTurningPa)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.TargetAnglePa = (Angle)value,
                        CreateParameterValue = () => Angle.FromDegree(14),
                        CreateOtherParameterValue = () => Angle.FromDegree(15),
                        ParameterName = nameof(QstProcessControlTech.TargetAnglePa)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.StartMeasurementPa = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(44),
                        CreateOtherParameterValue = () => Torque.FromNm(6),
                        ParameterName = nameof(QstProcessControlTech.StartMeasurementPa)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.AlarmTorquePa = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(65),
                        CreateOtherParameterValue = () => Torque.FromNm(68),
                        ParameterName = nameof(QstProcessControlTech.AlarmTorquePa)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.AlarmAnglePa = (Angle)value,
                        CreateParameterValue = () => Angle.FromDegree(15),
                        CreateOtherParameterValue = () => Angle.FromDegree(19),
                        ParameterName = nameof(QstProcessControlTech.AlarmAnglePa)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.MinimumTorqueMt = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(40),
                        CreateOtherParameterValue = () => Torque.FromNm(6),
                        ParameterName = nameof(QstProcessControlTech.MinimumTorqueMt)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.StartAngleMt = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(48),
                        CreateOtherParameterValue = () => Torque.FromNm(6),
                        ParameterName = nameof(QstProcessControlTech.StartAngleMt)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.StartMeasurementMt = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(46),
                        CreateOtherParameterValue = () => Torque.FromNm(68),
                        ParameterName = nameof(QstProcessControlTech.StartMeasurementMt)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.AlarmTorqueMt = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(4),
                        CreateOtherParameterValue = () => Torque.FromNm(6),
                        ParameterName = nameof(QstProcessControlTech.AlarmTorqueMt)
                    },
                    new EqualityParameter<QstProcessControlTech>()
                    {
                        SetParameter = (entity, value) => entity.AlarmAngleMt = (Angle)value,
                        CreateParameterValue = () => Angle.FromDegree(167),
                        CreateOtherParameterValue = () => Angle.FromDegree(1),
                        ParameterName = nameof(QstProcessControlTech.AlarmAngleMt)
                    },
                });
        }
    }
}
