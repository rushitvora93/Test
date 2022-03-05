using System;
using System.Collections.Generic;
using Client.UseCases.UseCases;
using Core.Diffs;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using NUnit.Framework;
using TestHelper.Factories;
using TestHelper.Mock;

namespace Core.Test.UseCases
{
    [Parallelizable(ParallelScope.Children)]
    class LocationToolAssignmentUseCaseTest
    {
        private class LocationToolAssignmentDataMock : ILocationToolAssignmentData
        {
            public LocationToolAssignment AssignToolToLocationParameter;
            public bool ThrowAssignToolToLocationError;
            public LocationId LoadAssignetToolsForLocationParameter;
            public List<LocationToolAssignment> LoadAssignedToolsForLocationReturnValue;
            public bool ThrowsLoadAssignedToolsForLocationException;
            public LocationToolAssignment AddTestConditionsParameter;

            public User AddTestConditionsUserParameter { get; private set; }

            public bool ThrowsAddTestConditionsException;
            public LocationId LoadUnusedToolUsagesForLocationParameter;
            public List<ToolUsage> LoadUnusedToolUsagesForLocationReturnValue;
            public bool ThrowsLoadUnusedToolUsagesForLocationException;
            public LocationToolAssignment RemoveLocationToolAssignmentParameter;

            public User RemoveLocationToolAssignmentUserParameter { get; private set; }

            public bool ThrowsRemoveLocationToolAssignmentException;
            public int LoadLocationReferencesForToolCallCount { get; set; }
            public int UpdateLocationToolAssignmentCallCount { get; set; }
            public List<LocationToolAssignmentDiff> UpdateLocationToolAssignmentParameter { get; set; }
            public bool UpdateLocationToolAssignmentThrowsException { get; set; }
            public User AssignToolToLocationUserParameter { get; internal set; }

            public List<LocationToolAssignment> LoadLocationToolAssignmentsReturnValue { get; set; }
            public bool LoadLocationToolAssignmentsThrowsError { get; set; }

            public List<LocationToolAssignment> LoadLocationToolAssignments()
            {
                if (LoadLocationToolAssignmentsThrowsError)
                {
                    throw new Exception();
                }

                return LoadLocationToolAssignmentsReturnValue;
            }

            public List<LocationToolAssignment> GetLocationToolAssignmentsByIds(List<LocationToolAssignmentId> ids)
            {
                throw new NotImplementedException();
            }

            public void AssignToolToLocation(LocationToolAssignment assignment, User user)
            {
                if (ThrowAssignToolToLocationError)
                {
                    throw new Exception();
                }

                AssignToolToLocationParameter = assignment;
                AssignToolToLocationUserParameter = user;
            }

            public List<LocationToolAssignment> LoadAssignedToolsForLocation(LocationId locationId)
            {
                if (ThrowsLoadAssignedToolsForLocationException)
                {
                    throw new Exception();
                }

                LoadAssignetToolsForLocationParameter = locationId;
                return LoadAssignedToolsForLocationReturnValue;
            }

            public void AddTestConditions(LocationToolAssignment assignment, User user)
            {
                if (ThrowsAddTestConditionsException)
                {
                    throw new Exception();
                }

                AddTestConditionsParameter = assignment;
                AddTestConditionsUserParameter = user;
            }

            public List<ToolUsage> LoadUnusedToolUsagesForLocation(LocationId locationId)
            {
                if (ThrowsLoadUnusedToolUsagesForLocationException)
                {
                    throw new Exception();
                }

                LoadUnusedToolUsagesForLocationParameter = locationId;
                return LoadUnusedToolUsagesForLocationReturnValue;
            }

            public void RemoveLocationToolAssignment(LocationToolAssignment assignment, User user)
            {
                if (ThrowsRemoveLocationToolAssignmentException)
                {
                    throw new Exception();
                }

                RemoveLocationToolAssignmentParameter = assignment;
                RemoveLocationToolAssignmentUserParameter = user;
            }

            public List<LocationReferenceLink> LoadLocationReferenceLinksForTool(ToolId toolId)
            {
                LoadLocationReferencesForToolCallCount++;
                return null;
            }

