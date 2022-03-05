using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Client.TestHelper.Mock;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using TestHelper.Factories;
using TestHelper.Mock;
using HelperTableService;
using State;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using Grpc.Core;
using TestHelper.Checker;
using HelperTableEntity = DtoTypes.HelperTableEntity;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    public class HelperTableClientMock : IHelperTableClient
    {
        public UpdateHelperTableEntityWithHistoryRequest UpdateHelperTableEntityWithHistoryParameter { get; private set; }
        public ListOfHelperTableEntity UpdateHelperTableEntityWithHistoryReturnValue { get; set; }
        public ListOfHelperTableEntity InsertHelperTableEntityWithHistoryReturnValue { get; set; } = new ListOfHelperTableEntity();
        public InsertHelperTableEntityWithHistoryRequest InsertHelperTableEntityWithHistoryParameter { get; private set; }
        public HelperTableEntityLinkRequest GetHelperTableEntityToolLinksParameter { get; private set; }
        public ListOfToolReferenceLink GetHelperTableEntityToolLinksReturnValue { get; set; } = new ListOfToolReferenceLink();
        public ListOfModelLink GetHelperTableEntityModelLinksReturnValue { get; set; } = new ListOfModelLink();
        public HelperTableEntityLinkRequest GetHelperTableEntityModelLinksParameter { get; private set; }
        public ListOfHelperTableEntity GetHelperTableByNodeIdReturnValue { get; set; } = new ListOfHelperTableEntity();
        public Long GetHelperTableByNodeIdParameter { get; private set; }
        public RpcException InsertHelperTableEntityWithHistoryThrow { get; set; }

        public ListOfHelperTableEntity GetAllHelperTableEntities()
        {
            throw new NotImplementedException();
        }

        public ListOfHelperTableEntity GetHelperTableByNodeId(Long request)
        {
            GetHelperTableByNodeIdParameter = request;
            return GetHelperTableByNodeIdReturnValue;
        }

        public ListOfModelLink GetHelperTableEntityModelLinks(HelperTableEntityLinkRequest request)
        {
            GetHelperTableEntityModelLinksParameter = request;
            return GetHelperTableEntityModelLinksReturnValue;
        }

        public ListOfToolReferenceLink GetHelperTableEntityToolLinks(HelperTableEntityLinkRequest request)
        {
            GetHelperTableEntityToolLinksParameter = request;
            return GetHelperTableEntityToolLinksReturnValue;
        }

        public ListOfHelperTableEntity InsertHelperTableEntityWithHistory(InsertHelperTableEntityWithHistoryRequest request)
        {
            if (InsertHelperTableEntityWithHistoryThrow != null)
            {
                throw InsertHelperTableEntityWithHistoryThrow;
            }
            InsertHelperTableEntityWithHistoryParameter = request;
            return InsertHelperTableEntityWithHistoryReturnValue;
        }

        public ListOfHelperTableEntity UpdateHelperTableEntityWithHistory(UpdateHelperTableEntityWithHistoryRequest request)
        {
            UpdateHelperTableEntityWithHistoryParameter = request;
            return UpdateHelperTableEntityWithHistoryReturnValue;
        }
    }

    public class HelperTableDataAccessTest
    {
        [TestCase(NodeId.ConfigurableField)]
        [TestCase(NodeId.CostCenter)]
        public void LoadItemsCallsClient(NodeId nodeId)
        {
            var environment = new Environment();
            environment.dataAccess = CreateHelperTableDataAccessParametrized(nodeId,
                new HelperTableEntityMockSupportMock(), environment.mocks.clientFactory);

            environment.dataAccess.LoadItems();

            Assert.AreEqual(nodeId, (NodeId)environment.mocks.helperTableClient.GetHelperTableByNodeIdParameter.Value);
        }

        private static IEnumerable<List<HelperTableEntityMock>> LoadItemsReturnsCorrectValueData =
            new List<List<HelperTableEntityMock>>()
            {
                new List<HelperTableEntityMock>()
                {
                    CreateHelperTableEntityMock.Parametrized(1, "Test A"),
                    CreateHelperTableEntityMock.Parametrized(15, "Helper Enitity 99")
                },
                new List<HelperTableEntityMock>()
                {
                    CreateHelperTableEntityMock.Parametrized(99, "Cost Center X")
                }
            };

        [TestCaseSource(nameof(LoadItemsReturnsCorrectValueData))]
        public void LoadItemsReturnsCorrectValue(List<HelperTableEntityMock> entities)
        {
            var environment = new Environment();
            var helperTableEntities = new ListOfHelperTableEntity();
            foreach (var entity in entities)
            {
                helperTableEntities.HelperTableEntities.Add(new HelperTableEntity()
                {
                    ListId = entity.ListId.ToLong(),
                    Value = entity.Description.ToDefaultString()
                });
            }
            environment.mocks.helperTableClient.GetHelperTableByNodeIdReturnValue = helperTableEntities;

            var result = environment.dataAccess.LoadItems();


            var comparer = new Func<HelperTableEntityMock, HelperTableEntityMock, bool>((res, exp) => res.EqualsByContent(exp));
            CheckerFunctions.CollectionAssertAreEquivalent(result, result, comparer);
        }

        static IEnumerable<(HelperTableEntityMock, Core.Entities.User, NodeId)> AddAndRemoveData = new List<(HelperTableEntityMock, Core.Entities.User, NodeId)>()
        {
            (
                CreateHelperTableEntityMock.Parametrized(1, "Test A"),
                CreateUser.IdOnly(2),
                NodeId.ConfigurableField
            ),
            (
                CreateHelperTableEntityMock.Parametrized(99, "Cost Center A"),
                CreateUser.IdOnly(2),
                NodeId.CostCenter
            )
        };

        [TestCaseSource(nameof(AddAndRemoveData))]
        public void AddItemCallsClient((HelperTableEntityMock entity, Core.Entities.User user, NodeId nodeId) data)
        {
            var environment = new Environment();
            environment.dataAccess = CreateHelperTableDataAccessParametrized(data.nodeId,
                new HelperTableEntityMockSupportMock(), environment.mocks.clientFactory);

            environment.mocks.helperTableClient.InsertHelperTableEntityWithHistoryReturnValue = new ListOfHelperTableEntity() { HelperTableEntities = { new HelperTableEntity() } };

            environment.dataAccess.AddItem(data.entity, data.user);

            var clientParam = environment.mocks.helperTableClient.InsertHelperTableEntityWithHistoryParameter;
            var entityDiff = clientParam.HelperTableEntityDiffs.HelperTableEntityDiff.First();

            Assert.AreEqual(1, clientParam.HelperTableEntityDiffs.HelperTableEntityDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), entityDiff.UserId);
            Assert.AreEqual("", entityDiff.Comment);
            Assert.IsTrue(CompareHelperTableEntityWithHelperTableEntityDto(data.entity, entityDiff.NewHelperTableEntity));
            Assert.IsNull(entityDiff.OldHelperTableEntity);
            Assert.IsTrue(clientParam.ReturnList);
        }

        [TestCase(1)]
        [TestCase(99)]
        public void AddItemReturnsCorrectValue(long id)
        {
            var environment = new Environment();
            environment.mocks.helperTableClient.InsertHelperTableEntityWithHistoryReturnValue = new ListOfHelperTableEntity()
            {
                HelperTableEntities = { new HelperTableEntity() { ListId = id } }
            };

            var result = environment.dataAccess.AddItem(CreateHelperTableEntityMock.WithDescription(""), CreateUser.Anonymous());
            Assert.AreEqual(id, result.ToLong());
        }

        [Test]
        public void AddItemReturnsNullThrowsException()
        {
            var environment = new Environment();

            Assert.Throws<NullReferenceException>(() =>
            {
                environment.dataAccess.AddItem(CreateHelperTableEntityMock.WithId(1), CreateUser.Anonymous());
            });
        }

        [Test]
        public void SavingItemWithMismatchingIdsThrowsArgumentException()
        {
            var environment = new Environment();

            Assert.Throws<ArgumentException>(() =>
            {
                environment.dataAccess.SaveItem(CreateHelperTableEntityMock.WithId(1),
                    CreateHelperTableEntityMock.WithId(2), CreateUser.Anonymous());
            });
        }

        static IEnumerable<(HelperTableEntityMock, HelperTableEntityMock, Core.Entities.User)> SaveManufacturerData
            = new List<(HelperTableEntityMock, HelperTableEntityMock, Core.Entities.User)>()
        {
            (
                CreateHelperTableEntityMock.Parametrized(1, "Test A"),
                CreateHelperTableEntityMock.Parametrized(1, "Test ABCDE"),
                CreateUser.IdOnly(2)
            ),
            (
                CreateHelperTableEntityMock.Parametrized(1, "T56768"),
                CreateHelperTableEntityMock.Parametrized(1, "CostCenter "),
                CreateUser.IdOnly(2)
            )
        };

        [TestCaseSource(nameof(SaveManufacturerData))]
        public void SaveItemCallsClient((HelperTableEntityMock oldEntity, HelperTableEntityMock newEntity, Core.Entities.User user) data)
        {
            var environment = new Environment();

            environment.dataAccess.SaveItem(data.oldEntity, data.newEntity, data.user);

            var clientParam = environment.mocks.helperTableClient.UpdateHelperTableEntityWithHistoryParameter;
            var clientDiff = clientParam.HelperTableEntityDiffs.HelperTableEntityDiff.First();

            Assert.AreEqual(1, clientParam.HelperTableEntityDiffs.HelperTableEntityDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.IsTrue(CompareHelperTableEntityWithHelperTableEntityDto(data.newEntity, clientDiff.NewHelperTableEntity));
            Assert.IsTrue(CompareHelperTableEntityWithHelperTableEntityDto(data.oldEntity, clientDiff.OldHelperTableEntity));
            Assert.AreEqual(true, clientDiff.OldHelperTableEntity.Alive);
            Assert.AreEqual(true, clientDiff.NewHelperTableEntity.Alive);
        }

        [TestCaseSource(nameof(AddAndRemoveData))]
        public void RemoveItemCallsClient((HelperTableEntityMock entity, Core.Entities.User user, NodeId nodeId) data)
        {
            var environment = new Environment();
            environment.dataAccess = CreateHelperTableDataAccessParametrized(data.nodeId,
                new HelperTableEntityMockSupportMock(), environment.mocks.clientFactory);

            environment.mocks.helperTableClient.UpdateHelperTableEntityWithHistoryReturnValue = new ListOfHelperTableEntity() { HelperTableEntities = { new HelperTableEntity() } };

            environment.dataAccess.RemoveItem(data.entity, data.user);

            var clientParam = environment.mocks.helperTableClient.UpdateHelperTableEntityWithHistoryParameter;
            var entityDiff = clientParam.HelperTableEntityDiffs.HelperTableEntityDiff.First();

            Assert.AreEqual(1, clientParam.HelperTableEntityDiffs.HelperTableEntityDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), entityDiff.UserId);
            Assert.AreEqual("", entityDiff.Comment);
            Assert.IsTrue(CompareHelperTableEntityWithHelperTableEntityDto(data.entity, entityDiff.NewHelperTableEntity));
            Assert.IsTrue(CompareHelperTableEntityWithHelperTableEntityDto(data.entity, entityDiff.OldHelperTableEntity));
            Assert.AreEqual(true, entityDiff.OldHelperTableEntity.Alive);
            Assert.AreEqual(false, entityDiff.NewHelperTableEntity.Alive);
            Assert.AreEqual(data.nodeId, (NodeId)entityDiff.OldHelperTableEntity.NodeId);
            Assert.AreEqual(data.nodeId, (NodeId)entityDiff.NewHelperTableEntity.NodeId);
        }

        [TestCase(1)]
        [TestCase(19)]
        public void LoadToolReferenceLinksCallsReferenceLoader(long id)
        {
            var environment = new Environment();
            environment.mocks.referenceLoader.GetReferencedToolsReturnValue = new List<Core.Entities.ReferenceLink.ToolReferenceLink>();

            environment.dataAccess.LoadToolReferenceLinks(new HelperTableEntityId(id));

            Assert.AreEqual(id, environment.mocks.referenceLoader.GetReferencedToolsParameter.ToLong());
        }

        [Test]
        public void LoadToolReferenceLinksReturnsCorrectValue()
        {
            var environment = new Environment();
            var referenceData = new List<Core.Entities.ReferenceLink.ToolReferenceLink>();
            environment.mocks.referenceLoader.GetReferencedToolsReturnValue = referenceData;

            var result = environment.dataAccess.LoadToolReferenceLinks(new HelperTableEntityId(1));

            Assert.AreEqual(referenceData, result);
        }

        [TestCase(1)]
        [TestCase(19)]
        public void LoadReferencedToolModelsCallsReferenceLoader(long id)
        {
            var environment = new Environment();
            environment.mocks.referenceLoader.GetReferencedToolModelsReturnValue = new List<ToolModelReferenceLink>();

            environment.dataAccess.LoadReferencedToolModels(new HelperTableEntityId(id));

            Assert.AreEqual(id, environment.mocks.referenceLoader.GetReferencedToolModelsParameter.ToLong());
        }

        [Test]
        public void LoadReferencedToolModelsReturnsCorrectValue()
        {
            var environment = new Environment();
            var referenceData = new List<ToolModelReferenceLink>();
            environment.mocks.referenceLoader.GetReferencedToolModelsReturnValue = referenceData;

            var result = environment.dataAccess.LoadReferencedToolModels(new HelperTableEntityId(1));

            Assert.AreEqual(referenceData, result);
        }

        [Test]
        public void AddItemThrowsEntryAlreadyExistsIfEntityAlreadyExists()
        {
            var environment = new Environment();
            environment.mocks.helperTableClient.InsertHelperTableEntityWithHistoryThrow =
                new RpcException(new Grpc.Core.Status(StatusCode.AlreadyExists,""));

            Assert.Throws<EntryAlreadyExists>(() =>
            {
                environment.dataAccess.AddItem(CreateHelperTableEntityMock.WithId(1), CreateUser.Anonymous());
            });
        }

        private bool CompareHelperTableEntityWithHelperTableEntityDto(HelperTableEntityMock entity, HelperTableEntity dto)
        {
            return entity.ListId.ToLong() == dto.ListId &&
                   entity.Description.ToDefaultString() == dto.Value;
        }

        private static HelperTableDataAccess<HelperTableEntityMock> CreateHelperTableDataAccessParametrized(
            NodeId nodeId,
            IHelperTableEntitySupport<HelperTableEntityMock> entitySupport,
            IClientFactory clientFactory)
        {
            return new HelperTableDataAccess<HelperTableEntityMock>(nodeId, entitySupport, false, false, new ReferenceLoaderMock(), clientFactory);
        }

        private class HelperTableEntityMockSupportMock : IHelperTableEntitySupport<HelperTableEntityMock>
        {
            public HelperTableEntityMock CreateEntity()
            {
                return CreateHelperTableEntityMock.Anonymous();
            }

            public void MapDtoToEntity(DtoTypes.HelperTableEntity dto, HelperTableEntityMock entity, Mapper mapper)
            {
                entity.ListId = new HelperTableEntityId(dto.ListId);
                entity.Description = new HelperTableDescription(dto.Value);
            }

            public HelperTableEntity MapEntityToDto(HelperTableEntityMock entity, Mapper mapper)
            {
                return new HelperTableEntity()
                {
                    ListId = entity.ListId.ToLong(),
                    Value = entity.Description.ToDefaultString()
                };
            }
        }

        private class ReferenceLoaderMock : IHelperTableReferenceLoader
        {
            public HelperTableEntityId GetReferencedToolsParameter { get; private set; }
            public List<Core.Entities.ReferenceLink.ToolReferenceLink> GetReferencedToolsReturnValue { get; set; }
            public List<ToolModelReferenceLink> GetReferencedToolModelsReturnValue { get; set; }
            public HelperTableEntityId GetReferencedToolModelsParameter { get; set; }

            public List<ToolModelReferenceLink> GetReferencedToolModels(HelperTableEntityId id)
            {
                GetReferencedToolModelsParameter = id;
                return GetReferencedToolModelsReturnValue;
            }

            public List<Core.Entities.ReferenceLink.ToolReferenceLink> GetReferencedTools(HelperTableEntityId id)
            {
                GetReferencedToolsParameter = id;
                return GetReferencedToolsReturnValue;
            }
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    clientFactory = new ClientFactoryMock();
                    channelWrapper = new ChannelWrapperMock();
                    helperTableClient = new HelperTableClientMock();
                    channelWrapper.GetHelperTableClientReturnValue = helperTableClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                    referenceLoader = new ReferenceLoaderMock();
                }
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
                public HelperTableClientMock helperTableClient;
                public ReferenceLoaderMock referenceLoader;
            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new HelperTableDataAccess<HelperTableEntityMock>(
                    NodeId.ShutOff,
                    new HelperTableEntityMockSupportMock(),
                    true,
                    true,
                    mocks.referenceLoader,
                    mocks.clientFactory);
            }

            public readonly Mocks mocks;
            public HelperTableDataAccess<HelperTableEntityMock> dataAccess;
        }
    }
}
