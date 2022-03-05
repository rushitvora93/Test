using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Core.Entities;
using Core;
using Core.Entities;
using Core.Enums;
using Core.UseCases;
using log4net;

namespace Client.UseCases.UseCases
{
    public interface ITestLevelSetAssignmentErrorHandler
    {
        void ShowTestLevelSetAssignmentError();
    }

    public interface ITestLevelSetAssignmentGui
    {
        void LoadLocationToolAssignments(List<LocationToolAssignment> assignments);
        void RemoveTestLevelSetAssignmentFor(List<(LocationToolAssignmentId, TestType)> ids);
        void AssignTestLevelSetToLocationToolAssignments(TestLevelSet testLevelSet, List<(LocationToolAssignmentId, TestType)> locationToolAssignmentIds);
    }

    public interface ITestLevelSetAssignmentGuiForProcessControl
    {
        void AssignTestLevelSetToProcessControlConditions(TestLevelSet testLevelSet, List<ProcessControlConditionId> processControlConditionIds);
        void RemoveTestLevelSetAssignmentFor(List<ProcessControlConditionId> ids);
    }

    public interface ITestLevelSetAssignmentData
    {
        List<LocationToolAssignment> LoadLocationToolAssignments();
        void RemoveTestLevelSetAssignmentFor(List<(LocationToolAssignmentId, TestType)> ids, User user);
        void AssignTestLevelSetToLocationToolAssignments(TestLevelSetId testLevelSetId, List<(LocationToolAssignmentId, TestType)> locationToolAssignmentIds, User user);
        void AssignTestLevelSetToProcessControlConditions(TestLevelSetId testLevelSet, List<ProcessControlConditionId> processControlConditionIds, User user);
        void RemoveTestLevelSetAssignmentFor(List<ProcessControlConditionId> ids, User user);
    }

    public interface ITestLevelSetAssignmentUseCase
    {
        void LoadLocationToolAssignments(ITestLevelSetAssignmentErrorHandler errorHandler);
        void RemoveTestLevelSetAssignmentFor(List<(LocationToolAssignmentId, TestType)> ids, ITestLevelSetAssignmentErrorHandler errorHandler);
        void AssignTestLevelSetToLocationToolAssignments(TestLevelSet testLevelSet, List<(LocationToolAssignmentId, TestType)> locationToolAssignmentIds, ITestLevelSetAssignmentErrorHandler errorHandler);
        void AssignTestLevelSetToProcessControlConditions(TestLevelSet testLevelSet, List<ProcessControlConditionId> processControlConditionIds, ITestLevelSetAssignmentErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler);
        void RemoveTestLevelSetAssignmentFor(List<ProcessControlConditionId> ids, ITestLevelSetAssignmentErrorHandler errorHandler);
    }