            public void UpdateLocationToolAssignment(List<LocationToolAssignmentDiff> diff)
            {
                if (UpdateLocationToolAssignmentThrowsException)
                {
                    throw new Exception();
                }
                UpdateLocationToolAssignmentCallCount++;
                UpdateLocationToolAssignmentParameter = diff;
            }

            public void UpdateNextChkTestDate(LocationToolAssignment assignment, DateTime testTimestamp)
            {
                throw new NotImplementedException();
            }

            public void UpdateNextMfuTestDate(LocationToolAssignment assignment, DateTime testTimestamp)
            {
                throw new NotImplementedException();
            }

            public void RestoreLocationToolAssignment(LocationToolAssignment assignment, User user)
            {
                throw new NotImplementedException();
            }
        }

        private class LocationToolAssignmentGuiMock : ILocationToolAssignmentGui
        {
            public LocationToolAssignment AssignToolToLocationParameter;
            public bool WasAssignToolToLocationErrorCalled;
            public List<LocationToolAssignment> ShowAssignetToolsForLocationParameter;
            public bool WasLoadAssignedToolsForLocationErrorCalled;
            public LocationToolAssignment AddTestConditionsParameter;
            public bool WasAddTestConditionsErrorCalled;
            public List<ToolUsage> LoadUnusedToolUsagesForLocationParameterToolUsages;
            public LocationId LoadUnusedToolUsagesForLocationParameterLocationId;
            public bool WasLoadUnusedToolUsagesForLocationErrorCalled;
            public LocationToolAssignment RemoveLocationToolAssignmentParameter;
            public bool WasRemoveLocationToolAssignmentErrorCalled;
            public int ShowLocationReferenceLinksForToolCallCount { get; set; }
            public int UpdateLocationToolAssignmentCallCount { get; set; }
            public List<LocationToolAssignment> UpdateLocationToolAssignmentParameter { get; set; }
            public int UpdateLocationToolAssignmentErrorCallCount { get; set; }
            public List<LocationToolAssignment> LoadLocationToolAssignmentsParameter { get; set; }
            public bool ShowLocationToolAssignmentErrorCalled { get; set; }

            public void LoadLocationToolAssignments(List<LocationToolAssignment> locationToolAssignments)
            {
                LoadLocationToolAssignmentsParameter = locationToolAssignments;
            }

            public void ShowLocationToolAssignmentError()
            {
                ShowLocationToolAssignmentErrorCalled = true;
            }

            public void AssignToolToLocation(LocationToolAssignment assignment)
            {
                AssignToolToLocationParameter = assignment;
            }

            public void AssignToolToLocationError()
            {
                WasAssignToolToLocationErrorCalled = true;
            }

            public void ShowAssignedToolsForLocation(List<LocationToolAssignment> assignments)
            {
                ShowAssignetToolsForLocationParameter = assignments;
            }

            public void LoadAssignedToolsForLocationError()
            {
                WasLoadAssignedToolsForLocationErrorCalled = true;
            }

            public void AddTestConditions(LocationToolAssignment assignment)
            {
                AddTestConditionsParameter = assignment;
            }

            public void AddTestConditionsError()
            {
                WasAddTestConditionsErrorCalled = true;
            }

            public void ShowUnusedToolUsagesForLocation(List<ToolUsage> toolUsages, LocationId locationId)
            {
                LoadUnusedToolUsagesForLocationParameterToolUsages = toolUsages;
                LoadUnusedToolUsagesForLocationParameterLocationId = locationId;
            }

            public void LoadUnusedToolUsagesForLocationError()
            {
                WasLoadUnusedToolUsagesForLocationErrorCalled = true;
            }

            public void RemoveLocationToolAssignment(LocationToolAssignment assignment)
            {
                RemoveLocationToolAssignmentParameter = assignment;
            }

            public void RemoveLocationToolAssignmentError()
            {
                WasRemoveLocationToolAssignmentErrorCalled = true;
            }

            public void ShowLocationReferenceLinksForTool(List<LocationReferenceLink> locationReferenceLinks)
            {
                ShowLocationReferenceLinksForToolCallCount++;
            }

