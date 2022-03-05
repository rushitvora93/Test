using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using TestEquipmentService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class TestEquipmentClient : ITestEquipmentClient
    {
        private readonly TestEquipmentService.TestEquipmentService.TestEquipmentServiceClient _testEquipmentClient;

        public TestEquipmentClient(TestEquipmentService.TestEquipmentService.TestEquipmentServiceClient testEquipmentClient)
        {
            _testEquipmentClient = testEquipmentClient;
        }

        public ListOfTestEquipmentModel LoadTestEquipmentModels()
        {
            return _testEquipmentClient.LoadTestEquipmentModels(new NoParams());
        }


        public ListOfTestEquipment GetTestEquipmentsByIds(ListOfLongs ids)
        {
            return _testEquipmentClient.GetTestEquipmentsByIds(ids);
        }

        public ListOfTestEquipment InsertTestEquipmentsWithHistory(InsertTestEquipmentsWithHistoryRequest request)
        {
            return _testEquipmentClient.InsertTestEquipmentsWithHistory(request);
        }

        public void UpdateTestEquipmentsWithHistory(UpdateTestEquipmentsWithHistoryRequest request)
        {
            _testEquipmentClient.UpdateTestEquipmentsWithHistory(request);
        }

        public void UpdateTestEquipmentModelsWithHistory(UpdateTestEquipmentModelsWithHistoryRequest request)
        {
            _testEquipmentClient.UpdateTestEquipmentModelsWithHistory(request);
        }

        public Bool IsTestEquipmentInventoryNumberUnique(String inventoryNumber)
        {
            return _testEquipmentClient.IsTestEquipmentInventoryNumberUnique(inventoryNumber);
        }

        public Bool IsTestEquipmentSerialNumberUnique(String serialNumber)
        {
            return _testEquipmentClient.IsTestEquipmentSerialNumberUnique(serialNumber);
        }

        public Bool IsTestEquipmentModelNameUnique(String name)
        {
            return _testEquipmentClient.IsTestEquipmentModelNameUnique(name);
        }

        public ListOfLongs LoadAvailableTestEquipmentTypes()
        {
            return _testEquipmentClient.LoadAvailableTestEquipmentTypes(new NoParams());
        }
    }
}
