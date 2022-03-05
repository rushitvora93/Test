using System;
using System.Collections.Generic;
using System.Linq;
using Client.Core.Diffs;
using Client.TestHelper.Factories;
using Common.Types.Enums;
using Core.Diffs;
using Core.Entities;
using Core.UseCases.Communication;
using NUnit.Framework;
using TestHelper.Factories;
using TestHelper.Mock;

namespace Core.Test.UseCases.Communication
{
    class TestEquipmentUseCaseTest
    {
        [Test]
        public void ShowTestEquipmentsCallsDataAccess()
        {
            var environment = new Environment();            
            environment.UseCase.ShowTestEquipments(null);
            Assert.IsTrue(environment.Mock.Data.LoadTestEquipmentModelsCalled);
        }

        [Test]
        public void ShowTestEquipmentsCallsGui()
        {
            var environment = new Environment();
            var testEquipments = new List<TestEquipmentModel>();
            environment.Mock.Data.LoadTestEquipmentModelsReturnValue = testEquipments;

            environment.UseCase.ShowTestEquipments(null);
            Assert.AreSame(testEquipments, environment.Mock.Gui.ShowTestEquipmentsParameter);
        }

        [Test]
        public void ShowTestEquipmentsHasErrorCallsShowLoadingError()
        {
            var environment = new Environment();
            var errorMock = new TestEquipmentErrorMock();
            environment.Mock.Gui.ShowTestEquipmentsError = true;
            environment.UseCase.ShowTestEquipments(errorMock);
            Assert.IsTrue(errorMock.ShowErrorMessageLoadingTestEquipmentsCalled);
        }

        [Test]
        public void ShowTestEquipmentsForProcessControlAndRotatingTestsCallsDataAccess()
        {
            var environment = new Environment();
            environment.Mock.Data.LoadTestEquipmentModelsReturnValue = new List<TestEquipmentModel>();
            environment.UseCase.ShowTestEquipmentsForProcessControlAndRotatingTests(null, true, true);
            Assert.IsTrue(environment.Mock.Data.LoadTestEquipmentModelsCalled);
        }

        [Test]
        public void ShowTestEquipmentsForProcessControlAndRotatingTestsCallsGui()
        {
            var environment = new Environment();
            var ctlTestEquipment1 = new TestEquipment() { UseForCtl = true };
            var ctlTestEquipment2 = new TestEquipment() { UseForRot = true };
            var rotTestEquipment1 = new TestEquipment() { UseForCtl = true };
            var rotTestEquipment2 = new TestEquipment() { UseForRot = true };

            var ctlModel = new TestEquipmentModel()
            {
                UseForCtl = true,
                UseForRot = false,
                TestEquipments = new List<TestEquipment>()
                {
                    ctlTestEquipment1,
                    ctlTestEquipment2
                }
            };

            var rotModel = new TestEquipmentModel()
            {
                UseForCtl = false,
                UseForRot = true,
                TestEquipments = new List<TestEquipment>()
                {
                    rotTestEquipment1,
                    rotTestEquipment2
                }
            };

            var models = new List<TestEquipmentModel>()
            {
                ctlModel,
                rotModel
            };

            environment.Mock.Data.LoadTestEquipmentModelsReturnValue = models;

            environment.UseCase.ShowTestEquipmentsForProcessControlAndRotatingTests(null, true, false);
            Assert.AreEqual(1, environment.Mock.Gui.ShowTestEquipmentsParameter.Count);
            Assert.AreSame(ctlModel, environment.Mock.Gui.ShowTestEquipmentsParameter.First());
            Assert.AreEqual(1, environment.Mock.Gui.ShowTestEquipmentsParameter.First().TestEquipments.Count);
            Assert.AreSame(ctlTestEquipment1, environment.Mock.Gui.ShowTestEquipmentsParameter.First().TestEquipments.First());

            environment.Mock.Gui.ShowTestEquipmentsParameter = new List<TestEquipmentModel>();
            environment.UseCase.ShowTestEquipmentsForProcessControlAndRotatingTests(null, false, true);
            Assert.AreEqual(1, environment.Mock.Gui.ShowTestEquipmentsParameter.Count);
            Assert.AreSame(rotModel, environment.Mock.Gui.ShowTestEquipmentsParameter.First());
            Assert.AreEqual(1, environment.Mock.Gui.ShowTestEquipmentsParameter.First().TestEquipments.Count);
            Assert.AreSame(rotTestEquipment2, environment.Mock.Gui.ShowTestEquipmentsParameter.First().TestEquipments.First());
        }

