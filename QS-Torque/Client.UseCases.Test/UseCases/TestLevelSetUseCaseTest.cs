using System;
using System.Collections.Generic;
using Client.Core.Diffs;
using Client.Core.Entities;
using Client.TestHelper.Mock;
using Client.UseCases.Test.UseCases;
using Client.UseCases.UseCases;
using Core.Entities;
using Core.UseCases;
using NUnit.Framework;
using TestHelper.Mock;

namespace Core.Test.UseCases
{
    class TestLevelSetGuiMock : ITestLevelSetGui
    {
        public List<TestLevelSet> LoadTestLevelSetsParameter { get; set; }
        public TestLevelSet AddTestLevelSetParameter { get; set; }
        public TestLevelSet RemoveTestLevelSetParameter { get; set; }
        public TestLevelSetDiff UpdateTestLevelSetParameter { get; set; }

        public void LoadTestLevelSets(List<TestLevelSet> testLevelSets)
        {
            LoadTestLevelSetsParameter = testLevelSets;
        }

        public void AddTestLevelSet(TestLevelSet newItem)
        {
            AddTestLevelSetParameter = newItem;
        }

        public void RemoveTestLevelSet(TestLevelSet oldItem)
        {
            RemoveTestLevelSetParameter = oldItem;
        }

        public void UpdateTestLevelSet(TestLevelSetDiff diff)
        {
            UpdateTestLevelSetParameter = diff;
        }
    }
    
    class TestDateCalculationUseCaseMock : ITestDateCalculationUseCase
    {
        public List<LocationToolAssignmentId> CalculateToolTestDateForParamter { get; set; }
        public TestLevelSetId CalculateToolTestDateForTestLevelSetParameter { get; set; }
        public bool CalculateToolTestDateForAllLocationToolAssignmentsCalled { get; set; }
        public List<ProcessControlConditionId> CalculateProcessControlDateForParameter { get; set; }
        public TestLevelSetId CalculateProcessControlDateForTestLevelSetParameter { get; set; }
        public bool CalculateProcessControlDateForAllProcessControlConditionsCalled { get; set; }

        public void CalculateToolTestDateFor(List<LocationToolAssignmentId> ids)
        {
            CalculateToolTestDateForParamter = ids;
        }

        public void CalculateToolTestDateForTestLevelSet(TestLevelSetId id)
        {
            CalculateToolTestDateForTestLevelSetParameter = id;
        }

        public void CalculateToolTestDateForAllLocationToolAssignments()
        {
            CalculateToolTestDateForAllLocationToolAssignmentsCalled = true;
        }

        public void CalculateProcessControlDateFor(List<ProcessControlConditionId> ids)
        {
            CalculateProcessControlDateForParameter = ids;
        }

        public void CalculateProcessControlDateForTestLevelSet(TestLevelSetId id)
        {
            CalculateProcessControlDateForTestLevelSetParameter = id;
        }

        public void CalculateProcessControlDateForAllProcessControlConditions()
        {
            CalculateProcessControlDateForAllProcessControlConditionsCalled = true;
        }
    }

    class TestLevelSetDataMock : ITestLevelSetData
    {
        public List<TestLevelSet> LoadTestLevelSetsReturnValue { get; set; }
        public TestLevelSetDiff AddTestLevelSetParameter { get; set; }
        public TestLevelSetDiff RemoveTestLevelSetParameter { get; set; }
        public TestLevelSetDiff UpdateTestLevelSetParameter { get; set; }
        public string IsTestLevelSetNameUniqueParameter { get; set; }
        public bool IsTestLevelSetNameUniqueReturnValue { get; set; }
        public TestLevelSet DoesTestLevelSetHaveReferencesParameter { get; set; }
        public bool DoesTestLevelSetHaveReferencesReturnValue { get; set; }
        public TestLevelSet AddTestLevelSetReturnValue { get; set; }

        public bool LoadTestLevelSetsThrowsError { get; set; }
        public bool AddTestLevelSetThrowsError { get; set; }
        public bool RemoveTestLevelSetThrowsError { get; set; }
        public bool UpdateTestLevelSetThrowsError { get; set; }
        public bool IsTestLevelSetNameUniqueThrowsError { get; set; }
        public bool DoesTestLevelSetHaveReferencesThrowsError { get; set; }

        public List<TestLevelSet> LoadTestLevelSets()
        {
            if(LoadTestLevelSetsThrowsError)
            {
                throw new Exception();
            }

            return LoadTestLevelSetsReturnValue;
        }

        public TestLevelSet AddTestLevelSet(TestLevelSetDiff diff)
        {
            if (AddTestLevelSetThrowsError)
            {
                throw new Exception();
            }

            AddTestLevelSetParameter = diff;
            return AddTestLevelSetReturnValue;
        }

