using System;
using Client.Core.Diffs;
using Client.TestHelper.Mock;
using Client.UseCases.Test.UseCases;
using Core.Entities;
using Core.UseCases;
using NUnit.Framework;
using TestHelper.Mock;

namespace Core.Test.UseCases
{
    class ShiftManagementGuiMock : IShiftManagementGui
    {
        public ShiftManagement LoadShiftManagementParameter { get; set; }
        public ShiftManagementDiff SaveShiftManagementParameterDiff { get; set; }

        public void LoadShiftManagement(ShiftManagement entity)
        {
            LoadShiftManagementParameter = entity;
        }

        public void SaveShiftManagement(ShiftManagementDiff diff)
        {
            SaveShiftManagementParameterDiff = diff;
        }
    }

    class ShiftManagementDataMock : IShiftManagementData
    {
        public ShiftManagement LoadShiftManagementReturnValue { get; set; }
        public ShiftManagementDiff SaveShiftManagementParameterDiff { get; set; }

        public bool LoadShiftManagementThrowsError { get; set; }
        public bool SaveShiftManagementThrowsError { get; set; }

        public ShiftManagement LoadShiftManagement()
        {
            if (LoadShiftManagementThrowsError)
            {
                throw new Exception();
            }

            return LoadShiftManagementReturnValue;
        }

        public void SaveShiftManagement(ShiftManagementDiff diff)
        {
            if (SaveShiftManagementThrowsError)
            {
                throw new Exception();
            }

            SaveShiftManagementParameterDiff = diff;
        }
    }
    
    class ShiftManagementErrorHandlerMock : IShiftManagementErrorHandler
    {
        public bool ShiftManagementErrorCalled { get; set; }
        
        public void ShiftManagementError()
        {
            ShiftManagementErrorCalled = true;
        }
    }

    class ShiftManagementDiffShowerMock : IShiftManagementDiffShower
    {
        public ShiftManagementDiff DiffParameter { get; set; }
        public Action SaveActionParameter { get; set; }
        public bool CallSaveActionAutomatically { get; set; }

        public void ShowDiffDialog(ShiftManagementDiff diff, Action saveAction)
        {
            DiffParameter = diff;
            SaveActionParameter = saveAction;

            if(CallSaveActionAutomatically)
            {
                saveAction();
            }
        }
    }

    public class ShiftManagementUseCaseTest
    {
        [Test]
        public void LoadShiftManagementPassesEntityFromDataToGui()
        {
            var environment = CreateShiftManagementEnvironment();
            environment.data.LoadShiftManagementReturnValue = new ShiftManagement();
            environment.useCase.LoadShiftManagement(environment.errorHandler);
            Assert.AreSame(environment.data.LoadShiftManagementReturnValue, environment.gui.LoadShiftManagementParameter);
        }

        [Test]
        public void LoadShiftManagementCallsError()
        {
            var environment = CreateShiftManagementEnvironment();
            environment.data.LoadShiftManagementThrowsError = true;
            environment.useCase.LoadShiftManagement(environment.errorHandler);
            Assert.IsTrue(environment.errorHandler.ShiftManagementErrorCalled);
        }

        [Test]
        public void SaveShiftManagementCallsDiffShowerWithCorrectParameter()
        {
            var environment = CreateShiftManagementEnvironment();
            var diffShower = new ShiftManagementDiffShowerMock();
            var diff = new ShiftManagementDiff(null, null);
            environment.useCase.SaveShiftManagement(diff, null, null, diffShower);
            Assert.AreSame(diff, diffShower.DiffParameter);
        }

        [Test]
        public void SaveShiftManagementDiffShowerSaveActionCallsData()
        {
            var environment = CreateShiftManagementEnvironment();
            var diffShower = new ShiftManagementDiffShowerMock();
            var diff = new ShiftManagementDiff(null, null);
            environment.userGetter.NextReturnedUser = new User();
            environment.useCase.SaveShiftManagement(diff, null, null, diffShower);
            diffShower.SaveActionParameter();
            Assert.AreSame(diff, environment.data.SaveShiftManagementParameterDiff);
            Assert.AreSame(environment.userGetter.NextReturnedUser, environment.data.SaveShiftManagementParameterDiff.User);
        }
        
        [Test]
        public void SaveShiftManagementDiffShowerSaveActionCallsGui()
        {
            var environment = CreateShiftManagementEnvironment();
            var diffShower = new ShiftManagementDiffShowerMock();
            var diff = new ShiftManagementDiff(null, null);
            environment.useCase.SaveShiftManagement(diff, null, null, diffShower);
            diffShower.SaveActionParameter();
            Assert.AreSame(diff, environment.gui.SaveShiftManagementParameterDiff);
        }

