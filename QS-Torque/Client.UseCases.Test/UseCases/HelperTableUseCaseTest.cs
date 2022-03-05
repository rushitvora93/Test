using Core.Entities;
using Core.UseCases;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Core.Entities.ReferenceLink;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;

namespace Core.Test.UseCases
{
    class HelperTableUseCaseTest
    {
        public static List<List<ToolType>> LoadItemsTestData =
            new List<List<ToolType>>
            {
                new List<ToolType>
                {
                    CreateToolType.Parametrized(1, "Spare Day"),
                    CreateToolType.Parametrized(2, "Inappropriate Density")
                },
                new List<ToolType>
                {
                    CreateToolType.Parametrized(7, "Desired Irony"),
                    CreateToolType.Parametrized(9, "Unknown Parcel"),
                    CreateToolType.Parametrized(103, "Nasty Tour"),
                    CreateToolType.Parametrized(5, "Estimated Integration"),
                    CreateToolType.Parametrized(1, "Terrible Borough")
                }
            };

        [TestCaseSource(nameof(LoadItemsTestData))]
        public void LoadItemsTest(List<ToolType> items)
        {
            var environment = new Environment<ToolType>();
            environment.Mock.Data.LoadItemsReturn = items;
            environment.UseCase.LoadItems(null);
            CollectionAssert.AreEquivalent(items, environment.Mock.Gui.ShowItemsParameterItems);
        }

        [Test]
        public void LoadItemsErrorTest()
        {
            var environment = new Environment<ToolType>();
            environment.Mock.Data.LoadItemsException = new Exception();
            environment.UseCase.LoadItems(environment.Mock.ErrorGui);
            Assert.IsTrue(environment.Mock.ErrorGui.ShowErrorMessageWasCalled);
        }

        [TestCase(4, "Peculiar Tiger")]
        [TestCase(7, "Rubber Rocket")]
        public void AddItemTest(int id, string description)
        {
            var environment = new Environment<ToolType>();
            var expectedToolType = CreateToolType.Parametrized(id, description);
            environment.Mock.Data.NextAddItemReturn = new HelperTableEntityId(id);
            environment.UseCase.AddItem(CreateToolType.Parametrized(0, description), null);
            Assert.IsTrue(expectedToolType.EqualsByContent(environment.Mock.Gui.AddParameterNewItem));
        }

        [Test]
        public void AddItemThatAlreadyExistsShowsInGui()
        {
            var environment = new Environment<ToolType>();
            environment.Mock.Data.AddItemException = new EntryAlreadyExists("");
            environment.UseCase.AddItem(CreateToolType.Anonymous(), environment.Mock.ErrorGui);
            Assert.IsTrue(environment.Mock.ErrorGui.ShowEntryAlreadyExistsWasCalled);
        }

        [Test]
        public void AddItemErrorTest()
        {
            var environment = new Environment<ToolType>();
            environment.Mock.Data.AddItemException = new Exception();
            environment.UseCase.AddItem(CreateToolType.Anonymous(), environment.Mock.ErrorGui);
            Assert.IsTrue(environment.Mock.ErrorGui.ShowErrorMessageWasCalled);
        }

        [TestCase(5)]
        [TestCase(5678)]
		public void AddingItemForwardsCorrectUserIdToDataInterface(long userId)
		{
            var environment = new Environment<ToolType>();
            environment.Mock.UserGetter.NextReturnedUser = CreateUser.IdOnly(userId);
            environment.UseCase.AddItem(CreateToolType.Anonymous(), null);
            Assert.AreEqual(userId, environment.Mock.Data.LastAddItemParameterByUser.UserId.ToLong());
        }

        [Test]
        public void RemovingItemRemovesItemFromDb()
        {
            var environment = new Environment<ToolType>();
            var item = CreateToolType.Anonymous();
            environment.UseCase.RemoveItem(item, environment.Mock.ErrorGui);
            Assert.AreSame(item, environment.Mock.Data.LastRemoveItemParameterItem);
        }

        [Test]
        public void RemovingItemRemovesItemFromGui()
        {
            var environment = new Environment<ToolType>();
            var item = CreateToolType.Anonymous();
            environment.UseCase.RemoveItem(item, environment.Mock.ErrorGui);
            Assert.AreSame(item, environment.Mock.Gui.LastRemoveParameterRemoveItem);
        }