        public void RemoveTestLevelSet(TestLevelSetDiff diff)
        {
            if (RemoveTestLevelSetThrowsError)
            {
                throw new Exception();
            }

            RemoveTestLevelSetParameter = diff;
        }

        public void UpdateTestLevelSet(TestLevelSetDiff diff)
        {
            if (UpdateTestLevelSetThrowsError)
            {
                throw new Exception();
            }

            UpdateTestLevelSetParameter = diff;
        }

        public bool IsTestLevelSetNameUnique(string name)
        {
            if(IsTestLevelSetNameUniqueThrowsError)
            {
                throw new Exception();
            }

            IsTestLevelSetNameUniqueParameter = name;
            return IsTestLevelSetNameUniqueReturnValue;
        }

        public bool DoesTestLevelSetHaveReferences(TestLevelSet set)
        {
            if(DoesTestLevelSetHaveReferencesThrowsError)
            {
                throw new Exception();
            }

            DoesTestLevelSetHaveReferencesParameter = set;
            return DoesTestLevelSetHaveReferencesReturnValue;
        }
    }

    public class TestLevelSetErrorHandlerMock : ITestLevelSetErrorHandler
    {
        public bool ShowTestLevelSetErrorCalled { get; set; }

        public void ShowTestLevelSetError()
        {
            ShowTestLevelSetErrorCalled = true;
        }
    }

    public class TestLevelSetDiffShowerMock : ITestLevelSetDiffShower
    {
        public TestLevelSetDiff DiffParameter { get; set; }

        public void ShowDiffDialog(TestLevelSetDiff diff, Action saveAction)
        {
            DiffParameter = diff;
            saveAction();
        }
    }

    public class TestLevelSetUseCaseTest
    {
        [Test]
        public void LoadTestLevelSetsPassesDataFromDataAccessToGui()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.LoadTestLevelSetsReturnValue = new List<TestLevelSet>();
            environment.useCase.LoadTestLevelSets(environment.errorHandler);
            Assert.AreSame(environment.data.LoadTestLevelSetsReturnValue, environment.gui.LoadTestLevelSetsParameter);
        }

