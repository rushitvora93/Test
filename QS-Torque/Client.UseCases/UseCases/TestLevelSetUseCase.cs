using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Core.Diffs;
using Client.UseCases.UseCases;
using Core.Entities;
using log4net;

namespace Core.UseCases
{
    public interface ITestLevelSetErrorHandler
    {
        void ShowTestLevelSetError();
    }

    public interface ITestLevelSetDiffShower
    {
        void ShowDiffDialog(TestLevelSetDiff diff, Action saveAction);
    }

    public interface ITestLevelSetGui
    {
        void LoadTestLevelSets(List<TestLevelSet> testLevelSets);
        void AddTestLevelSet(TestLevelSet newItem);
        void RemoveTestLevelSet(TestLevelSet oldItem);
        void UpdateTestLevelSet(TestLevelSetDiff updatedItem);
    }

    public interface ITestLevelSetData
    {
        List<TestLevelSet> LoadTestLevelSets();
        TestLevelSet AddTestLevelSet(TestLevelSetDiff diff);
        void RemoveTestLevelSet(TestLevelSetDiff diff);
        void UpdateTestLevelSet(TestLevelSetDiff updatedItem);
        bool IsTestLevelSetNameUnique(string nam);
        bool DoesTestLevelSetHaveReferences(TestLevelSet set);
    }

    public interface ITestLevelSetUseCase
    {
        void LoadTestLevelSets(ITestLevelSetErrorHandler errorHandler);
        void AddTestLevelSet(TestLevelSet newItem, ITestLevelSetErrorHandler errorHandler);
        void RemoveTestLevelSet(TestLevelSet oldItem, ITestLevelSetErrorHandler errorHandler);
        void UpdateTestLevelSet(TestLevelSetDiff diffupdatedItem, ITestLevelSetErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler, ITestLevelSetDiffShower diffShower);
        bool IsTestLevelSetNameUnique(string name);
        bool DoesTestLevelSetHaveReferences(TestLevelSet set);
    }

