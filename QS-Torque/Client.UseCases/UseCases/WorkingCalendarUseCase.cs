using Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.UseCases.UseCases;
using log4net;
using Client.Core.Diffs;

namespace Core.UseCases
{
    public interface IWorkingCalendarErrorHandler
    {
        void WorkingCalendarError(WorkingCalendarEntry problematicEntry);
    }

    public interface IWorkingCalendarGui
    {
        void ShowCalendarEntries(List<WorkingCalendarEntry> entries);
        void AddWorkingCalendarEntry(WorkingCalendarEntry newEntry);
        void RemoveWorkingCalendarEntry(WorkingCalendarEntry oldEntry);
        void LoadWeekendSettings(WorkingCalendar workingCalendar);
    }

    public interface IWorkingCalendarData
    {
        List<WorkingCalendarEntry> LoadWorkingCalendarEntriesForWorkingCalendarId(WorkingCalendarId id);
        void AddWorkingCalendarEntry(WorkingCalendarEntry newEntry, WorkingCalendarId calendarId);
        void RemoveWorkingCalendarEntry(WorkingCalendarEntry oldEntry);
        WorkingCalendar LoadWeekendSettings();
        void SetWeekendSettings(WorkingCalendarDiff diff);
    }

    public interface IWorkingCalendarUseCase
    {
        void LoadCalendarEntries(IWorkingCalendarErrorHandler errorHandler);
        void AddWorkingCalendarEntry(WorkingCalendarEntry newEntry, IWorkingCalendarErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler, WorkingCalendarEntry preexisting = null);
        void RemoveWorkingCalendarEntry(WorkingCalendarEntry oldEntry, IWorkingCalendarErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler, bool withTestDateCalculation = true);
        void LoadWeekendSettings(IWorkingCalendarErrorHandler errorHandler);
        void SetWeekendSettings(WorkingCalendarDiff diff, IWorkingCalendarErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler);
    }


