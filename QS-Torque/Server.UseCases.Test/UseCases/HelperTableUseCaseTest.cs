using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.UseCases.UseCases;
using State;
using System.Collections.Generic;
using System.Linq;
using HelperTableDataAccessFunction = Server.UseCases.Test.UseCases.HelperTableDataAccessMock.HelperTableDataAccessFunction;

namespace Server.UseCases.Test.UseCases
{
    public class HelperTableDataAccessMock : IHelperTableDataAccess
    {
        public enum HelperTableDataAccessFunction
        {
            InsertHelperTableEntityWithHistory,
            UpdateHelperTableEntityWithHistory,
            Commit
        }

        public bool GetAllHelperTableEntitiesCalled { get; private set; }
        public List<HelperTableEntity> GetAllHelperTableEntitiesReturnValue { get; set; }
        public NodeId GetHelperTableByNodeIdParameter { get; private set; }
        public List<HelperTableEntity> GetHelperTableByNodeIdReturnValue { get; set; }
        public long GetHelperTableEntityModelLinksIdParameter { get; private set; }
        public NodeId GetHelperTableEntityModelLinksNodeIdParameter { get; private set; }
        public List<ToolModelReferenceLink> GetHelperTableEntityModelLinksReturnValue { get; set; }
        public long GetHelperTableEntityToolLinksIdParameter { get; private set; }
        public NodeId GetHelperTableEntityToolLinksNodeIdParameter { get; private set; }
        public List<ToolReferenceLink> GetHelperTableEntityToolLinksReturnValue { get; set; }
        public List<HelperTableEntityDiff> InsertHelperTableEntityWithHistoryDiffParameter { get; private set; }
        public bool InsertHelperTableEntityWithHistoryReturnListParameter { get; private set; }
        public List<HelperTableEntity> InsertHelperTableEntityWithHistoryReturnValue { get; set; }
        public List<HelperTableEntityDiff> UpdateHelperTableEntityWithHistoryDiffParameter { get; private set; }
        public List<HelperTableEntity> UpdateHelperTableEntityWithHistoryReturnValue { get; set; }
        public List<HelperTableDataAccessFunction> CalledFunctions { get; set; } = new List<HelperTableDataAccessFunction>();

        public void Commit()
        {
            CalledFunctions.Add(HelperTableDataAccessFunction.Commit);
        }

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
            GetHelperTableEntityModelLinksNodeIdParameter = nodeId;
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
            CalledFunctions.Add(HelperTableDataAccessFunction.InsertHelperTableEntityWithHistory); 
            InsertHelperTableEntityWithHistoryDiffParameter = helperTableEntityDiffs;
            InsertHelperTableEntityWithHistoryReturnListParameter = returnList;
            return InsertHelperTableEntityWithHistoryReturnValue;
        }

