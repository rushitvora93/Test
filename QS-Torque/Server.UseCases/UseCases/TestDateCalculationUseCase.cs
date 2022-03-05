using System;
using System.Collections.Generic;
using System.Linq;
using Server.Core.Entities;
using Server.Core.Enums;

namespace Server.UseCases.UseCases
{
    public interface ITestDateCalculationUseCase
    {
        void CalculateToolTestDateFor(List<LocationToolAssignmentId> ids);
        void CalculateToolTestDateForTestLevelSet(TestLevelSetId id);
        void CalculateToolTestDateForAllLocationToolAssignments();
        void CalculateProcessControlDateFor(List<ProcessControlConditionId> ids);
        void CalculateProcessControlDateForTestLevelSet(TestLevelSetId id);
        void CalculateProcessControlDateForAllProcessControlConditions();
    }


    public class TestDateCalculationUseCase : ITestDateCalculationUseCase
    {
        private IShiftManagementData _shiftManagementData;
        private IWorkingCalendarData _workingCalendarData;
        private ILocationToolAssignmentData _locationToolAssignmentData;
        private IProcessControlDataAccess _processControlData;
        private IClassicMfuTestData _classicMfuTestData;
        private IClassicChkTestData _classicChkTestData;
        private IClassicProcessTestData _classicProcessTestData;

        public TestDateCalculationUseCase(IShiftManagementData shiftManagementData, 
            IWorkingCalendarData workingCalendarData, 
            ILocationToolAssignmentData locationToolAssignmentData,
            IProcessControlDataAccess processControlData,
            IClassicMfuTestData classicMfuTestData, 
            IClassicChkTestData classicChkTestData,
            IClassicProcessTestData classicProcessTestData)
        {
            _shiftManagementData = shiftManagementData;
            _workingCalendarData = workingCalendarData;
            _locationToolAssignmentData = locationToolAssignmentData;
            _processControlData = processControlData;
            _classicMfuTestData = classicMfuTestData;
            _classicChkTestData = classicChkTestData;
            _classicProcessTestData = classicProcessTestData;
        }
        
        public void CalculateToolTestDateFor(List<LocationToolAssignmentId> ids)
        {
            var shiftManagement = _shiftManagementData.GetShiftManagement();
            CalculateToolTestDateFor(ids, CreateTestDateCalculator(shiftManagement), shiftManagement);
        }

        public void CalculateToolTestDateForTestLevelSet(TestLevelSetId id)
        {
            CalculateToolTestDateFor(_locationToolAssignmentData.GetLocationToolAssignmentIdsForTestLevelSet(id));
        }

        public void CalculateToolTestDateForAllLocationToolAssignments()
        {
            CalculateToolTestDateFor(_locationToolAssignmentData.LoadLocationToolAssignments().Select(x => x.Id).ToList());
        }

        public TestDateCalculator CreateTestDateCalculator(ShiftManagement shiftManagement)
        {
            var workingCalendar = _workingCalendarData.GetWorkingCalendar();
            var workingCalendarEntries = _workingCalendarData.GetWorkingCalendarEntriesForWorkingCalendarId(workingCalendar.Id);
            return new TestDateCalculator(new TestIntervalAdder(workingCalendar, workingCalendarEntries, shiftManagement), shiftManagement);
        }

        public void CalculateToolTestDateFor(List<LocationToolAssignmentId> ids, ITestDateCalculator calculator, ShiftManagement shiftManagement)
        {
            foreach (var id in ids)
            {
                CalculateToolTestDateFor(id, calculator, shiftManagement);
            }
            _locationToolAssignmentData.Commit();
        }

