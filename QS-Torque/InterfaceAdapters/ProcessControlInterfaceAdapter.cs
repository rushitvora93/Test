using System;
using Core.UseCases;
using System.ComponentModel;
using Client.Core.Entities;
using Client.UseCases.UseCases;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core.Entities;

namespace InterfaceAdapters
{
    public interface IProcessControlInterface : INotifyPropertyChanged
    {
        ProcessControlConditionHumbleModel SelectedProcessControl { get; set; }
        ProcessControlConditionHumbleModel SelectedProcessControlWithoutChanges { get; set; }
        ObservableCollection<ProcessControlConditionHumbleModel> ProcessControlConditions { get; set; }
        ObservableCollection<ProcessControlConditionHumbleModel> SelectedProcessControlConditions { get; set; }
        event EventHandler<bool> ShowLoadingControlRequest;
    }

    public class ProcessControlInterfaceAdapter : BindableBase, IProcessControlGui, IProcessControlInterface, ITestLevelSetAssignmentGuiForProcessControl
    {
        private readonly ILocalizationWrapper _localization;
        public event EventHandler<bool> ShowLoadingControlRequest;

        public ProcessControlInterfaceAdapter(ILocalizationWrapper localization)
        {
            _localization = localization;
        }

        public void ShowProcessControlConditionForLocation(ProcessControlCondition processControlCondition)
        {
            SelectedProcessControl = ProcessControlConditionHumbleModel.GetModelFor(processControlCondition, _localization);
        }

        public void RemoveProcessControlCondition(ProcessControlCondition processControlCondition)
        {
            if (SelectedProcessControl != null && SelectedProcessControl.Entity.EqualsById(processControlCondition))
            {
                SelectedProcessControl = null;
            }
            ShowLoadingControlRequest?.Invoke(this, false);
        }

        public void AddProcessControlCondition(ProcessControlCondition processControlCondition)
        {
            SelectedProcessControl = ProcessControlConditionHumbleModel.GetModelFor(processControlCondition, _localization);
        }

        public void UpdateProcessControlCondition(List<ProcessControlCondition> processControlConditions)
        {
            foreach (var processControlCondition in processControlConditions)
            {
                if (SelectedProcessControl?.Entity.EqualsById(processControlCondition) ?? false)
                {
                    SelectedProcessControl.UpdateWith(processControlCondition);
                    SelectedProcessControlWithoutChanges = SelectedProcessControl?.CopyDeep();
                } 
            }

            ShowLoadingControlRequest?.Invoke(this, false);
        }

        public void ShowProcessControlConditions(List<ProcessControlCondition> processControlConditions)
        {
            ProcessControlConditions = new ObservableCollection<ProcessControlConditionHumbleModel>(processControlConditions.Select(x => new ProcessControlConditionHumbleModel(x, _localization)));
        }

        public void AssignTestLevelSetToProcessControlConditions(TestLevelSet testLevelSet, List<ProcessControlConditionId> processControlConditionIds)
        {
            foreach (var condition in ProcessControlConditions)
            {
                if(processControlConditionIds.Any(x => x.ToLong() == condition.Id))
                {
                    condition.TestLevelSet = TestLevelSetModel.GetModelFor(testLevelSet);
                    condition.TestLevelNumber = 1;
                }
            }
        }

        public void RemoveTestLevelSetAssignmentFor(List<ProcessControlConditionId> ids)
        {
            foreach (var id in ids)
            {
                var toRemove = ProcessControlConditions
                    .Where(x => x.Entity.Id.Equals(id))
                    .ToList();
                foreach (var item in toRemove)
                {
                    item.TestLevelSet = null;
                }
            }
        }

        private ProcessControlConditionHumbleModel _selectedProcessControl;
        public ProcessControlConditionHumbleModel SelectedProcessControl
        {
            get => _selectedProcessControl;
            set
            {
                _selectedProcessControl = value;
                RaisePropertyChanged();
                SelectedProcessControlWithoutChanges = _selectedProcessControl?.CopyDeep();
            }
        }

        private ProcessControlConditionHumbleModel _selectedProcessControlWithoutChanges;
        public ProcessControlConditionHumbleModel SelectedProcessControlWithoutChanges
        {
            get => _selectedProcessControlWithoutChanges;
            set
            {
                _selectedProcessControlWithoutChanges = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ProcessControlConditionHumbleModel> _processControlConditions;
        public ObservableCollection<ProcessControlConditionHumbleModel> ProcessControlConditions
        {
            get => _processControlConditions;
            set
            {
                _processControlConditions = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ProcessControlConditionHumbleModel> _selectedProcessControlConditions;
        public ObservableCollection<ProcessControlConditionHumbleModel> SelectedProcessControlConditions
        {
            get => _selectedProcessControlConditions;
            set
            {
                _selectedProcessControlConditions = value;
                RaisePropertyChanged();
            }
        }
    }
}