    public class TestLevelSetUseCase : ITestLevelSetUseCase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(TestLevelSetUseCase));

        private ITestLevelSetGui _gui;
        private ITestLevelSetData _data;
        private INotificationManager _notification;
        private ITestDateCalculationUseCase _testDateCalculationUseCase;
        private ILocationToolAssignmentUseCase _locationToolAssignmentUseCase;
        private IProcessControlUseCase _processControlUseCase;
        private ISessionInformationUserGetter _userGetter;

        public TestLevelSetUseCase(ITestLevelSetGui gui, ITestLevelSetData data, INotificationManager notification, ITestDateCalculationUseCase testDateCalculationUseCase, ILocationToolAssignmentUseCase locationToolAssignmentUseCase, IProcessControlUseCase processControlUseCase, ISessionInformationUserGetter userGetter)
        {
            _gui = gui;
            _data = data;
            _notification = notification;
            _testDateCalculationUseCase = testDateCalculationUseCase;
            _locationToolAssignmentUseCase = locationToolAssignmentUseCase;
            _processControlUseCase = processControlUseCase;
            _userGetter = userGetter;
        }


        public void LoadTestLevelSets(ITestLevelSetErrorHandler errorHandler)
        {
            try
            {
                _gui.LoadTestLevelSets(_data.LoadTestLevelSets());
            }
            catch (Exception e)
            {
                errorHandler.ShowTestLevelSetError();
                _log.Error("Error in LoadTestLevelSets", e);
            }
        }

        public void AddTestLevelSet(TestLevelSet newItem, ITestLevelSetErrorHandler errorHandler)
        {
            try
            {
                var result = _data.AddTestLevelSet(new TestLevelSetDiff()
                {
                    New = newItem,
                    User = _userGetter.GetCurrentUser()
                });

                newItem.Id = new TestLevelSetId(result.Id.ToLong());
                newItem.TestLevel1.Id = new TestLevelId(result.TestLevel1.Id.ToLong());
                newItem.TestLevel2.Id = new TestLevelId(result.TestLevel2.Id.ToLong());
                newItem.TestLevel3.Id = new TestLevelId(result.TestLevel3.Id.ToLong());
                
                _gui.AddTestLevelSet(newItem);
            }
            catch (Exception e)
            {
                errorHandler.ShowTestLevelSetError();
                _log.Error("Error in AddTestLevelSet", e);
            }
        }

        public void RemoveTestLevelSet(TestLevelSet oldItem, ITestLevelSetErrorHandler errorHandler)
        {
            try
            {
                _data.RemoveTestLevelSet(new TestLevelSetDiff()
                {
                    Old = oldItem,
                    User = _userGetter.GetCurrentUser()
                });
                _gui.RemoveTestLevelSet(oldItem);
            }
            catch (Exception e)
            {
                errorHandler.ShowTestLevelSetError();
                _log.Error("Error in RemoveTestLevelSet", e);
            }
        }

        public void UpdateTestLevelSet(TestLevelSetDiff diff, ITestLevelSetErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler, ITestLevelSetDiffShower diffShower)
        {
            try
            {
                if (FeatureToggles.FeatureToggles.SilverTowel_974_TestLevelSetHistory)
                {
                    diff.User = _userGetter.GetCurrentUser();

                    diffShower.ShowDiffDialog(diff, () =>
                    {
                        _data.UpdateTestLevelSet(diff);
                        _gui.UpdateTestLevelSet(diff);
                        _notification.SendSuccessNotification();

                        if (FeatureToggles.FeatureToggles.TestDateCalculation)
                        {
                            _testDateCalculationUseCase.CalculateToolTestDateForTestLevelSet(diff.New.Id);
                            _locationToolAssignmentUseCase.LoadLocationToolAssignments();
                            _testDateCalculationUseCase.CalculateProcessControlDateForTestLevelSet(diff.New.Id);
                            _processControlUseCase.LoadProcessControlConditions(processControlErrorHandler);
                        }
                    });
                }
                else
                {
                    _data.UpdateTestLevelSet(diff);
                    _gui.UpdateTestLevelSet(diff);
                    _notification.SendSuccessNotification();

                    if (FeatureToggles.FeatureToggles.TestDateCalculation)
                    {
                        _testDateCalculationUseCase.CalculateToolTestDateForTestLevelSet(diff.New.Id);
                        _locationToolAssignmentUseCase.LoadLocationToolAssignments();
                        _testDateCalculationUseCase.CalculateProcessControlDateForTestLevelSet(diff.New.Id);
                        _processControlUseCase.LoadProcessControlConditions(processControlErrorHandler);
                    }
                }
            }
            catch (Exception e)
            {
                errorHandler.ShowTestLevelSetError();
                _log.Error("Error in UpdateTestLevelSet", e);
            }
        }

        public bool IsTestLevelSetNameUnique(string name)
        {
            try
            {
                return _data.IsTestLevelSetNameUnique(name);
            }
            catch (Exception e)
            {
                _log.Error("Error in IsTestLevelSetNameUnique", e);
                return false;
            }
        }

        public bool DoesTestLevelSetHaveReferences(TestLevelSet set)
        {
            try
            {
                return _data.DoesTestLevelSetHaveReferences(set);
            }
            catch (Exception e)
            {
                _log.Error("Error in DoesTestLevelSetHaveReferences", e);
                return false;
            }
        }
    }


    public class TestLevelSetHumbleAsyncRunner : ITestLevelSetUseCase
    {
        private ITestLevelSetUseCase _real;

        public TestLevelSetHumbleAsyncRunner(ITestLevelSetUseCase real)
        {
            _real = real;
        }

        public void LoadTestLevelSets(ITestLevelSetErrorHandler errorHandler)
        {
            Task.Run(() => _real.LoadTestLevelSets(errorHandler));
        }

        public void AddTestLevelSet(TestLevelSet newItem, ITestLevelSetErrorHandler errorHandler)
        {
            Task.Run(() => _real.AddTestLevelSet(newItem, errorHandler));
        }

        public void RemoveTestLevelSet(TestLevelSet oldItem, ITestLevelSetErrorHandler errorHandler)
        {
            Task.Run(() => _real.RemoveTestLevelSet(oldItem, errorHandler));
        }

        public void UpdateTestLevelSet(TestLevelSetDiff diff, ITestLevelSetErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler, ITestLevelSetDiffShower diffShower)
        {
            Task.Run(() => _real.UpdateTestLevelSet(diff, errorHandler, processControlErrorHandler, diffShower));
        }

        public bool IsTestLevelSetNameUnique(string name)
        {
            // This method is not called async
            return _real.IsTestLevelSetNameUnique(name);
        }

        public bool DoesTestLevelSetHaveReferences(TestLevelSet set)
        {
            // This method is not called async
            return _real.DoesTestLevelSetHaveReferences(set);
        }
    }
}
