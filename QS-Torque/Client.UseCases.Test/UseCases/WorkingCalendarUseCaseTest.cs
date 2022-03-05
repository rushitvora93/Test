using Client.Core.Diffs;
using Client.TestHelper.Mock;
using Client.UseCases.Test.UseCases;
using Core.Entities;
using Core.UseCases;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestHelper.Mock;

namespace Core.Test.UseCases
{
    class WorkingCalendarGuiMock : IWorkingCalendarGui
    {
        public List<WorkingCalendarEntry> ShowCalendarEntriesParameter { get; set; }
        public WorkingCalendarEntry AddWorkingCalendarEntryParameter { get; set; }
        public WorkingCalendarEntry RemoveWorkingCalendarEntryParameter { get; set; }
        public WorkingCalendar WorkingCalendarParameter { get; set; }

        public void ShowCalendarEntries(List<WorkingCalendarEntry> entries)
        {
            ShowCalendarEntriesParameter = entries;
        }

        public void AddWorkingCalendarEntry(WorkingCalendarEntry newEntry)
        {
            AddWorkingCalendarEntryParameter = newEntry;
        }

        public void RemoveWorkingCalendarEntry(WorkingCalendarEntry oldEntry)
        {
            RemoveWorkingCalendarEntryParameter = oldEntry;
        }

        public void LoadWeekendSettings(WorkingCalendar workingCalendar)
        {
            WorkingCalendarParameter = workingCalendar;
        }
    }

    class WorkingCalendarDataMock : IWorkingCalendarData
    {
        public bool LoadWorkingCalendarEntriesThrowsException { get; set; }
        public bool AddWorkingCalendarEntryThrowsException { get; set; }
        public bool RemoveWorkingCalendarEntryThrowsException { get; set; }
        public bool LoadWeekendSettingsThrowsException { get; set; }
        public bool SetWeekendSettingsThrowsException { get; set; }

        public WorkingCalendarEntry AddWorkingCalendarEntryParameterNewEntry { get; set; }
        public WorkingCalendarEntry RemoveWorkingCalendarEntryParameter { get; set; }
        public WorkingCalendarDiff WorkingCalendarDiffParameter { get; set; }
        public WorkingCalendarId LoadWorkingCalendarEntriesForWorkingCalendarIdParameter { get; set; }
        public WorkingCalendarId AddWorkingCalendarEntryParameterCalendarId { get; set; }

        public List<WorkingCalendarEntry> LoadWorkingCalendarEntriesReturnValue { get; set; }
        public WorkingCalendar LoadWeekendSettingsReturnValue { get; set; }

        public List<WorkingCalendarEntry> LoadWorkingCalendarEntriesForWorkingCalendarId(WorkingCalendarId id)
        {
            if (LoadWorkingCalendarEntriesThrowsException)
            {
                throw new Exception();
            }

            LoadWorkingCalendarEntriesForWorkingCalendarIdParameter = id;
            return LoadWorkingCalendarEntriesReturnValue;
        }

        public void AddWorkingCalendarEntry(WorkingCalendarEntry newEntry, WorkingCalendarId calendarId)
        {
            if(AddWorkingCalendarEntryThrowsException)
            {
                throw new Exception();
            }

            AddWorkingCalendarEntryParameterNewEntry = newEntry;
            AddWorkingCalendarEntryParameterCalendarId = calendarId;
        }

        public void RemoveWorkingCalendarEntry(WorkingCalendarEntry oldEntry)
        {
            if (RemoveWorkingCalendarEntryThrowsException)
            {
                throw new Exception();
            }

            RemoveWorkingCalendarEntryParameter = oldEntry;
        }

        public WorkingCalendar LoadWeekendSettings()
        {
            if (LoadWeekendSettingsThrowsException)
            {
                throw new Exception();
            }

            return LoadWeekendSettingsReturnValue;
        }

        public void SetWeekendSettings(WorkingCalendarDiff diff)
        {
            if (SetWeekendSettingsThrowsException)
            {
                throw new Exception();
            }

            WorkingCalendarDiffParameter = diff;
        }
    }

    public class WorkingCalendarErrorHandlerMock : IWorkingCalendarErrorHandler
    {
        public WorkingCalendarEntry WorkingCalendarErrorParameter { get; set; }

        public void WorkingCalendarError(WorkingCalendarEntry problematicEntry)
        {
            WorkingCalendarErrorParameter = problematicEntry;
        }
    }