        [Test]
        public void RemoveItemErrorTest()
        {
            var environment = new Environment<ToolType>();
            environment.Mock.Data.RemoveItemException = new Exception();
            environment.UseCase.RemoveItem(CreateToolType.Anonymous(), environment.Mock.ErrorGui);
            Assert.IsTrue(environment.Mock.ErrorGui.ShowErrorMessageWasCalled);
        }

        [TestCase(5)]
        [TestCase(5678)]
        public void RemoveItemForwardsCorrectUserIdToDataInterface(long userId)
        {
            var environment = new Environment<ToolType>();
            environment.Mock.UserGetter.NextReturnedUser = CreateUser.IdOnly(userId);
            environment.UseCase.RemoveItem(CreateToolType.Anonymous(), environment.Mock.ErrorGui);
            Assert.AreEqual(userId, environment.Mock.Data.LastRemoveItemParameterByUser.UserId.ToLong());
        }

        [TestCase(false, false, true)]
        [TestCase(false, true, false)]
        [TestCase(false, true, true)]
        [TestCase(true, false, false)]
        [TestCase(true, false, true)]
        [TestCase(true, true, false)]
        [TestCase(true, true, true)]
        public void RemoveItemWithToolReferencesCallsGuiShowRemoveItemPreventingReferences(
            bool hasToolReferences,
            bool hasToolModelReferences,
            bool hasLocationToolAssignmentReferences)
        {
            var environment = new Environment<ToolType>();

            environment.Mock.Data.HasToolAsReference = hasToolReferences;
            var toolReferences = hasToolReferences 
                ? new List<ToolReferenceLink>
                    {
                        new ToolReferenceLink(new QstIdentifier(15), "", "", null)
                    }
                : new List<ToolReferenceLink>();
            environment.Mock.Data.LoadToolReferenceLinksReturn = toolReferences;

            environment.Mock.Data.HasToolModelAsReference = hasToolModelReferences;
            var toolModelReferences = hasToolModelReferences
                ? new List<ToolModelReferenceLink> {new ToolModelReferenceLink()}
                : new List<ToolModelReferenceLink>();
            environment.Mock.Data.LoadReferencedToolModelsReturn = toolModelReferences;

            environment.Mock.Data.HasLocationToolAssignmentAsReference = hasLocationToolAssignmentReferences;
            var locationToolAssignmentReferences = hasLocationToolAssignmentReferences
                ? new List<LocationToolAssignment> {CreateLocationToolAssignment.Anonymous()}
                : new List<LocationToolAssignment>();
            environment.Mock.AssignmentData.GetLocationToolAssignmentsByIdsReturnValue = locationToolAssignmentReferences;

            environment.UseCase.RemoveItem(CreateToolType.Anonymous(), environment.Mock.ErrorGui);

            CollectionAssert.AreEqual(
                toolReferences, 
                environment.Mock.ErrorGui.ShowRemoveHelperTableItemPreventingReferencesParameterTools);
            CollectionAssert.AreEqual(
                toolModelReferences,
                environment.Mock.ErrorGui.ShowRemoveHelperTableItemPreventingReferencesParameterToolModels);
            CollectionAssert.AreEqual(
                locationToolAssignmentReferences,
                environment.Mock.ErrorGui.ShowRemoveHelperTableItemPreventingReferencesParameterLocationToolAssignments);
        }

        [Test]
        public void SaveItemUpdatesGuiWithNewItem()
        {
            var environment = new Environment<ToolType>();
            var item = CreateToolType.Anonymous();
            environment.UseCase.SaveItem(null, item, null);
            Assert.AreSame(item, environment.Mock.Gui.LastSaveParameterSavedItem);
        }

        [Test]
        public void SaveItemUpdatesDataWithNewItem()
        {
            var environment = new Environment<ToolType>();
            var item = CreateToolType.Anonymous();
            environment.UseCase.SaveItem(null, item, null);
            Assert.AreSame(item, environment.Mock.Data.LastSaveItemParameterChangedItem);
        }

