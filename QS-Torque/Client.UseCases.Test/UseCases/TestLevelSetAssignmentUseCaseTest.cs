using System;
using System.Collections.Generic;
using Client.Core.Entities;
using Client.TestHelper.Mock;
using Client.UseCases.UseCases;
using Core.Entities;
using Core.Enums;
using Core.Test.UseCases;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Mock;

namespace Client.UseCases.Test.UseCases
{
    class TestLevelSetAssignmentGuiMock : ITestLevelSetAssignmentGui
    {
        public List<LocationToolAssignment> LoadLocationToolAssignmentsParameter { get; set; }
        public List<(LocationToolAssignmentId, TestType)> RemoveTestLevelSetAssignmentForParameter { get; set; }
        public TestLevelSet AssignTestLevelSetToLocationToolAssignmentsParameterTestLevelSet { get; set; }
        public List<(LocationToolAssignmentId, TestType)> AssignTestLevelSetToLocationToolAssignmentsParameterLocationToolAssignmentIds { get; set; }

        public void LoadLocationToolAssignments(List<LocationToolAssignment> assignments)
        {
            LoadLocationToolAssignmentsParameter = assignments;
        }

        public void RemoveTestLevelSetAssignmentFor(List<(LocationToolAssignmentId, TestType)> ids)
        {
            RemoveTestLevelSetAssignmentForParameter = ids;
        }

        public void AssignTestLevelSetToLocationToolAssignments(TestLevelSet testLevelSet, List<(LocationToolAssignmentId, TestType)> locationToolAssignmentIds)
        {
            AssignTestLevelSetToLocationToolAssignmentsParameterTestLevelSet = testLevelSet;
            AssignTestLevelSetToLocationToolAssignmentsParameterLocationToolAssignmentIds = locationToolAssignmentIds;
        }
    }

    class TestLevelSetAssignmentGuiForProcessControlMock : ITestLevelSetAssignmentGuiForProcessControl
    {
        public TestLevelSet AssignTestLevelSetToProcessControlConditionsTestLevelSetParameter { get; set; }
        public List<ProcessControlConditionId> AssignTestLevelSetToProcessControlConditionsProcessControlIdsParameter { get; set; }
        public List<ProcessControlConditionId> RemoveTestLevelSetAssignmentForParameter { get; set; }

        public void AssignTestLevelSetToProcessControlConditions(TestLevelSet testLevelSet, List<ProcessControlConditionId> processControlConditionIds)
        {
            AssignTestLevelSetToProcessControlConditionsTestLevelSetParameter = testLevelSet;
            AssignTestLevelSetToProcessControlConditionsProcessControlIdsParameter = processControlConditionIds;
        }

        public void RemoveTestLevelSetAssignmentFor(List<ProcessControlConditionId> ids)
        {
            RemoveTestLevelSetAssignmentForParameter = ids;
        }
    }

    class TestLevelSetAssignmentDataMock : ITestLevelSetAssignmentData
    {
        public List<LocationToolAssignment> LoadLocationToolAssignmentsReturnValue { get; set; }
        public List<(LocationToolAssignmentId, TestType)> RemoveTestLevelSetAssignmentForParameter { get; set; }
        public TestLevelSetId AssignTestLevelSetToLocationToolAssignmentsParameterTestLevelSetId { get; set; }
        public List<(LocationToolAssignmentId, TestType)> AssignTestLevelSetToLocationToolAssignmentsParameterLocationToolAssignmentIds { get; set; }
        public User RemoveTestLevelSetAssignmentForUserParameter { get; set; }
        public User AssignTestLevelSetToLocationToolAssignmentsUserParameter { get; set; }
        public TestLevelSetId AssignTestLevelSetToProcessControlConditionsTestLevelSetParameter { get; set; }
        public List<ProcessControlConditionId> AssignTestLevelSetToProcessControlConditionsProcessControlIdsParameter { get; set; }
        public User AssignTestLevelSetToProcessControlConditionsUserParameter { get; set; }
        public List<ProcessControlConditionId> RemoveTestLevelSetAssignmentForProcessControlIds { get; set; }

