using Client.Core.Diffs;
using Client.Core.Entities;
using Client.UseCases.UseCases;
using Core.Entities;
using System;
using System.Collections.Generic;

namespace Client.TestHelper.Mock
{
    public class ProcessControlDataMock : IProcessControlData
    {
        public ProcessControlCondition RemoveProcessControlConditionParameterCondition { get; set; }
        public User RemoveProcessControlConditionParameterUser { get; set; }
        public ProcessControlCondition LoadProcessControlConditionForLocationReturnValue { get; set; }
        public List<ProcessControlCondition> LoadProcessControlConditionForLocationReturnValues;
        private int LoadProcessControlConditionReturnValuesIndex = 0;
        public Location LoadProcessControlConditionForLocationParameter { get; set; }
        public List<Location> LoadProcessControlConditionForLocationParameterList = new List<Location>();
        public bool LoadProcessControlConditionForLocationThrowsException { get; set; }
        public ProcessControlCondition AddProcessControlConditionReturnValue { get; set; }
        public User AddProcessControlConditionParameterUser { get; set; }
        public ProcessControlCondition AddProcessControlConditionParameterCondition { get; set; }
        public bool AddProcessControlConditionThrowsError { get; set; }
        public List<ProcessControlConditionDiff> SaveProcessControlConditionParameter { get; set; }
        public bool SaveProcessControlConditionThrowsError { get; set; }
        public bool RemoveProcessControlConditionThrowsError { get; set; }
        public bool LoadProcessControlConditionsCalled { get; set; }
        public List<ProcessControlCondition> LoadProcessControlConditionsReturnValue { get; set; }

        public void RemoveProcessControlCondition(ProcessControlCondition processControlCondition, User byUser)
        {
            if (RemoveProcessControlConditionThrowsError)
            {
                throw new Exception("");
            }
            RemoveProcessControlConditionParameterCondition = processControlCondition;
            RemoveProcessControlConditionParameterUser = byUser;
        }

        public ProcessControlCondition LoadProcessControlConditionForLocation(Location location)
        {
            if (LoadProcessControlConditionForLocationThrowsException)
            {
                throw new Exception("");
            }
            LoadProcessControlConditionForLocationParameter = location;
            LoadProcessControlConditionForLocationParameterList.Add(location);
            if(LoadProcessControlConditionForLocationReturnValue != null)
            {
                return LoadProcessControlConditionForLocationReturnValue;
            }
            if(LoadProcessControlConditionForLocationReturnValues != null)
            {
                return LoadProcessControlConditionForLocationReturnValues[LoadProcessControlConditionReturnValuesIndex++];
            }
            return null;
        }

        public ProcessControlCondition AddProcessControlCondition(ProcessControlCondition processControlCondition, User byUser)
        {
            if (AddProcessControlConditionThrowsError)
            {
                throw new Exception();
            }
            AddProcessControlConditionParameterCondition = processControlCondition;
            AddProcessControlConditionParameterUser = byUser;
            return AddProcessControlConditionReturnValue;
        }

        public void SaveProcessControlCondition(List<ProcessControlConditionDiff> diffs)
        {
            if (SaveProcessControlConditionThrowsError)
            {
                throw new Exception();
            }
            SaveProcessControlConditionParameter = diffs;
        }

        public List<ProcessControlCondition> LoadProcessControlConditions()
        {
            if (LoadProcessControlConditionForLocationThrowsException)
            {
                throw new Exception("");
            }

            LoadProcessControlConditionsCalled = true;
            return LoadProcessControlConditionsReturnValue;
        }

        public void RestoreProcessControlCondition(ProcessControlCondition processControlCondition, User byUser)
        {
            throw new NotImplementedException();
        }
    }
}
