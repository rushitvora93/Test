using System;
using System.Collections.Generic;
using Client.TestHelper.Factories;
using Common.Types.Enums;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using TestHelper.Mock;

namespace TestHelper.Factories
{
    public class CreateTestEquipment : AbstractEntityFactory
    {
        public static TestEquipment Anonymous()
        {
            return new TestEquipment()
            {
                Id = new TestEquipmentId(99),
                SerialNumber = new TestEquipmentSerialNumber("abc"),
                InventoryNumber = new TestEquipmentInventoryNumber("abc"),
                CapacityMin = Torque.FromNm(5),
                CapacityMax = Torque.FromNm(6),
                TestEquipmentModel = CreateTestEquipmentModel.Randomized(43536)
            };
        }

        public static TestEquipment WithId(long id)
        {
            var result = Anonymous();
            result.Id = new TestEquipmentId(id);
            return result;
        }

        public static TestEquipment WithSerialNumber(string serialNumber)
        {
            var result = Anonymous();
            result.SerialNumber = new TestEquipmentSerialNumber(serialNumber);
            return result;
        }

        public static TestEquipment WithDriverProgramPath(string driverProgramPath)
        {
            var result = Anonymous();
            result.TestEquipmentModel.DriverProgramPath = new TestEquipmentSetupPath(driverProgramPath);
            return result;
        }

        public static TestEquipment WithCommunicationFilePath(string communicationFilePath)
        {
            var result = Anonymous();
            result.TestEquipmentModel.CommunicationFilePath = new TestEquipmentSetupPath(communicationFilePath);
            return result;
        }

        public static TestEquipment WithInventoryNumber(string inventoryNumber)
        {
            var result = Anonymous();
            result.InventoryNumber = new TestEquipmentInventoryNumber(inventoryNumber);
            return result;
        }

        public static TestEquipment WithIdSerialAndInventoryNumber(long id, string serialNumber, string inventoryNumber)
        {
            var result = Anonymous();
            result.Id = new TestEquipmentId(id);
            result.SerialNumber = new TestEquipmentSerialNumber(serialNumber);
            result.InventoryNumber = new TestEquipmentInventoryNumber(inventoryNumber);
            return result;
        }

        public static TestEquipment WithModel(TestEquipmentModel model)
        {
            var result = Anonymous();
            result.TestEquipmentModel = model;
            return result;
        }

        public static TestEquipment WithModelResultPath(string resultPath)
        {
            var result = Anonymous();
            result.TestEquipmentModel.ResultFilePath = new TestEquipmentSetupPath(resultPath);
            return result;
        }

        public static TestEquipment WithModelStatusPath(string statusPath)
        {
            var result = Anonymous();
            result.TestEquipmentModel.StatusFilePath = new TestEquipmentSetupPath(statusPath);
            return result;
        }

        public static TestEquipment ParametrizedForTransfer(
            string serialNumber,
            string driverProgramPath,
            string communicationFilePath)
        {
            var result = Anonymous();
            result.SerialNumber = new TestEquipmentSerialNumber(serialNumber);
            result.TestEquipmentModel.DriverProgramPath = new TestEquipmentSetupPath(driverProgramPath);
            result.TestEquipmentModel.CommunicationFilePath = new TestEquipmentSetupPath(communicationFilePath);
            return result;
        }

        public static TestEquipment Parametrized(
            string serialNumber,
            string inventoryNumber,
            TestEquipmentModel model,
            long id,
            bool transferTransducer, 
            bool transferAdapter, 
            TestEquipmentBehaviourTransferCurves transferCurves,
            TestEquipmentBehaviourAskForIdent askForIdent, 
            bool transferLocPicts, 
            bool transferUser, 
            bool transferNewLimits,
            bool useErrorCodes,
            TestEquipmentBehaviourConfirmMeasurements confirmMeasurements, 
            bool useForCtl, 
            bool useForRot, 
            bool askForSign, 
            bool doLoseCheck, 
            bool transferAttributes, 
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
                TestEquipmentModel = model,
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
                CapacityMax = Torque.FromNm(capacityMax),
                CapacityMin = Torque.FromNm(capacityMin),
                LastCalibration = lastCalibrationDate,
                CalibrationInterval = new Interval() { IntervalValue = intervalValue, Type = intervalType},
                CalibrationNorm = new CalibrationNorm(calibrationNorm),
                CanUseQstStandard = canUseQstStandard
            };
        }

