using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Core.Diffs;
using Client.Core.Entities;
using Core;
using Core.Entities;
using Core.UseCases;
using log4net;

namespace Client.UseCases.UseCases
{
    public interface IProcessControlGui
    {
        void ShowProcessControlConditionForLocation(ProcessControlCondition processControlCondition);
        void ShowProcessControlConditions(List<ProcessControlCondition> processControlConditions);
        void RemoveProcessControlCondition(ProcessControlCondition processControlCondition);
        void AddProcessControlCondition(ProcessControlCondition processControlCondition);
        void UpdateProcessControlCondition(List<ProcessControlCondition> processControlCondition);
    }

    public interface IProcessControlErrorGui
    {
        void ShowProblemLoadingLocationProcessControlCondition();
        void ShowProblemRemoveProcessControlCondition();
        void ShowProblemSavingProcessControlCondition();
    }

    public interface IProcessControlSaveGuiShower
    {
        void SaveProcessControl(List<ProcessControlConditionDiff> diffs, Action saveAction);
    }

    public interface IProcessControlData
    {
        void RemoveProcessControlCondition(ProcessControlCondition processControlCondition, User byUser);

        void RestoreProcessControlCondition(ProcessControlCondition processControlCondition, User byUser);
        ProcessControlCondition LoadProcessControlConditionForLocation(Location location);
        List<ProcessControlCondition> LoadProcessControlConditions();
        ProcessControlCondition AddProcessControlCondition(ProcessControlCondition processControlCondition, User byUser);
        void SaveProcessControlCondition(List<ProcessControlConditionDiff> diffs);
    }

    public interface IProcessControlUseCase
    {
        void RemoveProcessControlCondition(ProcessControlCondition processControlCondition, IProcessControlErrorGui errorHandler);
        void LoadProcessControlConditionForLocation(Location location, IProcessControlErrorGui errorHandler);
        void LoadProcessControlConditions(IProcessControlErrorGui errorHandler);
        void AddProcessControlCondition(ProcessControlCondition processControlCondition, IProcessControlErrorGui errorHandler);
        void SaveProcessControlCondition(List<ProcessControlConditionDiff> diff, IProcessControlErrorGui errorHandler, IProcessControlSaveGuiShower saveGuiShower);
        void UpdateProcessControlCondition(List<ProcessControlConditionDiff> diffs, IProcessControlErrorGui errorHandler);
    }

    public class ProcessControlUseCase : IProcessControlUseCase
    {
        private IProcessControlGui _gui;
        private IProcessControlData _dataAccess;
        private ISessionInformationUserGetter _userGetter;
        private readonly INotificationManager _notificationManager;
        private ITestDateCalculationUseCase _testDateCalculationUseCase;
        private static readonly ILog Log = LogManager.GetLogger(typeof(LocationUseCase));


        public ProcessControlUseCase(IProcessControlGui gui, IProcessControlData dataAccess, ISessionInformationUserGetter userGetter, INotificationManager notificationManager, ITestDateCalculationUseCase testDateCalculationUseCase)
        {
            _gui = gui;
            _dataAccess = dataAccess;
            _userGetter = userGetter;
            _notificationManager = notificationManager;
            _testDateCalculationUseCase = testDateCalculationUseCase;
        }

        public void RemoveProcessControlCondition(ProcessControlCondition processControlCondition, IProcessControlErrorGui errorHandler)
        {
            try
            {
                Log.Info("RemoveProcessControlCondition started");
                _dataAccess.RemoveProcessControlCondition(processControlCondition, _userGetter?.GetCurrentUser());
                _gui.RemoveProcessControlCondition(processControlCondition);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception e)
            {
                Log.Error("Error in RemoveProcessControlCondition", e);
                errorHandler.ShowProblemRemoveProcessControlCondition();
            }
            Log.Info("RemoveProcessControlCondition ended");
        }

        public void LoadProcessControlConditionForLocation(Location location, IProcessControlErrorGui errorHandler)
        {
            try
            {
                Log.Info("LoadProcessControlConditionForLocation started");
                var processControlCondition = _dataAccess.LoadProcessControlConditionForLocation(location);
                _gui.ShowProcessControlConditionForLocation(processControlCondition);
            }
            catch (Exception e)
            {
                Log.Error("Error in LoadProcessControlConditionForLocation", e);
                errorHandler.ShowProblemLoadingLocationProcessControlCondition();
            }
            Log.Info("LoadProcessControlConditionForLocation ended");
        }