        public void CalculateToolTestDateFor(LocationToolAssignmentId id, ITestDateCalculator calculator, ShiftManagement shiftManagement)
        {
            var locTool = _locationToolAssignmentData.GetLocationToolAssignmentById(id);

            if (locTool == null) { return; }

            Nullable<(DateTime resultDate, Shift? resultShift, DateTime? newEndOfLastTestPeriod, Shift? newEndOfLastTestPeriodShift)> mfuResult = null;
            Nullable<(DateTime resultDate, Shift? resultShift, DateTime? newEndOfLastTestPeriod, Shift? newEndOfLastTestPeriodShift)> chkResult = null;

            if (locTool.TestLevelSetMfu != null && locTool.TestOperationActiveMfu)
            {
                TestLevel mfuTestLevel;

                if (locTool.TestLevelNumberMfu == 2)
                {
                    mfuTestLevel = locTool.TestLevelSetMfu.TestLevel2;
                }
                else if (locTool.TestLevelNumberMfu == 3)
                {
                    mfuTestLevel = locTool.TestLevelSetMfu.TestLevel3;
                }
                else
                {
                    mfuTestLevel = locTool.TestLevelSetMfu.TestLevel1;
                }

                mfuResult = calculator.CalculateTestDate(mfuTestLevel,
                    locTool.EndOfLastTestPeriodMfu,
                    locTool.EndOfLastTestPeriodShiftMfu,
                    locTool.StartDateMfu ?? DateTime.Today,
                    (sd, ss, ed, es) =>
                        _classicMfuTestData.GetTestsForTimePeriod(id, sd, ss, ed, es, shiftManagement.ChangeOfDay)); 
            }

            if (locTool.TestLevelSetChk != null && locTool.TestOperationActiveChk)
            {
                TestLevel chkTestLevel;

                if (locTool.TestLevelNumberChk == 2)
                {
                    chkTestLevel = locTool.TestLevelSetChk.TestLevel2;
                }
                else if (locTool.TestLevelNumberChk == 3)
                {
                    chkTestLevel = locTool.TestLevelSetChk.TestLevel3;
                }
                else
                {
                    chkTestLevel = locTool.TestLevelSetChk.TestLevel1;
                }

                chkResult = calculator.CalculateTestDate(chkTestLevel,
                        locTool.EndOfLastTestPeriodChk,
                        locTool.EndOfLastTestPeriodShiftChk,
                        locTool.StartDateChk ?? DateTime.Today,
                        (sd, ss, ed, es) =>
                            _classicChkTestData.GetTestsForTimePeriod(id, sd, ss, ed, es, shiftManagement.ChangeOfDay)); 
            }

            _locationToolAssignmentData.SaveNextTestDatesFor(id, mfuResult?.resultDate, mfuResult?.resultShift, chkResult?.resultDate, chkResult?.resultShift,
                mfuResult?.newEndOfLastTestPeriod, mfuResult?.newEndOfLastTestPeriodShift, chkResult?.newEndOfLastTestPeriod, chkResult?.newEndOfLastTestPeriodShift);
        }


        public void CalculateProcessControlDateFor(List<ProcessControlConditionId> ids)
        {
            var shiftManagement = _shiftManagementData.GetShiftManagement();
            CalculateProcessControlDateFor(ids, CreateTestDateCalculator(shiftManagement), shiftManagement);
        }

        public void CalculateProcessControlDateForTestLevelSet(TestLevelSetId id)
        {
            CalculateProcessControlDateFor(_processControlData.GetProcessControlConditionIdsForTestLevelSet(id));
        }

        public void CalculateProcessControlDateForAllProcessControlConditions()
        {
            CalculateProcessControlDateFor(_processControlData.LoadProcessControlConditions().Select(x => x.Id).ToList());
        }

        public void CalculateProcessControlDateFor(List<ProcessControlConditionId> ids, ITestDateCalculator calculator, ShiftManagement shiftManagement)
        {
            foreach (var id in ids)
            {
                CalculateProcessControlDateFor(id, calculator, shiftManagement);
            }

            _processControlData.Commit();
        }

        public void CalculateProcessControlDateFor(ProcessControlConditionId id, ITestDateCalculator calculator, ShiftManagement shiftManagement)
        {
            var processControl = _processControlData.GetProcessControlConditionById(id);

            if (processControl == null) { return; }

            if (processControl.TestLevelSet != null && processControl.TestOperationActive)
            {
                TestLevel testLevel;

                if (processControl.TestLevelNumber == 2)
                {
                    testLevel = processControl.TestLevelSet.TestLevel2;
                }
                else if (processControl.TestLevelNumber == 3)
                {
                    testLevel = processControl.TestLevelSet.TestLevel3;
                }
                else
                {
                    testLevel = processControl.TestLevelSet.TestLevel1;
                }

                var result = calculator.CalculateTestDate(testLevel,
                    processControl.EndOfLastTestPeriod,
                    processControl.EndOfLastTestPeriodShift,
                    processControl.StartDate ?? DateTime.Today,
                    (sd, ss, ed, es) =>
                        _classicProcessTestData.GetTestsForTimePeriod(id, sd, ss, ed, es, shiftManagement.ChangeOfDay));

                _processControlData.SaveNextTestDatesFor(id, result.resultDate, result.resultShift,
                    result.newEndOfLastTestPeriod, result.newEndOfLastTestPeriodShift); 
            }
        }
    }
}