        [Test]
        public void ShowTestEquipmentsForProcessControlAndRotatingTestsCallsShowLoadingError()
        {
            var environment = new Environment();
            var errorMock = new TestEquipmentErrorMock();
            environment.Mock.Gui.ShowTestEquipmentsError = true;
            environment.UseCase.ShowTestEquipmentsForProcessControlAndRotatingTests(errorMock, true, true);
            Assert.IsTrue(errorMock.ShowErrorMessageLoadingTestEquipmentsCalled);
        }

        [Test]
        public void SaveTestEquipmentCallsDataAccess()
        {
            var environment = new Environment();
            var diff = new TestEquipmentDiff(new TestEquipment(), new TestEquipment(), CreateUser.Anonymous());
            environment.UseCase.SaveTestEquipment(diff, null, environment.Mock.SaveGuiShower);

            Assert.AreSame(diff, environment.Mock.Data.SaveTestEquipmentParameterTestEquipmentDiff);
        }

        [Test]
        public void UpdateTestEquipmentCallsDataAccess()
        {
            var environment = new Environment();
            var diff = new TestEquipmentDiff(new TestEquipment(), new TestEquipment(), CreateUser.Anonymous());
            environment.UseCase.UpdateTestEquipment(diff, null);

            Assert.AreSame(diff, environment.Mock.Data.SaveTestEquipmentParameterTestEquipmentDiff);
        }

        [Test]
        public void SaveTestEquipmentModelCallsDataAccess()
        {
            var environment = new Environment();
            var diff = new TestEquipmentModelDiff(new TestEquipmentModel(), new TestEquipmentModel(), CreateUser.Anonymous());
            environment.UseCase.SaveTestEquipmentModel(diff, null, environment.Mock.SaveGuiShower);

            Assert.AreSame(diff, environment.Mock.Data.SaveTestEquipmentModelParameterDiff);
        }

        [Test]
        public void UpdateTestEquipmentModelCallsDataAccess()
        {
            var environment = new Environment();
            var diff = new TestEquipmentModelDiff(new TestEquipmentModel(), new TestEquipmentModel(), CreateUser.Anonymous());
            environment.UseCase.UpdateTestEquipmentModel(diff, null);

            Assert.AreSame(diff, environment.Mock.Data.SaveTestEquipmentModelParameterDiff);
        }

        [Test]
        public void SaveTestEquipmentSetsUser()
        {
            var environment = new Environment();
            var user = CreateUser.Anonymous();
            environment.Mock.UserGetter.NextReturnedUser = user;
            var diff = new TestEquipmentDiff(new TestEquipment(), new TestEquipment(), null);
            environment.UseCase.SaveTestEquipment(diff, null, environment.Mock.SaveGuiShower);

            Assert.AreSame(user, environment.Mock.Data.SaveTestEquipmentParameterTestEquipmentDiff.User);
        }

        [Test]
        public void UpdateTestEquipmentSetsUser()
        {
            var environment = new Environment();
            var user = CreateUser.Anonymous();
            environment.Mock.UserGetter.NextReturnedUser = user;
            var diff = new TestEquipmentDiff(new TestEquipment(), new TestEquipment(), null);
            environment.UseCase.UpdateTestEquipment(diff, null);

            Assert.AreSame(user, environment.Mock.Data.SaveTestEquipmentParameterTestEquipmentDiff.User);
        }

        [Test]
        public void SaveTestEquipmentModelSetsUser()
        {
            var environment = new Environment();
            var user = CreateUser.Anonymous();
            environment.Mock.UserGetter.NextReturnedUser = user;
            var diff = new TestEquipmentModelDiff(new TestEquipmentModel(), new TestEquipmentModel(), null);
            environment.UseCase.SaveTestEquipmentModel(diff, null, environment.Mock.SaveGuiShower);

            Assert.AreSame(user, environment.Mock.Data.SaveTestEquipmentModelParameterDiff.User);
        }

        [Test]
        public void UpdateTestEquipmentModelSetsUser()
        {
            var environment = new Environment();
            var user = CreateUser.Anonymous();
            environment.Mock.UserGetter.NextReturnedUser = user;
            var diff = new TestEquipmentModelDiff(new TestEquipmentModel(), new TestEquipmentModel(), null);
            environment.UseCase.UpdateTestEquipmentModel(diff, null);

            Assert.AreSame(user, environment.Mock.Data.SaveTestEquipmentModelParameterDiff.User);
        }

