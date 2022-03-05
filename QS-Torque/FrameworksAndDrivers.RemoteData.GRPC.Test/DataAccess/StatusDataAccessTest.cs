using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Client.TestHelper.Mock;
using Core.Entities;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using StatusService;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;
using ToolReferenceLink = Core.Entities.ReferenceLink.ToolReferenceLink;
using User = Core.Entities.User;
using Status = Core.Entities.Status;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    public class StatusClientMock : IStatusClient
    {
        public bool LoadStatusCalled { get; private set; }
        public ListOfStatus LoadStatusReturnValue { get; set; } = new ListOfStatus();
        public ListOfToolReferenceLink GetStatusToolLinksReturnValue { get; set; } = new ListOfToolReferenceLink();
        public LongRequest GetStatusToolLinksParameter { get; set; }
        public ListOfStatus InsertStatusWithHistoryReturnValue { get; set; }
        public InsertStatusWithHistoryRequest InsertStatusWithHistoryParameter { get; set; }
        public ListOfStatus UpdateStatusWithHistoryReturnValue { get; set; }
        public UpdateStatusWithHistoryRequest UpdateStatusWithHistoryParameter { get; set; }

        public ListOfStatus LoadStatus()
        {
            LoadStatusCalled = true;
            return LoadStatusReturnValue;
        }

        public ListOfToolReferenceLink GetStatusToolLinks(LongRequest status)
        {
            GetStatusToolLinksParameter = status;
            return GetStatusToolLinksReturnValue;
        }

        public ListOfStatus InsertStatusWithHistory(InsertStatusWithHistoryRequest request)
        {
            InsertStatusWithHistoryParameter = request;
            return InsertStatusWithHistoryReturnValue;
        }

        public ListOfStatus UpdateStatusWithHistory(UpdateStatusWithHistoryRequest request)
        {
            UpdateStatusWithHistoryParameter = request;
            return UpdateStatusWithHistoryReturnValue;
        }
    }

    public class StatusDataAccessTest
    {
        [Test]
        public void LoadItemsCallsClient()
        {
            var environment = new Environment();
            environment.dataAccess.LoadItems();
            Assert.IsTrue(environment.mocks.statusClient.LoadStatusCalled);
        }

        static IEnumerable<ListOfStatus> LoadItemsData = new List<ListOfStatus>()
        {
            new ListOfStatus()
            {
                Status =
                {
                    CreateStatus(1, "status1"),
                    CreateStatus(99, "status 99")
                },
             },
             new ListOfStatus()
             {
                Status =
                {
                    CreateStatus(55, "ok status")
                }
             }
        };

        [TestCaseSource(nameof(LoadItemsData))]
        public void LoadItemsReturnsCorrectValue(ListOfStatus listOfStatus)
        {
            var environment = new Environment();
            environment.mocks.statusClient.LoadStatusReturnValue = listOfStatus;
            var result = environment.dataAccess.LoadItems();

            var comparer = new Func<DtoTypes.Status, Status, bool>((statusDto, status) =>
                EqualityChecker.CompareStatusWithStatusDto(status, statusDto)
             );

            CheckerFunctions.CollectionAssertAreEquivalent(listOfStatus.Status, result, comparer);
        }

        [TestCase(1)]
        [TestCase(100)]
        public void LoadToolReferenceLinksCallsClient(long status)
        {
            var environment = new Environment();
            environment.dataAccess.LoadToolReferenceLinks(new HelperTableEntityId(status));
            Assert.AreEqual(status, environment.mocks.statusClient.GetStatusToolLinksParameter.Value);
        }

        static IEnumerable<ListOfToolReferenceLink> ToolReferenceLinkData = new List<ListOfToolReferenceLink>()
        {
            new ListOfToolReferenceLink()
            {
                ToolReferenceLinks =
                {
                    CreateToolReferenceLink(1, "234435", "546547679"),
                    CreateToolReferenceLink(99, "344 99", "34576")
                }
            },
            new ListOfToolReferenceLink()
            {
                ToolReferenceLinks =
                {
                    CreateToolReferenceLink(55, "4444 54", "32446")
                }
            }
        };

        [TestCaseSource(nameof(ToolReferenceLinkData))]
        public void LoadItemsReturnsCorrectValue(ListOfToolReferenceLink toolReferenceLinks)
        {
            var environment = new Environment();
            environment.mocks.statusClient.GetStatusToolLinksReturnValue = toolReferenceLinks;
            var result = environment.dataAccess.LoadToolReferenceLinks(new HelperTableEntityId(1));

            var comparer = new Func<DtoTypes.ToolReferenceLink, ToolReferenceLink, bool>((toolReferenceLinkDto, toolReferenceLink) =>
                toolReferenceLinkDto.Id == toolReferenceLink.Id.ToLong() &&
                toolReferenceLinkDto.InventoryNumber == toolReferenceLink.InventoryNumber &&
                toolReferenceLinkDto.SerialNumber == toolReferenceLink.SerialNumber
            );

            CheckerFunctions.CollectionAssertAreEquivalent(toolReferenceLinks.ToolReferenceLinks, result, comparer);
        }

        [Test]
        public void AddingItemWithoutUserThrowsArgumentException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.AddItem(null, null); });
        }

        static IEnumerable<(Status, User)> AddAndRemoveStatusData = new List<(Status, User)>()
        {
            (
                new Status() {ListId = new HelperTableEntityId(92), Value = new StatusDescription("Status 1")},
                CreateUser.IdOnly(2)
            ),
            (
                new Status() {ListId = new HelperTableEntityId(9), Value = new StatusDescription("Status 2")},
                CreateUser.IdOnly(2)
            )
        };

        [TestCaseSource(nameof(AddAndRemoveStatusData))]
        public void AddItemCallsClient((Status status, User user) data)
        {
            var environment = new Environment();
            environment.mocks.statusClient.InsertStatusWithHistoryReturnValue = new ListOfStatus() { Status = { new DtoTypes.Status() } };

            environment.dataAccess.AddItem(data.status, data.user);

            var clientParam = environment.mocks.statusClient.InsertStatusWithHistoryParameter;
            var clientStatusDiff = clientParam.StatusDiffs.StatusDiff.First();

            Assert.AreEqual(1, clientParam.StatusDiffs.StatusDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientStatusDiff.UserId);
            Assert.AreEqual("", clientStatusDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareStatusWithStatusDto(data.status, clientStatusDiff.NewStatus));
            Assert.IsNull(clientStatusDiff.OldStatus);
            Assert.IsTrue(clientParam.ReturnList);
        }

        [TestCase(1)]
        [TestCase(55)]
        public void AddItemReturnsCorrectValue(long status)
        {
            var environment = new Environment();
            environment.mocks.statusClient.InsertStatusWithHistoryReturnValue = new ListOfStatus() { Status = { new DtoTypes.Status() { Id = status } } };
            var result = environment.dataAccess.AddItem(
                new Status() { ListId = new HelperTableEntityId(1), Value = new StatusDescription("") },
                CreateUser.Anonymous());

            Assert.AreEqual(result.ToLong(), status);
        }

        [Test]
        public void AddingItemReturnsNullThrowsException()
        {
            var environment = new Environment();
            Assert.Throws<NullReferenceException>(() =>
            {
                environment.dataAccess.AddItem(new Status(), CreateUser.Anonymous());
            });
        }

        [Test]
        public void SaveItemWithMismatchingIdsThrowsArgumentException()
        {
            var environment = new Environment();

            var oldItem = new Status() { ListId = new HelperTableEntityId(1) };
            var newItem = new Status() { ListId = new HelperTableEntityId(88) };
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.SaveItem(oldItem, newItem, CreateUser.Anonymous()); });
        }

        static IEnumerable<(Status, Status, User)> SaveManufacturerData = new List<(Status, Status, User)>()
        {
            (
                new Status(){ListId = new HelperTableEntityId(1), Value = new StatusDescription("23435")},
                new Status(){ListId = new HelperTableEntityId(1), Value = new StatusDescription("aaaaa")},
                CreateUser.IdOnly(2)
            ),
            (
                new Status(){ListId = new HelperTableEntityId(11), Value = new StatusDescription("bbbb")},
                new Status(){ListId = new HelperTableEntityId(11), Value = new StatusDescription("546567868")},
                CreateUser.IdOnly(2)
            )
        };

        [TestCaseSource(nameof(SaveManufacturerData))]
        public void SaveItemCallsClient((Status oldStatus, Status newStatus, User user) data)
        {
            var environment = new Environment();

            environment.dataAccess.SaveItem(data.oldStatus, data.newStatus, data.user);

            var clientParam = environment.mocks.statusClient.UpdateStatusWithHistoryParameter;
            var clientStatusDiff = clientParam.StatusDiffs.StatusDiff.First();

            Assert.AreEqual(1, clientParam.StatusDiffs.StatusDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientStatusDiff.UserId);
            Assert.IsTrue(EqualityChecker.CompareStatusWithStatusDto(data.newStatus, clientStatusDiff.NewStatus));
            Assert.IsTrue(EqualityChecker.CompareStatusWithStatusDto(data.oldStatus, clientStatusDiff.OldStatus));
            Assert.AreEqual(true, clientStatusDiff.OldStatus.Alive);
            Assert.AreEqual(true, clientStatusDiff.NewStatus.Alive);
        }

        [Test]
        public void RemoveItemWithoutUserThrowsArgumentException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.RemoveItem(null, null); });
        }

        [TestCaseSource(nameof(AddAndRemoveStatusData))]
        public void RemoveItemCallsClient((Status status, User user) data)
        {
            var environment = new Environment();

            environment.dataAccess.RemoveItem(data.status, data.user);

            var clientParam = environment.mocks.statusClient.UpdateStatusWithHistoryParameter;
            var clientStatusDiff = clientParam.StatusDiffs.StatusDiff.First();

            Assert.AreEqual(1, clientParam.StatusDiffs.StatusDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientStatusDiff.UserId);
            Assert.IsTrue(EqualityChecker.CompareStatusWithStatusDto(data.status, clientStatusDiff.NewStatus));
            Assert.IsTrue(EqualityChecker.CompareStatusWithStatusDto(data.status, clientStatusDiff.OldStatus));
            Assert.AreEqual(true, clientStatusDiff.OldStatus.Alive);
            Assert.AreEqual(false, clientStatusDiff.NewStatus.Alive);
        }

        

        private static DtoTypes.Status CreateStatus(long id, string description)
        {
            return new DtoTypes.Status()
            {
                Id = id,
                Description = description
            };
        }

        private static DtoTypes.ToolReferenceLink CreateToolReferenceLink(long id, string inventoryNumber, string serialNumber)
        {
            return new DtoTypes.ToolReferenceLink()
            {
                Id = id,
                SerialNumber = serialNumber,
                InventoryNumber = inventoryNumber
            };
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    clientFactory = new ClientFactoryMock();
                    channelWrapper = new ChannelWrapperMock();
                    statusClient = new StatusClientMock();
                    channelWrapper.GetStatusClientReturnValue = statusClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                }
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
                public StatusClientMock statusClient;
            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new StatusDataAccess(mocks.clientFactory, null);
            }

            public readonly Mocks mocks;
            public readonly StatusDataAccess dataAccess;
        }
    }
}
