using Client.Core.Diffs;
using Client.Core.Entities;
using Client.UseCases.UseCases;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.TestHelper.Mock
{
    public class ProcessControlUseCaseMock : IProcessControlUseCase
    {
        public ProcessControlCondition RemoveProcessControlConditionArgument { get; set; }
        public IProcessControlErrorGui RemoveProcessControlConditionErrorHandlerArgument { get; set; }
        public Location LoadProcessControlConditionForLocationParameter { get; set; }
        public IProcessControlErrorGui LoadProcessControlConditionForLocationErrorHandler { get; set; }
        public IProcessControlSaveGuiShower SaveProcessControlConditionParameterDiffSaveGuiShower { get; set; }
        public IProcessControlErrorGui SaveProcessControlConditionParameterErrorHandler { get; set; }
        public List<ProcessControlConditionDiff> SaveProcessControlConditionParameterDiff { get; set; }
        public IProcessControlErrorGui UpdateProcessControlConditionParameterErrorHandler { get; set; }
        public List<ProcessControlConditionDiff> UpdateProcessControlConditionParameterDiff { get; set; }
        public IProcessControlErrorGui LoadProcessControlConditionsErrorHandler { get; set; }

        public void RemoveProcessControlCondition(ProcessControlCondition processControlCondition,
            IProcessControlErrorGui errorHandler)
        {
            RemoveProcessControlConditionArgument = processControlCondition;
            RemoveProcessControlConditionErrorHandlerArgument = errorHandler;
        }

        public void LoadProcessControlConditionForLocation(Location location, IProcessControlErrorGui errorHandler)
        {
            LoadProcessControlConditionForLocationParameter = location;
            LoadProcessControlConditionForLocationErrorHandler = errorHandler;
        }

        public void AddProcessControlCondition(ProcessControlCondition processControlCondition, IProcessControlErrorGui errorHandler)
        {
            throw new System.NotImplementedException();
        }

        public void SaveProcessControlCondition(List<ProcessControlConditionDiff> diff, IProcessControlErrorGui errorHandler,
            IProcessControlSaveGuiShower saveGuiShower)
        {
            SaveProcessControlConditionParameterDiff = diff;
            SaveProcessControlConditionParameterErrorHandler = errorHandler;
            SaveProcessControlConditionParameterDiffSaveGuiShower = saveGuiShower;
        }

        public void UpdateProcessControlCondition(List<ProcessControlConditionDiff> diff, IProcessControlErrorGui errorHandler)
        {
            UpdateProcessControlConditionParameterDiff = diff;
            UpdateProcessControlConditionParameterErrorHandler = errorHandler;
        }

        public void LoadProcessControlConditions(IProcessControlErrorGui errorHandler)
        {
            LoadProcessControlConditionsErrorHandler = errorHandler;
        }
    }
}
