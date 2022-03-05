using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Client.Core.Diffs;
using Client.Core.Validator;
using Client.TestHelper.Factories;
using Common.Types.Enums;
using Core.Diffs;
using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using InterfaceAdapters.Communication;
using NUnit.Framework;
using TestHelper.Factories;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    public class TestEquipmentValidatorMock : ITestEquipmentValidator
    {
        public bool Validate(TestEquipment testEquipment)
        {
            ValidateTestEquipmentParameter = testEquipment;
            return ValidateTestEquipmentReturnValue;
        }

        public bool Validate(TestEquipmentModel testEquipmentModel)
        {
            ValidateTestEquipmentModelParameter = testEquipmentModel;
            return ValidateTestEquipmentModelReturnValue;
        }

        public bool ValidateTestEquipmentReturnValue { get; set; } = true;
        public TestEquipment ValidateTestEquipmentParameter { get; set; }
        public bool ValidateTestEquipmentModelReturnValue { get; set; } = true;
        public TestEquipmentModel ValidateTestEquipmentModelParameter { get; set; }
    }

    class TestEquipmentViewModelTest
    {

        [Test]
        public void LoadedCommandCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.ViewModel.LoadedCommand.Invoke(null);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test]
        public void LoadedCommandCallsUseCaseShowTestEquipments()
        {
            var environment = new Environment();
            environment.ViewModel.LoadedCommand.Invoke(null);
            Assert.IsTrue(environment.Mock.UseCase.ShowTestEquipmentsCalled);
            Assert.AreEqual(environment.ViewModel, environment.Mock.UseCase.ShowTestEquipmentsParameter);
        }

        [Test]
        public void IsTestEquipmentVisibleReturnsCorrectValue()
        {
            var environment = new Environment();
            Assert.IsFalse(environment.ViewModel.IsTestEquipmentVisible);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = new TestEquipmentHumbleModel(null, null);
            Assert.IsTrue(environment.ViewModel.IsTestEquipmentVisible);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = null;
            Assert.IsFalse(environment.ViewModel.IsTestEquipmentVisible);
        }

        [Test]
        public void IsTestEquipmentModelVisibleReturnsCorrectValue()
        {
            var environment = new Environment();
            Assert.IsFalse(environment.ViewModel.IsTestEquipmentModelVisible);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(null, null);
            Assert.IsTrue(environment.ViewModel.IsTestEquipmentModelVisible);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = null;
            Assert.IsFalse(environment.ViewModel.IsTestEquipmentModelVisible);
        }

        [Test]
        public void SaveTestEquipmentCanExecuteReturnsCorrectValueForTestEquipment()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = null;

            Assert.IsFalse(environment.ViewModel.SaveTestEquipmentCommand.CanExecute(null));

            environment.Mock.InterfaceAdapter.SelectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges = environment.Mock.InterfaceAdapter.SelectedTestEquipment.CopyDeep();

            Assert.IsFalse(environment.ViewModel.SaveTestEquipmentCommand.CanExecute(null));

            environment.Mock.InterfaceAdapter.SelectedTestEquipment.SerialNumber = "abcd";

            Assert.IsTrue(environment.ViewModel.SaveTestEquipmentCommand.CanExecute(null));

            environment.Mock.Validator.ValidateTestEquipmentReturnValue = false;

            Assert.IsFalse(environment.ViewModel.SaveTestEquipmentCommand.CanExecute(null));
        }

        [Test]
        public void SaveTestEquipmentCanExecuteReturnsCorrectValueForTestEquipmentModel()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = null;

            Assert.IsFalse(environment.ViewModel.SaveTestEquipmentCommand.CanExecute(null));

            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges = environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel.CopyDeep();

            Assert.IsFalse(environment.ViewModel.SaveTestEquipmentCommand.CanExecute(null));

            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel.TestEquipmentModelName = "abcd";

            Assert.IsTrue(environment.ViewModel.SaveTestEquipmentCommand.CanExecute(null));

            environment.Mock.Validator.ValidateTestEquipmentModelReturnValue = false;

            Assert.IsFalse(environment.ViewModel.SaveTestEquipmentCommand.CanExecute(null));
        }

        [Test]
        public void SaveCommandCallsUseCaseSaveTestEquipment()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges.CopyDeep();
            environment.Mock.InterfaceAdapter.SelectedTestEquipment.TransferAdapter = !environment.Mock.InterfaceAdapter.SelectedTestEquipment.TransferAdapter;

            environment.ViewModel.SaveTestEquipmentCommand.Invoke(null);
            Assert.AreSame(environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges.Entity, environment.Mock.UseCase.SaveTestEquipmentParameterDiff.OldTestEquipment);
            Assert.AreSame(environment.Mock.InterfaceAdapter.SelectedTestEquipment.Entity, environment.Mock.UseCase.SaveTestEquipmentParameterDiff.NewTestEquipment);
            Assert.IsNull(environment.Mock.UseCase.SaveTestEquipmentParameterDiff.User);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.SaveTestEquipmentParameterGuiError);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.SaveTestEquipmentSaveGuiShower);
        }

        [Test]
        public void SaveCommandCallsUseCaseSaveTestEquipmentModel()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges.CopyDeep();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel.TransferAdapter = !environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel.TransferAdapter;

            environment.ViewModel.SaveTestEquipmentCommand.Invoke(null);
            Assert.AreSame(environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges.Entity, environment.Mock.UseCase.SaveTestEquipmentModelParameterDiff.OldTestEquipmentModel);
            Assert.AreSame(environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel.Entity, environment.Mock.UseCase.SaveTestEquipmentModelParameterDiff.NewTestEquipmentModel);
            Assert.IsNull(environment.Mock.UseCase.SaveTestEquipmentModelParameterDiff.User);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.SaveTestEquipmentModelParameterGuiError);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.SaveTestEquipmentModelSaveGuiShower);
        }

        [Test]
        public void SaveCommandForTestEquipmentCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges.CopyDeep();
            environment.Mock.InterfaceAdapter.SelectedTestEquipment.TransferAdapter = !environment.Mock.InterfaceAdapter.SelectedTestEquipment.TransferAdapter;

            environment.ViewModel.SaveTestEquipmentCommand.Invoke(null);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test]
        public void SaveCommandForTestEquipmentModelCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges.CopyDeep();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel.TransferAdapter = !environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel.TransferAdapter;

            environment.ViewModel.SaveTestEquipmentCommand.Invoke(null);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [TestCase("abc")]
        [TestCase("def")]
        public void SelectStatusPathCommandSetsTestEquipmentModelStatusPath(string path)
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            environment.ViewModel.FileDialogRequest += (s, e) =>
            {
                e.ResultAction(path);
            };
            environment.ViewModel.SelectStatusPathCommand.Invoke(null);
            Assert.AreEqual(path, environment.ViewModel.SelectedTestEquipmentModel.StatusFilePath);
        }

        [TestCase("ghi")]
        [TestCase("jkl")]
        public void SelectCommunicationPathCommandSetsTestEquipmentModelCommunicationPath(string path)
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            environment.ViewModel.FileDialogRequest += (s, e) =>
            {
                e.ResultAction(path);
            };
            environment.ViewModel.SelectCommunicationPathCommand.Invoke(null);
            Assert.AreEqual(path, environment.ViewModel.SelectedTestEquipmentModel.CommunicationFilePath);
        }

        [TestCase("lmn")]
        [TestCase("opq")]
        public void SelectDriverPathCommandSetsTestEquipmentModelDriverPath(string path)
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            environment.ViewModel.FileDialogRequest += (s, e) =>
            {
                e.ResultAction(path);
            };
            environment.ViewModel.SelectDriverPathCommand.Invoke(null);
            Assert.AreEqual(path, environment.ViewModel.SelectedTestEquipmentModel.DriverProgramPath);
        }

        [TestCase("rst")]
        [TestCase("uvw")]
        public void SelectResultPathCommandSetsTestEquipmentResultDriverPath(string path)
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            environment.ViewModel.FileDialogRequest += (s, e) =>
            {
                e.ResultAction(path);
            };
            environment.ViewModel.SelectResultPathCommand.Invoke(null);
            Assert.AreEqual(path, environment.ViewModel.SelectedTestEquipmentModel.ResultFilePath);
        }

        [Test]
        public void SaveTestEquipmentDontShowChangesIfTestEquipmentHasNoChanges()
        {
            var environment = new Environment();
            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) => showDialogRequestInvoked = true;

            var testEquipment = CreateTestEquipment.Randomized(12324);
            var diff = new TestEquipmentDiff(testEquipment, testEquipment, null);
            environment.ViewModel.SaveTestEquipment(diff, () => {});

            Assert.IsFalse(showDialogRequestInvoked);
        }

        [Test]
        public void SaveTestEquipmentDontShowChangesIfTestEquipmentModelHasNoChanges()
        {
            var environment = new Environment();
            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) => showDialogRequestInvoked = true;

            var testEquipmentModel = CreateTestEquipmentModel.Randomized(12324);
            var diff = new TestEquipmentModelDiff(testEquipmentModel, testEquipmentModel, null);
            environment.ViewModel.SaveTestEquipmentModel(diff, () => { });

            Assert.IsFalse(showDialogRequestInvoked);
        }

        [Test]
        public void SaveTestEquipmentForTestEquipmentWithNoResultTest()
        {
            var environment = new Environment();
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(684543), null);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };
            var oldTestEquipment = CreateTestEquipment.Randomized(12324);
            var newTestEquipment = CreateTestEquipment.Randomized(6578);

            var diff = new TestEquipmentDiff(oldTestEquipment, newTestEquipment, null);

            environment.ViewModel.SaveTestEquipment(diff, () => {});

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsFalse(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.IsTrue(environment.Mock.InterfaceAdapter.SelectedTestEquipment.EqualsByContent(environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges));
        }

        [Test]
        public void SaveTestEquipmentForTestEquipmentModelWithNoResultTest()
        {
            var environment = new Environment();
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(684543), null);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };
            var oldTestEquipmentModel = CreateTestEquipmentModel.Randomized(12324);
            var newTestEquipmentModel = CreateTestEquipmentModel.Randomized(6578);

            var diff = new TestEquipmentModelDiff(oldTestEquipmentModel, newTestEquipmentModel, null);

            environment.ViewModel.SaveTestEquipmentModel(diff, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsFalse(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.IsTrue(environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel.EqualsByContent(environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges));
        }

        [Test]
        public void SaveTestEquipmentForTestEquipmentWithCancelResultTest()
        {
            var environment = new Environment();
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(684543), null);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Cancel;
            };
            var oldTestEquipment = CreateTestEquipment.Randomized(12324);
            var newTestEquipment = CreateTestEquipment.Randomized(6578);

            var diff = new TestEquipmentDiff(oldTestEquipment, newTestEquipment, null);

            environment.ViewModel.SaveTestEquipment(diff, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsFalse(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.IsFalse(environment.Mock.InterfaceAdapter.SelectedTestEquipment.EqualsByContent(environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges));
        }

        [Test]
        public void SaveTestEquipmentForTestEquipmentModelWithCancelResultTest()
        {
            var environment = new Environment();
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(684543), null);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Cancel;
            };
            var oldTestEquipmentModel = CreateTestEquipmentModel.Randomized(12324);
            var newTestEquipmentModel = CreateTestEquipmentModel.Randomized(6578);

            var diff = new TestEquipmentModelDiff(oldTestEquipmentModel, newTestEquipmentModel, null);

            environment.ViewModel.SaveTestEquipmentModel(diff, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsFalse(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.IsFalse(environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel.EqualsByContent(environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges));
        }

        [Test]
        public void SaveTestEquipmentForTestEquipmentWithYesResultCallsFinishedAction()
        {
            var environment = new Environment();
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;
            };

            var oldTestEquipment = CreateTestEquipment.Randomized(12324);
            var newTestEquipment = CreateTestEquipment.Randomized(6578);

            var diff = new TestEquipmentDiff(oldTestEquipment, newTestEquipment, null);
            var finishedActionCalled = false;
            environment.ViewModel.SaveTestEquipment(diff, () => { finishedActionCalled = true; });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(finishedActionCalled);
        }

        [Test]
        public void SaveTestEquipmentForTestEquipmentModelWithYesResultCallsFinishedAction()
        {
            var environment = new Environment();
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;
            };

            var oldTestEquipmentModel = CreateTestEquipmentModel.Randomized(12324);
            var newTestEquipmentModel = CreateTestEquipmentModel.Randomized(6578);

            var diff = new TestEquipmentModelDiff(oldTestEquipmentModel, newTestEquipmentModel, null);
            var finishedActionCalled = false;
            environment.ViewModel.SaveTestEquipmentModel(diff, () => { finishedActionCalled = true; });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(finishedActionCalled);
        }


        [Test]
        public void RemoveCommandCanExecutedTest()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = null;
            Assert.IsFalse(environment.ViewModel.RemoveTestEquipmentCommand.CanExecute(null));
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            Assert.IsTrue(environment.ViewModel.RemoveTestEquipmentCommand.CanExecute(null));
        }

        [Test]
        public void RemoveCommandCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges.CopyDeep();

            environment.ViewModel.RemoveTestEquipmentCommand.Invoke(null);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test]
        public void RemoveCommandCancelTest()
        {
            var environment = new Environment();
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(684543), null);

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.No);
            };

            environment.ViewModel.RemoveTestEquipmentCommand.Invoke(null);

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsFalse(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.IsNull(environment.Mock.UseCase.RemoveTestEquipmentParameterTestEquipment);
        }

        [Test]
        public void RemoveCommandYesTest()
        {
            var environment = new Environment();
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(684543), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.Yes);
            };

            environment.ViewModel.RemoveTestEquipmentCommand.Invoke(null);

            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsTrue(
                environment.Mock.InterfaceAdapter.SelectedTestEquipment.EqualsByContent(environment.Mock
                    .InterfaceAdapter.SelectedTestEquipmentWithoutChanges));


            Assert.AreSame(environment.Mock.InterfaceAdapter.SelectedTestEquipment.Entity, environment.Mock.UseCase.RemoveTestEquipmentParameterTestEquipment);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.RemoveTestEquipmentParameterErrorHandler);
        }

        [Test]
        public void SelectTestEquipmentWithSameTestEquipmentDoesNothing()
        {
            var environment = new Environment();
            var testEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);

            environment.Mock.InterfaceAdapter.SelectedTestEquipment = testEquipment;
            environment.ViewModel.SelectedTestEquipment = testEquipment;

            Assert.IsNull(environment.Mock.Validator.ValidateTestEquipmentParameter);
        }

        [Test]
        public void SelectTestEquipmentModelWithSameTestEquipmentModelDoesNothing()
        {
            var environment = new Environment();
            var testEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);

            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = testEquipmentModel;
            environment.ViewModel.SelectedTestEquipmentModel = testEquipmentModel;

            Assert.IsNull(environment.Mock.Validator.ValidateTestEquipmentModelParameter);
        }

        [Test]
        public void SelectNewTestEquipmentCallsValidatorWithSelectedTestEquipment()
        {
            var environment = new Environment();
            var selectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = selectedTestEquipment;

            environment.ViewModel.SelectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);

            Assert.AreSame(selectedTestEquipment.Entity, environment.Mock.Validator.ValidateTestEquipmentParameter);
        }

        [Test]
        public void SelectNewTestEquipmentWithInvalidTestEquipmentContinueEditing()
        {
            var environment = new Environment();
            var oldTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(684543), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = oldTestEquipment;
            environment.Mock.Validator.ValidateTestEquipmentReturnValue = false;

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.Yes);
            };

            var selectionRequestInvoked = false;
            environment.ViewModel.SelectionRequestTestEquipment += (s, e) => selectionRequestInvoked = true;

            environment.ViewModel.SelectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(45647), null);

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsTrue(selectionRequestInvoked);
            Assert.AreSame(oldTestEquipment, environment.ViewModel.SelectedTestEquipment);
        }

        [Test]
        public void SelectNewTestEquipmentWithInvalidTestEquipmentDontContinueEditing()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(684543), null);
            environment.Mock.Validator.ValidateTestEquipmentReturnValue = false;

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.No);
            };

            var selectionRequestInvoked = false;
            environment.ViewModel.SelectionRequestTestEquipment += (s, e) => selectionRequestInvoked = true;

            var newTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(45647), null);
            environment.ViewModel.SelectedTestEquipment = newTestEquipment;

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsFalse(selectionRequestInvoked);
            Assert.AreSame(newTestEquipment, environment.ViewModel.SelectedTestEquipment);
        }

        [Test]
        public void SelectNewTestEquipmentWithChangedTestEquipmentCancelTest()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(456), null);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Cancel;
            };

            var selectionRequestInvoked = false;
            environment.ViewModel.SelectionRequestTestEquipment += (s, e) => selectionRequestInvoked = true;

            var selectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(684543), null);
            environment.ViewModel.SelectedTestEquipment = selectedTestEquipment;

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.AreNotSame(environment.Mock.InterfaceAdapter.SelectedTestEquipment,selectedTestEquipment);
            Assert.IsTrue(selectionRequestInvoked);
        }

        [Test]
        public void SelectNewTestEquipmentWithChangedTestEquipmentYesTest()
        {
            var environment = new Environment();
            var oldTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            var changedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(684543), null);

            environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges = oldTestEquipment;
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = changedTestEquipment;

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;
            };

            var newTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(546), null);
            environment.ViewModel.SelectedTestEquipment = newTestEquipment;

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.AreSame(oldTestEquipment.Entity, environment.Mock.UseCase.UpdateTestEquipmentDiffParameter.OldTestEquipment);
            Assert.AreSame(changedTestEquipment.Entity, environment.Mock.UseCase.UpdateTestEquipmentDiffParameter.NewTestEquipment);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.UpdateTestEquipmentErrorParameter);
            Assert.AreSame(newTestEquipment, environment.ViewModel.SelectedTestEquipment);
        }

        [Test]
        public void SelectNewTestEquipmentWithChangedTestEquipmentNoTest()
        {
            var environment = new Environment();
            var oldTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges = oldTestEquipment;
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(34523), null);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };

            var newTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(684543), null);
            environment.ViewModel.SelectedTestEquipment = newTestEquipment;

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(environment.Mock.InterfaceAdapter.SelectedTestEquipment.EqualsByContent(newTestEquipment));
            Assert.IsTrue(oldTestEquipment.EqualsByContent(environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges));
            Assert.AreSame(newTestEquipment, environment.ViewModel.SelectedTestEquipment);
        }

        [Test]
        public void SelectNewTestEquipmentModelCallsValidatorWithSelectedTestEquipmentModel()
        {
            var environment = new Environment();
            var selectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = selectedTestEquipmentModel;

            environment.ViewModel.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(4564), null);

            Assert.AreSame(selectedTestEquipmentModel.Entity, environment.Mock.Validator.ValidateTestEquipmentModelParameter);
        }

        [Test]
        public void SelectNewTestEquipmentModelWithInvalidTestEquipmentModelContinueEditing()
        {
            var environment = new Environment();
            var oldTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(684543), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = oldTestEquipmentModel;
            environment.Mock.Validator.ValidateTestEquipmentModelReturnValue = false;

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.Yes);
            };

            var selectionRequestInvoked = false;
            environment.ViewModel.SelectionRequestTestEquipmentModel += (s, e) => selectionRequestInvoked = true;

            environment.ViewModel.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(45647), null);

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsTrue(selectionRequestInvoked);
            Assert.AreSame(oldTestEquipmentModel, environment.ViewModel.SelectedTestEquipmentModel);
        }

        [Test]
        public void SelectNewTestEquipmentModelWithInvalidTestEquipmentModelDontContinueEditing()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(684543), null);
            environment.Mock.Validator.ValidateTestEquipmentModelReturnValue = false;

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.No);
            };

            var selectionRequestInvoked = false;
            environment.ViewModel.SelectionRequestTestEquipmentModel += (s, e) => selectionRequestInvoked = true;

            var newTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(45647), null);
            environment.ViewModel.SelectedTestEquipmentModel = newTestEquipmentModel;

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsFalse(selectionRequestInvoked);
            Assert.AreSame(newTestEquipmentModel, environment.ViewModel.SelectedTestEquipmentModel);
        }

        [Test]
        public void SelectNewTestEquipmentModelWithChangedTestEquipmentModelCancelTest()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(456), null);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Cancel;
            };

            var selectionRequestInvoked = false;
            environment.ViewModel.SelectionRequestTestEquipmentModel += (s, e) => selectionRequestInvoked = true;

            var selectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(684543), null);
            environment.ViewModel.SelectedTestEquipmentModel = selectedTestEquipmentModel;

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.AreNotSame(environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel, selectedTestEquipmentModel);
            Assert.IsTrue(selectionRequestInvoked);
        }

        [Test]
        public void SelectNewTestEquipmentModelWithChangedTestEquipmentModelYesTest()
        {
            var environment = new Environment();
            var oldTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            var changedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(684543), null);

            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges = oldTestEquipmentModel;
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = changedTestEquipmentModel;

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;
            };

            var newTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(546), null);
            environment.ViewModel.SelectedTestEquipmentModel = newTestEquipmentModel;

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.AreSame(oldTestEquipmentModel.Entity, environment.Mock.UseCase.UpdateTestEquipmentModelDiffParameter.OldTestEquipmentModel);
            Assert.AreSame(changedTestEquipmentModel.Entity, environment.Mock.UseCase.UpdateTestEquipmentModelDiffParameter.NewTestEquipmentModel);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.UpdateTestEquipmentModelErrorParameter);
            Assert.AreSame(newTestEquipmentModel, environment.ViewModel.SelectedTestEquipmentModel);
        }

        [Test]
        public void SelectNewTestEquipmentModelWithChangedTestEquipmentModelNoTest()
        {
            var environment = new Environment();
            var oldTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges = oldTestEquipmentModel;
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(34523), null);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };

            var newTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(684543), null);
            environment.ViewModel.SelectedTestEquipmentModel = newTestEquipmentModel;

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel.EqualsByContent(newTestEquipmentModel));
            Assert.IsTrue(oldTestEquipmentModel.EqualsByContent(environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges));
            Assert.AreSame(newTestEquipmentModel, environment.ViewModel.SelectedTestEquipmentModel);
        }

        [Test]
        public void CanCloseReturnsTrueWhenNothingIsSelected()
        {
            var environment = new Environment();
            Assert.IsTrue(environment.ViewModel.CanClose());
        }

        
        [Test]
        public void CanCloseCallsValidatorWithSelectedTestEquipment()
        {
            var environment = new Environment();
            var selectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = selectedTestEquipment;

            environment.ViewModel.CanClose();

            Assert.AreSame(selectedTestEquipment.Entity, environment.Mock.Validator.ValidateTestEquipmentParameter);
        }

        [Test]
        public void CanCloseCloseWithInvalidTestEquipmentContinueEditing()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(684543), null);
            environment.Mock.Validator.ValidateTestEquipmentReturnValue = false;

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.Yes);
            };

            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsFalse(result);
        }

        [Test]
        public void CanCloseCloseWithInvalidTestEquipmentDontContinueEditing()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(684543), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(684543), null);
            environment.Mock.Validator.ValidateTestEquipmentReturnValue = false;

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.No);
            };

            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsNull(environment.Mock.InterfaceAdapter.SelectedTestEquipment);
            Assert.IsTrue(result);
        }

        [Test]
        public void CanCloseWithChangedTestEquipmentCancelTest()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            var testEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(684543), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = testEquipment;

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Cancel;
            };
            
            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.AreSame(testEquipment, environment.ViewModel.SelectedTestEquipment);
            Assert.IsFalse(result);
        }

        [Test]
        public void CanCloseWithChangedTestEquipmentYesTest()
        {
            var environment = new Environment();
            var oldTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            var changedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(684543), null);
            
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges = oldTestEquipment;
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = changedTestEquipment;

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;
            };

            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.AreSame(oldTestEquipment.Entity, environment.Mock.UseCase.UpdateTestEquipmentDiffParameter.OldTestEquipment);
            Assert.AreSame(changedTestEquipment.Entity, environment.Mock.UseCase.UpdateTestEquipmentDiffParameter.NewTestEquipment);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.UpdateTestEquipmentErrorParameter);
            Assert.IsNull(environment.Mock.InterfaceAdapter.SelectedTestEquipment);
            Assert.IsTrue(result);
        }

        [Test]
        public void CanCloseCloseWithChangedTestEquipmentNoTest()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(684543), null);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };

           var result = environment.ViewModel.CanClose();

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsNull(environment.Mock.InterfaceAdapter.SelectedTestEquipment);
            Assert.IsTrue(result);
        }

        [Test]
        public void CanCloseCloseWithValidAndNotChangedTest()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges.CopyDeep();

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };

            var selectionRequestInvoked = false;
            environment.ViewModel.SelectionRequestTestEquipment += (s, e) => selectionRequestInvoked = true;
            
            var result = environment.ViewModel.CanClose();

            Assert.IsFalse(showDialogRequestInvoked);
            Assert.IsFalse(selectionRequestInvoked);
            Assert.IsNull(environment.Mock.InterfaceAdapter.SelectedTestEquipment);
            Assert.IsTrue(result);
        }

        [Test]
        public void CanCloseCallsValidatorWithSelectedTestEquipmentModel()
        {
            var environment = new Environment();
            var selectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = selectedTestEquipmentModel;

            environment.ViewModel.CanClose();

            Assert.AreSame(selectedTestEquipmentModel.Entity, environment.Mock.Validator.ValidateTestEquipmentModelParameter);
        }

        [Test]
        public void CanCloseCloseWithInvalidTestEquipmentModelContinueEditing()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(684543), null);
            environment.Mock.Validator.ValidateTestEquipmentModelReturnValue = false;

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.Yes);
            };

            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsFalse(result);
        }

        [Test]
        public void CanCloseCloseWithInvalidTestEquipmentModelDontContinueEditing()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(684543), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(684543), null);
            environment.Mock.Validator.ValidateTestEquipmentModelReturnValue = false;

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.No);
            };

            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsNull(environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel);
            Assert.IsTrue(result);
        }

        [Test]
        public void CanCloseWithChangedTestEquipmentModelCancelTest()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            var testEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(684543), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = testEquipmentModel;

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Cancel;
            };

            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.AreSame(testEquipmentModel, environment.ViewModel.SelectedTestEquipmentModel);
            Assert.IsFalse(result);
        }

        [Test]
        public void CanCloseWithChangedTestEquipmentModelYesTest()
        {
            var environment = new Environment();
            var oldTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            var changedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(684543), null);

            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges = oldTestEquipmentModel;
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = changedTestEquipmentModel;

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;
            };

            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.AreSame(oldTestEquipmentModel.Entity, environment.Mock.UseCase.UpdateTestEquipmentModelDiffParameter.OldTestEquipmentModel);
            Assert.AreSame(changedTestEquipmentModel.Entity, environment.Mock.UseCase.UpdateTestEquipmentModelDiffParameter.NewTestEquipmentModel);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.UpdateTestEquipmentModelErrorParameter);
            Assert.IsNull(environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel);
            Assert.IsTrue(result);
        }

        [Test]
        public void CanCloseCloseWithChangedTestEquipmentModelNoTest()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(684543), null);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };

            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsNull(environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel);
            Assert.IsTrue(result);
        }

        [Test]
        public void CanCloseWithValidAndNotChangedTest()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges.CopyDeep();

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };

            var selectionRequestInvoked = false;
            environment.ViewModel.SelectionRequestTestEquipmentModel += (s, e) => selectionRequestInvoked = true;

            var result = environment.ViewModel.CanClose();

            Assert.IsFalse(showDialogRequestInvoked);
            Assert.IsFalse(selectionRequestInvoked);
            Assert.IsNull(environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel);
            Assert.IsTrue(result);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void AddTestEquipmentCommandCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.Mock.StartUp.OpenAddTestEquipmentAssistantReturnValue = new View.AssistentView("");
            environment.ViewModel.AddTestEquipmentCommand.Invoke(null);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void AddTestEquipmentCommandShowAssistantDialog()
        {
            var environment = new Environment();
            environment.Mock.StartUp.OpenAddTestEquipmentAssistantReturnValue = new View.AssistentView("");

            ICanShowDialog arg = null;
            environment.ViewModel.ShowDialogRequest += (s, e) => arg = e;
            environment.ViewModel.AddTestEquipmentCommand.Invoke(null);

            Assert.AreEqual(environment.Mock.StartUp.OpenAddTestEquipmentAssistantReturnValue, arg);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void AddTestEquipmentCommandDontCallsOpenAddTestEquipmentAssistantIfTestEquipmentIsChanged()
        {
            var environment = new Environment();
            environment.Mock.UseCase.LoadAvailableTestEquipmentTypesReturnValue = new List<TestEquipmentType>();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentWithoutChanges = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(4356), null); ;
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null); ;
            environment.ViewModel.RequestVerifyChangesView += (s,e) => e.Result = System.Windows.MessageBoxResult.Cancel;
            environment.ViewModel.AddTestEquipmentCommand.Invoke(null);
            Assert.IsNull(environment.Mock.StartUp.OpenAddTestEquipmentAssistantDefaultTestEquipment);
            Assert.IsNull(environment.Mock.StartUp.OpenAddTestEquipmentAssistantParameterAvailableTypes);
            Assert.IsNull(environment.Mock.StartUp.OpenAddTestEquipmentAssistantDefaultTestEquipmentType);
            Assert.IsNull(environment.Mock.StartUp.OpenAddTestEquipmentAssistantDefaultTestEquipmentModel);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void AddTestEquipmentCommandDontCallsOpenAddTestEquipmentAssistantIfTestEquipmentModelIsChanged()
        {
            var environment = new Environment();
            environment.Mock.UseCase.LoadAvailableTestEquipmentTypesReturnValue = new List<TestEquipmentType>();
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null); ;
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModelWithoutChanges = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(4564), null); ;
            environment.ViewModel.RequestVerifyChangesView += (s, e) => e.Result = System.Windows.MessageBoxResult.Cancel;
            environment.ViewModel.AddTestEquipmentCommand.Invoke(null);
            Assert.IsNull(environment.Mock.StartUp.OpenAddTestEquipmentAssistantDefaultTestEquipment);
            Assert.IsNull(environment.Mock.StartUp.OpenAddTestEquipmentAssistantParameterAvailableTypes);
            Assert.IsNull(environment.Mock.StartUp.OpenAddTestEquipmentAssistantDefaultTestEquipmentType);
            Assert.IsNull(environment.Mock.StartUp.OpenAddTestEquipmentAssistantDefaultTestEquipmentModel);
        }


        [Test, RequiresThread(ApartmentState.STA)]
        public void AddTestEquipmentCommandCallsOpenAddTestEquipmentAssistantWithTestEquipment()
        {
            var environment = new Environment();
            var availableTypes = new List<TestEquipmentType>();
            environment.Mock.UseCase.LoadAvailableTestEquipmentTypesReturnValue = availableTypes;
            environment.Mock.StartUp.OpenAddTestEquipmentAssistantReturnValue = new View.AssistentView("");
            var testEquipment = new TestEquipmentHumbleModel(CreateTestEquipment.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = testEquipment;
            environment.ViewModel.AddTestEquipmentCommand.Invoke(null);
            Assert.AreSame(testEquipment.Entity, environment.Mock.StartUp.OpenAddTestEquipmentAssistantDefaultTestEquipment);
            Assert.AreSame(availableTypes, environment.Mock.StartUp.OpenAddTestEquipmentAssistantParameterAvailableTypes);
            Assert.IsNull(environment.Mock.StartUp.OpenAddTestEquipmentAssistantDefaultTestEquipmentType);
            Assert.IsNull(environment.Mock.StartUp.OpenAddTestEquipmentAssistantDefaultTestEquipmentModel);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void AddTestEquipmentCommandCallsOpenAddTestEquipmentAssistantWithTestEquipmentModel()
        {
            var environment = new Environment();
            var availableTypes = new List<TestEquipmentType>();
            environment.Mock.UseCase.LoadAvailableTestEquipmentTypesReturnValue = availableTypes;
            environment.Mock.StartUp.OpenAddTestEquipmentAssistantReturnValue = new View.AssistentView("");
            environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel = new TestEquipmentModelHumbleModel(CreateTestEquipmentModel.Randomized(12324), null);
            environment.ViewModel.AddTestEquipmentCommand.Invoke(null);
            Assert.AreSame(environment.Mock.InterfaceAdapter.SelectedTestEquipmentModel.Entity, environment.Mock.StartUp.OpenAddTestEquipmentAssistantDefaultTestEquipmentModel);
            Assert.AreSame(availableTypes, environment.Mock.StartUp.OpenAddTestEquipmentAssistantParameterAvailableTypes);
            Assert.IsNull(environment.Mock.StartUp.OpenAddTestEquipmentAssistantDefaultTestEquipmentType);
            Assert.IsNull(environment.Mock.StartUp.OpenAddTestEquipmentAssistantDefaultTestEquipment);
        }

        [TestCase(TestEquipmentType.Wrench), RequiresThread(ApartmentState.STA)]
        [TestCase(TestEquipmentType.Analyse)]
        public void AddTestEquipmentCommandCallsOpenAddTestEquipmentAssistantWithTestEquipmentType(TestEquipmentType type)
        {
            var environment = new Environment();
            var availableTypes = new List<TestEquipmentType>();
            environment.Mock.UseCase.LoadAvailableTestEquipmentTypesReturnValue = availableTypes;
            environment.Mock.StartUp.OpenAddTestEquipmentAssistantReturnValue = new View.AssistentView("");
            environment.ViewModel.SelectedTestEquipmentType =new TestEquipmentTypeModel(null, type);
            environment.ViewModel.AddTestEquipmentCommand.Invoke(null);
            Assert.AreEqual(type, environment.Mock.StartUp.OpenAddTestEquipmentAssistantDefaultTestEquipmentType);
            Assert.AreSame(availableTypes, environment.Mock.StartUp.OpenAddTestEquipmentAssistantParameterAvailableTypes);
            Assert.IsNull(environment.Mock.StartUp.OpenAddTestEquipmentAssistantDefaultTestEquipmentModel);
            Assert.IsNull(environment.Mock.StartUp.OpenAddTestEquipmentAssistantDefaultTestEquipment);
        }

        [TestCase(TestEquipmentType.Analyse, false)]
        [TestCase(TestEquipmentType.Wrench, true)]
        [TestCase(TestEquipmentType.Bench, false)]
        public void IsAdditionalSettingVisibleReturnsCorrectValue(TestEquipmentType type, bool expectedValue)
        {
            var environment = new Environment();
            var testEquipment = CreateTestEquipment.Randomized(435346);
            testEquipment.TestEquipmentModel.Type = type;
            environment.Mock.InterfaceAdapter.SelectedTestEquipment = new TestEquipmentHumbleModel(testEquipment, null);

            Assert.AreEqual(expectedValue, environment.ViewModel.IsAdditionalSettingVisible);
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    UseCase = new TransferToTestEquipmentViewModelTest.TestEquipmentUseCaseMock();
                    InterfaceAdapter = new TransferToTestEquipmentViewModelTest.TestEquipmentInterfaceAdapterMock();
                    StartUp = new StartUpMock();
                    Validator = new TestEquipmentValidatorMock();
                }

                public readonly TransferToTestEquipmentViewModelTest.TestEquipmentUseCaseMock UseCase;
                public readonly TransferToTestEquipmentViewModelTest.TestEquipmentInterfaceAdapterMock InterfaceAdapter;
                public readonly StartUpMock StartUp;
                public readonly TestEquipmentValidatorMock Validator;
            }

            public Environment()
            {
                Mock = new Mocks();
                ViewModel = new TestEquipmentViewModel(Mock.UseCase, new NullLocalizationWrapper(), Mock.StartUp, Mock.InterfaceAdapter, null, Mock.Validator);
            }

            public readonly Mocks Mock;
            public readonly TestEquipmentViewModel ViewModel;
        }
    }
}