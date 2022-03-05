using System;
using System.Collections.Generic;
using System.Linq;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Enums;

namespace Server.UseCases.UseCases
{
    public interface IProcessControlUseCase
    {
        void UpdateProcessControlConditionsWithHistory(List<ProcessControlConditionDiff> processControlConditionDiffs);
        ProcessControlCondition LoadProcessControlConditionForLocation(LocationId locationId);
        List<ProcessControlCondition> InsertProcessControlConditionsWithHistory(List<ProcessControlConditionDiff> diffs, bool returnList);
        List<ProcessControlCondition> LoadProcessControlConditions();
    }

    public interface IProcessControlDataAccess
    {
        List<ProcessControlCondition> UpdateProcessControlConditionsWithHistory(List<ProcessControlConditionDiff> processControlConditionDiffs);
        ProcessControlCondition LoadProcessControlConditionForLocation(LocationId locationId);
        ProcessControlCondition GetProcessControlConditionById(ProcessControlConditionId id);
        List<ProcessControlCondition> InsertProcessControlConditionsWithHistory(List<ProcessControlConditionDiff> diffs, bool returnList);
        List<ProcessControlCondition> LoadProcessControlConditions();
        List<ProcessControlConditionId> GetProcessControlConditionIdsForTestLevelSet(TestLevelSetId id);
        void SaveNextTestDatesFor(ProcessControlConditionId id, DateTime? nextTestDate, Shift? nextTestShift, DateTime? endOfLastTestPeriod, Shift? endOfLastTestPeriodShift);
        void Commit();
    }

    public interface IProcessControlTechDataAccess
    {
        void UpdateProcessControlTechsWithHistory(List<ProcessControlTechDiff> processControlTechDiffs);
        List<ProcessControlTech> InsertProcessControlTechsWithHistory(List<ProcessControlTechDiff> diffs, bool returnList);
        void Commit();
    }


    public class ProcessControlUseCase : IProcessControlUseCase
    {
        private readonly IProcessControlDataAccess _processControlDataAccess;
        private readonly IProcessControlTechDataAccess _processControlTechDataAccess;


        public ProcessControlUseCase(IProcessControlDataAccess processControlDataAccess, IProcessControlTechDataAccess processControlTechDataAccess)
        {
            _processControlDataAccess = processControlDataAccess;
            _processControlTechDataAccess = processControlTechDataAccess;
        }

        public void UpdateProcessControlConditionsWithHistory(List<ProcessControlConditionDiff> processControlConditionDiffs)
        {
            _processControlDataAccess.UpdateProcessControlConditionsWithHistory(processControlConditionDiffs);
            var processControlTechDiffs = new List<ProcessControlTechDiff>();
            foreach (var diff in processControlConditionDiffs)
            {
                if (diff.GetOldProcessControlCondition()?.ProcessControlTech != null && diff.GetNewProcessControlCondition()?.ProcessControlTech != null)
                {
                    var processControlTechDiff = new ProcessControlTechDiff(diff.GetUser(), diff.GetComment(),
                                diff.GetOldProcessControlCondition()?.ProcessControlTech,
                                diff.GetNewProcessControlCondition()?.ProcessControlTech);

                    processControlTechDiffs.Add(processControlTechDiff); 
                }
            }

            _processControlTechDataAccess.UpdateProcessControlTechsWithHistory(processControlTechDiffs);
            _processControlDataAccess.Commit();
        }

        public ProcessControlCondition LoadProcessControlConditionForLocation(LocationId locationId)
        {
            return _processControlDataAccess.LoadProcessControlConditionForLocation(locationId);
        }

        public List<ProcessControlCondition> InsertProcessControlConditionsWithHistory(List<ProcessControlConditionDiff> diffs, bool returnList)
        {
            var processControlConditions =_processControlDataAccess.InsertProcessControlConditionsWithHistory(diffs, returnList);

            var processControlTechDiffs = new List<ProcessControlTechDiff>();
            foreach (var diff in diffs)
            {
                var processControlTechDiff = new ProcessControlTechDiff(diff.GetUser(), diff.GetComment(),
                    diff.GetOldProcessControlCondition()?.ProcessControlTech,
                    diff.GetNewProcessControlCondition()?.ProcessControlTech);

                processControlTechDiffs.Add(processControlTechDiff);
            }

            _processControlTechDataAccess.InsertProcessControlTechsWithHistory(processControlTechDiffs, returnList);

            _processControlDataAccess.Commit();
            return processControlConditions;
        }

        public List<ProcessControlCondition> LoadProcessControlConditions()
        {
            return _processControlDataAccess.LoadProcessControlConditions();
        }
    }
}