    public class TestLevelSetAssignmentUseCase : ITestLevelSetAssignmentUseCase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(TestLevelSetAssignmentUseCase));
        private ITestLevelSetAssignmentGui _gui;
        private ITestLevelSetAssignmentGuiForProcessControl _processControlGui;
        private ITestLevelSetAssignmentData _data;
        private INotificationManager _notification;
        private ITestDateCalculationUseCase _testDateCalculationUseCase;
        private ILocationToolAssignmentUseCase _locationToolAssignmentUseCase;
        private IProcessControlUseCase _processControlUseCase;
        private ISessionInformationUserGetter _userGetter;

        public TestLevelSetAssignmentUseCase(ITestLevelSetAssignmentGui gui, ITestLevelSetAssignmentGuiForProcessControl processControlGui, ITestLevelSetAssignmentData data, INotificationManager notification, ITestDateCalculationUseCase testDateCalculationUseCase, ILocationToolAssignmentUseCase locationToolAssignmentUseCase, IProcessControlUseCase processControlUseCase, ISessionInformationUserGetter userGetter)
        {
            _gui = gui;
            _processControlGui = processControlGui;
            _data = data;
            _notification = notification;
            _testDateCalculationUseCase = testDateCalculationUseCase;
            _locationToolAssignmentUseCase = locationToolAssignmentUseCase;
            _processControlUseCase = processControlUseCase;
            _userGetter = userGetter;
        }

        public void LoadLocationToolAssignments(ITestLevelSetAssignmentErrorHandler errorHandler)
        {
            try
            {
                _gui.LoadLocationToolAssignments(_data.LoadLocationToolAssignments());
            }
            catch (Exception e)
            {
                _log.Error("Error in LoadLocationToolAssignments", e);
                errorHandler.ShowTestLevelSetAssignmentError();
            }
        }

        public void RemoveTestLevelSetAssignmentFor(List<(LocationToolAssignmentId, TestType)> ids, ITestLevelSetAssignmentErrorHandler errorHandler)
        {
            try
            {
                _data.RemoveTestLevelSetAssignmentFor(ids, _userGetter.GetCurrentUser());
                _gui.RemoveTestLevelSetAssignmentFor(ids);
                _notification.SendSuccessNotification(ids.Count);
            }
            catch (Exception e)
            {
                _log.Error("Error in RemoveTestLevelSetAssignmentFor", e);
                errorHandler.ShowTestLevelSetAssignmentError();
            }
        }

        public void AssignTestLevelSetToLocationToolAssignments(TestLevelSet testLevelSet, List<(LocationToolAssignmentId, TestType)> locationToolAssignmentIds, ITestLevelSetAssignmentErrorHandler errorHandler)
        {
            try
            {
                _data.AssignTestLevelSetToLocationToolAssignments(testLevelSet.Id, locationToolAssignmentIds, _userGetter.GetCurrentUser());
                _gui.AssignTestLevelSetToLocationToolAssignments(testLevelSet, locationToolAssignmentIds);
                _notification.SendSuccessNotification(locationToolAssignmentIds.Count);

                if (FeatureToggles.FeatureToggles.TestDateCalculation)
                {
                    _testDateCalculationUseCase.CalculateToolTestDateFor(locationToolAssignmentIds.Select(x => x.Item1).ToList());
                    _locationToolAssignmentUseCase.LoadLocationToolAssignments();
                }
            }
            catch (Exception e)
            {
                _log.Error("Error in AssignTestLevelSetToLocationToolAssignments", e);
                errorHandler.ShowTestLevelSetAssignmentError();
            }
        }

        public void AssignTestLevelSetToProcessControlConditions(TestLevelSet testLevelSet, List<ProcessControlConditionId> processControlConditionIds, ITestLevelSetAssignmentErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler)
        {
            try
            {
                _data.AssignTestLevelSetToProcessControlConditions(testLevelSet.Id, processControlConditionIds, _userGetter.GetCurrentUser());
                _processControlGui.AssignTestLevelSetToProcessControlConditions(testLevelSet, processControlConditionIds);
                _notification.SendSuccessNotification(processControlConditionIds.Count);

                if (FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
                {
                    _testDateCalculationUseCase.CalculateProcessControlDateFor(processControlConditionIds);
                    _processControlUseCase.LoadProcessControlConditions(processControlErrorHandler);
                }
            }
            catch (Exception e)
            {
                _log.Error("Error in AssignTestLevelSetToProcessControlConditions", e);
                errorHandler.ShowTestLevelSetAssignmentError();
            }
        }

        public void RemoveTestLevelSetAssignmentFor(List<ProcessControlConditionId> ids, ITestLevelSetAssignmentErrorHandler errorHandler)
        {
            try
            {
                _data.RemoveTestLevelSetAssignmentFor(ids, _userGetter.GetCurrentUser());
                _processControlGui.RemoveTestLevelSetAssignmentFor(ids);
                _notification.SendSuccessNotification(ids.Count);
            }
            catch (Exception e)
            {
                _log.Error("Error in RemoveTestLevelSetAssignmentFor", e);
                errorHandler.ShowTestLevelSetAssignmentError();
            }
        }
    }


    public class TestLevelSetAssignmentHumbleAsyncRunner : ITestLevelSetAssignmentUseCase
    {
        private ITestLevelSetAssignmentUseCase _real;

        public TestLevelSetAssignmentHumbleAsyncRunner(ITestLevelSetAssignmentUseCase real)
        {
            _real = real;
        }

        public void LoadLocationToolAssignments(ITestLevelSetAssignmentErrorHandler errorHandler)
        {
            Task.Run(() => _real.LoadLocationToolAssignments(errorHandler));
        }

        public void RemoveTestLevelSetAssignmentFor(List<(LocationToolAssignmentId, TestType)> ids, ITestLevelSetAssignmentErrorHandler errorHandler)
        {
            Task.Run(() => _real.RemoveTestLevelSetAssignmentFor(ids, errorHandler));
        }

        public void AssignTestLevelSetToLocationToolAssignments(TestLevelSet testLevelSet, List<(LocationToolAssignmentId, TestType)> locationToolAssignmentIds, ITestLevelSetAssignmentErrorHandler errorHandler)
        {
            Task.Run(() => _real.AssignTestLevelSetToLocationToolAssignments(testLevelSet, locationToolAssignmentIds, errorHandler));
        }

        public void AssignTestLevelSetToProcessControlConditions(TestLevelSet testLevelSet, List<ProcessControlConditionId> processControlConditionIds, ITestLevelSetAssignmentErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler)
        {
            Task.Run(() => _real.AssignTestLevelSetToProcessControlConditions(testLevelSet, processControlConditionIds, errorHandler, processControlErrorHandler));
        }

        public void RemoveTestLevelSetAssignmentFor(List<ProcessControlConditionId> ids, ITestLevelSetAssignmentErrorHandler errorHandler)
        {
            Task.Run(() => _real.RemoveTestLevelSetAssignmentFor(ids, errorHandler));
        }
    }
}
