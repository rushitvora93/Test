using System;
using System.Collections.Generic;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.Entities;
using Core.PhysicalValueTypes;

namespace Client.TestHelper.Factories
{
    public class CreateQstProcessControlTech
    {
        public static QstProcessControlTech Parameterized(long id, TestMethod testMethod, ManufacturerIds manufacturerId, Extension extension, long processControlConditionId,
            double angleForFurtherTurningPa, double startAngleCountingPa, double alarmAngleMt, long angleLimitMt, double alarmAnglePa, double alarmTorqueMt,
            double alarmTorquePa, double minimumTorqueMt, double startAngleMt, double startMeasurementMt, double startMeasurementPa, double startMeasurementPeak, double targetAnglePa)
        {
            return new QstProcessControlTech()
            {
                Id = new ProcessControlTechId(id),
                TestMethod = testMethod,
                ManufacturerId = manufacturerId,
                Extension = extension,
                ProcessControlConditionId = new ProcessControlConditionId(processControlConditionId),
                AngleForFurtherTurningPa = Angle.FromDegree(angleForFurtherTurningPa),
                StartAngleCountingPa = Torque.FromNm(startAngleCountingPa),
                AlarmAngleMt = Angle.FromDegree(alarmAngleMt),
                AngleLimitMt = Angle.FromDegree(angleLimitMt),
                AlarmAnglePa = Angle.FromDegree(alarmAnglePa),
                AlarmTorqueMt = Torque.FromNm(alarmTorqueMt),
                AlarmTorquePa = Torque.FromNm(alarmTorquePa),
                MinimumTorqueMt = Torque.FromNm(minimumTorqueMt),
                StartAngleMt = Torque.FromNm(startAngleMt),
                StartMeasurementMt = Torque.FromNm(startMeasurementMt),
                StartMeasurementPa = Torque.FromNm(startMeasurementPa),
                StartMeasurementPeak = Torque.FromNm(startMeasurementPeak),
                TargetAnglePa = Angle.FromDegree(targetAnglePa)
            };
        }

        public static QstProcessControlTech Anonymous()
        {
            return Parameterized(1, TestMethod.QST_MT, ManufacturerIds.ID_QST, CreateExtension.Randomized(2345), 1,
                1, 1, 1, 1, 1,0, 
                1, 1, 1, 1, 1, 1, 1);
        }

        public static QstProcessControlTech Randomized(int seed)
        {
            var randomizer = new Random(seed);
            var testMethods = new List<TestMethod>()
            {
                TestMethod.QST_MT,
                TestMethod.QST_PA,
                TestMethod.QST_PEAK,
            };

            return Parameterized(
                randomizer.Next(),
                testMethods[randomizer.Next(0, testMethods.Count - 1)],
                ManufacturerIds.ID_QST,
                CreateExtension.Randomized(randomizer.Next()),
                randomizer.Next(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.Next(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble());
        }
    }
}
