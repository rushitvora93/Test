using System;
using System.Collections.Generic;
using System.Linq;
using Client.Core.Diffs;
using Common.Types.Enums;
using Core.Diffs;
using Core.Entities;
using log4net;

namespace Core.UseCases.Communication
{
    public interface ITestEquipmentGui
    {
        void UpdateTestEquipment(TestEquipment testEquipment);
        void ShowTestEquipments(List<TestEquipmentModel> testEquipmentModels);
        void UpdateTestEquipmentModel(TestEquipmentModel testEquipmentModel);
        void RemoveTestEquipment(TestEquipment testEquipment);
        void AddTestEquipment(TestEquipment testEquipment);
        void LoadTestEquipmentModels(List<TestEquipmentModel> testEquipmentModels);
    }

    public interface ITestEquipmentErrorGui
    {
        void ShowProblemSavingTestEquipment();
        void ShowProblemRemoveTestEquipment();
        void ShowErrorMessageLoadingTestEquipments();
        void ShowErrorMessageLoadingTestEquipmentModels();
        void TestEquipmentAlreadyExists();
        void TestEquipmentModelAlreadyExists();
    }

    public interface ITestEquipmentDataAccess
    {
        List<TestEquipmentModel> LoadTestEquipmentModels();
        List<TestEquipment> GetTestEquipmentsByIds(List<TestEquipmentId> ids);
        void SaveTestEquipment(TestEquipmentDiff testEquipmentDiff);
        void SaveTestEquipmentModel(TestEquipmentModelDiff testEquipmentModelDiff);
        void RemoveTestEquipment(TestEquipment testEquipment, User user);
        TestEquipment AddTestEquipment(TestEquipment testEquipment, User byUser);
        bool IsInventoryNumberUnique(TestEquipmentInventoryNumber inventoryNumber);
        bool IsSerialNumberUnique(TestEquipmentSerialNumber serialNumber);
        bool IsTestEquipmentModelNameUnique(TestEquipmentModelName name);
        List<TestEquipmentType> LoadAvailableTestEquipmentTypes();
    }

    public interface ITestEquipmentSaveGuiShower
    {
        void SaveTestEquipmentModel(TestEquipmentModelDiff diff, Action saveAction);
        void SaveTestEquipment(TestEquipmentDiff diff, Action saveAction);
    }

    public interface ITestEquipmentUseCase
    {
        void ShowTestEquipments(ITestEquipmentErrorGui loadingError);
        void ShowTestEquipmentsForProcessControlAndRotatingTests(ITestEquipmentErrorGui loadingError,
            bool forProcessControl, bool forRotatingTests);
        void SaveTestEquipmentModel(TestEquipmentModelDiff diff, ITestEquipmentErrorGui errorHandler, ITestEquipmentSaveGuiShower saveGuiShower);
        void SaveTestEquipment(TestEquipmentDiff diff, ITestEquipmentErrorGui errorHandler, ITestEquipmentSaveGuiShower saveGuiShower);
        void UpdateTestEquipment(TestEquipmentDiff diff, ITestEquipmentErrorGui errorHandler);
        void UpdateTestEquipmentModel(TestEquipmentModelDiff diff, ITestEquipmentErrorGui errorHandler);
        void RemoveTestEquipment(TestEquipment testEquipment, ITestEquipmentErrorGui errorHandler);
        void AddTestEquipment(TestEquipment testEquipment, ITestEquipmentErrorGui errorHandler);
        bool IsInventoryNumberUnique(string inventoryNumber);
        bool IsSerialNumberUnique(string serialNumber);
        void LoadTestEquipmentModels(ITestEquipmentErrorGui errorHandler);
        List<TestEquipmentType> LoadAvailableTestEquipmentTypes();
    }

    public class TestEquipmentUseCase: ITestEquipmentUseCase
    {
        public TestEquipmentUseCase(
            ITestEquipmentDataAccess dataAccess,
            ITestEquipmentGui gui,
            ISessionInformationUserGetter userGetter,
            INotificationManager notificationManager)
        {
            _dataAccess = dataAccess;
            _gui = gui;
            _userGetter = userGetter;
            _notificationManager = notificationManager;
        }

        public void ShowTestEquipments(ITestEquipmentErrorGui loadingError)
        {
            try
            {
                Log.Info("ShowTestEquipments started");
                var testEquipmentModels = _dataAccess.LoadTestEquipmentModels();
                _gui.ShowTestEquipments(testEquipmentModels);
            }
            catch (Exception exception)
            {
                Log.Error("Error while loading TestEquipments failed with Exception", exception);
                loadingError.ShowErrorMessageLoadingTestEquipments();
            }
            Log.Info("ShowTestEquipments ended");
        }

        public void ShowTestEquipmentsForProcessControlAndRotatingTests(ITestEquipmentErrorGui loadingError, bool forProcessControl, bool forRotatingTests)
        {
            try
            {
                Log.Info("ShowTestEquipmentsForProcessControlAndRotatingTests started");
                var testEquipmentModels = _dataAccess.LoadTestEquipmentModels();

                var filteredTestEquipmentModels = testEquipmentModels
                    .Where(x => forProcessControl && x.UseForCtl && x.TestEquipments.Any(y => y.UseForCtl) ||
                                forRotatingTests && x.UseForRot && x.TestEquipments.Any(y => y.UseForRot))
                    .ToList();

                foreach (var model in filteredTestEquipmentModels)
                {
                    model.TestEquipments = model.TestEquipments.Where(x => forProcessControl && x.UseForCtl || forRotatingTests && x.UseForRot).ToList();
                }
                _gui.ShowTestEquipments(filteredTestEquipmentModels);
            }
            catch (Exception exception)
            {
                Log.Error("Error while loading TestEquipments for process control and rotating tests failed with Exception", exception);
                loadingError.ShowErrorMessageLoadingTestEquipments();
            }
            Log.Info("ShowTestEquipmentsForProcessControlAndRotatingTests ended");
        }