    public class WorkingCalendarUseCaseTest
    {
        [Test]
        public void AddWorkingCalendarEntryCallsGui()
        {
            var environment = CreateUseCaseEnvironment();
            var entry = new WorkingCalendarEntry();
            environment.useCase.AddWorkingCalendarEntry(entry, environment.errorHandler, null);

            Assert.AreSame(entry, environment.gui.AddWorkingCalendarEntryParameter);
        }

        [Test]
        public void AddWorkingCalendarEntryCallsData()
        {
            var environment = CreateUseCaseEnvironment();
            var entry = new WorkingCalendarEntry();
            environment.useCase.AddWorkingCalendarEntry(entry, environment.errorHandler, null);

            Assert.AreSame(entry, environment.data.AddWorkingCalendarEntryParameterNewEntry);
        }

        [TestCase(4)]
        [TestCase(9)]
        public void AddWorkingCalendarEntryPassesCalendarIdToData(long calendarId)
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.LoadWeekendSettingsReturnValue = new WorkingCalendar()
            {
                Id = new WorkingCalendarId(calendarId)
            };
            environment.useCase.LoadWeekendSettings(environment.errorHandler);
            environment.useCase.AddWorkingCalendarEntry(new WorkingCalendarEntry(), environment.errorHandler, null);

            Assert.AreEqual(calendarId, environment.data.AddWorkingCalendarEntryParameterCalendarId.ToLong());
        }

        [Test]
        public void AddWorkingCalendarEntryPassesCalendarIdOneToData()
        {
            var environment = CreateUseCaseEnvironment();
            environment.useCase.AddWorkingCalendarEntry(new WorkingCalendarEntry(), environment.errorHandler, null);

            Assert.AreEqual(1, environment.data.AddWorkingCalendarEntryParameterCalendarId.ToLong());
        }

        [Test]
        public void AddWorkingCalendarEntryCallsError()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.AddWorkingCalendarEntryThrowsException = true;
            var entry = new WorkingCalendarEntry();
            environment.useCase.AddWorkingCalendarEntry(entry, environment.errorHandler, null);