        [Test]
        public void SaveShiftManagementDiffShowerSaveActionCallsNotification()
        {
            var environment = CreateShiftManagementEnvironment();
            var diffShower = new ShiftManagementDiffShowerMock();
            environment.useCase.SaveShiftManagement(new ShiftManagementDiff(new ShiftManagement(), new ShiftManagement()), null, null, diffShower);
            diffShower.SaveActionParameter();
            Assert.IsTrue(environment.notification.SendSuccessNotificationCalled);
        }

        [Test]
        public void SaveShiftManagementWithHistoryCallsTestDateCalculation()
        {
            var environment = CreateShiftManagementEnvironment();
            var diffShower = new ShiftManagementDiffShowerMock();
            environment.useCase.SaveShiftManagement(new ShiftManagementDiff(new ShiftManagement(), new ShiftManagement()), null, null, diffShower);
            diffShower.SaveActionParameter();
            Assert.AreEqual(FeatureToggles.FeatureToggles.TestDateCalculation, environment.testDateCalculationUseCase.CalculateToolTestDateForAllLocationToolAssignmentsCalled);
            Assert.AreEqual(FeatureToggles.FeatureToggles.TestDateCalculation && FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl,
                environment.testDateCalculationUseCase.CalculateProcessControlDateForAllProcessControlConditionsCalled);
        }

        [Test]
        public void SaveShiftManagementWithHistoryCallsLoadLocationToolAssignments()
        {
            var environment = CreateShiftManagementEnvironment();
            var diffShower = new ShiftManagementDiffShowerMock();
            environment.useCase.SaveShiftManagement(new ShiftManagementDiff(new ShiftManagement(), new ShiftManagement()), null, null, diffShower);
            diffShower.SaveActionParameter();
            Assert.IsTrue(environment.locationToolAssignmentUseCase.LoadLocationToolAssignmentsCalled);
        }

        [Test]
        public void SaveShiftManagementWithHistoryCallsLoadProcessControlConditions()
        {
            var environment = CreateShiftManagementEnvironment();
            var diffShower = new ShiftManagementDiffShowerMock();
            environment.useCase.SaveShiftManagement(new ShiftManagementDiff(new ShiftManagement(), new ShiftManagement()), null, environment.processControlErrorHandler, diffShower);
            diffShower.SaveActionParameter();

            if(FeatureToggles.FeatureToggles.TestDateCalculation && FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
            {
                Assert.AreEqual(environment.processControlErrorHandler, environment.processControlUseCase.LoadProcessControlConditionsErrorHandler);
            }
        }

        [Test]
        public void SaveShiftManagementWithHistoryCallsError()
        {
            var environment = CreateShiftManagementEnvironment();
            environment.data.SaveShiftManagementThrowsError = true;
            var diffShower = new ShiftManagementDiffShowerMock();
            diffShower.CallSaveActionAutomatically = true;
            environment.useCase.SaveShiftManagement(null, environment.errorHandler, null, diffShower);
            Assert.IsTrue(environment.errorHandler.ShiftManagementErrorCalled);
        }

        private static Environment CreateShiftManagementEnvironment()
        {
            var environment = new Environment();
            environment.gui = new ShiftManagementGuiMock();
            environment.data = new ShiftManagementDataMock();
            environment.notification = new NotificationManagerMock();
            environment.testDateCalculationUseCase = new TestDateCalculationUseCaseMock();
            environment.locationToolAssignmentUseCase = new LocationToolAssignmentUseCaseMock();
            environment.errorHandler = new ShiftManagementErrorHandlerMock();
            environment.processControlUseCase = new ProcessControlUseCaseMock();
            environment.processControlErrorHandler = new ProcessControlErrorMock();
            environment.userGetter = new UserGetterMock();
            environment.useCase = new ShiftManagementUseCase(environment.gui, 
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
            public ShiftManagementUseCase useCase;
            public ShiftManagementGuiMock gui;
            public ShiftManagementDataMock data;
            public NotificationManagerMock notification;
            public TestDateCalculationUseCaseMock testDateCalculationUseCase;
            public LocationToolAssignmentUseCaseMock locationToolAssignmentUseCase;
            public ShiftManagementErrorHandlerMock errorHandler;
            public ProcessControlUseCaseMock processControlUseCase;
            public ProcessControlErrorMock processControlErrorHandler;
            public UserGetterMock userGetter;
        }
    }
}
