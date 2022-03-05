using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Enums;
using Server.TestHelper.Factories;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    public enum ProcessControlDataAccessFunction
    {
        InsertProcessControlConditions,
        InsertProcessControlTech,
        UpdateProcessControlConditions,
        UpdateProcessControlTech,
        Commit
    }

    public class ProcessControlDataAccessMock : IProcessControlDataAccess
    {
        public List<ProcessControlDataAccessFunction> CalledFunctions { get; set; } = new List<ProcessControlDataAccessFunction>();
        public bool CommitExecuted { get; set; }
        public List<ProcessControlConditionDiff> UpdateProcessControlConditionWithHistoryArgument { get; set; }
        public List<ProcessControlCondition> UpdateProcessControlConditionWithHistoryReturnValue { get; set; }
        public ProcessControlCondition LoadProcessControlConditionForLocationReturnValue { get; set; }
        public LocationId LoadProcessControlConditionForLocationParameter { get; set; }
        public List<ProcessControlCondition> InsertProcessControlConditionsWithHistoryReturnValue { get; set; }
        public bool InsertProcessControlConditionsWithHistoryParameterReturnList { get; set; }
        public List<ProcessControlConditionDiff> InsertProcessControlConditionsWithHistoryParameterDiffs { get; set; }
        public List<ProcessControlCondition> LoadProcessControlConditionsReturnValue { get; set; }
        public ProcessControlConditionId GetProcessControlConditionByIdParameter { get; set; }
        public ProcessControlCondition GetProcessControlConditionByIdReturnValue { get; set; } = new ProcessControlCondition();
        public ProcessControlConditionId SaveNextTestDatesForParamId { get; set; }
        public DateTime? SaveNextTestDatesForParamNextTestDate { get; set; }
        public Shift? SaveNextTestDatesForParamNextTestShift { get; set; }
        public DateTime? SaveNextTestDatesForParamEndOfLastTestPeriod { get; set; }
        public Shift? SaveNextTestDatesForParamEndOfLastTestPeriodShift { get; set; }
        public TestLevelSetId GetProcessControlConditionIdsForTestLevelSetParameter { get; set; }
        public List<ProcessControlConditionId> GetProcessControlConditionIdsForTestLevelSetReturnValue { get; set; }

        public List<ProcessControlCondition> UpdateProcessControlConditionsWithHistory(List<ProcessControlConditionDiff> processControlConditionDiffs)
        {
            CalledFunctions.Add(ProcessControlDataAccessFunction.UpdateProcessControlConditions);
            UpdateProcessControlConditionWithHistoryArgument = processControlConditionDiffs;
            return UpdateProcessControlConditionWithHistoryReturnValue;
        }

        public ProcessControlCondition LoadProcessControlConditionForLocation(LocationId locationId)
        {
            LoadProcessControlConditionForLocationParameter = locationId;
            return LoadProcessControlConditionForLocationReturnValue;
        }

        public List<ProcessControlCondition> InsertProcessControlConditionsWithHistory(List<ProcessControlConditionDiff> diffs, bool returnList)
        {
            CalledFunctions.Add(ProcessControlDataAccessFunction.InsertProcessControlConditions);
            InsertProcessControlConditionsWithHistoryParameterDiffs = diffs;
            InsertProcessControlConditionsWithHistoryParameterReturnList = returnList;
            return InsertProcessControlConditionsWithHistoryReturnValue;
        }

        public void Commit()
        {
            CalledFunctions.Add(ProcessControlDataAccessFunction.Commit);
            CommitExecuted = true;
        }

        public List<ProcessControlCondition> LoadProcessControlConditions()
        {
            return LoadProcessControlConditionsReturnValue;
        }

        public ProcessControlCondition GetProcessControlConditionById(ProcessControlConditionId id)
        {
            GetProcessControlConditionByIdParameter = id;
            return GetProcessControlConditionByIdReturnValue;
        }

        public List<ProcessControlConditionId> GetProcessControlConditionIdsForTestLevelSet(TestLevelSetId id)
        {
            GetProcessControlConditionIdsForTestLevelSetParameter = id;
            return GetProcessControlConditionIdsForTestLevelSetReturnValue;
        }

        public void SaveNextTestDatesFor(ProcessControlConditionId id, System.DateTime? nextTestDate, Shift? nextTestShift, System.DateTime? endOfLastTestPeriod, Shift? endOfLastTestPeriodShift)
        {
            SaveNextTestDatesForParamId = id;
            SaveNextTestDatesForParamNextTestDate = nextTestDate;
            SaveNextTestDatesForParamNextTestShift = nextTestShift;
            SaveNextTestDatesForParamEndOfLastTestPeriod = endOfLastTestPeriod;
            SaveNextTestDatesForParamEndOfLastTestPeriodShift = endOfLastTestPeriodShift;
        }
    }

    public class ProcessControlTechDataAccessMock : IProcessControlTechDataAccess
    {
        private readonly ProcessControlDataAccessMock _processControlDataAccess;

        public ProcessControlTechDataAccessMock(ProcessControlDataAccessMock processControlDataAccess)
        {
            _processControlDataAccess = processControlDataAccess;
        }

        public List<ProcessControlTech> InsertProcessControlTechsWithHistoryReturnValue { get; set; }
        public bool InsertProcessControlTechsWithHistoryParameterReturnList { get; set; }
        public List<ProcessControlTechDiff> InsertProcessControlTechsWithHistoryParameterDiff { get; set; }
        public List<ProcessControlTechDiff> UpdateProcessControlTechsWithHistoryParameter { get; set; }

        public void UpdateProcessControlTechsWithHistory(List<ProcessControlTechDiff> processControlTechDiffs)
        {
            _processControlDataAccess.CalledFunctions.Add(ProcessControlDataAccessFunction.UpdateProcessControlTech);
            UpdateProcessControlTechsWithHistoryParameter = processControlTechDiffs;
        }

        public List<ProcessControlTech> InsertProcessControlTechsWithHistory(List<ProcessControlTechDiff> diffs, bool returnList)
        {
            _processControlDataAccess.CalledFunctions.Add(ProcessControlDataAccessFunction.InsertProcessControlTech);
            InsertProcessControlTechsWithHistoryParameterDiff = diffs;
            InsertProcessControlTechsWithHistoryParameterReturnList = returnList;
            return InsertProcessControlTechsWithHistoryReturnValue;
        }

        public void Commit()
        {
            throw new System.NotImplementedException();
        }
    }

    public class ProcessControlUseCaseTest
    {
        [Test]
        public void UpdateProcessControlConditionsWithHistoryCallsDataAccess()
        {
            var environment = new Environment();
            var processControlConditionDiffs = new List<ProcessControlConditionDiff>();
            environment.useCase.UpdateProcessControlConditionsWithHistory(processControlConditionDiffs);
            Assert.AreSame(environment.mocks.processControlDataAccess.UpdateProcessControlConditionWithHistoryArgument, processControlConditionDiffs);
        }

        [TestCaseSource(nameof(InsertProcessControlConditionCallsTechDataAccessData))]
        public void UpdateProcessControlConditionsWithHistoryCallsTechDataAccess((List<ProcessControlConditionDiff> diffs, bool returnList) data)
        {
            var environment = new Environment();
            environment.useCase.UpdateProcessControlConditionsWithHistory(data.diffs);

            var techDiffs = environment.mocks.processControlTechDataAccess.UpdateProcessControlTechsWithHistoryParameter;

            Assert.AreEqual(data.diffs.Select(x => x.GetOldProcessControlCondition().ProcessControlTech).ToList(), techDiffs.Select(x => x.GetOldProcessControlTech()).ToList());
            Assert.AreEqual(data.diffs.Select(x => x.GetNewProcessControlCondition().ProcessControlTech).ToList(), techDiffs.Select(x => x.GetNewProcessControlTech()).ToList());
            Assert.AreEqual(data.diffs.Select(x => x.GetUser()).ToList(), techDiffs.Select(x => x.GetUser()).ToList());
            Assert.AreEqual(data.diffs.Select(x => x.GetComment()).ToList(), techDiffs.Select(x => x.GetComment()).ToList());
        }

        [Test]
        public void UpdateProcessControlCallsCommit()
        {
            var environment = new Environment();

            var processControlConditionDiffs = new List<ProcessControlConditionDiff>();
            
            environment.useCase.UpdateProcessControlConditionsWithHistory(processControlConditionDiffs);
            Assert.AreEqual(ProcessControlDataAccessFunction.Commit, environment.mocks.processControlDataAccess.CalledFunctions.Last());
        }

        [Test]
        public void LoadProcessControlConditionForLocationCallsDataAccess()
        {
            var environment = new Environment();

            var locationId = new LocationId(1);

            environment.useCase.LoadProcessControlConditionForLocation(locationId);

            Assert.AreSame(locationId, environment.mocks.processControlDataAccess.LoadProcessControlConditionForLocationParameter);
        }

        [Test]
        public void LoadProcessControlConditionForLocationReturnsCorrectValue()
        {
            var environment = new Environment();

            var processControl = new ProcessControlCondition();
            environment.mocks.processControlDataAccess.LoadProcessControlConditionForLocationReturnValue = processControl;

            var result = environment.useCase.LoadProcessControlConditionForLocation(new LocationId(1));

            Assert.AreSame(processControl, result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InsertProcessControlConditionCallsDataAccess(bool returnList)
        {
            var environment = new Environment();

            var diffs =  new List<ProcessControlConditionDiff>();
            environment.useCase.InsertProcessControlConditionsWithHistory(diffs, returnList);
            Assert.AreSame(diffs, environment.mocks.processControlDataAccess.InsertProcessControlConditionsWithHistoryParameterDiffs);
            Assert.AreEqual(returnList, environment.mocks.processControlDataAccess.InsertProcessControlConditionsWithHistoryParameterReturnList);
        }

        [Test]
        public void InsertProcessControlConditionReturnsCorrectValue()
        {
            var environment = new Environment();
            var processControls = new List<ProcessControlCondition>();
            environment.mocks.processControlDataAccess.InsertProcessControlConditionsWithHistoryReturnValue = processControls;
            
            var result = environment.useCase.InsertProcessControlConditionsWithHistory(new List<ProcessControlConditionDiff>(),true);
            Assert.AreSame(processControls, result);
        }

        private static IEnumerable<(List<ProcessControlConditionDiff>, bool returnList)>
            InsertProcessControlConditionCallsTechDataAccessData =
                new List<(List<ProcessControlConditionDiff>, bool returnList)>()
                {
                    (
                        new List<ProcessControlConditionDiff>()
                        {
                            new ProcessControlConditionDiff(
                                CreateUser.Parametrized(1, "u1", new Group(){Id = new GroupId(1)}),
                                new HistoryComment("com"),
                                CreateProcessControlCondition.Randomized(235),
                                CreateProcessControlCondition.Randomized(43536)),
                            new ProcessControlConditionDiff(
                                CreateUser.Parametrized(3, "u3", new Group(){Id = new GroupId(51)}),
                                new HistoryComment("ABC"),
                                CreateProcessControlCondition.Randomized(23455),
                                CreateProcessControlCondition.Randomized(56768))
                        },
                        true
                    ),
                    (
                        new List<ProcessControlConditionDiff>()
                        {
                            new ProcessControlConditionDiff(
                                CreateUser.Parametrized(165, "X1", new Group(){Id = new GroupId(15)}),
                                new HistoryComment("ABCDE"),
                                CreateProcessControlCondition.Randomized(1111),
                                CreateProcessControlCondition.Randomized(45665))
                        },
                        false
                    )
                };

        [TestCaseSource(nameof(InsertProcessControlConditionCallsTechDataAccessData))]
        public void InsertProcessControlConditionCallsTechDataAccess((List<ProcessControlConditionDiff> diffs, bool returnList) data)
        {
            var environment = new Environment();
            environment.useCase.InsertProcessControlConditionsWithHistory(data.diffs, data.returnList);
            
            var techDiffs = environment.mocks.processControlTechDataAccess.InsertProcessControlTechsWithHistoryParameterDiff;

            Assert.AreEqual(data.diffs.Select(x => x.GetOldProcessControlCondition().ProcessControlTech).ToList(), techDiffs.Select(x => x.GetOldProcessControlTech()).ToList());
            Assert.AreEqual(data.diffs.Select(x => x.GetNewProcessControlCondition().ProcessControlTech).ToList(), techDiffs.Select(x => x.GetNewProcessControlTech()).ToList());
            Assert.AreEqual(data.diffs.Select(x => x.GetUser()).ToList(), techDiffs.Select(x => x.GetUser()).ToList());
            Assert.AreEqual(data.diffs.Select(x => x.GetComment()).ToList(), techDiffs.Select(x => x.GetComment()).ToList());
            Assert.AreEqual(data.returnList, environment.mocks.processControlTechDataAccess.InsertProcessControlTechsWithHistoryParameterReturnList);
        }


        [Test]
        public void InsertProcessControlConditionsCallsCommitAfterWork()
        {
            var environment = new Environment();

            environment.useCase.InsertProcessControlConditionsWithHistory(new List<ProcessControlConditionDiff>(), true);

            Assert.AreEqual(ProcessControlDataAccessFunction.Commit, environment.mocks.processControlDataAccess.CalledFunctions.Last());
        }

        [Test]
        public void LoadProcessControlConditionsReturnsValueOfDataAccess()
        {
            var environment = new Environment();
            environment.mocks.processControlDataAccess.LoadProcessControlConditionsReturnValue = new List<ProcessControlCondition>();
            var result = environment.useCase.LoadProcessControlConditions();

            Assert.AreSame(environment.mocks.processControlDataAccess.LoadProcessControlConditionsReturnValue, result);
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    processControlDataAccess = new ProcessControlDataAccessMock();
                    processControlTechDataAccess = new ProcessControlTechDataAccessMock(processControlDataAccess);

                }
                public readonly ProcessControlDataAccessMock processControlDataAccess;
                public readonly ProcessControlTechDataAccessMock processControlTechDataAccess;

            }

            public Environment()
            {
                mocks = new Mocks();
                useCase = new ProcessControlUseCase(mocks.processControlDataAccess, mocks.processControlTechDataAccess);
            }

            public readonly Mocks mocks;
            public readonly ProcessControlUseCase useCase;
        }
    }
}
