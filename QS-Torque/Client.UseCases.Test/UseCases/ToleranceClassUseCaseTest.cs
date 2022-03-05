using System.Collections.Generic;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using NUnit.Framework;
using TestHelper.Factories;
using TestHelper.Mock;

namespace Core.Test.UseCases
{
    public class ToleranceClassUseCaseTest
    {
        private class ToleranceClassGuiMock : IToleranceClassGui
        {
            public List<LocationReferenceLink> ShowReferencedLocationsParameter;
            public List<ToleranceClass> ShownToleranceClasses;
            public ToleranceClass RemovedToleranceClass;
            public int ShowToleranceClassesErrorCallCount;
            public int ShowToleranceClassesCallCount;
            public int RemoveToleranceClassErrorCallCount;
            public ToleranceClass AddedToleranceClass;
            public int AddToleranceClassErrorCallCount;
            public ToleranceClass SavedToleranceClass;
            public int SaveToleranceClassErrorCallCount;
            public int RemoveToleranceClassCallCount;
            public int AddToleranceClassCallCount;
            public int SaveToleranceClassCallCount;
            public bool ShowReferencesErrorCalled = false;
            public List<LocationToolAssignment> ShowReferencedLocationToolAssignmentsParameter;
            public List<LocationReferenceLink> ShowRemoveToleranceClassPreventingReferencesParameterReferencedLocations { get; set; }
            public List<LocationToolAssignment> ShowRemoveToleranceClassPreventingReferencesParameterReferencedAssignments { get; set; }


            public void ShowToleranceClasses(List<ToleranceClass> toleranceClasses)
            {
                ShownToleranceClasses = toleranceClasses;
                ShowToleranceClassesCallCount++;
            }

            public void ShowToleranceClassesError()
            {
                ShowToleranceClassesErrorCallCount++;
            }

            public void RemoveToleranceClass(ToleranceClass toleranceClass)
            {
                RemovedToleranceClass = toleranceClass;
                RemoveToleranceClassCallCount++;
            }

            public void RemoveToleranceClassError()
            {
                RemoveToleranceClassErrorCallCount++;
            }

            public void AddToleranceClass(ToleranceClass toleranceClass)
            {
                AddedToleranceClass = toleranceClass;
                AddToleranceClassCallCount++;
            }

            public void AddToleranceClassError()
            {
                AddToleranceClassErrorCallCount++;
            }

            public void UpdateToleranceClass(ToleranceClass toleranceClass)
            {
                SavedToleranceClass = toleranceClass;
                SaveToleranceClassCallCount++;
            }

            public void SaveToleranceClassError()
            {
                SaveToleranceClassErrorCallCount++;
            }

            public void ShowReferencedLocations(List<LocationReferenceLink> locations)
            {
                ShowReferencedLocationsParameter = locations;
            }

            public void ShowReferencesError()
            {
                ShowReferencesErrorCalled = true;
            }

            public void ShowReferencedLocationToolAssignments(List<LocationToolAssignment> assignments)
            {
                ShowReferencedLocationToolAssignmentsParameter = assignments;
            }

            public void ShowRemoveToleranceClassPreventingReferences(List<LocationReferenceLink> referencedLocations, List<LocationToolAssignment> referencedLocationToolAssignments)
            {
                ShowRemoveToleranceClassPreventingReferencesParameterReferencedLocations = referencedLocations;
                ShowRemoveToleranceClassPreventingReferencesParameterReferencedAssignments = referencedLocationToolAssignments;
            }
        }

        private ToleranceClassUseCase _useCase;
        private ToleranceClassGuiMock _guiInterface;
        private ToleranceClassDataAccessMock _dataInterface;
        private ToleranceClass _testToleranceClass;
        private UserGetterMock _userGetter;

        [SetUp]
        public void Setup()
        {
            _guiInterface = new ToleranceClassGuiMock();
            _dataInterface = new ToleranceClassDataAccessMock();
            _userGetter = new UserGetterMock();
            _useCase = new ToleranceClassUseCase(_guiInterface, _dataInterface, _userGetter, null, new NotificationManagerMock());
            _testToleranceClass = new ToleranceClass
                { Id = new ToleranceClassId(1), LowerLimit = 10.123, UpperLimit = 11.123, Name = "TestToleranceClass", Relative = true };
        }

