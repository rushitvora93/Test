using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Client.Core.Diffs;
using Client.TestHelper.Factories;
using Client.UseCases.UseCases;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using InterfaceAdapters;
using InterfaceAdapters.Communication;
using InterfaceAdapters.Models;
using NUnit.Framework;
using TestHelper.Factories;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    public class FilteredObservableCollectionMock<T> : IFilteredObservableCollectionExtension<T>
    {
        public bool IsDisposed = false;

        public Predicate<T> Filter { get; set; }
        public int Count { get; }
        public event EventHandler Refiltered;
        public void RefilterCollection()
        {
            throw new NotImplementedException();
        }

        public void SetNewSourceCollection(ObservableCollection<T> newColl)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            IsDisposed = true;
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public void Move(int oldIndex, int newIndex)
        {
            throw new NotImplementedException();
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
    }


    class TestLevelSetUseCaseMock : ITestLevelSetUseCase
    {
        public bool LoadTestLevelSetsCalled { get; set; }
        public TestLevelSet AddTestLevelSetParameter { get; set; }
        public TestLevelSet DoesTestLevelSetHaveReferencesParameter { get; set; }
        public bool DoesTestLevelSetHaveReferencesReturnValue { get; set; }
        public TestLevelSet RemoveTestLevelSetParameter { get; set; }
        public TestLevelSetDiff UpdateTestLevelSetParameter { get; set; }
        public string IsTestLevelSetNameUniqueParameter { get; set; }
        public bool IsTestLevelSetNameUniqueReturnValue { get; set; }

        public ITestLevelSetErrorHandler LoadTestLevelSetsErrorHandler { get; set; }
        public ITestLevelSetErrorHandler AddTestLevelSetErrorHandler { get; set; }
        public ITestLevelSetErrorHandler RemoveTestLevelSetErrorHandler { get; set; }
        public ITestLevelSetErrorHandler UpdateTestLevelSetErrorHandler { get; set; }

        public void LoadTestLevelSets(ITestLevelSetErrorHandler errorHandler)
        {
            LoadTestLevelSetsCalled = true;
            LoadTestLevelSetsErrorHandler = errorHandler;
        }

        public void AddTestLevelSet(TestLevelSet newItem, ITestLevelSetErrorHandler errorHandler)
        {
            AddTestLevelSetParameter = newItem;
            AddTestLevelSetErrorHandler = errorHandler;
        }

        public void RemoveTestLevelSet(TestLevelSet oldItem, ITestLevelSetErrorHandler errorHandler)
        {
            RemoveTestLevelSetParameter = oldItem;
            RemoveTestLevelSetErrorHandler = errorHandler;
        }

        public void UpdateTestLevelSet(TestLevelSetDiff diff, ITestLevelSetErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler, ITestLevelSetDiffShower diffShower)
        {
            UpdateTestLevelSetParameter = diff;
            UpdateTestLevelSetErrorHandler = errorHandler;
        }

        public bool IsTestLevelSetNameUnique(string name)
        {
            IsTestLevelSetNameUniqueParameter = name;
            return IsTestLevelSetNameUniqueReturnValue;
        }

        public bool DoesTestLevelSetHaveReferences(TestLevelSet set)
        {
            DoesTestLevelSetHaveReferencesParameter = set;
            return DoesTestLevelSetHaveReferencesReturnValue;
        }
    }

    class WorkingCalendarUseCaseMock : IWorkingCalendarUseCase
    {
        public bool LoadCalendarEntriesCalled { get; set; }
        public bool LoadWeekendSettingsCalled { get; set; }
        public WorkingCalendarEntry RemoveWorkingCalendarEntryParameter { get; set; }
        public WorkingCalendarDiff WorkingCalendarDiffParameter { get; set; }

        public IWorkingCalendarErrorHandler LoadCalendarEntriesErrorHandler { get; set; }
        public IWorkingCalendarErrorHandler RemoveWorkingCalendarEntryErrorHandler { get; set; }
        public IWorkingCalendarErrorHandler LoadWeekendSettingsErrorHandler { get; set; }
        public IWorkingCalendarErrorHandler SetWeekendSettingsErrorHandler { get; set; }

        public void LoadCalendarEntries(IWorkingCalendarErrorHandler errorHandler)
        {
            LoadCalendarEntriesCalled = true;
            LoadCalendarEntriesErrorHandler = errorHandler;
        }

        public void AddWorkingCalendarEntry(WorkingCalendarEntry newEntry, IWorkingCalendarErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler, WorkingCalendarEntry preexisting = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveWorkingCalendarEntry(WorkingCalendarEntry oldEntry, IWorkingCalendarErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler, bool withTestDateCalculation = true)
        {
            RemoveWorkingCalendarEntryParameter = oldEntry;
            RemoveWorkingCalendarEntryErrorHandler = errorHandler;
        }

        public void LoadWeekendSettings(IWorkingCalendarErrorHandler errorHandler)
        {
            LoadWeekendSettingsCalled = true;
            LoadWeekendSettingsErrorHandler = errorHandler;
        }

        public void SetWeekendSettings(WorkingCalendarDiff diff, IWorkingCalendarErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler)
        {
            WorkingCalendarDiffParameter = diff;
            SetWeekendSettingsErrorHandler = errorHandler;
        }
    }

    class ShiftManagementUseCaseMock : IShiftManagementUseCase
    {
        public bool LoadShiftManagementCalled { get; set; }
        public ShiftManagement SaveShiftManagementParameter { get; set; }
        public ShiftManagementDiff SaveShiftManagementDiffParameter { get; set; }
        public IShiftManagementErrorHandler SaveShiftManagementErrorHandlerParameter { get; set; }
        public IShiftManagementDiffShower SaveShiftManagementDiffShowerParameter { get; set; }
        public IShiftManagementErrorHandler LoadShiftManagementErrorHandler { get; set; }
        public IShiftManagementErrorHandler SaveShiftManagementErrorHandler { get; set; }

        public void LoadShiftManagement(IShiftManagementErrorHandler errorHandler)
        {
            LoadShiftManagementCalled = true;
            LoadShiftManagementErrorHandler = errorHandler;
        }

        public void SaveShiftManagement(ShiftManagement entity, IShiftManagementErrorHandler errorHandler)
        {
            SaveShiftManagementParameter = entity;
            SaveShiftManagementErrorHandler = errorHandler;
        }

        public void SaveShiftManagement(ShiftManagementDiff diff, IShiftManagementErrorHandler errorHandler, IProcessControlErrorGui processControlErrorHandler, IShiftManagementDiffShower diffShower)
        {
            SaveShiftManagementDiffParameter = diff;
            SaveShiftManagementErrorHandlerParameter = errorHandler;
            SaveShiftManagementDiffShowerParameter = diffShower;
        }
    }

    class TestLevelSetInterfaceMock : ITestLevelSetInterface
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<TestLevelSetModel> _testLevelSets;
        public ObservableCollection<TestLevelSetModel> TestLevelSets
        {
            get => _testLevelSets;
            set
            {
                _testLevelSets = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TestLevelSets)));
            }
        }
        public TestLevelSetModel SelectedTestLevelSet { get; set; }
        public TestLevelSetModel TestLevelSetWithoutChanges { get; set; }
        public event EventHandler<bool> ShowLoadingControlRequest;
        public event EventHandler TestLevelSetErrorRequest;

        public void SetGuiDispatcher(Dispatcher guiDispatcher)
        {
            
        }
    }

    class ShiftManagementInterfaceMock : IShiftManagementInterface
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ShiftManagementModel CurrentShiftManagement { get; set; }
        public ShiftManagementModel ShiftManagementWithoutChanges { get; set; }
        public event EventHandler<bool> ShowLoadingControlRequest;
        public event EventHandler ShiftManagementErrorRequest;
    }

    class WorkingCalendarInterfaceMock : IWorkingCalendarInterface
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<InterfaceAdapters.Models.WorkingCalendarEntryModel> WorkingCalendarEntries { get; set; } = new ObservableCollection<WorkingCalendarEntryModel>();
        public InterfaceAdapters.Models.WorkingCalendarEntryModel SelectedEntryInList { get; set; }
        public DateTime? SelectedDateInCalendar { get; set; }
        public bool AreSaturdaysFree { get; set; }
        public bool AreSundaysFree { get; set; }
        public WorkingCalendar WorkingCalendarWithoutChanges { get; set; }

        public event EventHandler<bool> ShowLoadingControlRequest;
        public void SetGuiDispatcher(Dispatcher dispatcher)
        {
            
        }

        public event EventHandler<WorkingCalendarEntry> WorkingCalendarErrorRequest;

        public void RaiseWorkingCalendarErrorRequest()
        {
            WorkingCalendarErrorRequest?.Invoke(this, null);
        }
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class TestLevelSetViewModelTest
    {
        [Test]
        public void LoadWorkingCalendarIfNotLoadedYetLoadsDataJustOnce()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.LoadWorkingCalendarIfNotLoadedYet();
            Assert.IsTrue(environment.workingCalendarUseCase.LoadCalendarEntriesCalled);
            Assert.IsTrue(environment.workingCalendarUseCase.LoadWeekendSettingsCalled);

            environment.workingCalendarUseCase.LoadCalendarEntriesCalled = false;
            environment.workingCalendarUseCase.LoadWeekendSettingsCalled = false;
            environment.viewModel.LoadWorkingCalendarIfNotLoadedYet();
            Assert.IsFalse(environment.workingCalendarUseCase.LoadCalendarEntriesCalled);
            Assert.IsFalse(environment.workingCalendarUseCase.LoadWeekendSettingsCalled);
        }

        [Test]
        public void AddTestLevelSetCommandAsksForSavingIfSelectedHasChanged()
        {
            var environment = CreateViewModelEnvironment();
            bool messageBoxRequestCalled = false;
            environment.viewModel.MessageBoxRequest += (s, e) => messageBoxRequestCalled = true;
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("gfisdhoü")
            });
            environment.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("305689hn305896g")
            });
            environment.viewModel.AddTestLevelSetCommand.Execute(null);
            Assert.IsTrue(messageBoxRequestCalled);
        }

        [Test]
        public void AddTestLevelSetCommandAddsNewEntryViaUseCase()
        {
            var environment = CreateViewModelEnvironment();
            environment.testLevelSetInterface.TestLevelSets = new ObservableCollection<TestLevelSetModel>();
            environment.viewModel.AddTestLevelSetCommand.Execute(null);
            Assert.IsNotNull(environment.testLevelSetUseCase.AddTestLevelSetParameter);
            Assert.AreSame(environment.viewModel, environment.testLevelSetUseCase.AddTestLevelSetErrorHandler);
        }

        [Test]
        public void SetSelectedEntryInListToNullDoesNotResetSelectedDateInCalendar()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedDateInCalendar = DateTime.Today;
            environment.viewModel.SelectedEntryInList = null;
            Assert.AreEqual(DateTime.Today, environment.viewModel.SelectedDateInCalendar);
        }

        [Test]
        public void SetSelectedDateInCalendarToNullDoesNotResetSelectedEntryInList()
        {
            var environment = CreateViewModelEnvironment();
            var entry = new WorkingCalendarEntry();
            environment.viewModel.SelectedEntryInList = new WorkingCalendarEntryModel(entry);
            environment.viewModel.SelectedDateInCalendar = null;
            Assert.AreEqual(entry, environment.viewModel.SelectedEntryInList.Entity);
        }

        [Test]
        public void RemoveEntryCommandCallsUseCaseWithSelectedEntryInList()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.MessageBoxRequest += (s, e) => e.ResultAction(MessageBoxResult.Yes);
            var entry = new WorkingCalendarEntry();
            environment.viewModel.SelectedEntryInList = new WorkingCalendarEntryModel(entry);
            environment.viewModel.RemoveWorkingCalendarEntryCommand.Invoke(null);
            Assert.AreEqual(entry, environment.workingCalendarUseCase.RemoveWorkingCalendarEntryParameter);
            Assert.AreEqual(environment.viewModel, environment.workingCalendarUseCase.RemoveWorkingCalendarEntryErrorHandler);
        }

        [Test]
        public void RemoveEntryCommandCallsMessageBoxRequest()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.MessageBoxRequest += (s, e) => Assert.Pass();
            var entry = new WorkingCalendarEntry();
            environment.viewModel.SelectedEntryInList = new WorkingCalendarEntryModel(entry);
            environment.viewModel.RemoveWorkingCalendarEntryCommand.Invoke(null);
        }

        [Test]
        public void RemoveEntryCanExecuteReturnsFalseIfSelectedEntryInListIsNull()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedEntryInList = null;
            Assert.IsFalse(environment.viewModel.RemoveWorkingCalendarEntryCommand.CanExecute(null));
        }

        [Test()]
        public void RemoveEntryCanExecuteReturnsTrueIfSelectedEntryInListIsNotNull()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedEntryInList = new WorkingCalendarEntryModel(new WorkingCalendarEntry());
            Assert.IsTrue(environment.viewModel.RemoveWorkingCalendarEntryCommand.CanExecute(null));
        }

        [Test]
        public void ChangeAreSaturdaysFreeCallsUseCase()
        {
            var environment = CreateViewModelEnvironment();
            var newValue = !environment.viewModel.AreSaturdaysFree;
            environment.workingCalendarInterface.WorkingCalendarWithoutChanges = new WorkingCalendar();
            environment.viewModel.AreSaturdaysFree = newValue;
            Assert.AreEqual(newValue, environment.workingCalendarUseCase.WorkingCalendarDiffParameter.New.AreSaturdaysFree);
            Assert.AreEqual(environment.viewModel.AreSundaysFree, environment.workingCalendarUseCase.WorkingCalendarDiffParameter.New.AreSundaysFree);
            Assert.AreSame(environment.workingCalendarInterface.WorkingCalendarWithoutChanges, environment.workingCalendarUseCase.WorkingCalendarDiffParameter.Old);
            Assert.AreEqual(environment.viewModel, environment.workingCalendarUseCase.SetWeekendSettingsErrorHandler);
        }

        [Test]
        public void ChangeAreSundaysFreeCallsUseCase()
        {
            var environment = CreateViewModelEnvironment();
            var newValue = !environment.viewModel.AreSundaysFree;
            environment.workingCalendarInterface.WorkingCalendarWithoutChanges = new WorkingCalendar();
            environment.viewModel.AreSundaysFree = newValue;
            Assert.AreEqual(environment.viewModel.AreSaturdaysFree, environment.workingCalendarUseCase.WorkingCalendarDiffParameter.New.AreSaturdaysFree);
            Assert.AreEqual(newValue, environment.workingCalendarUseCase.WorkingCalendarDiffParameter.New.AreSundaysFree);
            Assert.AreSame(environment.workingCalendarInterface.WorkingCalendarWithoutChanges, environment.workingCalendarUseCase.WorkingCalendarDiffParameter.Old);
            Assert.AreEqual(environment.viewModel, environment.workingCalendarUseCase.SetWeekendSettingsErrorHandler);
        }

        [Test]
        public void SaveShiftManagementCanExecuteReturnsTrueIfEntitiesAreNotEqual()
        {
            var environment = CreateViewModelEnvironment();
            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(new ShiftManagement()
            {
                FirstShiftStart = TimeSpan.FromTicks(9876546)
            }, new NullLocalizationWrapper());
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(new ShiftManagement()
            {
                FirstShiftStart = TimeSpan.FromTicks(541258)
            }, new NullLocalizationWrapper());
            Assert.IsTrue(environment.viewModel.SaveShiftManagementCommand.CanExecute(null));
        }

        [Test]
        public void SaveShiftManagementCanExecuteReturnsFalseIfEntitiesAreEqual()
        {
            var environment = CreateViewModelEnvironment();
            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(new ShiftManagement()
            {
                FirstShiftStart = TimeSpan.FromTicks(9876546)
            }, new NullLocalizationWrapper());
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(new ShiftManagement()
            {
                FirstShiftStart = TimeSpan.FromTicks(9876546)
            }, new NullLocalizationWrapper());
            Assert.IsFalse(environment.viewModel.SaveShiftManagementCommand.CanExecute(null));
        }

        [Test]
        public void SaveShiftManagementCanExecuteReturnsFalseIfCurrentShiftManagementIsNull()
        {
            var environment = CreateViewModelEnvironment();
            environment.shiftManagementInterface.CurrentShiftManagement = null;
            Assert.IsFalse(environment.viewModel.SaveShiftManagementCommand.CanExecute(null));
        }

        [Test]
        public void SaveShiftManagementCanExecuteReturnsFalseIfFirstShiftStartOfCurrentShiftManagementIsInvalid()
        {
            var environment = CreateViewModelEnvironment();
            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(new ShiftManagement()
            {
                FirstShiftStart = new TimeSpan(5, 0, 0),
                FirstShiftEnd = new TimeSpan(14, 0, 0),
                SecondShiftStart = new TimeSpan(14, 0, 0),
                SecondShiftEnd = new TimeSpan(22, 0, 0),
                ThirdShiftStart = new TimeSpan(22, 0, 0),
                ThirdShiftEnd = new TimeSpan(6, 0, 0),
                IsSecondShiftActive = true,
                IsThirdShiftActive = true
            }, new NullLocalizationWrapper());
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(new ShiftManagement()
            {
                FirstShiftStart = new TimeSpan(6, 0, 0),
                FirstShiftEnd = new TimeSpan(14, 0, 0),
                SecondShiftStart = new TimeSpan(14, 0, 0),
                SecondShiftEnd = new TimeSpan(22, 0, 0),
                ThirdShiftStart = new TimeSpan(22, 0, 0),
                ThirdShiftEnd = new TimeSpan(6, 0, 0),
                IsSecondShiftActive = true,
                IsThirdShiftActive = true
            }, new NullLocalizationWrapper());
            Assert.IsFalse(environment.viewModel.SaveShiftManagementCommand.CanExecute(null));
        }

        [Test]
        public void SaveShiftManagementCanExecuteReturnsFalseIfSecondShiftStartOfCurrentShiftManagementIsInvalid()
        {
            var environment = CreateViewModelEnvironment();
            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(new ShiftManagement()
            {
                FirstShiftStart = new TimeSpan(6, 0, 0),
                FirstShiftEnd = new TimeSpan(14, 0, 0),
                SecondShiftStart = new TimeSpan(13, 0, 0),
                SecondShiftEnd = new TimeSpan(22, 0, 0),
                ThirdShiftStart = new TimeSpan(22, 0, 0),
                ThirdShiftEnd = new TimeSpan(6, 0, 0),
                IsSecondShiftActive = true,
                IsThirdShiftActive = true
            }, new NullLocalizationWrapper());
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(new ShiftManagement()
            {
                FirstShiftStart = new TimeSpan(5, 0, 0),
                FirstShiftEnd = new TimeSpan(14, 0, 0),
                SecondShiftStart = new TimeSpan(14, 0, 0),
                SecondShiftEnd = new TimeSpan(22, 0, 0),
                ThirdShiftStart = new TimeSpan(22, 0, 0),
                ThirdShiftEnd = new TimeSpan(6, 0, 0),
                IsSecondShiftActive = true,
                IsThirdShiftActive = true
            }, new NullLocalizationWrapper());
            Assert.IsFalse(environment.viewModel.SaveShiftManagementCommand.CanExecute(null));
        }

        [Test]
        public void SaveShiftManagementCanExecuteReturnsFalseIfThirdShiftStartOfCurrentShiftManagementIsInvalid()
        {
            var environment = CreateViewModelEnvironment();
            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(new ShiftManagement()
            {
                FirstShiftStart = new TimeSpan(6, 0, 0),
                FirstShiftEnd = new TimeSpan(14, 0, 0),
                SecondShiftStart = new TimeSpan(14, 0, 0),
                SecondShiftEnd = new TimeSpan(22, 0, 0),
                ThirdShiftStart = new TimeSpan(21, 0, 0),
                ThirdShiftEnd = new TimeSpan(6, 0, 0),
                IsSecondShiftActive = true,
                IsThirdShiftActive = true
            }, new NullLocalizationWrapper());
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(new ShiftManagement()
            {
                FirstShiftStart = new TimeSpan(5, 0, 0),
                FirstShiftEnd = new TimeSpan(14, 0, 0),
                SecondShiftStart = new TimeSpan(14, 0, 0),
                SecondShiftEnd = new TimeSpan(22, 0, 0),
                ThirdShiftStart = new TimeSpan(22, 0, 0),
                ThirdShiftEnd = new TimeSpan(6, 0, 0),
                IsSecondShiftActive = true,
                IsThirdShiftActive = true
            }, new NullLocalizationWrapper());
            Assert.IsFalse(environment.viewModel.SaveShiftManagementCommand.CanExecute(null));
        }

        [Test]
        public void SaveShiftManagementExecuteCallsUseCaseWithDiff()
        {
            var environment = CreateViewModelEnvironment();
            var newShifts = new ShiftManagement();
            var oldShifts = new ShiftManagement();
            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(newShifts, new NullLocalizationWrapper());
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(oldShifts, new NullLocalizationWrapper());
            environment.viewModel.SaveShiftManagementCommand.Execute(null);
            Assert.AreSame(oldShifts, environment.shiftManagementUseCase.SaveShiftManagementDiffParameter.Old);
            Assert.AreSame(newShifts, environment.shiftManagementUseCase.SaveShiftManagementDiffParameter.New);
            Assert.AreSame(environment.viewModel, environment.shiftManagementUseCase.SaveShiftManagementErrorHandlerParameter);
            Assert.AreSame(environment.viewModel, environment.shiftManagementUseCase.SaveShiftManagementDiffShowerParameter);
        }

        [Test]
        public void HasShiftManagementChangedReturnsFalseIfThereAreNoChanges()
        {
            var environment = CreateViewModelEnvironment();
            var management = new ShiftManagement();
            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(management, new NullLocalizationWrapper());
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(management, new NullLocalizationWrapper());
            Assert.IsFalse(environment.viewModel.HasShiftManagementChanged());
        }

        [Test]
        public void HasShiftManagementChangedReturnsTrueIfThereAreChanges()
        {
            var environment = CreateViewModelEnvironment();
            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(new ShiftManagement() { FirstDayOfWeek = DayOfWeek.Monday }, new NullLocalizationWrapper());
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(new ShiftManagement() { FirstDayOfWeek = DayOfWeek.Tuesday }, new NullLocalizationWrapper());
            Assert.IsTrue(environment.viewModel.HasShiftManagementChanged());
        }

        [Test]
        public void ResetShiftManagementSetCurrentShiftManagementToShiftManagementWithoutChanges()
        {
            var environment = CreateViewModelEnvironment();
            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(new ShiftManagement() { FirstDayOfWeek = DayOfWeek.Monday }, new NullLocalizationWrapper());
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(new ShiftManagement() { FirstDayOfWeek = DayOfWeek.Tuesday }, new NullLocalizationWrapper());
            environment.viewModel.ResetShiftManagement();
            Assert.IsTrue(environment.shiftManagementInterface.CurrentShiftManagement.Entity.EqualsByContent(environment.shiftManagementInterface.ShiftManagementWithoutChanges.Entity));
        }

        [Test]
        public void CanCloseCallsMessageBoxRequestIfShiftManagementIsInvalid()
        {
            var environment = CreateViewModelEnvironment();
            bool messageBoxRequestInvoked = false;
            bool canCloseResult = false;

            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(new ShiftManagement()
            {
                FirstShiftEnd = TimeSpan.FromHours(5),
                SecondShiftStart = TimeSpan.FromHours(4),
                IsSecondShiftActive = true
            }, new NullLocalizationWrapper());
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(new ShiftManagement() { FirstShiftEnd = TimeSpan.FromHours(5), SecondShiftStart = TimeSpan.FromHours(4) }, new NullLocalizationWrapper());
            environment.viewModel.MessageBoxRequest += (s, e) => messageBoxRequestInvoked = true;
            canCloseResult = environment.viewModel.CanClose();

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsFalse(canCloseResult);
        }

        [Test]
        public void CanCloseCallsMessageBoxRequestIfShiftManagementChanged()
        {
            var environment = CreateViewModelEnvironment();
            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(new ShiftManagement() { FirstDayOfWeek = DayOfWeek.Monday }, new NullLocalizationWrapper());
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(new ShiftManagement() { FirstDayOfWeek = DayOfWeek.Tuesday }, new NullLocalizationWrapper());
            environment.viewModel.MessageBoxRequest += (s, e) => Assert.Pass();
            environment.viewModel.CanClose();
        }

        [Test]
        public void CanCloseDoesNotCallsMessageBoxRequestIfShiftManagementHasNotChanged()
        {
            var environment = CreateViewModelEnvironment();
            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(new ShiftManagement() { FirstDayOfWeek = DayOfWeek.Tuesday }, new NullLocalizationWrapper());
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(new ShiftManagement() { FirstDayOfWeek = DayOfWeek.Tuesday }, new NullLocalizationWrapper());
            environment.viewModel.MessageBoxRequest += (s, e) => Assert.Fail();
            Assert.Pass();
            Assert.IsTrue(environment.viewModel.CanClose());
        }

        [Test]
        public void CanCloseSavesShiftManagement()
        {
            var environment = CreateViewModelEnvironment();
            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(new ShiftManagement() { FirstDayOfWeek = DayOfWeek.Monday }, new NullLocalizationWrapper());
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(new ShiftManagement() { FirstDayOfWeek = DayOfWeek.Tuesday }, new NullLocalizationWrapper());
            environment.viewModel.MessageBoxRequest += (s, e) => e.ResultAction(MessageBoxResult.Yes);
            Assert.IsTrue(environment.viewModel.CanClose());
            Assert.AreEqual(environment.shiftManagementInterface.CurrentShiftManagement.Entity, environment.shiftManagementUseCase.SaveShiftManagementDiffParameter.New);
            Assert.AreEqual(environment.shiftManagementInterface.ShiftManagementWithoutChanges.Entity, environment.shiftManagementUseCase.SaveShiftManagementDiffParameter.Old);
            Assert.AreEqual(environment.viewModel, environment.shiftManagementUseCase.SaveShiftManagementErrorHandlerParameter);
            Assert.AreEqual(environment.viewModel, environment.shiftManagementUseCase.SaveShiftManagementDiffShowerParameter);
        }

        [Test]
        public void CanCloseResetsShiftManagement()
        {
            var environment = CreateViewModelEnvironment();
            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(new ShiftManagement() { FirstDayOfWeek = DayOfWeek.Monday }, new NullLocalizationWrapper());
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(new ShiftManagement() { FirstDayOfWeek = DayOfWeek.Tuesday }, new NullLocalizationWrapper());
            environment.viewModel.MessageBoxRequest += (s, e) => e.ResultAction(MessageBoxResult.No);
            Assert.IsTrue(environment.viewModel.CanClose());
            Assert.IsTrue(environment.shiftManagementInterface.CurrentShiftManagement.Entity.EqualsByContent(environment.shiftManagementInterface.ShiftManagementWithoutChanges.Entity));
        }

        [Test]
        public void CanCloseReturnsFalseIfCancel()
        {
            var environment = CreateViewModelEnvironment();
            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(new ShiftManagement() { FirstDayOfWeek = DayOfWeek.Monday }, new NullLocalizationWrapper());
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(new ShiftManagement() { FirstDayOfWeek = DayOfWeek.Tuesday }, new NullLocalizationWrapper());
            environment.viewModel.MessageBoxRequest += (s, e) => e.ResultAction(MessageBoxResult.Cancel);
            Assert.IsFalse(environment.viewModel.CanClose());
        }

        [Test]
        public void LoadedCommandCallsTestLevelSetUseCase()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.LoadedCommand.Invoke(null);
            Assert.IsTrue(environment.testLevelSetUseCase.LoadTestLevelSetsCalled);
            Assert.IsTrue(environment.shiftManagementUseCase.LoadShiftManagementCalled);
            Assert.AreSame(environment.viewModel, environment.testLevelSetUseCase.LoadTestLevelSetsErrorHandler);
            Assert.AreSame(environment.viewModel, environment.shiftManagementUseCase.LoadShiftManagementErrorHandler);
        }

        [Test]
        public void RemoveTestLevelSetCanExecuteReturnsFalseIfSelectedTestLevelSetIsNull()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = null;
            Assert.IsFalse(environment.viewModel.RemoveTestLevelSetCommand.CanExecute(null));
        }

        [Test]
        public void RemoveTestLevelSetCanExecuteReturnsTrueIfSelectedTestLevelSetIsNotNull()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            Assert.IsTrue(environment.viewModel.RemoveTestLevelSetCommand.CanExecute(null));
        }

        [Test]
        public void RemoveTestLevelSetCanExecuteAsksUseCaseForReferences()
        {
            var environment = CreateViewModelEnvironment();
            var testLevelSet = new TestLevelSet();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(testLevelSet);
            environment.viewModel.RemoveTestLevelSetCommand.Execute(null);
            Assert.AreEqual(testLevelSet, environment.testLevelSetUseCase.DoesTestLevelSetHaveReferencesParameter);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void RemoveTestLevelSetExecuteShowsMessageBoxDependingOnReferences(bool references)
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            int messageBoxRequestCount = 0;
            environment.testLevelSetUseCase.DoesTestLevelSetHaveReferencesReturnValue = references;
            environment.viewModel.MessageBoxRequest += (s, e) => messageBoxRequestCount++;
            environment.viewModel.RemoveTestLevelSetCommand.Execute(null);
            Assert.AreEqual(1, messageBoxRequestCount);
        }

        [Test]
        public void RemoveTestLevelSetExecuteCallsViewForYesNoQuestion()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            bool messageBoxRequestInvoked = false;
            environment.testLevelSetUseCase.DoesTestLevelSetHaveReferencesReturnValue = false;
            environment.viewModel.MessageBoxRequest += (s, e) => messageBoxRequestInvoked = true;
            environment.viewModel.RemoveTestLevelSetCommand.Execute(null);
            Assert.IsTrue(messageBoxRequestInvoked);
        }

        [Test]
        public void RemoveTestLevelSetExecuteCallsUseCaseWithResultYes()
        {
            var environment = CreateViewModelEnvironment();
            var testLevelSet = new TestLevelSet();
            environment.testLevelSetUseCase.DoesTestLevelSetHaveReferencesReturnValue = false;
            environment.viewModel.MessageBoxRequest += (s, e) => e.ResultAction(MessageBoxResult.Yes);
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(testLevelSet);
            environment.viewModel.RemoveTestLevelSetCommand.Execute(null);
            Assert.AreEqual(testLevelSet, environment.testLevelSetUseCase.RemoveTestLevelSetParameter);
            Assert.AreSame(environment.viewModel, environment.testLevelSetUseCase.RemoveTestLevelSetErrorHandler);
        }

        [Test]
        public void RemoveTestLevelSetExecuteDoesntCallUseCaseWithResultNo()
        {
            var environment = CreateViewModelEnvironment();
            environment.testLevelSetUseCase.DoesTestLevelSetHaveReferencesReturnValue = false;
            environment.viewModel.MessageBoxRequest += (s, e) => e.ResultAction(MessageBoxResult.No);
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.RemoveTestLevelSetCommand.Execute(null);
            Assert.IsNull(environment.testLevelSetUseCase.RemoveTestLevelSetParameter);
        }

        [Test]
        public void SaveTestLevelSetCanExecuteReturnsFalseIfSelectedTestLevelSetHasNoChanges()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("gfisdhoü")
            });
            environment.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("gfisdhoü")
            });
            Assert.IsFalse(environment.viewModel.SaveTestLevelSetCommand.CanExecute(null));
        }

        [Test]
        public void SaveTestLevelSetCanExecuteReturnsTrueIfSelectedTestLevelSetHasChanges()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("gfisdhoü")
            });
            environment.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("305689hn305896g")
            });
            Assert.IsTrue(environment.viewModel.SaveTestLevelSetCommand.CanExecute(null));
        }

        [Test]
        public void SaveTestLevelSetExecuteCallsUseCase()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("gfisdhoü")
            });
            environment.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("305689hn305896g")
            });
            environment.testLevelSetUseCase.IsTestLevelSetNameUniqueReturnValue = true;

            environment.viewModel.SaveTestLevelSetCommand.Invoke(null);
            Assert.AreSame(environment.viewModel.SelectedTestLevelSet.Entity, environment.testLevelSetUseCase.UpdateTestLevelSetParameter.New);
            Assert.AreSame(environment.viewModel.TestLevelSetWithoutChanges.Entity, environment.testLevelSetUseCase.UpdateTestLevelSetParameter.Old);
            Assert.AreSame(environment.viewModel, environment.testLevelSetUseCase.UpdateTestLevelSetErrorHandler);
        }

        [TestCase("ubhgjifokpldü", true, true, false)]
        [TestCase("r3l9jhg", false, false, true)]
        public void SaveTestLevelSetExecuteChecksIfNameIsUnique(string name, bool nameUnique, bool shouldUseCaseBeCalled, bool messageBoxShown)
        {
            var environment = CreateViewModelEnvironment();
            bool messageBoxRequestInvoked = false;
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName(name)
            });
            environment.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("305689hn305896g")
            });
            environment.viewModel.MessageBoxRequest += (s, e) => messageBoxRequestInvoked = true;
            environment.testLevelSetUseCase.IsTestLevelSetNameUniqueReturnValue = nameUnique;

            environment.viewModel.SaveTestLevelSetCommand.Invoke(null);

            Assert.AreEqual(name, environment.testLevelSetUseCase.IsTestLevelSetNameUniqueParameter);
            Assert.AreEqual(messageBoxShown, messageBoxRequestInvoked);
            if (shouldUseCaseBeCalled)
            {
                Assert.AreEqual(environment.viewModel.SelectedTestLevelSet.Entity, environment.testLevelSetUseCase.UpdateTestLevelSetParameter.New);
                Assert.AreSame(environment.viewModel, environment.testLevelSetUseCase.UpdateTestLevelSetErrorHandler);
            }
            else
            {
                Assert.AreEqual(null, environment.testLevelSetUseCase.UpdateTestLevelSetParameter);
            }
        }

        [TestCase("fgbdhnk,")]
        [TestCase("rtz,")]
        public void SaveTestLevelSetExecuteChecksUniqueNameJustIfItChanged(string name)
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName(name)
            });
            environment.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName(name)
            });

            environment.viewModel.SaveTestLevelSetCommand.Invoke(null);
            Assert.IsNull(environment.testLevelSetUseCase.IsTestLevelSetNameUniqueParameter);
        }

        [Test]
        public void HasTestLevelSetChangedReturnsFalseIfSelectedTestLevelSetHasNoChanges()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("gfisdhoü")
            });
            environment.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("gfisdhoü")
            });
            Assert.IsFalse(environment.viewModel.HasTestLevelSetChanged());
        }

        [Test]
        public void HasTestLevelSetChangedReturnsTrueIfSelectedTestLevelSetHasChanges()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("gfisdhoü")
            });
            environment.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("305689hn305896g")
            });
            Assert.IsTrue(environment.viewModel.HasTestLevelSetChanged());
        }

        [Test]
        public void ResetTestLevelSetUpdatesSelectedTestLevelSet()
        {
            var environment = CreateViewModelEnvironment();
            environment.testLevelSetInterface.TestLevelSets = new ObservableCollection<TestLevelSetModel>();
            environment.viewModel.TestLevelSets.Add(new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(954) }));
            environment.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("305689hn305896g"),
                TestLevel1 = new TestLevel() { Id = new TestLevelId(0) },
                TestLevel2 = new TestLevel() { Id = new TestLevelId(0) },
                TestLevel3 = new TestLevel() { Id = new TestLevelId(0) }
            });
            environment.viewModel.ResetTestLevelSet();
            Assert.AreNotSame(environment.viewModel.SelectedTestLevelSet.Entity, environment.viewModel.TestLevelSetWithoutChanges.Entity);
            Assert.IsTrue(environment.viewModel.SelectedTestLevelSet.Entity.EqualsByContent(environment.viewModel.TestLevelSetWithoutChanges.Entity));
        }

        [Test]
        public void CanCloseCallsMessageBoxRequestIfSelectedTestLevelSetChanged()
        {
            var environment = CreateViewModelEnvironment();
            bool messageBoxRequestInvoked = false;
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("gfisdhoü")
            });
            environment.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("305689hn305896g")
            });
            environment.viewModel.MessageBoxRequest += (s, e) => messageBoxRequestInvoked = true;
            environment.viewModel.CanClose();
            Assert.IsTrue(messageBoxRequestInvoked);
        }

        [Test]
        public void CanCloseDoesNotCallsMessageBoxRequestIfSelectedTestLevelSetHasNotChanged()
        {
            var environment = CreateViewModelEnvironment();
            bool messageBoxRequestInvoked = false;
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("gfisdhoü")
            });
            environment.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("gfisdhoü")
            });
            environment.viewModel.MessageBoxRequest += (s, e) => messageBoxRequestInvoked = true;
            environment.viewModel.CanClose();
            Assert.IsFalse(messageBoxRequestInvoked);
        }

        [Test]
        public void CanCloseSavesSelectedTestLevelSetAndReturnsTrueIfResultIsYes()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("gfisdhoü")
            });
            environment.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("305689hn305896g")
            });
            environment.testLevelSetUseCase.IsTestLevelSetNameUniqueReturnValue = true;

            environment.viewModel.MessageBoxRequest += (s, e) => e.ResultAction(MessageBoxResult.Yes);
            Assert.IsTrue(environment.viewModel.CanClose());
            Assert.AreEqual(environment.viewModel.SelectedTestLevelSet.Entity, environment.testLevelSetUseCase.UpdateTestLevelSetParameter.New);
        }

        [Test]
        public void CanCloseResetsSelectedTestLevelSetAndReturnsTrueIfResultIsNo()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("gfisdhoü")
            });
            environment.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("305689hn305896g"),
                TestLevel1 = new TestLevel() { Id = new TestLevelId(0)},
                TestLevel2 = new TestLevel() { Id = new TestLevelId(0) },
                TestLevel3 = new TestLevel() { Id = new TestLevelId(0) }
            });
            environment.testLevelSetInterface.TestLevelSets = new ObservableCollection<TestLevelSetModel>();
            environment.viewModel.TestLevelSets.Add(environment.viewModel.SelectedTestLevelSet);
            environment.viewModel.MessageBoxRequest += (s, e) => e.ResultAction(MessageBoxResult.No);
            Assert.IsTrue(environment.viewModel.CanClose());
            Assert.IsTrue(environment.viewModel.SelectedTestLevelSet.Entity.EqualsByContent(environment.viewModel.TestLevelSetWithoutChanges.Entity));
        }

        [Test]
        public void CanCloseReturnsFalseIfResultIsCancel()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("gfisdhoü")
            });
            environment.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(954),
                Name = new TestLevelSetName("305689hn305896g")
            });
            environment.viewModel.MessageBoxRequest += (s, e) => e.ResultAction(MessageBoxResult.Cancel);
            Assert.IsFalse(environment.viewModel.CanClose());
        }

        [TestCase("2020-03-06 00:00:00")]
        [TestCase("1958-11-22 00:00:00")]
        public void SelectedDateInCalendarInvokesWorkingCalendarEntrySelectionRequestNotIfNoEntryExistsForDate(DateTime date)
        {
            var environment = CreateViewModelEnvironment();
            bool selectionRequestInvoked = false;
            environment.viewModel.WorkingCalendarEntrySelectionRequest += (s, e) => selectionRequestInvoked = true;
            environment.viewModel.SelectedDateInCalendar = date;
            Assert.IsFalse(selectionRequestInvoked);
        }

        [Test]
        public void ShiftManagementErrorInvokesMessageRequest()
        {
            var environment = CreateViewModelEnvironment();
            bool requestInvoked = false;
            environment.viewModel.MessageBoxRequest += (s, e) => requestInvoked = true;
            environment.viewModel.ShiftManagementError();
            Assert.IsTrue(requestInvoked);
        }

        [Test]
        public void WorkingCalendarErrorInvokesMessageRequest()
        {
            var environment = CreateViewModelEnvironment();
            bool requestInvoked = false;
            environment.viewModel.MessageBoxRequest += (s, e) => requestInvoked = true;
            environment.viewModel.WorkingCalendarError(null);
            Assert.IsTrue(requestInvoked);
        }

        [Test]
        public void TestLevelSetErrorInvokesMessageRequest()
        {
            var environment = CreateViewModelEnvironment();
            bool requestInvoked = false;
            environment.viewModel.MessageBoxRequest += (s, e) => requestInvoked = true;
            environment.viewModel.ShowTestLevelSetError();
            Assert.IsTrue(requestInvoked);
        }
        
        [Test]
        public void ShowDiffDialogDoesntShowChangesIfShiftManagementHasNoChanges()
        {
            var environment = CreateViewModelEnvironment();
            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) => showDialogRequestInvoked = true;

            var shiftManagement = CreateShiftManagement.Randomized(12324);
            var diff = new ShiftManagementDiff(shiftManagement, shiftManagement, null, null);
            environment.viewModel.ShowDiffDialog(diff, () => { });

            Assert.IsFalse(showDialogRequestInvoked);
        }

        [Test]
        public void ShowDiffDialogForShiftManagementWithNoResultTest()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(CreateShiftManagement.Randomized(12324), null);
            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(CreateShiftManagement.Randomized(684543), null);

            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };
            var oldShiftManagement = CreateShiftManagement.Randomized(12324);
            var newShiftManagement = CreateShiftManagement.Randomized(6578);

            var diff = new ShiftManagementDiff(oldShiftManagement, newShiftManagement, null, null);

            environment.viewModel.ShowDiffDialog(diff, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(environment.shiftManagementInterface.CurrentShiftManagement.EqualsByContent(environment.shiftManagementInterface.ShiftManagementWithoutChanges.Entity));
        }

        [Test]
        public void ShowDiffDialogForShiftManagementWithCancelResultTest()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            environment.shiftManagementInterface.ShiftManagementWithoutChanges = new ShiftManagementModel(CreateShiftManagement.Randomized(12324), null);
            environment.shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(CreateShiftManagement.Randomized(684543), null);

            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Cancel;
            };
            var oldShiftManagement = CreateShiftManagement.Randomized(12324);
            var newShiftManagement = CreateShiftManagement.Randomized(6578);

            var diff = new ShiftManagementDiff(oldShiftManagement, newShiftManagement, null, null);

            environment.viewModel.ShowDiffDialog(diff, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsFalse(environment.shiftManagementInterface.CurrentShiftManagement.EqualsByContent(environment.shiftManagementInterface.ShiftManagementWithoutChanges.Entity));
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

            var oldShiftManagement = CreateShiftManagement.Randomized(12324);
            var newShiftManagement = CreateShiftManagement.Randomized(6578);

            var diff = new ShiftManagementDiff(oldShiftManagement, newShiftManagement, null, null);
            var finishedActionCalled = false;
            environment.viewModel.ShowDiffDialog(diff, () => { finishedActionCalled = true; });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(finishedActionCalled);
        }

        [Test]
        public void ShowDiffDialogShowsShiftManagementDiffs()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var oldShiftManagement = new ShiftManagement()
            {
                FirstShiftStart = TimeSpan.FromTicks(1),
                FirstShiftEnd = TimeSpan.FromTicks(2),
                SecondShiftStart = TimeSpan.FromTicks(3),
                SecondShiftEnd = TimeSpan.FromTicks(4),
                ThirdShiftStart = TimeSpan.FromTicks(5),
                ThirdShiftEnd = TimeSpan.FromTicks(6),
                IsSecondShiftActive = true,
                IsThirdShiftActive = false,
                ChangeOfDay = TimeSpan.FromTicks(7),
                FirstDayOfWeek = DayOfWeek.Tuesday
            };
            var newShiftManagement = new ShiftManagement()
            {
                FirstShiftStart = TimeSpan.FromTicks(8),
                FirstShiftEnd = TimeSpan.FromTicks(9),
                SecondShiftStart = TimeSpan.FromTicks(10),
                SecondShiftEnd = TimeSpan.FromTicks(11),
                ThirdShiftStart = TimeSpan.FromTicks(12),
                ThirdShiftEnd = TimeSpan.FromTicks(13),
                IsSecondShiftActive = false,
                IsThirdShiftActive = true,
                ChangeOfDay = TimeSpan.FromTicks(14),
                FirstDayOfWeek = DayOfWeek.Friday
            };


            var diff = new ShiftManagementDiff(oldShiftManagement, newShiftManagement, null, null);
            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) =>
            {
                var changes = e.ChangedValues;
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;

                Assert.AreEqual(changes.Count, 10);
                Assert.AreEqual(changes[0].OldValue, diff.Old.FirstShiftStart.ToString());
                Assert.AreEqual(changes[0].NewValue, diff.New.FirstShiftStart.ToString());
                Assert.AreEqual(changes[1].OldValue, diff.Old.FirstShiftEnd.ToString());
                Assert.AreEqual(changes[1].NewValue, diff.New.FirstShiftEnd.ToString());
                Assert.AreEqual(changes[2].OldValue, diff.Old.SecondShiftStart.ToString());
                Assert.AreEqual(changes[2].NewValue, diff.New.SecondShiftStart.ToString());
                Assert.AreEqual(changes[3].OldValue, diff.Old.SecondShiftEnd.ToString());
                Assert.AreEqual(changes[3].NewValue, diff.New.SecondShiftEnd.ToString());
                Assert.AreEqual(changes[4].OldValue, diff.Old.ThirdShiftStart.ToString());
                Assert.AreEqual(changes[4].NewValue, diff.New.ThirdShiftStart.ToString());
                Assert.AreEqual(changes[5].OldValue, diff.Old.ThirdShiftEnd.ToString());
                Assert.AreEqual(changes[5].NewValue, diff.New.ThirdShiftEnd.ToString());
                Assert.AreEqual(changes[6].OldValue, diff.Old.IsSecondShiftActive ? "✓" : "✗");
                Assert.AreEqual(changes[6].NewValue, diff.New.IsSecondShiftActive ? "✓" : "✗");
                Assert.AreEqual(changes[7].OldValue, diff.Old.IsThirdShiftActive ? "✓" : "✗");
                Assert.AreEqual(changes[7].NewValue, diff.New.IsThirdShiftActive ? "✓" : "✗");
                Assert.AreEqual(changes[8].OldValue, diff.Old.ChangeOfDay.ToString());
                Assert.AreEqual(changes[8].NewValue, diff.New.ChangeOfDay.ToString());
            };

            environment.viewModel.ShowDiffDialog(diff, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
        }

        [TestCase("2021-04-24", WorkingCalendarEntryType.Holiday, true, false, false)]
        [TestCase("2021-04-24", WorkingCalendarEntryType.Holiday, true, true, false)]
        [TestCase("2021-04-25", WorkingCalendarEntryType.Holiday, false, true, false)]
        [TestCase("2021-04-25", WorkingCalendarEntryType.Holiday, true, true, false)]
        [TestCase("2021-04-23", WorkingCalendarEntryType.ExtraShift, false, false, false)]
        [TestCase("2021-04-23", WorkingCalendarEntryType.ExtraShift, true, false, false)]
        [TestCase("2021-04-23", WorkingCalendarEntryType.ExtraShift, false, true, false)]
        [TestCase("2021-04-23", WorkingCalendarEntryType.ExtraShift, true, true, false)]
        [TestCase("2021-04-24", WorkingCalendarEntryType.ExtraShift, false, false, false)]
        [TestCase("2021-04-24", WorkingCalendarEntryType.ExtraShift, false, true, false)]
        [TestCase("2021-04-25", WorkingCalendarEntryType.ExtraShift, false, false, false)]
        [TestCase("2021-04-25", WorkingCalendarEntryType.ExtraShift, true, false, false)]
        [TestCase("2021-04-23", WorkingCalendarEntryType.Holiday, true, true, true)]
        [TestCase("2021-04-24", WorkingCalendarEntryType.ExtraShift, true, false, true)]
        [TestCase("2021-04-25", WorkingCalendarEntryType.ExtraShift, false, true, true)]
        public void SingleWorkingCalendarEntriesFilterTest(DateTime date, WorkingCalendarEntryType type, bool areSaturdaysFre, bool areSundaysFree, bool expectedResult)
        {
            var tuple = CreateViewModelEnvironment();
            tuple.viewModel.AreSaturdaysFree = areSaturdaysFre;
            tuple.viewModel.AreSundaysFree = areSundaysFree;
            var model = new WorkingCalendarEntryModel(new WorkingCalendarEntry()
            {
                Date = date,
                Type = type,
                Repetition = WorkingCalendarEntryRepetition.Once
            });
            Assert.AreEqual(expectedResult, tuple.viewModel.SingleWorkingCalendarEntries.Filter(model));

            model.Repetition = WorkingCalendarEntryRepetition.Yearly;
            Assert.IsFalse(tuple.viewModel.SingleWorkingCalendarEntries.Filter(model));
        }

        [TestCase(WorkingCalendarEntryRepetition.Yearly, true)]
        [TestCase(WorkingCalendarEntryRepetition.Once, false)]
        public void YearlyWorkingCalendarEntriesFilterTest(WorkingCalendarEntryRepetition repetition, bool expectedResult)
        {
            var tuple = CreateViewModelEnvironment();
            var model = new WorkingCalendarEntryModel(new WorkingCalendarEntry()
            {
                Repetition = repetition
            });
            Assert.AreEqual(expectedResult, tuple.viewModel.YearlyWorkingCalendarEntries.Filter(model));
        }

        [TestCase(1, "asldfh")]
        [TestCase(2, "34653456")]
        public void ResetTestLevelSetUpdatesEntityInTestLevelSets(long id, string name)
        {
            var tuple = CreateViewModelEnvironment();
            tuple.testLevelSetInterface.TestLevelSets = new ObservableCollection<TestLevelSetModel>();
            tuple.viewModel.TestLevelSets.Add(new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(id)}));
            tuple.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(id),
                Name = new TestLevelSetName(name),
                TestLevel1 = new TestLevel(),
                TestLevel2 = new TestLevel(),
                TestLevel3 = new TestLevel()

            });
            tuple.viewModel.ResetTestLevelSet();
            Assert.AreEqual(name, tuple.viewModel.TestLevelSets[0].Name);
        }

        [Test]
        public void RefilterSingleAndYearlyWorkingCalendarEntriesOnWorkingCalendarAdd()
        {
            var environment = CreateViewModelEnvironment();
            bool singleEntriesRefiltered = false;
            bool yearlyEntriesRefiltered = false;
            environment.viewModel.SingleWorkingCalendarEntries.Refiltered += (s, e) => singleEntriesRefiltered = true;
            environment.viewModel.YearlyWorkingCalendarEntries.Refiltered += (s, e) => yearlyEntriesRefiltered = true;
            environment.workingCalendarInterface.RaisePropertyChanged(nameof(IWorkingCalendarInterface.WorkingCalendarEntries));
            environment.workingCalendarInterface.WorkingCalendarEntries.Add(new WorkingCalendarEntryModel(new WorkingCalendarEntry()));
            Assert.IsTrue(singleEntriesRefiltered);
            Assert.IsTrue(yearlyEntriesRefiltered);
        }

        [Test]
        public void LoadedSetsSelectedTestLevelSetToNull()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet());
            environment.viewModel.LoadedCommand.Execute(null);
            Assert.IsNull(environment.viewModel.SelectedTestLevelSet);
        }

        [Test]
        public void DisposeDisposesFilteredObservableCollections()
        {
            var singleEntries = new FilteredObservableCollectionMock<WorkingCalendarEntryModel>();
            var yearlyEntries = new FilteredObservableCollectionMock<WorkingCalendarEntryModel>();
            var environment = CreateViewModelEnvironment(singleEntries, yearlyEntries);
            environment.viewModel.Dispose();
            Assert.IsTrue(singleEntries.IsDisposed);
            Assert.IsTrue(yearlyEntries.IsDisposed);
        }

        [Test]
        public void ShowDiffDialogDoesntShowChangesIfTestLevelSetHasNoChanges()
        {
            var environment = CreateViewModelEnvironment();
            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) => showDialogRequestInvoked = true;

            var testLevelSet = CreateTestLevelSet.Randomized(12324);
            var diff = new TestLevelSetDiff() { Old = testLevelSet, New = testLevelSet };
            environment.viewModel.ShowDiffDialog(diff, () => { });

            Assert.IsFalse(showDialogRequestInvoked);
        }

        [Test]
        public void ShowDiffDialogForTestLevelSetWithNoResultTest()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            environment.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(CreateTestLevelSet.Randomized(12324));
            environment.testLevelSetInterface.SelectedTestLevelSet = new TestLevelSetModel(CreateTestLevelSet.Randomized(684543));
            environment.testLevelSetInterface.TestLevelSets = new ObservableCollection<TestLevelSetModel>(new List<TestLevelSetModel>() { environment.testLevelSetInterface.SelectedTestLevelSet });

            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };
            var oldTestLevelSet = CreateTestLevelSet.Randomized(12324);
            var newTestLevelSet = CreateTestLevelSet.Randomized(684543);
            var diff = new TestLevelSetDiff() { Old = oldTestLevelSet, New = newTestLevelSet };

            environment.viewModel.ShowDiffDialog(diff, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(environment.testLevelSetInterface.SelectedTestLevelSet.Entity.EqualsByContent(environment.testLevelSetInterface.TestLevelSetWithoutChanges.Entity));
        }

        [Test]
        public void ShowDiffDialogForTestLevelSetWithCancelResultTest()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            environment.testLevelSetInterface.TestLevelSetWithoutChanges = new TestLevelSetModel(CreateTestLevelSet.Randomized(12324));
            environment.testLevelSetInterface.SelectedTestLevelSet = new TestLevelSetModel(CreateTestLevelSet.Randomized(684543));

            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Cancel;
            };
            var oldTestLevelSet = CreateTestLevelSet.Randomized(12324);
            var newTestLevelSet = CreateTestLevelSet.Randomized(6578);
            var diff = new TestLevelSetDiff() { Old = oldTestLevelSet, New = newTestLevelSet };

            environment.viewModel.ShowDiffDialog(diff, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsFalse(environment.testLevelSetInterface.SelectedTestLevelSet.Entity.EqualsByContent(environment.testLevelSetInterface.TestLevelSetWithoutChanges.Entity));
        }

        [Test]
        public void ShowDiffDialogForTestLevelSetWithYesResultCallsFinishedAction()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);

            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;
            };

            var oldTestLevelSet = CreateTestLevelSet.Randomized(12324);
            var newTestLevelSet = CreateTestLevelSet.Randomized(6578);
            var diff = new TestLevelSetDiff() { Old = oldTestLevelSet, New = newTestLevelSet };
            var finishedActionCalled = false;

            environment.viewModel.ShowDiffDialog(diff, () => { finishedActionCalled = true; });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(finishedActionCalled);
        }

        [Test]
        public void ShowDiffDialogShowsTestLevelSetDiffs()
        {
            var environment = CreateViewModelEnvironment();
            environment.viewModel.SetGuiDispatcher(Dispatcher.CurrentDispatcher);

            var oldTestLevelSet = new TestLevelSet()
            {
                Name = new TestLevelSetName("gzsdga bnh"),
                TestLevel1 = new TestLevel()
                {
                    ConsiderWorkingCalendar = true,
                    SampleNumber = 3456,
                    TestInterval = new Core.Entities.Interval()
                    {
                        IntervalValue = 357,
                        Type = IntervalType.XTimesADay
                    }
                },
                TestLevel2 = new TestLevel()
                {
                    ConsiderWorkingCalendar = false,
                    IsActive = false,
                    SampleNumber = 345,
                    TestInterval = new Core.Entities.Interval()
                    {
                        IntervalValue = 65,
                        Type = IntervalType.XTimesAShift
                    }
                },
                TestLevel3 = new TestLevel()
                {
                    ConsiderWorkingCalendar = true,
                    IsActive = false,
                    SampleNumber = 3456,
                    TestInterval = new Core.Entities.Interval()
                    {
                        IntervalValue = 357,
                        Type = IntervalType.XTimesAWeek
                    }
                }
            };
            var newTestLevelSet = new TestLevelSet()
            {
                Name = new TestLevelSetName("glkdbhnjkl"),
                TestLevel1 = new TestLevel()
                {
                    ConsiderWorkingCalendar = false,
                    SampleNumber = 74120,
                    TestInterval = new Core.Entities.Interval()
                    {
                        IntervalValue = 290,
                        Type = IntervalType.EveryXShifts
                    }
                },
                TestLevel2 = new TestLevel()
                {
                    ConsiderWorkingCalendar = true,
                    IsActive = true,
                    SampleNumber = 987,
                    TestInterval = new Core.Entities.Interval()
                    {
                        IntervalValue = 37,
                        Type = IntervalType.XTimesAWeek
                    }
                },
                TestLevel3 = new TestLevel()
                {
                    ConsiderWorkingCalendar = false,
                    IsActive = true,
                    SampleNumber = 654,
                    TestInterval = new Core.Entities.Interval()
                    {
                        IntervalValue = 59,
                        Type = IntervalType.EveryXDays
                    }
                }
            };


            var diff = new TestLevelSetDiff() { Old = oldTestLevelSet, New = newTestLevelSet };
            var showDialogRequestInvoked = false;
            environment.viewModel.RequestVerifyChangesView += (s, e) =>
            {
                var changes = e.ChangedValues;
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;

                Assert.AreEqual(15, changes.Count);
                Assert.AreEqual(changes[0].OldValue, diff.Old.Name.ToDefaultString());
                Assert.AreEqual(changes[0].NewValue, diff.New.Name.ToDefaultString());
                Assert.AreEqual(changes[1].OldValue, diff.Old.TestLevel1.TestInterval.IntervalValue.ToString());
                Assert.AreEqual(changes[1].NewValue, diff.New.TestLevel1.TestInterval.IntervalValue.ToString());
                Assert.AreEqual(changes[3].OldValue, diff.Old.TestLevel1.SampleNumber.ToString());
                Assert.AreEqual(changes[3].NewValue, diff.New.TestLevel1.SampleNumber.ToString());
                Assert.AreEqual(changes[4].OldValue, diff.Old.TestLevel1.ConsiderWorkingCalendar ? "✓" : "✗");
                Assert.AreEqual(changes[4].NewValue, diff.New.TestLevel1.ConsiderWorkingCalendar ? "✓" : "✗");
                Assert.AreEqual(changes[5].OldValue, diff.Old.TestLevel2.TestInterval.IntervalValue.ToString());
                Assert.AreEqual(changes[5].NewValue, diff.New.TestLevel2.TestInterval.IntervalValue.ToString());
                Assert.AreEqual(changes[7].OldValue, diff.Old.TestLevel2.SampleNumber.ToString());
                Assert.AreEqual(changes[7].NewValue, diff.New.TestLevel2.SampleNumber.ToString());
                Assert.AreEqual(changes[8].OldValue, diff.Old.TestLevel2.ConsiderWorkingCalendar ? "✓" : "✗");
                Assert.AreEqual(changes[8].NewValue, diff.New.TestLevel2.ConsiderWorkingCalendar ? "✓" : "✗");
                Assert.AreEqual(changes[9].OldValue, diff.Old.TestLevel2.IsActive ? "✓" : "✗");
                Assert.AreEqual(changes[9].NewValue, diff.New.TestLevel2.IsActive ? "✓" : "✗");
                Assert.AreEqual(changes[10].OldValue, diff.Old.TestLevel3.TestInterval.IntervalValue.ToString());
                Assert.AreEqual(changes[10].NewValue, diff.New.TestLevel3.TestInterval.IntervalValue.ToString());
                Assert.AreEqual(changes[12].OldValue, diff.Old.TestLevel3.SampleNumber.ToString());
                Assert.AreEqual(changes[12].NewValue, diff.New.TestLevel3.SampleNumber.ToString());
                Assert.AreEqual(changes[13].OldValue, diff.Old.TestLevel3.ConsiderWorkingCalendar ? "✓" : "✗");
                Assert.AreEqual(changes[13].NewValue, diff.New.TestLevel3.ConsiderWorkingCalendar ? "✓" : "✗");
                Assert.AreEqual(changes[14].OldValue, diff.Old.TestLevel3.IsActive ? "✓" : "✗");
                Assert.AreEqual(changes[14].NewValue, diff.New.TestLevel3.IsActive ? "✓" : "✗");
            };

            environment.viewModel.ShowDiffDialog(diff, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
        }


        static Environment CreateViewModelEnvironment(IFilteredObservableCollectionExtension<WorkingCalendarEntryModel> singleEntries = null, IFilteredObservableCollectionExtension<WorkingCalendarEntryModel> yearlyEntries = null)
        {
            var environment = new Environment();
            environment.testLevelSetUseCase = new TestLevelSetUseCaseMock();
            environment.testLevelSetInterface = new TestLevelSetInterfaceMock();
            environment.workingCalendarUseCase = new WorkingCalendarUseCaseMock();
            environment.workingCalendarInterface = new WorkingCalendarInterfaceMock();
            environment.shiftManagementUseCase = new ShiftManagementUseCaseMock();
            environment.shiftManagementInterface = new ShiftManagementInterfaceMock();
            environment.viewModel = new TestLevelSetViewModel(environment.testLevelSetUseCase,
                                                              environment.testLevelSetInterface,
                                                              environment.workingCalendarUseCase,
                                                              environment.workingCalendarInterface,
                                                              environment.shiftManagementUseCase,
                                                              environment.shiftManagementInterface,
                                                              new NullLocalizationWrapper(),
                                                              new StartUpMock(),
                                                              singleEntries,
                                                              yearlyEntries);

            return environment;
        }
        
        struct Environment
        {
            public TestLevelSetViewModel viewModel;
            public TestLevelSetUseCaseMock testLevelSetUseCase;
            public TestLevelSetInterfaceMock testLevelSetInterface;
            public WorkingCalendarUseCaseMock workingCalendarUseCase;
            public WorkingCalendarInterfaceMock workingCalendarInterface;
            public ShiftManagementUseCaseMock shiftManagementUseCase;
            public ShiftManagementInterfaceMock shiftManagementInterface;
        }
    }
}