        [Test]
        public void LoadTestLevelSetsHandlesError()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.LoadTestLevelSetsThrowsError = true;
            environment.useCase.LoadTestLevelSets(environment.errorHandler);
            Assert.IsTrue(environment.errorHandler.ShowTestLevelSetErrorCalled);
        }

        [Test]
        public void AddTestLevelSetCallsDataAccess()
        {
            var environment = CreateUseCaseEnvironment();
            var entity = new TestLevelSet();
            environment.userGetter.NextReturnedUser = new User();
            environment.useCase.AddTestLevelSet(entity, environment.errorHandler);
            Assert.AreSame(entity, environment.data.AddTestLevelSetParameter.New);
            Assert.AreSame(environment.userGetter.NextReturnedUser, environment.data.AddTestLevelSetParameter.User);
        }

        [Test]
        public void AddTestLevelSetCallsGui()
        {
            var environment = CreateUseCaseEnvironment();
            var entity = new TestLevelSet()
            {
                Id = new TestLevelSetId(0),
                TestLevel1 = new TestLevel() { Id = new TestLevelId(0) },
                TestLevel2 = new TestLevel() { Id = new TestLevelId(0) },
                TestLevel3 = new TestLevel() { Id = new TestLevelId(0) }
            };
            environment.data.AddTestLevelSetReturnValue = entity;
            environment.useCase.AddTestLevelSet(entity, environment.errorHandler);
            Assert.AreSame(entity, environment.gui.AddTestLevelSetParameter);
        }

        [Test]
        public void AddTestLevelSetHandlesError()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.AddTestLevelSetThrowsError = true;
            environment.useCase.AddTestLevelSet(null, environment.errorHandler);
            Assert.IsTrue(environment.errorHandler.ShowTestLevelSetErrorCalled);
        }

        [TestCase(1, 2, 3, 4)]
        [TestCase(5, 6, 7, 8)]
        public void AddTestLevelSetUpdatesIds(long setId, long level1Id, long level2Id, long level3Id)
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.AddTestLevelSetReturnValue = new TestLevelSet()
            {
                Id = new TestLevelSetId(setId),
                TestLevel1 = new TestLevel() { Id = new TestLevelId(level1Id) },
                TestLevel2 = new TestLevel() { Id = new TestLevelId(level2Id) },
                TestLevel3 = new TestLevel() { Id = new TestLevelId(level3Id) }
            };
            var entity = new TestLevelSet()
            {
                TestLevel1 = new TestLevel(),
                TestLevel2 = new TestLevel(),
                TestLevel3 = new TestLevel()
            };
            environment.useCase.AddTestLevelSet(entity, environment.errorHandler);

            Assert.AreEqual(setId, entity.Id.ToLong());
            Assert.AreEqual(level1Id, entity.TestLevel1.Id.ToLong());
            Assert.AreEqual(level2Id, entity.TestLevel2.Id.ToLong());
            Assert.AreEqual(level3Id, entity.TestLevel3.Id.ToLong());
        }

        [Test]
        public void RemoveTestLevelSetCallsDataAccess()
        {
            var environment = CreateUseCaseEnvironment();
            var entity = new TestLevelSet();
            environment.userGetter.NextReturnedUser = new User();
            environment.useCase.RemoveTestLevelSet(entity, environment.errorHandler);
            Assert.AreSame(entity, environment.data.RemoveTestLevelSetParameter.Old);
            Assert.AreSame(environment.userGetter.NextReturnedUser, environment.data.RemoveTestLevelSetParameter.User);
        }

        [Test]
        public void RemoveTestLevelSetCallsGui()
        {
            var environment = CreateUseCaseEnvironment();
            var entity = new TestLevelSet();
            environment.useCase.RemoveTestLevelSet(entity, environment.errorHandler);
            Assert.AreSame(entity, environment.gui.RemoveTestLevelSetParameter);
        }

        [Test]
        public void RemoveTestLevelSetHandlesError()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.RemoveTestLevelSetThrowsError = true;
            environment.useCase.RemoveTestLevelSet(null, environment.errorHandler);
            Assert.IsTrue(environment.errorHandler.ShowTestLevelSetErrorCalled);
        }

        [Test]
        public void UpdateTestLevelSetCallsDataAccess()
        {
            var environment = CreateUseCaseEnvironment();
            var diff = new TestLevelSetDiff();
            environment.useCase.UpdateTestLevelSet(diff, environment.errorHandler, null, environment.diffShower);
            Assert.AreSame(diff, environment.data.UpdateTestLevelSetParameter);
        }

        [Test]
        public void UpdateTestLevelSetCallsGui()
        {
            var environment = CreateUseCaseEnvironment();
            var diff = new TestLevelSetDiff();
            environment.useCase.UpdateTestLevelSet(diff, environment.errorHandler, null, environment.diffShower);
            Assert.AreSame(diff, environment.gui.UpdateTestLevelSetParameter);
        }

        [Test]
        public void UpdateTestLevelSetCallsNotificationManager()
        {
            var environment = CreateUseCaseEnvironment();
            environment.useCase.UpdateTestLevelSet(new TestLevelSetDiff() { New = new TestLevelSet() }, environment.errorHandler, null, environment.diffShower);
            Assert.IsTrue(environment.notification.SendSuccessNotificationCalled);
        }

        [Test]
        public void UpdateTestLevelSetHandlesError()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.UpdateTestLevelSetThrowsError = true;
            environment.useCase.UpdateTestLevelSet(null, environment.errorHandler, null, environment.diffShower);
            Assert.IsTrue(environment.errorHandler.ShowTestLevelSetErrorCalled);
        }

        [TestCase("hidgafhnaörifj")]
        [TestCase("ef46dr2gdr")]
        public void IsTestLevelSetNameUniqueCallsData(string name)
        {
            var environment = CreateUseCaseEnvironment();
            environment.useCase.IsTestLevelSetNameUnique(name);
            Assert.AreEqual(name, environment.data.IsTestLevelSetNameUniqueParameter);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsTestLevelSetNameUniqueReturnsBoolFromData(bool returns)
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.IsTestLevelSetNameUniqueReturnValue = returns;
            Assert.AreEqual(returns, environment.useCase.IsTestLevelSetNameUnique(""));
        }

        [Test]
        public void IsTestLevelSetNameUniqueReturnsFallsInErrorCase()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.IsTestLevelSetNameUniqueThrowsError = true;
            Assert.IsFalse(environment.useCase.IsTestLevelSetNameUnique(""));
        }

        [Test]
        public void DoesTestLevelSetHaveReferencesCallsData()
        {
            var environment = CreateUseCaseEnvironment();
            var set = new TestLevelSet();
            environment.useCase.DoesTestLevelSetHaveReferences(set);
            Assert.AreSame(set, environment.data.DoesTestLevelSetHaveReferencesParameter);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void DoesTestLevelSetHaveReferencesReturnsBoolFromData(bool returns)
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.DoesTestLevelSetHaveReferencesReturnValue = returns;
            Assert.AreEqual(returns, environment.useCase.DoesTestLevelSetHaveReferences(new TestLevelSet()));
        }

        [Test]
        public void DoesTestLevelSetHaveReferencesReturnsFallsInErrorCase()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.DoesTestLevelSetHaveReferencesThrowsError = true;
            Assert.IsFalse(environment.useCase.DoesTestLevelSetHaveReferences(new TestLevelSet()));
        }
        
        [Test]
        public void UpdateTestLevelSetCallsTestDateCalculation()
        {
            var environment = CreateUseCaseEnvironment();
            var id = new TestLevelSetId(0);
            environment.useCase.UpdateTestLevelSet(new TestLevelSetDiff() { New = new TestLevelSet() { Id = id } }, environment.errorHandler, null, environment.diffShower);

            if (FeatureToggles.FeatureToggles.TestDateCalculation)
            {
                Assert.AreSame(id, environment.testDateCalculationUseCase.CalculateToolTestDateForTestLevelSetParameter);
                if(FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
                {
                    Assert.AreSame(id, environment.testDateCalculationUseCase.CalculateProcessControlDateForTestLevelSetParameter);
                }
            }
        }

        [Test]
        public void UpdateTestLevelSetCallsLoadLocationToolAssignments()
        {
            var environment = CreateUseCaseEnvironment();
            environment.useCase.UpdateTestLevelSet(new TestLevelSetDiff() { New = new TestLevelSet() }, environment.errorHandler, null, environment.diffShower);
            Assert.IsTrue(environment.locationToolAssignmentUseCase.LoadLocationToolAssignmentsCalled);
        }

        [Test]
        public void UpdateTestLevelSetCallsLoadProcessControlConditions()
        {
            var environment = CreateUseCaseEnvironment();
            environment.useCase.UpdateTestLevelSet(new TestLevelSetDiff() { New = new TestLevelSet() }, environment.errorHandler, environment.processControlErrorHandler, environment.diffShower);
            if (FeatureToggles.FeatureToggles.TestDateCalculation && FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
            {
                Assert.AreEqual(environment.processControlErrorHandler, environment.processControlUseCaseMock.LoadProcessControlConditionsErrorHandler); 
            }
        }

        [Test]
        public void UpdateTestLevelSetSetsDiffUser()
        {
            var environment = CreateUseCaseEnvironment();
            environment.userGetter.NextReturnedUser = new User();
            var diff = new TestLevelSetDiff() { New = new TestLevelSet() };
            environment.useCase.UpdateTestLevelSet(diff, null, null, environment.diffShower);
            if (FeatureToggles.FeatureToggles.SilverTowel_974_TestLevelSetHistory)
            {
                Assert.AreSame(environment.userGetter.NextReturnedUser, diff.User); 
            }
            else
            {
                Assert.Pass();
            }
        }

        [Test]
        public void UpdateTestLevelCallsDiffShower()
        {
            var environment = CreateUseCaseEnvironment();
            environment.userGetter.NextReturnedUser = new User();
            var diff = new TestLevelSetDiff() { New = new TestLevelSet() };
            environment.useCase.UpdateTestLevelSet(diff, null, null, environment.diffShower);
            if (FeatureToggles.FeatureToggles.SilverTowel_974_TestLevelSetHistory)
            {
                Assert.AreSame(diff, environment.diffShower.DiffParameter);
            }
            else
            {
                Assert.Pass();
            }
        }


        static Environment CreateUseCaseEnvironment()
        {
            var environment = new Environment();
            environment.gui = new TestLevelSetGuiMock();
            environment.data = new TestLevelSetDataMock();
            environment.notification = new NotificationManagerMock();
            environment.testDateCalculationUseCase = new TestDateCalculationUseCaseMock();
            environment.locationToolAssignmentUseCase = new LocationToolAssignmentUseCaseMock();
            environment.processControlUseCaseMock = new ProcessControlUseCaseMock();
            environment.processControlErrorHandler = new ProcessControlErrorMock();
            environment.errorHandler = new TestLevelSetErrorHandlerMock();
            environment.diffShower = new TestLevelSetDiffShowerMock();
            environment.userGetter = new UserGetterMock();
            environment.useCase = new TestLevelSetUseCase(environment.gui, 
                environment.data, 
                environment.notification,
                environment.testDateCalculationUseCase, 
                environment.locationToolAssignmentUseCase,
                environment.processControlUseCaseMock,
                environment.userGetter);
            return environment;
        }

        struct Environment
        {
            public TestLevelSetUseCase useCase;
            public TestLevelSetGuiMock gui;
            public TestLevelSetDataMock data;
            public NotificationManagerMock notification;
            public TestDateCalculationUseCaseMock testDateCalculationUseCase;
            public LocationToolAssignmentUseCaseMock locationToolAssignmentUseCase;
            public ProcessControlUseCaseMock processControlUseCaseMock;
            public ProcessControlErrorMock processControlErrorHandler;
            public TestLevelSetErrorHandlerMock errorHandler;
            public TestLevelSetDiffShowerMock diffShower;
            public UserGetterMock userGetter;
        }
    }
}