        [Test]
        public void SaveItemErrorTest()
        {
            var environment = new Environment<ToolType>();
            environment.Mock.Data.SaveItemException = new Exception();
            environment.UseCase.SaveItem(null, null, environment.Mock.ErrorGui);
            Assert.IsTrue(environment.Mock.ErrorGui.ShowErrorMessageWasCalled);
        }

        [Test]
        public void SaveItemAlreadyExistsTest2()
        {
            var environment = new Environment<ToolType>();
            environment.Mock.Data.SaveItemException = new EntryAlreadyExists("");
            environment.UseCase.SaveItem(null, null, environment.Mock.ErrorGui);
            Assert.IsTrue(environment.Mock.ErrorGui.ShowEntryAlreadyExistsWasCalled);
        }

        [Test]
        public void SavingItemForwardsCorrectUserIdToDataInterface()
        {
            var environment = new Environment<ToolType>();
            var user = CreateUser.Anonymous();
            environment.Mock.UserGetter.NextReturnedUser = user;
            environment.UseCase.SaveItem(null, null, null);
            Assert.AreSame(user, environment.Mock.Data.LastSaveItemParameterByUser);
        }

        [Test]
        public void SavingForwardsCorrectOldItemToDataInterface()
        {
            var environment = new Environment<ToolType>();
            var oldItem = CreateToolType.Anonymous();
            environment.UseCase.SaveItem(oldItem, CreateToolType.Anonymous(), null);
            Assert.AreSame(oldItem, environment.Mock.Data.LastSaveItemParameterOldItem);
        }

        [Test]
        public void LoadReferencedToolModelsTest()
        {
            var environment = new Environment<ToolType>();
            environment.Mock.Data.HasToolModelAsReference = true;
            var toolModels = new List<ToolModelReferenceLink>();
            environment.Mock.Data.LoadReferencedToolModelsReturn = toolModels;
            var helperTableItem = new HelperTableEntityId(0);
            environment.UseCase.LoadReferences(helperTableItem, environment.Mock.ShowReferences);
            Assert.AreSame(helperTableItem, environment.Mock.Data.LastLoadReferencedToolModelsParameterId);
            Assert.AreSame(
                toolModels,
                environment.Mock.ShowReferences.LastShowToolReferenceLinksParameterToolModelReferenceLinks);
        }

        [Test]
        public void LoadReferencedToolModelsErrorTest()
        {
            var environment = new Environment<ToolType>();
            environment.Mock.Data.HasToolModelAsReference = true;
            environment.Mock.Data.LoadReferencedToolModelsException = new Exception();
            environment.UseCase.LoadReferences(null, environment.Mock.ShowReferences);
            Assert.IsTrue(environment.Mock.ShowReferences.ShowReferencesErrorWasCalled);
        }

        [Test]
        public void LoadReferencedTools()
        {
            var environment = new Environment<ToolType>();
            environment.Mock.Data.HasToolAsReference = true;
            var tools = new List<ToolReferenceLink>();
            environment.Mock.Data.LoadToolReferenceLinksReturn = tools;
            var helperTableItem = new HelperTableEntityId(0);
            environment.UseCase.LoadReferences(helperTableItem, environment.Mock.ShowReferences);
            Assert.AreSame(helperTableItem, environment.Mock.Data.LastLoadToolReferenceLinksParameterId);
            Assert.AreSame(tools, environment.Mock.ShowReferences.LastShowToolReferenceLinksParameterToolReferenceLinks);
        }

        [Test]
        public void LoadReferencedToolsError()
        {
            var environment = new Environment<ToolType>();
            environment.Mock.Data.HasToolAsReference = true;
            environment.Mock.Data.LoadToolReferenceLinksException = new Exception();
            environment.UseCase.LoadReferences(null, environment.Mock.ShowReferences);
            Assert.IsTrue(environment.Mock.ShowReferences.ShowReferencesErrorWasCalled);
        }

        [Test]
        public void LoadReferencedLocationToolAssignmentsCallsDataAccess()
        {
            var environment = new Environment<ToolType>();
            environment.Mock.Data.HasLocationToolAssignmentAsReference = true;
            var assignments = new List<LocationToolAssignment>();
            environment.Mock.AssignmentData.GetLocationToolAssignmentsByIdsReturnValue = assignments;
            environment.UseCase.LoadReferences(null, environment.Mock.ShowReferences);
            Assert.AreSame(
                assignments, 
                environment.Mock.ShowReferences.ShowReferencedLocationToolAssignmentsParameterAssignments);
        }

