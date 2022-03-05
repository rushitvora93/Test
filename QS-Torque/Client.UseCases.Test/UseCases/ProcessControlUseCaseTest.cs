using System;
using System.Collections.Generic;
using System.Linq;
using Client.Core.Diffs;
using Client.Core.Entities;
using Client.TestHelper.Factories;
using Client.TestHelper.Mock;
using Client.UseCases.UseCases;
using Core.Entities;
using Core.Test.UseCases;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using NUnit.Framework;
using TestHelper.Factories;
using TestHelper.Mock;

namespace Client.UseCases.Test.UseCases
{
    public class ProcessControlGuiMock : IProcessControlGui
    {
        public void ShowProcessControlConditionForLocation(ProcessControlCondition processControlCondition)
        {
            ShowProcessControlConditionForLocationParameter = processControlCondition;
        }

        public void RemoveProcessControlCondition(ProcessControlCondition processControlCondition)
        {
            RemoveProcessControlConditionParameter = processControlCondition;
        }

        public void AddProcessControlCondition(ProcessControlCondition processControlCondition)
        {
            AddProcessControlConditionParameter = processControlCondition;
        }

        public void UpdateProcessControlCondition(List<ProcessControlCondition> processControlCondition)
        {
            UpdateProcessControlConditionParameter = processControlCondition;
        }

        public void ShowProcessControlConditions(List<ProcessControlCondition> processControlConditions)
        {
            ShowProcessControlConditionsParameter = processControlConditions;
        }

        public List<ProcessControlCondition> UpdateProcessControlConditionParameter { get; set; }

        public ProcessControlCondition AddProcessControlConditionParameter { get; set; }

        public ProcessControlCondition ShowProcessControlConditionForLocationParameter { get; set; }

        public ProcessControlCondition RemoveProcessControlConditionParameter { get; set; }

        public List<ProcessControlCondition> ShowProcessControlConditionsParameter { get; set; }
    }

    public class ProcessControlErrorMock : IProcessControlErrorGui
    {
        public void ShowProblemLoadingLocationProcessControlCondition()
        {
            ShowProblemLoadingLocationProcessControlConditionCalled = true;
        }

        public void ShowProblemRemoveProcessControlCondition()
        {
            ShowProblemRemoveProcessControlConditionCalled = true;
        }

        public void ShowProblemSavingProcessControlCondition()
        {
            ShowProblemSavingProcessControlConditionCalled = true;
        }

        public bool ShowProblemSavingProcessControlConditionCalled { get; set; }

        public bool ShowProblemLoadingLocationProcessControlConditionCalled { get; set; }

        public bool ShowProblemRemoveProcessControlConditionCalled { get; set; }
    }

    public class ProcessControlSaveGuiShowerMock : IProcessControlSaveGuiShower
    {
        public void SaveProcessControl(List<ProcessControlConditionDiff> diff, Action saveAction)
        {
            SaveProcessControlParameterDiff = diff;
            SaveProcessControlParameterAction = saveAction;
            saveAction();
        }

        public Action SaveProcessControlParameterAction { get; set; }

        public List<ProcessControlConditionDiff> SaveProcessControlParameterDiff { get; set; }
    }

    public class ProcessControlUseCaseTest
    {
        [Test]
        public void RemoveProcessControlConditionCallsDataAccess()
        {
            var environment = new Environment();
            var user = CreateUser.Anonymous();
            environment.Mock.UserGetter.NextReturnedUser = user;
            var processControlCondition = CreateProcessControlCondition.Anonymous();
            environment.UseCase.RemoveProcessControlCondition(processControlCondition, null);

            Assert.AreSame(processControlCondition, environment.Mock.ProcessControlData.RemoveProcessControlConditionParameterCondition);
            Assert.AreSame(user, environment.Mock.ProcessControlData.RemoveProcessControlConditionParameterUser);
        }