        public bool LoadLocationToolAssignmentsThrowsError { get; set; }
        public bool RemoveTestLevelSetAssignmentForThrowsError { get; set; }
        public bool AssignTestLevelSetToLocationToolAssignmentsThrowsError { get; set; }
        public bool AssignTestLevelSetToProcessControlConditionsThrowsError { get; set; }


        public List<LocationToolAssignment> LoadLocationToolAssignments()
        {
            if(LoadLocationToolAssignmentsThrowsError)
            {
                throw new Exception();
            }

            return LoadLocationToolAssignmentsReturnValue;
        }

        public void RemoveTestLevelSetAssignmentFor(List<(LocationToolAssignmentId, TestType)> ids, User user)
        {
            if(RemoveTestLevelSetAssignmentForThrowsError)
            {
                throw new Exception();
            }

            RemoveTestLevelSetAssignmentForParameter = ids;
            RemoveTestLevelSetAssignmentForUserParameter = user;
        }

        public void AssignTestLevelSetToLocationToolAssignments(TestLevelSetId testLevelSetId, List<(LocationToolAssignmentId, TestType)> locationToolAssignmentIds, User user)
        {
            if (AssignTestLevelSetToLocationToolAssignmentsThrowsError)
            {
                throw new Exception();
            }

            AssignTestLevelSetToLocationToolAssignmentsParameterTestLevelSetId = testLevelSetId;
            AssignTestLevelSetToLocationToolAssignmentsParameterLocationToolAssignmentIds = locationToolAssignmentIds;
            AssignTestLevelSetToLocationToolAssignmentsUserParameter = user;
        }

        public void AssignTestLevelSetToProcessControlConditions(TestLevelSetId testLevelSet, List<ProcessControlConditionId> processControlConditionIds, User user)
        {
            if (AssignTestLevelSetToProcessControlConditionsThrowsError)
            {
                throw new Exception();
            }

            AssignTestLevelSetToProcessControlConditionsTestLevelSetParameter = testLevelSet;
            AssignTestLevelSetToProcessControlConditionsProcessControlIdsParameter = processControlConditionIds;
            AssignTestLevelSetToProcessControlConditionsUserParameter = user;
        }

        public void RemoveTestLevelSetAssignmentFor(List<ProcessControlConditionId> ids, User user)
        {
            if (RemoveTestLevelSetAssignmentForThrowsError)
            {
                throw new Exception();
            }

            RemoveTestLevelSetAssignmentForProcessControlIds = ids;
            RemoveTestLevelSetAssignmentForUserParameter = user;
        }
    }

    public class TestLevelSetAssignmentErrorHandlerMock : ITestLevelSetAssignmentErrorHandler
    {
        public bool ShowTestLevelSetAssignmentErrorCalled { get; set; }

        public void ShowTestLevelSetAssignmentError()
        {
            ShowTestLevelSetAssignmentErrorCalled = true;
        }
    }


    public class TestLevelSetAssignmentUseCaseTest
    {
        [Test]
        public void LoadLocationToolAssignmentsPassesDataFromDataAccessToGui()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.LoadLocationToolAssignmentsReturnValue = new List<LocationToolAssignment>();
            environment.useCase.LoadLocationToolAssignments(environment.errorHandler);
            Assert.AreSame(environment.data.LoadLocationToolAssignmentsReturnValue, environment.gui.LoadLocationToolAssignmentsParameter);
        }

