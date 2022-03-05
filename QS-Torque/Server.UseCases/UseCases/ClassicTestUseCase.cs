using System;
using System.Collections.Generic;
using Server.Core.Entities;
using Server.Core.Enums;

namespace Server.UseCases.UseCases
{
    public interface IClassicChkTestData
    {
        List<(DateTime, Shift?)> GetTestsForTimePeriod(LocationToolAssignmentId locToolId, DateTime startPeriodDate, Shift? startPeriodShift, DateTime endPeriodDate, Shift? endPeriodShift, TimeSpan changeOfDay);
        List<ClassicChkTest> GetClassicChkHeaderFromTool(long powToolId, long? locationId);
        List<ClassicChkTestValue> GetValuesFromClassicChkHeader(List<GlobalHistoryId> ids);
        void InsertClassicChkTests(List<ClassicChkTest> tests);
        void Commit();
    }
    public interface IClassicMfuTestData
    {
        List<(DateTime, Shift?)> GetTestsForTimePeriod(LocationToolAssignmentId locToolId, DateTime startPeriodDate, Shift? startPeriodShift, DateTime endPeriodDate, Shift? endPeriodShift, TimeSpan changeOfDay);
        List<ClassicMfuTest> GetClassicMfuHeaderFromTool(long powToolId, long? locationId);
        List<ClassicMfuTestValue> GetValuesFromClassicMfuHeader(List<GlobalHistoryId> ids);
        void InsertClassicMfuTests(List<ClassicMfuTest> tests);
        void Commit();
    }

    public interface IClassicProcessTestData
    {
        List<ClassicProcessTest> GetClassicProcessHeaderFromLocation(LocationId locationId);
        List<ClassicProcessTestValue> GetValuesFromClassicProcessHeader(List<GlobalHistoryId> ids);
        List<(DateTime, Shift?)> GetTestsForTimePeriod(ProcessControlConditionId id, DateTime startPeriodDate, Shift? startPeriodShift, DateTime endPeriodDate, Shift? endPeriodShift, TimeSpan changeOfDay);
        void Commit();
    }

    public interface IClassicTestDataAccess
    {
        Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)> GetToolsFromLocationTests(LocationId locationId);
    }

    public interface IClassicTestUseCase
    {
        List<ClassicChkTest> GetClassicChkHeaderFromTool(long powToolId, long? locationId);
        List<ClassicMfuTest> GetClassicMfuHeaderFromTool(long powToolId, long? locationId);
        List<ClassicChkTestValue> GetValuesFromClassicChkHeader(List<GlobalHistoryId> ids);
        List<ClassicMfuTestValue> GetValuesFromClassicMfuHeader(List<GlobalHistoryId> ids);
        Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)> GetToolsFromLocationTests(LocationId locationId);
        List<ClassicProcessTest> GetClassicProcessHeaderFromLocation(LocationId locationId);
        List<ClassicProcessTestValue> GetValuesFromClassicProcessHeader(List<GlobalHistoryId> ids);
    }

    public class ClassicTestUseCase : IClassicTestUseCase
    {
        private readonly IClassicChkTestData _classicChkTestData;
        private readonly IClassicMfuTestData _classicMfuTestData;
        private readonly IClassicTestDataAccess _classicTestDataAccess;
        private readonly IClassicProcessTestData _classicProcessTestDataAccess;

        public ClassicTestUseCase(IClassicChkTestData classicChkTestData, IClassicMfuTestData classicMfuTestData,
            IClassicTestDataAccess classicTestDataAccess, IClassicProcessTestData classicProcessTestDataAccess)
        {
            _classicChkTestData = classicChkTestData;
            _classicMfuTestData = classicMfuTestData;
            _classicTestDataAccess = classicTestDataAccess;
            _classicProcessTestDataAccess = classicProcessTestDataAccess;
        }


        public List<ClassicChkTest> GetClassicChkHeaderFromTool(long powToolId, long? locationId)
        {
            return _classicChkTestData.GetClassicChkHeaderFromTool(powToolId, locationId);
        }

        public List<ClassicMfuTest> GetClassicMfuHeaderFromTool(long powToolId, long? locationId)
        {
            return _classicMfuTestData.GetClassicMfuHeaderFromTool(powToolId, locationId);
        }

        public List<ClassicChkTestValue> GetValuesFromClassicChkHeader(List<GlobalHistoryId> ids)
        {
            return _classicChkTestData.GetValuesFromClassicChkHeader(ids);
        }

        public List<ClassicMfuTestValue> GetValuesFromClassicMfuHeader(List<GlobalHistoryId> ids)
        {
            return _classicMfuTestData.GetValuesFromClassicMfuHeader(ids);
        }

        public Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)> GetToolsFromLocationTests(LocationId locationId)
        {
            return _classicTestDataAccess.GetToolsFromLocationTests(locationId);
        }

        public List<ClassicProcessTest> GetClassicProcessHeaderFromLocation(LocationId locationId)
        {
            return _classicProcessTestDataAccess.GetClassicProcessHeaderFromLocation(locationId);
        }

        public List<ClassicProcessTestValue> GetValuesFromClassicProcessHeader(List<GlobalHistoryId> ids)
        {
            return _classicProcessTestDataAccess.GetValuesFromClassicProcessHeader(ids);
        }
    }
}
