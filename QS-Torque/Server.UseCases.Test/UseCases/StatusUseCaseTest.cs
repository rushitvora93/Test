using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    public class StatusDataAccessMock : IStatusDataAccess
    {
        public enum StatusDataAccessFunction
        {
            InsertStatusWithHistory,
            UpdateStatusWithHistory,
            Commit
        }

        public void Commit()
        {
            CalledFunctions.Add(StatusDataAccessFunction.Commit);
        }

        public List<Status> LoadStatus()
        {
            LoadStatusCalled = true;
            return LoadStatusReturnValue;
        }

        public List<ToolReferenceLink> GetStatusToolLinks(StatusId statusId)
        {
            GetStatusToolLinksParameter = statusId;
            return GetStatusToolLinksReturnValue;
        }

        public List<Status> InsertStatusWithHistory(List<StatusDiff> statusDiffs, bool returnList)
        {
            CalledFunctions.Add(StatusDataAccessFunction.InsertStatusWithHistory);
            InsertStatusWithHistoryParameterStatusDiffs = statusDiffs;
            InsertStatusWithHistoryParameterReturnList = returnList;
            return InsertStatusWithHistoryReturnValue;
        }

        public List<Status> UpdateStatusWithHistory(List<StatusDiff> statusDiffs)
        {
            CalledFunctions.Add(StatusDataAccessFunction.UpdateStatusWithHistory);
            UpdateStatusWithHistoryParameterStatusDiffs = statusDiffs;
            return UpdateStatusWithHistoryReturnValue;
        }

        public bool LoadStatusCalled { get; set; }
        public List<Status> LoadStatusReturnValue { get; set; } = new List<Status>();
        public List<Status> InsertStatusWithHistoryReturnValue { get; set; }
        public bool InsertStatusWithHistoryParameterReturnList { get; set; }
        public List<StatusDiff> InsertStatusWithHistoryParameterStatusDiffs { get; set; }
        public List<Status> UpdateStatusWithHistoryReturnValue { get; set; }
        public List<StatusDiff> UpdateStatusWithHistoryParameterStatusDiffs { get; set; }
        public StatusId GetStatusToolLinksParameter { get; set; }
        public List<ToolReferenceLink> GetStatusToolLinksReturnValue { get; set; }
        public List<StatusDataAccessFunction> CalledFunctions { get; set; } = new List<StatusDataAccessFunction>();
    }

    public class StatusUseCaseTest
    {
        [Test]
        public void LoadStatusCallsDataAccess()
        {
            var dataAccess = new StatusDataAccessMock();
            var useCase = new StatusUseCase(dataAccess);

            useCase.LoadStatus();

            Assert.IsTrue(dataAccess.LoadStatusCalled);
        }

        [Test]
        public void LoadStatusReturnsCorrectValue()
        {
            var dataAccess = new StatusDataAccessMock();
            var useCase = new StatusUseCase(dataAccess);
            var status = new List<Status>();
            dataAccess.LoadStatusReturnValue = status;

            Assert.AreSame(status, useCase.LoadStatus());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InsertStatusWithHistoryCallsDataAccess(bool returnList)
        {
            var dataAccess = new StatusDataAccessMock();
            var useCase = new StatusUseCase(dataAccess);

            var diffs = new List<StatusDiff>();
            useCase.InsertStatusWithHistory(diffs, returnList);

            Assert.AreSame(diffs, dataAccess.InsertStatusWithHistoryParameterStatusDiffs);
            Assert.AreEqual(returnList, dataAccess.InsertStatusWithHistoryParameterReturnList);
        }

        [Test]
        public void InsertStatusWithHistoryCallsCommitAfterWork()
        {
            var dataAccess = new StatusDataAccessMock();
            var useCase = new StatusUseCase(dataAccess);

            useCase.InsertStatusWithHistory(new List<StatusDiff>(), false);

            Assert.AreEqual(StatusDataAccessMock.StatusDataAccessFunction.Commit, dataAccess.CalledFunctions.Last());
        }

        [Test]
        public void InsertStatusWithHistoryReturnsCorrectValue()
        {
            var dataAccess = new StatusDataAccessMock();
            var useCase = new StatusUseCase(dataAccess);

            var status = new List<Status>();
            dataAccess.InsertStatusWithHistoryReturnValue = status;

            var returnValue = useCase.InsertStatusWithHistory(null, true);

            Assert.AreSame(status, returnValue);
        }

        [Test]
        public void UpdateStatusWithHistoryCallsDataAccess()
        {
            var dataAccess = new StatusDataAccessMock();
            var useCase = new StatusUseCase(dataAccess);

            var diffs = new List<StatusDiff>();
            useCase.UpdateStatusWithHistory(diffs);

            Assert.AreSame(diffs, dataAccess.UpdateStatusWithHistoryParameterStatusDiffs);
        }

        [Test]
        public void UpdateStatusWithHistoryCallsCommitAfterWork()
        {
            var dataAccess = new StatusDataAccessMock();
            var useCase = new StatusUseCase(dataAccess);

            useCase.UpdateStatusWithHistory(new List<StatusDiff>());

            Assert.AreEqual(StatusDataAccessMock.StatusDataAccessFunction.Commit, dataAccess.CalledFunctions.Last());
        }

        [Test]
        public void UpdateStatusWithHistoryReturnsCorrectValue()
        {
            var dataAccess = new StatusDataAccessMock();
            var useCase = new StatusUseCase(dataAccess);

            var status = new List<Status>();
            dataAccess.UpdateStatusWithHistoryReturnValue = status;

            var returnValue = useCase.UpdateStatusWithHistory(null);

            Assert.AreSame(status, returnValue);
        }

        [TestCase(10)]
        [TestCase(20)]
        public void GetStatusToolLinksCallsDataAccess(long manufacturerId)
        {
            var dataAccess = new StatusDataAccessMock();
            var useCase = new StatusUseCase(dataAccess);

            useCase.GetStatusToolLinks(new StatusId(manufacturerId));

            Assert.AreEqual(manufacturerId, dataAccess.GetStatusToolLinksParameter.ToLong());
        }

        [Test]
        public void GetStatusToolLinksReturnsCorrectValue()
        {
            var dataAccess = new StatusDataAccessMock();
            var useCase = new StatusUseCase(dataAccess);

            var toolLinks = new List<ToolReferenceLink>();
            dataAccess.GetStatusToolLinksReturnValue = toolLinks;

            var result = useCase.GetStatusToolLinks(new StatusId(1));

            Assert.AreSame(toolLinks, result);
        }
    }
}
