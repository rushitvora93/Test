using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Core.Diffs;
using Common.Types.Enums;
using Core.Diffs;
using Core.Entities;

namespace Core.UseCases.Communication
{
    public class TestEquipmentUseCaseHumbleAsyncRunner : ITestEquipmentUseCase
    {
        public TestEquipmentUseCaseHumbleAsyncRunner(ITestEquipmentUseCase real)
        {
            _real = real;
        }
        public void ShowTestEquipments(ITestEquipmentErrorGui loadingError)
        {
            Task.Run(() => _real.ShowTestEquipments(loadingError));
        }

        public void ShowTestEquipmentsForProcessControlAndRotatingTests(ITestEquipmentErrorGui loadingError, bool forProcessControl,
            bool forRotatingTests)
        {
            Task.Run(() => _real.ShowTestEquipmentsForProcessControlAndRotatingTests(loadingError,forProcessControl,forRotatingTests));
        }

        public void SaveTestEquipmentModel(TestEquipmentModelDiff diff, ITestEquipmentErrorGui errorHandler, ITestEquipmentSaveGuiShower saveGuiShower)
        {
            Task.Run(() => _real.SaveTestEquipmentModel(diff, errorHandler, saveGuiShower));
        }

        public void SaveTestEquipment(TestEquipmentDiff diff, ITestEquipmentErrorGui errorHandler,
            ITestEquipmentSaveGuiShower saveGuiShower)
        {
            Task.Run(() => _real.SaveTestEquipment(diff, errorHandler, saveGuiShower));
        }

        public void RemoveTestEquipment(TestEquipment testEquipment, ITestEquipmentErrorGui errorHandler)
        {
            Task.Run(() => _real.RemoveTestEquipment(testEquipment, errorHandler));
        }

        public void AddTestEquipment(TestEquipment testEquipment, ITestEquipmentErrorGui errorHandler)
        {
            Task.Run(() => _real.AddTestEquipment(testEquipment, errorHandler));
        }


        public bool IsInventoryNumberUnique(string inventoryNumber)
        {
            return _real.IsInventoryNumberUnique(inventoryNumber);
        }

        public bool IsSerialNumberUnique(string serialNumber)
        {
            return _real.IsSerialNumberUnique(serialNumber);
        }

        public void LoadTestEquipmentModels(ITestEquipmentErrorGui errorHandler)
        {
            Task.Run(() => _real.LoadTestEquipmentModels(errorHandler));
        }

        public List<TestEquipmentType> LoadAvailableTestEquipmentTypes()
        {
            return _real.LoadAvailableTestEquipmentTypes();
        }

        public void UpdateTestEquipment(TestEquipmentDiff diff, ITestEquipmentErrorGui errorHandler)
        {
            Task.Run(() => _real.UpdateTestEquipment(diff, errorHandler));
        }

        public void UpdateTestEquipmentModel(TestEquipmentModelDiff diff, ITestEquipmentErrorGui errorHandler)
        {
            Task.Run(() => _real.UpdateTestEquipmentModel(diff, errorHandler));
        }

        public ITestEquipmentUseCase _real;
    }
}