        [Test]
        public void LoadReferencedLocationToolAssignmentsError()
        {
            var environment = new Environment<ToolType>();
            environment.Mock.Data.HasLocationToolAssignmentAsReference = true;
            environment.Mock.Data.LoadReferencedLocationToolAssignmentIdsException = new Exception();
            environment.UseCase.LoadReferences(null, environment.Mock.ShowReferences);
            Assert.IsTrue(environment.Mock.ShowReferences.ShowReferencesErrorWasCalled);
        }

        [Test]
        public void AddItemWithoutErrorCallsSendSuccessNotification()
        {
            var environment = new Environment<ToolType>();
            environment.UseCase.AddItem(CreateToolType.DescriptionOnly("NewItem"), null);
            AsyncCallCheckerNoAssuredTimeout.OnCallCheck(environment.Mock.NotificationManager.SendSuccessNotificationTaskCalled.Task, 0, () =>
            {
                Assert.IsTrue(environment.Mock.NotificationManager.SendSuccessNotificationCalled);
            });
        }

        [Test]
        public void SaveItemWithoutErrorCallsSendSuccessNotification()
        {
            var environment = new Environment<ToolType>();
            environment.UseCase.SaveItem(CreateToolType.Anonymous(), CreateToolType.Anonymous(), null);
            AsyncCallCheckerNoAssuredTimeout.OnCallCheck(environment.Mock.NotificationManager.SendSuccessNotificationTaskCalled.Task, 0, () =>
            {
                Assert.IsTrue(environment.Mock.NotificationManager.SendSuccessNotificationCalled);
            });
        }

        [Test]
        public void RemoveItemWithoutErrorCallsSendSuccessNotification()
        {
            var environment = new Environment<ToolType>();
            environment.UseCase.RemoveItem(CreateToolType.Anonymous(), environment.Mock.ErrorGui);
            AsyncCallCheckerNoAssuredTimeout.OnCallCheck(environment.Mock.NotificationManager.SendSuccessNotificationTaskCalled.Task, 0, () =>
            {
                Assert.IsTrue(environment.Mock.NotificationManager.SendSuccessNotificationCalled);
            });
        }


        private class HelperTableGuiSynchronousMock<T> : IHelperTableGui<T> where T : HelperTableEntity
        {
            public void ShowItems(List<T> items)
            {
                ShowItemsParameterItems = items;
            }

            public void Add(T newItem)
            {
                AddParameterNewItem = newItem;
            }

            public void Save(T savedItem)
            {
                LastSaveParameterSavedItem = savedItem;
            }

            public void Remove(T removeItem)
            {
                LastRemoveParameterRemoveItem = removeItem;
            }

            public void ShowErrorMessage()
            {
                ShowErrorMessageWasCalled = true;
            }

            public void ShowReferencedToolModels(List<ToolModelReferenceLink> toolModelReferenceLinks)
            {
                LastShowToolReferenceLinksParameterToolModelReferenceLinks = toolModelReferenceLinks;
            }

            public void ShowToolReferenceLinks(List<ToolReferenceLink> toolReferenceLinks)
            {
                LastShowToolReferenceLinksParameterToolReferenceLinks = toolReferenceLinks;
            }

            public void ShowReferencedLocationToolAssignments(List<LocationToolAssignment> assignments)
            {
                ShowReferencedLocationToolAssignmentsParameterAssignments = assignments;
            }

            public void ShowReferencesError()
            {
                ShowReferencesErrorWasCalled = true;
            }

            public void ShowEntryAlreadyExists(T newItem)
            {
                ShowEntryAlreadyExistsWasCalled = true;
            }

            public void ShowRemoveHelperTableItemPreventingReferences(
                List<ToolModelReferenceLink> toolModels,
                List<ToolReferenceLink> tools,
                List<LocationToolAssignment> locationToolAssignments)
            {
                ShowRemoveHelperTableItemPreventingReferencesParameterTools = tools;
                ShowRemoveHelperTableItemPreventingReferencesParameterToolModels = toolModels;
                ShowRemoveHelperTableItemPreventingReferencesParameterLocationToolAssignments = locationToolAssignments;
            }