    public class WorkingCalendarUseCase : IWorkingCalendarUseCase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(WorkingCalendarUseCase));

        private IWorkingCalendarGui _gui;
        private IWorkingCalendarData _data;
        private ITestDateCalculationUseCase _testDateCalculationUseCase;
        private ILocationToolAssignmentUseCase _locationToolAssignmentUseCase;
        private IProcessControlUseCase _processControlUseCase;
        private ISessionInformationUserGetter _userGetter;

        private WorkingCalendar _previouslyLoadedWorkingCalendar;

        public WorkingCalendarUseCase(IWorkingCalendarGui gui, IWorkingCalendarData data, ITestDateCalculationUseCase testDateCalculationUseCase, ILocationToolAssignmentUseCase locationToolAssignmentUseCase, IProcessControlUseCase processControlUseCase, ISessionInformationUserGetter userGetter)
        {
            _gui = gui;
            _data = data;
            _testDateCalculationUseCase = testDateCalculationUseCase;
            _locationToolAssignmentUseCase = locationToolAssignmentUseCase;
            _processControlUseCase = processControlUseCase;
            _userGetter = userGetter;
        }

        public void LoadCalendarEntries(IWorkingCalendarErrorHandler errorHandler)
        {
            try
            {
                _gui.ShowCalendarEntries(_data.LoadWorkingCalendarEntriesForWorkingCalendarId(
                    _previouslyLoadedWorkingCalendar != null ? _previouslyLoadedWorkingCalendar.Id : new WorkingCalendarId(1)));
            }
            catch (Exception e)
            {
                _log.Error("Error in LoadCalendarEntries()", e);
                errorHandler.WorkingCalendarError(null);
            }
        }

        public void AddWorkingCalendarEntry(WorkingCalendarEntry newEntry, IWorkingCalendarErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler, WorkingCalendarEntry preexisting = null)
        {
            try
            {
                if (preexisting != null)
                {
                    RemoveWorkingCalendarEntry(preexisting, errorHandler, processControlErrorHandler, false);
                }
                _data.AddWorkingCalendarEntry(newEntry, _previouslyLoadedWorkingCalendar != null ? _previouslyLoadedWorkingCalendar.Id : new WorkingCalendarId(1) );
                _gui.AddWorkingCalendarEntry(newEntry);
                
                if(FeatureToggles.FeatureToggles.TestDateCalculation)
                {
                    _testDateCalculationUseCase.CalculateToolTestDateForAllLocationToolAssignments();
                    _locationToolAssignmentUseCase.LoadLocationToolAssignments();
                    if (FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
                    {
                        _testDateCalculationUseCase.CalculateProcessControlDateForAllProcessControlConditions();
                        _processControlUseCase.LoadProcessControlConditions(processControlErrorHandler);
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("Error in AddWorkingCalendarEntry()", e);
                errorHandler.WorkingCalendarError(newEntry);
            }
        }

        public void RemoveWorkingCalendarEntry(WorkingCalendarEntry oldEntry, IWorkingCalendarErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler, bool withTestDateCalculation = true)
        {
            try
            {
                _data.RemoveWorkingCalendarEntry(oldEntry);
                _gui.RemoveWorkingCalendarEntry(oldEntry);

                if (FeatureToggles.FeatureToggles.TestDateCalculation && withTestDateCalculation)
                {
                    _testDateCalculationUseCase.CalculateToolTestDateForAllLocationToolAssignments();
                    _locationToolAssignmentUseCase.LoadLocationToolAssignments();
                    if (FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
                    {
                        _testDateCalculationUseCase.CalculateProcessControlDateForAllProcessControlConditions();
                        _processControlUseCase.LoadProcessControlConditions(processControlErrorHandler);
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("Error in RemoveWorkingCalendarEntry()", e);
                errorHandler.WorkingCalendarError(oldEntry);
            }
        }

        public void LoadWeekendSettings(IWorkingCalendarErrorHandler errorHandler)
        {
            try
            {
                var result = _data.LoadWeekendSettings();
                _previouslyLoadedWorkingCalendar = result;
                _gui.LoadWeekendSettings(result);
            }
            catch (Exception e)
            {
                _log.Error("Error in LoadWeekendSettings()", e);
                errorHandler.WorkingCalendarError(null);
            }
        }

        public void SetWeekendSettings(WorkingCalendarDiff diff, IWorkingCalendarErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler)
        {
            try
            {
                 diff.User = _userGetter.GetCurrentUser();
                _data.SetWeekendSettings(diff);

                if (FeatureToggles.FeatureToggles.TestDateCalculation)
                {
                    _testDateCalculationUseCase.CalculateToolTestDateForAllLocationToolAssignments();
                    _locationToolAssignmentUseCase.LoadLocationToolAssignments();
                    if (FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
                    {
                        _testDateCalculationUseCase.CalculateProcessControlDateForAllProcessControlConditions();
                        _processControlUseCase.LoadProcessControlConditions(processControlErrorHandler);
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("Error in SetWeekendSettings()", e);
                errorHandler.WorkingCalendarError(null);
            }
        }
    }


    public class WorkingCalendarHumbleAsyncRunner : IWorkingCalendarUseCase
    {
        private IWorkingCalendarUseCase _real;

        public WorkingCalendarHumbleAsyncRunner(IWorkingCalendarUseCase real)
        {
            _real = real;
        }

        public void LoadCalendarEntries(IWorkingCalendarErrorHandler errorHandler)
        {
            Task.Run(() => _real.LoadCalendarEntries(errorHandler));
        }

        public void AddWorkingCalendarEntry(WorkingCalendarEntry newEntry, IWorkingCalendarErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler, WorkingCalendarEntry preexisting = null)
        {
            Task.Run(() => _real.AddWorkingCalendarEntry(newEntry, errorHandler, processControlErrorHandler, preexisting));
        }

        public void RemoveWorkingCalendarEntry(WorkingCalendarEntry oldEntry, IWorkingCalendarErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler, bool withTestDateCalculation = true)
        {
            Task.Run(() => _real.RemoveWorkingCalendarEntry(oldEntry, errorHandler, processControlErrorHandler, withTestDateCalculation));
        }

        public void LoadWeekendSettings(IWorkingCalendarErrorHandler errorHandler)
        {
            Task.Run(() => _real.LoadWeekendSettings(errorHandler));
        }

        public void SetWeekendSettings(WorkingCalendarDiff diff, IWorkingCalendarErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler)
        {
            Task.Run(() => _real.SetWeekendSettings(diff, errorHandler, processControlErrorHandler));
        }
    }
}
