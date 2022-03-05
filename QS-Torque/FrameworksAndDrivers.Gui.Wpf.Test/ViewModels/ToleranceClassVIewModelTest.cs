using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Core;
using Core.Entities.ReferenceLink;
using InterfaceAdapters.Models;
using TestHelper.Factories;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    [TestFixture]
    public class ToleranceClassViewModelTest
    {
        private class ToleranceClassUseCaseMock : ToleranceClassUseCase
        {
            public int LoadToleranceClassesCallCount = 0;
            public int AddToleranceClassCallCount = 0;
            public int SaveToleranceClassCallCount = 0;
            public int RemoveToleranceClassCallCount = 0;
            public ToleranceClass SaveToleranceClassNewToleranceClass;
            public ToleranceClass SaveToleranceClassOldToleranceClass;
            public ToleranceClassId LoadReferencedLocationToolAssignmentsParameter;
            public ToleranceClass RemoveToleranceClassParameter;

            public ToleranceClassId LoadReferencedLocationsParameter;
            public bool WasLoadReferencedLocationsCalled = false;

            public ToleranceClassUseCaseMock(IToleranceClassGui guiInterface, IToleranceClassData dataInterface, ISessionInformationUserGetter userGetter, INotificationManager notificationManager) : base(guiInterface, dataInterface, userGetter, null, notificationManager)
            {
            }

            public override void LoadToleranceClasses()
            {
                LoadToleranceClassesCallCount++;
            }

            public override void AddToleranceClass(ToleranceClass toleranceClass)
            {
                AddToleranceClassCallCount++;
            }

            public override void SaveToleranceClass(ToleranceClass newToleranceClass, ToleranceClass oldToleranceClass)
            {
                SaveToleranceClassCallCount++;
                SaveToleranceClassNewToleranceClass = newToleranceClass;
                SaveToleranceClassOldToleranceClass = oldToleranceClass;
            }

            public override void RemoveToleranceClass(ToleranceClass toleranceClass, IToleranceClassGui active)
            {
                RemoveToleranceClassCallCount++;
                RemoveToleranceClassParameter = toleranceClass;
            }

            public override void LoadReferencedLocations(ToleranceClassId id)
            {
                LoadReferencedLocationsParameter = id;
                WasLoadReferencedLocationsCalled = true;
            }

            public override void LoadReferencedLocationToolAssignments(ToleranceClassId id)
            {
                LoadReferencedLocationToolAssignmentsParameter = id;
            }
        }

        private ToleranceClassUseCaseMock _toleranceClassUseCaseMock;
        private ToleranceClassViewModel _viewModel;
        [SetUp]
        public void SetUp()
        {
            _toleranceClassUseCaseMock = new ToleranceClassUseCaseMock(null, null, null, null);
            _viewModel = new ToleranceClassViewModel(new StartUpMock(), _toleranceClassUseCaseMock,null, new NullLocalizationWrapper());
			_viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
        }

        [Test]
        public void ShowToleranceClassesTest()
        {
            var toleranceClasses = new List<ToleranceClass>
            {
                CreateParameterizedToleranceClass(0,"UpperLower",5,10),
                CreateParameterizedToleranceClass(1,"LowerUpper",1,2)
            };
            _viewModel.ShowToleranceClasses(toleranceClasses);

           Assert.AreEqual(2, _viewModel.ToleranceClasses.Count);
           Assert.AreEqual("UpperLower", (_viewModel.ToleranceClasses.GetItemAt(0) as ToleranceClassModel).Name);
           Assert.AreEqual("LowerUpper",(_viewModel.ToleranceClasses.GetItemAt(1) as ToleranceClassModel).Name);
        }

        [Test]
        public void ShowToleranceClassesErrorTest()
        {
            _viewModel.MessageBoxRequest += (sender, args) => { Assert.Pass(); };
            _viewModel.ShowToleranceClassesError();
            Assert.Fail("No MessageBox was shown");
        }

        [Test]
        public void RemoveToleranceClassCommandTest()
        {
            _viewModel.SelectedToleranceClassModel = CreateParameterizedToleranceClassModel(5, "Test");
            _viewModel.MessageBoxRequest += (sender, args) => { args.ResultAction.Invoke(MessageBoxResult.Yes); };
            _viewModel.RemoveToleranceClassCommand.Invoke(null);
            Assert.AreEqual(1, _toleranceClassUseCaseMock.RemoveToleranceClassCallCount);
        }

        [Test]
        public void InvokeRemoveToleranceClassExecuteCallsToleranceClassWithResetedParameters()
        {
            var toleranceClassUseCaseMock = new ToleranceClassUseCaseMock(null, null, null, null);
            var viewModel = new ToleranceClassViewModel(new StartUpMock(), toleranceClassUseCaseMock, null, new NullLocalizationWrapper());
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            var toleranceClass = CreateToleranceClass.Anonymous();
            var toleranceClassModel = ToleranceClassModel.GetModelFor(toleranceClass);

            viewModel.SelectedToleranceClassModel = toleranceClassModel.CopyDeep();
            viewModel.SelectedToleranceClassModel.Name = "345345";
            viewModel.SelectedToleranceClassModel.Relative = true;
            viewModel.SelectedToleranceClassModel.UpperLimit = 92;
            viewModel.SelectedToleranceClassModel.UpperLimit = 976775.6;

            viewModel.MessageBoxRequest += (sender, args) => args.ResultAction?.Invoke(MessageBoxResult.Yes);
            viewModel.RemoveToleranceClassCommand.Invoke(null);

            Assert.IsTrue(toleranceClassUseCaseMock.RemoveToleranceClassParameter.EqualsByContent(toleranceClassModel.Entity));
        }

        [Test]
        public void RemoveToleranceClassTest()
        {
            var toleranceClassRemoved = CreateParameterizedToleranceClass(0, "UpperLower", 5, 10);
            var remainingToleranceClass = CreateParameterizedToleranceClass(1, "LowerUpper", 1, 2);

            _viewModel.ToleranceClasses.AddNewItem(new ToleranceClassModel(toleranceClassRemoved));
            _viewModel.ToleranceClasses.AddNewItem(new ToleranceClassModel(remainingToleranceClass));
            _viewModel.RemoveToleranceClass(toleranceClassRemoved);
            var remainingToleranceClassModel = ((ObservableCollection<ToleranceClassModel>) _viewModel.ToleranceClasses.SourceCollection).FirstOrDefault();
            if (remainingToleranceClassModel != null)
            {
                if (AreEqual(remainingToleranceClass, remainingToleranceClassModel))
                {
                    Assert.Pass();
                }
            }
            Assert.Fail();
        }
        
        [Test]
        public void RemoveToleranceClassErrorTest()
        {
            _viewModel.MessageBoxRequest += (sender, args) => { Assert.Pass(); };
            _viewModel.RemoveToleranceClassError();
            Assert.Fail("No Message was shown");
        }

        [Test]
        public void AddToleranceCommandTest()
        {
            _viewModel.AddToleranceClassCommand.Invoke(null);
            Assert.AreEqual(1, _toleranceClassUseCaseMock.AddToleranceClassCallCount);
        }

        [Test]
        public void AddToleranceClassTest()
        {
            var toleranceClassToAdd = CreateParameterizedToleranceClass(5, "TestForAdd", 3, 8, false);
            _viewModel.AddToleranceClass(toleranceClassToAdd);
            Assert.IsTrue(AreEqual(toleranceClassToAdd, _viewModel.ToleranceClasses.GetItemAt(0) as ToleranceClassModel));
        }

        [Test]
        public void AddToleranceClassErrorTest()
        {
            _viewModel.MessageBoxRequest += (sender, args) => { Assert.Pass(); };
            _viewModel.AddToleranceClassError();
            Assert.Fail("No Message was shown");
        }


        [Test]
        public void SaveToleranceClassCommandTest()
        {
            var toleranceClassToSave = CreateParameterizedToleranceClassModel(5, "TestForSave", 5, 8, false);

            _viewModel.SelectedToleranceClassModel = toleranceClassToSave;
            _viewModel.SelectedToleranceClassModel.LowerLimit = 15;
            _viewModel.SaveToleranceClassCommand.Invoke(null);
            Assert.AreEqual(1, _toleranceClassUseCaseMock.SaveToleranceClassCallCount);
        }

        [TestCase(4)]
        [TestCase(6)]
        public void SaveToleranceCalllsUseCaseWithCorrectOldAndNewToleranceClass(double lowerLimit)
        {
            var originalToleranceClassToSave = CreateParameterizedToleranceClassModel(5,"Test", 5,6,true);
            _viewModel.SelectedToleranceClassModel = originalToleranceClassToSave;
            _viewModel.SelectedToleranceClassModel.LowerLimit = lowerLimit;
            _viewModel.SaveToleranceClassCommand.Invoke(null);
            Assert.AreEqual(lowerLimit, _toleranceClassUseCaseMock.SaveToleranceClassNewToleranceClass.LowerLimit);
            Assert.AreEqual(5, _toleranceClassUseCaseMock.SaveToleranceClassOldToleranceClass.LowerLimit);
        }
            
        [Test]
        public void UpdateToleranceClassTest()
        {
            var toleranceClassToUpdate = CreateParameterizedToleranceClass(5, "TestForUpdate", 5, 10, true);
            var updatedToleranceClass = CreateParameterizedToleranceClass(toleranceClassToUpdate.Id.ToLong(), "TestAfterUpdate",
                toleranceClassToUpdate.LowerLimit, toleranceClassToUpdate.UpperLimit, toleranceClassToUpdate.Relative);
            _viewModel.ToleranceClasses.AddNewItem(new ToleranceClassModel(toleranceClassToUpdate));
            _viewModel.ToleranceClasses.AddNewItem(CreateParameterizedToleranceClassModel(8, "Test", 5, 8, false));
            _viewModel.UpdateToleranceClass(updatedToleranceClass);
            Assert.AreEqual("TestAfterUpdate", (_viewModel.ToleranceClasses.GetItemAt(0) as ToleranceClassModel).Name);
        }

        [Test]
        public void SaveToleranceClassErrorTest()
        {
            _viewModel.MessageBoxRequest += (sender, args) => { Assert.Pass(); };
            _viewModel.SaveToleranceClassError();
            Assert.Fail("No Message was shown");
        }


        [Test]
        public void LoadToleranceClassesTest()
        {
            _viewModel.LoadToleranceClasses();
            Assert.AreEqual(1, _toleranceClassUseCaseMock.LoadToleranceClassesCallCount);
        }

        [Test]
        public void SelectionChangedCommandTest()
        {
            var addedItems = new List<ToleranceClassModel>
            {
                CreateParameterizedToleranceClassModel(5,"Test1"),
                CreateParameterizedToleranceClassModel(8,"Test2")
            };
            SelectionChangedEventArgs args = new SelectionChangedEventArgs(EventManager.RegisterRoutedEvent("Test",
                RoutingStrategy.Direct, typeof(ToleranceClassViewModel),
                typeof(ToleranceClassViewModel)), new List<ToleranceClassModel>(), addedItems);
            _viewModel.SelectionChanged.Invoke(args);
            Assert.IsTrue(_viewModel.IsListViewVisible);
            Assert.AreEqual(_viewModel.SelectedToleranceClassListCollectionView.GetItemAt(0), addedItems[0]);
            Assert.AreEqual(_viewModel.SelectedToleranceClassListCollectionView.GetItemAt(1), addedItems[1]);
        }

        [Test]
        public void CanCloseTest()
        {
            int messageBoxCallCount = 0;
            _viewModel.MessageBoxRequest += (sender, args) => { messageBoxCallCount++; };
            _viewModel.CanClose();
            Assert.AreEqual(0,messageBoxCallCount);
        }

        [Test]
        public void CanCloseWithChangesTest()
        {
            int messageBoxCallCount = 0;
            _viewModel.MessageBoxRequest += (sender, args) => { messageBoxCallCount++; };
            var toleranceClassForChanges = CreateParameterizedToleranceClassModel(5, "Test", 10, 15, false, 0, false);
            _viewModel.SelectedToleranceClassModel = toleranceClassForChanges;
            _viewModel.SelectedToleranceClassModel.Name = "Hans";
            _viewModel.CanClose();
            Assert.AreEqual(1, messageBoxCallCount);
        }

        [Test]
        public void UpdateColumnWidths()
        {
            var updatedColumns = new List<(string, double)>()
            {
                ("Name", 500),
                ("LowerLimit", 60)
            };
            _viewModel.UpdateColumnWidths("ToleranceClass", updatedColumns);

            Assert.AreEqual(updatedColumns[0].Item2, _viewModel.ListViewColumns["Name"].Width);
            Assert.AreEqual(updatedColumns[1].Item2, _viewModel.ListViewColumns["LowerLimit"].Width);
        }

        [Test]
        public void ShowSaveColumnErrorTest()
        {
            _viewModel.MessageBoxRequest += (sender, args) => { Assert.Pass(); };
            _viewModel.ShowSaveColumnError("ToleranceClass");
            Assert.Fail("No Message was shown");
        }

        [Test]
        public void ShowColumnWidthsTest()
        {
            var updatedColumns = new List<(string, double)>()
            {
                ("Name", 500),
                ("LowerLimit", 60)
            };
            _viewModel.ShowColumnWidths("ToleranceClass", updatedColumns);

            Assert.AreEqual(updatedColumns[0].Item2, _viewModel.ListViewColumns["Name"].Width);
            Assert.AreEqual(updatedColumns[1].Item2, _viewModel.ListViewColumns["LowerLimit"].Width);
        }

        [Test]
        public void ShowLoadColumnWidthsError()
        {
            _viewModel.MessageBoxRequest += (sender, args) => { Assert.Pass(); };
            _viewModel.ShowLoadColumnWidthsError("ToleranceClass");
            Assert.Fail("No Message was shown");
        }

        [Test]
        public void ShowReferencedLocationsFillsReferencedLocations()
        {
            var viewModel = new ToleranceClassViewModel(new StartUpMock(), null, null, new NullLocalizationWrapper());
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            var location = new LocationReferenceLink(new QstIdentifier(65432137741), new LocationNumber("0459t68zghfnmdel34izh"),
                new LocationDescription("059t68zfhdcnskwl4eprtig"), null);

            viewModel.ReferencedLocations.Add(new LocationReferenceLink(new QstIdentifier(15), new LocationNumber(""), new LocationDescription(""), null ));
            viewModel.ReferencedLocations.Add(new LocationReferenceLink(new QstIdentifier(15), new LocationNumber(""), new LocationDescription(""), null));

            viewModel.ShowReferencedLocations(new List<LocationReferenceLink>() { location });

            Assert.IsNotNull(viewModel.ReferencedLocations);
            Assert.AreEqual(1, viewModel.ReferencedLocations.Count);
            Assert.AreEqual(location.Id.ToLong(), viewModel.ReferencedLocations[0].Id.ToLong());
            Assert.AreEqual(location.Description, viewModel.ReferencedLocations[0].Description);
            Assert.AreEqual(location.Number, viewModel.ReferencedLocations[0].Number);
        }

        [Test]
        public void ShowReferencedLocationToolAssignmentsClearsReferencedLocationToolAssignmentsBeforeFilling()
        {
            var viewModel = new ToleranceClassViewModel(new StartUpMock(), null, null, new NullLocalizationWrapper());
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            var assignment = new LocationToolAssignment();

            viewModel.ReferencedLocationToolAssignments.Add(new LocationToolAssignmentModel(null, null));
            viewModel.ReferencedLocationToolAssignments.Add(new LocationToolAssignmentModel(null, null));

            viewModel.ShowReferencedLocationToolAssignments(new List<LocationToolAssignment>() {  });

            Assert.AreEqual(0, viewModel.ReferencedLocationToolAssignments.Count);
        }

        [Test]
        public void ShowReferencedLocationToolAssignmentsFillsReferencedLocationToolAssignments()
        {
            var viewModel = new ToleranceClassViewModel(new StartUpMock(), null, null, new NullLocalizationWrapper());
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            var assignment = new LocationToolAssignment();

            viewModel.ShowReferencedLocationToolAssignments(new List<LocationToolAssignment>() { assignment });

            Assert.IsNotNull(viewModel.ReferencedLocationToolAssignments);
            Assert.AreEqual(1, viewModel.ReferencedLocationToolAssignments.Count);
            Assert.AreEqual(assignment, viewModel.ReferencedLocationToolAssignments[0].Entity);
        }

        [Test]
        public void ShowReferencedLocationToolAssignmentsAddsThreeAssignments()
        {
            var viewModel = new ToleranceClassViewModel(new StartUpMock(), null, null, new NullLocalizationWrapper());
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            var assignment = new LocationToolAssignment();
            var assignment2 = new LocationToolAssignment();
            var assignment3 = new LocationToolAssignment();

            viewModel.ShowReferencedLocationToolAssignments(new List<LocationToolAssignment>() { assignment, assignment2, assignment3 });

            Assert.IsNotNull(viewModel.ReferencedLocationToolAssignments);
            Assert.AreEqual(3, viewModel.ReferencedLocationToolAssignments.Count);
            Assert.AreEqual(assignment, viewModel.ReferencedLocationToolAssignments[0].Entity);
            Assert.AreEqual(assignment2, viewModel.ReferencedLocationToolAssignments[1].Entity);
            Assert.AreEqual(assignment3, viewModel.ReferencedLocationToolAssignments[2].Entity);
        }

        [Test]
        public void ShowReferencedLocationsErrorInvokesMessageBoxRequest()
        {
            var viewModel = new ToleranceClassViewModel(new StartUpMock(), null, null, new NullLocalizationWrapper());

            viewModel.MessageBoxRequest += (s, e) => Assert.Pass();

            viewModel.ShowReferencesError();
        }

        [Test]
        public void LoadReferencedLocationsCanExecuteReturnsFalseIfSelectedIsNull()
        {
            var viewModel = new ToleranceClassViewModel(new StartUpMock(), null, null, new NullLocalizationWrapper());
            Assert.IsNull(viewModel.SelectedToleranceClassModel);
            Assert.IsFalse(viewModel.LoadReferencedLocationsCommand.CanExecute(null));
        }

        [Test]
        public void LoadReferencedLocationsCanExecuteTrueIfSelectedIsNotNull()
        {
            var viewModel = new ToleranceClassViewModel(new StartUpMock(), null, null, new NullLocalizationWrapper());
            viewModel.SelectedToleranceClassModel = new ToleranceClassModel(new ToleranceClass())
            {
                Id = 8789685452
            };
            
            Assert.IsTrue(viewModel.LoadReferencedLocationsCommand.CanExecute(null));
        }

        [TestCase(987878545)]
        [TestCase(67890467)]
        public void LoadReferencedLocationsCallsMethodsOnUseCase(long id)
        {
            var useCase = new ToleranceClassUseCaseMock(null, null, null, null);
            var viewModel = new ToleranceClassViewModel(new StartUpMock(), useCase, null, new NullLocalizationWrapper());

            viewModel.SelectedToleranceClassModel = new ToleranceClassModel(new ToleranceClass()) {Id = id};
            viewModel.LoadReferencedLocationsCommand.Execute(null);

            Assert.IsTrue(useCase.WasLoadReferencedLocationsCalled);
            Assert.AreEqual(id, useCase.LoadReferencedLocationsParameter.ToLong());
        }

        [Test]
        public void LoadReferencedLocationToolAssignmentsCanExecuteReturnsFalseIfSelectedIsNull()
        {
            var viewModel = new ToleranceClassViewModel(new StartUpMock(), null, null, new NullLocalizationWrapper());
            Assert.IsNull(viewModel.SelectedToleranceClassModel);
            Assert.IsFalse(viewModel.LoadReferencedLocationToolAssignmentsCommand.CanExecute(null));
        }

        [Test]
        public void LoadReferencedLocationToolAssignmentsCanExecuteTrueIfSelectedIsNotNull()
        {
            var viewModel = new ToleranceClassViewModel(new StartUpMock(), null, null, new NullLocalizationWrapper());
            viewModel.SelectedToleranceClassModel = new ToleranceClassModel(new ToleranceClass())
            {
                Id = 5415874
            };

            Assert.IsTrue(viewModel.LoadReferencedLocationToolAssignmentsCommand.CanExecute(null));
        }

        [TestCase(987878545)]
        [TestCase(67890467)]
        public void LoadReferencedLocationToolAssignmentsCallsMethodsOnUseCase(long id)
        {
            var useCase = new ToleranceClassUseCaseMock(null, null, null, null);
            var viewModel = new ToleranceClassViewModel(new StartUpMock(), useCase, null, new NullLocalizationWrapper());

            viewModel.SelectedToleranceClassModel = new ToleranceClassModel(new ToleranceClass()) { Id = id };
            viewModel.LoadReferencedLocationToolAssignmentsCommand.Execute(null);
            
            Assert.AreEqual(id, useCase.LoadReferencedLocationToolAssignmentsParameter.ToLong());
        }

        [Test]
        [Parallelizable]
        public void ShowRemoveToleranceClassPreventingReferencesCallsRequestWithCorrectParameters()
        {
            var toleranceClassUseCase = new ToleranceClassUseCaseMock(null, null, null, null);
            var toleranceClassViewModel = new ToleranceClassViewModel(new StartUpMock(), toleranceClassUseCase, null, new NullLocalizationWrapper());
            toleranceClassViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            var formatter = new MockLocationDisplayFormatter();
            string displayString = "Test";
            formatter.DisplayString = displayString;
            var location1 = new LocationReferenceLink(new QstIdentifier(15), new LocationNumber("Test"), new LocationDescription("Test"), formatter );
            var location2 = new LocationReferenceLink(new QstIdentifier(26), new LocationNumber("blub"), new LocationDescription("blub"), formatter);
            toleranceClassViewModel.ReferencesDialogRequest += (sender, list) =>
            {
                Assert.AreEqual(2, list[0].References.Count);
                Assert.AreEqual(displayString, list[0].References[0]);
                Assert.AreEqual(displayString, list[0].References[1]);
                Assert.Pass();
            };
            toleranceClassViewModel.ShowRemoveToleranceClassPreventingReferences(new List<LocationReferenceLink> { location1, location2}, null);
            Assert.Fail();
        }

        [Test]
        [Parallelizable]
        public void ShowRemoveToleranceClassPreventingReferencesWithReferencedAssignmentsCallsRequest()
        {
            var toleranceClassUseCase = new ToleranceClassUseCaseMock(null, null, null, null);
            var toleranceClassViewModel = new ToleranceClassViewModel(new StartUpMock(), toleranceClassUseCase, null, new NullLocalizationWrapper());
            toleranceClassViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            var assignment1 = new LocationToolAssignment()
            {
                AssignedTool = CreateTool.Anonymous(),
                AssignedLocation = CreateLocation.Anonymous()
            };
            var assignment2 = new LocationToolAssignment()
            {
                AssignedTool = CreateTool.Anonymous(),
                AssignedLocation = CreateLocation.Anonymous()
            };
            toleranceClassViewModel.ReferencesDialogRequest += (sender, list) =>
            {
                Assert.AreEqual(2, list[0].References.Count);
                Assert.Pass();
            };
            toleranceClassViewModel.ShowRemoveToleranceClassPreventingReferences(null, new List<LocationToolAssignment> { assignment1, assignment2 });
            Assert.Fail();
        }

        private bool AreEqual(ToleranceClass toleranceClass, ToleranceClassModel toleranceClassModel)
        {
            return toleranceClass.Id.ToLong() == toleranceClassModel.Id &&
                   toleranceClass.Name == toleranceClassModel.Name &&
                   toleranceClass.Relative == toleranceClassModel.Relative &&
                   toleranceClass.LowerLimit.Equals(toleranceClassModel.LowerLimit) &&
                   toleranceClass.UpperLimit.Equals(toleranceClassModel.UpperLimit);
        }

        private ToleranceClass CreateParameterizedToleranceClass(long id, string name = "", double lowerLimit = 0, double upperLimit = 0, bool relative = true)
        {
            return new ToleranceClass
            {
                Id = new ToleranceClassId(id),
                LowerLimit = lowerLimit,
                Name = name,
                Relative = relative,
                UpperLimit = upperLimit
            };
        }

        private ToleranceClassModel CreateParameterizedToleranceClassModel(long id, string name = "",
            double lowerLimit = 0, double upperLimit = 0, bool relative = true, double? symmetricalLimitsValue = null, bool symmetricalLimits=false)
        {
            var toleranceClassModel =  new ToleranceClassModel(new ToleranceClass())
            {
                Id = id,
                Name = name,
                LowerLimit = lowerLimit,
                UpperLimit = upperLimit,
                Relative = relative,
                SymmetricalLimits = symmetricalLimits
            };
            if (symmetricalLimitsValue.HasValue)
            {
                toleranceClassModel.SymmetricLimitsValue = symmetricalLimitsValue.Value;
            }

            return toleranceClassModel;
        }
    }
}