            public List<T> ShowItemsParameterItems;
            public bool ShowErrorMessageWasCalled;
            public T AddParameterNewItem;
            public bool ShowEntryAlreadyExistsWasCalled;
            public T LastRemoveParameterRemoveItem;
            public List<ToolReferenceLink> ShowRemoveHelperTableItemPreventingReferencesParameterTools;
            public List<ToolModelReferenceLink> ShowRemoveHelperTableItemPreventingReferencesParameterToolModels;
            public List<LocationToolAssignment> ShowRemoveHelperTableItemPreventingReferencesParameterLocationToolAssignments;
            public T LastSaveParameterSavedItem;
            public List<ToolReferenceLink> LastShowToolReferenceLinksParameterToolReferenceLinks;
            public List<ToolModelReferenceLink> LastShowToolReferenceLinksParameterToolModelReferenceLinks;
            public List<LocationToolAssignment> ShowReferencedLocationToolAssignmentsParameterAssignments;
            public bool ShowReferencesErrorWasCalled;
        }

        private class HelperTableDataSynchronousMock<T> : IHelperTableData<T> where T : HelperTableEntity
        {
            public List<T> LoadItems()
            {
                if (LoadItemsException != null)
                {
                    throw LoadItemsException;
                }
                return LoadItemsReturn;
            }

            public HelperTableEntityId AddItem(T item, User byUser)
            {
                LastAddItemParameterItem = item;
                LastAddItemParameterByUser = byUser;
                if (AddItemException != null)
                {
                    throw AddItemException;
                }
                return NextAddItemReturn;
            }

            public void RemoveItem(T item, User byUser)
            {
                LastRemoveItemParameterItem = item;
                LastRemoveItemParameterByUser = byUser;
                if (RemoveItemException != null)
                {
                    throw RemoveItemException;
                }
            }

            public void SaveItem(T oldItem, T changedItem, User byUser)
            {
                LastSaveItemParameterChangedItem = changedItem;
                LastSaveItemParameterOldItem = oldItem;
                LastSaveItemParameterByUser = byUser;
                if (SaveItemException != null)
                {
                    throw SaveItemException;
                }
            }

            public List<ToolModelReferenceLink> LoadReferencedToolModels(HelperTableEntityId id)
            {
                LastLoadReferencedToolModelsParameterId = id;
                if (LoadReferencedToolModelsException != null)
                {
                    throw LoadReferencedToolModelsException;
                }
                return LoadReferencedToolModelsReturn;
            }

            public List<ToolReferenceLink> LoadToolReferenceLinks(HelperTableEntityId id)
            {
                LastLoadToolReferenceLinksParameterId = id;
                if (LoadToolReferenceLinksException != null)
                {
                    throw LoadToolReferenceLinksException;
                }
                return LoadToolReferenceLinksReturn;
            }

            public List<LocationToolAssignmentId> LoadReferencedLocationToolAssignmentIds(HelperTableEntityId id)
            {
                if (LoadReferencedLocationToolAssignmentIdsException != null)
                {
                    throw LoadReferencedLocationToolAssignmentIdsException;
                }
                return null;
            }

            public bool HasToolModelAsReference { get; set; }
            public bool HasToolAsReference { get; set; }
            public bool HasLocationToolAssignmentAsReference { get; set; }

            public List<T> LoadItemsReturn;
            public Exception LoadItemsException;
            public T LastAddItemParameterItem;
            public HelperTableEntityId NextAddItemReturn;
            public Exception AddItemException;
            public User LastAddItemParameterByUser;
            public T LastRemoveItemParameterItem;
            public Exception RemoveItemException;
            public User LastRemoveItemParameterByUser;
            public List<ToolReferenceLink> LoadToolReferenceLinksReturn;
            public List<ToolModelReferenceLink> LoadReferencedToolModelsReturn;
            public T LastSaveItemParameterChangedItem;
            public Exception SaveItemException;
            public T LastSaveItemParameterOldItem;
            public User LastSaveItemParameterByUser;
            public HelperTableEntityId LastLoadToolReferenceLinksParameterId;
            public HelperTableEntityId LastLoadReferencedToolModelsParameterId;
            public Exception LoadReferencedToolModelsException;
            public Exception LoadToolReferenceLinksException;
            public Exception LoadReferencedLocationToolAssignmentIdsException;
        }

