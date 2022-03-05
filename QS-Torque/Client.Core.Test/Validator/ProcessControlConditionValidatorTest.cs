using Client.Core.Validator;
using Client.TestHelper.Factories;
using Core.PhysicalValueTypes;
using NUnit.Framework;

namespace Client.Core.Test.Validator
{
    public class ProcessControlConditionValidatorTest
    {
        [TestCase(3, 2, false)]
        [TestCase(7.8, 2.3, false)]
        [TestCase(5, 8, true)]
        [TestCase(6, 6, false)]
        [TestCase(-1, 6, false)]
        [TestCase(10, 99990, false)]
        public void ValidateReturnsCorrectValueForLowerAndUpperMeasuringLimit(double min, double max, bool result)
        {
            var testEquipment = CreateProcessControlCondition.Anonymous();
            testEquipment.LowerMeasuringLimit = Torque.FromNm(min);
            testEquipment.UpperMeasuringLimit = Torque.FromNm(max);

            var validator = new ProcessControlConditionValidator();
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }

        [TestCase(3, 2, false)]
        [TestCase(7.8, 2.3, false)]
        [TestCase(5, 8, true)]
        [TestCase(6, 6, true)]
        [TestCase(-1, 6, false)]
        [TestCase(10, 99990, false)]
        public void ValidateReturnsCorrectValueForLowerAndUpperInterventionLimit(double min, double max, bool result)
        {
            var testEquipment = CreateProcessControlCondition.Anonymous();
            testEquipment.LowerInterventionLimit = Torque.FromNm(min);
            testEquipment.UpperInterventionLimit = Torque.FromNm(max);

            var validator = new ProcessControlConditionValidator();
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }

        [TestCase(-1, false)]
        [TestCase(0.4, false)]
        [TestCase(0.5, true)]
        [TestCase(10, true)]
        [TestCase(999, true)]
        [TestCase(1000, false)]
        public void ValidateReturnsCorrectValueForQstMinimumTorqueMt(double minimumTorque, bool result)
        {
            var testEquipment = CreateProcessControlCondition.Anonymous();
            var qstTech = CreateQstProcessControlTech.Anonymous();
            qstTech.MinimumTorqueMt = Torque.FromNm(minimumTorque);
            testEquipment.ProcessControlTech = qstTech;

            var validator = new ProcessControlConditionValidator();
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }

        [TestCase(-1, false)]
        [TestCase(0.4, false)]
        [TestCase(0.5, true)]
        [TestCase(10, true)]
        [TestCase(999, true)]
        [TestCase(1000, false)]
        public void ValidateReturnsCorrectValueForStartAngleMt(double startAngleMt, bool result)
        {
            var testEquipment = CreateProcessControlCondition.Anonymous();
            var qstTech = CreateQstProcessControlTech.Anonymous();
            qstTech.StartAngleMt = Torque.FromNm(startAngleMt);
            qstTech.StartMeasurementMt = Torque.FromNm(0.5);
            testEquipment.ProcessControlTech = qstTech;

            var validator = new ProcessControlConditionValidator();
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }

        [TestCase(0.4, 0.5, false)]
        [TestCase(0.5, 0.5, true)]
        [TestCase(10, 10, true)]
        [TestCase(999, 999, true)]
        [TestCase(1000, 1000, false)]
        [TestCase(100, 10, false)]
        [TestCase(10, 100, true)]
        public void ValidateReturnsCorrectValueForStartMeasurementMt(double startMeasurementMt, double startAngleMt, bool result)
        {
            var testEquipment = CreateProcessControlCondition.Anonymous();
            var qstTech = CreateQstProcessControlTech.Anonymous();
            qstTech.StartAngleMt = Torque.FromNm(startAngleMt);
            qstTech.StartMeasurementMt = Torque.FromNm(startMeasurementMt);
            qstTech.AlarmTorqueMt = Torque.FromNm(0);
            testEquipment.ProcessControlTech = qstTech;

            var validator = new ProcessControlConditionValidator();
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }

        [TestCase(-1, -1, -1, -1, false)]
        [TestCase(0, 10, 10, 10, true)]
        [TestCase(10, 10, 10, 10, true)]
        [TestCase(9999, 999, 999, 999, true)]
        [TestCase(10000, 10000, 10000, 10000, false)]
        [TestCase(5, 6, 4, 4, false)]
        [TestCase(5, 4, 6, 4, false)]
        [TestCase(5, 4, 4, 7, false)]
        [TestCase(55, 10, 15, 13, true)]
        public void ValidateReturnsCorrectValueForAngleTorqueMt(double alarmTorqueMt, double startMeasurementMt, double startAngleMt, double minimumTorqueMt, bool result)
        {
            var testEquipment = CreateProcessControlCondition.Anonymous();
            var qstTech = CreateQstProcessControlTech.Anonymous();
            qstTech.AlarmTorqueMt = Torque.FromNm(alarmTorqueMt);
            qstTech.StartMeasurementMt = Torque.FromNm(startMeasurementMt);
            qstTech.StartAngleMt = Torque.FromNm(startAngleMt);
            qstTech.MinimumTorqueMt = Torque.FromNm(minimumTorqueMt);
            testEquipment.ProcessControlTech = qstTech;

            var validator = new ProcessControlConditionValidator();
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }

        [TestCase(-1, false)]
        [TestCase(0.4, false)]
        [TestCase(0.5, true)]
        [TestCase(10, true)]
        [TestCase(999, true)]
        [TestCase(1000, false)]
        public void ValidateReturnsCorrectValueForStartAngleCountingPa(double startAngleCountingPa, bool result)
        {
            var testEquipment = CreateProcessControlCondition.Anonymous();
            var qstTech = CreateQstProcessControlTech.Anonymous();
            qstTech.StartAngleCountingPa = Torque.FromNm(startAngleCountingPa);
            qstTech.StartMeasurementPa = Torque.FromNm(startAngleCountingPa);
            qstTech.AlarmTorquePa = Torque.FromNm(0);
            testEquipment.ProcessControlTech = qstTech;

            var validator = new ProcessControlConditionValidator();
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }

        [TestCase(-1, -1, -1, false)]
        [TestCase(0, 10, 10, true)]
        [TestCase(10, 10, 10, true)]
        [TestCase(9999, 999, 999, true)]
        [TestCase(10000, 10000, 10000, false)]
        [TestCase(5, 6, 4, false)]
        [TestCase(5, 4, 6, false)]
        [TestCase(55, 10, 15, true)]
        public void ValidateReturnsCorrectValueForAlarmTorquePa(double alarmTorquePa, double startMeasurementPa, double startAnglePa, bool result)
        {
            var testEquipment = CreateProcessControlCondition.Anonymous();
            var qstTech = CreateQstProcessControlTech.Anonymous();
            qstTech.AlarmTorquePa = Torque.FromNm(alarmTorquePa);
            qstTech.StartMeasurementPa = Torque.FromNm(startMeasurementPa);
            qstTech.StartAngleCountingPa = Torque.FromNm(startAnglePa);
            testEquipment.ProcessControlTech = qstTech;

            var validator = new ProcessControlConditionValidator();
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }

        [TestCase(0.4, 0.5, false)]
        [TestCase(0.5, 0.5, true)]
        [TestCase(10, 10, true)]
        [TestCase(999, 999, true)]
        [TestCase(1000, 1000, false)]
        [TestCase(100, 10, false)]
        [TestCase(10, 100, true)]
        public void ValidateReturnsCorrectValueForStartMeasurementPa(double startMeasurementPa, double startAnglePa, bool result)
        {
            var testEquipment = CreateProcessControlCondition.Anonymous();
            var qstTech = CreateQstProcessControlTech.Anonymous();
            qstTech.StartAngleCountingPa = Torque.FromNm(startAnglePa);
            qstTech.StartMeasurementPa = Torque.FromNm(startMeasurementPa);
            qstTech.AlarmTorquePa = Torque.FromNm(0);
            testEquipment.ProcessControlTech = qstTech;

            var validator = new ProcessControlConditionValidator();
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }

