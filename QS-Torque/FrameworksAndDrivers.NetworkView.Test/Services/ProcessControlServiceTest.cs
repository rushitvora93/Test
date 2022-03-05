using System;
using Server.UseCases.UseCases;
using System.Collections.Generic;
using BasicTypes;
using NUnit.Framework;
using ProcessControlService;
using Server.Core.Entities;
using Server.TestHelper.Factories;
using TestHelper.Checker;
using ProcessControlCondition = DtoTypes.ProcessControlCondition;
using ProcessControlConditionDiff = Server.Core.Diffs.ProcessControlConditionDiff;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    public class ProcessControlUseCaseMock : IProcessControlUseCase
    {
        public List<ProcessControlConditionDiff> UpdateProcessControlConditionsWithHistoryParameter { get; set; }
        public Server.Core.Entities.ProcessControlCondition LoadProcessControlConditionForLocationReturnValue { get; set; }
        public LocationId LoadProcessControlConditionForLocationParameter { get; set; }
        public List<Server.Core.Entities.ProcessControlCondition> InsertProcessControlConditionsWithHistoryReturnValue { get; set; } = new List<Server.Core.Entities.ProcessControlCondition>();
        public bool InsertProcessControlConditionsWithHistoryParameterReturnList { get; set; }
        public List<ProcessControlConditionDiff> InsertProcessControlConditionsWithHistoryParameterDiff { get; set; }
        public bool LoadProcessControlConditionsCalled { get; set; }
        public List<Server.Core.Entities.ProcessControlCondition> LoadProcessControlConditionsReturnValue { get; set; }

        public void UpdateProcessControlConditionsWithHistory(List<ProcessControlConditionDiff> processControlConditionDiffs)
        {
            UpdateProcessControlConditionsWithHistoryParameter = processControlConditionDiffs;
        }

        public Server.Core.Entities.ProcessControlCondition LoadProcessControlConditionForLocation(LocationId locationId)
        {
            LoadProcessControlConditionForLocationParameter = locationId;
            return LoadProcessControlConditionForLocationReturnValue;
        }

        public List<Server.Core.Entities.ProcessControlCondition> InsertProcessControlConditionsWithHistory(List<ProcessControlConditionDiff> diffs, bool returnList)
        {
            InsertProcessControlConditionsWithHistoryParameterDiff = diffs;
            InsertProcessControlConditionsWithHistoryParameterReturnList = returnList;
            return InsertProcessControlConditionsWithHistoryReturnValue;
        }

        public List<Server.Core.Entities.ProcessControlCondition> LoadProcessControlConditions()
        {
            LoadProcessControlConditionsCalled = true;
            return LoadProcessControlConditionsReturnValue;
        }
    }

    public class ProcessControlServiceTest
    {
        [TestCase(1)]
        [TestCase(6)]
        public void LoadProcessControlConditionForLocationCallsUseCase(long id)
        {
            var useCase = new ProcessControlUseCaseMock();
            var service = new NetworkView.Services.ProcessControlService(null, useCase);

            service.LoadProcessControlConditionForLocation(new Long(){Value = id}, null);

            Assert.AreEqual(id, useCase.LoadProcessControlConditionForLocationParameter.ToLong());
        }


        private static IEnumerable<Server.Core.Entities.ProcessControlCondition> ProcessControlConditionDatas =
            new List<Server.Core.Entities.ProcessControlCondition>()
            {
                CreateProcessControlCondition.Randomized(34654),
                CreateProcessControlCondition.Randomized(43535),
                null
            };

        [TestCaseSource(nameof(ProcessControlConditionDatas))]
        public void LoadProcessControlConditionForLocationReturnsCorrectValue(Server.Core.Entities.ProcessControlCondition processControl)
        {
            var useCase = new ProcessControlUseCaseMock();
            var service = new NetworkView.Services.ProcessControlService(null, useCase);
            useCase.LoadProcessControlConditionForLocationReturnValue = processControl;

            var result = service.LoadProcessControlConditionForLocation(new Long(), null);

            Assert.IsTrue(EqualityChecker.CompareProcessControlConditionToDto(processControl, result?.Result?.Value));
        }


        static IEnumerable<(ListOfProcessControlConditionDiffs, bool)> InsertUpdateProcessControlConditionWithHistoryData = new List<(ListOfProcessControlConditionDiffs, bool)>
        {
            (
                new ListOfProcessControlConditionDiffs()
                {
                    ConditionDiff =
                    {
                        new DtoTypes.ProcessControlConditionDiff()
                        {
                            UserId = 1,
                            Comment = new NullableString(){IsNull = false, Value = "4365678"},
                            OldCondition = DtoFactory.CreateProcessControlConditionRandomized(45646),
                            NewCondition = DtoFactory.CreateProcessControlConditionRandomized(32423),
                        },
                        new DtoTypes.ProcessControlConditionDiff()
                        {
                            UserId = 14,
                            Comment = new NullableString(){IsNull = false, Value = "345647"},
                            OldCondition = DtoFactory.CreateProcessControlConditionRandomized(111),
                            NewCondition = DtoFactory.CreateProcessControlConditionRandomized(2222),
                        },
                    }
                },
                true
             ),
            (
                new ListOfProcessControlConditionDiffs()
                {
                    ConditionDiff =
                    {
                        new DtoTypes.ProcessControlConditionDiff()
                        {
                            UserId = 451,
                            Comment = new NullableString(){IsNull = false, Value = "43547558"},
                            OldCondition = DtoFactory.CreateProcessControlConditionRandomized(4567),
                            NewCondition = DtoFactory.CreateProcessControlConditionRandomized(12),
                        },
                        new DtoTypes.ProcessControlConditionDiff()
                        {
                            UserId = 23,
                            Comment = new NullableString(){IsNull = false, Value = "43546"},
                            OldCondition = DtoFactory.CreateProcessControlConditionRandomized(124324),
                            NewCondition = DtoFactory.CreateProcessControlConditionRandomized(5674),
                        },
                    }
                },
                false
            )
        };


        [TestCaseSource(nameof(InsertUpdateProcessControlConditionWithHistoryData))]
        public void InsertProcessControlConditionsWithHistoryCallsUseCase((ListOfProcessControlConditionDiffs processControlConditionDiffs, bool returnList) data)
        {
            var useCase = new ProcessControlUseCaseMock();
            var service = new NetworkView.Services.ProcessControlService(null, useCase);

            var request = new InsertProcessControlConditionsWithHistoryRequest()
            {
                Diffs = data.processControlConditionDiffs,
                ReturnList = data.returnList
            };

            service.InsertProcessControlConditionsWithHistory(request, null);

            Assert.AreEqual(data.returnList, useCase.InsertProcessControlConditionsWithHistoryParameterReturnList);

            var comparer = new Func<DtoTypes.ProcessControlConditionDiff, ProcessControlConditionDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment.Value == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareProcessControlConditionToDto(diff.GetOldProcessControlCondition(), dtoDiff.OldCondition) &&
                EqualityChecker.CompareProcessControlConditionToDto(diff.GetNewProcessControlCondition(), dtoDiff.NewCondition)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.processControlConditionDiffs.ConditionDiff, useCase.InsertProcessControlConditionsWithHistoryParameterDiff, comparer);
        }


        [TestCaseSource(nameof(InsertUpdateProcessControlConditionWithHistoryData))]
        public void UpdateProcessControlConditionsWithHistoryCallsUseCase((ListOfProcessControlConditionDiffs processControlConditionDiffs, bool returnList) data)
        {
            var useCase = new ProcessControlUseCaseMock();
            var service = new NetworkView.Services.ProcessControlService(null, useCase);

            var request = new UpdateProcessControlConditionsWithHistoryRequest()
            {
                ConditionDiffs = data.processControlConditionDiffs,
            };

            service.UpdateProcessControlConditionsWithHistory(request, null);

            var comparer = new Func<DtoTypes.ProcessControlConditionDiff, ProcessControlConditionDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment.Value == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareProcessControlConditionToDto(diff.GetOldProcessControlCondition(), dtoDiff.OldCondition) &&
                EqualityChecker.CompareProcessControlConditionToDto(diff.GetNewProcessControlCondition(), dtoDiff.NewCondition)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.processControlConditionDiffs.ConditionDiff, useCase.UpdateProcessControlConditionsWithHistoryParameter, comparer);
        }


        private static IEnumerable<List<Server.Core.Entities.ProcessControlCondition>> ProcessControlConditionData =
            new List<List<Server.Core.Entities.ProcessControlCondition>>()
            {
                new List<Server.Core.Entities.ProcessControlCondition>()
                {
                    CreateProcessControlCondition.Randomized(9878),
                    CreateProcessControlCondition.Randomized(1324),
                },
                new List<Server.Core.Entities.ProcessControlCondition>()
                {
                    CreateProcessControlCondition.Randomized(7777),
                }
            };

        [TestCaseSource(nameof(ProcessControlConditionData))]
        public void InsertProcessControlConditionsWithHistoryReturnsCorrectValue(List<Server.Core.Entities.ProcessControlCondition> processControlConditions)
        {
            var useCase = new ProcessControlUseCaseMock();
            var service = new NetworkView.Services.ProcessControlService(null, useCase);

            useCase.InsertProcessControlConditionsWithHistoryReturnValue = processControlConditions;

            var request = new InsertProcessControlConditionsWithHistoryRequest()
            {
                Diffs = new ListOfProcessControlConditionDiffs()
            };

            var result = service.InsertProcessControlConditionsWithHistory(request, null).Result;

            var comparer = new Func<Server.Core.Entities.ProcessControlCondition, DtoTypes.ProcessControlCondition, bool>((processControlCondition, dtoProcessControlCondition) =>
                EqualityChecker.CompareProcessControlConditionToDto(processControlCondition, dtoProcessControlCondition)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(processControlConditions, result.ProcessControlConditions, comparer);
        }

        [Test]
        public void LoadProcessControlConditionsCallsUseCase()
        {
            var useCase = new ProcessControlUseCaseMock();
            useCase.LoadProcessControlConditionsReturnValue = new List<Server.Core.Entities.ProcessControlCondition>();
            var service = new NetworkView.Services.ProcessControlService(null, useCase);

            service.LoadProcessControlConditions(new NoParams(), null);

            Assert.IsTrue(useCase.LoadProcessControlConditionsCalled);
        }


        private static IEnumerable<List<Server.Core.Entities.ProcessControlCondition>> ProcessControlConditionDataLists =
            new List<List<Server.Core.Entities.ProcessControlCondition>>()
            {
                new List<Server.Core.Entities.ProcessControlCondition>()
                {
                    CreateProcessControlCondition.Randomized(34654),
                    CreateProcessControlCondition.Randomized(43535)
                },
                new List<Server.Core.Entities.ProcessControlCondition>()
                {
                    CreateProcessControlCondition.Randomized(741),
                    CreateProcessControlCondition.Randomized(258)
                }
            };

        [TestCaseSource(nameof(ProcessControlConditionDataLists))]
        public void LoadProcessControlConditionsReturnsCorrectValue(List<Server.Core.Entities.ProcessControlCondition> processControls)
        {
            var useCase = new ProcessControlUseCaseMock();
            var service = new NetworkView.Services.ProcessControlService(null, useCase);
            useCase.LoadProcessControlConditionsReturnValue = processControls;

            var result = service.LoadProcessControlConditions(new NoParams(), null).Result;

            Assert.IsTrue(EqualityChecker.CompareProcessControlConditionToDto(processControls[0], result.Conditions[0]));
            Assert.IsTrue(EqualityChecker.CompareProcessControlConditionToDto(processControls[1], result.Conditions[1]));
        }
    }
}
