using System;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using InterfaceAdapters.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Client.Core.Diffs;
using Client.Core.Entities;
using Client.Core.Validator;
using Client.TestHelper.Factories;
using Client.UseCases.UseCases;
using InterfaceAdapters;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;
using System.Collections.ObjectModel;
using Client.TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    class ProcessControlViewModelTest
    {
        [Test]
        public void LoadCommandLoadsLocationTree()
        {
            var environment = new Environment();
            environment.ViewModel.LoadCommand.Invoke(null);

            AsyncCallCheckerNoAssuredTimeout.OnCallCheck(environment.Mock.LocationUseCase.LoadTreeCalled.Task, 0, () => Assert.Pass());
        }

        [Test]
        public void LoadCommandLoadsLoadTestLevelSets()
        {
            var environment = new Environment();
            environment.ViewModel.LoadCommand.Invoke(null);

            Assert.IsTrue(environment.Mock.TestLevelSetUseCase.LoadTestLevelSetsCalled);
            Assert.AreSame(environment.ViewModel, environment.Mock.TestLevelSetUseCase.LoadTestLevelSetsErrorHandler);
        }

        [Test]
        public void LoadCommandLoadsExtensions()
        {
            var environment = new Environment();
            environment.ViewModel.LoadCommand.Invoke(null);

            Assert.IsTrue(environment.Mock.ExtensionUseCase.ShowExtensionsCalled);
            Assert.AreSame(environment.ViewModel, environment.Mock.ExtensionUseCase.ShowExtensionsParameter);
        }

        [Test]        
        public void LocationTreeIsClearedWithShowLocationTree()
        {
            var environment = new Environment();

            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.ViewModel.LocationTree.LocationModels.Add(new LocationModel(null, new NullLocalizationWrapper(), null));
            environment.ViewModel.LocationTree.LocationModels.Add(new LocationModel(null, new NullLocalizationWrapper(), null));
            environment.ViewModel.LocationTree.LocationModels.Add(new LocationModel(null, new NullLocalizationWrapper(), null));

            environment.ViewModel.ShowLocationTree(new List<LocationDirectory>());

            Assert.AreEqual(0, environment.ViewModel.LocationTree.LocationModels.Count);
        }

        [Test]
        public void AddLocationInvokesInitializeLocationTreeRequest()
        {
            var environment = new Environment();

            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.ViewModel.InitializeLocationTreeRequest += (s, e) => Assert.Pass();
            environment.ViewModel.ShowLocationTree(new List<LocationDirectory>());
        }

        [Test]
        public void SelectLocationCallsUseCaseLoadProcessControlConditionForLocation()
        {
            var environment = new Environment();
            var location = CreateLocation.Randomized(213);
            environment.ViewModel.SelectedLocation = LocationModel.GetModelFor(location, null,null);

            Assert.AreSame(location, environment.Mock.UseCase.LoadProcessControlConditionForLocationParameter);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.LoadProcessControlConditionForLocationErrorHandler);
        }

        [Test]
        public void SelectLocationWithNullDontCallsUseCaseLoadProcessControlConditionForLocation()
        {
            var environment = new Environment();
            environment.ViewModel.SelectedLocation = null;

            Assert.IsNull(environment.Mock.UseCase.LoadProcessControlConditionForLocationParameter);
            Assert.IsNull(environment.Mock.UseCase.LoadProcessControlConditionForLocationErrorHandler);
        }

        [Test]
        public void ShowProblemLoadingLocationProcessControlConditionInvokesMessageBox()
        {
            var environment = new Environment();
            var messageBoxInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) => messageBoxInvoked = true;
            environment.ViewModel.ShowProblemLoadingLocationProcessControlCondition();
            Assert.IsTrue(messageBoxInvoked);
        }

        [Test]
        public void IsProcessControlConditionEnabledReturnsCorrectValue()
        {
            var environment = new Environment();
            Assert.IsFalse(environment.ViewModel.IsProcessControlConditionEnabled);
            environment.ViewModel.SelectedProcessControl = ProcessControlConditionHumbleModel.GetModelFor(CreateProcessControlCondition.Randomized(5435), null);
            Assert.IsTrue(environment.ViewModel.IsProcessControlConditionEnabled);
        }

        [Test]
        public void IsLocationParamEnabledReturnsCorrectValue()
        {
            var environment = new Environment();
            Assert.IsFalse(environment.ViewModel.IsLocationParamEnabled);
            environment.ViewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.Randomized(5435), null, null);
            Assert.IsTrue(environment.ViewModel.IsLocationParamEnabled);
        }

        [Test]
        public void IsQstStandardMethodExpandedReturnsCorrectValue()
        {
            var environment = new Environment();
            Assert.IsFalse(environment.ViewModel.IsQstStandardMethodExpanded);
            environment.ViewModel.SelectedProcessControl = ProcessControlConditionHumbleModel.GetModelFor(new ProcessControlCondition(), null);
            Assert.IsFalse(environment.ViewModel.IsQstStandardMethodExpanded);
            environment.ViewModel.SelectedProcessControl = ProcessControlConditionHumbleModel.GetModelFor(CreateProcessControlCondition.Randomized(5435), null);
            Assert.IsTrue(environment.ViewModel.IsQstStandardMethodExpanded);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsQstStandardMethodExpandedSetsCorrectValue(bool newValue)
        {
            var environment = new Environment();
            environment.ViewModel.SelectedProcessControl = ProcessControlConditionHumbleModel.GetModelFor(new ProcessControlCondition(), null);
            environment.ViewModel.IsQstStandardMethodExpanded = newValue;
            Assert.IsFalse(environment.ViewModel.IsQstStandardMethodExpanded);

            environment.ViewModel.SelectedProcessControl = ProcessControlConditionHumbleModel.GetModelFor(CreateProcessControlCondition.Randomized(5435), null);
            environment.ViewModel.IsQstStandardMethodExpanded = newValue;
            Assert.AreEqual(newValue, environment.ViewModel.IsQstStandardMethodExpanded);
        }

        [Test]
        public void ShowTestLevelSetErrorInvokesMessageBox()
        {
            var environment = new Environment();
            var messageBoxInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) => messageBoxInvoked = true;
            environment.ViewModel.ShowTestLevelSetError();
            Assert.IsTrue(messageBoxInvoked);
        }

        [Test]
        public void CreateProcessControlForLocationCanExecuteReturnsCorrectValue()
        {
            var environment = new Environment();
            Assert.IsFalse(environment.ViewModel.CreateProcessControlForLocationCommand.CanExecute(null));
            environment.ViewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.Randomized(324), new NullLocalizationWrapper(), null);
            Assert.IsTrue(environment.ViewModel.CreateProcessControlForLocationCommand.CanExecute(null));
            environment.ViewModel.SelectedProcessControl = ProcessControlConditionHumbleModel.GetModelFor(CreateProcessControlCondition.Randomized(5435), new NullLocalizationWrapper());
            Assert.IsFalse(environment.ViewModel.CreateProcessControlForLocationCommand.CanExecute(null));
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void CreateProcessControlCommandCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.ViewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.Randomized(324), new NullLocalizationWrapper(), null);
            environment.Mock.StartUp.OpenAddProcessControlAssistantReturnValue = new View.AssistentView("");
            environment.ViewModel.CreateProcessControlForLocationCommand.Invoke(null);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void CreateProcessControlCommandCallsOpenAddProcessControlAssistant()
        {
            var environment = new Environment();
            environment.Mock.StartUp.OpenAddProcessControlAssistantReturnValue = new View.AssistentView("");
            var location = CreateLocation.Randomized(324);
            environment.ViewModel.SelectedLocation = LocationModel.GetModelFor(location, new NullLocalizationWrapper(), null);
            environment.ViewModel.CreateProcessControlForLocationCommand.Execute(null);
            Assert.AreSame(location, environment.Mock.StartUp.OpenAddProcessControlAssistantParameter);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void CreateProcessControlCommandShowsDialogRequest()
        {
            var environment = new Environment();
            environment.Mock.StartUp.OpenAddProcessControlAssistantReturnValue = new View.AssistentView("");
            var location = CreateLocation.Randomized(324);
            environment.ViewModel.SelectedLocation = LocationModel.GetModelFor(location, new NullLocalizationWrapper(), null);

            var dialogShowed = false;
            environment.ViewModel.ShowDialogRequest += (s, e) => dialogShowed = true;

            environment.ViewModel.CreateProcessControlForLocationCommand.Execute(null);
            
            Assert.IsTrue(dialogShowed);
        }

        [Test]
        public void SaveProcessControlCanExecuteReturnsCorrectValue()
        {
            var environment = new Environment();
            environment.Mock.ProcessControlConditionValidator.ValidateReturnValue = true;
            environment.Mock.InterfaceAdapter.SelectedProcessControl = null;

            Assert.IsFalse(environment.ViewModel.SaveProcessControlCommand.CanExecute(null));

            environment.Mock.InterfaceAdapter.SelectedProcessControl = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = environment.Mock.InterfaceAdapter.SelectedProcessControl.CopyDeep();

            Assert.IsFalse(environment.ViewModel.SaveProcessControlCommand.CanExecute(null));

            environment.Mock.InterfaceAdapter.SelectedProcessControl.LowerInterventionLimit = environment.Mock.InterfaceAdapter.SelectedProcessControl.LowerInterventionLimit + 1;

            Assert.IsTrue(environment.ViewModel.SaveProcessControlCommand.CanExecute(null));

            environment.Mock.ProcessControlConditionValidator.ValidateReturnValue = false;

            Assert.IsFalse(environment.ViewModel.SaveProcessControlCommand.CanExecute(null));
        }

        [Test]
        public void SaveProcessControlCanExecuteCallsValidator()
        {
            var environment = new Environment();
            environment.Mock.ProcessControlConditionValidator.ValidateReturnValue = true;
            environment.Mock.InterfaceAdapter.SelectedProcessControl = null;

            environment.Mock.InterfaceAdapter.SelectedProcessControl = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(43545657), null);
            environment.ViewModel.SaveProcessControlCommand.CanExecute(null);

            Assert.AreSame(environment.Mock.InterfaceAdapter.SelectedProcessControl.Entity, environment.Mock.ProcessControlConditionValidator.ValidateParameter);
        }

        [Test]
        public void SaveProcessControlExecuteCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedProcessControl = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);
            environment.ViewModel.SaveProcessControlCommand.Execute(null);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test]
        public void SaveProcessControlExecuteCallsUseCase()
        {
            var environment = new Environment();
            var oldControl = CreateProcessControlCondition.Randomized(12324);
            var newControl = CreateProcessControlCondition.Randomized(12324);
            environment.Mock.InterfaceAdapter.SelectedProcessControl = new ProcessControlConditionHumbleModel(newControl, null);
            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = new ProcessControlConditionHumbleModel(oldControl, null);
            environment.ViewModel.SaveProcessControlCommand.Execute(null);
            Assert.AreSame(oldControl, environment.Mock.UseCase.SaveProcessControlConditionParameterDiff.First().GetOldProcessControlCondition());
            Assert.AreSame(newControl, environment.Mock.UseCase.SaveProcessControlConditionParameterDiff.First().GetNewProcessControlCondition());
            Assert.IsNull(environment.Mock.UseCase.SaveProcessControlConditionParameterDiff.First().User);
            Assert.IsNull(environment.Mock.UseCase.SaveProcessControlConditionParameterDiff.First().Comment);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.SaveProcessControlConditionParameterErrorHandler);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.SaveProcessControlConditionParameterDiffSaveGuiShower);
        }

        [Test]
        public void RemoveProcessControlForLocationCanExecuteReturnsCorrectValue()
        {
            var environment = new Environment();
            Assert.IsFalse(environment.ViewModel.RemoveProcessControlForLocationCommand.CanExecute(null));
            environment.ViewModel.SelectedProcessControl = ProcessControlConditionHumbleModel.GetModelFor(CreateProcessControlCondition.Randomized(5435), new NullLocalizationWrapper());
            Assert.IsTrue(environment.ViewModel.RemoveProcessControlForLocationCommand.CanExecute(null));
        }

        [Test]
        public void RemoveProcessControlForLocationCommandCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(684543), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControl = environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges.CopyDeep();

            environment.ViewModel.RemoveProcessControlForLocationCommand.Invoke(null);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test]
        public void RemoveProcessControlForLocationCommandCancelTest()
        {
            var environment = new Environment();
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.Mock.InterfaceAdapter.SelectedProcessControl = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(684543), null);

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.No);
            };

            environment.ViewModel.RemoveProcessControlForLocationCommand.Invoke(null);

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsFalse(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.IsNull(environment.Mock.UseCase.RemoveProcessControlConditionArgument);
        }

        [Test]
        public void RemoveProcessControlForLocationCommandYesTest()
        {
            var environment = new Environment();
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.Mock.InterfaceAdapter.SelectedProcessControl = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(684543), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.Yes);
            };

            environment.ViewModel.RemoveProcessControlForLocationCommand.Invoke(null);

            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsTrue(
                environment.Mock.InterfaceAdapter.SelectedProcessControl.EqualsByContent(environment.Mock
                    .InterfaceAdapter.SelectedProcessControlWithoutChanges));

            Assert.AreSame(environment.Mock.InterfaceAdapter.SelectedProcessControl.Entity, environment.Mock.UseCase.RemoveProcessControlConditionArgument);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.RemoveProcessControlConditionErrorHandlerArgument);
        }

        [Test]
        public void AvailableTestLevelSetNumbersReturnsCorrectValue()
        {
            var environment = new Environment();
            environment.ViewModel.SelectedProcessControl = ProcessControlConditionHumbleModel.GetModelFor(CreateProcessControlCondition.Randomized(5435), new NullLocalizationWrapper());
            environment.ViewModel.SelectedProcessControl.TestLevelSet.IsActive2 = false;
            environment.ViewModel.SelectedProcessControl.TestLevelSet.IsActive3 = false;
            Assert.AreEqual(new List<int>() {1}, environment.ViewModel.AvailableTestLevelSetNumbers.ToList());

            environment.ViewModel.SelectedProcessControl.TestLevelSet.IsActive2 = true;
            Assert.AreEqual(new List<int>() { 1, 2 }, environment.ViewModel.AvailableTestLevelSetNumbers.ToList());

            environment.ViewModel.SelectedProcessControl.TestLevelSet.IsActive3 = true;
            Assert.AreEqual(new List<int>() { 1, 2, 3 }, environment.ViewModel.AvailableTestLevelSetNumbers.ToList());
        }

        [Test]
        public void SaveProcessControlDontShowChangesIfProcessControlConditionHasNoChanges()
        {
            var environment = new Environment();
            environment.ViewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.Randomized(5435), null, null);
            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) => showDialogRequestInvoked = true;

            var processControlCondition = CreateProcessControlCondition.Randomized(12324);
            var diff = new ProcessControlConditionDiff(null, null, processControlCondition, processControlCondition);
            environment.ViewModel.SaveProcessControl(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { diff }, () => { });

            Assert.IsFalse(showDialogRequestInvoked);
        }


        [Test]
        public void SaveProcessControlWithNoResultTest()
        {
            var environment = new Environment();
            environment.ViewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.Randomized(5435), null, null);
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControl = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(684543), null);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };
            var oldProcessControlCondition = CreateProcessControlCondition.Randomized(12324);
            var newProcessControlCondition = CreateProcessControlCondition.Randomized(6578);

            var diff = new ProcessControlConditionDiff(null, null, oldProcessControlCondition, newProcessControlCondition);

            environment.ViewModel.SaveProcessControl(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { diff }, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsFalse(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.IsTrue(environment.Mock.InterfaceAdapter.SelectedProcessControl.EqualsByContent(environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges));
        }

        [Test]
        public void SaveProcessControlWithCancelResultTest()
        {
            var environment = new Environment();
            environment.ViewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.Randomized(5435), null, null);
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControl = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(684543), null);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Cancel;
            };
            var oldProcessControlCondition = CreateProcessControlCondition.Randomized(12324);
            var newProcessControlCondition = CreateProcessControlCondition.Randomized(6578);

            var diff = new ProcessControlConditionDiff(null, null, oldProcessControlCondition, newProcessControlCondition);

            environment.ViewModel.SaveProcessControl(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { diff }, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsFalse(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.IsFalse(environment.Mock.InterfaceAdapter.SelectedProcessControl.EqualsByContent(environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges));
        }

        [Test]
        public void SaveProcessControlWithYesResultCallsFinishedAction()
        {
            var environment = new Environment();
            environment.ViewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.Randomized(5435), null, null);
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;
            };

            var oldProcessControlCondition = CreateProcessControlCondition.Randomized(12324);
            var newProcessControlCondition = CreateProcessControlCondition.Randomized(6578);

            var diff = new ProcessControlConditionDiff(null, null, oldProcessControlCondition, newProcessControlCondition);
            var finishedActionCalled = false;
            environment.ViewModel.SaveProcessControl(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { diff }, () => { finishedActionCalled = true; });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(finishedActionCalled);
        }

        [Test]
        public void CanCloseReturnsTrueWhenNothingIsSelected()
        {
            var environment = new Environment();
            Assert.IsTrue(environment.ViewModel.CanClose());
        }


        [Test]
        public void CanCloseCallsValidatorWithSelectedProcessControlCondition()
        {
            var environment = new Environment();
            var selectedProcessControlCondition = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControl = selectedProcessControlCondition;

            environment.ViewModel.CanClose();

            Assert.AreSame(selectedProcessControlCondition.Entity, environment.Mock.ProcessControlConditionValidator.ValidateParameter);
        }

        [Test]
        public void CanCloseCloseWithInvalidProcessControlConditionContinueEditing()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedProcessControl = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(684543), null);
            environment.Mock.ProcessControlConditionValidator.ValidateReturnValue = false;

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
        public void CanCloseCloseWithInvalidProcessControlConditionDontContinueEditing()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedProcessControl = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(684543), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(684543), null);
            environment.Mock.ProcessControlConditionValidator.ValidateReturnValue = false;

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.No);
            };

            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsNull(environment.Mock.InterfaceAdapter.SelectedProcessControl);
            Assert.IsTrue(result);
        }

        [Test]
        public void CanCloseWithChangedProcessControlConditionCancelTest()
        {
            var environment = new Environment();
            environment.ViewModel.SelectedLocation = new LocationModel(CreateLocation.Anonymous(), null, null);
            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);
            var processControlCondition = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(684543), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControl = processControlCondition;

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Cancel;
            };

            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.AreSame(processControlCondition, environment.ViewModel.SelectedProcessControl);
            Assert.IsFalse(result);
        }

        [Test]
        public void CanCloseWithChangedProcessControlConditionYesTest()
        {
            var environment = new Environment();
            environment.ViewModel.SelectedLocation = new LocationModel(CreateLocation.Anonymous(), null, null);
            var oldProcessControlCondition = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);
            var changedProcessControlCondition = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(684543), null);

            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = oldProcessControlCondition;
            environment.Mock.InterfaceAdapter.SelectedProcessControl = changedProcessControlCondition;

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;
            };

            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.AreSame(oldProcessControlCondition.Entity, environment.Mock.UseCase.UpdateProcessControlConditionParameterDiff.First().GetOldProcessControlCondition());
            Assert.AreSame(changedProcessControlCondition.Entity, environment.Mock.UseCase.UpdateProcessControlConditionParameterDiff.First().GetNewProcessControlCondition());
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.UpdateProcessControlConditionParameterErrorHandler);
            Assert.IsNull(environment.Mock.InterfaceAdapter.SelectedProcessControl);
            Assert.IsTrue(result);
        }

        [Test]
        public void CanCloseCloseWithChangedProcessControlConditionNoTest()
        {
            var environment = new Environment();
            environment.ViewModel.SelectedLocation = new LocationModel(CreateLocation.Anonymous(), null, null);
            environment.ViewModel.SelectedLocation = new LocationModel(CreateLocation.Anonymous(), null, null);
            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControl = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(684543), null);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };

            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsNull(environment.Mock.InterfaceAdapter.SelectedProcessControl);
            Assert.IsTrue(result);
        }

        [Test]
        public void CanCloseCloseWithValidAndNotChangedTest()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControl = environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges.CopyDeep();

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };

            var selectionRequestInvoked = false;
            environment.ViewModel.SelectionRequestLocation += (s, e) => selectionRequestInvoked = true;

            var result = environment.ViewModel.CanClose();

            Assert.IsFalse(showDialogRequestInvoked);
            Assert.IsFalse(selectionRequestInvoked);
            Assert.IsNull(environment.Mock.InterfaceAdapter.SelectedProcessControl);
            Assert.IsTrue(result);
        }
        
        [Test]
        public void SelectNewLocationCallsValidatorWithSelectedProcessControlCondition()
        {
            var environment = new Environment();
            var selectedProcessControlCondition = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControl = selectedProcessControlCondition;

            environment.ViewModel.SelectedLocation = new LocationModel(CreateLocation.Anonymous(), null, null);

            Assert.AreSame(selectedProcessControlCondition.Entity, environment.Mock.ProcessControlConditionValidator.ValidateParameter);
        }

        [Test]
        public void SelectNewLocationWithInvalidProcessControlConditionContinueEditing()
        {
            var environment = new Environment();
            var oldProcessControlCondition = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(684543), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControl = oldProcessControlCondition;
            var oldLocation = new LocationModel(CreateLocation.IdOnly(435457658), null, null);
            environment.ViewModel.SelectedLocation = oldLocation;
            environment.Mock.ProcessControlConditionValidator.ValidateReturnValue = false;

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.Yes);
            };

            var selectionRequestInvoked = false;
            environment.ViewModel.SelectionRequestLocation += (s, e) => selectionRequestInvoked = true;

            environment.ViewModel.SelectedLocation = new LocationModel(CreateLocation.Anonymous(), null, null);

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsTrue(selectionRequestInvoked);
            Assert.AreSame(oldProcessControlCondition, environment.ViewModel.SelectedProcessControl);
            Assert.AreSame(oldLocation, environment.ViewModel.SelectedLocation);
        }

        [Test]
        public void SelectNewLocationWithInvalidProcessControlConditionDontContinueEditing()
        {
            var environment = new Environment();
            var oldProcessControlCondition = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);
            var changedProcessControlCondition = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(684543), null);

            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = oldProcessControlCondition;
            environment.Mock.InterfaceAdapter.SelectedProcessControl = changedProcessControlCondition;

            environment.Mock.ProcessControlConditionValidator.ValidateReturnValue = false;

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.No);
            };

            var selectionRequestInvoked = false;
            environment.ViewModel.SelectionRequestLocation += (s, e) => selectionRequestInvoked = true;

            var newLocation =new LocationModel(CreateLocation.Anonymous(), null, null);
            environment.ViewModel.SelectedLocation = newLocation;

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsFalse(selectionRequestInvoked);
            Assert.IsTrue(environment.ViewModel.SelectedProcessControl.EqualsByContent(oldProcessControlCondition));
            Assert.AreSame(newLocation, environment.ViewModel.SelectedLocation);
        }

        [Test]
        public void SelectNewLocationWithChangedProcessControlConditionCancelTest()
        {
            var environment = new Environment();
            environment.ViewModel.SelectedLocation = new LocationModel(CreateLocation.Randomized(34532), null, null);
            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControl = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(456), null);
            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Cancel;
            };

            var selectionRequestInvoked = false;
            environment.ViewModel.SelectionRequestLocation += (s, e) => selectionRequestInvoked = true;

            var selectedLocation = new LocationModel(CreateLocation.Randomized(684543), null, null);
            environment.ViewModel.SelectedLocation = selectedLocation;

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.AreNotSame(environment.ViewModel.SelectedLocation, selectedLocation);
            Assert.IsTrue(selectionRequestInvoked);
        }

        [Test]
        public void SelectNewLocationWithChangedProcessControlConditionYesTest()
        {
            var environment = new Environment();
            environment.ViewModel.SelectedLocation = new LocationModel(CreateLocation.Randomized(34532), null, null);
            var oldProcessControlCondition = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);
            var changedProcessControlCondition = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(684543), null);

            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = oldProcessControlCondition;
            environment.Mock.InterfaceAdapter.SelectedProcessControl = changedProcessControlCondition;

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;
            };

            var newLocation = new LocationModel(CreateLocation.Randomized(546), null, null);
            environment.ViewModel.SelectedLocation = newLocation;

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.AreSame(oldProcessControlCondition.Entity, environment.Mock.UseCase.UpdateProcessControlConditionParameterDiff.First().GetOldProcessControlCondition());
            Assert.AreSame(changedProcessControlCondition.Entity, environment.Mock.UseCase.UpdateProcessControlConditionParameterDiff.First().GetNewProcessControlCondition());
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.UpdateProcessControlConditionParameterErrorHandler);
            Assert.AreSame(newLocation, environment.ViewModel.SelectedLocation);
        }

        [Test]
        public void SelectNewLocationWithChangedProcessControlConditionNoTest()
        {
            var environment = new Environment();
            environment.ViewModel.SelectedLocation = new LocationModel(CreateLocation.Randomized(123111), null, null);
            var oldProcessControlCondition = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControlWithoutChanges = oldProcessControlCondition;
            environment.Mock.InterfaceAdapter.SelectedProcessControl = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(34523), null);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };

           var newLocation = new LocationModel(CreateLocation.Randomized(11111), null, null);
           environment.ViewModel.SelectedLocation = newLocation;

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(environment.Mock.InterfaceAdapter.SelectedProcessControl.EqualsByContent(oldProcessControlCondition));
            Assert.AreSame(newLocation, environment.ViewModel.SelectedLocation);
        }

        private static IEnumerable<Extension> Extensions = new List<Extension>()
        {
            CreateExtension.Randomized(43536),
            null,
            CreateExtension.Randomized(34343)
        };

        [TestCaseSource(nameof(Extensions))]
        public void SelectedExtensionReturnsCorrectValue(Extension extension)
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedProcessControl = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(34523), null);
            environment.Mock.InterfaceAdapter.SelectedProcessControl.Entity.ProcessControlTech.Extension = extension;
            Assert.AreSame(extension, environment.ViewModel.SelectedExtension?.Entity);
        }

        [TestCaseSource(nameof(Extensions))]
        public void SelectedExtensionSetsCorrectValue(Extension extension)
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedProcessControl = new ProcessControlConditionHumbleModel(CreateProcessControlCondition.Randomized(34523), null);
            environment.ViewModel.SelectedExtension = ExtensionModel.GetModelFor(extension, new NullLocalizationWrapper());
            Assert.AreSame(extension, environment.Mock.InterfaceAdapter.SelectedProcessControl.Entity.ProcessControlTech.Extension);
        }

        public class ProcessControlInterfaceAdapterMock : IProcessControlInterface
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public ProcessControlConditionHumbleModel SelectedProcessControl { get; set; }
            public ProcessControlConditionHumbleModel SelectedProcessControlWithoutChanges { get; set; }
            public ObservableCollection<ProcessControlConditionHumbleModel> ProcessControlConditions { get; set; }
            public ObservableCollection<ProcessControlConditionHumbleModel> SelectedProcessControlConditions { get; set; }

            public event EventHandler<bool> ShowLoadingControlRequest;
        }

        public class ProcessControlConditionValidatorMock : IProcessControlConditionValidator
        {
            public bool Validate(ProcessControlCondition processControlCondition)
            {
                ValidateParameter = processControlCondition;
                return ValidateReturnValue;
            }

            public bool ValidateReturnValue { get; set; } = true;

            public ProcessControlCondition ValidateParameter { get; set; }
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    UseCase = new ProcessControlUseCaseMock();
                    LocationUseCase = new LocationUseCaseMock(null);
                    InterfaceAdapter = new ProcessControlInterfaceAdapterMock();
                    StartUp = new StartUpMock();
                    TestLevelSetUseCase = new TestLevelSetUseCaseMock();
                    TestLevelSetInterface = new TestLevelSetInterfaceMock();
                    ProcessControlConditionValidator = new ProcessControlConditionValidatorMock();
                    ExtensionInterfaceAdapter = new ExtensionViewModelTest.ExtensionInterfaceAdapterMock();
                    ExtensionUseCase = new ExtensionViewModelTest.ExtensionUseCaseMock();
                }

                public readonly ProcessControlUseCaseMock UseCase;
                public readonly LocationUseCaseMock LocationUseCase;
                public readonly ProcessControlInterfaceAdapterMock InterfaceAdapter;
                public readonly ExtensionViewModelTest.ExtensionInterfaceAdapterMock ExtensionInterfaceAdapter;
                public readonly ExtensionViewModelTest.ExtensionUseCaseMock ExtensionUseCase;
                public readonly StartUpMock StartUp;
                public readonly TestLevelSetUseCaseMock TestLevelSetUseCase;
                public readonly TestLevelSetInterfaceMock TestLevelSetInterface;
                public readonly ProcessControlConditionValidatorMock ProcessControlConditionValidator;
            }

            public Environment()
            {
                Mock = new Mocks();
                ViewModel = new ProcessControlViewModel(Mock.UseCase, Mock.LocationUseCase, Mock.StartUp, new NullLocalizationWrapper(), Mock.InterfaceAdapter,
                    Mock.TestLevelSetUseCase, Mock.TestLevelSetInterface, Mock.ProcessControlConditionValidator, Mock.ExtensionInterfaceAdapter, Mock.ExtensionUseCase);
            }

            public readonly Mocks Mock;
            public readonly ProcessControlViewModel ViewModel;
        }
    }
}
