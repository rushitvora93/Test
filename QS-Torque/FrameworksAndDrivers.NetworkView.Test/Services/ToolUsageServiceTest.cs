using System;
using System.Collections.Generic;
using BasicTypes;
using DtoTypes;
using NUnit.Framework;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using TestHelper.Checker;
using ToolUsageService;
using ToolUsage = Server.Core.Entities.ToolUsage;
using ToolUsageDiff = Server.Core.Diffs.ToolUsageDiff;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    public class ToolUsageUseCaseMock : IToolUsageUseCase
    {
        public List<ToolUsage> GetAllToolUsagesReturnValue { get; set; } = new List<ToolUsage>();
        public bool GetAllToolUsagesCalled { get; set; }
        public List<ToolUsage> InsertToolUsagesWithHistoryReturnValue { get; set; } = new List<ToolUsage>();
        public bool InsertToolUsagesWithHistoryReturnListParameter { get; set; }
        public List<ToolUsageDiff> InsertToolUsagesWithHistoryDiffParameter { get; set; }
        public List<ToolUsage> UpdateToolUsagesWithHistoryReturnValue { get; set; } = new List<ToolUsage>();
        public List<ToolUsageDiff> UpdateToolUsagesWithHistoryParameter { get; set; }
        public List<long> GetToolUsageLocationToolAssignmentReferencesReturnValue { get; set; } = new List<long>();
        public ToolUsageId GetToolUsageLocationToolAssignmentReferencesParameter { get; set; }

        public List<ToolUsage> GetAllToolUsages()
        {
            GetAllToolUsagesCalled = true;
            return GetAllToolUsagesReturnValue;
        }

        public List<ToolUsage> InsertToolUsagesWithHistory(List<ToolUsageDiff> toolUsagesDiff, bool returnList)
        {
            InsertToolUsagesWithHistoryDiffParameter = toolUsagesDiff;
            InsertToolUsagesWithHistoryReturnListParameter = returnList;
            return InsertToolUsagesWithHistoryReturnValue;
        }

        public List<ToolUsage> UpdateToolUsagesWithHistory(List<ToolUsageDiff> toolUsagesDiff)
        {
            UpdateToolUsagesWithHistoryParameter = toolUsagesDiff;
            return UpdateToolUsagesWithHistoryReturnValue;
        }

        public List<long> GetToolUsageLocationToolAssignmentReferences(ToolUsageId id)
        {
            GetToolUsageLocationToolAssignmentReferencesParameter = id;
            return GetToolUsageLocationToolAssignmentReferencesReturnValue;
        }
    }

    public class ToolUsageServiceTest
    {
        [Test]
        public void GetAllToolUsagesCallsUseCase()
        {
            var useCase = new ToolUsageUseCaseMock();
            var service = new NetworkView.Services.ToolUsageService(null, useCase);

            service.GetAllToolUsages(new NoParams(), null);

            Assert.IsTrue(useCase.GetAllToolUsagesCalled);
        }

        private static IEnumerable<List<ToolUsage>> toolUsageData = new List<List<ToolUsage>>()
        {
            new List<ToolUsage>()
            {
                new ToolUsage() {Id = new ToolUsageId(1), Description = new ToolUsageDescription("test"), Alive = true},
                new ToolUsage() {Id = new ToolUsageId(12), Description = new ToolUsageDescription("Hand 1"), Alive = false}
            },
            new List<ToolUsage>()
            {
                new ToolUsage() {Id = new ToolUsageId(145), Description = new ToolUsageDescription("1. Hand"), Alive = true}
            }
        };

        [TestCaseSource(nameof(toolUsageData))]
        public void GetAllToolUsagesReturnsCorrectValue(List<ToolUsage> toolUsages)
        {
            var useCase = new ToolUsageUseCaseMock();
            var service = new NetworkView.Services.ToolUsageService(null, useCase);
            useCase.GetAllToolUsagesReturnValue = toolUsages;

            var result = service.GetAllToolUsages(new NoParams(), null);

            var comparer = new Func<ToolUsage, DtoTypes.ToolUsage, bool>((toolUsage, dtoToolUsage) =>
                EqualityChecker.CompareToolUsageDtoWithToolUsage(dtoToolUsage, toolUsage)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(toolUsages, result.Result.ToolUsageList, comparer);
        }

        [TestCase(1)]
        [TestCase(99)]
        public void GetToolUsageLocationToolAssignmentReferencesCallsUseCase(long classId)
        {
            var useCase = new ToolUsageUseCaseMock();
            var service = new NetworkView.Services.ToolUsageService(null, useCase);

            service.GetToolUsageLocationToolAssignmentReferences(new Long() { Value = classId }, null);

            Assert.AreEqual(classId, useCase.GetToolUsageLocationToolAssignmentReferencesParameter.ToLong());
        }

        static IEnumerable<List<long>> GetToolUsageLocationToolAssignmentReferencesData = new List<List<long>>()
        {
            new List<long>{1,2,3,4},
            new List<long>{5,8,9,11}
        };

        [TestCaseSource(nameof(GetToolUsageLocationToolAssignmentReferencesData))]
        public void GetToleranceClassToolLinksReturnsCorrectValue(List<long> toolLinksIds)
        {
            var useCase = new ToolUsageUseCaseMock();
            var service = new NetworkView.Services.ToolUsageService(null, useCase);
            useCase.GetToolUsageLocationToolAssignmentReferencesReturnValue = toolLinksIds;

            var result = service.GetToolUsageLocationToolAssignmentReferences(new Long(), null);

            var comparer = new Func<long, Long, bool>((val, dtoVal) =>
                val == dtoVal.Value
            );

            CheckerFunctions.CollectionAssertAreEquivalent(toolLinksIds, result.Result.Values, comparer);
        }

        static IEnumerable<(ListOfToolUsageDiffs, bool)> InsertUpdateToolUsageWithHistoryData = new List<(ListOfToolUsageDiffs, bool)>
        {
            (
                new ListOfToolUsageDiffs()
                {
                    ToolUsageDiffs =
                    {
                        new DtoTypes.ToolUsageDiff()
                        {
                            UserId = 1,
                            Comment = "",
                            OldToolUsage = new DtoTypes.ToolUsage() {Id = 1, Description = "1. Hand", Alive = true},
                            NewToolUsage = new DtoTypes.ToolUsage() {Id = 1, Description = "1. Hand A", Alive = false}
                        },
                        new DtoTypes.ToolUsageDiff()
                        {
                            UserId = 2,
                            Comment = "ABCDEFG",
                            OldToolUsage =  new DtoTypes.ToolUsage() {Id = 3, Description = "TBC 983", Alive = true},
                            NewToolUsage = new DtoTypes.ToolUsage() {Id = 1, Description = "1. Hand", Alive = true},
                        }
                    }
                },
                true
             ),
            (
                new ListOfToolUsageDiffs()
                {
                    ToolUsageDiffs =
                    {
                        new DtoTypes.ToolUsageDiff()
                        {
                            UserId = 1,
                            Comment = "",
                            OldToolUsage = new DtoTypes.ToolUsage() {Id = 9, Description = "1. Fuß", Alive = true},
                            NewToolUsage = new DtoTypes.ToolUsage() {Id = 9, Description = "1. Fuß A", Alive = false}
                        }
                    }
                },
                false
            )
        };

        [TestCaseSource(nameof(InsertUpdateToolUsageWithHistoryData))]
        public void InsertToolUsagesWithHistoryCallsUseCase((ListOfToolUsageDiffs toolUsageDiffs, bool returnList) data)
        {
            var useCase = new ToolUsageUseCaseMock();
            var service = new NetworkView.Services.ToolUsageService(null, useCase);

            var request = new InsertToolUsagesWithHistoryRequest()
            {
                ToolUsageDiffs = data.toolUsageDiffs,
                ReturnList = data.returnList
            };

            service.InsertToolUsagesWithHistory(request, null);

            Assert.AreEqual(data.returnList, useCase.InsertToolUsagesWithHistoryReturnListParameter);

            var comparer = new Func<DtoTypes.ToolUsageDiff, ToolUsageDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareToolUsageDtoWithToolUsage(dtoDiff.OldToolUsage, diff.GetOldToolUsage()) &&
                EqualityChecker.CompareToolUsageDtoWithToolUsage(dtoDiff.NewToolUsage, diff.GetNewToolUsage())
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.toolUsageDiffs.ToolUsageDiffs, useCase.InsertToolUsagesWithHistoryDiffParameter, comparer);
        }

        [TestCaseSource(nameof(toolUsageData))]
        public void InsertToolUsageWithHistoryReturnsCorrectValue(List<ToolUsage> toolUsages)
        {
            var useCase = new ToolUsageUseCaseMock();
            var service = new NetworkView.Services.ToolUsageService(null, useCase);

            useCase.InsertToolUsagesWithHistoryReturnValue = toolUsages;

            var request = new InsertToolUsagesWithHistoryRequest()
            {
                ToolUsageDiffs = new ListOfToolUsageDiffs()
            };

            var result = service.InsertToolUsagesWithHistory(request, null).Result;

            var comparer = new Func<ToolUsage, DtoTypes.ToolUsage, bool>((toolUsage, dtoToolUsage) =>
                EqualityChecker.CompareToolUsageDtoWithToolUsage(dtoToolUsage, toolUsage)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(toolUsages, result.ToolUsageList, comparer);
        }

        [TestCaseSource(nameof(InsertUpdateToolUsageWithHistoryData))]
        public void UpdateToolUsageWithHistoryCallsUseCase((ListOfToolUsageDiffs toolUsageDiffs, bool returnList) data)
        {
            var useCase = new ToolUsageUseCaseMock();
            var service = new NetworkView.Services.ToolUsageService(null, useCase);

            var request = new UpdateToolUsagesWithHistoryRequest()
            {
                ToolUsageDiffs = data.toolUsageDiffs
            };

            service.UpdateToolUsagesWithHistory(request, null);

            var comparer = new Func<DtoTypes.ToolUsageDiff, ToolUsageDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareToolUsageDtoWithToolUsage(dtoDiff.OldToolUsage, diff.GetOldToolUsage()) &&
                EqualityChecker.CompareToolUsageDtoWithToolUsage(dtoDiff.NewToolUsage, diff.GetNewToolUsage())
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.toolUsageDiffs.ToolUsageDiffs, useCase.UpdateToolUsagesWithHistoryParameter, comparer);
        }

        [TestCaseSource(nameof(toolUsageData))]
        public void UpdateToolUsageWithHistoryReturnsCorrectValue(List<ToolUsage> toolUsages)
        {
            var useCase = new ToolUsageUseCaseMock();
            var service = new NetworkView.Services.ToolUsageService(null, useCase);

            useCase.UpdateToolUsagesWithHistoryReturnValue = toolUsages;

            var request = new UpdateToolUsagesWithHistoryRequest()
            {
                ToolUsageDiffs = new ListOfToolUsageDiffs()
            };

            var result = service.UpdateToolUsagesWithHistory(request, null).Result;

            var comparer = new Func<ToolUsage, DtoTypes.ToolUsage, bool>((toolUsage, dtoToolUsage) =>
                EqualityChecker.CompareToolUsageDtoWithToolUsage(dtoToolUsage, toolUsage)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(toolUsages, result.ToolUsageList, comparer);
        }
    }
}
