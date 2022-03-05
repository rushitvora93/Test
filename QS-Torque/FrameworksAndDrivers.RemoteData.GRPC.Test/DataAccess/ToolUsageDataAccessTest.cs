using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Client.TestHelper.Mock;
using Core.Entities;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;
using ToolUsageService;
using ToolUsage = DtoTypes.ToolUsage;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    public class ToolUsageClientMock : IToolUsageClient
    {
        public bool GetAllToolUsagesCalled { get; set; }
        public ListOfToolUsage GetAllToolUsagesReturnValue { get; set; } = new ListOfToolUsage();
        public Long GetToolUsageLocationToolAssignmentReferencesParameter { get; set; }
        public ListOfLongs GetToolUsageLocationToolAssignmentReferencesReturnValue { get; set; } = new ListOfLongs();
        public ListOfToolUsage InsertToolUsagesWithHistoryReturnValue { get; set; } = new ListOfToolUsage();
        public InsertToolUsagesWithHistoryRequest InsertToolUsagesWithHistoryParameter { get; set; }
        public ListOfToolUsage UpdateToolUsagesWithHistoryReturnValue { get; set; } = new ListOfToolUsage();
        public UpdateToolUsagesWithHistoryRequest UpdateToolUsagesWithHistoryParameter { get; set; }


        public ListOfToolUsage GetAllToolUsages()
        {
            GetAllToolUsagesCalled = true;
            return GetAllToolUsagesReturnValue;
        }

        public ListOfLongs GetToolUsageLocationToolAssignmentReferences(Long id)
        {
            GetToolUsageLocationToolAssignmentReferencesParameter = id;
            return GetToolUsageLocationToolAssignmentReferencesReturnValue;
        }

        public ListOfToolUsage InsertToolUsagesWithHistory(InsertToolUsagesWithHistoryRequest request)
        {
            InsertToolUsagesWithHistoryParameter = request;
            return InsertToolUsagesWithHistoryReturnValue;
        }

        public ListOfToolUsage UpdateToolUsagesWithHistory(UpdateToolUsagesWithHistoryRequest request)
        {
            UpdateToolUsagesWithHistoryParameter = request;
            return UpdateToolUsagesWithHistoryReturnValue;
        }
    }

    public class ToolUsageDataAccessTest
    {
        [Test]
        public void LoadItemsCallsClient()
        {
            var environment = new Environment();

            environment.dataAccess.LoadItems();

            Assert.IsTrue(environment.mocks.toolUsageClient.GetAllToolUsagesCalled);
        }

        private static IEnumerable<ListOfToolUsage> LoadToolUsagesData = new List<ListOfToolUsage>()
        {
            new ListOfToolUsage()
            {
                ToolUsageList =
                {
                    new ToolUsage() { Id = 1, Description = "1. Hand", Alive = true},
                    new ToolUsage() { Id = 133, Description = "Test 99", Alive = false}
                }
            },
            new ListOfToolUsage()
            {
                ToolUsageList =
                {
                    new ToolUsage() { Id = 31, Description = "1. Hand A", Alive = true},
                }
            }
        };

        [TestCaseSource(nameof(LoadToolUsagesData))]
        public void LoadItemsReturnsCorrectValue(ListOfToolUsage listOfToolUsages)
        {
            var environment = new Environment();
            environment.mocks.toolUsageClient.GetAllToolUsagesReturnValue = listOfToolUsages;

            var result = environment.dataAccess.LoadItems();

            var comparer = new Func<ToolUsage, Core.Entities.ToolUsage, bool>((dtoToolUsage, toolUsage) =>
                EqualityChecker.CompareToolUsageWithToolUsageDto(toolUsage, dtoToolUsage)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(listOfToolUsages.ToolUsageList, result, comparer);
        }

        [Test]
        public void AddItemWithoutUserThrowsArgumentException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.AddItem(null, null); });
        }

        static IEnumerable<(Core.Entities.ToolUsage, Core.Entities.User)> AddAndRemoveToolUsageData = new List<(Core.Entities.ToolUsage, Core.Entities.User)>()
        {
            (
                new Core.Entities.ToolUsage() {ListId = new HelperTableEntityId(1), Value = new ToolUsageDescription("1. Hand")},
                CreateUser.IdOnly(2)
            ),
            (
                new Core.Entities.ToolUsage() {ListId = new HelperTableEntityId(2), Value = new ToolUsageDescription("122. Hand")},
                CreateUser.IdOnly(32)
            )
        };

        [TestCaseSource(nameof(AddAndRemoveToolUsageData))]
        public void AddToolUsageCallsClient((Core.Entities.ToolUsage toolUsage, Core.Entities.User user) data)
        {
            var environment = new Environment();
            environment.mocks.toolUsageClient.InsertToolUsagesWithHistoryReturnValue = new ListOfToolUsage() { ToolUsageList = { new ToolUsage() } };

            environment.dataAccess.AddItem(data.toolUsage, data.user);

            var clientParam = environment.mocks.toolUsageClient.InsertToolUsagesWithHistoryParameter;
            var clientDiff = clientParam.ToolUsageDiffs.ToolUsageDiffs.First();

            Assert.AreEqual(1, clientParam.ToolUsageDiffs.ToolUsageDiffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.AreEqual("", clientDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareToolUsageWithToolUsageDto(data.toolUsage, clientDiff.NewToolUsage));
            Assert.IsNull(clientDiff.OldToolUsage);
            Assert.IsTrue(clientParam.ReturnList);
        }

        static IEnumerable<DtoTypes.ToolUsage> ToolUsageDtoData = new List<DtoTypes.ToolUsage>()
        {
            DtoFactory.CreateToolUsageDto(1, "1. Hand", true),
            DtoFactory.CreateToolUsageDto(13, "1. Test", false),
        };

        [TestCaseSource(nameof(ToolUsageDtoData))]
        public void AddToolUsageReturnsCorrectValue(DtoTypes.ToolUsage toolUsageDto)
        {
            var environment = new Environment();
            environment.mocks.toolUsageClient.InsertToolUsagesWithHistoryReturnValue =
                new ListOfToolUsage { ToolUsageList = { toolUsageDto } };

            var result = environment.dataAccess.AddItem(new Core.Entities.ToolUsage(), CreateUser.Anonymous());

            Assert.AreEqual(toolUsageDto.Id, result.ToLong());
        }

        [Test]
        public void AddToolUsageReturnsNullThrowsException()
        {
            var environment = new Environment();
            Assert.Throws<NullReferenceException>(() =>
            {
                environment.dataAccess.AddItem(new Core.Entities.ToolUsage(), CreateUser.Anonymous());
            });
        }

        [Test]
        public void RemoveItemWithoutUserThrowsArgumentException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.RemoveItem(null, null); });
        }

        [TestCaseSource(nameof(AddAndRemoveToolUsageData))]
        public void RemoveItemCallsClient((Core.Entities.ToolUsage toolUsage, Core.Entities.User user) data)
        {
            var environment = new Environment();
            environment.mocks.toolUsageClient.UpdateToolUsagesWithHistoryReturnValue =
                new ListOfToolUsage() { ToolUsageList = { new ToolUsage() } };

            environment.dataAccess.RemoveItem(data.toolUsage, data.user);

            var clientParam = environment.mocks.toolUsageClient.UpdateToolUsagesWithHistoryParameter;
            var clientDiff = clientParam.ToolUsageDiffs.ToolUsageDiffs.First();

            Assert.AreEqual(1, clientParam.ToolUsageDiffs.ToolUsageDiffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.AreEqual("", clientDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareToolUsageWithToolUsageDto(data.toolUsage, clientDiff.NewToolUsage));
            Assert.IsTrue(EqualityChecker.CompareToolUsageWithToolUsageDto(data.toolUsage, clientDiff.OldToolUsage));
            Assert.AreEqual(true, clientDiff.OldToolUsage.Alive);
            Assert.AreEqual(false, clientDiff.NewToolUsage.Alive);
        }

        [Test]
        public void SaveItemWithMismatchingIdsThrowsArgumentException()
        {
            var environment = new Environment();

            var oldItem = new Core.Entities.ToolUsage() { ListId = new HelperTableEntityId(1) };
            var newItem = new Core.Entities.ToolUsage() { ListId = new HelperTableEntityId(2) };
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.SaveItem(oldItem, newItem, CreateUser.Anonymous()); });
        }

        static IEnumerable<(Core.Entities.ToolUsage, Core.Entities.ToolUsage, Core.Entities.User)> SaveToolUsageData =
            new List<(Core.Entities.ToolUsage, Core.Entities.ToolUsage, Core.Entities.User)>()
        {
            (
                new Core.Entities.ToolUsage() {ListId = new HelperTableEntityId(1), Value = new ToolUsageDescription("1. Hand")},
                new Core.Entities.ToolUsage() {ListId = new HelperTableEntityId(1), Value = new ToolUsageDescription("1. Hand X")},
                CreateUser.IdOnly(2)
            ),
            (
                new Core.Entities.ToolUsage() {ListId = new HelperTableEntityId(14), Value = new ToolUsageDescription("Test")},
                new Core.Entities.ToolUsage() {ListId = new HelperTableEntityId(14), Value = new ToolUsageDescription("Test ABC")},
                CreateUser.IdOnly(20)
            )
        };

        [TestCaseSource(nameof(SaveToolUsageData))]
        public void SaveItemCallsClient((Core.Entities.ToolUsage oldToolUsage, Core.Entities.ToolUsage newToolUsage, Core.Entities.User user) data)
        {
            var environment = new Environment();
            environment.dataAccess.SaveItem(data.oldToolUsage, data.newToolUsage, data.user);

            var clientParam = environment.mocks.toolUsageClient.UpdateToolUsagesWithHistoryParameter;
            var clientDiff = clientParam.ToolUsageDiffs.ToolUsageDiffs.First();

            Assert.AreEqual(1, clientParam.ToolUsageDiffs.ToolUsageDiffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.IsTrue(EqualityChecker.CompareToolUsageWithToolUsageDto(data.newToolUsage, clientDiff.NewToolUsage));
            Assert.IsTrue(EqualityChecker.CompareToolUsageWithToolUsageDto(data.oldToolUsage, clientDiff.OldToolUsage));
            Assert.AreEqual(true, clientDiff.OldToolUsage.Alive);
            Assert.AreEqual(true, clientDiff.NewToolUsage.Alive);
        }

        [TestCase(1)]
        [TestCase(100)]
        public void LoadReferencedLocationToolAssignmentIdsCallsClient(long id)
        {
            var environment = new Environment();
            environment.dataAccess.LoadReferencedLocationToolAssignmentIds(new HelperTableEntityId(id));
            Assert.AreEqual(id, environment.mocks.toolUsageClient.GetToolUsageLocationToolAssignmentReferencesParameter.Value);
        }

        static IEnumerable<ListOfLongs> LoadReferencedLocationToolAssignmentsData = new List<ListOfLongs>()
        {
            new ListOfLongs()
            {
                Values =
                {
                    new Long{Value = 1},
                    new Long{Value = 2},
                    new Long{Value = 3},
                }
            },
            new ListOfLongs()
            {
                Values =
                {
                    new Long{Value = 5},
                    new Long{Value = 7},
                    new Long{Value = 9},
                }
            }
        };

        [TestCaseSource(nameof(LoadReferencedLocationToolAssignmentsData))]
        public void LoadReferencedLocationToolAssignmentIdsReturnsCorrectValue(ListOfLongs longs)
        {
            var environment = new Environment();
            environment.mocks.toolUsageClient.GetToolUsageLocationToolAssignmentReferencesReturnValue = longs;
            var result = environment.dataAccess.LoadReferencedLocationToolAssignmentIds(new HelperTableEntityId(1));

            var comparer = new Func<long, Long, bool>((val, valDto) =>
                val == valDto.Value
            );

            CheckerFunctions.CollectionAssertAreEquivalent(result.Select(x => x.ToLong()).ToList(), longs.Values, comparer);
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    clientFactory = new ClientFactoryMock();
                    channelWrapper = new ChannelWrapperMock();
                    toolUsageClient = new ToolUsageClientMock();
                    channelWrapper.GetToolUsageClientReturnValue = toolUsageClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                }
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
                public ToolUsageClientMock toolUsageClient;
            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new ToolUsageDataAccess(mocks.clientFactory);
            }

            public readonly Mocks mocks;
            public readonly ToolUsageDataAccess dataAccess;
        }
    }
}
