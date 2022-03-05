using System;
using System.Collections.Generic;
using BasicTypes;
using Core.Entities;
using NUnit.Framework;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using TestHelper.Checker;
using StatusDiff = Server.Core.Diffs.StatusDiff;
using ToolReferenceLink = Server.Core.Entities.ReferenceLink.ToolReferenceLink;
using Status = Server.Core.Entities.Status;
using StatusService;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    public class StatusUseCaseMock : IStatusUseCase
    {
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
            InsertStatusWithHistoryParameterStatusDiffs = statusDiffs;
            InsertStatusWithHistoryParameterReturnList = returnList;
            return InsertStatusWithHistoryReturnValue;
        }

        public List<Status> UpdateStatusWithHistory(List<StatusDiff> statusDiffs)
        {
            UpdateStatusWithHistoryParameter = statusDiffs;
            return UpdateStatusWithHistoryReturnValue;
        }

        public List<Status> UpdateStatusWithHistoryReturnValue { get; set; } = new List<Status>();
        public List<StatusDiff> UpdateStatusWithHistoryParameter { get; set; }
        public List<Status> InsertStatusWithHistoryReturnValue { get; set; } = new List<Status>();
        public bool InsertStatusWithHistoryParameterReturnList { get; set; }
        public List<StatusDiff> InsertStatusWithHistoryParameterStatusDiffs { get; set; }
        public StatusId GetStatusToolLinksParameter { get; set; }
        public List<ToolReferenceLink> GetStatusToolLinksReturnValue { get; set; } = new List<ToolReferenceLink>();
        public List<Status> LoadStatusReturnValue { get; set; } = new List<Status>();
        public bool LoadStatusCalled { get; set; }
    }

    public class StatusServiceTest
    {
        [Test]
        public void LoadStatusCallsUseCase()
        {
            var useCase = new StatusUseCaseMock();
            var service = new NetworkView.Services.StatusService(null, useCase);

            service.LoadStatus(new NoParams(), null);

            Assert.IsTrue(useCase.LoadStatusCalled);
        }

        private static IEnumerable<List<Status>> statusData = new List<List<Status>>()
        {
            new List<Status>()
            {
                new Status() {Id = new StatusId(1), Value = new StatusDescription("status 1")},
                new Status() {Id = new StatusId(99), Value = new StatusDescription("state 99")}

            },
            new List<Status>()
            {
                new Status() {Id = new StatusId(12), Value = new StatusDescription("status ok")},
            }
        };

        [TestCaseSource(nameof(statusData))]
        public void LoadStatusReturnsCorrectValue(List<Status> statusList)
        {
            var useCase = new StatusUseCaseMock();
            useCase.LoadStatusReturnValue = statusList;
            var service = new NetworkView.Services.StatusService(null, useCase);

            var result = service.LoadStatus(new NoParams(), null);

            var comparer = new Func<Status, DtoTypes.Status, bool>((status, dtoStatus) =>
                EqualityChecker.CompareStatusDtoWithStatus(dtoStatus, status)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(statusList, result.Result.Status, comparer);
        }

        [TestCase(1)]
        [TestCase(99)]
        public void GetStatusToolLinksCallsUseCase(long statusId)
        {
            var useCase = new StatusUseCaseMock();
            var service = new NetworkView.Services.StatusService(null, useCase);

            service.GetStatusToolLinks(new LongRequest(){Value = statusId}, null);

            Assert.AreEqual(statusId, useCase.GetStatusToolLinksParameter.ToLong());
        }

        private static IEnumerable<List<ToolReferenceLink>> toolLinkData = new List<List<ToolReferenceLink>>()
        {
            new List<ToolReferenceLink>()
            {
                new ToolReferenceLink(new QstIdentifier(1), "21435", "435456" ),
                new ToolReferenceLink(new QstIdentifier(99), "99765", "11111" )
            },
            new List<ToolReferenceLink>()
            {
                new ToolReferenceLink(new QstIdentifier(66), "666", "44444" ),
            }
        };

        [TestCaseSource(nameof(toolLinkData))]
        public void GetStatusToolLinksReturnsCorrectValue(List<ToolReferenceLink> toolReferenceLink)
        {
            var useCase = new StatusUseCaseMock();
            useCase.GetStatusToolLinksReturnValue = toolReferenceLink;
            var service = new NetworkView.Services.StatusService(null, useCase);

            var result = service.GetStatusToolLinks(new LongRequest(), null);

            var comparer = new Func<ToolReferenceLink, DtoTypes.ToolReferenceLink, bool>((toolLink, dtoToolLink) =>
                toolLink.Id.ToLong() == dtoToolLink.Id &&
                toolLink.SerialNumber == dtoToolLink.SerialNumber &&
                toolLink.InventoryNumber == dtoToolLink.InventoryNumber
            );

            CheckerFunctions.CollectionAssertAreEquivalent(toolReferenceLink, result.Result.ToolReferenceLinks, comparer);
        }

        static IEnumerable<(ListOfStatusDiffs, bool)> InsertUpdateStatusWithHistoryData = new List<(ListOfStatusDiffs, bool)>
        {
            (
                new ListOfStatusDiffs()
                {
                    StatusDiff =
                    {
                        new DtoTypes.StatusDiff()
                        {
                            UserId = 1,
                            Comment = "",
                            OldStatus = new DtoTypes.Status() {Id = 1, Description = "435345", Alive = true},
                            NewStatus = new DtoTypes.Status() {Id = 1, Description = "we4354", Alive = false}
                        },
                        new DtoTypes.StatusDiff()
                        {
                            UserId = 2,
                            Comment = "ABCDEFG",
                            OldStatus = new DtoTypes.Status() {Id = 1, Description = "435345", Alive = true},
                            NewStatus = new DtoTypes.Status() {Id = 1, Description = "aaaaa", Alive = false},
                        }
                    }
                },
                true
             ),
            (
                new ListOfStatusDiffs()
                {
                    StatusDiff =
                    {
                        new DtoTypes.StatusDiff()
                        {
                            UserId = 9,
                            Comment = "04359 435646",
                            OldStatus = new DtoTypes.Status() {Id = 19, Description = "test1", Alive = false},
                            NewStatus = new DtoTypes.Status() {Id = 19, Description = "test2", Alive = true},
                        }
                    }
                },
                false
            )
        };

        [TestCaseSource(nameof(InsertUpdateStatusWithHistoryData))]
        public void InsertStatusWithHistoryCallsUseCase((ListOfStatusDiffs statusDiffs, bool returnList) data)
        {
            var useCase = new StatusUseCaseMock();
            var service = new NetworkView.Services.StatusService(null, useCase);

            var request = new InsertStatusWithHistoryRequest()
            {
                StatusDiffs = data.statusDiffs,
                ReturnList = data.returnList
            };

            service.InsertStatusWithHistory(request, null);

            Assert.AreEqual(data.returnList, useCase.InsertStatusWithHistoryParameterReturnList);

            var comparer = new Func<DtoTypes.StatusDiff, StatusDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareStatusDtoWithStatus(dtoDiff.OldStatus, diff.GetOldStatus()) &&
                EqualityChecker.CompareStatusDtoWithStatus(dtoDiff.NewStatus, diff.GetNewStatus())
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.statusDiffs.StatusDiff, useCase.InsertStatusWithHistoryParameterStatusDiffs, comparer);
        }

        [TestCaseSource(nameof(statusData))]
        public void InsertStatusWithHistoryReturnsCorrectValue(List<Status> statusIds)
        {
            var useCase = new StatusUseCaseMock();
            var service = new NetworkView.Services.StatusService(null, useCase);

            useCase.InsertStatusWithHistoryReturnValue = statusIds;

            var request = new InsertStatusWithHistoryRequest()
            {
                StatusDiffs = new ListOfStatusDiffs()
            };

            var result = service.InsertStatusWithHistory(request, null).Result;

            var comparer = new Func<Status, DtoTypes.Status, bool>((status, statusDto) =>
                EqualityChecker.CompareStatusDtoWithStatus(statusDto, status)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(statusIds, result.Status, comparer);
        }

        [TestCaseSource(nameof(InsertUpdateStatusWithHistoryData))]
        public void UpdateStatusWithHistoryCallsUseCase((ListOfStatusDiffs statusDiffs, bool returnList) data)
        {
            var useCase = new StatusUseCaseMock();
            var service = new NetworkView.Services.StatusService(null, useCase);

            var request = new UpdateStatusWithHistoryRequest()
            {
                StatusDiffs = data.statusDiffs
            };

            service.UpdateStatusWithHistory(request, null);

            var comparer = new Func<DtoTypes.StatusDiff, StatusDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareStatusDtoWithStatus(dtoDiff.OldStatus, diff.GetOldStatus()) &&
                EqualityChecker.CompareStatusDtoWithStatus(dtoDiff.NewStatus, diff.GetNewStatus())
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.statusDiffs.StatusDiff, useCase.UpdateStatusWithHistoryParameter, comparer);
        }

        [TestCaseSource(nameof(statusData))]
        public void UpdateStatusWithHistoryReturnsCorrectValue(List<Status> statusIds)
        {
            var useCase = new StatusUseCaseMock();
            var service = new NetworkView.Services.StatusService(null, useCase);

            useCase.UpdateStatusWithHistoryReturnValue = statusIds;

            var request = new UpdateStatusWithHistoryRequest()
            {
                StatusDiffs = new ListOfStatusDiffs()
            };

            var result = service.UpdateStatusWithHistory(request, null).Result;

            var comparer = new Func<Status, DtoTypes.Status, bool>((status, statusDto) =>
                EqualityChecker.CompareStatusDtoWithStatus(statusDto, status)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(statusIds, result.Status, comparer);
        }
    }
}