        private class ErrorGuiMock<T> : IHelperTableErrorGui<T>
        {
            public void ShowErrorMessage()
            {
                ShowErrorMessageWasCalled = true;
            }

            public void ShowEntryAlreadyExists(T newItem)
            {
                ShowEntryAlreadyExistsWasCalled = true;
            }

            public void ShowRemoveHelperTableItemPreventingReferences(
                List<ToolModelReferenceLink> toolModels,
                List<ToolReferenceLink> tools,
                List<LocationToolAssignment> locationToolAssignments)
            {
                ShowRemoveHelperTableItemPreventingReferencesParameterTools = tools;
                ShowRemoveHelperTableItemPreventingReferencesParameterToolModels = toolModels;
                ShowRemoveHelperTableItemPreventingReferencesParameterLocationToolAssignments = locationToolAssignments;
            }

            public bool ShowErrorMessageWasCalled = false;
            public bool ShowReferencesErrorWasCalled = false;
            public bool ShowEntryAlreadyExistsWasCalled = false;
            public List<ToolReferenceLink> ShowRemoveHelperTableItemPreventingReferencesParameterTools;
            public List<ToolModelReferenceLink> ShowRemoveHelperTableItemPreventingReferencesParameterToolModels;
            public List<LocationToolAssignment> ShowRemoveHelperTableItemPreventingReferencesParameterLocationToolAssignments;
        }

        private class ShowReferencesMock : IHelperTableShowReferencesGui
        {
            public void ShowReferencedToolModels(List<ToolModelReferenceLink> toolModelReferenceLinks)
            {
                LastShowToolReferenceLinksParameterToolModelReferenceLinks = toolModelReferenceLinks;
            }

            public void ShowToolReferenceLinks(List<ToolReferenceLink> toolReferenceLinks)
            {
                LastShowToolReferenceLinksParameterToolReferenceLinks = toolReferenceLinks;
            }

            public void ShowReferencedLocationToolAssignments(List<LocationToolAssignment> assignments)
            {
                ShowReferencedLocationToolAssignmentsParameterAssignments = assignments;
            }

            public void ShowReferencesError()
            {
                ShowReferencesErrorWasCalled = true;
            }

            public bool ShowReferencesErrorWasCalled = false;
            public List<ToolReferenceLink> LastShowToolReferenceLinksParameterToolReferenceLinks;
            public List<ToolModelReferenceLink> LastShowToolReferenceLinksParameterToolModelReferenceLinks;
            public List<LocationToolAssignment> ShowReferencedLocationToolAssignmentsParameterAssignments;
        }

        private class Environment<T> where T : HelperTableEntity
        {
            public class Mocks
            {
                public Mocks()
                {
                    Gui = new HelperTableGuiSynchronousMock<T>();
                    Data = new HelperTableDataSynchronousMock<T>();
                    UserGetter = new UserGetterMock();
                    AssignmentData = new LocationToolAssignmentDataMock();
                    NotificationManager = new NotificationManagerMock();
                    ErrorGui = new ErrorGuiMock<T>();
                    ShowReferences = new ShowReferencesMock();
                }

                public readonly HelperTableGuiSynchronousMock<T> Gui;
                public readonly HelperTableDataSynchronousMock<T> Data;
                public readonly UserGetterMock UserGetter;
                public readonly LocationToolAssignmentDataMock AssignmentData;
                public readonly NotificationManagerMock NotificationManager;
                public readonly ErrorGuiMock<T> ErrorGui;
                public readonly ShowReferencesMock ShowReferences;
            }

            public Environment()
            {
                Mock = new Mocks();
                UseCase = new HelperTableUseCase<T>(
                    Mock.Data,
                    Mock.Gui,
                    Mock.UserGetter,
                    Mock.NotificationManager,
                    Mock.AssignmentData);
            }

            public readonly HelperTableUseCase<T> UseCase;
            public readonly Mocks Mock;
        }
    }
}
