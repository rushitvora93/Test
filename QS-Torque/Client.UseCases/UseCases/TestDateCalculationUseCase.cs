using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Core.Entities;
using Core;
using Core.Entities;
using log4net;

namespace Client.UseCases.UseCases
{
    public interface ITestDateCalculationData
    {
        void CalculateToolTestDateFor(List<LocationToolAssignmentId> ids);
        void CalculateToolTestDateForTestLevelSet(TestLevelSetId id);
        void CalculateToolTestDateForAllLocationToolAssignments();
        void CalculateProcessControlDateFor(List<ProcessControlConditionId> ids);
        void CalculateProcessControlDateForTestLevelSet(TestLevelSetId id);
        void CalculateProcessControlDateForAllProcessControlConditions();
    }

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
        private static readonly ILog _log = LogManager.GetLogger(typeof(TestDateCalculationUseCase));
        
        private ITestDateCalculationData _data;
        private INotificationManager _notification;

        public TestDateCalculationUseCase(ITestDateCalculationData data, INotificationManager notification)
        {
            _data = data;
            _notification = notification;
        }
        
        public void CalculateToolTestDateFor(List<LocationToolAssignmentId> ids)
        {
            try
            {
                _data.CalculateToolTestDateFor(ids);
                _notification.SendSuccessfulToolTestDateCalculationNotification();
            }
            catch (Exception e)
            {
                _log.Error("Error in CalculateToolTestDateFor", e);
                _notification.SendFailedTestDateCalculationNotification();
            }
        }

        public void CalculateToolTestDateForTestLevelSet(TestLevelSetId id)
        {
            try
            {
                _data.CalculateToolTestDateForTestLevelSet(id);
                _notification.SendSuccessfulToolTestDateCalculationNotification();
            }
            catch (Exception e)
            {
                _log.Error("Error in CalculateToolTestDateForTestLevelSet", e);
                _notification.SendFailedTestDateCalculationNotification();
            }
        }

        public void CalculateToolTestDateForAllLocationToolAssignments()
        {
            try
            {
                _data.CalculateToolTestDateForAllLocationToolAssignments();
                _notification.SendSuccessfulToolTestDateCalculationNotification();
            }
            catch (Exception e)
            {
                _log.Error("Error in CalculateToolTestDateForAllLocationToolAssignments", e);
                _notification.SendFailedTestDateCalculationNotification();
            }
        }

        public void CalculateProcessControlDateFor(List<ProcessControlConditionId> ids)
        {
            try
            {
                _data.CalculateProcessControlDateFor(ids);
                _notification.SendSuccessfulProcessControlDateCalculationNotification();
            }
            catch (Exception e)
            {
                _log.Error("Error in CalculateProcessControlDateFor", e);
                _notification.SendFailedTestDateCalculationNotification();
            }
        }

        public void CalculateProcessControlDateForTestLevelSet(TestLevelSetId id)
        {
            try
            {
                _data.CalculateProcessControlDateForTestLevelSet(id);
                _notification.SendSuccessfulProcessControlDateCalculationNotification();
            }
            catch (Exception e)
            {
                _log.Error("Error in CalculateProcessControlDateForTestLevelSet", e);
                _notification.SendFailedTestDateCalculationNotification();
            }
        }

        public void CalculateProcessControlDateForAllProcessControlConditions()
        {
            try
            {
                _data.CalculateProcessControlDateForAllProcessControlConditions();
                _notification.SendSuccessfulProcessControlDateCalculationNotification();
            }
            catch (Exception e)
            {
                _log.Error("Error in CalculateProcessControlDateForAllProcessControlConditions", e);
                _notification.SendFailedTestDateCalculationNotification();
            }
        }
    }

    public class TestDateCalculationHumbleAsyncRunner : ITestDateCalculationUseCase
    {
        private ITestDateCalculationUseCase _real;

        public TestDateCalculationHumbleAsyncRunner(ITestDateCalculationUseCase real)
        {
            _real = real;
        }

        public void CalculateToolTestDateFor(List<LocationToolAssignmentId> ids)
        {
            Task.Run(() => _real.CalculateToolTestDateFor(ids));
        }

        public void CalculateToolTestDateForTestLevelSet(TestLevelSetId id)
        {
            Task.Run(() => _real.CalculateToolTestDateForTestLevelSet(id));
        }

        public void CalculateToolTestDateForAllLocationToolAssignments()
        {
            Task.Run(() => _real.CalculateToolTestDateForAllLocationToolAssignments());
        }

        public void CalculateProcessControlDateFor(List<ProcessControlConditionId> ids)
        {
            Task.Run(() => _real.CalculateProcessControlDateFor(ids));
        }

        public void CalculateProcessControlDateForTestLevelSet(TestLevelSetId id)
        {
            Task.Run(() => _real.CalculateProcessControlDateForTestLevelSet(id));
        }

        public void CalculateProcessControlDateForAllProcessControlConditions()
        {
            Task.Run(() => _real.CalculateProcessControlDateForAllProcessControlConditions());
        }
    }
}
