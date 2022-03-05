using System;
using System.Threading.Tasks;
using Client.Core.Diffs;
using Client.UseCases.UseCases;
using Core.Entities;
using log4net;

namespace Core.UseCases
{
    public interface IShiftManagementErrorHandler
    {
        void ShiftManagementError();
    }

    public interface IShiftManagementDiffShower
    {
        void ShowDiffDialog(ShiftManagementDiff diff, Action saveAction);
    }

    public interface IShiftManagementGui
    {
        void LoadShiftManagement(ShiftManagement entity);
        void SaveShiftManagement(ShiftManagementDiff diff);
    }

    public interface IShiftManagementData
    {
        ShiftManagement LoadShiftManagement();
        void SaveShiftManagement(ShiftManagementDiff diff);
    }

    public interface IShiftManagementUseCase
    {
        void LoadShiftManagement(IShiftManagementErrorHandler errorHandler);
        void SaveShiftManagement(ShiftManagementDiff diff, IShiftManagementErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler, IShiftManagementDiffShower diffShower);
    }


    public class ShiftManagementUseCase : IShiftManagementUseCase
    {
        private IShiftManagementGui _gui;
        private IShiftManagementData _data;
        private INotificationManager _notification;
        private ITestDateCalculationUseCase _testDateCalculationUseCase;
        private ILocationToolAssignmentUseCase _locationToolAssignmentUseCase;
        private IProcessControlUseCase _processControlUseCase;
        private ISessionInformationUserGetter _userGetter;
        private static readonly ILog _log = LogManager.GetLogger(typeof(ShiftManagementUseCase));

        public ShiftManagementUseCase(IShiftManagementGui gui, IShiftManagementData data, INotificationManager notification, ITestDateCalculationUseCase testDateCalculationUseCase, ILocationToolAssignmentUseCase locationToolAssignmentUseCase, IProcessControlUseCase processControlUseCase, ISessionInformationUserGetter userGetter)
        {
            _gui = gui;
            _data = data;
            _notification = notification;
            _testDateCalculationUseCase = testDateCalculationUseCase;
            _locationToolAssignmentUseCase = locationToolAssignmentUseCase;
            _processControlUseCase = processControlUseCase;
            _userGetter = userGetter;
        }

        public void LoadShiftManagement(IShiftManagementErrorHandler errorHandler)
        {
            try
            {
                _gui.LoadShiftManagement(_data.LoadShiftManagement());
            }
            catch (Exception e)
            {
                _log.Error("Error in LoadShiftManagement", e);
                errorHandler.ShiftManagementError();
            }
        }

        public void SaveShiftManagement(ShiftManagementDiff diff, IShiftManagementErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler, IShiftManagementDiffShower diffShower)
        {
            try
            {
                diff.User = _userGetter.GetCurrentUser();

                diffShower.ShowDiffDialog(diff, () =>
                {
                    _data.SaveShiftManagement(diff);
                    _gui.SaveShiftManagement(diff);
                    _notification.SendSuccessNotification();

                    if (FeatureToggles.FeatureToggles.TestDateCalculation)
                    {
                        _testDateCalculationUseCase.CalculateToolTestDateForAllLocationToolAssignments();
                        _locationToolAssignmentUseCase.LoadLocationToolAssignments();
                        if(FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
                        {
                            _testDateCalculationUseCase.CalculateProcessControlDateForAllProcessControlConditions();
                            _processControlUseCase.LoadProcessControlConditions(processControlErrorHandler);
                        }
                    }
                });
            }
            catch (Exception e)
            {
                _log.Error("Error in SaveShiftManagement", e);
                errorHandler.ShiftManagementError();
            }
        }
    }


    public class ShiftManagementHumbleAsyncRunner : IShiftManagementUseCase
    {
        private IShiftManagementUseCase _real;

        public ShiftManagementHumbleAsyncRunner(IShiftManagementUseCase real)
        {
            _real = real;
        }

        public void LoadShiftManagement(IShiftManagementErrorHandler errorHandler)
        {
            Task.Run(() => _real.LoadShiftManagement(errorHandler));
        }

        public void SaveShiftManagement(ShiftManagementDiff diff, IShiftManagementErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler, IShiftManagementDiffShower diffShower)
        {
            Task.Run(() => _real.SaveShiftManagement(diff, errorHandler, processControlErrorHandler, diffShower));
        }
    }
}