            Assert.AreSame(entry, environment.errorHandler.WorkingCalendarErrorParameter);
        }

        [Test]
        public void AddWorkingCalendarEntryCallsRemoveWorkingCalendarEntryForPreexisting()
        {
            var environment = CreateUseCaseEnvironment();
            var entry = new WorkingCalendarEntry();
            var preexisting = new WorkingCalendarEntry();
            environment.useCase.AddWorkingCalendarEntry(entry, environment.errorHandler, null, preexisting);
            
            Assert.AreSame(preexisting, environment.data.RemoveWorkingCalendarEntryParameter);
            Assert.AreSame(preexisting, environment.gui.RemoveWorkingCalendarEntryParameter);
        }

        [Test]
        public void RemoveWorkingCalendarEntryCallsGui()
        {
            var environment = CreateUseCaseEnvironment();
            var entry = new WorkingCalendarEntry();
            environment.useCase.RemoveWorkingCalendarEntry(entry, environment.errorHandler, null);

            Assert.AreSame(entry, environment.gui.RemoveWorkingCalendarEntryParameter);
        }

        [Test]
        public void RemoveWorkingCalendarEntryCallsData()
        {
            var environment = CreateUseCaseEnvironment();
            var entry = new WorkingCalendarEntry();
            environment.useCase.RemoveWorkingCalendarEntry(entry, environment.errorHandler, null);

            Assert.AreSame(entry, environment.data.RemoveWorkingCalendarEntryParameter);
        }

        [Test]
        public void RemoveWorkingCalendarEntryCallsError()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.RemoveWorkingCalendarEntryThrowsException = true;
            var entry = new WorkingCalendarEntry();
            environment.useCase.RemoveWorkingCalendarEntry(entry, environment.errorHandler, null);

            Assert.AreSame(entry, environment.errorHandler.WorkingCalendarErrorParameter);
        }

        [Test]
        public void LoadWorkingCalendarEntriesPassesDataFromDataToGui()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.LoadWorkingCalendarEntriesReturnValue = new List<WorkingCalendarEntry>();
            environment.useCase.LoadCalendarEntries(environment.errorHandler);

            Assert.AreSame(environment.data.LoadWorkingCalendarEntriesReturnValue, environment.gui.ShowCalendarEntriesParameter);
        }

        [Test]
        public void LoadWorkingCalendarEntriesCallsError()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.LoadWorkingCalendarEntriesThrowsException = true;
            environment.errorHandler.WorkingCalendarErrorParameter = new WorkingCalendarEntry();
            environment.useCase.LoadCalendarEntries(environment.errorHandler);

            Assert.IsNull(environment.errorHandler.WorkingCalendarErrorParameter);
        }

        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void LoadWeekendSettingsPassesDataFromDataAccessToGui(bool saturdaysFree, bool sundaysFree)
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.LoadWeekendSettingsReturnValue = new WorkingCalendar()
            {
                AreSaturdaysFree = saturdaysFree,
                AreSundaysFree = sundaysFree
            };
            environment.useCase.LoadWeekendSettings(environment.errorHandler);
            Assert.AreEqual(saturdaysFree, environment.gui.WorkingCalendarParameter.AreSaturdaysFree);
            Assert.AreEqual(sundaysFree, environment.gui.WorkingCalendarParameter.AreSundaysFree);
        }

        [Test]
        public void LoadWeekendSettingsCallsError()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.LoadWeekendSettingsThrowsException = true;
            environment.errorHandler.WorkingCalendarErrorParameter = new WorkingCalendarEntry();
            environment.useCase.LoadWeekendSettings(environment.errorHandler);

            Assert.IsNull(environment.errorHandler.WorkingCalendarErrorParameter);
        }

        [Test]
        public void SetWeekendSettingsPassesDataToDataAccess()
        {
            var environment = CreateUseCaseEnvironment();
            var oldVal = new WorkingCalendar();
            var newVal = new WorkingCalendar();
            var user = new User();
            environment.userGetter.NextReturnedUser = user;
            environment.useCase.SetWeekendSettings(new WorkingCalendarDiff(oldVal, newVal), environment.errorHandler, null);
            Assert.AreSame(newVal, environment.data.WorkingCalendarDiffParameter.New);
            Assert.AreSame(oldVal, environment.data.WorkingCalendarDiffParameter.Old);
            Assert.AreSame(user, environment.data.WorkingCalendarDiffParameter.User); 
        }

        [Test]
        public void SetWeekendSettingsCallsError()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.SetWeekendSettingsThrowsException = true;
            environment.errorHandler.WorkingCalendarErrorParameter = new WorkingCalendarEntry();
            environment.useCase.SetWeekendSettings(new WorkingCalendarDiff(null, null), environment.errorHandler, null);

            Assert.IsNull(environment.errorHandler.WorkingCalendarErrorParameter);
        }

        [TestCase(98)]
        [TestCase(65)]
        public void LoadWorkingCalendarEntriesForWorkingCalendarIdPassesPreviouslyLoadedIdToDataAccess(long id)
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.LoadWeekendSettingsReturnValue = new WorkingCalendar()
            {
                Id = new WorkingCalendarId(id)
            };
            environment.useCase.LoadWeekendSettings(environment.errorHandler);
            environment.useCase.LoadCalendarEntries(environment.errorHandler);
            Assert.AreEqual(id, environment.data.LoadWorkingCalendarEntriesForWorkingCalendarIdParameter.ToLong());
        }
        
        [Test]
        public void AddWorkingCalendarEntryCallsTestDateCalculation()
        {
            var environment = CreateUseCaseEnvironment();
            environment.useCase.AddWorkingCalendarEntry(null, environment.errorHandler, null);
            Assert.AreEqual(FeatureToggles.FeatureToggles.TestDateCalculation, environment.testDateCalculationUseCase.CalculateToolTestDateForAllLocationToolAssignmentsCalled);
            Assert.AreEqual((FeatureToggles.FeatureToggles.TestDateCalculation && FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl),
                environment.testDateCalculationUseCase.CalculateProcessControlDateForAllProcessControlConditionsCalled);
        }

        [Test]
        public void RemoveWorkingCalendarEntryCallsTestDateCalculation()
        {
            var environment = CreateUseCaseEnvironment();
            environment.useCase.RemoveWorkingCalendarEntry(null, environment.errorHandler, null);
            Assert.AreEqual(FeatureToggles.FeatureToggles.TestDateCalculation, environment.testDateCalculationUseCase.CalculateToolTestDateForAllLocationToolAssignmentsCalled);
            Assert.AreEqual((FeatureToggles.FeatureToggles.TestDateCalculation && FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl),
                environment.testDateCalculationUseCase.CalculateProcessControlDateForAllProcessControlConditionsCalled);
        }
        
        [Test]
        public void SetWeekendSettingsCallsTestDateCalculation()
        {
            var environment = CreateUseCaseEnvironment();
            environment.useCase.SetWeekendSettings(new WorkingCalendarDiff(null, null), environment.errorHandler, null);
            Assert.AreEqual(FeatureToggles.FeatureToggles.TestDateCalculation, environment.testDateCalculationUseCase.CalculateToolTestDateForAllLocationToolAssignmentsCalled);
            Assert.AreEqual((FeatureToggles.FeatureToggles.TestDateCalculation && FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl),
                environment.testDateCalculationUseCase.CalculateProcessControlDateForAllProcessControlConditionsCalled);
        }

        [Test]
        public void AddWorkingCalendarEntryCallsLoadProcessControlConditions()
        {
            var environment = CreateUseCaseEnvironment();
            environment.useCase.AddWorkingCalendarEntry(new WorkingCalendarEntry(), null, environment.processControlErrorHandler);

            if (FeatureToggles.FeatureToggles.TestDateCalculation && FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
            {
                Assert.AreEqual(environment.processControlErrorHandler, environment.processControlUseCase.LoadProcessControlConditionsErrorHandler);
            }
        }

        [Test]
        public void RomveWorkingCalendarEntryCallsLoadProcessControlConditions()
        {
            var environment = CreateUseCaseEnvironment();
            environment.useCase.RemoveWorkingCalendarEntry(new WorkingCalendarEntry(), null, environment.processControlErrorHandler);

            if (FeatureToggles.FeatureToggles.TestDateCalculation && FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
            {
                Assert.AreEqual(environment.processControlErrorHandler, environment.processControlUseCase.LoadProcessControlConditionsErrorHandler);
            }
        }

        [Test]
        public void SetWeekendSettingsEntryCallsLoadProcessControlConditions()
        {
            var environment = CreateUseCaseEnvironment();
            environment.useCase.SetWeekendSettings(new WorkingCalendarDiff(new WorkingCalendar(), new WorkingCalendar()), null, environment.processControlErrorHandler);

            if (FeatureToggles.FeatureToggles.TestDateCalculation && FeatureToggles.FeatureToggles.SilverTowel_1136_TestPlanningForProcessControl)
            {
                Assert.AreEqual(environment.processControlErrorHandler, environment.processControlUseCase.LoadProcessControlConditionsErrorHandler);
            }
        }

        [Test]
        public void AssignTestLevelSetToLocationToolAssignmentsCallsLoadLocationToolAssignments()
        {
            var environment = CreateUseCaseEnvironment();
            environment.useCase.AddWorkingCalendarEntry(null, environment.errorHandler, null);
            Assert.IsTrue(environment.locationToolAssignmentUseCase.LoadLocationToolAssignmentsCalled);
        }

        [Test]
        public void RemoveWorkingCalendarEntryCallsLoadLocationToolAssignments()
        {
            var environment = CreateUseCaseEnvironment();
            environment.useCase.RemoveWorkingCalendarEntry(null, environment.errorHandler, null);
            Assert.IsTrue(environment.locationToolAssignmentUseCase.LoadLocationToolAssignmentsCalled);
        }

        [Test]
        public void SetWeekendSettingsCallsLoadLocationToolAssignments()
        {
            var environment = CreateUseCaseEnvironment();
            environment.useCase.SetWeekendSettings(new WorkingCalendarDiff(null, null), environment.errorHandler, null);
            Assert.IsTrue(environment.locationToolAssignmentUseCase.LoadLocationToolAssignmentsCalled);
        }


        private static Environment CreateUseCaseEnvironment()
        {
            var environment = new Environment();
            environment.gui = new WorkingCalendarGuiMock();
            environment.data = new WorkingCalendarDataMock();
            environment.testDateCalculationUseCase = new TestDateCalculationUseCaseMock();
            environment.locationToolAssignmentUseCase = new LocationToolAssignmentUseCaseMock();
            environment.processControlUseCase = new ProcessControlUseCaseMock();
            environment.processControlErrorHandler = new ProcessControlErrorMock();
            environment.errorHandler = new WorkingCalendarErrorHandlerMock();
            environment.userGetter = new UserGetterMock();
            environment.notification = new NotificationManagerMock();
            environment.useCase = new WorkingCalendarUseCase(environment.gui, 
                environment.data, 
                environment.testDateCalculationUseCase, 
                environment.locationToolAssignmentUseCase,
                environment.processControlUseCase,
                environment.userGetter);
            return environment;
        }

        struct Environment
        {
            public WorkingCalendarUseCase useCase;
            public WorkingCalendarGuiMock gui;
            public WorkingCalendarDataMock data;
            public TestDateCalculationUseCaseMock testDateCalculationUseCase;
            public LocationToolAssignmentUseCaseMock locationToolAssignmentUseCase;
            public ProcessControlUseCaseMock processControlUseCase;
            public ProcessControlErrorMock processControlErrorHandler;
            public WorkingCalendarErrorHandlerMock errorHandler;
            public UserGetterMock userGetter;
            public NotificationManagerMock notification;
        }
    }
}
