using System;
using System.Collections.Generic;
using Common.Types.Enums;
using Core.Entities;
using Core.Enums;
using Server.Core.Entities;

namespace Server.TestHelper.Factories
{
    public class CreateTestEquipment
    {
        public static TestEquipment Anonymous()
        {
            return new TestEquipment()
            {
                Id = new TestEquipmentId(99)
            };
        }
        public static TestEquipment Parametrized(long id, string serialNumber, string inventoryNumber, TestEquipmentModel testEquipmentModel, bool alive,
            bool transferTransducer, bool transferAdapter, TestEquipmentBehaviourTransferCurves transferCurves, TestEquipmentBehaviourAskForIdent askForIdent, bool transferLocPicts, bool transferUser, bool transferNewLimits,
            bool useErrorCodes, TestEquipmentBehaviourConfirmMeasurements confirmMeasurements, bool useForCtl, bool useForRot, bool askForSign, bool doLoseCheck, bool transferAttributes, 
            bool canDeleteMeasurements,
            Status status,
            string version,
            double capacityMin,
            double capacityMax,
            long intervalValue,
            IntervalType intervalType,
            DateTime lastCalibrationDate,
            string calibrationNorm,
            bool canUseQstStandard)
        {
            return new TestEquipment()
            {
                Id = new TestEquipmentId(id),
                SerialNumber = new TestEquipmentSerialNumber(serialNumber),
                InventoryNumber = new TestEquipmentInventoryNumber(inventoryNumber),
                TestEquipmentModel = testEquipmentModel,
                Alive = alive,
                TransferTransducer = transferTransducer,
                TransferAdapter = transferAdapter,
                TransferCurves = transferCurves,
                AskForIdent = askForIdent,
                TransferLocationPictures = transferLocPicts,
                TransferUser = transferUser,
                TransferNewLimits = transferNewLimits,
                UseErrorCodes = useErrorCodes,
                ConfirmMeasurements = confirmMeasurements,
                UseForCtl = useForCtl,
                UseForRot = useForRot,
                AskForSign = askForSign,
                DoLoseCheck = doLoseCheck,
                TransferAttributes = transferAttributes,
                CanDeleteMeasurements = canDeleteMeasurements,
                Status = status,
                Version = new TestEquipmentVersion(version),
                CapacityMax = capacityMax,
                CapacityMin = capacityMin,
                LastCalibration = lastCalibrationDate,
                CalibrationInterval = new Interval() { IntervalValue = intervalValue, Type = intervalType },
                CalibrationNorm = new CalibrationNorm(calibrationNorm),
                CanUseQstStandard = canUseQstStandard
            };
        }

        public static TestEquipment Randomized(int seed)
        {
            var askForIdents = new List<TestEquipmentBehaviourAskForIdent>()
            {
                TestEquipmentBehaviourAskForIdent.Never,
                TestEquipmentBehaviourAskForIdent.OnlyNio,
                TestEquipmentBehaviourAskForIdent.PerRoute,
                TestEquipmentBehaviourAskForIdent.PerVal,
                TestEquipmentBehaviourAskForIdent.PerTest
            };

            var confirmMeasure = new List<TestEquipmentBehaviourConfirmMeasurements>()
            {
                TestEquipmentBehaviourConfirmMeasurements.Never,
                TestEquipmentBehaviourConfirmMeasurements.OnlyNio,
                TestEquipmentBehaviourConfirmMeasurements.Always
            };

            var transferCurves = new List<TestEquipmentBehaviourTransferCurves>()
            {
                TestEquipmentBehaviourTransferCurves.Never,
                TestEquipmentBehaviourTransferCurves.Always,
                TestEquipmentBehaviourTransferCurves.OnlyNio
            };


            var randomizer = new Random(seed);
            return Parametrized(randomizer.Next(),
                CreateString.Randomized((int) (randomizer.NextDouble() * 5), randomizer.Next()),
                CreateString.Randomized((int) (randomizer.NextDouble() * 5), randomizer.Next()),
                CreateTestEquipmentModel.Randomized(randomizer.Next()),
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                transferCurves[randomizer.Next(0, transferCurves.Count - 1)],
                askForIdents[randomizer.Next(0, askForIdents.Count - 1)],
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                confirmMeasure[randomizer.Next(0, confirmMeasure.Count - 1)],
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                new Status()
                {
                    Id = new StatusId(randomizer.Next()),
                    Value = new StatusDescription(CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()))
                },
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.Next(0, 5),
                IntervalType.EveryXDays,
                DateTime.Now,
                CreateString.Randomized(randomizer.Next(0, 30), randomizer.Next()), 
                randomizer.Next(0, 1) == 1);
        }
    }
}
