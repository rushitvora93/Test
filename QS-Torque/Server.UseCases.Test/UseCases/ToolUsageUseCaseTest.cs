using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    public class ToolUsageDataAccessMock : IToolUsageDataAccess
    {
        public enum ToolUsageDataAccessFunction
        {
            InsertToolUsagesWithHistory,
            UpdateToolUsagesWithHistory,
            Commit
        }

        public List<ToolUsage> GetAllToolUsagesReturnValue { get; set; }
        public bool GetAllToolUsagesCalled { get; set; }
        public List<ToolUsage> InsertToolUsagesWithHistoryReturnValue { get; set; }
        public bool InsertToolUsagesWithHistoryReturnListParameter { get; set; }
        public List<ToolUsageDiff> InsertToolUsagesWithHistoryDiffParameter { get; set; }
        public List<ToolUsage> UpdateToolUsagesWithHistoryReturnValue { get; set; }
        public List<ToolUsageDiff> UpdateToolUsagesWithHistoryParameter { get; set; }
        public List<long> GetToolUsageLocationToolAssignmentReferencesReturnValue { get; set; }
        public ToolUsageId GetToolUsageLocationToolAssignmentReferencesParameter { get; set; }
        public List<ToolUsageDataAccessFunction> CalledFunctions { get; set; } = new List<ToolUsageDataAccessFunction>();


        public void Commit()
        {
            CalledFunctions.Add(ToolUsageDataAccessFunction.Commit);
        }

        public List<ToolUsage> GetAllToolUsages()
        {
            GetAllToolUsagesCalled = true;
            return GetAllToolUsagesReturnValue;
        }

        public List<ToolUsage> InsertToolUsagesWithHistory(List<ToolUsageDiff> toolUsagesDiff, bool returnList)
        {
            CalledFunctions.Add(ToolUsageDataAccessFunction.InsertToolUsagesWithHistory);
            InsertToolUsagesWithHistoryDiffParameter = toolUsagesDiff;
            InsertToolUsagesWithHistoryReturnListParameter = returnList;
            return InsertToolUsagesWithHistoryReturnValue;
        }

        public List<ToolUsage> UpdateToolUsagesWithHistory(List<ToolUsageDiff> toolUsagesDiff)
        {
            CalledFunctions.Add(ToolUsageDataAccessFunction.UpdateToolUsagesWithHistory);
            UpdateToolUsagesWithHistoryParameter = toolUsagesDiff;
            return UpdateToolUsagesWithHistoryReturnValue;
        }

        public List<long> GetToolUsageLocationToolAssignmentReferences(ToolUsageId id)
        {
            GetToolUsageLocationToolAssignmentReferencesParameter = id;
            return GetToolUsageLocationToolAssignmentReferencesReturnValue;
        }
    }

    public class ToolUsageUseCaseTest
    {
        [Test]
        public void GetAllToolUsagesCallsDataAccess()
        {
            var dataAccess = new ToolUsageDataAccessMock();
            var useCase = new ToolUsageUseCase(dataAccess);

            useCase.GetAllToolUsages();

            Assert.IsTrue(dataAccess.GetAllToolUsagesCalled);
        }

        [Test]
        public void GetAllToolUsagesReturnsCorrectValue()
        {
            var dataAccess = new ToolUsageDataAccessMock();
            var useCase = new ToolUsageUseCase(dataAccess);

            var entities = new List<ToolUsage>();
            dataAccess.GetAllToolUsagesReturnValue = entities;

            Assert.AreSame(entities, useCase.GetAllToolUsages());
        }

        [TestCase(1)]
        [TestCase(23)]
        public void GetToolUsageLocationToolAssignmentReferencesCallsDataAccess(long id)
        {
            var dataAccess = new ToolUsageDataAccessMock();
            var useCase = new ToolUsageUseCase(dataAccess);

            useCase.GetToolUsageLocationToolAssignmentReferences(new ToolUsageId(id));

            Assert.AreEqual(id, dataAccess.GetToolUsageLocationToolAssignmentReferencesParameter.ToLong());
        }

        [Test]
        public void GetToolUsageLocationToolAssignmentReferencesReturnsCorrectValue()
        {
            var dataAccess = new ToolUsageDataAccessMock();
            var useCase = new ToolUsageUseCase(dataAccess);

            var longs = new List<long>();
            dataAccess.GetToolUsageLocationToolAssignmentReferencesReturnValue = longs;

            Assert.AreSame(longs, useCase.GetToolUsageLocationToolAssignmentReferences(new ToolUsageId(1)));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InsertToolUsagesWithHistoryCallsDataAccess(bool returnList)
        {
            var dataAccess = new ToolUsageDataAccessMock();
            var useCase = new ToolUsageUseCase(dataAccess);

            var diffs = new List<ToolUsageDiff>();
            useCase.InsertToolUsagesWithHistory(diffs, returnList);

            Assert.AreSame(diffs, dataAccess.InsertToolUsagesWithHistoryDiffParameter);
            Assert.AreEqual(returnList, dataAccess.InsertToolUsagesWithHistoryReturnListParameter);
        }

        [Test]
        public void InsertToolUsagesWithHistoryCallsCommitAfterWork()
        {
            var dataAccess = new ToolUsageDataAccessMock();
            var useCase = new ToolUsageUseCase(dataAccess);

            useCase.InsertToolUsagesWithHistory(new List<ToolUsageDiff>(), false);

            Assert.AreEqual(ToolUsageDataAccessMock.ToolUsageDataAccessFunction.Commit, dataAccess.CalledFunctions.Last());
        }

        [Test]
        public void InsertToolUsagesWithHistoryReturnsCorrectValue()
        {
            var dataAccess = new ToolUsageDataAccessMock();
            var useCase = new ToolUsageUseCase(dataAccess);

            var entities = new List<ToolUsage>();
            dataAccess.InsertToolUsagesWithHistoryReturnValue = entities;

            var returnValue = useCase.InsertToolUsagesWithHistory(null, true);

            Assert.AreSame(entities, returnValue);
        }

        [Test]
        public void UpdateToolUsagesWithHistoryCallsDataAccess()
        {
            var dataAccess = new ToolUsageDataAccessMock();
            var useCase = new ToolUsageUseCase(dataAccess);

            var diffs = new List<ToolUsageDiff>();
            useCase.UpdateToolUsagesWithHistory(diffs);

            Assert.AreSame(diffs, dataAccess.UpdateToolUsagesWithHistoryParameter);
        }

        [Test]
        public void UpdateToolUsagesWithHistoryCallsCommitAfterWork()
        {
            var dataAccess = new ToolUsageDataAccessMock();
            var useCase = new ToolUsageUseCase(dataAccess);

            useCase.UpdateToolUsagesWithHistory(new List<ToolUsageDiff>());

            Assert.AreEqual(ToolUsageDataAccessMock.ToolUsageDataAccessFunction.Commit, dataAccess.CalledFunctions.Last());
        }

        [Test]
        public void UpdateToolUsagesWithHistoryReturnsCorrectValue()
        {
            var dataAccess = new ToolUsageDataAccessMock();
            var useCase = new ToolUsageUseCase(dataAccess);

            var entities = new List<ToolUsage>();
            dataAccess.UpdateToolUsagesWithHistoryReturnValue = entities;

            var returnValue = useCase.UpdateToolUsagesWithHistory(null);

            Assert.AreSame(entities, returnValue);
        }
    }
}