        public List<HelperTableEntity> UpdateHelperTableEntityWithHistory(List<HelperTableEntityDiff> helperTableEntityDiffs)
        {
            CalledFunctions.Add(HelperTableDataAccessFunction.UpdateHelperTableEntityWithHistory);
            UpdateHelperTableEntityWithHistoryDiffParameter = helperTableEntityDiffs;
            return UpdateHelperTableEntityWithHistoryReturnValue;
        }
    }

    public class HelperTableUseCaseTest
    {
        [Test]
        public void GetAllHelperTableEntitiesCallsDataAccess()
        {
            var dataAccess = new HelperTableDataAccessMock();
            var useCase = new HelperTableUseCase(dataAccess);

            useCase.GetAllHelperTableEntities();

            Assert.IsTrue(dataAccess.GetAllHelperTableEntitiesCalled);
        }

        [Test]
        public void GetAllHelperTableEntitiesReturnsCorrectValue()
        {
            var dataAccess = new HelperTableDataAccessMock();
            var useCase = new HelperTableUseCase(dataAccess);

            var entities = new List<HelperTableEntity>();
            dataAccess.GetAllHelperTableEntitiesReturnValue = entities;

            Assert.AreSame(entities, useCase.GetAllHelperTableEntities());
        }

        [TestCase(NodeId.ConfigurableField)]
        [TestCase(NodeId.ConstructionType)]
        public void GetHelperTableByNodeIdCallsDataAccess(NodeId nodeId)
        {
            var dataAccess = new HelperTableDataAccessMock();
            var useCase = new HelperTableUseCase(dataAccess);

            useCase.GetHelperTableByNodeId(nodeId);

            Assert.AreEqual(nodeId, dataAccess.GetHelperTableByNodeIdParameter);
        }

        [Test]
        public void GetHelperTableByNodeIdReturnsCorrectValue()
        {
            var dataAccess = new HelperTableDataAccessMock();
            var useCase = new HelperTableUseCase(dataAccess);

            var entities = new List<HelperTableEntity>();
            dataAccess.GetHelperTableByNodeIdReturnValue = entities;

            Assert.AreSame(entities, useCase.GetHelperTableByNodeId(NodeId.ConfigurableField));
        }

        [TestCase(1, NodeId.ConfigurableField)]
        [TestCase(123, NodeId.ConstructionType)]
        public void GetHelperTableEntityModelLinksCallsDataAccess(long id, NodeId nodeId)
        {
            var dataAccess = new HelperTableDataAccessMock();
            var useCase = new HelperTableUseCase(dataAccess);

            useCase.GetHelperTableEntityModelLinks(id, nodeId);

            Assert.AreEqual(id, dataAccess.GetHelperTableEntityModelLinksIdParameter);
            Assert.AreEqual(nodeId, dataAccess.GetHelperTableEntityModelLinksNodeIdParameter);
        }

        [Test]
        public void GetHelperTableEntityModelLinksReturnsCorrectValue()
        {
            var dataAccess = new HelperTableDataAccessMock();
            var useCase = new HelperTableUseCase(dataAccess);

            var entities = new List<ToolModelReferenceLink>();
            dataAccess.GetHelperTableEntityModelLinksReturnValue = entities;

            Assert.AreSame(entities, useCase.GetHelperTableEntityModelLinks(1, NodeId.ConfigurableField));
        }

        [TestCase(1, NodeId.ConfigurableField)]
        [TestCase(123, NodeId.ConstructionType)]
        public void GetHelperTableEntityToolLinksCallsDataAccess(long id, NodeId nodeId)
        {
            var dataAccess = new HelperTableDataAccessMock();
            var useCase = new HelperTableUseCase(dataAccess);

            useCase.GetHelperTableEntityToolLinks(id, nodeId);

            Assert.AreEqual(id, dataAccess.GetHelperTableEntityToolLinksIdParameter);
            Assert.AreEqual(nodeId, dataAccess.GetHelperTableEntityToolLinksNodeIdParameter);
        }

        [Test]
        public void GetHelperTableEntityToolLinksReturnsCorrectValue()
        {
            var dataAccess = new HelperTableDataAccessMock();
            var useCase = new HelperTableUseCase(dataAccess);

            var entities = new List<ToolReferenceLink>();
            dataAccess.GetHelperTableEntityToolLinksReturnValue = entities;

            Assert.AreSame(entities, useCase.GetHelperTableEntityToolLinks(1, NodeId.ConfigurableField));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InsertHelperTableEntityWithHistoryCallsDataAccess(bool returnList)
        {
            var dataAccess = new HelperTableDataAccessMock();
            var useCase = new HelperTableUseCase(dataAccess);

            var diffs = new List<HelperTableEntityDiff>();
            useCase.InsertHelperTableEntityWithHistory(diffs, returnList);

            Assert.AreSame(diffs, dataAccess.InsertHelperTableEntityWithHistoryDiffParameter);
            Assert.AreEqual(returnList, dataAccess.InsertHelperTableEntityWithHistoryReturnListParameter);
        }

        [Test]
        public void InsertHelperTableEntityWithHistoryCallsCommitAfterWork()
        {
            var dataAccess = new HelperTableDataAccessMock();
            var useCase = new HelperTableUseCase(dataAccess);

            useCase.InsertHelperTableEntityWithHistory(new List<HelperTableEntityDiff>(), false);

            Assert.AreEqual(HelperTableDataAccessFunction.Commit, dataAccess.CalledFunctions.Last());
        }

        [Test]
        public void InsertHelperTableEntityWithHistoryReturnsCorrectValue()
        {
            var dataAccess = new HelperTableDataAccessMock();
            var useCase = new HelperTableUseCase(dataAccess);

            var entities = new List<HelperTableEntity>();
            dataAccess.InsertHelperTableEntityWithHistoryReturnValue = entities;

            var returnValue = useCase.InsertHelperTableEntityWithHistory(null, true);

            Assert.AreSame(entities, returnValue);
        }

        [Test]
        public void UpdateHelperTableEntityWithHistoryCallsDataAccess()
        {
            var dataAccess = new HelperTableDataAccessMock();
            var useCase = new HelperTableUseCase(dataAccess);

            var diffs = new List<HelperTableEntityDiff>();
            useCase.UpdateHelperTableEntityWithHistory(diffs);

            Assert.AreSame(diffs, dataAccess.UpdateHelperTableEntityWithHistoryDiffParameter);
        }

        [Test]
        public void UpdateHelperTableEntityWithHistoryCallsCommitAfterWork()
        {
            var dataAccess = new HelperTableDataAccessMock();
            var useCase = new HelperTableUseCase(dataAccess);

            useCase.UpdateHelperTableEntityWithHistory(new List<HelperTableEntityDiff>());

            Assert.AreEqual(HelperTableDataAccessFunction.Commit, dataAccess.CalledFunctions.Last());
        }

        [Test]
        public void UpdateHelperTableEntityWithHistoryReturnsCorrectValue()
        {
            var dataAccess = new HelperTableDataAccessMock();
            var useCase = new HelperTableUseCase(dataAccess);

            var entities = new List<HelperTableEntity>();
            dataAccess.UpdateHelperTableEntityWithHistoryReturnValue = entities;

            var returnValue = useCase.UpdateHelperTableEntityWithHistory(null);

            Assert.AreSame(entities, returnValue);
        }
    }
}
