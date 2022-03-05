using System;
using System.Collections.Generic;
using Common.Types.Exceptions;
using Core.Entities;
using Grpc.Core;
using HelperTableService;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.UseCases.UseCases;
using State;
using TestHelper.Checker;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    public class HelperTableUseCaseMock : IHelperTableUseCase
    {
        public NodeId GetHelperTableByNodeIdParameter { get; private set; }
        public List<HelperTableEntity> GetHelperTableByNodeIdReturnValue { get; set; } = new List<HelperTableEntity>();
        public bool GetAllHelperTableEntitiesCalled { get; private set; }
        public List<HelperTableEntity> GetAllHelperTableEntitiesReturnValue { get; set; } = new List<HelperTableEntity>();
        public long GetHelperTableEntityModelLinksIdParameter { get; set; }
        public NodeId GetHelperTableEntityModelLinksNodeParameter { get; set; }
        public List<ToolModelReferenceLink> GetHelperTableEntityModelLinksReturnValue { get; set; } = new List<ToolModelReferenceLink>();
        public long GetHelperTableEntityToolLinksIdParameter { get; set; }
        public NodeId GetHelperTableEntityToolLinksNodeIdParameter { get; set; }
        public List<ToolReferenceLink> GetHelperTableEntityToolLinksReturnValue { get; set; } = new List<ToolReferenceLink>();
        public List<HelperTableEntity> InsertHelperTableEntityWithHistoryReturnValue { get; set; } = new List<HelperTableEntity>();
        public bool InsertHelperTableEntityWithHistoryReturnListParameter { get; set; }
        public List<HelperTableEntityDiff> InsertHelperTableEntityWithHistoryDiffParameter { get; set; }
        public List<HelperTableEntity> UpdateHelperTableEntityWithHistoryReturnValue { get; set; } = new List<HelperTableEntity>();
        public List<HelperTableEntityDiff> UpdateHelperTableEntityWithHistoryParameter { get; set; }
        public EntryAlreadyExistsException InsertHelperTableEntityWithHistoryThrow { get; set; }


        public List<HelperTableEntity> GetAllHelperTableEntities()
        {
            GetAllHelperTableEntitiesCalled = true;
            return GetAllHelperTableEntitiesReturnValue;
        }

        public List<HelperTableEntity> GetHelperTableByNodeId(NodeId nodeId)
        {
            GetHelperTableByNodeIdParameter = nodeId;
            return GetHelperTableByNodeIdReturnValue;
        }

        public List<ToolModelReferenceLink> GetHelperTableEntityModelLinks(long id, NodeId nodeId)
        {
            GetHelperTableEntityModelLinksIdParameter = id;
            GetHelperTableEntityModelLinksNodeParameter = nodeId;
            return GetHelperTableEntityModelLinksReturnValue;
        }

        public List<ToolReferenceLink> GetHelperTableEntityToolLinks(long id, NodeId nodeId)
        {
            GetHelperTableEntityToolLinksIdParameter = id;
            GetHelperTableEntityToolLinksNodeIdParameter = nodeId;
            return GetHelperTableEntityToolLinksReturnValue;
        }

        public List<HelperTableEntity> InsertHelperTableEntityWithHistory(List<HelperTableEntityDiff> helperTableEntityDiffs, bool returnList)
        {
            if (InsertHelperTableEntityWithHistoryThrow != null)
            {
                throw InsertHelperTableEntityWithHistoryThrow;
            }
            InsertHelperTableEntityWithHistoryDiffParameter = helperTableEntityDiffs;
            InsertHelperTableEntityWithHistoryReturnListParameter = returnList;
            return InsertHelperTableEntityWithHistoryReturnValue;
        }

        public List<HelperTableEntity> UpdateHelperTableEntityWithHistory(List<HelperTableEntityDiff> helperTableEntityDiffs)
        {
            UpdateHelperTableEntityWithHistoryParameter = helperTableEntityDiffs;
            return UpdateHelperTableEntityWithHistoryReturnValue;
        }
    }

    public class HelperTableServiceTest
    { 
        [TestCase(1)]
        [TestCase(25)]
        public void GetHelperTableByNodeIdCallsUseCase(long nodeId)
        {
            var useCase = new HelperTableUseCaseMock();
            var service = new NetworkView.Services.HelperTableService(null, useCase);

            service.GetHelperTableByNodeId(new BasicTypes.Long() { Value = nodeId }, null);

            Assert.AreEqual(nodeId, (long) useCase.GetHelperTableByNodeIdParameter);
        }

        private static IEnumerable<List<HelperTableEntity>> helperTableData = new List<List<HelperTableEntity>>()
        {
            new List<HelperTableEntity>()
            {
               new HelperTableEntity() {ListId = new HelperTableEntityId(1), Value = new HelperTableEntityValue("56765 65757")},
               new HelperTableEntity() {ListId = new HelperTableEntityId(99), Value = new HelperTableEntityValue("Test 2")}
            },
            new List<HelperTableEntity>()
            {
               new HelperTableEntity() {ListId = new HelperTableEntityId(145), Value = new HelperTableEntityValue("Werkzeug")}
            }
        };

        [TestCaseSource(nameof(helperTableData))]
        public void GetHelperTableByNodeIdReturnsCorrectValue(List<HelperTableEntity> helperTableEntity)
        {
            var useCase = new HelperTableUseCaseMock();
            var service = new NetworkView.Services.HelperTableService(null, useCase);

            useCase.GetHelperTableByNodeIdReturnValue = helperTableEntity;

            var result = service.GetHelperTableByNodeId(new BasicTypes.Long(), null);

            var comparer = new Func<HelperTableEntity, DtoTypes.HelperTableEntity, bool>((entity, dto) =>
                EqualityChecker.CompareHelperTableEntityDtoWithHelperTableEntity(dto, entity)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(helperTableEntity, result.Result.HelperTableEntities, comparer);
        }

        [Test]
        public void GetHelperTableByNodeIdCallsUseCase()
        {
            var useCase = new HelperTableUseCaseMock();
            var service = new NetworkView.Services.HelperTableService(null, useCase);

            service.GetAllHelperTableEntities(new BasicTypes.NoParams() ,null);

            Assert.IsTrue(useCase.GetAllHelperTableEntitiesCalled);
        }

        [TestCaseSource(nameof(helperTableData))]
        public void GetAllHelperTableEntitiesReturnsCorrectValue(List<HelperTableEntity> helperTableEntiy)
        {
            var useCase = new HelperTableUseCaseMock();
            var service = new NetworkView.Services.HelperTableService(null, useCase);

            useCase.GetAllHelperTableEntitiesReturnValue = helperTableEntiy;

            var result = service.GetAllHelperTableEntities(new BasicTypes.NoParams(), null);

            var comparer = new Func<HelperTableEntity, DtoTypes.HelperTableEntity, bool>((entity, dto) =>
                EqualityChecker.CompareHelperTableEntityDtoWithHelperTableEntity(dto, entity)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(helperTableEntiy, result.Result.HelperTableEntities, comparer);
        }

        [TestCase(1, 67)]
        [TestCase(25, 456)]
        public void GetHelperTableEntityModelLinksCallsUseCase(long id, long nodeId)
        {
            var useCase = new HelperTableUseCaseMock();
            var service = new NetworkView.Services.HelperTableService(null, useCase);

            service.GetHelperTableEntityModelLinks(new HelperTableService.HelperTableEntityLinkRequest() { Id = id, NodeId = nodeId }, null);

            Assert.AreEqual(id, useCase.GetHelperTableEntityModelLinksIdParameter);
            Assert.AreEqual(nodeId, (long)useCase.GetHelperTableEntityModelLinksNodeParameter);
        }

        static IEnumerable<List<ToolModelReferenceLink>> GetHelperTableModelLinksData = new List<List<ToolModelReferenceLink>>()
        {
            new List<ToolModelReferenceLink>()
            {
                new ToolModelReferenceLink()
                {
                    Id = new QstIdentifier(99),
                    DisplayName = "Wheelmaster"
                },
                new ToolModelReferenceLink()
                {
                    Id = new QstIdentifier(1),
                    DisplayName = "blub"
                }
            },
            new List<ToolModelReferenceLink>()
            {
                new ToolModelReferenceLink()
                {
                    Id = new QstIdentifier(12),
                    DisplayName = "Gerät 9999"
                }
            }
        };

        [TestCaseSource(nameof(GetHelperTableModelLinksData))]
        public void GetHelperTableEntityModelLinksReturnsCorrectValue(List<ToolModelReferenceLink> toolModelReferenceLink)
        {
            var useCase = new HelperTableUseCaseMock();
            useCase.GetHelperTableEntityModelLinksReturnValue = toolModelReferenceLink;
            var service = new NetworkView.Services.HelperTableService(null, useCase);

            var result = service.GetHelperTableEntityModelLinks(new HelperTableService.HelperTableEntityLinkRequest(), null);

            var comparer = new Func<ToolModelReferenceLink, DtoTypes.ModelLink, bool>((modelLink, dtoModelLink) =>
                modelLink.Id.ToLong() == dtoModelLink.Id &&
                modelLink.DisplayName == dtoModelLink.Model
            );

            CheckerFunctions.CollectionAssertAreEquivalent(toolModelReferenceLink, result.Result.ModelLinks, comparer);
        }

        [TestCase(1, 67)]
        [TestCase(25, 456)]
        public void GetHelperTableEntityToolLinksCallsUseCase(long id, long nodeId)
        {
            var useCase = new HelperTableUseCaseMock();
            var service = new NetworkView.Services.HelperTableService(null, useCase);

            service.GetHelperTableEntityToolLinks(new HelperTableEntityLinkRequest() { Id = id, NodeId = nodeId }, null);

            Assert.AreEqual(id, useCase.GetHelperTableEntityToolLinksIdParameter);
            Assert.AreEqual(nodeId, (long)useCase.GetHelperTableEntityToolLinksNodeIdParameter);
        }

        static IEnumerable<List<ToolReferenceLink>> GetHelperTableEntityToolLinksData = new List<List<ToolReferenceLink>>()
        {
            new List<ToolReferenceLink>()
            {
                new ToolReferenceLink(new QstIdentifier(99), "324345", "34546 568679"),
                new ToolReferenceLink(new QstIdentifier(65), "asad", "ewr 4565")
            },
            new List<ToolReferenceLink>()
            {
                new ToolReferenceLink(new QstIdentifier(199), "testB", "testA")
            }
        };

        [TestCaseSource(nameof(GetHelperTableEntityToolLinksData))]
        public void GetHelperTableEntityToolLinksReturnsCorrectValue(List<ToolReferenceLink> toolReferenceLink)
        {
            var useCase = new HelperTableUseCaseMock();
            useCase.GetHelperTableEntityToolLinksReturnValue = toolReferenceLink;
            var service = new NetworkView.Services.HelperTableService(null, useCase);

            var result = service.GetHelperTableEntityToolLinks(new HelperTableService.HelperTableEntityLinkRequest(), null);

            var comparer = new Func<ToolReferenceLink, DtoTypes.ToolReferenceLink, bool>((link, dtoLink) =>
                link.Id.ToLong() == dtoLink.Id &&
                link.InventoryNumber == dtoLink.InventoryNumber &&
                link.SerialNumber == dtoLink.SerialNumber
            );

            CheckerFunctions.CollectionAssertAreEquivalent(toolReferenceLink, result.Result.ToolReferenceLinks, comparer);
        }


        static IEnumerable<(ListOfHelperTableEntityDiff, bool)> InsertUpdateHelperTableEntityWithHistoryData = new List<(ListOfHelperTableEntityDiff, bool)>
        {
            (
                new ListOfHelperTableEntityDiff()
                {
                    HelperTableEntityDiff =
                    {
                        new DtoTypes.HelperTableEntityDiff()
                        {
                            UserId = 1,
                            Comment = "",
                            OldHelperTableEntity = new DtoTypes.HelperTableEntity() {ListId = 1, NodeId = (long)NodeId.ConfigurableField, Value = "Field1", Alive = true},
                            NewHelperTableEntity = new DtoTypes.HelperTableEntity() {ListId = 16, NodeId = (long)NodeId.CostCenter, Value = "Cost Center 1", Alive = false}
                        },
                        new DtoTypes.HelperTableEntityDiff()
                        {
                            UserId = 14,
                            Comment = "",
                            OldHelperTableEntity = new DtoTypes.HelperTableEntity() {ListId = 156, NodeId = (long)NodeId.DriveSize, Value = "DriveSize1", Alive = true},
                            NewHelperTableEntity = new DtoTypes.HelperTableEntity() {ListId = 1646, NodeId = (long)NodeId.Location, Value = "Location1", Alive = false}
                        }
                    }
                },
                true
             ),
            (
                new ListOfHelperTableEntityDiff()
                {
                    HelperTableEntityDiff =
                    {
                        new DtoTypes.HelperTableEntityDiff()
                        {
                            UserId = 91,
                            Comment = "",
                            OldHelperTableEntity = new DtoTypes.HelperTableEntity() {ListId = 31, NodeId = (long)NodeId.ConfigurableField, Value = "Field ABCD", Alive = true},
                            NewHelperTableEntity = new DtoTypes.HelperTableEntity() {ListId = 16, NodeId = (long)NodeId.SwitchOff, Value = "Switch Off 1", Alive = false}
                        }
                    }
                },
                false
            )
        };

        [TestCaseSource(nameof(InsertUpdateHelperTableEntityWithHistoryData))]
        public void InsertHelperTableEntityWithHistoryCallsUseCase((ListOfHelperTableEntityDiff diff, bool returnList) data)
        {
            var useCase = new HelperTableUseCaseMock();
            var service = new NetworkView.Services.HelperTableService(null, useCase);

            var request = new InsertHelperTableEntityWithHistoryRequest()
            {
                HelperTableEntityDiffs = data.diff,
                ReturnList = data.returnList
            };

            service.InsertHelperTableEntityWithHistory(request, null);

            Assert.AreEqual(data.returnList, useCase.InsertHelperTableEntityWithHistoryReturnListParameter);

            var comparer = new Func<DtoTypes.HelperTableEntityDiff, HelperTableEntityDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareHelperTableEntityDtoWithHelperTableEntity(dtoDiff.OldHelperTableEntity, diff.GetOldHelperTableEntity()) &&
                EqualityChecker.CompareHelperTableEntityDtoWithHelperTableEntity(dtoDiff.NewHelperTableEntity, diff.GetNewHelperTableEntity())
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.diff.HelperTableEntityDiff, useCase.InsertHelperTableEntityWithHistoryDiffParameter, comparer);
        }

        [TestCaseSource(nameof(helperTableData))]
        public void InsertHelperTableEntityWithHistoryReturnsCorrectValue(List<HelperTableEntity> entities)
        {
            var useCase = new HelperTableUseCaseMock();
            var service = new NetworkView.Services.HelperTableService(null, useCase);

            useCase.InsertHelperTableEntityWithHistoryReturnValue = entities;

            var request = new InsertHelperTableEntityWithHistoryRequest()
            {
                HelperTableEntityDiffs = new ListOfHelperTableEntityDiff()
            };

            var result = service.InsertHelperTableEntityWithHistory(request, null).Result;

            var comparer = new Func<HelperTableEntity, DtoTypes.HelperTableEntity, bool>((entity, dto) =>
                EqualityChecker.CompareHelperTableEntityDtoWithHelperTableEntity(dto, entity)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(entities, result.HelperTableEntities, comparer);
        }

        [Test]
        public void InsertHelperTableEntityWithHistoryThrowsRpcExceptionAlreadyExists()
        {
            var useCase = new HelperTableUseCaseMock();
            var service = new NetworkView.Services.HelperTableService(null, useCase);
            useCase.InsertHelperTableEntityWithHistoryReturnValue = new List<HelperTableEntity>();
            useCase.InsertHelperTableEntityWithHistoryThrow = new EntryAlreadyExistsException("");

            try
            {
                service.InsertHelperTableEntityWithHistory(
                    new InsertHelperTableEntityWithHistoryRequest()
                        {HelperTableEntityDiffs = new ListOfHelperTableEntityDiff()}, null);
            }
            catch (RpcException e)
            {
                Assert.AreEqual(StatusCode.AlreadyExists, e.Status.StatusCode);
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCaseSource(nameof(InsertUpdateHelperTableEntityWithHistoryData))]
        public void UpdateHelperTableEntityWithHistoryCallsUseCase((ListOfHelperTableEntityDiff diffs, bool returnList) data)
        {
            var useCase = new HelperTableUseCaseMock();
            var service = new NetworkView.Services.HelperTableService(null, useCase);

            var request = new UpdateHelperTableEntityWithHistoryRequest()
            {
                HelperTableEntityDiffs = data.diffs
            };

            service.UpdateHelperTableEntityWithHistory(request, null);

            var comparer = new Func<DtoTypes.HelperTableEntityDiff, HelperTableEntityDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareHelperTableEntityDtoWithHelperTableEntity(dtoDiff.OldHelperTableEntity, diff.GetOldHelperTableEntity()) &&
                EqualityChecker.CompareHelperTableEntityDtoWithHelperTableEntity(dtoDiff.NewHelperTableEntity, diff.GetNewHelperTableEntity())
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.diffs.HelperTableEntityDiff, useCase.UpdateHelperTableEntityWithHistoryParameter, comparer);
        }

        [TestCaseSource(nameof(helperTableData))]
        public void UpdateHelperTableEntityWithHistoryReturnValueReturnsCorrectValue(List<HelperTableEntity> entities)
        {
            var useCase = new HelperTableUseCaseMock();
            var service = new NetworkView.Services.HelperTableService(null, useCase);

            useCase.UpdateHelperTableEntityWithHistoryReturnValue = entities;

            var request = new UpdateHelperTableEntityWithHistoryRequest()
            {
                HelperTableEntityDiffs = new ListOfHelperTableEntityDiff()
            };

            var result = service.UpdateHelperTableEntityWithHistory(request, null).Result;

            var comparer = new Func<HelperTableEntity, DtoTypes.HelperTableEntity, bool>((entity, dto) =>
                EqualityChecker.CompareHelperTableEntityDtoWithHelperTableEntity(dto, entity)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(entities, result.HelperTableEntities, comparer);
        }
    }
}
