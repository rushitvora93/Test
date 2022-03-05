using System;
using System.Collections.Generic;
using Common.Types.Enums;
using Server.Core;
using Server.Core.Entities;

namespace Server.TestHelper.Factories
{
    public class CreateQstProcessControlTech
    {
        public static QstProcessControlTech Parameterized(long id, TestMethod testMethod, ManufacturerIds manufacturerId, Extension extension, long processControlConditionId,
            bool alive, double angleForFurtherTurningPa, double startAngleCountingPa, double alarmAngleMt, long angleLimitMt, double alarmAnglePa, double alarmTorqueMt,
            double alarmTorquePa, double minimumTorqueMt, double startAngleMt, double startMeasurementMt, double startMeasurementPa, double startMeasurementPeak, double targetAnglePa)
        {
            return new QstProcessControlTech()
            {
                Id = new ProcessControlTechId(id),
                TestMethod = testMethod,
                ManufacturerId = manufacturerId,
                Extension = extension,
                ProcessControlConditionId = new ProcessControlConditionId(processControlConditionId),
                Alive = alive,
                AngleForFurtherTurningPa = angleForFurtherTurningPa,
                StartAngleCountingPa = startAngleCountingPa,
                AlarmAngleMt = alarmAngleMt,
                AngleLimitMt = angleLimitMt,
                AlarmAnglePa = alarmAnglePa,
                AlarmTorqueMt = alarmTorqueMt,
                AlarmTorquePa = alarmTorquePa,
                MinimumTorqueMt = minimumTorqueMt,
                StartAngleMt = startAngleMt,
                StartMeasurementMt = startMeasurementMt,
                StartMeasurementPa = startMeasurementPa,
                StartMeasurementPeak = startMeasurementPeak,
                TargetAnglePa = targetAnglePa
            };
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
                randomizer.Next(0,1) == 1,
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