        [Test]
        public void SaveTestEquipmentCallsSuccessNotification()
        {
            var environment = new Environment();
            var diff = new TestEquipmentDiff(new TestEquipment(), new TestEquipment(), null);
            environment.UseCase.SaveTestEquipment(diff, null, environment.Mock.SaveGuiShower);

            Assert.IsTrue(environment.Mock.notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void UpdateTestEquipmentCallsSuccessNotification()
        {
            var environment = new Environment();
            var diff = new TestEquipmentDiff(new TestEquipment(), new TestEquipment(), null);
            environment.UseCase.UpdateTestEquipment(diff, null);

            Assert.IsTrue(environment.Mock.notificationManager.SendSuccessNotificationCalled);
        }


        [Test]
        public void SaveTestEquipmentModelCallsSuccessNotification()
        {
            var environment = new Environment();
            var diff = new TestEquipmentModelDiff(new TestEquipmentModel(), new TestEquipmentModel(), null);
            environment.UseCase.SaveTestEquipmentModel(diff, null, environment.Mock.SaveGuiShower);

            Assert.IsTrue( environment.Mock.notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void UpdateTestEquipmentModelCallsSuccessNotification()
        {
            var environment = new Environment();
            var diff = new TestEquipmentModelDiff(new TestEquipmentModel(), new TestEquipmentModel(), null);
            environment.UseCase.UpdateTestEquipmentModel(diff, null);

            Assert.IsTrue(environment.Mock.notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void SaveTestEquipmentCallsGui()
        {
            var environment = new Environment();
            var diff = new TestEquipmentDiff(new TestEquipment(), new TestEquipment(), null);
            environment.UseCase.SaveTestEquipment(diff, null, environment.Mock.SaveGuiShower);

            Assert.AreSame(diff.NewTestEquipment, environment.Mock.Gui.UpdateTestEquipmentParameter);
        }

        [Test]
        public void UpdateTestEquipmentCallsGui()
        {
            var environment = new Environment();
            var diff = new TestEquipmentDiff(new TestEquipment(), new TestEquipment(), null);
            environment.UseCase.UpdateTestEquipment(diff, null);

            Assert.AreSame(diff.NewTestEquipment, environment.Mock.Gui.UpdateTestEquipmentParameter);
        }

        [Test]
        public void SaveTestEquipmentModelCallsGui()
        {
            var environment = new Environment();
            var diff = new TestEquipmentModelDiff(new TestEquipmentModel(), new TestEquipmentModel(), null);
            environment.UseCase.SaveTestEquipmentModel(diff, null, environment.Mock.SaveGuiShower);

            Assert.AreSame(diff.NewTestEquipmentModel, environment.Mock.Gui.UpdateTestEquipmentModelParameter);
        }

        [Test]
        public void UpdateTestEquipmentModelCallsGui()
        {
            var environment = new Environment();
            var diff = new TestEquipmentModelDiff(new TestEquipmentModel(), new TestEquipmentModel(), null);
            environment.UseCase.UpdateTestEquipmentModel(diff, null);

            Assert.AreSame(diff.NewTestEquipmentModel, environment.Mock.Gui.UpdateTestEquipmentModelParameter);
        }

        
        [Test]
        public void SaveTestEquipmentWithErrorCallsShowSavingError()
        {
            var environment = new Environment();
            environment.Mock.Data.SaveTestEquipmentThrowsError = true;

            var diff = new TestEquipmentDiff(new TestEquipment(), new TestEquipment(), null);
            environment.UseCase.SaveTestEquipment(diff, environment.Mock.Error, environment.Mock.SaveGuiShower);

            Assert.IsTrue(environment.Mock.Error.ShowProblemSavingTestEquipmentWasCalled);
        }

        [Test]
        public void UpdateTestEquipmentWithErrorCallsShowSavingError()
        {
            var environment = new Environment();
            environment.Mock.Data.SaveTestEquipmentThrowsError = true;

            var diff = new TestEquipmentDiff(new TestEquipment(), new TestEquipment(), null);
            environment.UseCase.UpdateTestEquipment(diff, environment.Mock.Error);

            Assert.IsTrue(environment.Mock.Error.ShowProblemSavingTestEquipmentWasCalled);
        }

        [Test]
        public void SaveTestEquipmentModelWithErrorCallsShowSavingError()
        {
            var environment = new Environment();
            environment.Mock.Data.SaveTestEquipmentThrowsError = true;

            var diff = new TestEquipmentModelDiff(new TestEquipmentModel(), new TestEquipmentModel(), null);
            environment.UseCase.SaveTestEquipmentModel(diff, environment.Mock.Error, environment.Mock.SaveGuiShower);

            Assert.IsTrue(environment.Mock.Error.ShowProblemSavingTestEquipmentWasCalled);
        }

        [Test]
        public void UpdateTestEquipmentModelWithErrorCallsShowSavingError()
        {
            var environment = new Environment();
            environment.Mock.Data.SaveTestEquipmentThrowsError = true;

            var diff = new TestEquipmentModelDiff(new TestEquipmentModel(), new TestEquipmentModel(), null);
            environment.UseCase.UpdateTestEquipmentModel(diff, environment.Mock.Error);

            Assert.IsTrue(environment.Mock.Error.ShowProblemSavingTestEquipmentWasCalled);
        }

        [Test]
        public void SaveTestEquipmentCallsGuiShowerSaveTestEquipment()
        {
            var environment = new Environment();

            var diff = new TestEquipmentDiff(new TestEquipment(), new TestEquipment(), null);
            environment.UseCase.SaveTestEquipment(diff, environment.Mock.Error, environment.Mock.SaveGuiShower);

            Assert.AreSame(diff, environment.Mock.SaveGuiShower.SaveTestEquipmentParameterDiff);
        }

        [Test]
        public void SaveTestEquipmentModelCallsGuiShowerSaveTestEquipmentModel()
        {
            var environment = new Environment();

            var diff = new TestEquipmentModelDiff(new TestEquipmentModel(), new TestEquipmentModel(), null);
            environment.UseCase.SaveTestEquipmentModel(diff, environment.Mock.Error, environment.Mock.SaveGuiShower);

            Assert.AreSame(diff, environment.Mock.SaveGuiShower.SaveTestEquipmentModelParameterDiff);
        }

        [Test]
        public void RemoveTestEquipmentCallsDataAccess()
        {
            var environment = new Environment();
            var user = CreateUser.Anonymous();
            environment.Mock.UserGetter.NextReturnedUser = user;
            var testEquipment = CreateTestEquipment.Anonymous();
            environment.UseCase.RemoveTestEquipment(testEquipment, null);

            Assert.AreSame(testEquipment, environment.Mock.Data.RemoveTestEquipmentParameterTestEquipment);
            Assert.AreSame(user, environment.Mock.Data.RemoveTestEquipmentParameterUser);
        }

        [Test]
        public void RemoveTestEquipmentCallsSuccessNotification()
        {
            var environment = new Environment();
            environment.UseCase.RemoveTestEquipment(CreateTestEquipment.Anonymous(), null);

            Assert.IsTrue(environment.Mock.notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void RemoveTestEquipmentCallsGui()
        {
            var environment = new Environment();
            var testEquipment = CreateTestEquipment.Anonymous();
            environment.UseCase.RemoveTestEquipment(testEquipment, null);

            Assert.AreSame(testEquipment, environment.Mock.Gui.RemoveTestEquipmentParameter);
        }
        
        [Test]
        public void RemoveTestEquipmentWithErrorCallsShowProblemRemoveTestEquipment()
        {
            var environment = new Environment();
            environment.Mock.Data.RemoveTestEquipmentThrowsError = true;

            environment.UseCase.RemoveTestEquipment(CreateTestEquipment.Anonymous(), environment.Mock.Error);

            Assert.IsTrue(environment.Mock.Error.ShowProblemRemoveTestEquipmentCalled);
        }


        [TestCase("231425")]
        [TestCase("abcde")]
        public void SaveTestEquipmentCallsDataAccessIsInventoryNumberUnique(string inventoryNumber)
        {
            var environment = new Environment();

            environment.UseCase.SaveTestEquipment(
                new TestEquipmentDiff(CreateTestEquipment.RandomizedWithInventoryNumber(32324, "s1"),
                    CreateTestEquipment.RandomizedWithInventoryNumber(32324, inventoryNumber), null), environment.Mock.Error,
                environment.Mock.SaveGuiShower);

            Assert.AreEqual(inventoryNumber, environment.Mock.Data.IsInventoryNumberUniqueParameter.ToDefaultString());
        }

        [TestCase("231425")]
        [TestCase("abcde")]
        public void UpdateTestEquipmentCallsDataAccessIsInventoryNumberUnique(string inventoryNumber)
        {
            var environment = new Environment();

            environment.UseCase.UpdateTestEquipment(
                new TestEquipmentDiff(CreateTestEquipment.RandomizedWithInventoryNumber(32324, "s1"),
                    CreateTestEquipment.RandomizedWithInventoryNumber(32324, inventoryNumber), null), environment.Mock.Error);

            Assert.AreEqual(inventoryNumber, environment.Mock.Data.IsInventoryNumberUniqueParameter.ToDefaultString());
        }

        [Test]
        public void SaveTestEquipmentCallsTestEquipmentAlreadyExistsIfInventoryNumberIsNotUnique()
        {
            var environment = new Environment();
            environment.Mock.Data.IsInventoryNumberUniqueReturnValue = false;
            environment.Mock.Data.IsSerialNumberUniqueReturnValue = true;

            environment.UseCase.SaveTestEquipment(
                new TestEquipmentDiff(CreateTestEquipment.RandomizedWithInventoryNumber(32324, "s1"),
                    CreateTestEquipment.RandomizedWithInventoryNumber(32324, "s2"), null), environment.Mock.Error,
                environment.Mock.SaveGuiShower);

            Assert.IsTrue(environment.Mock.Error.TestEquipmentAlreadyExistsCalled);
        }

        [Test]
        public void UpdateTestEquipmentCallsTestEquipmentAlreadyExistsIfInventoryNumberIsNotUnique()
        {
            var environment = new Environment();
            environment.Mock.Data.IsInventoryNumberUniqueReturnValue = false;
            environment.Mock.Data.IsSerialNumberUniqueReturnValue = true;

            environment.UseCase.UpdateTestEquipment(
                new TestEquipmentDiff(CreateTestEquipment.RandomizedWithInventoryNumber(32324, "s1"),
                    CreateTestEquipment.RandomizedWithInventoryNumber(32324, "s2"), null), environment.Mock.Error);

            Assert.IsTrue(environment.Mock.Error.TestEquipmentAlreadyExistsCalled);
        }

        [TestCase("231425")]
        [TestCase("abcde")]
        public void SaveTestEquipmentCallsDataAccessIsSerialNumberUnique(string serialNumber)
        {
            var environment = new Environment();

            environment.UseCase.SaveTestEquipment(
                new TestEquipmentDiff(CreateTestEquipment.RandomizedWithSerialNumber(32324, "s1"),
                    CreateTestEquipment.RandomizedWithSerialNumber(32324, serialNumber), null), environment.Mock.Error,
                environment.Mock.SaveGuiShower);

            Assert.AreEqual(serialNumber, environment.Mock.Data.IsSerialNumberUniqueParameter.ToDefaultString());
        }

        [TestCase("231425")]
        [TestCase("abcde")]
        public void UpdateTestEquipmentCallsDataAccessIsSerialNumberUnique(string serialNumber)
        {
            var environment = new Environment();

            environment.UseCase.UpdateTestEquipment(
                new TestEquipmentDiff(CreateTestEquipment.RandomizedWithSerialNumber(32324, "s1"),
                    CreateTestEquipment.RandomizedWithSerialNumber(32324, serialNumber), null), environment.Mock.Error);

            Assert.AreEqual(serialNumber, environment.Mock.Data.IsSerialNumberUniqueParameter.ToDefaultString());
        }

        [Test]
        public void SaveTestEquipmentCallsTestEquipmentAlreadyExistsIfSerialNumberIsNotUnique()
        {
            var environment = new Environment();
            environment.Mock.Data.IsInventoryNumberUniqueReturnValue = true;
            environment.Mock.Data.IsSerialNumberUniqueReturnValue = false;

            environment.UseCase.SaveTestEquipment(
                new TestEquipmentDiff(CreateTestEquipment.RandomizedWithSerialNumber(32324, "s1"),
                    CreateTestEquipment.RandomizedWithSerialNumber(32324, "s2"), null), environment.Mock.Error,
                environment.Mock.SaveGuiShower);

            Assert.IsTrue(environment.Mock.Error.TestEquipmentAlreadyExistsCalled);
        }

        [Test]
        public void UpdateTestEquipmentCallsTestEquipmentAlreadyExistsIfSerialNumberIsNotUnique()
        {
            var environment = new Environment();
            environment.Mock.Data.IsInventoryNumberUniqueReturnValue = true;
            environment.Mock.Data.IsSerialNumberUniqueReturnValue = false;

            environment.UseCase.UpdateTestEquipment(
                new TestEquipmentDiff(CreateTestEquipment.RandomizedWithSerialNumber(32324, "s1"),
                    CreateTestEquipment.RandomizedWithSerialNumber(32324, "s2"), null), environment.Mock.Error);

            Assert.IsTrue(environment.Mock.Error.TestEquipmentAlreadyExistsCalled);
        }

       
        [TestCase("231425")]
        [TestCase("abcde")]
        public void SaveTestEquipmentModelCallsDataAccessIsTestEquipmentModelNameUnique(string model)
        {
            var environment = new Environment();

            environment.UseCase.SaveTestEquipmentModel(
                new TestEquipmentModelDiff(CreateTestEquipmentModel.RandomizedWithName(32324, "s1"),
                    CreateTestEquipmentModel.RandomizedWithName(32324, model), null), environment.Mock.Error,
                environment.Mock.SaveGuiShower);

            Assert.AreEqual(model, environment.Mock.Data.IsTestEquipmentModelNameUniqueParameter.ToDefaultString());
        }

        [TestCase("231425")]
        [TestCase("abcde")]
        public void UpdateTestEquipmentModelCallsDataAccessIsTestEquipmentModelNameUnique(string model)
        {
            var environment = new Environment();

            environment.UseCase.UpdateTestEquipmentModel(
                new TestEquipmentModelDiff(CreateTestEquipmentModel.RandomizedWithName(32324, "s1"),
                    CreateTestEquipmentModel.RandomizedWithName(32324, model), null), environment.Mock.Error);

            Assert.AreEqual(model, environment.Mock.Data.IsTestEquipmentModelNameUniqueParameter.ToDefaultString());
        }

        [Test]
        public void SaveTestEquipmentModelCallsTestEquipmentModelAlreadyExistsIfModelNameIsNotUnique()
        {
            var environment = new Environment();
            environment.Mock.Data.IsTestEquipmentModelNameUniqueReturnValue = false;

            environment.UseCase.SaveTestEquipmentModel(
                new TestEquipmentModelDiff(CreateTestEquipmentModel.RandomizedWithName(32324, "s1"),
                    CreateTestEquipmentModel.RandomizedWithName(32324, "s2"), null), environment.Mock.Error,
                environment.Mock.SaveGuiShower);

            Assert.IsTrue(environment.Mock.Error.TestEquipmentModelAlreadyExistsCalled);
        }

        [Test]
        public void UpdateTestEquipmentModelCallsTestEquipmentModelAlreadyExistsIfModelNameIsNotUnique()
        {
            var environment = new Environment();
            environment.Mock.Data.IsTestEquipmentModelNameUniqueReturnValue = false;

            environment.UseCase.UpdateTestEquipmentModel(
                new TestEquipmentModelDiff(CreateTestEquipmentModel.RandomizedWithName(32324, "s1"),
                    CreateTestEquipmentModel.RandomizedWithName(32324, "s2"), null), environment.Mock.Error);

            Assert.IsTrue(environment.Mock.Error.TestEquipmentModelAlreadyExistsCalled);
        }

        [Test]
        public void AddTestEquipmentCallsDataAccess()
        {
            var environment = new Environment();
            var user = CreateUser.Anonymous();
            environment.Mock.UserGetter.NextReturnedUser = user;
            var testEquipment = CreateTestEquipment.Randomized(433647);
            environment.UseCase.AddTestEquipment(testEquipment, null);
            Assert.AreSame(testEquipment, environment.Mock.Data.AddTestEquipmentTestEquipmentParameter);
            Assert.AreSame(user, environment.Mock.Data.AddTestEquipmentUserParameter);
        }

        [Test]
        public void AddTestEquipmentCallsGui()
        {
            var environment = new Environment();
            var testEquipment = CreateTestEquipment.Randomized(433647);
            environment.Mock.Data.AddTestEquipmentReturnValue = testEquipment;
            environment.UseCase.AddTestEquipment(CreateTestEquipment.Randomized(456456), null);
            Assert.AreSame(testEquipment, environment.Mock.Gui.AddTestEquipmentParameter);
        }

        [Test]
        public void AddTestEquipmentCallsNotificationManager()
        {
            var environment = new Environment();
            environment.UseCase.AddTestEquipment(CreateTestEquipment.Randomized(456456), null);
            Assert.IsTrue(environment.Mock.notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void AddTestEquipmentCallsShowProblemSavingTestEquipment()
        {
            var environment = new Environment();
            environment.Mock.Data.AddTestEquipmentThrowsError = true;
            environment.UseCase.AddTestEquipment(CreateTestEquipment.Randomized(456456), environment.Mock.Error);
            Assert.IsTrue(environment.Mock.Error.ShowProblemSavingTestEquipmentWasCalled);
        }

        [TestCase("324356")]
        [TestCase("abcdefg")]
        public void IsInventoryNumberUniqueCallsDataAccess(string inventoryNumber)
        {
            var environment = new Environment();
            environment.UseCase.IsInventoryNumberUnique(inventoryNumber);
            Assert.AreEqual(inventoryNumber, environment.Mock.Data.IsInventoryNumberUniqueParameter.ToDefaultString());
        }

        [TestCase(false)]
        [TestCase(true)]
        public void IsInventoryNumberUniqueReturnsCorrectValue(bool isInventoryNumberUnique)
        {
            var environment = new Environment();
            environment.Mock.Data.IsInventoryNumberUniqueReturnValue = isInventoryNumberUnique;
            Assert.AreEqual(isInventoryNumberUnique, environment.UseCase.IsInventoryNumberUnique(""));
        }

        [TestCase("324356")]
        [TestCase("abcdefg")]
        public void IsSerialNumberUniqueCallsDataAccess(string serialNumber)
        {
            var environment = new Environment();
            environment.UseCase.IsSerialNumberUnique(serialNumber);
            Assert.AreEqual(serialNumber, environment.Mock.Data.IsSerialNumberUniqueParameter.ToDefaultString());
        }

        [TestCase(false)]
        [TestCase(true)]
        public void IsSerialNumberUniqueReturnsCorrectValue(bool isSerialNumberUnique)
        {
            var environment = new Environment();
            environment.Mock.Data.IsSerialNumberUniqueReturnValue = isSerialNumberUnique;
            Assert.AreEqual(isSerialNumberUnique, environment.UseCase.IsSerialNumberUnique(""));
        }

        [Test]
        public void LoadTestEquipmentModelsCallsDataAccess()
        {
            var environment = new Environment();
            environment.UseCase.LoadTestEquipmentModels(null);
            Assert.IsTrue(environment.Mock.Data.LoadTestEquipmentModelsCalled);
        }

        [Test]
        public void LoadTestEquipmentModelsCallsGui()
        {
            var environment = new Environment();
            var modelList = new List<TestEquipmentModel>();
            environment.Mock.Data.LoadTestEquipmentModelsReturnValue = modelList;
            environment.UseCase.LoadTestEquipmentModels(null);

            Assert.AreSame(modelList, environment.Mock.Gui.LoadTestEquipmentModelsParameter);
        }

        [Test]
        public void LoadTestEquipmentModelsCallsShowErrorMessageLoadingTestEquipmentModels()
        {
            var environment = new Environment();
            environment.Mock.Data.LoadTestEquipmentModelsThrowsError = true;
            environment.UseCase.LoadTestEquipmentModels(environment.Mock.Error);
            Assert.IsTrue(environment.Mock.Error.ShowErrorMessageLoadingTestEquipmentModelsCalled);
        }

        [Test]
        public void LoadAvailableTestEquipmentTypesCallsDataAccess()
        {
            var environment = new Environment();
            environment.UseCase.LoadAvailableTestEquipmentTypes();
            Assert.IsTrue(environment.Mock.Data.LoadAvailableTestEquipmentTypesCalled);
        }

        [Test]
        public void LoadAvailableTestEquipmentTypesReturnsCorrectValue()
        {
            var environment = new Environment();
            var testEquipmentTypes = new List<TestEquipmentType>();
            environment.Mock.Data.LoadAvailableTestEquipmentTypesReturnValue = testEquipmentTypes;
            Assert.AreSame(testEquipmentTypes, environment.UseCase.LoadAvailableTestEquipmentTypes());
        }

        private class TestEquipmentGuiMock: ITestEquipmentGui
        {
            public void UpdateTestEquipment(TestEquipment testEquipment)
            {
                UpdateTestEquipmentParameter = testEquipment;
            }

            public void ShowTestEquipments(List<TestEquipmentModel> testEquipmentModels)
            {
                if (ShowTestEquipmentsError)
                {
                    throw new Exception();
                }
                ShowTestEquipmentsParameter = testEquipmentModels;
            }

            public void UpdateTestEquipmentModel(TestEquipmentModel testEquipmentModel)
            {
                UpdateTestEquipmentModelParameter = testEquipmentModel;
            }

            public void RemoveTestEquipment(TestEquipment testEquipment)
            {
                RemoveTestEquipmentParameter = testEquipment;
            }

            public void AddTestEquipment(TestEquipment testEquipment)
            {
                AddTestEquipmentParameter = testEquipment;
            }
            
            public void LoadTestEquipmentModels(List<TestEquipmentModel> testEquipmentModels)
            {
                LoadTestEquipmentModelsParameter = testEquipmentModels;
            }

            public TestEquipment AddTestEquipmentParameter { get; set; }
            public List<TestEquipmentModel> LoadTestEquipmentModelsParameter { get; set; }
            public TestEquipmentModel UpdateTestEquipmentModelParameter { get; set; }
            public List<TestEquipmentModel> ShowTestEquipmentsParameter { get; set; }
            public bool ShowTestEquipmentsError { get; set; }
            public TestEquipment UpdateTestEquipmentParameter;
            public TestEquipment RemoveTestEquipmentParameter { get; set; }
        }

        private class TestEquipmentSaveGuiShowerMock : ITestEquipmentSaveGuiShower
        {
            public void SaveTestEquipmentModel(TestEquipmentModelDiff diff, Action saveAction)
            {
                SaveTestEquipmentModelParameterDiff = diff;
                SaveTestEquipmentModelParameterAction = saveAction;
                saveAction();
            }

            public void SaveTestEquipment(TestEquipmentDiff diff, Action saveAction)
            {
                SaveTestEquipmentParameterDiff = diff;
                SaveTestEquipmentParameterAction = saveAction;
                saveAction();
            }

            public TestEquipmentModelDiff SaveTestEquipmentModelParameterDiff { get; set; }
            public Action SaveTestEquipmentModelParameterAction { get; set; }
            public TestEquipmentDiff SaveTestEquipmentParameterDiff { get; set; }
            public Action SaveTestEquipmentParameterAction { get; set; }
        }

        private class TestEquipmentErrorMock: ITestEquipmentErrorGui
        {
            public void ShowProblemSavingTestEquipment()
            {
                ShowProblemSavingTestEquipmentWasCalled = true;
            }

            public void ShowProblemRemoveTestEquipment()
            {
                ShowProblemRemoveTestEquipmentCalled = true;
            }

            public void ShowErrorMessageLoadingTestEquipments()
            {
                ShowErrorMessageLoadingTestEquipmentsCalled = true;
            }

            public void ShowErrorMessageLoadingTestEquipmentModels()
            {
                ShowErrorMessageLoadingTestEquipmentModelsCalled = true;
            }

            public void TestEquipmentAlreadyExists()
            {
                TestEquipmentAlreadyExistsCalled = true;
            }

            public void TestEquipmentModelAlreadyExists()
            {
                TestEquipmentModelAlreadyExistsCalled = true;
            }

            public bool TestEquipmentModelAlreadyExistsCalled { get; set; }
            public bool TestEquipmentAlreadyExistsCalled { get; set; }
            public bool ShowProblemSavingTestEquipmentWasCalled = false; 
            public bool ShowErrorMessageLoadingTestEquipmentsCalled { get; set; }
            public bool ShowProblemRemoveTestEquipmentCalled { get; set; }
            public bool ShowErrorMessageLoadingTestEquipmentModelsCalled { get; set; }
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    Data = new TestEquipmentDataAccessMock();
                    Gui = new TestEquipmentGuiMock();
                    UserGetter = new UserGetterMock();
                    Error = new TestEquipmentErrorMock();
                    SaveGuiShower = new TestEquipmentSaveGuiShowerMock();
                    notificationManager = new NotificationManagerMock();
                }

                public readonly TestEquipmentDataAccessMock Data;
                public readonly TestEquipmentGuiMock Gui;
                public readonly TestEquipmentErrorMock Error;
                public readonly TestEquipmentSaveGuiShowerMock SaveGuiShower;
                public readonly UserGetterMock UserGetter;
                public readonly NotificationManagerMock notificationManager;
            }

            public Environment()
            {
                Mock = new Mocks();
                UseCase = new TestEquipmentUseCase(Mock.Data, Mock.Gui, Mock.UserGetter, Mock.notificationManager);
            }

            public readonly Mocks Mock;
            public readonly TestEquipmentUseCase UseCase;
        }
    }
}