        [Test]
        public void RemoveProcessControlConditionCallsSuccessNotification()
        {
            var environment = new Environment();
            environment.UseCase.RemoveProcessControlCondition(CreateProcessControlCondition.Anonymous(), null);

            Assert.IsTrue(environment.Mock.NotificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void RemoveProcessControlConditionCallsGui()
        {
            var environment = new Environment();
            var processControlCondition = CreateProcessControlCondition.Anonymous();
            environment.UseCase.RemoveProcessControlCondition(processControlCondition, null);

            Assert.AreSame(processControlCondition, environment.Mock.ProcessControlGui.RemoveProcessControlConditionParameter);
        }

        [Test]
        public void RemoveProcessControlConditionWithErrorCallsErrorHandler()
        {
            var environment = new Environment();
            environment.Mock.ProcessControlData.RemoveProcessControlConditionThrowsError = true;

            environment.UseCase.RemoveProcessControlCondition(CreateProcessControlCondition.Anonymous(), environment.Mock.Error);

            Assert.IsTrue(environment.Mock.Error.ShowProblemRemoveProcessControlConditionCalled);
        }

        [Test]
        public void LoadProcessControlConditionForLocationCallsDataAccess()
        {
            var environment = new Environment();
            var location = CreateLocation.Randomized(345);
            environment.UseCase.LoadProcessControlConditionForLocation(location, null);
            Assert.AreSame(location, environment.Mock.ProcessControlData.LoadProcessControlConditionForLocationParameter);
        }

        [Test]
        public void LoadProcessControlConditionForLocationCallsGui()
        {
            var environment = new Environment();
            var processControl = CreateProcessControlCondition.Randomized(345);
            environment.Mock.ProcessControlData.LoadProcessControlConditionForLocationReturnValue = processControl;

            environment.UseCase.LoadProcessControlConditionForLocation(null, null);
            Assert.AreSame(processControl, environment.Mock.ProcessControlGui.ShowProcessControlConditionForLocationParameter);
        }

        [Test]
        public void LoadProcessControlConditionForLocationCallsErrorGui()
        {
            var environment = new Environment();
            environment.Mock.ProcessControlData.LoadProcessControlConditionForLocationThrowsException = true;
            
            environment.UseCase.LoadProcessControlConditionForLocation(null, environment.Mock.Error);
            Assert.IsTrue(environment.Mock.Error.ShowProblemLoadingLocationProcessControlConditionCalled);
        }

        [Test]
        public void AddProcessControlConditionCallsDataAccess()
        {
            var environment = new Environment();
            var user = CreateUser.Anonymous();
            environment.Mock.UserGetter.NextReturnedUser = user;
            var processControlCondition = CreateProcessControlCondition.Randomized(433647);
            environment.UseCase.AddProcessControlCondition(processControlCondition, null);
            Assert.AreSame(processControlCondition, environment.Mock.ProcessControlData.AddProcessControlConditionParameterCondition);
            Assert.AreSame(user, environment.Mock.ProcessControlData.AddProcessControlConditionParameterUser);
        }

        [Test]
        public void AddProcessControlConditionCallsGui()
        {
            var environment = new Environment();
            var processControlCondition = CreateProcessControlCondition.Randomized(433647);
            environment.Mock.ProcessControlData.AddProcessControlConditionReturnValue = processControlCondition;
            environment.UseCase.AddProcessControlCondition(CreateProcessControlCondition.Randomized(456456), null);
            Assert.AreSame(processControlCondition, environment.Mock.ProcessControlGui.AddProcessControlConditionParameter);
        }

        [Test]
        public void AddProcessControlConditionCallsNotificationManager()
        {
            var environment = new Environment();
            environment.UseCase.AddProcessControlCondition(CreateProcessControlCondition.Randomized(456456), null);
            Assert.IsTrue(environment.Mock.NotificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void AddProcessControlConditionCallsShowProblemSavingProcessControlCondition()
        {
            var environment = new Environment();
            environment.Mock.ProcessControlData.AddProcessControlConditionThrowsError = true;
            environment.UseCase.AddProcessControlCondition(CreateProcessControlCondition.Randomized(456456), environment.Mock.Error);
            Assert.IsTrue(environment.Mock.Error.ShowProblemSavingProcessControlConditionCalled);
        }

        [Test]
        public void AddProcessControlConditionCallsTestDateCalculation()
        {
            var environment = new Environment();
            var id = new ProcessControlConditionId(0);
            environment.UseCase.AddProcessControlCondition(new ProcessControlCondition() { Id = id }, null);
            if (FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
            {
                Assert.AreEqual(1, environment.Mock.testDateCalculationUseCase.CalculateProcessControlDateForParameter.Count);
                Assert.AreSame(environment.Mock.testDateCalculationUseCase.CalculateProcessControlDateForParameter[0], id);
            }
        }

        [Test]
        public void SaveProcessControlConditionModelCallsGuiShowerSaveProcessControlConditionModel()
        {
            var environment = new Environment();

            var diff = new ProcessControlConditionDiff(null, null,new ProcessControlCondition(), new ProcessControlCondition());
            environment.UseCase.SaveProcessControlCondition(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { diff }, environment.Mock.Error, environment.Mock.SaveGuiShower);

            Assert.AreSame(diff, environment.Mock.SaveGuiShower.SaveProcessControlParameterDiff.First());
        }

        [Test]
        public void SaveProcessControlConditionSetsUser()
        {
            var environment = new Environment();
            var user = CreateUser.Anonymous();
            environment.Mock.UserGetter.NextReturnedUser = user;
            var diff = new ProcessControlConditionDiff(null, null, new ProcessControlCondition(), new ProcessControlCondition());
            environment.UseCase.SaveProcessControlCondition(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { diff }, null, environment.Mock.SaveGuiShower);

            Assert.AreSame(user, environment.Mock.ProcessControlData.SaveProcessControlConditionParameter.First().User);
        }

        [Test]
        public void SaveProcessControlConditionCallsDataAccess()
        {
            var environment = new Environment();
            var diff = new ProcessControlConditionDiff(CreateUser.Anonymous(), null, new ProcessControlCondition(), new ProcessControlCondition());
            environment.UseCase.SaveProcessControlCondition(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { diff }, null, environment.Mock.SaveGuiShower);

            Assert.AreSame(diff, environment.Mock.ProcessControlData.SaveProcessControlConditionParameter.First());
        }

        [Test]
        public void SaveProcessControlConditionCallsGui()
        {
            var environment = new Environment();
            var diff = new ProcessControlConditionDiff(null, null,new ProcessControlCondition(), new ProcessControlCondition());
            environment.UseCase.SaveProcessControlCondition(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { diff }, null, environment.Mock.SaveGuiShower);

            Assert.AreSame(diff.GetNewProcessControlCondition(), environment.Mock.ProcessControlGui.UpdateProcessControlConditionParameter.First());
        }
        
        [Test]
        public void SaveProcessControlConditionCallsSuccessNotification()
        {
            var environment = new Environment();
            var diff = new ProcessControlConditionDiff(null, null, new ProcessControlCondition(), new ProcessControlCondition());
            environment.UseCase.SaveProcessControlCondition(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { diff }, null, environment.Mock.SaveGuiShower);

            Assert.IsTrue(environment.Mock.NotificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void SaveProcessControlConditionWithErrorCallsShowSavingError()
        {
            var environment = new Environment();
            environment.Mock.ProcessControlData.SaveProcessControlConditionThrowsError = true;

            var diff = new ProcessControlConditionDiff(null, null, new ProcessControlCondition(), new ProcessControlCondition());
            environment.UseCase.SaveProcessControlCondition(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { diff }, environment.Mock.Error, environment.Mock.SaveGuiShower);

            Assert.IsTrue(environment.Mock.Error.ShowProblemSavingProcessControlConditionCalled);
        }

        [Test]
        public void UpdateProcessControlConditionSetsUser()
        {
            var environment = new Environment();
            var user = CreateUser.Anonymous();
            environment.Mock.UserGetter.NextReturnedUser = user;
            var diff = new ProcessControlConditionDiff(null, null, new ProcessControlCondition(), new ProcessControlCondition());
            environment.UseCase.UpdateProcessControlCondition(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { diff }, null);

            Assert.AreSame(user, environment.Mock.ProcessControlData.SaveProcessControlConditionParameter.First().User);
        }

        [Test]
        public void UpdateProcessControlConditionCallsDataAccess()
        {
            var environment = new Environment();
            var diff = new ProcessControlConditionDiff(CreateUser.Anonymous(), null, new ProcessControlCondition(), new ProcessControlCondition());
            environment.UseCase.UpdateProcessControlCondition(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { diff }, null);

            Assert.AreSame(diff, environment.Mock.ProcessControlData.SaveProcessControlConditionParameter.First());
        }

        [Test]
        public void UpdateProcessControlConditionCallsGui()
        {
            var environment = new Environment();
            var diff = new ProcessControlConditionDiff(null, null, new ProcessControlCondition(), new ProcessControlCondition());
            environment.UseCase.UpdateProcessControlCondition(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { diff }, null);

            Assert.AreSame(diff.GetNewProcessControlCondition(), environment.Mock.ProcessControlGui.UpdateProcessControlConditionParameter.First());
        }

        [Test]
        public void UpdateProcessControlConditionCallsSuccessNotification()
        {
            var environment = new Environment();
            var diff = new ProcessControlConditionDiff(null, null, new ProcessControlCondition(), new ProcessControlCondition());
            environment.UseCase.UpdateProcessControlCondition(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { diff }, null);

            Assert.IsTrue(environment.Mock.NotificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void UpdateProcessControlConditionWithErrorCallsShowSavingError()
        {
            var environment = new Environment();
            environment.Mock.ProcessControlData.SaveProcessControlConditionThrowsError = true;

            var diff = new ProcessControlConditionDiff(null, null, new ProcessControlCondition(), new ProcessControlCondition());
            environment.UseCase.UpdateProcessControlCondition(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { diff }, environment.Mock.Error);

            Assert.IsTrue(environment.Mock.Error.ShowProblemSavingProcessControlConditionCalled);
        }

        [Test]
        public void UpdateProcessControlConditionCallsTestDateCalculation()
        {
            var environment = new Environment();
            var id = new ProcessControlConditionId(0);
            environment.UseCase.UpdateProcessControlCondition(new List<ProcessControlConditionDiff>() { new ProcessControlConditionDiff(null, null, new ProcessControlCondition(), new ProcessControlCondition() { Id = id }) }, null);
            if (FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
            {
                Assert.AreEqual(1, environment.Mock.testDateCalculationUseCase.CalculateProcessControlDateForParameter.Count);
                Assert.AreSame(environment.Mock.testDateCalculationUseCase.CalculateProcessControlDateForParameter[0], id);
            }
        }

        [Test]
        public void LoadProcessControlConditionsWithErrorCallsShowSavingError()
        {
            var environment = new Environment();
            environment.Mock.ProcessControlData.LoadProcessControlConditionForLocationThrowsException = true;
            environment.UseCase.LoadProcessControlConditions(environment.Mock.Error);

            Assert.IsTrue(environment.Mock.Error.ShowProblemLoadingLocationProcessControlConditionCalled);
        }

        [Test]
        public void LoadProcessControlConditionsPassesDataFromDataAccessToGui()
        {
            var environment = new Environment();
            var list = new List<ProcessControlCondition>();
            environment.Mock.ProcessControlData.LoadProcessControlConditionsReturnValue = list;
            environment.UseCase.LoadProcessControlConditions(null);

            Assert.AreSame(list, environment.Mock.ProcessControlGui.ShowProcessControlConditionsParameter);
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    ProcessControlGui = new ProcessControlGuiMock();
                    ProcessControlData = new ProcessControlDataMock();
                    UserGetter = new UserGetterMock();
                    NotificationManager = new NotificationManagerMock();
                    Error = new ProcessControlErrorMock();
                    SaveGuiShower = new ProcessControlSaveGuiShowerMock();
                    testDateCalculationUseCase = new TestDateCalculationUseCaseMock();
                }

                public readonly ProcessControlGuiMock ProcessControlGui;
                public readonly ProcessControlDataMock ProcessControlData;
                public readonly UserGetterMock UserGetter;
                public readonly NotificationManagerMock NotificationManager;
                public readonly ProcessControlErrorMock Error;
                public readonly ProcessControlSaveGuiShowerMock SaveGuiShower;
                public readonly TestDateCalculationUseCaseMock testDateCalculationUseCase;
            }

            public Environment()
            {
                Mock = new Mocks();
                UseCase = new ProcessControlUseCase(Mock.ProcessControlGui, Mock.ProcessControlData, Mock.UserGetter, Mock.NotificationManager, Mock.testDateCalculationUseCase);
            }

            public readonly Mocks Mock;
            public readonly IProcessControlUseCase UseCase;
        }
    }
}