        [Test]
        public void LoadToleranceClassesTest()
        {
            var toleranceClasses = new List<ToleranceClass>
            {
                new ToleranceClass {Id = new ToleranceClassId(0), Name = "UpperLower", LowerLimit = 5, UpperLimit = 10},
                new ToleranceClass {Id = new ToleranceClassId(1), Name = "LowerUpper", LowerLimit = 1, UpperLimit = 2}
            };

            _dataInterface.LoadToleranceClassesData = toleranceClasses;
            _useCase.LoadToleranceClasses();
            Assert.AreEqual(toleranceClasses, _guiInterface.ShownToleranceClasses);
        }

        [Test]
        public void LoadToleranceClassesErrorTest()
        {
            var toleranceClasses = new List<ToleranceClass>
            {
                new ToleranceClass {Id = new ToleranceClassId(0), Name = "UpperLower", LowerLimit = 5, UpperLimit = 10},
                new ToleranceClass {Id = new ToleranceClassId(1), Name = "LowerUpper", LowerLimit = 1, UpperLimit = 2}
            };

            _dataInterface.LoadToleranceClassesThrowsException = true;
            _dataInterface.LoadToleranceClassesData = toleranceClasses;
            _useCase.LoadToleranceClasses();
            Assert.AreEqual(1, _guiInterface.ShowToleranceClassesErrorCallCount);
        }

        [Test]
        public void RemoveToleranceClassTest()
        {
            _useCase.RemoveToleranceClass(_testToleranceClass, _guiInterface);
            Assert.AreEqual(_testToleranceClass, _guiInterface.RemovedToleranceClass);
        }

        [Test]
        public void RemoveToleranceClassErrorTest()
        {
            _dataInterface.RemoveToleranceClassThrowsException = true;
            _useCase.RemoveToleranceClass(_testToleranceClass, _guiInterface);
            Assert.AreEqual(1, _guiInterface.RemoveToleranceClassErrorCallCount);
        }

        [Test]
        public void RemoveToleranceClassDiffHasCorrectUser()
        {
            var user = CreateUser.Parametrized(5, "Test", CreateGroup.Anonymous());
            _userGetter.NextReturnedUser = user;
            _useCase.RemoveToleranceClass(CreateParameterizedToleranceClass(5), _guiInterface);
            Assert.AreEqual(user, _dataInterface.RemovedToleranceClassUser);
        }

        [Test]
        public void RemoveToleranceClassWithReferencesCallsGuiShowRemoveToleranceClassPreventingReferences()
        {
            var location = new LocationReferenceLink(new QstIdentifier(15), new LocationNumber("blub"), new LocationDescription("blub"), null);
            _dataInterface.LoadReferencedLocationsReturnValue = new List<LocationReferenceLink>
            {
                location
            };
            _useCase.RemoveToleranceClass(CreateAnonymousToleranceClass(), _guiInterface);
            Assert.AreEqual(1, _guiInterface.ShowRemoveToleranceClassPreventingReferencesParameterReferencedLocations.Count);
            Assert.AreEqual(location.Id.ToLong(), _guiInterface.ShowRemoveToleranceClassPreventingReferencesParameterReferencedLocations[0].Id.ToLong());
        }

        [Test]
        public void RemoveToleranceClassWithReferencedLocationToolAssignmentsCallsGui()
        {
            var data = new ToleranceClassDataAccessMock();
            var gui = new ToleranceClassGuiMock();
            var locationToolAssignmentData = new LocationToolAssignmentDataMock();
            var useCase = new ToleranceClassUseCase(gui, data, null, locationToolAssignmentData, null);

            locationToolAssignmentData.GetLocationToolAssignmentsByIdsReturnValue = new List<LocationToolAssignment>();
            data.LoadReferencedLocationToolAssignmentsReturnValue = new List<LocationToolAssignmentId>()
            {
                new LocationToolAssignmentId(8546)
            };

            useCase.RemoveToleranceClass(CreateAnonymousToleranceClass(), gui);
            
            Assert.AreEqual(data.LoadReferencedLocationToolAssignmentsReturnValue, locationToolAssignmentData.GetLocationToolAssignmentsByIdsParameter);
            Assert.AreEqual(locationToolAssignmentData.GetLocationToolAssignmentsByIdsReturnValue, gui.ShowRemoveToleranceClassPreventingReferencesParameterReferencedAssignments);
        }

