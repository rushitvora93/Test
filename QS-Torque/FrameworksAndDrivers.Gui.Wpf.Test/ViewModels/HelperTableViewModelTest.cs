using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using InterfaceAdapters.Models;
using TestHelper.Factories;
using TestHelper.Mock;
using InterfaceAdapters;
using System.Collections.ObjectModel;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    [TestFixture]
    class HelperTableViewModelTest
    {
        private RoutedEvent _selectionChanged;

        [Test, TestCaseSource(nameof(SavingHelperTableItemPassesNewHelperTableItemTestCases))]
        public void SavingHelperTableItemPassesNewHelperTableItem(SavingHelperTableItemTestCase testCase)
        {
            var helperTableUseCase = new HelperTableUseCaseMock();
            var startUp = new StartUpMock();
            var helperTableInterface = new HelperTableInterfaceMock();
            var viewModel = CreateHelperTableViewModelForMock(helperTableUseCase, startUp, helperTableInterface);

            AddThenChangeThenSaveItem(testCase, viewModel, helperTableInterface);

            Assert.AreEqual(testCase.itemId, helperTableUseCase.lastSaveItemChangedItem.ListId.ToLong());
            Assert.AreEqual(testCase.newDescription, helperTableUseCase.lastSaveItemChangedItem.Description.ToDefaultString());
        }

        [Test, TestCaseSource(nameof(SavingHelperTableItemPassesOldHelperTableItemTestCases))]
        public void SavingHelperTableItemPassesOldHelperTableItem(SavingHelperTableItemTestCase testCase)
        {
            var helperTableUseCase = new HelperTableUseCaseMock();
            var startUp = new StartUpMock();
            var helperTableInterface = new HelperTableInterfaceMock();
            var viewModel = CreateHelperTableViewModelForMock(helperTableUseCase, startUp, helperTableInterface);

            AddThenChangeThenSaveItem(testCase, viewModel, helperTableInterface);

            Assert.AreEqual(testCase.itemId, helperTableUseCase.lastSaveItemOldItem.ListId.ToLong());
            Assert.AreEqual(testCase.oldDescription, helperTableUseCase.lastSaveItemOldItem.Description.ToDefaultString());
        }

        [Test]
        public void SaveReferencedToolsInCollection()
        {
            var helperTableUseCase = new HelperTableUseCaseMock();
            var startUp = new StartUpMock();
            var helperTableInterface = new HelperTableInterfaceMock();
            var viewModel = CreateHelperTableViewModelForMock(helperTableUseCase, startUp, helperTableInterface);

            var tool1 = new ToolReferenceLink(new QstIdentifier(587458), "blub", "blub", null);
            var tool2 = new ToolReferenceLink(new QstIdentifier(3214578), "blub", "blub", null);

            var list = new List<ToolReferenceLink>() { tool1, tool2 };
            viewModel.ShowToolReferenceLinks(list);

            Assert.AreEqual(tool1.Id.ToLong(), (viewModel.ReferencedTools.GetItemAt(0) as ToolReferenceLink).Id.ToLong());
            Assert.AreEqual(tool2.Id.ToLong(), (viewModel.ReferencedTools.GetItemAt(1) as ToolReferenceLink).Id.ToLong());
        }

        [Test]
        public void SaveReferencedToolModelsInCollection()
        {
            var helperTableUseCase = new HelperTableUseCaseMock();
            var startUp = new StartUpMock();
            var helperTableInterface = new HelperTableInterfaceMock();
            var viewModel = CreateHelperTableViewModelForMock(helperTableUseCase, startUp, helperTableInterface);

            var toolModel1 = new ToolModelReferenceLink { Id = new ToolModelId(587458), DisplayName = "fgoiuzhgbvjfkle" };
            var toolModel2 = new ToolModelReferenceLink { Id = new ToolModelId(3214578), DisplayName = "t98gu7zhfvjdksl" };

            var list = new List<ToolModelReferenceLink>() { toolModel1, toolModel2 };
            viewModel.ShowReferencedToolModels(list);

            Assert.AreEqual(toolModel1.Id.ToLong(), (viewModel.ReferencedToolModels.GetItemAt(0) as ToolModelReferenceLink).Id.ToLong());
            Assert.AreEqual(toolModel1.DisplayName, (viewModel.ReferencedToolModels.GetItemAt(0) as ToolModelReferenceLink).DisplayName);
            Assert.AreEqual(toolModel2.Id.ToLong(), (viewModel.ReferencedToolModels.GetItemAt(1) as ToolModelReferenceLink).Id.ToLong());
            Assert.AreEqual(toolModel2.DisplayName, (viewModel.ReferencedToolModels.GetItemAt(1) as ToolModelReferenceLink).DisplayName);
        }

        [Test]
        public void SaveReferencedLocationToolAssignmentsInCollection()
        {
            var helperTableUseCase = new HelperTableUseCaseMock();
            var startUp = new StartUpMock();
            var helperTableInterface = new HelperTableInterfaceMock();
            var viewModel = CreateHelperTableViewModelForMock(helperTableUseCase, startUp, helperTableInterface);
            viewModel.ReferencedLocationToolAssignments.Add(new LocationToolAssignmentModel(null, new NullLocalizationWrapper()));

            var assignment1 = new LocationToolAssignment();
            var assignment2 = new LocationToolAssignment();

            var list = new List<LocationToolAssignment>() { assignment1, assignment2 };
            viewModel.ShowReferencedLocationToolAssignments(list);

            Assert.AreEqual(2, viewModel.ReferencedLocationToolAssignments.Count);
            Assert.AreEqual(assignment1, viewModel.ReferencedLocationToolAssignments[0].Entity);
            Assert.AreEqual(assignment2, viewModel.ReferencedLocationToolAssignments[1].Entity);
        }

        [Test]
        public void SavingItemTwoTimesWithoutSelectionChangePassesCorrectOldItem()
        {
            var helperTableUseCase = new HelperTableUseCaseMock();
            var startUp = new StartUpMock();
            var helperTableInterface = new HelperTableInterfaceMock();
            var viewModel = CreateHelperTableViewModelForMock(helperTableUseCase, startUp, helperTableInterface);

            AddThenChangeThenSaveItem(
                new SavingHelperTableItemTestCase { itemId = 5, oldDescription = "test", newDescription = "changed" },
                viewModel,
                helperTableInterface);
            ChangeThenSaveSelectedItem(new SavingHelperTableItemTestCase { itemId = 5, newDescription = "changed again" }, viewModel);

            Assert.AreEqual("changed", helperTableUseCase.lastSaveItemOldItem.Description.ToDefaultString());
        }

        [Test]
        public void SavingItemPassesItselfAsErrorGui()
        {
            var helperTableUseCase = new HelperTableUseCaseMock();
            var startUp = new StartUpMock();
            var helperTableInterface = new HelperTableInterfaceMock();
            var viewModel = CreateHelperTableViewModelForMock(helperTableUseCase, startUp, helperTableInterface);

            AddThenChangeThenSaveItem(
                new SavingHelperTableItemTestCase { itemId = 5, oldDescription = "test", newDescription = "changed" },
                viewModel,
                helperTableInterface);

            Assert.AreSame(viewModel, helperTableUseCase.LastSaveItemErrorGui);
        }

        [Test]
        public void ShowRemoveHelperTableItemPreventingReferencesCallsRequestWithCorrectParameters()
        {
            var helperTableUseCase = new HelperTableUseCaseMock();
            var startUp = new StartUpMock();
            var helperTableInterface = new HelperTableInterfaceMock();
            var viewModel = CreateHelperTableViewModelForMock(helperTableUseCase, startUp, helperTableInterface);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var toolModel1 = CreateToolModelByValueAndIdOnly(15, "Test");
            var toolModel2 = CreateToolModelByValueAndIdOnly(26, "Blub");
            viewModel.ReferencesDialogRequest += (sender, list) =>
            {
                Assert.AreEqual(2, list[0].References.Count);
                Assert.AreEqual(toolModel1.DisplayName, list[0].References[0]);
                Assert.AreEqual(toolModel2.DisplayName, list[0].References[1]);
                Assert.Pass();
            };
            viewModel.ShowRemoveHelperTableItemPreventingReferences(new List<ToolModelReferenceLink>{toolModel1, toolModel2}, new List<ToolReferenceLink>(), new List<LocationToolAssignment>());
            Assert.Fail();
        }

        [Test]
        public void InvokeRemoveItemExecuteCallsRemoveItemWithResetedParameters()
        {
            var helperTableUseCase = new HelperTableUseCaseMock();
            var helperTableInterface = new HelperTableInterfaceMock();
            var viewModel = CreateHelperTableViewModelForMock(helperTableUseCase, new StartUpMock(), helperTableInterface);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);

            var item = CreateHelperTableEntityMock.Anonymous();

            viewModel.SelectedItem = MapEntityToModel(item.CopyDeep());
            viewModel.SelectedItem.Value = "345345";

            viewModel.MessageBoxRequest += (sender, args) => args.ResultAction?.Invoke(MessageBoxResult.Yes);
            viewModel.RemoveItemCommand.Invoke(null);

            Assert.IsTrue(helperTableUseCase.RemoveItemParameter.EqualsByContent(item));
        }

        [Test]
        public void AddInvokesSelectAndFocusInputField()
        {
            var helperTableUseCase = new HelperTableUseCaseMock();
            var startUp = new StartUpMock();
            var helperTableInterface = new HelperTableInterfaceMock();
            var viewModel = CreateHelperTableViewModelForMock(helperTableUseCase, startUp, helperTableInterface);
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.SelectAndFocusInputField += (sender, args) => Assert.Pass();
            viewModel.Add(CreateHelperTableEntityMock.WithId(15));
            Assert.Fail();
        }

        [Test]
        public void MissingTests()
        {
            Assert.Ignore("Many things are not tested like loadItems, and invoking commands (add, remove, etc)");
        }

        private ToolModelReferenceLink CreateToolModelByValueAndIdOnly(int id, string value)
        {
            return new ToolModelReferenceLink { Id = new ToolModelId(id), DisplayName = value };
        }

        public class SavingHelperTableItemTestCase
        {
            public long itemId;
            public string oldDescription;
            public string newDescription;
        }

        static readonly List<SavingHelperTableItemTestCase> SavingHelperTableItemPassesNewHelperTableItemTestCases = new List<SavingHelperTableItemTestCase>
            {
                new SavingHelperTableItemTestCase{ itemId = 5, oldDescription = "Test", newDescription = "Changed"},
                new SavingHelperTableItemTestCase{ itemId=4567, oldDescription = "fjaksdlf", newDescription = "F" }
            };

        static readonly List<SavingHelperTableItemTestCase> SavingHelperTableItemPassesOldHelperTableItemTestCases = SavingHelperTableItemPassesNewHelperTableItemTestCases;

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            _selectionChanged = EventManager.RegisterRoutedEvent("selectionchanged", RoutingStrategy.Bubble, this.GetType(), this.GetType());
        }

        private class HelperTableUseCaseMock : IHelperTableUseCase<HelperTableEntityMock>
        {
            public HelperTableEntityMock lastSaveItemChangedItem;
            public HelperTableEntityMock lastSaveItemOldItem;
            public HelperTableEntityMock RemoveItemParameter { get; set; }
            public IHelperTableErrorGui<HelperTableEntityMock> LastSaveItemErrorGui;

            public void LoadItems(IHelperTableReadOnlyErrorGui<HelperTableEntityMock> errorGui)
            {
                throw new NotImplementedException();
            }

            public void AddItem(HelperTableEntityMock newItem, IHelperTableErrorGui<HelperTableEntityMock> errorGui)
            {
                throw new NotImplementedException();
            }

            public void RemoveItem(HelperTableEntityMock removedItem, IHelperTableErrorGui<HelperTableEntityMock> errorGui)
            {
                RemoveItemParameter = removedItem;
            }

            public void SaveItem(
                HelperTableEntityMock oldItem,
                HelperTableEntityMock changedItem,
                IHelperTableErrorGui<HelperTableEntityMock> errorGui)
            {
                lastSaveItemOldItem = oldItem;
                lastSaveItemChangedItem = changedItem;
                LastSaveItemErrorGui = errorGui;
            }

            public void LoadReferences(HelperTableEntityId id, IHelperTableShowReferencesGui errorGui)
            {
                throw new NotImplementedException();
            }
        }

        HelperTableItemModel<HelperTableEntityMock, string> MapEntityToModel(HelperTableEntityMock entity)
        {
            return new HelperTableItemModel<HelperTableEntityMock, string>(entity,
                e => e.Description.ToDefaultString(),
                (e, value) => entity.Description = new HelperTableDescription(value),
                () => new HelperTableEntityMock());
        }

        HelperTableEntityMock MapModelToEntity(HelperTableItemModel<HelperTableEntityMock, string> model)
        {
            return CreateHelperTableEntityMock.Parametrized(model.ListId, model.Value);
        }

        string GetHelperTableName()
        {
            return "HelperTableName";
        }

        private void AddOneItemAndSelectIt(IHelperTableInterface<HelperTableEntityMock, string> interfaceAdapter, HelperTableViewModel<HelperTableEntityMock, string> viewModel, HelperTableEntityMock entityToSave)
        {
            interfaceAdapter.HelperTableItems.Add(MapEntityToModel(entityToSave));
			viewModel.ShowItems(new List<HelperTableEntityMock> { entityToSave }); // to be removed with HelperTablesWithInterfaceAdapter
            viewModel.SelectedItem = (HelperTableItemModel<HelperTableEntityMock, string>)viewModel.HelperTableCollectionView.GetItemAt(0);
            viewModel.SelectionChangedCommand.Invoke(
                new SelectionChangedEventArgs(
                    _selectionChanged,
                    removedItems: new List<HelperTableItemModel<HelperTableEntityMock, string>>(),
                    addedItems: new List<HelperTableItemModel<HelperTableEntityMock, string>> { viewModel.SelectedItem }));
        }

        private static void SaveSelectedItem(HelperTableViewModel<HelperTableEntityMock, string> viewModel)
        {
            EventHandler<MessageBoxEventArgs> handler = (sender, eventArgs) => { eventArgs.ResultAction.Invoke(MessageBoxResult.Yes); };
            viewModel.MessageBoxRequest += handler;
            viewModel.SaveCommand.Execute(null);
            viewModel.MessageBoxRequest -= handler;
        }

        private HelperTableViewModel<HelperTableEntityMock, string> CreateHelperTableViewModelForMock(
            HelperTableUseCaseMock helperTableUseCase,
            StartUpMock startUp,
            HelperTableInterfaceMock helperTableInterface)
        {
            var viewModel = new HelperTableViewModel<HelperTableEntityMock, string>(
                            startUp,
                            helperTableUseCase,
                            helperTableInterface,
                            GetHelperTableName,
                            MapEntityToModel,
                            MapModelToEntity,
                            null,
                            () => new HelperTableEntityMock(), 
                            new NullLocalizationWrapper());
            viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            return viewModel;
        }

        private static void ChangeSelectedItemContents(HelperTableViewModel<HelperTableEntityMock, string> viewModel, string newContent)
        {
            viewModel.SelectedItem.Value = newContent;
        }

        private void AddThenChangeThenSaveItem(
            SavingHelperTableItemTestCase testCase,
            HelperTableViewModel<HelperTableEntityMock, string> viewModel,
            IHelperTableInterface<HelperTableEntityMock, string> interfaceAdapter)
        {
            AddOneItemAndSelectIt(interfaceAdapter, viewModel, CreateHelperTableEntityMock.Parametrized(testCase.itemId, testCase.oldDescription));
            ChangeThenSaveSelectedItem(testCase, viewModel);
        }

        private static void ChangeThenSaveSelectedItem(SavingHelperTableItemTestCase testCase, HelperTableViewModel<HelperTableEntityMock, string> viewModel)
        {
            ChangeSelectedItemContents(viewModel, testCase.newDescription);
            SaveSelectedItem(viewModel);
        }

        private class HelperTableInterfaceMock : IHelperTableInterface<HelperTableEntityMock, string>
        {
            public ObservableCollection<HelperTableItemModel<HelperTableEntityMock, string>> HelperTableItems { get; set; } = new ObservableCollection<HelperTableItemModel<HelperTableEntityMock, string>>();
        }
    }
}
