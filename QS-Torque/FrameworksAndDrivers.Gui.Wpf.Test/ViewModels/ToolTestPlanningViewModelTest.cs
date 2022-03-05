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

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    class TestLevelSetAssignmentUseCaseMock : ITestLevelSetAssignmentUseCase
    {
        public bool LoadLocationToolAssignmentsCalled { get; set; }
        public TestLevelSet AssignTestLevelSetToLocationToolAssignmentsParameterTestLevelSet { get; set; }
        public List<(LocationToolAssignmentId, TestType)> AssignTestLevelSetToLocationToolAssignmentsParameterLocationToolAssignmentIds { get; set; }
        public List<(LocationToolAssignmentId, TestType)> RemoveTestLevelSetAssignmentForParameter { get; set; }
        public TestLevelSet AssignTestLevelSetToProcessControlConditionsParameterTestLevelSet { get; set; }
        public List<ProcessControlConditionId> AssignTestLevelSetToProcessControlConditionsParameterProcessControlConditionIds { get; set; }
        public List<ProcessControlConditionId> RemoveTestLevelSetAssignmentForProcessControlParameter { get; set; }

        public ITestLevelSetAssignmentErrorHandler LoadLocationToolAssignmentsErrorHandler { get; set; }
        public ITestLevelSetAssignmentErrorHandler RemoveTestLevelSetAssignmentForErrorHandler { get; set; }
        public ITestLevelSetAssignmentErrorHandler AssignTestLevelSetToLocationToolAssignmentsErrorHandler { get; set; }
        public ITestLevelSetAssignmentErrorHandler AssignTestLevelSetToProcessControlConditionsErrorHandler { get; set; }

        public void LoadLocationToolAssignments(ITestLevelSetAssignmentErrorHandler errorHandler)
        {
            LoadLocationToolAssignmentsCalled = true;
            LoadLocationToolAssignmentsErrorHandler = errorHandler;
        }

        public void RemoveTestLevelSetAssignmentFor(List<(LocationToolAssignmentId, TestType)> ids, ITestLevelSetAssignmentErrorHandler errorHandler)
        {
            RemoveTestLevelSetAssignmentForParameter = ids;
            RemoveTestLevelSetAssignmentForErrorHandler = errorHandler;
        }

        public void AssignTestLevelSetToLocationToolAssignments(TestLevelSet testLevelSet, List<(LocationToolAssignmentId, TestType)> locationToolAssignmentIds, ITestLevelSetAssignmentErrorHandler errorHandler)
        {
            AssignTestLevelSetToLocationToolAssignmentsParameterTestLevelSet = testLevelSet;
            AssignTestLevelSetToLocationToolAssignmentsParameterLocationToolAssignmentIds = locationToolAssignmentIds;
            AssignTestLevelSetToLocationToolAssignmentsErrorHandler = errorHandler;
        }

        public void AssignTestLevelSetToProcessControlConditions(TestLevelSet testLevelSet, List<ProcessControlConditionId> processControlConditionIds, ITestLevelSetAssignmentErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler)
        {
            AssignTestLevelSetToProcessControlConditionsParameterTestLevelSet = testLevelSet;
            AssignTestLevelSetToProcessControlConditionsParameterProcessControlConditionIds = processControlConditionIds;
            AssignTestLevelSetToProcessControlConditionsErrorHandler = errorHandler;
        }

        public void RemoveTestLevelSetAssignmentFor(List<ProcessControlConditionId> ids, ITestLevelSetAssignmentErrorHandler errorHandler)
        {
            RemoveTestLevelSetAssignmentForProcessControlParameter = ids;
            RemoveTestLevelSetAssignmentForErrorHandler = errorHandler;
        }
    }

    class TestLevelSetAssignmentInterfaceMock : ITestLevelSetAssignmentInterface
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<LocationToolAssignmentModelWithTestType> LocationToolAssignments { get; }
        public ObservableCollection<LocationToolAssignmentModelWithTestType> SelectedLocationToolAssignments { get; set; } = new ObservableCollection<LocationToolAssignmentModelWithTestType>();
        public TestLevelSetModel SelectedTestLevelSet { get; set; }
        public event EventHandler TestLevelSetAssignmentErrorRequest;
    }

    class LocationToolAssignmentInterfaceMock : ILocationToolAssignmentInterface
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<LocationToolAssignmentModel> LocationToolAssignments { get; } = new ObservableCollection<LocationToolAssignmentModel>();
        public ObservableCollection<LocationToolAssignmentModelWithTestType> LocationToolAssignmentsWithTestTypes { get; set; }
        public event EventHandler LocationToolAssignmentErrorRequest;
    }

    public class ToolTestPlanningViewModelTest
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
        public void LoadedCommandCallsTestLevelSetAssignmentUseCase()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.LoadedCommand.Execute(null);
            Assert.IsTrue(environment.testLevelSetAssignmentUseCase.LoadLocationToolAssignmentsCalled);
            Assert.AreSame(environment.viewModel, environment.testLevelSetAssignmentUseCase.LoadLocationToolAssignmentsErrorHandler);
        }

        [Test]
        public void LoadedCommandCallsLocationToolAssignmentUseCase()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.LoadedCommand.Execute(null);
            Assert.IsTrue(environment.locationToolAssignmentUseCase.LoadLocationToolAssignmentsCalled);
            Assert.AreSame(environment.viewModel, environment.testLevelSetAssignmentUseCase.LoadLocationToolAssignmentsErrorHandler);
        }

        [Test]
        public void LoadedCommandClearsSelectedLocationToolAssignmentsForTestLevelAssignment()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment.Add(new LocationToolAssignmentModelWithTestType(new LocationToolAssignment(), new NullLocalizationWrapper()));
            environment.viewModel.LoadedCommand.Execute(null);
            Assert.AreEqual(0, environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment.Count);
        }

        [Test]
        public void AssignTestLevelSetCanExecuteReturnsTrueIfTestLevelSetAndLocationToolAssignmentAreSelected()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment = new ObservableCollection<LocationToolAssignmentModelWithTestType>(new List<LocationToolAssignmentModelWithTestType>()
            {
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment(), new NullLocalizationWrapper())
            });
            Assert.IsTrue(environment.viewModel.AssignTestLevelSetCommand.CanExecute(null));
        }

        [Test]
        public void AssignTestLevelSetCanExecuteReturnsTrueIfTestLevelSetAndLocationToolAssignmentsAreSelected()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment = new ObservableCollection<LocationToolAssignmentModelWithTestType>(new List<LocationToolAssignmentModelWithTestType>()
            {
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment(), new NullLocalizationWrapper()),
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment(), new NullLocalizationWrapper()),
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment(), new NullLocalizationWrapper())
            });
            Assert.IsTrue(environment.viewModel.AssignTestLevelSetCommand.CanExecute(null));
        }

        [Test]
        public void AssignTestLevelSetCanExecuteReturnsFalseIfNoTestLevelSetSelected()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = null;
            environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment = new ObservableCollection<LocationToolAssignmentModelWithTestType>(new List<LocationToolAssignmentModelWithTestType>()
            {
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment(), new NullLocalizationWrapper())
            });
            Assert.IsFalse(environment.viewModel.AssignTestLevelSetCommand.CanExecute(null));
        }

        [Test]
        public void AssignTestLevelSetCanExecuteReturnsFalseIfNoLocationToolAssignmentSelected()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment = new ObservableCollection<LocationToolAssignmentModelWithTestType>(new List<LocationToolAssignmentModelWithTestType>());
            Assert.IsFalse(environment.viewModel.AssignTestLevelSetCommand.CanExecute(null));
        }

        [Test]
        public void AssignTestLevelSetCanExecuteReturnsFalseIfSelectedLocationToolAssignmentsAreNull()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment = null;
            Assert.IsFalse(environment.viewModel.AssignTestLevelSetCommand.CanExecute(null));
        }

        [TestCase(5)]
        [TestCase(6)]
        public void AssignTestLevelSetCanExecuteReturnsFalseIfSelectedLocationToolAssignmentsIsAlreadyAssignedToSelectedTestLevelSetMfu(long id)
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(id) });
            environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment = new ObservableCollection<LocationToolAssignmentModelWithTestType>(new List<LocationToolAssignmentModelWithTestType>()
            {
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment(), new NullLocalizationWrapper())
                {
                    TestLevelSetMfu = new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(id) }),
                    TestType = TestType.Mfu
                }
            });
            Assert.IsFalse(environment.viewModel.AssignTestLevelSetCommand.CanExecute(null));
        }

        [TestCase(5)]
        [TestCase(6)]
        public void AssignTestLevelSetCanExecuteReturnsFalseIfSelectedLocationToolAssignmentsIsAlreadyAssignedToSelectedTestLevelSetChk(long id)
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(id) });
            environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment = new ObservableCollection<LocationToolAssignmentModelWithTestType>(new List<LocationToolAssignmentModelWithTestType>()
            {
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment(), new NullLocalizationWrapper())
                {
                    TestLevelSetChk = new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(id) }),
                    TestType = TestType.Chk
                }
            });
            Assert.IsFalse(environment.viewModel.AssignTestLevelSetCommand.CanExecute(null));
        }

        [TestCase(5)]
        [TestCase(6)]
        public void AssignTestLevelSetCanExecuteReturnsTrueIfOneSelectedLocationToolAssignmentsIsAlreadyAssignedToSelectedTestLevelSetMfu(long id)
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(id) });
            environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment = new ObservableCollection<LocationToolAssignmentModelWithTestType>(new List<LocationToolAssignmentModelWithTestType>()
            {
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment(), new NullLocalizationWrapper())
                {
                    TestLevelSetMfu = new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(2) }),
                    TestType = TestType.Mfu
                },
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment(), new NullLocalizationWrapper())
                {
                    TestLevelSetMfu = new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(id) }),
                    TestType = TestType.Mfu
                }
            });
            Assert.IsTrue(environment.viewModel.AssignTestLevelSetCommand.CanExecute(null));
        }

        [TestCase(5, TestType.Chk)]
        [TestCase(1, TestType.Chk)]
        [TestCase(100, TestType.Mfu)]
        public void AssignTestLevelSetExecuteCallsUseCase(int locToolIdCount, TestType testType)
        {
            var environment = CreateViewModelEnvironment();
            var locToolIdList = new List<LocationToolAssignmentId>();
            var random = new Random();

            for (int i = 0; i < locToolIdCount; i++)
            {
                locToolIdList.Add(new LocationToolAssignmentId(random.Next()));
            }

            foreach (var id in locToolIdList)
            {
                environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment.Add(
                    new LocationToolAssignmentModelWithTestType(new LocationToolAssignment() { Id = id }, new NullLocalizationWrapper())
                    {
                        TestType = testType
                    });
            }
            var testLevelSetEntity = new TestLevelSet();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(testLevelSetEntity);

            environment.viewModel.AssignTestLevelSetCommand.Execute(null);
            Assert.AreSame(testLevelSetEntity, environment.testLevelSetAssignmentUseCase.AssignTestLevelSetToLocationToolAssignmentsParameterTestLevelSet);
            CheckerFunctions.CollectionAssertAreEquivalent(locToolIdList, environment.testLevelSetAssignmentUseCase.AssignTestLevelSetToLocationToolAssignmentsParameterLocationToolAssignmentIds,
                (x, y) => x.Equals(y.Item1) && y.Item2 == testType);
            Assert.AreSame(environment.viewModel, environment.testLevelSetAssignmentUseCase.AssignTestLevelSetToLocationToolAssignmentsErrorHandler);
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
        public void RemoveTestLevelSetAssignmentsCanExecuteReturnsTrueIfTestLevelSetAndLocationToolAssignmentAreSelected()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment = new ObservableCollection<LocationToolAssignmentModelWithTestType>(new List<LocationToolAssignmentModelWithTestType>()
            {
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment()
                {
                    TestLevelSetMfu = new TestLevelSet(),
                    TestLevelSetChk = new TestLevelSet()
                }, new NullLocalizationWrapper())
            });
            Assert.IsTrue(environment.viewModel.RemoveTestLevelSetAssignmentsCommand.CanExecute(null));
        }

        [Test]
        public void RemoveTestLevelSetAssignmentsCanExecuteReturnsTrueIfTestLevelSetAndLocationToolAssignmentsAreSelected()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment = new ObservableCollection<LocationToolAssignmentModelWithTestType>(new List<LocationToolAssignmentModelWithTestType>()
            {
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment()
                {
                    TestLevelSetMfu = new TestLevelSet(),
                    TestLevelSetChk = new TestLevelSet()
                }, new NullLocalizationWrapper()),
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment()
                {
                    TestLevelSetMfu = new TestLevelSet(),
                    TestLevelSetChk = new TestLevelSet()
                }, new NullLocalizationWrapper()),
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment()
                {
                    TestLevelSetMfu = new TestLevelSet(),
                    TestLevelSetChk = new TestLevelSet()
                }, new NullLocalizationWrapper())
            });
            Assert.IsTrue(environment.viewModel.RemoveTestLevelSetAssignmentsCommand.CanExecute(null));
        }

        [Test]
        public void RemoveTestLevelSetAssignmentsCanExecuteReturnsFalseIfNoLocationToolAssignmentSelected()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment = new ObservableCollection<LocationToolAssignmentModelWithTestType>(new List<LocationToolAssignmentModelWithTestType>());
            Assert.IsFalse(environment.viewModel.RemoveTestLevelSetAssignmentsCommand.CanExecute(null));
        }

        [Test]
        public void RemoveTestLevelSetAssignmentsCanExecuteReturnsFalseIfSelectedLocationToolAssignmentsIsNull()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment = null;
            Assert.IsFalse(environment.viewModel.RemoveTestLevelSetAssignmentsCommand.CanExecute(null));
        }

        [Test]
        public void RemoveTestLevelSetAssignmentsCanExecuteReturnsFalseIfTestLevelSetForChkAreNotSet()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment = new ObservableCollection<LocationToolAssignmentModelWithTestType>(new List<LocationToolAssignmentModelWithTestType>()
            {
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment()
                {
                    TestLevelSetMfu = new TestLevelSet()
                }, new NullLocalizationWrapper())
                {
                    TestType = TestType.Chk
                }
            });
            Assert.IsFalse(environment.viewModel.RemoveTestLevelSetAssignmentsCommand.CanExecute(null));
        }

        [Test]
        public void RemoveTestLevelSetAssignmentsCanExecuteReturnsFalseIfTestLevelSetForMfuAreNotSet()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment = new ObservableCollection<LocationToolAssignmentModelWithTestType>(new List<LocationToolAssignmentModelWithTestType>()
            {
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment()
                {
                    TestLevelSetChk = new TestLevelSet()
                }, new NullLocalizationWrapper())
                {
                    TestType = TestType.Mfu
                }
            });
            Assert.IsFalse(environment.viewModel.RemoveTestLevelSetAssignmentsCommand.CanExecute(null));
        }

        [Test]
        public void RemoveTestLevelSetAssignmentsCanExecuteReturnsTrueIfTestLevelSetForSomeSelectedEntriesAreSet()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment = new ObservableCollection<LocationToolAssignmentModelWithTestType>(new List<LocationToolAssignmentModelWithTestType>()
            {
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment()
                {
                    TestLevelSetChk = new TestLevelSet()
                }, new NullLocalizationWrapper())
                {
                    TestType = TestType.Mfu
                },
                new LocationToolAssignmentModelWithTestType(new LocationToolAssignment()
                {
                    TestLevelSetMfu = new TestLevelSet(),
                    TestLevelSetChk = new TestLevelSet()
                }, new NullLocalizationWrapper())
                {
                    TestType = TestType.Mfu
                }
            });
            Assert.IsTrue(environment.viewModel.RemoveTestLevelSetAssignmentsCommand.CanExecute(null));
        }

        [TestCase(5, TestType.Mfu)]
        [TestCase(1, TestType.Mfu)]
        [TestCase(100, TestType.Chk)]
        public void RemoveTestLevelSetAssignmentsExecuteCallsUseCase(int locToolIdCount, TestType testType)
        {
            var environment = CreateViewModelEnvironment();
            var locToolIdList = new List<LocationToolAssignmentId>();
            var random = new Random();

            for (int i = 0; i < locToolIdCount; i++)
            {
                locToolIdList.Add(new LocationToolAssignmentId(random.Next()));
            }

            foreach (var id in locToolIdList)
            {
                environment.viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment.Add(
                    new LocationToolAssignmentModelWithTestType(new LocationToolAssignment() {Id = id}, new NullLocalizationWrapper())
                    {
                        TestType = testType
                    });
            }

            environment.viewModel.RemoveTestLevelSetAssignmentsCommand.Execute(null);
            CheckerFunctions.CollectionAssertAreEquivalent(locToolIdList, environment.testLevelSetAssignmentUseCase.RemoveTestLevelSetAssignmentForParameter,
                (x, y) => x.Equals(y.Item1) && y.Item2 == testType);
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

        [TestCase(true, true, false, false, false)]
        [TestCase(false, false, false, false, false)]
        [TestCase(false, true, true, false, false)]
        [TestCase(false, true, false, true, false)]
        [TestCase(false, true, false, false, true)]
        public void McaLocationToolAssignmentsForOverviewFilter(bool isTestLevelSetMfuNull, bool testOperationActiveMfu, bool isTestParametersNull, bool isTestTechniqueNull, bool expectedResult)
        {
            var environment = CreateViewModelEnvironment();
            var model = new LocationToolAssignmentModel(new LocationToolAssignment()
            {
                Id = new LocationToolAssignmentId(0),
                TestLevelSetMfu = isTestLevelSetMfuNull ? null : new TestLevelSet(),
                TestOperationActiveMfu = testOperationActiveMfu,
                TestParameters = isTestParametersNull ? null : new Core.Entities.TestParameters(),
                TestTechnique = isTestTechniqueNull ? null : new TestTechnique()
            },
            new NullLocalizationWrapper());
            Assert.AreEqual(expectedResult, environment.viewModel.McaLocationToolAssignmentsForOverview.Filter(model));
        }

        [TestCase(true, true, false, false, false)]
        [TestCase(false, false, false, false, false)]
        [TestCase(false, true, true, false, false)]
        [TestCase(false, true, false, true, false)]
        [TestCase(false, true, false, false, true)]
        public void ChkLocationToolAssignmentsForOverviewFilter(bool isTestLevelSetChkNull, bool testOperationActiveChk, bool isTestParametersNull, bool isTestTechniqueNull, bool expectedResult)
        {
            var environment = CreateViewModelEnvironment();
            var model = new LocationToolAssignmentModel(new LocationToolAssignment()
                {
                    Id = new LocationToolAssignmentId(0),
                    TestLevelSetChk = isTestLevelSetChkNull ? null : new TestLevelSet(),
                    TestOperationActiveChk = testOperationActiveChk,
                    TestParameters = isTestParametersNull ? null : new Core.Entities.TestParameters(),
                    TestTechnique = isTestTechniqueNull ? null : new TestTechnique()
                },
                new NullLocalizationWrapper());
            Assert.AreEqual(expectedResult, environment.viewModel.ChkLocationToolAssignmentsForOverview.Filter(model));
        }

        [Test]
        public void ShowDiffDialogDontShowChangesIfLocationToolAssignmentsHaveNoChanges()
        {
            var environment = CreateViewModelEnvironment();
            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) => showDialogRequestInvoked = true;

            var locTool = CreateLocationToolAssignment.Anonymous();
            var diff = new LocationToolAssignmentDiff() { OldLocationToolAssignment = locTool, NewLocationToolAssignment = locTool };
            environment.viewModel.ShowDiffDialog(new List<LocationToolAssignmentDiff>() { diff }, () => { });

            Assert.IsFalse(showDialogRequestInvoked);
        }

        [TestCase(4, TestType.Chk)]
        [TestCase(9, TestType.Chk)]
        [TestCase(4, TestType.Mfu)]
        [TestCase(9, TestType.Mfu)]
        public void ShowDiffDialogForShiftManagementWithNoResultTest(int oldVal, TestType testType)
        {
            var environment = CreateViewModelEnvironment();
            var locTool = new LocationToolAssignmentModelWithTestType(new LocationToolAssignment(), new NullLocalizationWrapper())
            {
                TestType = testType
            };
            environment.viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            environment.viewModel.ChangedTestLevelNumbersAssignments.Add(locTool, oldVal);

            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };

            environment.viewModel.ShowDiffDialog(new List<LocationToolAssignmentDiff>() { new LocationToolAssignmentDiff() 
                {
                    OldLocationToolAssignment = new LocationToolAssignment()
                    {
                        TestLevelNumberChk = 5
                    },
                    NewLocationToolAssignment = new LocationToolAssignment()
                    {
                        TestLevelNumberChk = 6
                    }
                }
            }, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.AreEqual(0, environment.viewModel.ChangedTestLevelNumbersAssignments.Count);
            if (testType == TestType.Mfu)
            {
                Assert.AreEqual(oldVal, locTool.TestLevelNumberMfu); 
            }
            else if(testType == TestType.Chk)
            {
                Assert.AreEqual(oldVal, locTool.TestLevelNumberChk);
            }
        }

        [Test]
        public void ShowDiffDialogForShiftManagementWithYesResultCallsFinishedAction()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);

            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;
            };

            var locToolOld = CreateLocationToolAssignment.Anonymous();
            var locToolNew = CreateLocationToolAssignment.Anonymous();
            locToolNew.TestLevelNumberChk = 465465465;

            var diff = new LocationToolAssignmentDiff() { OldLocationToolAssignment = locToolOld, NewLocationToolAssignment = locToolNew };
            var finishedActionCalled = false;
            environment.viewModel.ShowDiffDialog(new List<LocationToolAssignmentDiff>() { diff }, () => { finishedActionCalled = true; });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(finishedActionCalled);
        }

        [Test]
        public void ShowDiffDialogShowsShiftManagementDiffs()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var mfuDiff = new LocationToolAssignmentDiff()
            {
                OldLocationToolAssignment = new LocationToolAssignment()
                {
                    TestLevelNumberMfu = 5
                },
                NewLocationToolAssignment = new LocationToolAssignment()
                {
                    TestLevelNumberMfu = 6
                }
            };
            var chkDiff = new LocationToolAssignmentDiff()
            {
                OldLocationToolAssignment = new LocationToolAssignment()
                {
                    TestLevelNumberChk = 8
                },
                NewLocationToolAssignment = new LocationToolAssignment()
                {
                    TestLevelNumberChk = 9
                }
            };

            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) =>
            {
                var changes = e.ChangedValues;
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;

                Assert.AreEqual(changes.Count, 2);
                Assert.AreEqual(changes[0].OldValue, mfuDiff.OldLocationToolAssignment.TestLevelNumberMfu.ToString());
                Assert.AreEqual(changes[0].NewValue, mfuDiff.NewLocationToolAssignment.TestLevelNumberMfu.ToString());
                Assert.AreEqual(changes[1].OldValue, chkDiff.OldLocationToolAssignment.TestLevelNumberChk.ToString());
                Assert.AreEqual(changes[1].NewValue, chkDiff.NewLocationToolAssignment.TestLevelNumberChk.ToString());
            };

            environment.viewModel.ShowDiffDialog(new List<LocationToolAssignmentDiff>() { mfuDiff, chkDiff }, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
        }

        [Test]
        public void LocationToolAssignmentErrorInvokesMessageRequest()
        {
            var environment = CreateViewModelEnvironment();
            bool requestInvoked = false;
            environment.viewModel.MessageBoxRequest += (s, e) => requestInvoked = true;
            environment.viewModel.ShowLocationToolAssignmentError();
            Assert.IsTrue(requestInvoked);
        }


        static Environment CreateViewModelEnvironment()
        {
            var environment = new Environment();
            environment.testLevelSetUseCase = new TestLevelSetUseCaseMock();
            environment.testLevelSetAssignmentUseCase = new TestLevelSetAssignmentUseCaseMock();
            environment.testLevelSetInterface = new TestLevelSetInterfaceMock();
            environment.testLevelSetAssignmentInterface = new TestLevelSetAssignmentInterfaceMock();
            environment.locationToolAssignmentUseCase = new LocationToolAssignmentUseCaseMock();
            environment.locationToolAssignmentInterface = new LocationToolAssignmentInterfaceMock();
            environment.viewModel = new ToolTestPlanningViewModel(environment.testLevelSetAssignmentUseCase, 
                environment.testLevelSetAssignmentInterface, 
                environment.testLevelSetUseCase, 
                environment.testLevelSetInterface,
                environment.locationToolAssignmentUseCase,
                environment.locationToolAssignmentInterface,
                new NullLocalizationWrapper(),
                new MockLocationToolAssignmentDisplayFormatter(),
                new ShiftManagementUseCaseMock(),
                new ShiftManagementInterfaceMock(),
                new StartUpMock());

            return environment;
        }

        struct Environment
        {
            public ToolTestPlanningViewModel viewModel;
            public TestLevelSetUseCaseMock testLevelSetUseCase;
            public TestLevelSetAssignmentUseCaseMock testLevelSetAssignmentUseCase;
            public TestLevelSetInterfaceMock testLevelSetInterface;
            public TestLevelSetAssignmentInterfaceMock testLevelSetAssignmentInterface;
            public LocationToolAssignmentUseCaseMock locationToolAssignmentUseCase;
            public LocationToolAssignmentInterfaceMock locationToolAssignmentInterface;
        }
    }
}