        private ToleranceClass CreateAnonymousToleranceClass()
        {
            return CreateParameterizedToleranceClass(15);
        }

        [TestCase(6)]
        [TestCase(253)]
        public void AddToleranceClassTest(long addId)
        {
            _dataInterface.AddId = new ToleranceClassId(addId);
            _testToleranceClass.Id = new ToleranceClassId(0);

            _useCase.AddToleranceClass(_testToleranceClass);
            Assert.AreEqual(_testToleranceClass, _dataInterface.AddedToleranceClass);
            Assert.AreEqual(_guiInterface.AddedToleranceClass.Id, _dataInterface.AddId);
        }

        [TestCase(5)]
        [TestCase(265)]
        public void AddToleranceClassErrorTest(long addId )
        {
            _dataInterface.AddId = new ToleranceClassId(addId);
            _testToleranceClass.Id = new ToleranceClassId(0);
            _dataInterface.AddToleranceCLassThrowsException = true;
            _useCase.AddToleranceClass(_testToleranceClass);
            Assert.AreEqual(1, _guiInterface.AddToleranceClassErrorCallCount);
        }

        [Test]
        public void AddToleranceClassHasCorrectUserParameter()
        {
            var user = CreateUser.Parametrized(5, "Test", CreateGroup.Anonymous());
            _userGetter.NextReturnedUser = user;
            _useCase.AddToleranceClass(CreateParameterizedToleranceClass(5));
            Assert.AreEqual(user, _dataInterface.AddToleranceClassUser);
        }

        [Test]
        public void SaveToleranceClassTest()
        {
            _useCase.SaveToleranceClass(_testToleranceClass, _testToleranceClass);
            Assert.AreEqual(_testToleranceClass, _dataInterface.SavedToleranceClass);
            Assert.AreEqual(_testToleranceClass, _guiInterface.SavedToleranceClass);
        }

        [Test]
        public void SaveToleranceClassErrorTest()
        {
            _dataInterface.SaveToleranceClassThrowsException = true;
            _useCase.SaveToleranceClass(_testToleranceClass,_testToleranceClass);
            Assert.AreEqual(1, _guiInterface.SaveToleranceClassErrorCallCount);
        }

        [Test]
        public void SaveToleranceClassDiffHasCorrectUser()
        {
            var user = CreateUser.Parametrized(5, "Test", CreateGroup.Anonymous());
            _userGetter.NextReturnedUser = user;
            _useCase.SaveToleranceClass(CreateParameterizedToleranceClass(5),CreateParameterizedToleranceClass(5));
            Assert.AreEqual(user, _dataInterface.SaveToleranceClassUser);
        }

        [Test]
        public void LoadReferencedLocationsCallsMethodOnDataAccess()
        {
            var data = new ToleranceClassDataAccessMock();
            var gui = new ToleranceClassGuiMock();
            var useCase = new ToleranceClassUseCase(gui, data, null, null, null);

            var id = new ToleranceClassId(8654);
            useCase.LoadReferencedLocations(id);

            Assert.AreEqual(id, data.LoadReferencedLocationsParameter);
        }

        [Test]
        public void LoadReferencedLocationCallsMethodOnGui()
        {
            var data = new ToleranceClassDataAccessMock();
            var gui = new ToleranceClassGuiMock();
            var useCase = new ToleranceClassUseCase(gui, data, null, null, null);
            data.LoadReferencedLocationsReturnValue = new List<LocationReferenceLink>() { };

            useCase.LoadReferencedLocations(new ToleranceClassId(564));
            Assert.AreEqual(data.LoadReferencedLocationsReturnValue, gui.ShowReferencedLocationsParameter);
        }