            public void UpdateLocationToolAssignment(List<LocationToolAssignment> updatedLocationToolAssignment)
            {
                UpdateLocationToolAssignmentCallCount++;
                UpdateLocationToolAssignmentParameter = updatedLocationToolAssignment;
            }

            public void UpdateLocationToolAssignmentError()
            {
                UpdateLocationToolAssignmentErrorCallCount++;
            }
        }

        public class LocationToolAssignmentErrorHandlerMock : ILocationToolAssignmentErrorHandler
        {
            public bool ShowLocationToolAssignmentErrorCalled { get; set; }

            public void ShowLocationToolAssignmentError()
            {
                ShowLocationToolAssignmentErrorCalled = true;
            }
        }

        public class LocationToolAssignmentDiffShowerMock : ILocationToolAssignmentDiffShower
        {
            public List<LocationToolAssignmentDiff> DiffsParameter { get; set; }
            public Action SaveActionParameter { get; set; }

            public void ShowDiffDialog(List<LocationToolAssignmentDiff> diffs, Action saveAction)
            {
                DiffsParameter = diffs;
                SaveActionParameter = saveAction;
            }
        }


        [Test]
        public void AssignToolToLocationCallsDataAccess()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);
            var assignment = CreateLocationToolAssignment.Anonymous();

            useCase.AssignToolToLocation(assignment);
            
