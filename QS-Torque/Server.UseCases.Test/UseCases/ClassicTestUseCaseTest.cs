using System;
using System.Collections.Generic;
using NUnit.Framework;
using Server.Core.Entities;
using Server.Core.Enums;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    public class ClassicProcessTestDataMock : IClassicProcessTestData
    {
        public List<ClassicProcessTest> GetClassicProcessHeaderFromLocationReturnValue { get; set; }
        public LocationId GetClassicProcessHeaderFromLocationParameter { get; set; }
        public List<ClassicProcessTestValue> GetValuesFromClassicProcessHeaderReturnValue { get; set; }
        public List<GlobalHistoryId> GetValuesFromClassicProcessHeaderGlobalHistoryIds { get; set; }
        public ProcessControlConditionId GetTestsForTimePeriodParameterLocToolId { get; set; }
        public DateTime GetTestsForTimePeriodParameterStartPeriodDate { get; set; }
        public Shift? GetTestsForTimePeriodParameterStartPeriodShift { get; set; }
        public DateTime GetTestsForTimePeriodParameterEndPeriodDate { get; set; }
        public Shift? GetTestsForTimePeriodParameterEndPeriodShift { get; set; }
        public TimeSpan GetTestsForTimePeriodParameterChangeOfDay { get; set; }

        public List<ClassicProcessTest> GetClassicProcessHeaderFromLocation(LocationId locationId)
        {
            GetClassicProcessHeaderFromLocationParameter = locationId;
            return GetClassicProcessHeaderFromLocationReturnValue;
        }

        public List<ClassicProcessTestValue> GetValuesFromClassicProcessHeader(List<GlobalHistoryId> ids)
        {
            GetValuesFromClassicProcessHeaderGlobalHistoryIds = ids;
            return GetValuesFromClassicProcessHeaderReturnValue;
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public List<(DateTime, Shift?)> GetTestsForTimePeriod(ProcessControlConditionId id, DateTime startPeriodDate, Shift? startPeriodShift, DateTime endPeriodDate, Shift? endPeriodShift, TimeSpan changeOfDay)
        {
            GetTestsForTimePeriodParameterLocToolId = id;
            GetTestsForTimePeriodParameterStartPeriodDate = startPeriodDate;
            GetTestsForTimePeriodParameterStartPeriodShift = startPeriodShift;
            GetTestsForTimePeriodParameterEndPeriodDate = endPeriodDate;
            GetTestsForTimePeriodParameterEndPeriodShift = endPeriodShift;
            GetTestsForTimePeriodParameterChangeOfDay = changeOfDay;
            return null;
        }
    }

    public class ClassicChkTestDataMock : IClassicChkTestData
    {
        public enum ClassicChkTestDataFunction
        {
            InsertClassicChkTests, 
            Commit
        }
        public LocationToolAssignmentId GetTestsForTimePeriodParameterLocToolId { get; set; }
        public DateTime GetTestsForTimePeriodParameterStartPeriodDate { get; set; }
        public Shift? GetTestsForTimePeriodParameterStartPeriodShift { get; set; }
        public DateTime GetTestsForTimePeriodParameterEndPeriodDate { get; set; }
        public Shift? GetTestsForTimePeriodParameterEndPeriodShift { get; set; }
        public TimeSpan GetTestsForTimePeriodParameterChangeOfDay { get; set; }
        public List<ClassicChkTestValue> GetValuesFromClassicChkHeaderReturnValue { get; set; }
        public List<GlobalHistoryId> GetValuesFromClassicChkHeaderParameter { get; set; }
        public List<ClassicChkTest> GetClassicChkHeaderFromToolReturnValue { get; set; }
        public long? GetClassicChkHeaderFromToolParameterLocationId { get; set; }
        public long GetClassicChkHeaderFromToolParameterPowToolId { get; set; }
        public List<ClassicChkTest> InsertClassicChkTestsParameter { get; set; }
        public List<ClassicChkTestDataFunction> CalledFunctions { get; set; } = new List<ClassicChkTestDataFunction>();

        public List<(DateTime, Shift?)> GetTestsForTimePeriod(LocationToolAssignmentId locToolId, DateTime startPeriodDate, Shift? startPeriodShift,
            DateTime endPeriodDate, Shift? endPeriodShift, TimeSpan changeOfDay)
        {
            GetTestsForTimePeriodParameterLocToolId = locToolId;
            GetTestsForTimePeriodParameterStartPeriodDate = startPeriodDate;
            GetTestsForTimePeriodParameterStartPeriodShift = startPeriodShift;
            GetTestsForTimePeriodParameterEndPeriodDate = endPeriodDate;
            GetTestsForTimePeriodParameterEndPeriodShift = endPeriodShift;
            GetTestsForTimePeriodParameterChangeOfDay = changeOfDay;
            return null;
        }

        public List<ClassicChkTest> GetClassicChkHeaderFromTool(long powToolId, long? locationId)
        {
            GetClassicChkHeaderFromToolParameterPowToolId = powToolId;
            GetClassicChkHeaderFromToolParameterLocationId = locationId;
            return GetClassicChkHeaderFromToolReturnValue;
        }

        public List<ClassicChkTestValue> GetValuesFromClassicChkHeader(List<GlobalHistoryId> ids)
        {
            GetValuesFromClassicChkHeaderParameter = ids;
            return GetValuesFromClassicChkHeaderReturnValue;
        }

        public void InsertClassicChkTests(List<ClassicChkTest> tests)
        {
            CalledFunctions.Add(ClassicChkTestDataFunction.InsertClassicChkTests);
            InsertClassicChkTestsParameter = tests;
        }

        public void Commit()
        {
            CalledFunctions.Add(ClassicChkTestDataFunction.Commit);
        }
    }

    public class ClassicMfuTestDataMock : IClassicMfuTestData
    {
        public enum ClassicMfuTestDataFunction
        {
            InsertClassicMfuTests,
            Commit
        }
        public LocationToolAssignmentId GetTestsForTimePeriodParameterLocToolId { get; set; }
        public DateTime GetTestsForTimePeriodParameterStartPeriodDate { get; set; }
        public Shift? GetTestsForTimePeriodParameterStartPeriodShift { get; set; }
        public DateTime GetTestsForTimePeriodParameterEndPeriodDate { get; set; }
        public Shift? GetTestsForTimePeriodParameterEndPeriodShift { get; set; }
        public TimeSpan GetTestsForTimePeriodParameterChangeOfDay { get; set; }
        public List<ClassicMfuTest> GetClassicMfuHeaderFromToolReturnValue { get; set; }
        public long? GetClassicMfuHeaderFromToolParameterLocationId { get; set; }
        public long GetClassicMfuHeaderFromToolParameterPowToolId { get; set; }
        public List<ClassicMfuTestValue> GetValuesFromClassicMfuHeaderReturnValue { get; set; }
        public List<GlobalHistoryId> GetValuesFromClassicMfuHeaderParameter { get; set; }
        public List<ClassicMfuTest> InsertClassicMfuTestsParameter { get; set; }
        public List<ClassicMfuTestDataFunction> CalledFunctions { get; set; } = new List<ClassicMfuTestDataFunction>();

        public List<(DateTime, Shift?)> GetTestsForTimePeriod(LocationToolAssignmentId locToolId, DateTime startPeriodDate, Shift? startPeriodShift,
            DateTime endPeriodDate, Shift? endPeriodShift, TimeSpan changeOfDay)
        {
            GetTestsForTimePeriodParameterLocToolId = locToolId;
            GetTestsForTimePeriodParameterStartPeriodDate = startPeriodDate;
            GetTestsForTimePeriodParameterStartPeriodShift = startPeriodShift;
            GetTestsForTimePeriodParameterEndPeriodDate = endPeriodDate;
            GetTestsForTimePeriodParameterEndPeriodShift = endPeriodShift;
            GetTestsForTimePeriodParameterChangeOfDay = changeOfDay;
            return null;
        }

        public List<ClassicMfuTest> GetClassicMfuHeaderFromTool(long powToolId, long? locationId)
        {
            GetClassicMfuHeaderFromToolParameterPowToolId = powToolId;
            GetClassicMfuHeaderFromToolParameterLocationId = locationId;
            return GetClassicMfuHeaderFromToolReturnValue;
        }

        public List<ClassicMfuTestValue> GetValuesFromClassicMfuHeader(List<GlobalHistoryId> ids)
        {
            GetValuesFromClassicMfuHeaderParameter = ids;
            return GetValuesFromClassicMfuHeaderReturnValue;
        }

        public void InsertClassicMfuTests(List<ClassicMfuTest> tests)
        {
            CalledFunctions.Add(ClassicMfuTestDataFunction.InsertClassicMfuTests);
            InsertClassicMfuTestsParameter = tests;
        }        

        public void Commit()
        {
            CalledFunctions.Add(ClassicMfuTestDataFunction.Commit);
        }
    }

    public class ClassicTestDataAccessMock : IClassicTestDataAccess
    {
        public Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)> GetToolsFromLocationTestsReturnValue { get; set; }
        public LocationId GetToolsFromLocationTestsParameter { get; set; }

        public Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)> GetToolsFromLocationTests(LocationId locationId)
        {
            GetToolsFromLocationTestsParameter = locationId;
            return GetToolsFromLocationTestsReturnValue;
        }
    }

    class ClassicTestUseCaseTest
    {
        [TestCase(1, 5)]
        [TestCase(132, 435)]
        public void GetClassicChkHeaderFromToolCallsDataAccess(long powToolId, long locationId)
        {
            var environment = new Environment();

            environment.useCase.GetClassicChkHeaderFromTool(powToolId, locationId);

            Assert.AreEqual(powToolId, environment.mocks.classicChkTestData.GetClassicChkHeaderFromToolParameterPowToolId);
            Assert.AreEqual(locationId, environment.mocks.classicChkTestData.GetClassicChkHeaderFromToolParameterLocationId);
        }

        [Test]
        public void GetClassicChkHeaderFromToolReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<ClassicChkTest>();
            environment.mocks.classicChkTestData.GetClassicChkHeaderFromToolReturnValue = entities;

            Assert.AreSame(entities, environment.useCase.GetClassicChkHeaderFromTool(1, 1));
        }

        [TestCase(1, 5)]
        [TestCase(132, 435)]
        public void GetClassicMfuHeaderFromToolCallsDataAccess(long powToolId, long locationId)
        {
            var environment = new Environment();

            environment.useCase.GetClassicMfuHeaderFromTool(powToolId, locationId);

            Assert.AreEqual(powToolId, environment.mocks.classicMfuTestData.GetClassicMfuHeaderFromToolParameterPowToolId);
            Assert.AreEqual(locationId, environment.mocks.classicMfuTestData.GetClassicMfuHeaderFromToolParameterLocationId);
        }

        [Test]
        public void GetClassicMfuHeaderFromToolReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<ClassicMfuTest>();
            environment.mocks.classicMfuTestData.GetClassicMfuHeaderFromToolReturnValue = entities;

            Assert.AreSame(entities, environment.useCase.GetClassicMfuHeaderFromTool(1, 1));
        }

        [Test]
        public void GetValuesFromClassicChkHeaderCallsDataAccess()
        {
            var environment = new Environment();

            var ids = new List<GlobalHistoryId>();
            environment.useCase.GetValuesFromClassicChkHeader(ids);

            Assert.AreSame(ids, environment.mocks.classicChkTestData.GetValuesFromClassicChkHeaderParameter);
        }

        [Test]
        public void GetValuesFromClassicChkHeaderReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<ClassicChkTestValue>();
            environment.mocks.classicChkTestData.GetValuesFromClassicChkHeaderReturnValue = entities;

            Assert.AreSame(entities, environment.useCase.GetValuesFromClassicChkHeader(new List<GlobalHistoryId>()));
        }

        [TestCase(1)]
        [TestCase(132)]
        public void GetToolsFromLocationTestsCallsDataAccess(long locationId)
        {
            var environment = new Environment();

            environment.useCase.GetToolsFromLocationTests(new LocationId(locationId));

            Assert.AreEqual(locationId, environment.mocks.classicTestData.GetToolsFromLocationTestsParameter.ToLong());
        }

        [Test]
        public void GetToolsFromLocationTestsReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)>();
            environment.mocks.classicTestData.GetToolsFromLocationTestsReturnValue = entities;

            Assert.AreSame(entities, environment.useCase.GetToolsFromLocationTests(new LocationId(1)));
        }

        [TestCase(1)]
        [TestCase(132)]
        public void GetClassicProcessHeaderFromLocationCallsDataAccess(long locationId)
        {
            var environment = new Environment();

            environment.useCase.GetClassicProcessHeaderFromLocation(new LocationId(locationId));

            Assert.AreEqual(locationId, environment.mocks.classicProcessTestData.GetClassicProcessHeaderFromLocationParameter.ToLong());
        }

        [Test]
        public void GetClassicProcessHeaderFromLocationReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<ClassicProcessTest>();
            environment.mocks.classicProcessTestData.GetClassicProcessHeaderFromLocationReturnValue = entities;

            Assert.AreSame(entities, environment.useCase.GetClassicProcessHeaderFromLocation(new LocationId(1)));
        }

        [Test]
        public void GetValuesFromClassicProcessHeaderCallsDataAccess()
        {
            var environment = new Environment();

            var ids = new List<GlobalHistoryId>();
            environment.useCase.GetValuesFromClassicProcessHeader(ids);

            Assert.AreSame(ids, environment.mocks.classicProcessTestData.GetValuesFromClassicProcessHeaderGlobalHistoryIds);
        }

        [Test]
        public void GetValuesFromClassicProcessHeaderReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<ClassicProcessTestValue>();
            environment.mocks.classicProcessTestData.GetValuesFromClassicProcessHeaderReturnValue = entities;

            Assert.AreSame(entities, environment.useCase.GetValuesFromClassicProcessHeader(new List<GlobalHistoryId>()));
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    classicChkTestData = new ClassicChkTestDataMock();
                    classicMfuTestData = new ClassicMfuTestDataMock();
                    classicTestData = new ClassicTestDataAccessMock();
                    classicProcessTestData = new ClassicProcessTestDataMock();
                }

                public readonly ClassicChkTestDataMock classicChkTestData;
                public readonly ClassicMfuTestDataMock classicMfuTestData;
                public readonly ClassicTestDataAccessMock classicTestData;
                public readonly ClassicProcessTestDataMock classicProcessTestData;
            }

            public Environment()
            {
                mocks = new Mocks();
                useCase = new ClassicTestUseCase(mocks.classicChkTestData, mocks.classicMfuTestData, mocks.classicTestData, mocks.classicProcessTestData);
            }

            public readonly Mocks mocks;
            public readonly ClassicTestUseCase useCase;
        }
    }
}
