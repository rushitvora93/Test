using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using Core.UseCases.Communication;
using Syncfusion.Windows.Shared;
using TestHelper.Factories;

namespace Client.TestHelper.Factories
{
    public class CreateProcessControlForTransfer : AbstractEntityFactory
    {
        public static ProcessControlForTransfer Parametrized(long locationId, long processControlId,
            long processControlTechId, TestMethod testMethod, string locationDescription, int sampleNumber,
            DateTime lastTestDate,
            double minimumTorque, double setPointTorque, double maximumTorque, IntervalType intervalType,
            long intervalValue,
            string locationNumber, DateTime nextTestDate)
        {
            return new ProcessControlForTransfer()
            {
                LocationId = new LocationId(locationId),
                TestMethod = testMethod,
                ProcessControlConditionId = new ProcessControlConditionId(processControlId),
                TestInterval = new Interval() {Type = intervalType, IntervalValue = intervalValue},
                ProcessControlTechId = new ProcessControlTechId(processControlTechId),
                LocationDescription = new LocationDescription(locationDescription),
                SampleNumber = sampleNumber,
                LastTestDate = lastTestDate,
                MinimumTorque = Torque.FromNm(minimumTorque),
                SetPointTorque = Torque.FromNm(setPointTorque),
                MaximumTorque = Torque.FromNm(maximumTorque),
                NextTestDateShift = Shift.SecondShiftOfDay,
                LocationNumber = new LocationNumber(locationNumber),
                NextTestDate = nextTestDate
            };
        }

        public static ProcessControlForTransfer RandomizedWithMinimumMaximumAndSetPoint(int seed, double min, double max, double setpoint)
        {
            var data = Randomized(seed);
            data.MinimumTorque = Torque.FromNm(min);
            data.MaximumTorque = Torque.FromNm(max);
            data.SetPointTorque = Torque.FromNm(setpoint);
            return data;
        }

        public static ProcessControlForTransfer Randomized(int seed)
        {
            var randomizer = new Random(seed);
            var testMethods = new List<TestMethod>()
            {
                TestMethod.QST_MT, 
                TestMethod.QST_PA, 
                TestMethod.QST_PEAK
            };

            var intervalType = new List<IntervalType>()
            {
                IntervalType.EveryXDays, 
                IntervalType.EveryXShifts, 
                IntervalType.XTimesADay,
                IntervalType.XTimesAShift, 
                IntervalType.XTimesAWeek,
                IntervalType.XTimesAMonth,
                IntervalType.XTimesAYear
            };

            return Parametrized(
                randomizer.Next(),
                randomizer.Next(),
                randomizer.Next(),
                testMethods[randomizer.Next(0, testMethods.Count - 1)],
                CreateString.Randomized(randomizer.Next(0, 10),
                randomizer.Next()), randomizer.Next(),
                DateTime.Now, 
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                intervalType[randomizer.Next(0, intervalType.Count - 1)],
                randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                DateTime.Now);
        }
    }
}
