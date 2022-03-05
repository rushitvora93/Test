using System;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.Entities;
using TestHelper.Factories;

namespace Client.TestHelper.Factories
{
    public class CreateTestEquipmentModel : AbstractEntityFactory
    {
        public static TestEquipmentModel Parametrized(long id, string name, long type, string communicationFile,
            string driverProgram, Manufacturer manufacturer, string resultFilePath, string statusFilePath, bool transferAdapter, bool askForIdent, bool askForSign, bool canDeleteMeasurements,
            bool confirmMeasurements, int dataGateVersion, bool doLoseCheck, bool transferAttributes, bool transferCurves, bool transferLocPicts, bool transferNewLimits, bool transferTransducer,
            bool transferUser, bool useErrorCode, bool useForCtl, bool useForRot, bool canUseQstStandard)
        {
            return new TestEquipmentModel()
            {
                Id = new TestEquipmentModelId(id),
                TestEquipmentModelName = new TestEquipmentModelName(name),
                Type = (TestEquipmentType) type,
                CommunicationFilePath = new TestEquipmentSetupPath(communicationFile),
                DriverProgramPath = new TestEquipmentSetupPath(driverProgram),
                Manufacturer = manufacturer,
                ResultFilePath = new TestEquipmentSetupPath(resultFilePath),
                StatusFilePath = new TestEquipmentSetupPath(statusFilePath),
                TransferAdapter = transferAdapter,
                AskForIdent = askForIdent,
                AskForSign = askForSign,
                CanDeleteMeasurements = canDeleteMeasurements,
                ConfirmMeasurements = confirmMeasurements,
                DataGateVersion = new DataGateVersion(dataGateVersion),
                DoLoseCheck = doLoseCheck,
                TransferAttributes = transferAttributes,
                TransferCurves = transferCurves,
                TransferLocationPictures = transferLocPicts,
                TransferNewLimits = transferNewLimits,
                TransferTransducer = transferTransducer,
                TransferUser = transferUser,
                UseErrorCodes = useErrorCode,
                UseForCtl = useForCtl,
                UseForRot = useForRot,
                CanUseQstStandard = canUseQstStandard
            };
        }


        public static TestEquipmentModel RandomizedWithName(int seed, string name)
        {
            var model = Randomized(seed);
            model.TestEquipmentModelName = new TestEquipmentModelName(name);
            return model;
        }

        public static TestEquipmentModel RandomizedWithTestEquipmentType(int seed, TestEquipmentType type)
        {
            var model = Randomized(seed);
            model.Type = type;
            return model;
        }

        public static TestEquipmentModel Randomized(int seed)
        {
            var randomizer = new Random(seed);

            return Parametrized(
                randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                CreateManufacturer.Randomized(randomizer.Next()),
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 7),
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1);
        }

        public static TestEquipmentModel WithId(int id)
        {
            var model = CreateTestEquipmentModel.Randomized(12325);
            model.Id = new TestEquipmentModelId(id);
            return model;
        }
    }
}