            Assert.AreEqual(assignment, data.AssignToolToLocationParameter);
        }

        [Test]
        public void AssignToolToLocationCallsDataAccessWithCorrectUser()
        {
            var data = CreateData();
            var gui = CreateGui();
            var userGetter = new UserGetterMock();
            var useCase = CreateUseCase(data, gui, userGetter);
            var user = CreateUser.IdOnly(15);
            userGetter.NextReturnedUser = user;
            useCase.AssignToolToLocation(CreateLocationToolAssignment.Anonymous());
            Assert.AreEqual(user, data.AssignToolToLocationUserParameter);
        }

        [Test]
        public void AssignToolToLocationCallsGui()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);
            var assignment = CreateLocationToolAssignment.Anonymous();

            useCase.AssignToolToLocation(assignment);

            Assert.AreEqual(assignment, gui.AssignToolToLocationParameter);
        }

        [Test]
        public void AssignToolToLocationWithExceptionThrownInDataAccessCallsGui()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);

            data.ThrowAssignToolToLocationError = true;
            useCase.AssignToolToLocation(null);

            Assert.IsTrue(gui.WasAssignToolToLocationErrorCalled);
        }
        
        [Test]
        public void LoadToolAssignmentsForLocationCallsDataAccess()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);
            var location = CreateLocation.IdOnly(984613);
            useCase.LoadToolAssignmentsForLocation(location);

            Assert.AreEqual(location.Id, data.LoadAssignetToolsForLocationParameter);
        }

        [Test]
        public void LoadToolAssignmentsForLocationCallsGui()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);
            var tools = new List<LocationToolAssignment>();

            data.LoadAssignedToolsForLocationReturnValue = tools;
            useCase.LoadToolAssignmentsForLocation(CreateLocation.Anonymous());

            Assert.AreEqual(tools, gui.ShowAssignetToolsForLocationParameter);
        }

        [Test]
        public void LoadToolAssignmentsForLocationWithExceptionThrownInDataAccessCallsGui()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);

            data.ThrowsLoadAssignedToolsForLocationException = true;
            useCase.LoadToolAssignmentsForLocation(null);

            Assert.IsTrue(gui.WasLoadAssignedToolsForLocationErrorCalled);
        }

        [Test]
        public void AddTestConditionsCallsDataAccess()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);
            var assignment = CreateLocationToolAssignment.Anonymous();

            useCase.AddTestConditions(assignment);

            Assert.AreEqual(assignment, data.AddTestConditionsParameter);
        }

        [Test]
        public void AddTestConditionsCallsDataAccessWitUser()
        {
            var data = CreateData();
            var gui = CreateGui();
            var userGetter = new UserGetterMock();
            var useCase = CreateUseCase(data, gui, userGetter);
            var user = CreateUser.IdOnly(15);
            userGetter.NextReturnedUser = user;
            useCase.AddTestConditions(CreateLocationToolAssignment.Anonymous());
            Assert.AreEqual(user, data.AddTestConditionsUserParameter);
        }

        [Test]
        public void AddTestConditionsCallsGui()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);
            var assignment = CreateLocationToolAssignment.Anonymous();

            useCase.AddTestConditions(assignment);

            Assert.AreEqual(assignment, gui.AddTestConditionsParameter);
        }

        [Test]
        public void AddTestConditionsWithExceptionThrownInDataAccessCallsGui()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);

            data.ThrowsAddTestConditionsException = true;
            useCase.AddTestConditions(null);

            Assert.IsTrue(gui.WasAddTestConditionsErrorCalled);
        }

        [Test]
        public void LoadUnusedToolUsagesForLocationCallsDataAccess()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);
            var id = new LocationId(984613);

            useCase.LoadUnusedToolUsagesForLocation(id);

            Assert.AreEqual(id, data.LoadUnusedToolUsagesForLocationParameter);
        }

        [Test]
        public void LoadUnusedToolUsagesForLocationCallsGui()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);
            var toolUsages = new List<ToolUsage>();
            var id = new LocationId(15485123);

            data.LoadUnusedToolUsagesForLocationReturnValue = toolUsages;
            useCase.LoadUnusedToolUsagesForLocation(id);

            Assert.AreEqual(toolUsages, gui.LoadUnusedToolUsagesForLocationParameterToolUsages);
            Assert.AreEqual(id, gui.LoadUnusedToolUsagesForLocationParameterLocationId);
        }

        [Test]
        public void LoadUnusedToolUsagesForLocationWithExceptionThrownInDataAccessCallsGui()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);

            data.ThrowsLoadUnusedToolUsagesForLocationException = true;
            useCase.LoadUnusedToolUsagesForLocation(new LocationId(5));

            Assert.IsTrue(gui.WasLoadUnusedToolUsagesForLocationErrorCalled);
        }

        [Test]
        public void RemoveLocationToolAssignmentCallsDataAccess()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);
            var assignment = CreateLocationToolAssignment.Anonymous();

            useCase.RemoveLocationToolAssignment(assignment);

            Assert.AreEqual(assignment, data.RemoveLocationToolAssignmentParameter);
        }

        [Test]
        public void RemoveLocationToolAssignmentCallsDataAccessWithUser()
        {
            var data = CreateData();
            var gui = CreateGui();
            var userGetter = new UserGetterMock();
            var useCase = CreateUseCase(data, gui, userGetter);
            var user = CreateUser.IdOnly(15);
            userGetter.NextReturnedUser = user;
            useCase.RemoveLocationToolAssignment(CreateLocationToolAssignment.Anonymous());
            Assert.AreEqual(user, data.RemoveLocationToolAssignmentUserParameter);
        }

        [Test]
        public void RemoveLocationToolAssignmentCallsGui()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);
            var assignment = CreateLocationToolAssignment.Anonymous();

            useCase.RemoveLocationToolAssignment(assignment);

            Assert.AreEqual(assignment, gui.RemoveLocationToolAssignmentParameter);
        }

        [Test]
        public void RemoveLocationToolAssignmentWithExceptionThrownInDataAccessCallsGui()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);

            data.ThrowsRemoveLocationToolAssignmentException = true;
            useCase.RemoveLocationToolAssignment(CreateLocationToolAssignment.Anonymous());

            Assert.IsTrue(gui.WasRemoveLocationToolAssignmentErrorCalled);
        }

        [Test]
        public void LoadLocationReferencesForToolCallsDataLoadLocationReferencesForTool()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);

            useCase.LoadLocationReferencesForTool(new ToolId(15));
            Assert.AreEqual(1, data.LoadLocationReferencesForToolCallCount);
        }

        [Test]
        public void LoadLocationReferencesForToolCallsGuiShowLocationReferenceLinksForTool()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);

            useCase.LoadLocationReferencesForTool(new ToolId(15));
            Assert.AreEqual(1, gui.ShowLocationReferenceLinksForToolCallCount);
        }

        [Test]
        public void UpdateLocationToolAssignmentCallsDataUpdateLocationToolAssignmentWithParameters()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);

            var diffs = new List<LocationToolAssignmentDiff>()
            {
                new LocationToolAssignmentDiff
                {
                    NewLocationToolAssignment = CreateLocationToolAssignment.Anonymous(),
                    OldLocationToolAssignment = CreateLocationToolAssignment.Anonymous()
                },
                new LocationToolAssignmentDiff
                {
                    NewLocationToolAssignment = CreateLocationToolAssignment.Anonymous(),
                    OldLocationToolAssignment = CreateLocationToolAssignment.Anonymous()
                }
            };
            var diffShower = new LocationToolAssignmentDiffShowerMock();
            useCase.UpdateLocationToolAssignment(diffs, new LocationToolAssignmentErrorHandlerMock(), diffShower);
            diffShower.SaveActionParameter();
            Assert.AreEqual(1, data.UpdateLocationToolAssignmentCallCount);
            Assert.AreEqual(2, data.UpdateLocationToolAssignmentParameter.Count);
            Assert.AreEqual(diffs[0], data.UpdateLocationToolAssignmentParameter[0]);
            Assert.AreEqual(diffs[1], data.UpdateLocationToolAssignmentParameter[1]);
        }

        [Test]
        public void UpdateLocationToolAssignmentCallsGuiUpdateLocationToolAssignmentWithParameters()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);
            
            var diffs = new List<LocationToolAssignmentDiff>()
            {
                new LocationToolAssignmentDiff
                {
                    NewLocationToolAssignment = CreateLocationToolAssignment.Anonymous(),
                    OldLocationToolAssignment = CreateLocationToolAssignment.Anonymous()
                },
                new LocationToolAssignmentDiff
                {
                    NewLocationToolAssignment = CreateLocationToolAssignment.Anonymous(),
                    OldLocationToolAssignment = CreateLocationToolAssignment.Anonymous()
                }
            };
            var diffShower = new LocationToolAssignmentDiffShowerMock();
            useCase.UpdateLocationToolAssignment(diffs, new LocationToolAssignmentErrorHandlerMock(), diffShower);
            diffShower.SaveActionParameter();
            Assert.AreEqual(1, gui.UpdateLocationToolAssignmentCallCount);
            Assert.AreEqual(2, gui.UpdateLocationToolAssignmentParameter.Count);
            Assert.AreEqual(diffs[0].NewLocationToolAssignment, gui.UpdateLocationToolAssignmentParameter[0]);
            Assert.AreEqual(diffs[1].NewLocationToolAssignment, gui.UpdateLocationToolAssignmentParameter[1]);
        }

        [Test]
        public void UpdateLocationToolAssignmentCallsGuiUpdateLocationToolAssignmentError()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);

            var diffShower = new LocationToolAssignmentDiffShowerMock();
            useCase.UpdateLocationToolAssignment(new List<LocationToolAssignmentDiff>()
            {
                new LocationToolAssignmentDiff()
                {
                    OldLocationToolAssignment = new LocationToolAssignment() { TestLevelNumberChk = 2 },
                    NewLocationToolAssignment = new LocationToolAssignment() { TestLevelNumberChk = 3 }
                }
            }, new LocationToolAssignmentErrorHandlerMock(), diffShower);
            diffShower.SaveActionParameter();
            useCase.UpdateLocationToolAssignment(new List<LocationToolAssignmentDiff>() { new LocationToolAssignmentDiff() });
            Assert.AreEqual(1, gui.UpdateLocationToolAssignmentErrorCallCount);
        }

        [Test]
        public void UpdateLocationToolAssignmentThrowsArgumentNullExceptionOnNullLocationToolAssignmentDiff()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);

            Assert.Throws<ArgumentNullException>(() => useCase.UpdateLocationToolAssignment(null));
        }

        [Test]
        public void UpdateLocationToolAssignmentThrowsArgumentNullExceptionOnEmptyLocationToolAssignmentDiffs()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);

            Assert.Throws<ArgumentNullException>(() => useCase.UpdateLocationToolAssignment(new List<LocationToolAssignmentDiff>()));
        }

        [Test]
        public void ChangeLocationParentWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var useCase = CreateUseCase(CreateData(), CreateGui(), null, notificationManager);
            useCase.AssignToolToLocation(CreateLocationToolAssignment.Anonymous());
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void AddTestConditionsWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var useCase = CreateUseCase(CreateData(), CreateGui(), null, notificationManager);
            useCase.AddTestConditions(CreateLocationToolAssignment.Anonymous());
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void RemoveLocationToolAssignmentWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var useCase = CreateUseCase(CreateData(), CreateGui(), null, notificationManager);
            useCase.RemoveLocationToolAssignment(CreateLocationToolAssignment.Anonymous());
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void UpdateLocationToolAssignmentWithoutErrorCallsSendSuccessNotification(int changes)
        {
            var notificationManager = new NotificationManagerMock();
            var useCase = CreateUseCase(CreateData(), CreateGui(), null, notificationManager);
            var diffs = new List<LocationToolAssignmentDiff>();
            for (int i = 0; i < changes; i++)
            {
                diffs.Add(new LocationToolAssignmentDiff() { NewLocationToolAssignment = new LocationToolAssignment() { Id = new LocationToolAssignmentId(i) } });
            }
            var diffShower = new LocationToolAssignmentDiffShowerMock();
            useCase.UpdateLocationToolAssignment(diffs, new LocationToolAssignmentErrorHandlerMock(), diffShower);
            diffShower.SaveActionParameter();
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void UpdateLocationToolAssignmentSetsUserBeforeCallsDataAccessUpdateLocationToolAssignment()
        {
            var dataAccess = CreateData();

            var userGetter = new UserGetterMock();
            var user = CreateUser.Anonymous();
            userGetter.NextReturnedUser = user; 

            var useCase = CreateUseCase(dataAccess, CreateGui(), userGetter);
            var diffShower = new LocationToolAssignmentDiffShowerMock();
            useCase.UpdateLocationToolAssignment(new List<LocationToolAssignmentDiff>()
            {
                new LocationToolAssignmentDiff() { NewLocationToolAssignment = new LocationToolAssignment() { Id = new LocationToolAssignmentId(1) } },
                new LocationToolAssignmentDiff() { NewLocationToolAssignment = new LocationToolAssignment() { Id = new LocationToolAssignmentId(1) } }
            }, new LocationToolAssignmentErrorHandlerMock(), diffShower);
            diffShower.SaveActionParameter();

            Assert.AreSame(user, dataAccess.UpdateLocationToolAssignmentParameter[0].User);
            Assert.AreSame(user, dataAccess.UpdateLocationToolAssignmentParameter[1].User);
        }
        
        [Test]
        public void UpdateLocationToolAssignmentsPassesDataFromDataAccessToGui()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);
            data.LoadLocationToolAssignmentsReturnValue = new List<LocationToolAssignment>();
            useCase.UpdateLocationToolAssignment(new List<LocationToolAssignmentDiff>()
                {
                    new LocationToolAssignmentDiff() { NewLocationToolAssignment = new LocationToolAssignment() { Id = new LocationToolAssignmentId(1) } },
                    new LocationToolAssignmentDiff() { NewLocationToolAssignment = new LocationToolAssignment() { Id = new LocationToolAssignmentId(1) } }
                });
            Assert.AreSame(data.LoadLocationToolAssignmentsReturnValue, gui.LoadLocationToolAssignmentsParameter);
        }


        [Test]
        public void LoadLocationToolAssignmentsPassesDataFromDataAccessToGui()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);
            data.LoadLocationToolAssignmentsReturnValue = new List<LocationToolAssignment>();
            useCase.LoadLocationToolAssignments();
            Assert.AreSame(data.LoadLocationToolAssignmentsReturnValue, gui.LoadLocationToolAssignmentsParameter);
        }

        [Test]
        public void LoadLocationToolAssignmentsHandlesError()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);
            data.LoadLocationToolAssignmentsThrowsError = true;
            useCase.LoadLocationToolAssignments();
            Assert.IsTrue(gui.ShowLocationToolAssignmentErrorCalled);
        }

        [TestCase(1)]
        [TestCase(50)]
        public void AddTestConditionsCalculatesTestDate(long idVal)
        {
            var data = CreateData();
            var gui = CreateGui();
            var testDateCalculationUseCase = new TestDateCalculationUseCaseMock();
            var useCase = CreateUseCase(data, gui, testDateCalculationUseCase:testDateCalculationUseCase);
            var id = new LocationToolAssignmentId(idVal);
            var assignment = new LocationToolAssignment() { Id = id };
            useCase.AddTestConditions(assignment);

            if(FeatureToggles.FeatureToggles.TestDateCalculation)
            {
                Assert.AreEqual(1, testDateCalculationUseCase.CalculateToolTestDateForParamter.Count);
                Assert.IsTrue(testDateCalculationUseCase.CalculateToolTestDateForParamter.Contains(id));
            }
            else
            {
                Assert.IsNull(testDateCalculationUseCase.CalculateToolTestDateForParamter);
            }
        }

        [TestCase(1)]
        [TestCase(50)]
        public void UpdateLocationToolAssignmentCalculatesTestDate(long idVal)
        {
            var data = CreateData();
            var gui = CreateGui();
            var testDateCalculationUseCase = new TestDateCalculationUseCaseMock();
            var useCase = CreateUseCase(data, gui, testDateCalculationUseCase: testDateCalculationUseCase);
            var id = new LocationToolAssignmentId(idVal);
            var assignment = new LocationToolAssignment() { Id = id };
            var diffShower = new LocationToolAssignmentDiffShowerMock();
            useCase.UpdateLocationToolAssignment(new List<LocationToolAssignmentDiff>
            {
                new LocationToolAssignmentDiff() { OldLocationToolAssignment = assignment, NewLocationToolAssignment = assignment }
            }, new LocationToolAssignmentErrorHandlerMock(), diffShower);
            diffShower.SaveActionParameter();

            if (FeatureToggles.FeatureToggles.TestDateCalculation)
            {
                Assert.AreEqual(1, testDateCalculationUseCase.CalculateToolTestDateForParamter.Count);
                Assert.IsTrue(testDateCalculationUseCase.CalculateToolTestDateForParamter.Contains(id));
            }
            else
            {
                Assert.IsNull(testDateCalculationUseCase.CalculateToolTestDateForParamter);
            }
        }

        [Test]
        public void UpdateLocationToolAssignmentCallsErrorHandler()
        {
            var data = CreateData();
            var gui = CreateGui();
            var useCase = CreateUseCase(data, gui);
            var errorHandler = new LocationToolAssignmentErrorHandlerMock();
            data.UpdateLocationToolAssignmentThrowsException = true;
            useCase.UpdateLocationToolAssignment(new List<LocationToolAssignmentDiff>() { new LocationToolAssignmentDiff()
            {
                OldLocationToolAssignment = new LocationToolAssignment() { TestLevelNumberChk = 2 },
                NewLocationToolAssignment = new LocationToolAssignment() { TestLevelNumberChk = 3 }
            }}, errorHandler);
            Assert.IsTrue(errorHandler.ShowLocationToolAssignmentErrorCalled);
        }


        private LocationToolAssignmentDataMock CreateData()
        {
            return new LocationToolAssignmentDataMock();
        }

        private LocationToolAssignmentGuiMock CreateGui()
        {
            return new LocationToolAssignmentGuiMock();
        }

        private ILocationToolAssignmentUseCase CreateUseCase(ILocationToolAssignmentData data,
            ILocationToolAssignmentGui gui, ISessionInformationUserGetter userGetter = null, INotificationManager notificationManager = null, ITestDateCalculationUseCase testDateCalculationUseCase = null)
        {
            return new LocationToolAssignmentUseCase(data, gui, userGetter ?? new UserGetterMock(), notificationManager ?? new NotificationManagerMock(), testDateCalculationUseCase ?? new TestDateCalculationUseCaseMock());
        }
    }
}