        public void SaveTestEquipmentModel(TestEquipmentModelDiff diff, ITestEquipmentErrorGui errorHandler, ITestEquipmentSaveGuiShower saveGuiShower)
        {
            saveGuiShower.SaveTestEquipmentModel(diff, () =>
            {
                SaveTestEquipmentModel(diff, errorHandler);
            });
        }

        public void UpdateTestEquipmentModel(TestEquipmentModelDiff diff, ITestEquipmentErrorGui errorHandler)
        {
            SaveTestEquipmentModel(diff, errorHandler);
        }

        private void SaveTestEquipmentModel(TestEquipmentModelDiff diff, ITestEquipmentErrorGui errorHandler)
        {
            try
            {
                Log.Info("SaveTestEquipmentModel started");

                if (!diff.OldTestEquipmentModel.TestEquipmentModelName.Equals(diff.NewTestEquipmentModel.TestEquipmentModelName) &&
                    !_dataAccess.IsTestEquipmentModelNameUnique(diff.NewTestEquipmentModel.TestEquipmentModelName))
                {
                    errorHandler.TestEquipmentModelAlreadyExists();
                    return;
                }

                diff.User = _userGetter?.GetCurrentUser();
                _dataAccess.SaveTestEquipmentModel(diff);
                _gui.UpdateTestEquipmentModel(diff.NewTestEquipmentModel);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception e)
            {
                errorHandler.ShowProblemSavingTestEquipment();
                Log.Error("Error in SaveTestEquipmentModel", e);
            }
        }

        public void AddTestEquipment(TestEquipment testEquipment, ITestEquipmentErrorGui errorHandler)
        {
            try
            {
                Log.Info("AddTestEquipment started");
                var addedTestEquipment = _dataAccess.AddTestEquipment(testEquipment, _userGetter?.GetCurrentUser());
                _gui.AddTestEquipment(addedTestEquipment);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception exception)
            {
                Log.Error("Error in AddTestEquipment", exception);
                errorHandler.ShowProblemSavingTestEquipment();
            }
            Log.Info("AddTestEquipment ended");
        }

        public bool IsInventoryNumberUnique(string inventoryNumber)
        {
            return _dataAccess.IsInventoryNumberUnique(new TestEquipmentInventoryNumber(inventoryNumber));
        }

        public bool IsSerialNumberUnique(string serialNumber)
        {
            return _dataAccess.IsSerialNumberUnique(new TestEquipmentSerialNumber(serialNumber));
        }

        public void LoadTestEquipmentModels(ITestEquipmentErrorGui errorHandler)
        {
            try
            {
                _gui.LoadTestEquipmentModels(_dataAccess.LoadTestEquipmentModels());
            }
            catch (Exception e)
            {
                errorHandler.ShowErrorMessageLoadingTestEquipmentModels();
                Log.Error("Error in LoadTestEquipmentModels", e);
            }
        }

        public List<TestEquipmentType> LoadAvailableTestEquipmentTypes()
        {
            return _dataAccess.LoadAvailableTestEquipmentTypes();
        }

        public void SaveTestEquipment(TestEquipmentDiff diff, ITestEquipmentErrorGui errorHandler,
            ITestEquipmentSaveGuiShower saveGuiShower)
        {
            saveGuiShower.SaveTestEquipment(diff, () =>
            {
                SaveTestEquipment(diff, errorHandler);
            });
        }

        public void UpdateTestEquipment(TestEquipmentDiff diff, ITestEquipmentErrorGui errorHandler)
        {
            SaveTestEquipment(diff, errorHandler);
        }

        private void SaveTestEquipment(TestEquipmentDiff diff, ITestEquipmentErrorGui errorHandler)
        {
            try
            {
                Log.Info("SaveTestEquipment started");

                if ((!diff.OldTestEquipment.InventoryNumber.Equals(diff.NewTestEquipment.InventoryNumber) && !_dataAccess.IsInventoryNumberUnique(diff.NewTestEquipment.InventoryNumber))
                    || (!diff.OldTestEquipment.SerialNumber.Equals(diff.NewTestEquipment.SerialNumber) && !_dataAccess.IsSerialNumberUnique(diff.NewTestEquipment.SerialNumber)))
                {
                    errorHandler.TestEquipmentAlreadyExists();
                    return;
                }

                diff.User = _userGetter?.GetCurrentUser();
                _dataAccess.SaveTestEquipment(diff);
                _gui.UpdateTestEquipment(diff.NewTestEquipment);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception e)
            {
                errorHandler.ShowProblemSavingTestEquipment();
                Log.Error("Error in SaveTestEquipment", e);
            }
        }

        public void RemoveTestEquipment(TestEquipment testEquipment, ITestEquipmentErrorGui errorHandler)
        {
            try
            {
                Log.Info("RemoveTestEquipment started");
                
                _dataAccess.RemoveTestEquipment(testEquipment, _userGetter?.GetCurrentUser());
                _gui.RemoveTestEquipment(testEquipment);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception e)
            {
                errorHandler.ShowProblemRemoveTestEquipment();
                Log.Error("Error in RemoveTestEquipment", e);
            }
        }

        private readonly ITestEquipmentDataAccess _dataAccess;
        private readonly ITestEquipmentGui _gui;
        private readonly ISessionInformationUserGetter _userGetter;
        private readonly INotificationManager _notificationManager;
        private static readonly ILog Log = LogManager.GetLogger(typeof(TestEquipmentUseCase));
    }
}