        public static TestEquipment RandomizedWithId(int seed, long id)
        {
            var testEquipment = Randomized(seed);
            testEquipment.Id = new TestEquipmentId(id);
            return testEquipment;
        }

        public static TestEquipment RandomizedWithInventoryNumber(int seed, string inventoryNumber)
        {
            var testEquipment = Randomized(seed);
            testEquipment.InventoryNumber = new TestEquipmentInventoryNumber(inventoryNumber);
            return testEquipment;
        }

        public static TestEquipment RandomizedWithSerialNumber(int seed, string serialNumber)
        {
            var testEquipment = Randomized(seed);
            testEquipment.SerialNumber = new TestEquipmentSerialNumber(serialNumber);
            return testEquipment;
        }

        public static TestEquipment RandomizedWithCapacity(int seed, double min, double max)
        {
            var testEquipment = Randomized(seed);
            testEquipment.CapacityMin = Torque.FromNm(min);
            testEquipment.CapacityMax = Torque.FromNm(max);
            return testEquipment;
        }

        public static TestEquipment RandomizedWithCapacityAndType(int seed, double min, double max, TestEquipmentType type)
        {
            var testEquipment = Randomized(seed);
            testEquipment.TestEquipmentModel.Type = type;
            testEquipment.CapacityMin = Torque.FromNm(min);
            testEquipment.CapacityMax = Torque.FromNm(max);
            return testEquipment;
        }

        public static TestEquipment Randomized(int seed)
        {
            var randomizer = new Random(seed);

            var askForIdentList = new List<TestEquipmentBehaviourAskForIdent>()
            {
                TestEquipmentBehaviourAskForIdent.Never,
                TestEquipmentBehaviourAskForIdent.OnlyNio,
                TestEquipmentBehaviourAskForIdent.PerRoute,
                TestEquipmentBehaviourAskForIdent.PerTest,
                TestEquipmentBehaviourAskForIdent.PerVal
            };

            var confirmMeasurementList = new List<TestEquipmentBehaviourConfirmMeasurements>()
            {
                TestEquipmentBehaviourConfirmMeasurements.Never,
                TestEquipmentBehaviourConfirmMeasurements.Always,
                TestEquipmentBehaviourConfirmMeasurements.OnlyNio
            };

            var transferCurves = new List<TestEquipmentBehaviourTransferCurves>()
            {
                TestEquipmentBehaviourTransferCurves.Never,
                TestEquipmentBehaviourTransferCurves.Always,
                TestEquipmentBehaviourTransferCurves.OnlyNio
            };

            return Parametrized(CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                CreateTestEquipmentModel.Randomized(randomizer.Next()),
                randomizer.Next(),
                randomizer.Next(0,1) == 1,
                randomizer.Next(0, 1) == 1,
                transferCurves[randomizer.Next(0, transferCurves.Count - 1)],
                askForIdentList[randomizer.Next(0, askForIdentList.Count - 1)],
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                confirmMeasurementList[randomizer.Next(0, confirmMeasurementList.Count - 1)],
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                new Status(){ListId = new HelperTableEntityId(randomizer.Next()), Value = new StatusDescription(CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next())) },
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.Next(0,5),
                IntervalType.EveryXDays,
                DateTime.Now,
                CreateString.Randomized(randomizer.Next(0, 30), randomizer.Next()),
                randomizer.Next(0, 1) == 1);
        }
    }
}