        [Test]
        public void LoadLocationToolAssignmentsHandlesError()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.LoadLocationToolAssignmentsThrowsError = true;
            environment.useCase.LoadLocationToolAssignments(environment.errorHandler);
            Assert.IsTrue(environment.errorHandler.ShowTestLevelSetAssignmentErrorCalled);
        }

        [Test]
        public void RemoveTestLevelSetAssignmentForPassesValuesToData()
        {
            var environment = CreateUseCaseEnvironment();
            var ids = new List<(LocationToolAssignmentId, TestType)>();
            environment.useCase.RemoveTestLevelSetAssignmentFor(ids, environment.errorHandler);

            Assert.AreSame(ids, environment.data.RemoveTestLevelSetAssignmentForParameter);
        }

        [Test]
        public void RemoveTestLevelSetAssignmentForPassesValuesToGui()
        {
            var environment = CreateUseCaseEnvironment();
            var ids = new List<(LocationToolAssignmentId, TestType)>();
            environment.useCase.RemoveTestLevelSetAssignmentFor(ids, environment.errorHandler);

            Assert.AreSame(ids, environment.gui.RemoveTestLevelSetAssignmentForParameter);
        }

        [Test]
        public void RemoveTestLevelSetAssignmentForHandlesError()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.RemoveTestLevelSetAssignmentForThrowsError = true;
            environment.useCase.RemoveTestLevelSetAssignmentFor(new List<(LocationToolAssignmentId, TestType)>(), environment.errorHandler);
            Assert.IsTrue(environment.errorHandler.ShowTestLevelSetAssignmentErrorCalled);
        }

        [TestCase(2)]
        [TestCase(10)]
        [TestCase(15)]
        public void RemoveTestLevelSetAssignmentForCallsNotification(int locToolAssignmentCount)
        {
            var environment = CreateUseCaseEnvironment();
            var locToolAssignmentIds = new List<(LocationToolAssignmentId, TestType)>();

            for (int i = 0; i < locToolAssignmentCount; i++)
            {
                locToolAssignmentIds.Add((new LocationToolAssignmentId(0), TestType.Chk));
            }

            environment.useCase.RemoveTestLevelSetAssignmentFor(locToolAssignmentIds, environment.errorHandler);
            Assert.IsTrue(environment.notification.SendSuccessNotificationCalled);
            Assert.AreEqual(locToolAssignmentCount, environment.notification.SendSuccessNotificationParameter);
        }

        [Test]
        public void AssignTestLevelSetToLocationToolAssignmentsPassesValuesToGui()
        {
            var environment = CreateUseCaseEnvironment();
            var testLevelSet = new TestLevelSet();
            var locToolAssignmentIds = new List<(LocationToolAssignmentId, TestType)>();
            environment.useCase.AssignTestLevelSetToLocationToolAssignments(testLevelSet, locToolAssignmentIds, environment.errorHandler);

            Assert.AreSame(testLevelSet, environment.gui.AssignTestLevelSetToLocationToolAssignmentsParameterTestLevelSet);
            Assert.AreSame(locToolAssignmentIds, environment.gui.AssignTestLevelSetToLocationToolAssignmentsParameterLocationToolAssignmentIds);
        }

        [Test]
        public void AssignTestLevelSetToLocationToolAssignmentsPassesValuesToData()
        {
            var environment = CreateUseCaseEnvironment();
            var testLevelSet = new TestLevelSet() { Id = new TestLevelSetId(5) };
            var locToolAssignmentIds = new List<(LocationToolAssignmentId, TestType)>();
            environment.useCase.AssignTestLevelSetToLocationToolAssignments(testLevelSet, locToolAssignmentIds, environment.errorHandler);

            Assert.AreSame(testLevelSet.Id, environment.data.AssignTestLevelSetToLocationToolAssignmentsParameterTestLevelSetId);
            Assert.AreSame(locToolAssignmentIds, environment.data.AssignTestLevelSetToLocationToolAssignmentsParameterLocationToolAssignmentIds);
        }

        [Test]
        public void AssignTestLevelSetToLocationToolAssignmentsHandlesError()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.AssignTestLevelSetToLocationToolAssignmentsThrowsError = true;
            environment.useCase.AssignTestLevelSetToLocationToolAssignments(new TestLevelSet(), new List<(LocationToolAssignmentId, TestType)>(), environment.errorHandler);
            Assert.IsTrue(environment.errorHandler.ShowTestLevelSetAssignmentErrorCalled);
        }

        [TestCase(2)]
        [TestCase(10)]
        [TestCase(15)]
        public void AssignTestLevelSetToLocationToolAssignmentsCallsNotification(int locToolAssignmentCount)
        {
            var environment = CreateUseCaseEnvironment();
            var locToolAssignmentIds = new List<(LocationToolAssignmentId, TestType)>();

            for (int i = 0; i < locToolAssignmentCount; i++)
            {
                locToolAssignmentIds.Add((new LocationToolAssignmentId(0), TestType.Chk));
            }

            environment.useCase.AssignTestLevelSetToLocationToolAssignments(new TestLevelSet(), locToolAssignmentIds, environment.errorHandler);
            Assert.IsTrue(environment.notification.SendSuccessNotificationCalled);
            Assert.AreEqual(locToolAssignmentCount, environment.notification.SendSuccessNotificationParameter);
        }

        [TestCase(15, 5, 56, 66666, 7)]
        [TestCase(9)]
        [TestCase(6, 9, 12, 1321324)]
        public void AssignTestLevelSetToLocationToolAssignmentsCallsTestDateCalculation(params long[] ids)
        {
            if (!FeatureToggles.FeatureToggles.TestDateCalculation)
            {
                Assert.Pass();
                return;
            }

            var environment = CreateUseCaseEnvironment();
            var locToolAssignmentIds = new List<(LocationToolAssignmentId, TestType)>();

            foreach (var id in ids)
            {
                locToolAssignmentIds.Add((new LocationToolAssignmentId(id), TestType.Chk));
            }

            environment.useCase.AssignTestLevelSetToLocationToolAssignments(new TestLevelSet(), locToolAssignmentIds, environment.errorHandler);
            CheckerFunctions.CollectionAssertAreEquivalent(ids, environment.testDateCalculationUseCase.CalculateToolTestDateForParamter, (x, y) => x == y.ToLong());
        }

        [Test]
        public void AssignTestLevelSetToLocationToolAssignmentsCallsLoadLocationToolAssignments()
        {
            var environment = CreateUseCaseEnvironment();
            environment.useCase.AssignTestLevelSetToLocationToolAssignments(new TestLevelSet(), new List<(LocationToolAssignmentId, TestType)>(), environment.errorHandler);
            Assert.IsTrue(environment.locationToolAssignmentUseCase.LoadLocationToolAssignmentsCalled);
        }

        [TestCase(5)]
        [TestCase(6)]
        public void RemoveTestLevelSetAssignmentForPassesUserToDataAccess(long userId)
        {
            var environment = CreateUseCaseEnvironment();
            environment.userGetter.NextReturnedUser = new User { UserId = new UserId(userId) };
            environment.useCase.RemoveTestLevelSetAssignmentFor(new List<(LocationToolAssignmentId, TestType)>(), null);
            Assert.AreEqual(userId, environment.data.RemoveTestLevelSetAssignmentForUserParameter.UserId.ToLong());
        }

        [TestCase(5)]
        [TestCase(6)]
        public void AssignTestLevelSetToLocationToolAssignmentsPassesUserToDataAccess(long userId)
        {
            var environment = CreateUseCaseEnvironment();
            environment.userGetter.NextReturnedUser = new User { UserId = new UserId(userId) };
            environment.useCase.AssignTestLevelSetToLocationToolAssignments(new TestLevelSet(), new List<(LocationToolAssignmentId, TestType)>(), null);
            Assert.AreEqual(userId, environment.data.AssignTestLevelSetToLocationToolAssignmentsUserParameter.UserId.ToLong());
        }

        [Test]
        public void AssignTestLevelSetToProcessControlConditionsPassesValuesToGui()
        {
            var environment = CreateUseCaseEnvironment();
            var testLevelSet = new TestLevelSet();
            var ids = new List<ProcessControlConditionId>();
            environment.useCase.AssignTestLevelSetToProcessControlConditions(testLevelSet, ids, environment.errorHandler, null);

            Assert.AreSame(testLevelSet, environment.processControlGui.AssignTestLevelSetToProcessControlConditionsTestLevelSetParameter);
            Assert.AreSame(ids, environment.processControlGui.AssignTestLevelSetToProcessControlConditionsProcessControlIdsParameter);
        }

        [Test]
        public void AssignTestLevelSetToProcessControlConditionsPassesValuesToData()
        {
            var environment = CreateUseCaseEnvironment();
            var testLevelSet = new TestLevelSet() { Id = new TestLevelSetId(5) };
            var ids = new List<ProcessControlConditionId>();
            environment.useCase.AssignTestLevelSetToProcessControlConditions(testLevelSet, ids, environment.errorHandler, null);

            Assert.AreSame(testLevelSet.Id, environment.data.AssignTestLevelSetToProcessControlConditionsTestLevelSetParameter);
            Assert.AreSame(ids, environment.data.AssignTestLevelSetToProcessControlConditionsProcessControlIdsParameter);
        }

        [Test]
        public void AssignTestLevelSetToProcessControlConditionsHandlesError()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.AssignTestLevelSetToProcessControlConditionsThrowsError = true;
            environment.useCase.AssignTestLevelSetToProcessControlConditions(new TestLevelSet(), new List<ProcessControlConditionId>(), environment.errorHandler, null);
            Assert.IsTrue(environment.errorHandler.ShowTestLevelSetAssignmentErrorCalled);
        }

        [TestCase(2)]
        [TestCase(10)]
        [TestCase(15)]
        public void AssignTestLevelSetToProcessControlConditionsCallsNotification(int count)
        {
            var environment = CreateUseCaseEnvironment();
            var ids = new List<ProcessControlConditionId>();

            for (int i = 0; i < count; i++)
            {
                ids.Add(new ProcessControlConditionId(0));
            }

            environment.useCase.AssignTestLevelSetToProcessControlConditions(new TestLevelSet(), ids, environment.errorHandler, null);
            Assert.IsTrue(environment.notification.SendSuccessNotificationCalled);
            Assert.AreEqual(count, environment.notification.SendSuccessNotificationParameter);
        }

        [TestCase(5)]
        [TestCase(6)]
        public void AssignTestLevelSetToProcessControlConditionsPassesUserToDataAccess(long userId)
        {
            var environment = CreateUseCaseEnvironment();
            environment.userGetter.NextReturnedUser = new User { UserId = new UserId(userId) };
            environment.useCase.AssignTestLevelSetToProcessControlConditions(new TestLevelSet(), new List<ProcessControlConditionId>(), null, null);
            Assert.AreEqual(userId, environment.data.AssignTestLevelSetToProcessControlConditionsUserParameter.UserId.ToLong());
        }

        [Test]
        public void AssignTestLevelSetToProcessControlConditionsCallsTestDateCalculation()
        {
            var environment = CreateUseCaseEnvironment();
            var idList = new List<ProcessControlConditionId>();
            environment.useCase.AssignTestLevelSetToProcessControlConditions(new TestLevelSet(), idList, null, null);
            if (FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
            {
                Assert.AreSame(environment.testDateCalculationUseCase.CalculateProcessControlDateForParameter, idList); 
            }
        }

        [Test]
        public void AssignTestLevelSetToProcessControlConditionsResloadsProcessControlConditions()
        {
            var environment = CreateUseCaseEnvironment();
            environment.useCase.AssignTestLevelSetToProcessControlConditions(new TestLevelSet(), new List<ProcessControlConditionId>(), null, environment.processControlErrorHandler);
            if (FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
            {
                Assert.AreSame(environment.processControlErrorHandler, environment.processControlUseCase.LoadProcessControlConditionsErrorHandler);
            }
        }

        [Test]
        public void RemoveTestLevelSetAssignmentForProcessControlPassesValuesToData()
        {
            var environment = CreateUseCaseEnvironment();
            var ids = new List<ProcessControlConditionId>();
            environment.useCase.RemoveTestLevelSetAssignmentFor(ids, environment.errorHandler);

            Assert.AreSame(ids, environment.data.RemoveTestLevelSetAssignmentForProcessControlIds);
        }

        [Test]
        public void RemoveTestLevelSetAssignmentForProcessControlPassesValuesToGui()
        {
            var environment = CreateUseCaseEnvironment();
            var ids = new List<ProcessControlConditionId>();
            environment.useCase.RemoveTestLevelSetAssignmentFor(ids, environment.errorHandler);

            Assert.AreSame(ids, environment.processControlGui.RemoveTestLevelSetAssignmentForParameter);
        }

        [Test]
        public void RemoveTestLevelSetAssignmentForProcessControlHandlesError()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.RemoveTestLevelSetAssignmentForThrowsError = true;
            environment.useCase.RemoveTestLevelSetAssignmentFor(new List<ProcessControlConditionId>(), environment.errorHandler);
            Assert.IsTrue(environment.errorHandler.ShowTestLevelSetAssignmentErrorCalled);
        }

        [TestCase(2)]
        [TestCase(10)]
        [TestCase(15)]
        public void RemoveTestLevelSetAssignmentForProcessControlCallsNotification(int processControlCount)
        {
            var environment = CreateUseCaseEnvironment();
            var processControlIds = new List<ProcessControlConditionId>();

            for (int i = 0; i < processControlCount; i++)
            {
                processControlIds.Add(new ProcessControlConditionId(0));
            }

            environment.useCase.RemoveTestLevelSetAssignmentFor(processControlIds, environment.errorHandler);
            Assert.IsTrue(environment.notification.SendSuccessNotificationCalled);
            Assert.AreEqual(processControlCount, environment.notification.SendSuccessNotificationParameter);
        }

        [TestCase(5)]
        [TestCase(6)]
        public void RemoveTestLevelSetAssignmentForProcessControlPassesUserToDataAccess(long userId)
        {
            var environment = CreateUseCaseEnvironment();
            environment.userGetter.NextReturnedUser = new User { UserId = new UserId(userId) };
            environment.useCase.RemoveTestLevelSetAssignmentFor(new List<ProcessControlConditionId>(), null);
            Assert.AreEqual(userId, environment.data.RemoveTestLevelSetAssignmentForUserParameter.UserId.ToLong());
        }

        static Environment CreateUseCaseEnvironment()
        {
            var environment = new Environment();
            environment.gui = new TestLevelSetAssignmentGuiMock();
            environment.processControlGui = new TestLevelSetAssignmentGuiForProcessControlMock();
            environment.data = new TestLevelSetAssignmentDataMock();
            environment.notification = new NotificationManagerMock();
            environment.testDateCalculationUseCase = new TestDateCalculationUseCaseMock();
            environment.locationToolAssignmentUseCase = new LocationToolAssignmentUseCaseMock();
            environment.processControlUseCase = new ProcessControlUseCaseMock();
            environment.processControlErrorHandler = new ProcessControlErrorMock();
            environment.errorHandler = new TestLevelSetAssignmentErrorHandlerMock();
            environment.userGetter = new UserGetterMock();
            environment.useCase = new TestLevelSetAssignmentUseCase(environment.gui, 
                environment.processControlGui,
                environment.data, 
                environment.notification,
                environment.testDateCalculationUseCase,
                environment.locationToolAssignmentUseCase,
                environment.processControlUseCase,
                environment.userGetter);
            return environment;
        }

        struct Environment
        {
            public TestLevelSetAssignmentUseCase useCase;
            public TestLevelSetAssignmentGuiMock gui;
            public TestLevelSetAssignmentGuiForProcessControlMock processControlGui;
            public TestLevelSetAssignmentDataMock data;
            public NotificationManagerMock notification;
            public TestDateCalculationUseCaseMock testDateCalculationUseCase;
            public LocationToolAssignmentUseCaseMock locationToolAssignmentUseCase;
            public ProcessControlUseCaseMock processControlUseCase;
            public ProcessControlErrorMock processControlErrorHandler;
            public TestLevelSetAssignmentErrorHandlerMock errorHandler;
            public UserGetterMock userGetter;
        }
    }
}
