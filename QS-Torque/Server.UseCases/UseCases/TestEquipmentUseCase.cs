using System.Collections.Generic;
using System.Linq;
using Common.Types.Enums;
using Server.Core.Diffs;
using Server.Core.Entities;

namespace Server.UseCases.UseCases
{
    public interface ITestEquipmentUseCase
    {
        List<TestEquipment> GetTestEquipmentsByIds(List<TestEquipmentId> ids);
        void UpdateTestEquipmentsWithHistory(List<TestEquipmentDiff> getTestEquipmentDiffs, bool withModelUpdate);
        List<TestEquipmentModel> LoadTestEquipmentModels();
        void UpdateTestEquipmentModelsWithHistory(List<TestEquipmentModelDiff> diff);
        List<TestEquipment> InsertTestEquipmentsWithHistory(List<TestEquipmentDiff> diffs, bool returnList);
        bool IsInventoryNumberUnique(TestEquipmentInventoryNumber inventoryNumber);
        bool IsSerialNumberUnique(TestEquipmentSerialNumber serialNumber);
        bool IsTestEquipmentModelNameUnique(TestEquipmentModelName name);
        List<TestEquipmentType> LoadAvailableTestEquipmentTypes();
    }

    public interface ITestEquipmentDataAccess
    {
        List<TestEquipment> GetTestEquipmentsByIds(List<TestEquipmentId> ids);
        List<TestEquipment> InsertTestEquipmentsWithHistory(List<TestEquipmentDiff> diffs, bool returnList);
        void UpdateTestEquipmentsWithHistory(List<TestEquipmentDiff> testEquipmentDiffs);
        bool IsInventoryNumberUnique(TestEquipmentInventoryNumber inventoryNumber);
        bool IsSerialNumberUnique(TestEquipmentSerialNumber serialNumber);
        void Commit();
        List<TestEquipmentType> LoadAvailableTestEquipmentTypes();
    }

    public interface ITestEquipmentModelDataAccess
    {
        List<TestEquipmentModel> LoadTestEquipmentModels();
        void UpdateTestEquipmentModelsWithHistory(List<TestEquipmentModelDiff> testEquipmentModelDiffs);
        bool IsTestEquipmentModelNameUnique(TestEquipmentModelName name);
        void Commit();
    }

    public class TestEquipmentUseCase : ITestEquipmentUseCase
    {
        private readonly ITestEquipmentDataAccess _testEquipmentDataAccess;
        private readonly ITestEquipmentModelDataAccess _testEquipmentModelDataAccess;


        public TestEquipmentUseCase(ITestEquipmentDataAccess testEquipmentDataAccess, ITestEquipmentModelDataAccess testEquipmentModelDataAccess)
        {
            _testEquipmentDataAccess = testEquipmentDataAccess;
            _testEquipmentModelDataAccess = testEquipmentModelDataAccess;
        }

        public List<TestEquipment> GetTestEquipmentsByIds(List<TestEquipmentId> ids)
        {
            return _testEquipmentDataAccess.GetTestEquipmentsByIds(ids);
        }

        public void UpdateTestEquipmentsWithHistory(List<TestEquipmentDiff> testEquipmentDiffs, bool withModelUpdate)
        {
            _testEquipmentDataAccess.UpdateTestEquipmentsWithHistory(testEquipmentDiffs);

            if (withModelUpdate)
            {
                var diffsWithDifferentModels = testEquipmentDiffs.Where(x =>
                    !x.GetOldTestEquipment().TestEquipmentModel
                        .EqualsByContent(x.GetNewTestEquipment().TestEquipmentModel)).ToList();

                var modelDiffs = new List<TestEquipmentModelDiff>();

                foreach (var diff in diffsWithDifferentModels)
                {
                    var modelDiff = new Server.Core.Diffs.TestEquipmentModelDiff(diff.GetUser(), diff.GetComment(),
                        diff.GetOldTestEquipment().TestEquipmentModel, diff.GetNewTestEquipment().TestEquipmentModel);

                    modelDiffs.Add(modelDiff);
                }

                _testEquipmentModelDataAccess.UpdateTestEquipmentModelsWithHistory(modelDiffs);
            }
            

            _testEquipmentDataAccess.Commit();
        }

        public List<TestEquipmentModel> LoadTestEquipmentModels()
        {
            return _testEquipmentModelDataAccess.LoadTestEquipmentModels();
        }

        public void UpdateTestEquipmentModelsWithHistory(List<TestEquipmentModelDiff> diffs)
        {
            _testEquipmentModelDataAccess.UpdateTestEquipmentModelsWithHistory(diffs);
            _testEquipmentModelDataAccess.Commit();
        }

        public List<TestEquipment> InsertTestEquipmentsWithHistory(List<TestEquipmentDiff> diffs, bool returnList)
        {
            var result =_testEquipmentDataAccess.InsertTestEquipmentsWithHistory(diffs,returnList);
            _testEquipmentDataAccess.Commit();
            return result;
        }

        public bool IsInventoryNumberUnique(TestEquipmentInventoryNumber inventoryNumber)
        {
            return _testEquipmentDataAccess.IsInventoryNumberUnique(inventoryNumber);
        }

        public bool IsSerialNumberUnique(TestEquipmentSerialNumber serialNumber)
        {
            return _testEquipmentDataAccess.IsSerialNumberUnique(serialNumber);
        }

        public bool IsTestEquipmentModelNameUnique(TestEquipmentModelName name)
        {
            return _testEquipmentModelDataAccess.IsTestEquipmentModelNameUnique(name);
        }

        public List<TestEquipmentType> LoadAvailableTestEquipmentTypes()
        {
            return _testEquipmentDataAccess.LoadAvailableTestEquipmentTypes();
        }
    }
}
