using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.Validator;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using FrameworksAndDrivers.Threads;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Core;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;
using Interval = Core.Entities.Interval;
using TestParameters = Core.Entities.TestParameters;
using ToolModel = Core.Entities.ToolModel;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    class LocationToolAssignmentValidatorMock : ILocationToolAssignmentValidtor
    {
        public int ValidateCallCount { get; internal set; }
        public bool ValidateResult = true;

        public bool Validate(LocationToolAssignmentModel locationToolAssignmentModel)
        {
            ValidateCallCount++;
            return ValidateResult;
        }
    }
    class LocationToolAssignmentViewModelTest
    {
        [Test]
        public void AddLocationInvokesInitializeLocationTreeRequest()
        {
            var viewModel =
                CreateLocationToolAssignmentViewModel();

            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.InitializeLocationTreeRequest += (s, e) => Assert.Pass();
            viewModel.ShowLocationTree(new List<LocationDirectory>());
        }

        [Test]
        public void LocationTreeIsClearedWithShowLocationTree()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();

            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.LocationTree.LocationModels.Add(new LocationModel(null, new NullLocalizationWrapper(), null));
            viewModel.LocationTree.LocationModels.Add(new LocationModel(null, new NullLocalizationWrapper(), null));
            viewModel.LocationTree.LocationModels.Add(new LocationModel(null, new NullLocalizationWrapper(), null));

            viewModel.ShowLocationTree(new List<LocationDirectory>());

            Assert.AreEqual(0, viewModel.LocationTree.LocationModels.Count);
        }

        [Test]
        public void LoadCommandLoadsLocationTree()
        {
            var locationUseCase = new LocationUseCaseMock(null);
            var toolUseCase = new ToolUseCaseMock(null, null, null, null, null, null, null, null, null, null);
            var viewModel = CreateLocationToolAssignmentViewModel(locationUseCase: locationUseCase, toolUseCase: toolUseCase, toleranceClassUseCase: new ToleranceClassUseCaseMock(null, null, null, null));
            viewModel.LoadCommand.Invoke(null);

            AsyncCallCheckerNoAssuredTimeout.OnCallCheck(locationUseCase.LoadTreeCalled.Task, 0, () => Assert.Pass());
        }

        [Test]
        public void LoadCommandLoadsTestLevelSets()
        {
            var locationUseCase = new LocationUseCaseMock(null);
            var toolUseCase = new ToolUseCaseMock(null, null, null, null, null, null, null, null, null, null);
            var testLevelSetUseCase = new TestLevelSetUseCaseMock();
            var viewModel = CreateLocationToolAssignmentViewModel(
                locationUseCase: locationUseCase, 
                toolUseCase: toolUseCase, 
                toleranceClassUseCase: new ToleranceClassUseCaseMock(null, null, null, null),
                testLevelSetUseCase:testLevelSetUseCase,
                testLevelSetInterface:new TestLevelSetInterfaceMock());
            viewModel.LoadCommand.Invoke(null);

            Assert.IsTrue(testLevelSetUseCase.LoadTestLevelSetsCalled);
            Assert.AreSame(viewModel, testLevelSetUseCase.LoadTestLevelSetsErrorHandler);
        }

        [Test]
        public void ChangeTestLevelSetsInInterfaceAdapterInvokesPropertyChanged()
        {
            var locationUseCase = new LocationUseCaseMock(null);
            var toolUseCase = new ToolUseCaseMock(null, null, null, null, null, null, null, null, null, null);
            var testLevelSetUseCase = new TestLevelSetUseCaseMock();
            var testLevelSetInterface = new TestLevelSetInterfaceMock();
            var viewModel = CreateLocationToolAssignmentViewModel(
                locationUseCase: locationUseCase,
                toolUseCase: toolUseCase,
                toleranceClassUseCase: new ToleranceClassUseCaseMock(null, null, null, null),
                testLevelSetUseCase: testLevelSetUseCase,
                testLevelSetInterface: testLevelSetInterface);

            var invoked = false;
            viewModel.PropertyChanged += (s, e) => invoked = e.PropertyName == nameof(LocationToolAssignmentViewModel.AvailableTestLevelSets);
            testLevelSetInterface.TestLevelSets = new ObservableCollection<TestLevelSetModel>();

            Assert.IsTrue(invoked);
        }

        [Test]
        public void ShowTestLevelSetErrorCallsMessageBoxRequest()
        {
            var locationUseCase = new LocationUseCaseMock(null);
            var toolUseCase = new ToolUseCaseMock(null, null, null, null, null, null, null, null, null, null);
            var testLevelSetUseCase = new TestLevelSetUseCaseMock();
            var testLevelSetInterface = new TestLevelSetInterfaceMock();
            var viewModel = CreateLocationToolAssignmentViewModel(
                locationUseCase: locationUseCase,
                toolUseCase: toolUseCase,
                toleranceClassUseCase: new ToleranceClassUseCaseMock(null, null, null, null),
                testLevelSetUseCase: testLevelSetUseCase,
                testLevelSetInterface: testLevelSetInterface);

            var invoked = false;
            viewModel.MessageBoxRequest += (s, e) => invoked = true;
            viewModel.ShowTestLevelSetError();

            Assert.IsTrue(invoked);
        }

        [Test]
        public void ShowModelsWithAtLeastOneToolAddsToCollection()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);

            const long id = 987654324789;

            var toolModel = new ToolModel() {Id = new ToolModelId(id)};
            viewModel.ShowModelsWithAtLeastOneTool(new List<ToolModel>() {toolModel});

            Assert.AreEqual(1, viewModel.AllToolModelModels.Count);
            Assert.AreEqual(id, viewModel.AllToolModelModels[0].Id);
        }

        [Test]
        public void ShowModelsWithAtLeastOneToolWithPreviousLoadedToolModelsResetsCollection()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);

            viewModel.AllToolModelModels.Add(new ToolModelModel(new ToolModel(), new NullLocalizationWrapper()));
            viewModel.AllToolModelModels.Add(new ToolModelModel(new ToolModel(), new NullLocalizationWrapper()));
            viewModel.AllToolModelModels.Add(new ToolModelModel(new ToolModel(), new NullLocalizationWrapper()));
            viewModel.AllToolModelModels.Add(new ToolModelModel(new ToolModel(), new NullLocalizationWrapper()));

            const long id = 987654324789;

            var toolModel = new ToolModel() {Id = new ToolModelId(id)};
            viewModel.ShowModelsWithAtLeastOneTool(new List<ToolModel>() {toolModel});

            Assert.AreEqual(1, viewModel.AllToolModelModels.Count);
            Assert.AreEqual(id, viewModel.AllToolModelModels[0].Id);
        }

        [Test]
        public void ShowToolsAddsToCollection()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);

            const long id = 987654324789;

            var tool = new Tool() {Id = new ToolId(id)};
            viewModel.ShowTools(new List<Tool>() {tool});

            Assert.AreEqual(1, viewModel.AllToolModels.Count);
            Assert.AreEqual(id, viewModel.AllToolModels[0].Id);
        }

        [Test]
        public void LoadCommandLoadsToolModelsTree()
        {
            var toolUseCase = new ToolUseCaseMock(null, null, null, null, null, null, null, null, null, null);
            var locationUseCase = new LocationUseCaseMock(null);
            var viewModel = CreateLocationToolAssignmentViewModel(locationUseCase: locationUseCase, toolUseCase: toolUseCase, toleranceClassUseCase: new ToleranceClassUseCaseMock(null, null, null, null));
            viewModel.LoadCommand.Invoke(null);

            Assert.IsTrue(toolUseCase.WasLoadModelsWithAtLeastOneToolCalled);
        }

        [Test]
        public void AssignToolToLocationCanExecuteWithNoSelectedLocationReturnsFalse()
        {
            var toolUseCase = new ToolUseCaseMock(null, null, null, null, null, null, null, null, null, null);
            var locationUseCase = new LocationUseCaseMock(null);
            var viewModel = CreateLocationToolAssignmentViewModel(locationUseCase: locationUseCase, toolUseCase: toolUseCase);

            viewModel.SelectedLocation = null;
            viewModel.SelectedTool = new InterfaceAdapters.Models.ToolModel(new Tool(), new NullLocalizationWrapper());

            Assert.IsFalse(viewModel.AssignToolToLocationCommand.CanExecute(null));
        }

        [Test]
        public void AssignToolToLocationCanExecuteWithNoSelectedToolReturnsFalse()
        {
            var toolUseCase = new ToolUseCaseMock(null, null, null, null, null, null, null, null, null, null);
            var locationUseCase = new LocationUseCaseMock(null);
            var viewModel = CreateLocationToolAssignmentViewModel(locationUseCase: locationUseCase, toolUseCase: toolUseCase);

            viewModel.SelectedLocation = new LocationModel(new Location(), new NullLocalizationWrapper(), null);
            viewModel.SelectedTool = null;

            Assert.IsFalse(viewModel.AssignToolToLocationCommand.CanExecute(null));
        }

        [Test]
        public void SetSelectedLocationCallsUseCase()
        {
            var useCase = new LocationToolAssignmentUseCaseMock();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentUseCase: useCase);

            const long id = 8946195;
            viewModel.SelectedLocation = new LocationModel(new Location(), new NullLocalizationWrapper(), null)
            {
                Id = id
            };

            Assert.AreEqual(id, useCase.LoadAssignedToolsForLocationParameter.Id.ToLong());
        }

        [Test]
        public void SetSelectedLocationToNullClearsAssignmentsForSelectedLocation()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();

            viewModel.AssignmentsForSelectedLocation.Add(
                new LocationToolAssignmentModel(new LocationToolAssignment(), new NullLocalizationWrapper()));
            viewModel.AssignmentsForSelectedLocation.Add(
                new LocationToolAssignmentModel(new LocationToolAssignment(), new NullLocalizationWrapper()));
            viewModel.AssignmentsForSelectedLocation.Add(
                new LocationToolAssignmentModel(new LocationToolAssignment(), new NullLocalizationWrapper()));

            viewModel.SelectedLocation = null;

            Assert.AreEqual(0, viewModel.AssignmentsForSelectedLocation.Count);
        }

        [Test]
        public void ShowAssignedToolsForLocationFillsAssignmentsForSelectedLocation()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var assignment1 = new LocationToolAssignment()
            {
                AssignedLocation = new Location() {Id = new LocationId(9517)}
            };
            var assignment2 = new LocationToolAssignment()
            {
                AssignedTool = new Tool() {Id = new ToolId(9863126478)}
            };

            viewModel.ShowAssignedToolsForLocation(new List<LocationToolAssignment>() {assignment1, assignment2});

            Assert.AreEqual(2, viewModel.AssignmentsForSelectedLocation.Count);
            Assert.AreEqual(assignment1.AssignedLocation.Id.ToLong(),
                viewModel.AssignmentsForSelectedLocation[0].AssignedLocation.Id);
            Assert.AreEqual(assignment2.AssignedTool.Id.ToLong(),
                viewModel.AssignmentsForSelectedLocation[1].AssignedTool.Id);
        }

        [Test]
        public void AssignToolToLocationAddsToAssignmentsForSelectedLocation()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);

            var assignment = new LocationToolAssignment()
            {
                Id = new LocationToolAssignmentId(15),
                AssignedLocation = new Location() {Id = new LocationId(9517)},
                AssignedTool = new Tool() {Id = new ToolId(9863126478)}
            };

            viewModel.AssignToolToLocation(assignment);

            Assert.AreEqual(1, viewModel.AssignmentsForSelectedLocation.Count);
            Assert.AreEqual(assignment.AssignedLocation.Id.ToLong(),
                viewModel.AssignmentsForSelectedLocation[0].AssignedLocation.Id);
            Assert.AreEqual(assignment.AssignedTool.Id.ToLong(),
                viewModel.AssignmentsForSelectedLocation[0].AssignedTool.Id);
        }

        [Test]
        public void UpdateToolUpdatesRightModel()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            var tool = new Tool()
            {
                Id = new ToolId(987465547),
                Status = new Status()
                    {ListId = new HelperTableEntityId(321654), Value = new StatusDescription(" bjeo0ßhpyfajivmadöofo")}
            };
            var model = new InterfaceAdapters.Models.ToolModel(new Tool(), new NullLocalizationWrapper())
            {
                Id = tool.Id.ToLong(),
                Status = HelperTableItemModel.GetModelForStatus(new Status()
                {
                    ListId = new HelperTableEntityId(1478963258),
                    Value = new StatusDescription("t4ßhdu9bnv öpodtu7 qz+ ")
                })
            };

            viewModel.AllToolModels.Add(model);

            viewModel.UpdateTool(tool);

            Assert.AreEqual(tool.Status.ListId.ToLong(), model.Status.ListId);
            Assert.AreEqual(tool.Status.Value.ToDefaultString(), model.Status.Value);
        }

        [Test]
        public void AssignToolToLocationErrorCallsMessageBoxRequest()
        {
            var wasMessageBoxRequestCalled = false;
            var viewModel = CreateLocationToolAssignmentViewModel();

            viewModel.MessageBoxRequest += (s, e) => wasMessageBoxRequestCalled = true;
            viewModel.AssignToolToLocationError();

            Assert.IsTrue(wasMessageBoxRequestCalled);
        }

        [Test]
        public void LoadAssignedToolsForLocationErrorCallsMessageBoxRequest()
        {
            var wasMessageBoxRequestCalled = false;
            var viewModel = CreateLocationToolAssignmentViewModel();

            viewModel.MessageBoxRequest += (s, e) => wasMessageBoxRequestCalled = true;
            viewModel.LoadAssignedToolsForLocationError();

            Assert.IsTrue(wasMessageBoxRequestCalled);
        }

        [Test]
        public void AddTestConditionsErrorCallsMessageBoxRequest()
        {
            var wasMessageBoxRequestCalled = false;
            var viewModel = CreateLocationToolAssignmentViewModel();

            viewModel.MessageBoxRequest += (s, e) => wasMessageBoxRequestCalled = true;
            viewModel.AddTestConditionsError();

            Assert.IsTrue(wasMessageBoxRequestCalled);
        }

        [Test]
        public void AssignToolToLocationCanExecuteWithNoExistingAssignmentReturnsFalse()
        {
            var toolUseCase = new ToolUseCaseMock(null, null, null, null, null, null, null, null, null, null);
            var locationUseCase = new LocationUseCaseMock(null);
            var viewModel = CreateLocationToolAssignmentViewModel(toolUseCase: toolUseCase, locationUseCase: locationUseCase);

            viewModel.SelectedLocationToolAssignmentModel = null;

            Assert.IsFalse(viewModel.AddTestConditionsCommand.CanExecute(null));
        }

        [Test]
        public void AssignToolToLocationCanExecuteWithExistingButAlreadyFilledAssignmentReturnsFalse()
        {
            var toolUseCase = new ToolUseCaseMock(null, null, null, null, null, null, null, null, null, null);
            var locationUseCase = new LocationUseCaseMock(null);
            var viewModel = CreateLocationToolAssignmentViewModel(toolUseCase: toolUseCase, locationUseCase: locationUseCase);

            viewModel.SelectedLocationToolAssignmentModel = new LocationToolAssignmentModel(new LocationToolAssignment()
            {
                AssignedLocation = CreateLocation.Anonymous(),
                AssignedTool = CreateTool.WithId(15),
                ToolUsage = new ToolUsage{ListId = new HelperTableEntityId(15), Value = new ToolUsageDescription("blub")},
                TestParameters = new TestParameters(),
                TestTechnique = new TestTechnique(),
                Id = new LocationToolAssignmentId(15)
            }, new NullLocalizationWrapper());

            Assert.IsFalse(viewModel.AddTestConditionsCommand.CanExecute(null));
        }

        [Test]
        public void AssignToolToLocationCanExecuteWithExistingEmptyAssignmentReturnsTrue()
        {
            var toolUseCase = new ToolUseCaseMock(null, null, null, null, null, null, null, null, null, null);
            var locationUseCase = new LocationUseCaseMock(null);
            var viewModel = CreateLocationToolAssignmentViewModel(toolUseCase: toolUseCase, locationUseCase: locationUseCase);

            viewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);
            viewModel.SelectedTool = new InterfaceAdapters.Models.ToolModel(CreateTool.Anonymous(), new NullLocalizationWrapper());
            viewModel.SelectedLocationToolAssignmentModel = new LocationToolAssignmentModel(new LocationToolAssignment()
            {
                AssignedLocation = CreateLocation.Anonymous(),
                AssignedTool = CreateTool.WithModel(null),
                ToolUsage = new ToolUsage()
            }, new NullLocalizationWrapper());

            Assert.IsTrue(viewModel.AddTestConditionsCommand.CanExecute(null));
        }

        private static IEnumerable<(int, List<int>)>
            SelectedLocationToolAssignmentModelSetsAvailableToolUsagesCorrectData = new List<(int, List<int>)>
            {
                (
                    1, new List<int>{1,2}
                ),
                (
                    3, new List<int>{1,2,3}
                ),
                (
                    4, new List<int>{1,2,4}
                )
            };

        [TestCaseSource(nameof(SelectedLocationToolAssignmentModelSetsAvailableToolUsagesCorrectData))]
        public void SelectedLocationToolAssignmentModelSetsAvailableToolUsagesCorrect((int selectedToolUsageId, List<int> expectedIds) data)
        {
            var toolUseCase = new ToolUseCaseMock(null, null, null, null, null, null, null, null, null, null);
            var locationUseCase = new LocationUseCaseMock(null);
            var viewModel = CreateLocationToolAssignmentViewModel(toolUseCase: toolUseCase, locationUseCase: locationUseCase);

            var selectedToolUsage = new ToolUsage {ListId = new HelperTableEntityId(data.selectedToolUsageId) };
            var unusedToolUsages = new List<ToolUsage>
            {
                new ToolUsage {ListId = new HelperTableEntityId(1)},
                new ToolUsage {ListId = new HelperTableEntityId(2)}
            };

            foreach (var toolUsage in unusedToolUsages)
            {
                viewModel.AvailableToolUsages.Add(
                    HelperTableItemModel.GetModelForToolUsage(toolUsage));
            }

            viewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);
            viewModel.SelectedTool = new InterfaceAdapters.Models.ToolModel(CreateTool.Anonymous(), new NullLocalizationWrapper());

            viewModel.SelectedLocationToolAssignmentModel = new LocationToolAssignmentModel(new LocationToolAssignment()
            {
                AssignedLocation = CreateLocation.Anonymous(),
                AssignedTool = CreateTool.WithModel(null),
                ToolUsage = selectedToolUsage
            }, new NullLocalizationWrapper());

            Assert.AreEqual(data.expectedIds.ToList(), viewModel.AvailableToolUsages.Select(x => x.Entity.ListId.ToLong()).ToList());
        }

        [Test]
        public void RemoveLocationToolAssignmentCanExecuteWithSelectedAssignmentReturnsTrue()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SelectedLocationToolAssignmentModel =
                new LocationToolAssignmentModel(new LocationToolAssignment{Id = new LocationToolAssignmentId(15)}, new NullLocalizationWrapper());

            Assert.IsTrue(viewModel.RemoveLocationToolAssignmentCommand.CanExecute(null));
        }

        [Test]
        public void RemoveLocationToolAssignmentCanExecuteWithNoSelectedAssignmentReturnsFalse()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SelectedLocationToolAssignmentModel = null;

            Assert.IsFalse(viewModel.RemoveLocationToolAssignmentCommand.CanExecute(null));
        }

        [Test]
        [RequiresThread(ApartmentState.STA)]
        public void RemoveLocationToolAssignmentExecuteCallsUseCase()
        {
            var useCase = new LocationToolAssignmentUseCaseMock();
            var startUp = new StartUpMock();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentUseCase: useCase, startUp: startUp);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var tool = CreateTool.Anonymous();
            tool.Status = new Status {ListId = new HelperTableEntityId(15)};
            var assignment = new LocationToolAssignment{Id = new LocationToolAssignmentId(15), AssignedTool = tool};
            
            viewModel.MessageBoxRequest += (s, e) => e.ResultAction(MessageBoxResult.Yes);
            viewModel.ShowDialogRequest += (s, e) => e.ShowDialog();
            startUp.GetAssistentViewReturn = new GetAssistentViewReturn();
            viewModel.SelectedLocationToolAssignmentModel =
                new LocationToolAssignmentModel(assignment, new NullLocalizationWrapper());
            viewModel.RemoveLocationToolAssignmentCommand.Execute(null);

            Assert.AreEqual(assignment, useCase.RemoveLocationToolAssignmentParameter);
        }

        [Test]
        public void InvokeRemoveLocationToolAssignmentExecuteCallsRemoveLocationToolAssignmentWithResetedParameters()
        {
            var useCase = new LocationToolAssignmentUseCaseMock();
            var startUp = new StartUpMock();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentUseCase: useCase, startUp: startUp);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);

            viewModel.ShowDialogRequest += (s, e) => e.ShowDialog();
            startUp.GetAssistentViewReturn = new GetAssistentViewReturn();

            var tool = CreateLocationToolAssignment.Anonymous();

            var locationToolAssignmentModel = LocationToolAssignmentModel.GetModelFor(tool, new NullLocalizationWrapper());

            viewModel.SelectedLocationToolAssignmentModel = locationToolAssignmentModel.CopyDeep();
            viewModel.SelectedLocationToolAssignmentModel.ToolUsage = new HelperTableItemModel<ToolUsage, string>(new ToolUsage { ListId = new HelperTableEntityId(9565), Value = new ToolUsageDescription("blub") }, null, null, null);
            viewModel.SelectedLocationToolAssignmentModel.TestLevelNumberMfu = 1;
            viewModel.SelectedLocationToolAssignmentModel.TestLevelSetMfu = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(1),
                TestLevel1 = new TestLevel()
                {
                    SampleNumber = 65
                }
            });
            viewModel.SelectedLocationToolAssignmentModel.StartDateMfu = DateTime.Now;

            viewModel.MessageBoxRequest += (sender, args) => args.ResultAction?.Invoke(MessageBoxResult.Yes);
            viewModel.RemoveLocationToolAssignmentCommand.Invoke(null);

            Assert.IsTrue(useCase.RemoveLocationToolAssignmentParameter.EqualsByContent(locationToolAssignmentModel.Entity));
        }

        private class GetAssistentViewReturn : IAssistentView
        {
            public event EventHandler EndOfAssistent;
            public Dispatcher Dispatcher { get; }
            public AssistentViewModel ViewModel { get; } = new AssistentViewModel();
            public void SetParentPlan(ParentAssistentPlan plan)
            {
            }

            public bool? ShowDialog()
            {
                EndOfAssistent.Invoke(this, System.EventArgs.Empty);
                return true;
            }
        }

        [Test]
        public void RemoveLocationToolAssignmentErrorCallsMessageBoxRequest()
        {
            var wasMessageBoxRequestCalled = false;
            var viewModel = CreateLocationToolAssignmentViewModel();

            viewModel.MessageBoxRequest += (s, e) => wasMessageBoxRequestCalled = true;
            viewModel.RemoveLocationToolAssignmentError();

            Assert.IsTrue(wasMessageBoxRequestCalled);
        }

        [Test]
        public void RemoveLocationToolAssignmentRemovesContainingAssignment()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var assignment = new LocationToolAssignment() {Id = new LocationToolAssignmentId(321)};
            var model = new LocationToolAssignmentModel(new LocationToolAssignment()
                {
                    Id = new LocationToolAssignmentId(987)
                },
                new NullLocalizationWrapper());

            viewModel.AssignmentsForSelectedLocation.Add(model);
            viewModel.AssignmentsForSelectedLocation.Add(
                new LocationToolAssignmentModel(assignment, new NullLocalizationWrapper()));
            viewModel.RemoveLocationToolAssignment(assignment);

            Assert.AreEqual(1, viewModel.AssignmentsForSelectedLocation.Count);
            Assert.AreEqual(model, viewModel.AssignmentsForSelectedLocation[0]);
        }

        [Test]
        public void RemoveLocationToolAssignmentWithSameSelectedAssignmentSetsSelectedToNull()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var assignment = new LocationToolAssignment() {Id = new LocationToolAssignmentId(321)};

            viewModel.SelectedLocationToolAssignmentModel =
                new LocationToolAssignmentModel(assignment, new NullLocalizationWrapper());
            viewModel.RemoveLocationToolAssignment(assignment);

            Assert.IsNull(viewModel.SelectedLocationToolAssignmentModel);
        }

        [Test]
        public void RemoveLocationToolAssignmentWithDifferentSelectedAssignmentSetsSelectedToNull()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var assignment1 = new LocationToolAssignment() {Id = new LocationToolAssignmentId(321)};
            var assignment2 = new LocationToolAssignment() {Id = new LocationToolAssignmentId(456)};

            viewModel.SelectedLocationToolAssignmentModel =
                new LocationToolAssignmentModel(assignment1, new NullLocalizationWrapper());
            viewModel.RemoveLocationToolAssignment(assignment2);

            Assert.AreEqual(assignment1, viewModel.SelectedLocationToolAssignmentModel.Entity);
        }

        [Test]
        public void UpdateToolUpdatesSelectedToolIfIdsAreEqual()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            const long id = 56874125;
            const string invNumber = "4ße4prtozigujaölilgj";

            viewModel.SelectedTool = new InterfaceAdapters.Models.ToolModel(new Tool(), new NullLocalizationWrapper())
            {
                Id = id
            };
            viewModel.UpdateTool(new Tool()
            {
                Id = new ToolId(id),
                InventoryNumber = new ToolInventoryNumber(invNumber)
            });

            Assert.AreEqual(invNumber, viewModel.SelectedTool.InventoryNumber);
        }

        [Test]
        public void AssignToolToLocationCanExecuteWithLoadingAssignmentsForLocationReturnsFalse()
        {
            var toolUseCase = new ToolUseCaseMock(null, null, null, null, null, null, null, null, null, null);
            var locationUseCase = new LocationUseCaseMock(null);
            var viewModel = CreateLocationToolAssignmentViewModel(toolUseCase: toolUseCase, locationUseCase: locationUseCase);

            viewModel.SelectedLocation = new LocationModel(new Location(), new NullLocalizationWrapper(), null);
            viewModel.SelectedTool = new InterfaceAdapters.Models.ToolModel(new Tool(), new NullLocalizationWrapper());

            Assert.IsFalse(viewModel.AssignToolToLocationCommand.CanExecute(null));
        }

        [Test]
        public void AssignToolToLocationCanExecuteWithShownAssignmentsForLocationReturnsTrue()
        {
            var toolUseCase = new ToolUseCaseMock(null, null, null, null, null, null, null, null, null, null);
            var locationUseCase = new LocationUseCaseMock(null);
            var viewModel = CreateLocationToolAssignmentViewModel(toolUseCase: toolUseCase, locationUseCase: locationUseCase);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);

            viewModel.SelectedLocation = new LocationModel(new Location(), new NullLocalizationWrapper(), null);
            viewModel.SelectedTool = new InterfaceAdapters.Models.ToolModel(new Tool(), new NullLocalizationWrapper());
            viewModel.ShowAssignedToolsForLocation(new List<LocationToolAssignment>());

            Assert.IsTrue(viewModel.AssignToolToLocationCommand.CanExecute(null));
        }

        [Test]
        public void AssignToolToLocationCanExecuteWithShowAssignmentsForLocationErrorReturnsTrue()
        {
            var toolUseCase = new ToolUseCaseMock(null, null, null, null, null, null, null, null, null, null);
            var locationUseCase = new LocationUseCaseMock(null);
            var viewModel = CreateLocationToolAssignmentViewModel(toolUseCase: toolUseCase, locationUseCase: locationUseCase);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);

            viewModel.SelectedLocation = new LocationModel(new Location(), new NullLocalizationWrapper(), null);
            viewModel.SelectedTool = new InterfaceAdapters.Models.ToolModel(new Tool(), new NullLocalizationWrapper());
            viewModel.LoadAssignedToolsForLocationError();

            Assert.IsTrue(viewModel.AssignToolToLocationCommand.CanExecute(null));
        }

        [Test]
        public void ShowToolDetailsDialogCanExecuteIfSelectedToolIsNotNull()
        {
            var startUpMock = new StartUpMock();
            var nullLocalizationWrapper = new NullLocalizationWrapper();
            var viewModel = CreateLocationToolAssignmentViewModel(startUp: startUpMock, localizationWrapper: nullLocalizationWrapper);
            viewModel.SelectedTool = new InterfaceAdapters.Models.ToolModel(CreateTool.Anonymous(), nullLocalizationWrapper);
            Assert.IsTrue(viewModel.ShowToolDetailsDialog.CanExecute(null));
        }

        [Test]
        public void ShowToolDetailsDialogCanExecuteIsFalseIfSelectedToolIsNull()
        {
            var startUpMock = new StartUpMock();
            var nullLocalizationWrapper = new NullLocalizationWrapper();
            var viewModel = CreateLocationToolAssignmentViewModel(startUp: startUpMock, localizationWrapper: nullLocalizationWrapper);
            Assert.IsFalse(viewModel.ShowToolDetailsDialog.CanExecute(null));
        }

        [Test]
        public void InvokeShowToolDetailsDialogCallsStartUpOpenLocationToolAssignmentToolDetailsView()
        {
            var startUpMock = new StartUpMock();
            var nullLocalizationWrapper = new NullLocalizationWrapper();
            var viewModel = CreateLocationToolAssignmentViewModel(startUp: startUpMock, localizationWrapper: nullLocalizationWrapper);
            viewModel.SelectedTool = new InterfaceAdapters.Models.ToolModel(CreateTool.Anonymous(), nullLocalizationWrapper);
            viewModel.ShowToolDetailsDialog.Invoke(null);
            Assert.AreEqual(1, startUpMock.OpenLocationToolAssignmentToolDetailDialogCallCount);
        }

        [Test]
        public void
            InvokeShowToolDetailsDialogCallsStartUpOpenLocationToolAssignmentToolDetailsViewWithCorrectParameter()
        {
            var startUpMock = new StartUpMock();
            var nullLocalizationWrapper = new NullLocalizationWrapper();
            var viewModel = CreateLocationToolAssignmentViewModel(startUp: startUpMock, localizationWrapper: nullLocalizationWrapper);
            viewModel.SelectedTool = new InterfaceAdapters.Models.ToolModel(CreateTool.WithId(15), nullLocalizationWrapper);
            var locationToolAssignmentModel =
                new LocationToolAssignmentModel(new LocationToolAssignment(), nullLocalizationWrapper);
            locationToolAssignmentModel.AssignedTool =
                new InterfaceAdapters.Models.ToolModel(CreateTool.WithId(15), nullLocalizationWrapper);
            locationToolAssignmentModel.ToolUsage = new HelperTableItemModel<ToolUsage, string>(new ToolUsage{ListId = new HelperTableEntityId(15), Value = new ToolUsageDescription("blub")}, null, null, null);
            viewModel.AssignmentsForSelectedLocation.Add(locationToolAssignmentModel);
            viewModel.ShowToolDetailsDialog.Invoke(null);
            Assert.IsTrue(locationToolAssignmentModel.AssignedTool.EqualsByContent(
                startUpMock.OpenLocationToolAssignmentToolDetailDialogParameter.FirstOrDefault().ToolModel));
            Assert.IsTrue(locationToolAssignmentModel.ToolUsage.EqualsByContent(startUpMock.OpenLocationToolAssignmentToolDetailDialogParameter.FirstOrDefault().ToolUsage));
        }

        #region Changes

        [Test]
        [TestCaseSource(nameof(VerifyChangesDiffPassesCorrectParametersToRequestChangesVerificationData))]
        public void SelectedLocation_ChangingWithChangedSelectedLocationToolAssignment_OpensVerifyDiffDialog((LocationToolAssignmentModel changedLocationToolAssignmentModel,
            Action<LocationToolAssignmentModel> changeLocationToolAssignmentModel,
            Func<(string oldValue, string newValue)> getParameterStrings, string nameOf) parameters)
        {
            var mockLocationToolAssignmentDisplayFormatter = new MockLocationToolAssignmentDisplayFormatter();
            mockLocationToolAssignmentDisplayFormatter.DisplayString = "Test";
            var nullLocalizationWrapper = new NullLocalizationWrapper();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentDisplayFormatter: mockLocationToolAssignmentDisplayFormatter, localizationWrapper: nullLocalizationWrapper);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.RequestChangesVerification += (sender, args) =>
            {
                Assert.AreEqual(1, args.ChangedValues.Count);
                if (parameters.nameOf != nameof(LocationToolAssignmentModel.TestParameters.ControlledBy) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestOperationActiveChk) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestTechnique.MustTorqueAndAngleBeInLimits) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestOperationActiveMfu) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestOperationActiveChk))
                {
                    var parameterStrings = parameters.getParameterStrings.Invoke();
                    Assert.AreEqual(parameterStrings.oldValue, args.ChangedValues[0].OldValue);
                    Assert.AreEqual(parameterStrings.newValue, args.ChangedValues[0].NewValue);
                }
                Assert.Pass();
            };
            var model = parameters.changedLocationToolAssignmentModel;
            viewModel.SelectedLocationToolAssignmentModel = model;
            parameters.changeLocationToolAssignmentModel.Invoke(model);
            viewModel.SelectedLocation = new LocationModel(new Location(), new NullLocalizationWrapper(), null);
            Assert.Fail();
        }

        [Test]
        [TestCaseSource(nameof(VerifyChangesDiffPassesCorrectParametersToRequestChangesVerificationData))]
        public void SelectedTool_ChangingWithChangedSelectedLocationToolAssignment_OpensVerifyDiffDialog((LocationToolAssignmentModel changedLocationToolAssignmentModel,
            Action<LocationToolAssignmentModel> changeLocationToolAssignmentModel,
            Func<(string oldValue, string newValue)> getParameterStrings, string nameOf) parameters)
        {
            var mockLocationToolAssignmentDisplayFormatter = new MockLocationToolAssignmentDisplayFormatter();
            mockLocationToolAssignmentDisplayFormatter.DisplayString = "Test";
            var nullLocalizationWrapper = new NullLocalizationWrapper();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentDisplayFormatter: mockLocationToolAssignmentDisplayFormatter, localizationWrapper: nullLocalizationWrapper);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.RequestChangesVerification += (sender, args) =>
            {
                Assert.AreEqual(1, args.ChangedValues.Count);
                if (parameters.nameOf != nameof(LocationToolAssignmentModel.TestParameters.ControlledBy) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestOperationActiveChk) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestTechnique.MustTorqueAndAngleBeInLimits) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestOperationActiveMfu) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestOperationActiveChk))
                {
                    var parameterStrings = parameters.getParameterStrings.Invoke();
                    Assert.AreEqual(parameterStrings.oldValue, args.ChangedValues[0].OldValue);
                    Assert.AreEqual(parameterStrings.newValue, args.ChangedValues[0].NewValue);
                }

                Assert.Pass();
            };
            var model = parameters.changedLocationToolAssignmentModel;
            viewModel.SelectedLocationToolAssignmentModel = model;
            parameters.changeLocationToolAssignmentModel.Invoke(model);
            viewModel.SelectedTool = new InterfaceAdapters.Models.ToolModel(new Tool(), new NullLocalizationWrapper());
            Assert.Fail();
        }

        [Test]
        [TestCaseSource(nameof(VerifyChangesDiffPassesCorrectParametersToRequestChangesVerificationData))]
        public void SelectedLocationToolAssignment_ChangingWithChangedSelectedLocationToolAssignment_OpensVerifyDiffDialog((LocationToolAssignmentModel changedLocationToolAssignmentModel,
            Action<LocationToolAssignmentModel> changeLocationToolAssignmentModel,
            Func<(string oldValue, string newValue)> getParameterStrings, string nameOf) parameters)
        {
            var mockLocationToolAssignmentDisplayFormatter = new MockLocationToolAssignmentDisplayFormatter();
            mockLocationToolAssignmentDisplayFormatter.DisplayString = "Test";
            var nullLocalizationWrapper = new NullLocalizationWrapper();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentDisplayFormatter: mockLocationToolAssignmentDisplayFormatter, localizationWrapper: nullLocalizationWrapper);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.RequestChangesVerification += (sender, args) =>
            {
                Assert.AreEqual(1, args.ChangedValues.Count);
                if (parameters.nameOf != nameof(LocationToolAssignmentModel.TestParameters.ControlledBy) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestOperationActiveChk) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestTechnique.MustTorqueAndAngleBeInLimits) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestOperationActiveMfu) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestOperationActiveChk))
                {
                    var parameterStrings = parameters.getParameterStrings.Invoke();
                    Assert.AreEqual(parameterStrings.oldValue, args.ChangedValues[0].OldValue);
                    Assert.AreEqual(parameterStrings.newValue, args.ChangedValues[0].NewValue);
                }

                Assert.Pass();
            };
            var model = parameters.changedLocationToolAssignmentModel;
            viewModel.SelectedLocationToolAssignmentModel = model;
            parameters.changeLocationToolAssignmentModel.Invoke(model);
            viewModel.SelectedLocationToolAssignmentModel = new LocationToolAssignmentModel(new LocationToolAssignment(), nullLocalizationWrapper);
            Assert.Fail();
        }

        [Test]
        [TestCase(MessageBoxResult.Yes)]
        [TestCase(MessageBoxResult.No)]
        public void ControlledByChangeResetsControlledByOnUnvalidLocationToolAssignment(MessageBoxResult result)
        {
            //Arrange

            var validationMock = new LocationToolAssignmentValidatorMock();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentValidtor: validationMock);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.MessageBoxRequest += (sender, args) => args.ResultAction.Invoke(result);
            var locationToolAssignment = LocationToolAssignmentModel.GetModelFor(CreateLocationToolAssignment.Anonymous(), new NullLocalizationWrapper());
            locationToolAssignment.TestParameters.ControlledBy = LocationControlledBy.Torque;
            viewModel.SelectedLocationToolAssignmentModel = locationToolAssignment;

            //Act

            validationMock.ValidateResult = false;
            viewModel.SelectedLocationToolAssignmentModel.TestParameters.ControlledBy = LocationControlledBy.Angle;

            //Assert
            ProcessAllInvokes();
            Assert.AreEqual(LocationControlledBy.Torque, viewModel.SelectedLocationToolAssignmentModel.TestParameters.ControlledBy);
        }

        [Test]
        public void ControlledByChangeCallsRequestChangeVerification()
        {
            //Arrange

            var validationMock = new LocationToolAssignmentValidatorMock();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentValidtor: validationMock);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.RequestChangesVerification += (sender, args) => RequestChangesVerification();
            var locationToolAssignment = LocationToolAssignmentModel.GetModelFor(CreateLocationToolAssignment.Anonymous(), new NullLocalizationWrapper());
            locationToolAssignment.TestParameters.ControlledBy = LocationControlledBy.Torque;
            viewModel.SelectedLocationToolAssignmentModel = locationToolAssignment;

            //Act

            viewModel.SelectedLocationToolAssignmentModel.TestParameters.ControlledBy = LocationControlledBy.Angle;

            //Assert
            ProcessAllInvokes();

            void RequestChangesVerification()
            {
                Assert.Pass();
            };
            Assert.Fail();
        }

        private void ProcessAllInvokes()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
        }
        private static object ExitFrame(object frame)
        {
            ((DispatcherFrame)frame).Continue = false;
            return null;
        }

        [Test]
        [TestCaseSource(nameof(VerifyChangesDiffPassesCorrectParametersToRequestChangesVerificationData))]
        public void SaveLocationToolAssignment_ChangingWithChangedSelectedLocationToolAssignment_OpensVerifyDiffDialog((LocationToolAssignmentModel changedLocationToolAssignmentModel,
            Action<LocationToolAssignmentModel> changeLocationToolAssignmentModel,
            Func<(string oldValue, string newValue)> getParameterStrings, string nameOf) parameters)
        {
            var mockLocationToolAssignmentDisplayFormatter = new MockLocationToolAssignmentDisplayFormatter();
            mockLocationToolAssignmentDisplayFormatter.DisplayString = "Test";
            var nullLocalizationWrapper = new NullLocalizationWrapper();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentDisplayFormatter: mockLocationToolAssignmentDisplayFormatter, localizationWrapper: nullLocalizationWrapper);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.RequestChangesVerification += (sender, args) =>
            {
                Assert.AreEqual(1, args.ChangedValues.Count);
                if (parameters.nameOf != nameof(LocationToolAssignmentModel.TestParameters.ControlledBy) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestOperationActiveChk) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestTechnique.MustTorqueAndAngleBeInLimits) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestOperationActiveMfu) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestOperationActiveChk))
                {
                    var parameterStrings = parameters.getParameterStrings.Invoke();
                    Assert.AreEqual(parameterStrings.oldValue, args.ChangedValues[0].OldValue);
                    Assert.AreEqual(parameterStrings.newValue, args.ChangedValues[0].NewValue);
                }

                Assert.Pass();
            };
            var model = parameters.changedLocationToolAssignmentModel;
            viewModel.SelectedLocationToolAssignmentModel = model;
            parameters.changeLocationToolAssignmentModel.Invoke(model);
            viewModel.SaveLocationToolAssignmentCommand.Invoke(null);
            Assert.Fail();
        }

        [Test]
        [TestCaseSource(nameof(VerifyChangesDiffPassesCorrectParametersToRequestChangesVerificationData))]
        public void CanClose_ChangingWithChangedSelectedLocationToolAssignment_OpensVerifyDiffDialog((LocationToolAssignmentModel changedLocationToolAssignmentModel,
            Action<LocationToolAssignmentModel> changeLocationToolAssignmentModel,
            Func<(string oldValue, string newValue)> getParameterStrings, string nameOf) parameters)
        {
            var mockLocationToolAssignmentDisplayFormatter = new MockLocationToolAssignmentDisplayFormatter();
            mockLocationToolAssignmentDisplayFormatter.DisplayString = "Test";
            var nullLocalizationWrapper = new NullLocalizationWrapper();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentDisplayFormatter: mockLocationToolAssignmentDisplayFormatter, localizationWrapper: nullLocalizationWrapper);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.RequestChangesVerification += (sender, args) =>
            {
                Assert.AreEqual(1, args.ChangedValues.Count);
                if (parameters.nameOf != nameof(LocationToolAssignmentModel.TestParameters.ControlledBy) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestOperationActiveChk) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestTechnique.MustTorqueAndAngleBeInLimits) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestOperationActiveMfu) &&
                    parameters.nameOf != nameof(LocationToolAssignmentModel.TestOperationActiveChk))
                {
                    var parameterStrings = parameters.getParameterStrings.Invoke();
                    Assert.AreEqual(parameterStrings.oldValue, args.ChangedValues[0].OldValue);
                    Assert.AreEqual(parameterStrings.newValue, args.ChangedValues[0].NewValue);
                }

                Assert.Pass();
            };
            var model = parameters.changedLocationToolAssignmentModel;
            viewModel.SelectedLocationToolAssignmentModel = model;
            parameters.changeLocationToolAssignmentModel.Invoke(model);
            viewModel.CanClose();
            Assert.Fail();
        }

        

        #endregion
        
        [Test]
        public void ChangingSelectedLocationToolAssignmentFromNullToNullNotOpeningVerifyChangesDialog()
        {
            var mockLocationToolAssignmentDisplayFormatter = new MockLocationToolAssignmentDisplayFormatter();
            mockLocationToolAssignmentDisplayFormatter.DisplayString = "Test";
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentDisplayFormatter: mockLocationToolAssignmentDisplayFormatter);
            viewModel.RequestChangesVerification += (sender, args) => Assert.Fail();
            viewModel.SelectedLocationToolAssignmentModel = null;
            viewModel.SelectedLocationToolAssignmentModel = null;
            Assert.Pass();
        }

        [Test]
        public void ChangingSelectedLocationToolAssignmentFromSomethingToNullNotOpeningVerifyChangesDialog()
        {
            var mockLocationToolAssignmentDisplayFormatter = new MockLocationToolAssignmentDisplayFormatter();
            mockLocationToolAssignmentDisplayFormatter.DisplayString = "Test";
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentDisplayFormatter: mockLocationToolAssignmentDisplayFormatter);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.RequestChangesVerification += (sender, args) => Assert.Fail();
            viewModel.SelectedLocationToolAssignmentModel = CreateDefaultLocationToolAssignmentModel();
            viewModel.SelectedLocationToolAssignmentModel = null;
        }

        [Test]
        public void ChangingSelectedLocationToolAssignmentWithCancelOnDialogSelectsPreviouslySelected()
        {
            var mockLocationToolAssignmentDisplayFormatter = new MockLocationToolAssignmentDisplayFormatter();
            mockLocationToolAssignmentDisplayFormatter.DisplayString = "Test";
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentDisplayFormatter: mockLocationToolAssignmentDisplayFormatter);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.RequestChangesVerification += (sender, args) => args.Result = MessageBoxResult.Cancel;
            var locationToolAssignment = CreateDefaultLocationToolAssignmentModel();
            viewModel.SelectedLocationToolAssignmentModel = locationToolAssignment;
            locationToolAssignment.TestParameters.MaximumAngle = 25;
            viewModel.SelectedLocationToolAssignmentModel = null;
            Assert.AreEqual(locationToolAssignment, viewModel.SelectedLocationToolAssignmentModel);
        }

        [Test]
        public void SaveLocationToolAssignmentCanExecuteReturnsTrueIfSelectedLocationToolAssignmentHasChanges()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            var locationToolAssignment = CreateDefaultLocationToolAssignmentModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.SelectedLocationToolAssignmentModel = locationToolAssignment;
            locationToolAssignment.TestParameters.MaximumAngle = 36;
            Assert.IsTrue(viewModel.SaveLocationToolAssignmentCommand.CanExecute(null));
        }

        [Test]
        public void SaveLocationToolAssignmentCanExecuteReturnsFalseIfSelectedLocationToolAssignmentHasNoChanges()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var locationToolAssignment = CreateDefaultLocationToolAssignmentModel();
            viewModel.SelectedLocationToolAssignmentModel = locationToolAssignment;
            var changedLocationToolAssignmentModel = locationToolAssignment.CopyDeep();
            viewModel.SelectedLocationToolAssignmentModel = changedLocationToolAssignmentModel;
            Assert.IsFalse(viewModel.SaveLocationToolAssignmentCommand.CanExecute(null));
        }

        [Test]
        public void SaveLocationToolAssignmentInvokeCallsRequestVerificationDialog()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var locationToolAssignment = CreateDefaultLocationToolAssignmentModel();
            viewModel.SelectedLocationToolAssignmentModel = locationToolAssignment;
            locationToolAssignment.TestParameters.MaximumAngle = 36;
            viewModel.RequestChangesVerification += (sender, args) => Assert.Pass();
            viewModel.SaveLocationToolAssignmentCommand.Invoke(null);
            Assert.Fail();
        }

        [Test]
        public void SaveLocationToolAssignmentInvokeCallsUpdateLocationToolAssignment()
        {
            var locationToolAssignmentUseCaseMock = new LocationToolAssignmentUseCaseMock();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentUseCase: locationToolAssignmentUseCaseMock);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var locationToolAssignment = CreateDefaultLocationToolAssignmentModel();
            viewModel.SelectedLocationToolAssignmentModel = locationToolAssignment;
            locationToolAssignment.TestParameters.MaximumAngle = 36;
            viewModel.RequestChangesVerification += (sender, args) => args.Result = MessageBoxResult.Yes;
            viewModel.SaveLocationToolAssignmentCommand.Invoke(null);
            Assert.AreEqual(1, locationToolAssignmentUseCaseMock.UpdateLocationToolAssignmentCallCount);
        }

        [Test]
        public void UpdateLocationToolAssignmentUpdatesAssignmentsForSelectedLocation()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var locationToolAssignment = new LocationToolAssignment
            {
                Id = new LocationToolAssignmentId(15),
                TestParameters = new TestParameters
                    {MaximumAngle = Angle.FromDegree(15), MaximumTorque = Torque.FromNm(15)}
            };
            var newLocationToolAssignment = locationToolAssignment.CopyDeep();

            viewModel.AssignmentsForSelectedLocation.Add(new LocationToolAssignmentModel(locationToolAssignment, new NullLocalizationWrapper()));

            newLocationToolAssignment.TestParameters.MaximumAngle = Angle.FromDegree(36);

            viewModel.UpdateLocationToolAssignment(new List<LocationToolAssignment>() { newLocationToolAssignment });
            Assert.AreEqual(36, viewModel.AssignmentsForSelectedLocation.FirstOrDefault()?.TestParameters.MaximumAngle);
        }

        [Test]
        public void UpdateLocationToolAssignmentErrorInvokesMessageBoxRequest()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.MessageBoxRequest += (sender, args) => Assert.Pass();
            viewModel.UpdateLocationToolAssignmentError();
            Assert.Fail();
        }

        [Test]
        public void CanClose_NoChangesInSelectedLocationToolAssignment_ReturnsTrue()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            Assert.IsTrue(viewModel.CanClose());
        }

        [Test]
        public void CanClose_ChangesInSelectedLocationToolAssignment_OpensVerifyChangesDiffDialog()
        {
            var locationToolAssignmentUseCaseMock = new LocationToolAssignmentUseCaseMock();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentUseCase: locationToolAssignmentUseCaseMock);
            var locationToolAssignment = CreateDefaultLocationToolAssignmentModel();
            viewModel.SelectedLocationToolAssignmentModel = locationToolAssignment;
            locationToolAssignment.TestParameters.MaximumAngle = 36;
            viewModel.RequestChangesVerification += (sender, args) => Assert.Pass();
            viewModel.CanClose();
            Assert.Fail();
        }

        [Test]
        public void CanClose_OpensVerifyChangesDiffDialog_CallsUpdateLocationToolAssignmentOnChangesVerificationReturnsYes()
        {
            var locationToolAssignmentUseCaseMock = new LocationToolAssignmentUseCaseMock();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentUseCase: locationToolAssignmentUseCaseMock);
            var locationToolAssignment = CreateDefaultLocationToolAssignmentModel();
            viewModel.SelectedLocationToolAssignmentModel = locationToolAssignment;
            locationToolAssignment.TestParameters.MaximumAngle = 36;
            viewModel.RequestChangesVerification += (sender, args) => args.Result = MessageBoxResult.Yes;
            viewModel.CanClose();
            Assert.AreEqual(1, locationToolAssignmentUseCaseMock.UpdateLocationToolAssignmentCallCount);
        }

        [Test]
        public void CanClose_OpensVerifyChangesDiffDialog_ReturnsTrueOnChangesVerificationReturnsYes()
        {
            var locationToolAssignmentUseCaseMock = new LocationToolAssignmentUseCaseMock();
            var viewModel =
                CreateLocationToolAssignmentViewModel(locationToolAssignmentUseCase: locationToolAssignmentUseCaseMock);
            var locationToolAssignment = CreateDefaultLocationToolAssignmentModel();
            viewModel.SelectedLocationToolAssignmentModel = locationToolAssignment;
            locationToolAssignment.TestParameters.MaximumAngle = 36;
            viewModel.RequestChangesVerification += (sender, args) => args.Result = MessageBoxResult.Yes;
            Assert.IsTrue(viewModel.CanClose());
        }

        [Test]
        public void CanClose_OpensVerifyChangesDiffDialog_ReturnsTrueOnChangesVerificationReturnsNo()
        {
            var locationToolAssignmentUseCaseMock = new LocationToolAssignmentUseCaseMock();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentUseCase: locationToolAssignmentUseCaseMock);
            var locationToolAssignment = CreateDefaultLocationToolAssignmentModel();
            viewModel.SelectedLocationToolAssignmentModel = locationToolAssignment;
            locationToolAssignment.TestParameters.MaximumAngle = 36;
            viewModel.RequestChangesVerification += (sender, args) => args.Result = MessageBoxResult.No;
            Assert.IsTrue(viewModel.CanClose());
        }

        [Test]
        public void CanClose_OpensVerifyChangesDiffDialog_ReturnsFalseOnChangesVerificationReturnsCancel()
        {
            var locationToolAssignmentUseCaseMock = new LocationToolAssignmentUseCaseMock();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentUseCase: locationToolAssignmentUseCaseMock);
            var locationToolAssignment = CreateDefaultLocationToolAssignmentModel();
            viewModel.SelectedLocationToolAssignmentModel = locationToolAssignment;
            locationToolAssignment.TestParameters.MaximumAngle = 36;
            viewModel.RequestChangesVerification += (sender, args) => args.Result = MessageBoxResult.Cancel;
            Assert.IsFalse(viewModel.CanClose());
        }

        [TestCase(MessageBoxResult.Yes, false)]
        [TestCase(MessageBoxResult.No, true)]
        public void ClosingWithInvalidLocationToolAssignmentShowsMessageAndReturnsCorrectValue(MessageBoxResult msgResult, bool expectedResult)
        {
            var validationMock = new LocationToolAssignmentValidatorMock();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentValidtor: validationMock);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.MessageBoxRequest += (sender, args) => args.ResultAction.Invoke(msgResult);
            var locationToolAssignment = CreateDefaultLocationToolAssignmentModel();
            viewModel.SelectedLocationToolAssignmentModel = locationToolAssignment;

            validationMock.ValidateResult = false;

            var wasMessageBoxRequestCalled = false;
            viewModel.MessageBoxRequest += (s, e) => wasMessageBoxRequestCalled = true;

            var result = viewModel.CanClose();

            Assert.IsTrue(wasMessageBoxRequestCalled);
            Assert.AreEqual(expectedResult, result);
        }


        [Test]
        [TestCase("blub")]
        [TestCase("hanse")]
        public void SelectedLocationFormattedNameReturnsFormattedString(string displayString)
        {
            var locationDisplayFormatter = new MockLocationDisplayFormatter();
            locationDisplayFormatter.DisplayString = displayString;
            var viewModel = CreateLocationToolAssignmentViewModel(locationDisplayFormatter: locationDisplayFormatter);
            viewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);
            Assert.AreEqual(displayString, viewModel.SelectedLocationFormattedName);
        }

        [Test]
        [TestCase("blub")]
        [TestCase("hanse")]
        public void SelectedToolFormattedNameReturnsFormattedString(string displayString)
        {
            var toolDisplayFormatter = new MockToolDisplayFormatter();
            toolDisplayFormatter.DisplayString = displayString;
            var viewModel = CreateLocationToolAssignmentViewModel(toolDisplayFormatter: toolDisplayFormatter);
            viewModel.SelectedTool =
                InterfaceAdapters.Models.ToolModel.GetModelFor(CreateTool.Anonymous(),
                    new NullLocalizationWrapper());
            Assert.AreEqual(displayString, viewModel.SelectedToolFormattedName);
        }

        [Test]
        public void ShowUnusedToolUsagesForLocationAddsNewToolUsagesToAvailableToolUsages()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            List<ToolUsage> toolUsages = new List<ToolUsage>()
            {
                new ToolUsage {ListId = new HelperTableEntityId(15)},
                new ToolUsage {ListId = new HelperTableEntityId(26)},
                new ToolUsage {ListId = new HelperTableEntityId(93)}
            };
            var location = CreateLocation.IdOnly(15);
            viewModel.SelectedLocation = LocationModel.GetModelFor(location, new NullLocalizationWrapper(), null);
            viewModel.ShowUnusedToolUsagesForLocation(toolUsages, location.Id);
            foreach (var toolUsage in toolUsages)
            {
                Assert.IsNotNull(viewModel.AvailableToolUsages.FirstOrDefault(x => x.ListId == toolUsage.ListId.ToLong()));
            }
        }

        [Test]
        public void ShowUnusedToolUsagesForLocationDontChangeAvailableToolUsagesForDifferentLocation()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            List<ToolUsage> toolUsages = new List<ToolUsage>()
            {
                new ToolUsage {ListId = new HelperTableEntityId(15)},
                new ToolUsage {ListId = new HelperTableEntityId(26)},
                new ToolUsage {ListId = new HelperTableEntityId(93)}
            };
            viewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.IdOnly(1), new NullLocalizationWrapper(), null);
            viewModel.ShowUnusedToolUsagesForLocation(toolUsages, new LocationId(15));
            
            Assert.AreEqual(0, viewModel.AvailableToolUsages.Count);
        }

        [Test]
        public void ShowUnusedToolUsagesForLocationDontChangeAvailableToolIfNoLocationIsSelected()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            List<ToolUsage> toolUsages = new List<ToolUsage>()
            {
                new ToolUsage {ListId = new HelperTableEntityId(15)},
                new ToolUsage {ListId = new HelperTableEntityId(26)},
                new ToolUsage {ListId = new HelperTableEntityId(93)}
            };
            
            viewModel.ShowUnusedToolUsagesForLocation(toolUsages, new LocationId(15));

            Assert.AreEqual(0, viewModel.AvailableToolUsages.Count);
        }

        [Test]
        public void ShowUnusedToolUsageForLocationRemovesToolUsagesThatAreNoLongerUnusedFromAvailableToolUsages()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            List<ToolUsage> toolUsagesToRemove = new List<ToolUsage>
            {
                new ToolUsage {ListId = new HelperTableEntityId(15)},
                new ToolUsage {ListId = new HelperTableEntityId(23)},
                new ToolUsage {ListId = new HelperTableEntityId(76)}
            };
            foreach (var toolUsage in toolUsagesToRemove)
            {
                viewModel.AvailableToolUsages.Add(
                    HelperTableItemModel.GetModelForToolUsage(toolUsage));
            }

            var location = CreateLocation.IdOnly(15);
            viewModel.SelectedLocation = LocationModel.GetModelFor(location, new NullLocalizationWrapper(), null);
            viewModel.ShowUnusedToolUsagesForLocation(new List<ToolUsage>(), location.Id);
            foreach (var toolUsage in toolUsagesToRemove)
            {
                Assert.IsNull(viewModel.AvailableToolUsages.FirstOrDefault(x => x.ListId == toolUsage.ListId.ToLong()));
            }
        }

        [Test]
        public void ShowUnusedToolUsageForLocationNotRemovingToolUsageForCurrentLocationTOolAssignment()
        {
            var viewModel = CreateLocationToolAssignmentViewModel();
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var toolUsage = new ToolUsage
                {ListId = new HelperTableEntityId(15), Value = new ToolUsageDescription("blub")};
            viewModel.SelectedLocationToolAssignmentModel =
                new LocationToolAssignmentModel(new LocationToolAssignment {ToolUsage = toolUsage}, new NullLocalizationWrapper());
            viewModel.ShowUnusedToolUsagesForLocation(new List<ToolUsage>(), new LocationId(15));
            Assert.IsNotNull(viewModel.AvailableToolUsages.FirstOrDefault(x => x.ListId == toolUsage.ListId.ToLong()));
        }

        [Test]
        public void UpdateLocationToolAssignmentCallsLoadUnusedToolUsagesForLocation()
        {
            var locationToolAssignmentUseCase = new LocationToolAssignmentUseCaseMock();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentUseCase: locationToolAssignmentUseCase);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.UpdateLocationToolAssignment(new List<LocationToolAssignment>());
            Assert.AreEqual(1, locationToolAssignmentUseCase.LoadUnusedToolUsagesForLocationCallCount);
        }

        [Test]
        public void SelectedLocationChangeCallsLoadUnusedToolUsagesForLocation()
        {
            var locationToolAssignmentUseCase = new LocationToolAssignmentUseCaseMock();
            var viewModel = CreateLocationToolAssignmentViewModel(locationToolAssignmentUseCase: locationToolAssignmentUseCase);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);
            Assert.AreEqual(1, locationToolAssignmentUseCase.LoadUnusedToolUsagesForLocationCallCount);
        }

        public LocationToolAssignmentViewModel CreateLocationToolAssignmentViewModel(ILocationToolAssignmentUseCase locationToolAssignmentUseCase = null,
            ILocationUseCase locationUseCase = null,
            ToolUseCase toolUseCase = null,
            ToleranceClassUseCase toleranceClassUseCase = null,
            IStartUp startUp = null,
            ILocalizationWrapper localizationWrapper = null,
            ILocationToolAssignmentDisplayFormatter locationToolAssignmentDisplayFormatter = null,
            IThreadCreator threadCreator = null,
            ILocationToolAssignmentValidtor locationToolAssignmentValidtor = null,
            ILocationDisplayFormatter locationDisplayFormatter = null,
            IToolDisplayFormatter toolDisplayFormatter = null,
            ITestLevelSetUseCase testLevelSetUseCase = null,
            ITestLevelSetInterface testLevelSetInterface = null)
        {
            return new LocationToolAssignmentViewModel(
                locationToolAssignmentUseCase ?? new LocationToolAssignmentUseCaseMock(),
                locationUseCase,
                toolUseCase,
                toleranceClassUseCase,
                testLevelSetUseCase ?? new TestLevelSetUseCaseMock(),
                testLevelSetInterface ?? new TestLevelSetInterfaceMock(),
                startUp ?? new StartUpMock(),
                localizationWrapper ?? new NullLocalizationWrapper(),
                locationToolAssignmentDisplayFormatter ?? new MockLocationToolAssignmentDisplayFormatter(),
                threadCreator ?? new MockThreadCreator(),
                locationToolAssignmentValidtor ?? new LocationToolAssignmentValidatorMock(),
                locationDisplayFormatter ?? new MockLocationDisplayFormatter(),
                toolDisplayFormatter ?? new MockToolDisplayFormatter());
        }

        private static LocationToolAssignmentModel CreateDefaultLocationToolAssignmentModel(LocationControlledBy controlledBy = LocationControlledBy.Angle)
        {
            var locationToolAssignment =
                LocationToolAssignmentModel.GetModelFor(new LocationToolAssignment(), new NullLocalizationWrapper());
            locationToolAssignment.TestParameters = new TestParametersModel(new TestParameters(), new NullLocalizationWrapper());
            locationToolAssignment.TestParameters.ControlledBy = controlledBy;
            locationToolAssignment.TestParameters.ToleranceClassAngle =
                new ToleranceClassModel(new ToleranceClass {Id = new ToleranceClassId(15), Name = "test"});
            locationToolAssignment.TestParameters.ToleranceClassTorque =
                new ToleranceClassModel(new ToleranceClass {Id = new ToleranceClassId(15), Name = "test"});
            locationToolAssignment.TestTechnique = new TestTechniqueModel(new TestTechnique(), new NullLocalizationWrapper());
            locationToolAssignment.Id = 1;
            return locationToolAssignment;
        }

        private static
            IEnumerable<(LocationToolAssignmentModel changedLocationToolAssignmentModel, Action<LocationToolAssignmentModel>, Func<(string oldValue, string newValue)>, string)>
            VerifyChangesDiffPassesCorrectParametersToRequestChangesVerificationData()
        {
            var originalLocationToolAssignmentModel = CreateDefaultLocationToolAssignmentModel();
            //var changedLocationToolAssignmentModel = originalLocationToolAssignmentModel.CopyDeep();

            yield return (CreateDefaultLocationToolAssignmentModel(), (changedLocationToolAssignmentModel) =>
                changedLocationToolAssignmentModel.TestParameters.ControlledBy =
                    originalLocationToolAssignmentModel.TestParameters.ControlledBy == LocationControlledBy.Angle
                        ? LocationControlledBy.Torque
                        : LocationControlledBy.Angle,
                null,
                nameof(LocationToolAssignmentModel.TestParameters.ControlledBy));

            var setPointTorqueObjet = CreateDefaultLocationToolAssignmentModel(LocationControlledBy.Torque);
            setPointTorqueObjet.TestParameters.MaximumTorque = 90;
            yield return (setPointTorqueObjet,
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestParameters.SetPointTorque = 86,
                () => ("0", "86"),
                nameof(LocationToolAssignmentModel.TestParameters.SetPointTorque));

            yield return (CreateDefaultLocationToolAssignmentModel(LocationControlledBy.Torque),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestParameters.ToleranceClassTorque =
                    new ToleranceClassModel(new ToleranceClass { Id = new ToleranceClassId(36), Name = "blub" }),
                () => ("test", "blub"),
                nameof(LocationToolAssignmentModel.TestParameters.ToleranceClassTorque));

            var minimumTorqueObject = CreateDefaultLocationToolAssignmentModel(LocationControlledBy.Torque);
            minimumTorqueObject.TestParameters.SetPointTorque = 40;
            minimumTorqueObject.TestParameters.MaximumTorque = 41;
            yield return (minimumTorqueObject,
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestParameters.MinimumTorque = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestParameters.MinimumTorque));

            yield return (CreateDefaultLocationToolAssignmentModel(LocationControlledBy.Torque),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestParameters.MaximumTorque = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestParameters.MaximumTorque));

            yield return (CreateDefaultLocationToolAssignmentModel(),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestParameters.ThresholdTorque = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestParameters.ThresholdTorque));

            var setPointAngleObjet = CreateDefaultLocationToolAssignmentModel(LocationControlledBy.Angle);
            setPointAngleObjet.TestParameters.MaximumAngle = 40;
            yield return (setPointAngleObjet,
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestParameters.SetPointAngle = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestParameters.SetPointAngle));

            yield return (CreateDefaultLocationToolAssignmentModel(LocationControlledBy.Angle),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestParameters.ToleranceClassAngle =
                    new ToleranceClassModel(new ToleranceClass { Id = new ToleranceClassId(36), Name = "blub" }),
                () => ("test", "blub"),
                nameof(LocationToolAssignmentModel.TestParameters.ToleranceClassAngle));

            var minimumAngleObject = CreateDefaultLocationToolAssignmentModel(LocationControlledBy.Angle);
            minimumAngleObject.TestParameters.SetPointAngle = 40;
            minimumAngleObject.TestParameters.MaximumAngle = 41;
            yield return (minimumAngleObject,
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestParameters.MinimumAngle = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestParameters.MinimumAngle));

            yield return (CreateDefaultLocationToolAssignmentModel(LocationControlledBy.Angle),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestParameters.MaximumAngle = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestParameters.MaximumAngle));

            yield return (CreateDefaultLocationToolAssignmentModel(),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestTechnique.EndCycleTime = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestTechnique.EndCycleTime));

            yield return (CreateDefaultLocationToolAssignmentModel(),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestTechnique.FilterFrequency = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestTechnique.FilterFrequency));

            yield return (CreateDefaultLocationToolAssignmentModel(),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestTechnique.CycleComplete = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestTechnique.CycleComplete));

            yield return (CreateDefaultLocationToolAssignmentModel(),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestTechnique.MeasureDelayTime = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestTechnique.MeasureDelayTime));

            yield return (CreateDefaultLocationToolAssignmentModel(),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestTechnique.ResetTime = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestTechnique.ResetTime));

            yield return (CreateDefaultLocationToolAssignmentModel(),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestTechnique.MustTorqueAndAngleBeInLimits = true,
                null,
                nameof(LocationToolAssignmentModel.TestTechnique.MustTorqueAndAngleBeInLimits));

            yield return (CreateDefaultLocationToolAssignmentModel(),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestTechnique.CycleStart = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestTechnique.CycleStart));

            yield return (CreateDefaultLocationToolAssignmentModel(),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestTechnique.StartFinalAngle = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestTechnique.StartFinalAngle));

            yield return (CreateDefaultLocationToolAssignmentModel(),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestTechnique.SlipTorque = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestTechnique.SlipTorque));

            yield return (CreateDefaultLocationToolAssignmentModel(),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestTechnique.TorqueCoefficient = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestTechnique.TorqueCoefficient));

            yield return (CreateDefaultLocationToolAssignmentModel(),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestTechnique.MinimumPulse = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestTechnique.MinimumPulse));

            yield return (CreateDefaultLocationToolAssignmentModel(),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestTechnique.MaximumPulse = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestTechnique.MaximumPulse));

            yield return (CreateDefaultLocationToolAssignmentModel(),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.TestTechnique.Threshold = 36,
                () => ("0", "36"),
                nameof(LocationToolAssignmentModel.TestTechnique.Threshold));

            yield return (CreateDefaultLocationToolAssignmentModel(),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.StartDateMfu = new DateTime(2021, 3, 29),
                () => ("01.01.0001", "29.03.2021"),
                nameof(LocationToolAssignmentModel.StartDateMfu));

            yield return (CreateDefaultLocationToolAssignmentModel(),
                (changedLocationToolAssignmentModel) => changedLocationToolAssignmentModel.StartDateChk = new DateTime(2021, 3, 29),
                () => ("01.01.0001", "29.03.2021"),
                nameof(LocationToolAssignmentModel.StartDateChk));
        }
    }
}