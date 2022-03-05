using Client.UseCases.UseCases;
using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using InterfaceAdapters;
using InterfaceAdapters.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Core.Enums;
using TestHelper.Checker;
using TestHelper.Mock;
using Client.TestHelper.Factories;
using TestHelper.Factories;
using Core.Diffs;
using System.Windows.Threading;
using Client.Core.Entities;
using Client.Core.Diffs;
using Client.TestHelper.Mock;
using System.Linq;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    public class ProcessControlPlanningViewModelTest
    {
        [Test]
        public void LoadedCommandCallsTestLevelSetUseCase()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.LoadedCommand.Execute(null);
            Assert.IsTrue(environment.testLevelSetUseCase.LoadTestLevelSetsCalled);
            Assert.AreSame(environment.viewModel, environment.testLevelSetUseCase.LoadTestLevelSetsErrorHandler);
        }

        [Test]
        public void LoadedCommandClearsSelectedProcessControlsForTestLevelAssignment()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedProcessControlsForTestLevelAssignment = new ObservableCollection<ProcessControlConditionHumbleModel>();
            environment.viewModel.SelectedProcessControlsForTestLevelAssignment.Add(new ProcessControlConditionHumbleModel(new ProcessControlCondition(), new NullLocalizationWrapper()));
            environment.viewModel.LoadedCommand.Execute(null);
            Assert.AreEqual(0, environment.viewModel.SelectedProcessControlsForTestLevelAssignment.Count);
        }

        [Test]
        public void LoadedCommandCallsProcessControlUseCase()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.LoadedCommand.Execute(null);
            Assert.AreSame(environment.viewModel, environment.processControlUseCase.LoadProcessControlConditionsErrorHandler);
        }

        [Test]
        public void AssignTestLevelSetCanExecuteReturnsTrueIfTestLevelSetAndProcessControlCinditionAreSelected()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedProcessControlsForTestLevelAssignment = new ObservableCollection<ProcessControlConditionHumbleModel>(new List<ProcessControlConditionHumbleModel>()
            {
                new ProcessControlConditionHumbleModel(new ProcessControlCondition(), new NullLocalizationWrapper())
            });
            Assert.IsTrue(environment.viewModel.AssignTestLevelSetCommand.CanExecute(null));
        }

        [Test]
        public void AssignTestLevelSetCanExecuteReturnsTrueIfTestLevelSetAndProcessControlConditionsAreSelected()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedProcessControlsForTestLevelAssignment = new ObservableCollection<ProcessControlConditionHumbleModel>(new List<ProcessControlConditionHumbleModel>()
            {
                new ProcessControlConditionHumbleModel(new ProcessControlCondition(), new NullLocalizationWrapper()),
                new ProcessControlConditionHumbleModel(new ProcessControlCondition(), new NullLocalizationWrapper()),
                new ProcessControlConditionHumbleModel(new ProcessControlCondition(), new NullLocalizationWrapper())
            });
            Assert.IsTrue(environment.viewModel.AssignTestLevelSetCommand.CanExecute(null));
        }

        [Test]
        public void AssignTestLevelSetCanExecuteReturnsFalseIfNoTestLevelSetSelected()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = null;
            environment.viewModel.SelectedProcessControlsForTestLevelAssignment = new ObservableCollection<ProcessControlConditionHumbleModel>(new List<ProcessControlConditionHumbleModel>()
            {
                new ProcessControlConditionHumbleModel(new ProcessControlCondition(), new NullLocalizationWrapper())
            });
            Assert.IsFalse(environment.viewModel.AssignTestLevelSetCommand.CanExecute(null));
        }

        [Test]
        public void AssignTestLevelSetCanExecuteReturnsFalseIfNoProcessControlConditionSelected()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedProcessControlsForTestLevelAssignment = new ObservableCollection<ProcessControlConditionHumbleModel>(new List<ProcessControlConditionHumbleModel>());
            Assert.IsFalse(environment.viewModel.AssignTestLevelSetCommand.CanExecute(null));
        }

        [Test]
        public void AssignTestLevelSetCanExecuteReturnsFalseIfSelectedProcessControlConditionsAreNull()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedProcessControlsForTestLevelAssignment = null;
            Assert.IsFalse(environment.viewModel.AssignTestLevelSetCommand.CanExecute(null));
        }

        [TestCase(5)]
        [TestCase(6)]
        public void AssignTestLevelSetCanExecuteReturnsFalseIfSelectedProcessControlConditionsIsAlreadyAssignedToSelectedTestLevelSet(long id)
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(id) });
            environment.viewModel.SelectedProcessControlsForTestLevelAssignment = new ObservableCollection<ProcessControlConditionHumbleModel>(new List<ProcessControlConditionHumbleModel>()
            {
                new ProcessControlConditionHumbleModel(new ProcessControlCondition(), new NullLocalizationWrapper())
                {
                    TestLevelSet = new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(id) })
                }
            });
            Assert.IsFalse(environment.viewModel.AssignTestLevelSetCommand.CanExecute(null));
        }

        [TestCase(5)]
        [TestCase(6)]
        public void AssignTestLevelSetCanExecuteReturnsTrueIfOneSelectedProcessControlConditionsIsAlreadyAssignedToSelectedTestLevelSet(long id)
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(id) });
            environment.viewModel.SelectedProcessControlsForTestLevelAssignment = new ObservableCollection<ProcessControlConditionHumbleModel>(new List<ProcessControlConditionHumbleModel>()
            {
                new ProcessControlConditionHumbleModel(new ProcessControlCondition(), new NullLocalizationWrapper())
                {
                    TestLevelSet = new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(2) })
                },
                new ProcessControlConditionHumbleModel(new ProcessControlCondition(), new NullLocalizationWrapper())
                {
                    TestLevelSet = new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(id) })
                }
            });
            Assert.IsTrue(environment.viewModel.AssignTestLevelSetCommand.CanExecute(null));
        }

        [TestCase(5)]
        [TestCase(1)]
        [TestCase(100)]
        public void AssignTestLevelSetExecuteCallsUseCase(int idCount)
        {
            var environment = CreateViewModelEnvironment();
            var idList = new List<ProcessControlConditionId>();
            var random = new Random();

            for (int i = 0; i < idCount; i++)
            {
                idList.Add(new ProcessControlConditionId(random.Next()));
            }

            environment.viewModel.SelectedProcessControlsForTestLevelAssignment = new ObservableCollection<ProcessControlConditionHumbleModel>();
            foreach (var id in idList)
            {
                environment.viewModel.SelectedProcessControlsForTestLevelAssignment.Add(
                    new ProcessControlConditionHumbleModel(new ProcessControlCondition() { Id = id }, new NullLocalizationWrapper()));
            }
            var testLevelSetEntity = new TestLevelSet();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(testLevelSetEntity);

            environment.viewModel.AssignTestLevelSetCommand.Execute(null);
            Assert.AreSame(testLevelSetEntity, environment.testLevelSetAssignmentUseCase.AssignTestLevelSetToProcessControlConditionsParameterTestLevelSet);
            CheckerFunctions.CollectionAssertAreEquivalent(idList, environment.testLevelSetAssignmentUseCase.AssignTestLevelSetToProcessControlConditionsParameterProcessControlConditionIds,
                (x, y) => x.Equals(y));
            Assert.AreSame(environment.viewModel, environment.testLevelSetAssignmentUseCase.AssignTestLevelSetToProcessControlConditionsErrorHandler);
        }

        [Test]
        public void ResetTestLevelSetSelectionCanExecuteReturnsTrueIfATestLevelSetIsSelected()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            Assert.IsTrue(environment.viewModel.ResetTestLevelSetSelectionCommand.CanExecute(null));
        }

        [Test]
        public void ResetTestLevelSetSelectionCanExecuteReturnsFalseIfNoTestLevelSetIsSelected()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = null;
            Assert.IsFalse(environment.viewModel.ResetTestLevelSetSelectionCommand.CanExecute(null));
        }

        [Test]
        public void ResetTestLevelSetSelectionSetsSelectedTestLevelSetToNull()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.ResetTestLevelSetSelectionCommand.Execute(null);
            Assert.IsNull(environment.viewModel.SelectedTestLevelSet);
        }

        [Test]
        public void RemoveTestLevelSetAssignmentsCanExecuteReturnsTrueIfTestLevelSetAndProcessControlConditionAreSelected()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedProcessControlsForTestLevelAssignment = new ObservableCollection<ProcessControlConditionHumbleModel>(new List<ProcessControlConditionHumbleModel>()
            {
                new ProcessControlConditionHumbleModel(new ProcessControlCondition()
                {
                    TestLevelSet = new TestLevelSet()
                }, new NullLocalizationWrapper())
            });
            Assert.IsTrue(environment.viewModel.RemoveTestLevelSetAssignmentsCommand.CanExecute(null));
        }

        [Test]
        public void RemoveTestLevelSetAssignmentsCanExecuteReturnsTrueIfTestLevelSetAndProcessControlConditionsAreSelected()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedProcessControlsForTestLevelAssignment = new ObservableCollection<ProcessControlConditionHumbleModel>(new List<ProcessControlConditionHumbleModel>()
            {
                new ProcessControlConditionHumbleModel(new ProcessControlCondition()
                {
                    TestLevelSet = new TestLevelSet()
                }, new NullLocalizationWrapper()),
                new ProcessControlConditionHumbleModel(new ProcessControlCondition()
                {
                    TestLevelSet = new TestLevelSet()
                }, new NullLocalizationWrapper()),
                new ProcessControlConditionHumbleModel(new ProcessControlCondition()
                {
                    TestLevelSet = new TestLevelSet()
                }, new NullLocalizationWrapper())
            });
            Assert.IsTrue(environment.viewModel.RemoveTestLevelSetAssignmentsCommand.CanExecute(null));
        }

        [Test]
        public void RemoveTestLevelSetAssignmentsCanExecuteReturnsFalseIfNoProcessControlConditionSelected()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedProcessControlsForTestLevelAssignment = new ObservableCollection<ProcessControlConditionHumbleModel>(new List<ProcessControlConditionHumbleModel>());
            Assert.IsFalse(environment.viewModel.RemoveTestLevelSetAssignmentsCommand.CanExecute(null));
        }

        [Test]
        public void RemoveTestLevelSetAssignmentsCanExecuteReturnsFalseIfSelectedProcessControlConditionsIsNull()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedProcessControlsForTestLevelAssignment = null;
            Assert.IsFalse(environment.viewModel.RemoveTestLevelSetAssignmentsCommand.CanExecute(null));
        }

        [Test]
        public void RemoveTestLevelSetAssignmentsCanExecuteReturnsTrueIfTestLevelSetForSomeSelectedEntriesAreSet()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedProcessControlsForTestLevelAssignment = new ObservableCollection<ProcessControlConditionHumbleModel>(new List<ProcessControlConditionHumbleModel>()
            {
                new ProcessControlConditionHumbleModel(new ProcessControlCondition()
                {
                    TestLevelSet = new TestLevelSet()
                }, new NullLocalizationWrapper()),
                new ProcessControlConditionHumbleModel(new ProcessControlCondition()
                {
                    TestLevelSet = new TestLevelSet()
                }, new NullLocalizationWrapper())
            });
            Assert.IsTrue(environment.viewModel.RemoveTestLevelSetAssignmentsCommand.CanExecute(null));
        }

        [TestCase(5)]
        [TestCase(1)]
        [TestCase(100)]
        public void RemoveTestLevelSetAssignmentsExecuteCallsUseCase(int idCount)
        {
            var environment = CreateViewModelEnvironment();
            var idList = new List<ProcessControlConditionId>();
            var random = new Random();

            for (int i = 0; i < idCount; i++)
            {
                idList.Add(new ProcessControlConditionId(random.Next()));
            }

            environment.viewModel.SelectedProcessControlsForTestLevelAssignment = new ObservableCollection<ProcessControlConditionHumbleModel>();
            foreach (var id in idList)
            {
                environment.viewModel.SelectedProcessControlsForTestLevelAssignment.Add(
                    new ProcessControlConditionHumbleModel(new ProcessControlCondition() { Id = id }, new NullLocalizationWrapper()));
            }

            environment.viewModel.RemoveTestLevelSetAssignmentsCommand.Execute(null);
            CheckerFunctions.CollectionAssertAreEquivalent(idList, environment.testLevelSetAssignmentUseCase.RemoveTestLevelSetAssignmentForProcessControlParameter,
                (x, y) => x.Equals(y));
            Assert.AreSame(environment.viewModel, environment.testLevelSetAssignmentUseCase.RemoveTestLevelSetAssignmentForErrorHandler);
        }

        [Test]
        public void ShowTestLevelSetAssignmentErrorInvokesMessageRequest()
        {
            var environment = CreateViewModelEnvironment();
            bool requestInvoked = false;
            environment.viewModel.MessageBoxRequest += (s, e) => requestInvoked = true;
            environment.viewModel.ShowTestLevelSetAssignmentError();
            Assert.IsTrue(requestInvoked);
        }

        [Test]
        public void ShowDiffDialogDontShowChangesIfProcessControlConditionsHaveNoChanges()
        {
            var environment = CreateViewModelEnvironment();
            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) => showDialogRequestInvoked = true;

            var processControlCondition = CreateProcessControlCondition.Anonymous();
            var diff = new ProcessControlConditionDiff(null, null, processControlCondition, processControlCondition);
            environment.viewModel.SaveProcessControl(new List<ProcessControlConditionDiff>() { diff }, () => { });

            Assert.IsFalse(showDialogRequestInvoked);
        }

        [TestCase(4)]
        [TestCase(9)]
        [TestCase(4)]
        [TestCase(9)]
        public void ShowDiffDialogWithNoResultTest(int oldVal)
        {
            var environment = CreateViewModelEnvironment();
            var condition = new ProcessControlConditionHumbleModel(new ProcessControlCondition() 
            { 
                Id = new ProcessControlConditionId(10), 
                TestLevelSet = new TestLevelSet()
            }, new NullLocalizationWrapper());
            environment.viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            environment.viewModel.ChangedTestLevelNumbersAssignments.Add(condition, oldVal);

            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };

            environment.viewModel.SaveProcessControl(new List<ProcessControlConditionDiff>() { new ProcessControlConditionDiff(null, null,
                new ProcessControlCondition()
                {
                    Id = new ProcessControlConditionId(1),
                    TestLevelNumber = 5
                },
                new ProcessControlCondition()
                {
                    Id = new ProcessControlConditionId(2),
                    TestLevelNumber = 6
                })
            }, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.AreEqual(0, environment.viewModel.ChangedTestLevelNumbersAssignments.Count);
            Assert.AreEqual(oldVal, condition.TestLevelNumber);
        }

        [Test]
        public void ShowDiffDialogForWithYesResultCallsFinishedAction()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);

            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;
            };

            var conditionOld = CreateProcessControlCondition.Anonymous();
            var conditionNew = CreateProcessControlCondition.Anonymous();
            conditionNew.TestLevelNumber = 465465465;

            var diff = new ProcessControlConditionDiff(null, null, conditionOld, conditionNew);
            var finishedActionCalled = false;
            environment.viewModel.SaveProcessControl(new List<ProcessControlConditionDiff>() { diff }, () => { finishedActionCalled = true; });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(finishedActionCalled);
        }

        [Test]
        public void ShowDiffDialogShowsDiffs()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var diff1 = new ProcessControlConditionDiff(null, null,
                new ProcessControlCondition()
                {
                    TestLevelNumber = 5
                },
                new ProcessControlCondition()
                {
                    TestLevelNumber = 6
                });
            var diff2 = new ProcessControlConditionDiff(null, null,
                new ProcessControlCondition()
                {
                    TestLevelNumber = 8
                },
                new ProcessControlCondition()
                {
                    TestLevelNumber = 9
                });

            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) =>
            {
                var changes = e.ChangedValues;
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;

                Assert.AreEqual(changes.Count, 2);
                Assert.AreEqual(changes[0].OldValue, diff1.GetOldProcessControlCondition().TestLevelNumber.ToString());
                Assert.AreEqual(changes[0].NewValue, diff1.GetNewProcessControlCondition().TestLevelNumber.ToString());
                Assert.AreEqual(changes[1].OldValue, diff2.GetOldProcessControlCondition().TestLevelNumber.ToString());
                Assert.AreEqual(changes[1].NewValue, diff2.GetNewProcessControlCondition().TestLevelNumber.ToString());
            };

            environment.viewModel.SaveProcessControl(new List<ProcessControlConditionDiff>() { diff1, diff2 }, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
        }

        [Test]
        public void SaveProcessControlFillsChangedTestLevelNumbersAssignmentsOnCancel()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.RequestVerifyChangesView += (s, e) => e.Result = System.Windows.MessageBoxResult.Cancel;
            environment.viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);

            var new1 = new ProcessControlCondition() { Id = new ProcessControlConditionId(1) };
            var new2 = new ProcessControlCondition() { Id = new ProcessControlConditionId(2) };
            environment.viewModel.SaveProcessControl(new List<ProcessControlConditionDiff>()
            {
                new ProcessControlConditionDiff(null, null, new ProcessControlCondition() { Id = new ProcessControlConditionId(1), TestLevelNumber = 5 }, new1),
                new ProcessControlConditionDiff(null, null, new ProcessControlCondition() { Id = new ProcessControlConditionId(2), TestLevelNumber = 9 }, new2)
            },
            () => { });

            Assert.AreSame(new1, environment.viewModel.ChangedTestLevelNumbersAssignments.Keys.ToList()[0].Entity);
            Assert.AreEqual(5, environment.viewModel.ChangedTestLevelNumbersAssignments.Values.ToList()[0]);
            Assert.AreSame(new2, environment.viewModel.ChangedTestLevelNumbersAssignments.Keys.ToList()[1].Entity);
            Assert.AreEqual(9, environment.viewModel.ChangedTestLevelNumbersAssignments.Values.ToList()[1]);
        }


        static Environment CreateViewModelEnvironment()
        {
            var environment = new Environment();
            environment.testLevelSetUseCase = new TestLevelSetUseCaseMock();
            environment.testLevelSetAssignmentUseCase = new TestLevelSetAssignmentUseCaseMock();
            environment.testLevelSetInterface = new TestLevelSetInterfaceMock();
            environment.testLevelSetAssignmentInterface = new TestLevelSetAssignmentInterfaceMock();
            environment.processControlUseCase = new ProcessControlUseCaseMock();
            environment.viewModel = new ProcessControlPlanningViewModel(environment.testLevelSetAssignmentUseCase, 
                environment.testLevelSetAssignmentInterface, 
                environment.testLevelSetUseCase, 
                environment.testLevelSetInterface,
                new ShiftManagementUseCaseMock(),
                new ShiftManagementInterfaceMock(),
                environment.processControlUseCase,
                new ProcessControlViewModelTest.ProcessControlInterfaceAdapterMock(),
                new NullLocalizationWrapper(),
                new MockLocationDisplayFormatter(),
                new StartUpMock());

            return environment;
        }

        struct Environment
        {
            public ProcessControlPlanningViewModel viewModel;
            public TestLevelSetUseCaseMock testLevelSetUseCase;
            public TestLevelSetAssignmentUseCaseMock testLevelSetAssignmentUseCase;
            public TestLevelSetInterfaceMock testLevelSetInterface;
            public TestLevelSetAssignmentInterfaceMock testLevelSetAssignmentInterface;
            public ProcessControlUseCaseMock processControlUseCase;
        }
    }
}