        [Test]
        public void LoadReferencesPassesCorectDataFromDataAccessToGui()
        {
            var data = new ToleranceClassDataAccessMock();
            var gui = new ToleranceClassGuiMock();
            var useCase = new ToleranceClassUseCase(gui, data, null, null, null);

            data.LoadReferencedLocationsReturnValue = new List<LocationReferenceLink>();
            useCase.LoadReferencedLocations(new ToleranceClassId(564));
            Assert.AreEqual(data.LoadReferencedLocationsReturnValue, gui.ShowReferencedLocationsParameter);
        }

        [Test]
        public void LoadReferncedLocationsInvokesMethodOnGuiIfErrorOccures()
        {
            var data = new ToleranceClassDataAccessMock();
            var gui = new ToleranceClassGuiMock();
            var useCase = new ToleranceClassUseCase(gui, data, null, null, null);

            data.ThrowsLoadReferencedLocationsException = true;
            useCase.LoadReferencedLocations(new ToleranceClassId(564));
            Assert.IsTrue(gui.ShowReferencesErrorCalled);
        }

        [Test]
        public void LoadReferencedLocationToolAssignmentsCallsDataAccess()
        {
            var data = new ToleranceClassDataAccessMock();
            var gui = new ToleranceClassGuiMock();
            var useCase = new ToleranceClassUseCase(gui, data, null, null, null);

            var id = new ToleranceClassId(8654);
            useCase.LoadReferencedLocationToolAssignments(id);
            
            Assert.AreEqual(id, data.LoadReferencedLocationToolAssignmentsParameter);
        }

        [Test]
        public void LoadReferencedLocationToolAssignmentsCallsGui()
        {
            var data = new ToleranceClassDataAccessMock();
            var gui = new ToleranceClassGuiMock();
            var locationToolAssignmentData = new LocationToolAssignmentDataMock();
            var useCase = new ToleranceClassUseCase(gui, data, null, locationToolAssignmentData, null);

            locationToolAssignmentData.GetLocationToolAssignmentsByIdsReturnValue = new List<LocationToolAssignment>();
            data.LoadReferencedLocationToolAssignmentsReturnValue = new List<LocationToolAssignmentId>();
            useCase.LoadReferencedLocationToolAssignments(new ToleranceClassId(564));
            Assert.AreEqual(locationToolAssignmentData.GetLocationToolAssignmentsByIdsReturnValue, gui.ShowReferencedLocationToolAssignmentsParameter);
            Assert.AreEqual(data.LoadReferencedLocationToolAssignmentsReturnValue, locationToolAssignmentData.GetLocationToolAssignmentsByIdsParameter);
        }

        [Test]
        public void LoadReferencedLocationToolAssignmentsWithThrownErrorCallsGui()
        {
            var data = new ToleranceClassDataAccessMock();
            var gui = new ToleranceClassGuiMock();
            var useCase = new ToleranceClassUseCase(gui, data, null, null, null);

            data.ThrowsLoadReferencedLocationToolAssignmentsException = true;
            useCase.LoadReferencedLocationToolAssignments(new ToleranceClassId(564));
            Assert.IsTrue(gui.ShowReferencesErrorCalled);
        }

        [Test]
        public void AddToleranceClassWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var data = new ToleranceClassDataAccessMock();
            var gui = new ToleranceClassGuiMock();
            var useCase = new ToleranceClassUseCase(gui, data, new UserGetterMock(), null, notificationManager);
            useCase.AddToleranceClass(CreateAnonymousToleranceClass());
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void RemoveToleranceClassWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var data = new ToleranceClassDataAccessMock();
            var gui = new ToleranceClassGuiMock();
            var useCase = new ToleranceClassUseCase(gui, data, new UserGetterMock(), null, notificationManager);
            useCase.RemoveToleranceClass(CreateAnonymousToleranceClass(), gui);
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void SaveToleranceClassWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var data = new ToleranceClassDataAccessMock();
            var gui = new ToleranceClassGuiMock();
            var useCase = new ToleranceClassUseCase(gui, data, new UserGetterMock(), null, notificationManager);
            useCase.SaveToleranceClass(CreateAnonymousToleranceClass(), CreateAnonymousToleranceClass());
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
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
    }
}