        public void AddProcessControlCondition(ProcessControlCondition processControlCondition, IProcessControlErrorGui errorHandler)
        {
            try
            {
                Log.Info("AddProcessControlCondition started");
                var addProcessControlCondition = _dataAccess.AddProcessControlCondition(processControlCondition, _userGetter?.GetCurrentUser());
                _gui.AddProcessControlCondition(addProcessControlCondition);
                _notificationManager.SendSuccessNotification();

                if (FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
                {
                    _testDateCalculationUseCase.CalculateProcessControlDateFor(new List<ProcessControlConditionId>() { processControlCondition.Id });
                    LoadProcessControlConditions(errorHandler);
                }
            }
            catch (Exception exception)
            {
                Log.Error("Error in AddProcessControlCondition", exception);
                errorHandler.ShowProblemSavingProcessControlCondition();
            }
            Log.Info("AddProcessControlCondition ended");
        }

        public void SaveProcessControlCondition(List<ProcessControlConditionDiff> diffs, IProcessControlErrorGui errorHandler,
            IProcessControlSaveGuiShower saveGuiShower)
        {
            saveGuiShower.SaveProcessControl(diffs, () => { UpdateProcessControlCondition(diffs, errorHandler); });
        }

        public void UpdateProcessControlCondition(List<ProcessControlConditionDiff> diffs, IProcessControlErrorGui errorHandler)
        {
            try
            {
                Log.Info("SaveProcessControlCondition started");

                diffs.ForEach(x => x.User = _userGetter?.GetCurrentUser());
                _dataAccess.SaveProcessControlCondition(diffs);
                _gui.UpdateProcessControlCondition(diffs.Select(x => x.GetNewProcessControlCondition()).ToList());
                _notificationManager.SendSuccessNotification();

                if (FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
                {
                    _testDateCalculationUseCase.CalculateProcessControlDateFor(diffs.Select(x => x.GetNewProcessControlCondition().Id).ToList());
                    LoadProcessControlConditions(errorHandler);
                }

                Log.Info("SaveProcessControlCondition ended");
            }
            catch (Exception e)
            {
                errorHandler.ShowProblemSavingProcessControlCondition();
                Log.Error("Error in SaveProcessControlCondition", e);
            }
        }

        public void LoadProcessControlConditions(IProcessControlErrorGui errorHandler)
        {
            try
            {
                _gui.ShowProcessControlConditions(_dataAccess.LoadProcessControlConditions());
            }
            catch (Exception e)
            {
                errorHandler.ShowProblemLoadingLocationProcessControlCondition();
                Log.Error("Error in LoadProcessControlConditions", e);
            }
        }
    }

    public class ProcessControlUseCaseHumbleAsyncRunner : IProcessControlUseCase
    {
        private readonly IProcessControlUseCase _real;

        public ProcessControlUseCaseHumbleAsyncRunner(IProcessControlUseCase real)
        {
            _real = real;
        }

        public void RemoveProcessControlCondition(ProcessControlCondition processControlCondition, IProcessControlErrorGui errorHandler)
        {
            Task.Run(() => _real.RemoveProcessControlCondition(processControlCondition, errorHandler));
        }

        public void LoadProcessControlConditionForLocation(Location location, IProcessControlErrorGui errorHandler)
        {
            Task.Run(() => _real.LoadProcessControlConditionForLocation(location, errorHandler));
        }

        public void AddProcessControlCondition(ProcessControlCondition processControlCondition, IProcessControlErrorGui errorHandler)
        {
            Task.Run(() => _real.AddProcessControlCondition(processControlCondition, errorHandler));
        }

        public void SaveProcessControlCondition(List<ProcessControlConditionDiff> diffs, IProcessControlErrorGui errorHandler,
            IProcessControlSaveGuiShower saveGuiShower)
        {
            Task.Run(() => _real.SaveProcessControlCondition(diffs, errorHandler, saveGuiShower));
        }

        public void UpdateProcessControlCondition(List<ProcessControlConditionDiff> diffs, IProcessControlErrorGui errorHandler)
        {
            Task.Run(() => _real.UpdateProcessControlCondition(diffs, errorHandler));
        }

        public void LoadProcessControlConditions(IProcessControlErrorGui errorHandler)
        {
            Task.Run(() => _real.LoadProcessControlConditions(errorHandler));
        }
    }
}