        [TestCase(-1, false)]
        [TestCase(0, true)]
        [TestCase(10, true)]
        [TestCase(99, true)]
        [TestCase(100, false)]
        public void ValidateReturnsCorrectValueForAngleForFurtherTurningPa(double turningPa, bool result)
        {
            var testEquipment = CreateProcessControlCondition.Anonymous();
            var qstTech = CreateQstProcessControlTech.Anonymous();
            qstTech.AngleForFurtherTurningPa = Angle.FromDegree(turningPa);
            qstTech.AlarmAnglePa = Angle.FromDegree(0);
            testEquipment.ProcessControlTech = qstTech;

            var validator = new ProcessControlConditionValidator();
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }

        [TestCase(-1, false)]
        [TestCase(0, true)]
        [TestCase(10, true)]
        [TestCase(20, true)]
        [TestCase(21, false)]
        public void ValidateReturnsCorrectValueForTargetAnglePa(double targetPa, bool result)
        {
            var testEquipment = CreateProcessControlCondition.Anonymous();
            var qstTech = CreateQstProcessControlTech.Anonymous();
            qstTech.TargetAnglePa = Angle.FromDegree(targetPa);
            qstTech.AlarmAnglePa = Angle.FromDegree(0);
            testEquipment.ProcessControlTech = qstTech;

            var validator = new ProcessControlConditionValidator();
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }

        [TestCase(-1, -1, -1, false)]
        [TestCase(0, 10, 10, true)]
        [TestCase(10, 10, 10, true)]
        [TestCase(9999, 99, 20, true)]
        [TestCase(10000, 10000, 21, false)]
        [TestCase(5, 6, 4, false)]
        [TestCase(5, 4, 6, false)]
        [TestCase(55, 10, 15, true)]
        public void ValidateReturnsCorrectValueForAlarmAnglePa(double alarmAngle, double angleForFurtherTurningPa, double targetAnglePa, bool result)
        {
            var testEquipment = CreateProcessControlCondition.Anonymous();
            var qstTech = CreateQstProcessControlTech.Anonymous();
            qstTech.AlarmAnglePa = Angle.FromDegree(alarmAngle);
            qstTech.AngleForFurtherTurningPa = Angle.FromDegree(angleForFurtherTurningPa);
            qstTech.TargetAnglePa = Angle.FromDegree(targetAnglePa);
            testEquipment.ProcessControlTech = qstTech;

            var validator = new ProcessControlConditionValidator();
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }

        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(50, true)]
        [TestCase(99, true)]
        [TestCase(100, false)]
        public void ValidateReturnsCorrectValueForAngleLimitMt(double angleLimitMt, bool result)
        {
            var testEquipment = CreateProcessControlCondition.Anonymous();
            var qstTech = CreateQstProcessControlTech.Anonymous();
            qstTech.AngleLimitMt = Angle.FromDegree(angleLimitMt);
            testEquipment.ProcessControlTech = qstTech;

            var validator = new ProcessControlConditionValidator();
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }

        [TestCase(-1, false)]
        [TestCase(0, true)]
        [TestCase(1000, true)]
        [TestCase(9999, true)]
        [TestCase(10000, false)]
        public void ValidateReturnsCorrectValueForAlarmAngleMt(double alarmAngleMt, bool result)
        {
            var testEquipment = CreateProcessControlCondition.Anonymous();
            var qstTech = CreateQstProcessControlTech.Anonymous();
            qstTech.AlarmAngleMt = Angle.FromDegree(alarmAngleMt);
            testEquipment.ProcessControlTech = qstTech;

            var validator = new ProcessControlConditionValidator();
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }

        [TestCase(0, false)]
        [TestCase(0.5, true)]
        [TestCase(100, true)]
        [TestCase(999, true)]
        [TestCase(1000, false)]
        public void ValidateReturnsCorrectValueForStartMeasurementPeak(double startMeasurementPeak, bool result)
        {
            var testEquipment = CreateProcessControlCondition.Anonymous();
            var qstTech = CreateQstProcessControlTech.Anonymous();
            qstTech.StartMeasurementPeak = Torque.FromNm(startMeasurementPeak);
            testEquipment.ProcessControlTech = qstTech;

            var validator = new ProcessControlConditionValidator();
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }
    }
}
