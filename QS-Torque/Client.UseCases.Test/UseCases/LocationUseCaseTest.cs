using Core.Diffs;
using Core.Entities;
using Core.UseCases;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Client.TestHelper.Factories;
using Client.TestHelper.Mock;
using Client.UseCases.Test.UseCases;
using Core.Entities.ReferenceLink;
using TestHelper.Factories;
using TestHelper.Mock;

namespace Core.Test.UseCases
{
    class ToolDataAccessMock : IToolData
    {
        public Tool AddTool(Tool newTool, User byUser)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<List<Tool>> LoadTools()
        {
            throw new NotImplementedException();
        }

        public string LoadComment(Tool tool)
        {
            throw new NotImplementedException();
        }

        public Picture LoadPictureForTool(Tool tool)
        {
            throw new NotImplementedException();
        }

        public List<ToolModel> LoadModelsWithAtLeasOneTool()
        {
            throw new NotImplementedException();
        }

        public List<Tool> LoadToolsForModel(ToolModel toolModel)
        {
            throw new NotImplementedException();
        }

        public bool IsSerialNumberUnique(string serialNumber)
        {
            throw new NotImplementedException();
        }

        public bool IsInventoryNumberUnique(string inventoryNumber)
        {
            throw new NotImplementedException();
        }

        public void RemoveTool(Tool tool, User byUser)
        {
            throw new NotImplementedException();
        }

        public Tool UpdateTool(ToolDiff diff)
        {
            UpdateToolParameters.Add(diff);
            if (SetNewToolStatesThrowsException)
            {
                throw new Exception();
            }
            UpdateToolCallCount++;
            return null;
        }

        public int UpdateToolCallCount { get; set; }
        public bool SetNewToolStatesThrowsException { get; set; }
        public List<ToolDiff> UpdateToolParameters { get; private set; } = new List<ToolDiff>();

        public List<LocationToolAssignmentReferenceLink> LoadLocationToolAssignmentLinksForToolId(ToolId toolId)
        {
            throw new NotImplementedException();
        }
    }
    [Parallelizable(ParallelScope.Children)]
    class LocationUseCaseTest
    {
        [Test]
        public void LoadingTreeFetchesLocationsFromDataAccess()
        {
            var gui = CreateGuiMock();
            var dataAccess = CreateDataAccessMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess);

            dataAccess.LoadLocationsReturnValue = new List<Location>
            {
                CreateLocation.WithIdNumberDescription(7658490, "75849302rfgojdknbv,c", "98tzhvjncm,d.ö")
            };
            dataAccess.LoadDirectoriesReturnValue = new List<LocationDirectory>();
            useCase.LoadTree(gui);
            Assert.AreEqual(1, gui.ShowLocationTreeCallCount);
            Assert.IsTrue(dataAccess.LoadLocationWasCalled);
        }

        [Test]
        public void LoadingTreeFetchesLocationDirectoriesFromDataAccess()
        {
            var gui = CreateGuiMock();
            var dataAccess = CreateDataAccessMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess);

            dataAccess.LoadLocationsReturnValue = new List<Location>()
            {
                CreateLocation.WithIdNumberDescription(7658490, "75849302rfgojdknbv,c", "98tzhvjncm,d.ö")
            };
            dataAccess.LoadDirectoriesReturnValue = new List<LocationDirectory>();
            useCase.LoadTree(gui);
            Assert.AreEqual(1, gui.ShowLocationTreeCallCount);
            Assert.AreEqual(0, gui.ShowLocationTreeErrorCallCount);
            Assert.IsTrue(dataAccess.LoadDirectoriesWasCalled);
        }

        [Test]
        public void LoadLocationTreeCallsShowLocationForEachLocation()
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess, new UserGetterMock(), null);
            dataAccess.LoadLocationsReturnValue = new List<Location>
            {
                CreateLocation.IdOnly(15)
            };
            dataAccess.LoadDirectoriesReturnValue = new List<LocationDirectory>();
            useCase.LoadTree(gui);
            Assert.AreEqual(1, gui.ShowLocationCallCount);
            Assert.AreEqual(15, gui.ShowLocationParameter.Id.ToLong());
        }

        [Test]
        public void GuiShowsErrorIfLoadTreeFails()
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess);
            dataAccess.ThrowLoadDirectoriesException = true;
            useCase.LoadTree(gui);
            Assert.AreEqual(1, gui.ShowLocationTreeErrorCallCount);
            Assert.AreEqual(0, gui.ShowLocationTreeCallCount);
            Assert.Pass();
        }

        #region RemoveLocation

        [Test]
        public void RemoveLocationCallsDataLoadLocationToolAssignmentsForLocation()
        {
            var dataAccess = CreateDataAccessMock();
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataMock();
            var gui = CreateGuiMock();
            var useCase =
                CreateLocationUseCaseParametrized(gui, dataAccess, null, null, locationToolAssignmentDataAccess);
            useCase.RemoveLocation(CreateLocation.Anonymous());
            Assert.AreEqual(1, locationToolAssignmentDataAccess.LoadAssignedToolsForLocationCallCount);
        }

        [Test]
        public void RemoveLocationCallsDataRemoveLocationIfLoadLocationToolAssignmentsForLocationReturnsNull()
        {
            var dataAccess = CreateDataAccessMock();
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataMock();
            var gui = CreateGuiMock();
            var useCase =
                CreateLocationUseCaseParametrized(gui, dataAccess, null, null, locationToolAssignmentDataAccess);
            locationToolAssignmentDataAccess.LoadAssignedToolsForLocationReturn = null;
            useCase.RemoveLocation(CreateLocation.Anonymous());
            Assert.AreEqual(1, locationToolAssignmentDataAccess.LoadAssignedToolsForLocationCallCount);
        }

        [Test]
        public void RemoveLocationCallsDataRemoveLocationIfLoadLocationToolAssignmentsForLocationReturnsCountZero()
        {
            var dataAccess = CreateDataAccessMock();
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataMock();
            var gui = CreateGuiMock();
            var useCase =
                CreateLocationUseCaseParametrized(gui, dataAccess, null, null, locationToolAssignmentDataAccess);
            locationToolAssignmentDataAccess.LoadAssignedToolsForLocationReturn = new List<LocationToolAssignment>();
            useCase.RemoveLocation(CreateLocation.Anonymous());
            Assert.AreEqual(1, locationToolAssignmentDataAccess.LoadAssignedToolsForLocationCallCount);
        }

        [Test]
        public void RemoveLocationCallsGuiShowRemoveLocationErrorOnError()
        {
            var dataAccess = CreateDataAccessMock();
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataMock();
            var gui = CreateGuiMock();
            var useCase =
                CreateLocationUseCaseParametrized(gui, dataAccess, null, null, locationToolAssignmentDataAccess);
            locationToolAssignmentDataAccess.LoadAssignedToolsForLocationThrowsException = true;
            useCase.RemoveLocation(CreateLocation.Anonymous());
            Assert.AreEqual(1, gui.ShowRemoveLocationErrorCallCount);
        }

        [Test]
        public void RemoveLocationCallsGuiShowChangeToolStatusDialogIfLocationToolAssignmentsIsNotNullOrCountZero()
        {
            var dataAccess = CreateDataAccessMock();
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataMock();
            var gui = CreateGuiMock();
            var useCase =
                CreateLocationUseCaseParametrized(gui, dataAccess, null, null, locationToolAssignmentDataAccess);
            var locationToolAssignments = new List<LocationToolAssignment>
            {
                CreateLocationToolAssignment.Anonymous(),
                CreateLocationToolAssignment.Anonymous()
        };
            locationToolAssignmentDataAccess.LoadAssignedToolsForLocationReturn = locationToolAssignments;
            useCase.RemoveLocation(CreateLocation.Anonymous());
            Assert.AreEqual(1, gui.ShowChangeToolStatusDialogCallCount);
        }

        [Test]
        public void RemoveLocationCallsDataRemoveLocationIfGuiShowChangeToolStatusInvokesOnSuccess()
        {
            var dataAccess = CreateDataAccessMock();
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataMock();
            var gui = CreateGuiMock();
            var useCase =
                CreateLocationUseCaseParametrized(gui, dataAccess, null, null, locationToolAssignmentDataAccess);
            var locationToolAssignments = new List<LocationToolAssignment>
            {
                CreateLocationToolAssignment.Anonymous(),
                CreateLocationToolAssignment.Anonymous()
        };
            locationToolAssignmentDataAccess.LoadAssignedToolsForLocationReturn = locationToolAssignments;
            gui.ShowChangeToolStatusDialogCallsOnSuccess = true;
            var location = CreateLocation.Anonymous(); 
            useCase.RemoveLocation(location);
            Assert.AreEqual(location, dataAccess.RemovedLocation);
        }

        [Test]
        public void RemoveLocationCallsDataRemoveLocationToolAssignment()
        {
            var dataAccess = CreateDataAccessMock();
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataMock();
            var gui = CreateGuiMock();
            var useCase =
                CreateLocationUseCaseParametrized(gui, dataAccess, null, null, locationToolAssignmentDataAccess);
            var locationToolAssignments = new List<LocationToolAssignment>
            {
                CreateLocationToolAssignment.Anonymous(),
                CreateLocationToolAssignment.Anonymous()
        };
            locationToolAssignmentDataAccess.LoadAssignedToolsForLocationReturn = locationToolAssignments;
            gui.ShowChangeToolStatusDialogCallsOnSuccess = true;
            useCase.RemoveLocation(CreateLocation.Anonymous());
            Assert.AreEqual(locationToolAssignments.Count, locationToolAssignmentDataAccess.RemoveLocationToolAssignmentCallCount);
        }

        [Test]
        [TestCase(15)]
        [TestCase(25)]
        public void RemoveLocationCallsGuiRemoveLocation(long locationId)
        {
            var dataAccess = CreateDataAccessMock();
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataMock();
            var gui = CreateGuiMock();
            var useCase =
                CreateLocationUseCaseParametrized(gui, dataAccess, null, null, locationToolAssignmentDataAccess);
            var locationToolAssignments = new List<LocationToolAssignment>
            {
                CreateLocationToolAssignment.Anonymous(),
                CreateLocationToolAssignment.Anonymous()
            };
            locationToolAssignmentDataAccess.LoadAssignedToolsForLocationReturn = locationToolAssignments;
            gui.ShowChangeToolStatusDialogCallsOnSuccess = true;
            var location = CreateLocation.IdOnly(locationId);
            useCase.RemoveLocation(location);
            Assert.AreEqual(location, gui.RemoveLocationParameter);
        }

        [Test]
        public void RemoveLocationWithNullLocationThrowsArgumentNullException()
        {
            var dataAccess = CreateDataAccessMock();
            var useCase =
                CreateLocationUseCaseParametrized(null, dataAccess, null, null, null);
            Assert.Throws<ArgumentNullException>(() =>  useCase.RemoveLocation(null));
        }
        #endregion

        #region RemoveDirectory

        [Test]
        public void RemoveDirectoryWithNullLocationDirectoryThrowsArgumentNullException()
        {
            var dataAccess = CreateDataAccessMock();
            var useCase = CreateLocationUseCaseParametrized(null, dataAccess, null, null, null);
            Assert.Throws<ArgumentNullException>(() => useCase.RemoveDirectory(null));
        }

        [Test]
        public void RemoveDirectoryCallsShowRemoveDirectoryErrorOnException()
        {
            //Arrange
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess, null, null, locationToolAssignmentDataAccess);
            var location = CreateLocation.Anonymous();
            var locationParentDirectoryId = new LocationDirectoryId(15);
            location.ParentDirectoryId = locationParentDirectoryId;
            locationToolAssignmentDataAccess.LoadAssignedToolsForLocationReturn = new List<LocationToolAssignment> { CreateLocationToolAssignment.WithLocation(location) };
            dataAccess.AddLocationReturnValue = location;
            useCase.AddLocation(location);

            gui.ShowChangeToolStatusDialogThrowsException = true;
            //Act
            useCase.RemoveDirectory(new LocationDirectory{Id = locationParentDirectoryId});
            //Assert
            Assert.AreEqual(1, gui.ShowRemoveDirectoryErrorCallCount);
        }

        [Test]
        public void RemoveDirectoryCallsShowChangeToolStatusDialog()
        {
            //Arrange
            
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess, null, null, locationToolAssignmentDataAccess, null);
            var location = CreateLocation.Anonymous();
            var locationParentDirectoryId = new LocationDirectoryId(15);
            location.ParentDirectoryId = locationParentDirectoryId;
            locationToolAssignmentDataAccess.LoadAssignedToolsForLocationReturn = new List<LocationToolAssignment>{CreateLocationToolAssignment.WithLocation(location)};
            dataAccess.AddLocationReturnValue = location;
            useCase.AddLocation(location);

            //Act
            
            useCase.RemoveDirectory(new LocationDirectory{Id = locationParentDirectoryId});
            
            //Assert

            Assert.AreEqual(1, gui.ShowChangeToolStatusDialogCallCount);
        }

        [Test]
        public void RemoveDirectoryCallsLoadLocationReferencesForLocation()
        {
            var dataAccess = CreateDataAccessMock();
            var locationToolAssingmentDataAccess = new LocationToolAssignmentDataMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess, null, null, locationToolAssingmentDataAccess, null);
            var location = CreateLocation.IdOnly(15);
            location.ParentDirectoryId = new LocationDirectoryId(15);
            dataAccess.AddLocationReturnValue = location;
            useCase.AddLocation(location);
            var locationDirectory = new LocationDirectory();
            locationDirectory.Id = new LocationDirectoryId(15);
            useCase.RemoveDirectory(locationDirectory);
            Assert.AreEqual(1, locationToolAssingmentDataAccess.LoadAssignedToolsForLocationCallCount);
        }

        [Test]
        public void RemoveDirectoryCallsRemoveLocationToolAssignmentOnSuccess()
        {
            var dataAccess = CreateDataAccessMock();
            var locationToolAssingmentDataAccess = new LocationToolAssignmentDataMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess, null, null, locationToolAssingmentDataAccess, null);
            var location = CreateLocation.IdOnly(15);
            location.ParentDirectoryId = new LocationDirectoryId(15);
            dataAccess.AddLocationReturnValue = location;
            useCase.AddLocation(location);
            var locationDirectory = new LocationDirectory();
            locationDirectory.Id = new LocationDirectoryId(15);
            gui.ShowChangeToolStatusDialogCallsOnSuccess = true;
            locationToolAssingmentDataAccess.LoadAssignedToolsForLocationReturn = new List<LocationToolAssignment>{ CreateLocationToolAssignment.Anonymous()};
            useCase.RemoveDirectory(locationDirectory);
            Assert.AreEqual(1, locationToolAssingmentDataAccess.RemoveLocationToolAssignmentCallCount);
        }

        [Test]
        public void RemoveDirectoryCallsRemoveLocationOnSuccess()
        {
            var dataAccess = CreateDataAccessMock();
            var locationToolAssingmentDataAccess = new LocationToolAssignmentDataMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess, null, null, locationToolAssingmentDataAccess, null);
            var location = CreateLocation.IdOnly(15);
            location.ParentDirectoryId = new LocationDirectoryId(15);
            dataAccess.AddLocationReturnValue = location;
            useCase.AddLocation(location);
            var locationDirectory = new LocationDirectory();
            locationDirectory.Id = new LocationDirectoryId(15);
            gui.ShowChangeToolStatusDialogCallsOnSuccess = true;
            locationToolAssingmentDataAccess.LoadAssignedToolsForLocationReturn = new List<LocationToolAssignment> { CreateLocationToolAssignment.Anonymous()};
            useCase.RemoveDirectory(locationDirectory);
            Assert.AreEqual(1, dataAccess.RemoveLocationCallCount);
        }

        [Test]
        public void RemoveDirectoryCallsGuiRemoveLocationOnSuccess()
        {
            var dataAccess = CreateDataAccessMock();
            var locationToolAssingmentDataAccess = new LocationToolAssignmentDataMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess, null, null, locationToolAssingmentDataAccess, null);
            var location = CreateLocation.IdOnly(15);
            location.ParentDirectoryId = new LocationDirectoryId(15);
            dataAccess.AddLocationReturnValue = location;
            useCase.AddLocation(location);
            var locationDirectory = new LocationDirectory();
            locationDirectory.Id = new LocationDirectoryId(15);
            gui.ShowChangeToolStatusDialogCallsOnSuccess = true;
            locationToolAssingmentDataAccess.LoadAssignedToolsForLocationReturn = new List<LocationToolAssignment> { CreateLocationToolAssignment.Anonymous()};
            useCase.RemoveDirectory(locationDirectory);
            Assert.AreEqual(1, gui.RemoveLocationCallCount);
        }

        [Test]
        [TestCase(15)]
        [TestCase(42)]
        public void RemoveDirectoryCallsDataRemoveDirectoryOnSuccessWithCorrectParameter(long directoryId)
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess, null, null, null, null);
            gui.ShowChangeToolStatusDialogCallsOnSuccess = true;
            var locationDirectory = new LocationDirectory{Id = new LocationDirectoryId(directoryId) };    
            useCase.RemoveDirectory(locationDirectory);
            Assert.AreEqual(locationDirectory, dataAccess.RemoveDirectoryParameter);
        }

        private static readonly IEnumerable<List<LocationDirectory>> RemoveDirectoryNestedDirectorys = new List<List<LocationDirectory>>
        {
            new List<LocationDirectory>
            {
                CreateLocationDirectory.Parameterized(1, "root", null),
                CreateLocationDirectory.Parameterized(2, "layer1.1", 1),
                CreateLocationDirectory.Parameterized(3, "layer1.2", 1),
                CreateLocationDirectory.Parameterized(4, "layer2.1", 2),
                CreateLocationDirectory.Parameterized(5, "layer2.2", 2),
            },
            new List<LocationDirectory>
            {
                CreateLocationDirectory.Parameterized(1, "root", null),
                CreateLocationDirectory.Parameterized(2, "layer1.1", 1),
                CreateLocationDirectory.Parameterized(3, "layer1.2", 1),
                CreateLocationDirectory.Parameterized(4, "layer2.1", 2),
                CreateLocationDirectory.Parameterized(5, "layer2.2", 2),
                CreateLocationDirectory.Parameterized(6, "layer3.1", 4),
                CreateLocationDirectory.Parameterized(7,"layer3.2", 4),
                CreateLocationDirectory.Parameterized(8, "layer4.1", 6),
                CreateLocationDirectory.Parameterized(9, "layer4.2",7)
            }
        };

        [Test]
        [TestCaseSource(nameof(RemoveDirectoryNestedDirectorys))]
        public void RemoveDirectoryWithNestedDirectorysCallsDataRemoveDirectoryWithAllDirectories(List<LocationDirectory> nestedDirectories)
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess, null, null, null);
            gui.ShowChangeToolStatusDialogCallsOnSuccess = true;
            dataAccess.LoadDirectoriesReturnValue = nestedDirectories;
            useCase.LoadTree(gui);
            useCase.RemoveDirectory(dataAccess.LoadDirectoriesReturnValue.FirstOrDefault(x => x.ParentId is null || x.ParentId?.ToLong() == 0));
            Assert.AreEqual(nestedDirectories.Count, dataAccess.RemoveDirectoryCallCount);
        }

        [Test]
        [TestCase(15)]
        [TestCase(42)]
        public void RemoveDirectoryCallsGuiRemoveDirectoryOnSuccessWithCorrectParameter(long directoryId)
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess, null, null, null, null);
            gui.ShowChangeToolStatusDialogCallsOnSuccess = true;
            var locationDirectory = new LocationDirectory { Id = new LocationDirectoryId(directoryId) };
            useCase.RemoveDirectory(locationDirectory);
            Assert.AreEqual(locationDirectory.Id, gui.RemoveDirectoryParameter);
        }

        [Test]
        public void RemoveDirectoryNotCallsShowChangeToolStatusDialogWhenLocationToolAssignmentIsEmpty()
        {
            //Arange
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess, null, null, null, null);
            //Act
            useCase.RemoveDirectory(new LocationDirectory{Id = new LocationDirectoryId(15)});

            //Assert
            Assert.AreEqual(1, dataAccess.RemoveDirectoryCallCount);

        }

        #endregion

        [TestCase(1,99)]
        [TestCase(12, 55)]
        public void RemoveLocationCallsDataWithCorrectParameter(long locid, long userid)
        {
            var dataAccess = CreateDataAccessMock();

            var gui = CreateGuiMock();
            var userMock = CreateUserGetterWithUser(userid);
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess, userMock, null, new LocationToolAssignmentDataMock());

            var location = CreateLocation.IdOnly(locid);

            useCase.RemoveLocation(location, null);

            Assert.AreEqual(1, gui.RemoveLocationCallCount);
            Assert.AreEqual(location, dataAccess.RemovedLocation);
            Assert.AreEqual(userid, dataAccess.RemovedLocationParameterUser.UserId.ToLong());
        }

        [Test]
        public void RemoveLocationCallsGuiWithCorrectLocation()
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess, null, null, new LocationToolAssignmentDataMock());

            var location = CreateLocation.Anonymous();
            useCase.RemoveLocation(location, null);
            Assert.AreEqual(1, gui.RemoveLocationCallCount);
            Assert.AreEqual(location, gui.RemoveLocationParameter);
        }

        [Test]
        public void RemoveLocationWithErrorShowsGuiError()
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess);


            var location = CreateLocation.Anonymous();
            dataAccess.RemoveLocationThrowsException = true;

            useCase.RemoveLocation(location, null);
            Assert.AreEqual(1, gui.ShowRemoveLocationErrorCallCount, null);
            Assert.AreEqual(0, gui.RemoveLocationCallCount);
            Assert.Pass();
        }

        [Test]
        public void RemoveLocationWithLocationToolAssignmentCallsRemoveLocationToolAssignment()
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var locationToolAssignmentDataMock = new LocationToolAssignmentDataMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess,null,null, locationToolAssignmentDataMock);
            var locationToolAssignment = new LocationToolAssignment
                {AssignedLocation = CreateLocation.Anonymous(), AssignedTool = CreateTool.Anonymous()};

            useCase.RemoveLocation(CreateLocation.Anonymous(), new List<LocationToolAssignment>{locationToolAssignment});
            Assert.AreEqual(1, locationToolAssignmentDataMock.RemoveLocationToolAssignmentCallCount);
        }

        [Test]
        [TestCase(15)]
        [TestCase(26)]
        public void UpdateLocationCallsGetLocationToolAssignmentIdsByLocationId(long id)
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess);

            var location = CreateLocation.IdOnly(id);
            useCase.UpdateLocation(new LocationDiff {NewLocation = location, OldLocation = location});
            Assert.AreEqual(1, dataAccess.GetLocationToolAssignmentIdsByLocationIdCallCounter);
            Assert.AreEqual(id, dataAccess.GetLocationToolAssignmentIdsByLocationIdParameter.ToLong());
        }

        [Test]
        public void UpdateLocationCallsShowChangeLocationToolAssignmentNotice()
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess);

            var location = CreateLocation.Anonymous();
            dataAccess.GetLocationToolAssignmentIdsByLocationIdReturn = new List<long> {15};
            useCase.UpdateLocation(new LocationDiff{NewLocation = location, OldLocation = location});
            Assert.AreEqual(1, gui.ShowChangeLocationToolAssignmentNoticeCallCount);
        }

        [Test]
        public void AddLocationUpdatesToleranceLimitsOnEntity()
        {
            var location = new LocationMock();
            var useCase = CreateLocationUseCaseParametrized(CreateGuiMock(), CreateDataAccessMock(), null, null, null,
                null, new NotificationManagerMock());

            useCase.AddLocation(location);

            Assert.AreEqual(1, location.UpdateToleranceLimitsCallCount);
            Assert.Pass();
        }

        [TestCase(1, 6)]
        [TestCase(3, 99)]
        public void AddLocationCallsAddLocationOnDataAccessWithCorrectParameter(long locid, long userid)
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var userMock = CreateUserGetterWithUser(userid);
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess, userMock);
            var location = CreateLocation.IdOnly(locid);

            useCase.AddLocation(location);

            Assert.AreEqual(1,dataAccess.AddLocationCallCount);
            Assert.AreEqual(location, dataAccess.AddLocationParameter);
            Assert.AreEqual(userid, dataAccess.AddLocationParameterUser.UserId.ToLong());
        }

        [Test]
        public void AddLocationCallsAddLocationOnGuiWithCorrectParameter()
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess);
            var location = CreateLocation.Anonymous();

            dataAccess.AddLocationReturnValue = location;
            useCase.AddLocation(location);

            Assert.AreEqual(1, gui.AddLocationCallCount);
            Assert.AreEqual(location, gui.AddLocationParameter);
        }

        [Test]
        public void GuiShowsErrorIfAddLocationFails()
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess);
            dataAccess.ThrowAddLocationException = true;

            useCase.AddLocation(null);
            Assert.AreEqual(1, gui.AddLocationErrorCallCount);
            Assert.AreEqual(0, gui.AddLocationCallCount);
            Assert.Pass();
        }

        [Test]
        [TestCase(5)]
        [TestCase(49857)]
        public void LoadingPictureForLocationIdCallsDataAccessWithCorrectId(long id)
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess);
            useCase.LoadPictureForLocation(new LocationId(id));
            Assert.AreEqual(1, dataAccess.LoadPictureForLocationCallCount);
            Assert.AreEqual(id, dataAccess.LoadPictureForLocationParameter.ToLong());
        }

        [Test]
        [TestCase(15,15)]
        [TestCase(29,25)]
        public void LoadingPictureForLocationCallsGuiAndReturnsCorrectPictureAndCorrectLocationId(long pictureId, long locationId)
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess);
            var picture = new Picture { SeqId = pictureId };
            dataAccess.LoadPictureForLocationReturn = picture;
            useCase.LoadPictureForLocation(new LocationId(locationId));
            Assert.AreEqual(1, gui.ShowPictureForLocationCallCount);
            Assert.AreEqual(pictureId, gui.ShowPictureForLocationPictureParameter.SeqId);
            Assert.AreEqual(locationId, gui.ShowPictureForLocationLocationIdParameter.ToLong());
        }

        [TestCase("4hbfvcn.eörtij", true)]
        [TestCase(" /98754z1gdsyuikr", false)]
        public void IsNumberUniqueTest(string number, bool unique)
        {
            var dataAccess = CreateDataAccessMock();
            var useCase = CreateLocationUseCaseParametrized(null, dataAccess, null);
            dataAccess.IsNumberUniqueReturnValue = unique;

            Assert.AreEqual(unique, useCase.IsNumberUnique(number));
            Assert.IsTrue(dataAccess.WasIsNumberUniqueInvoked);
            Assert.AreEqual(number, dataAccess.IsNumberUniqueParameter);
        }

        [Test]
        [TestCase(5)]
        [TestCase(49857)]
        public void LoadingCommentForLocationIdCallsDataAccessWithCorrectId(long id)
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess);
            useCase.LoadCommentForLocation(new LocationId(id));

            Assert.AreEqual(1, dataAccess.LoadCommentForLocationCallCount);
            Assert.AreEqual(id, dataAccess.LoadCommentForLocationParameter.ToLong());
        }

        [Test]
        [TestCase( 15,"TesDing1")]
        [TestCase( 26, "TestDing2")]
        public void LoadingCommentForLocationCallsGuiAndReturnsCorrectCommentAndCorrectLocationId(long locationId,
            string comment)
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess);
            dataAccess.LoadCommentForLocationReturn = comment;
            useCase.LoadCommentForLocation(new LocationId(locationId));
            Assert.AreEqual(1, gui.ShowCommentForLocationCallCount);
            Assert.AreEqual(comment, gui.ShowCommentForLocationCommentParameter);
            Assert.AreEqual(locationId, gui.ShowCommentForLocationLocationIdParameter.ToLong());
        }


        [Test]
        [TestCase(1, "TestName", 4)]
        [TestCase(2, "Baum", 99)]
        public void AddLocationDirectoryCallsDataAccessWithCorrectParameter(long dirId ,string name, long userid)
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var userMock = CreateUserGetterWithUser(userid);
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess, userMock);
            useCase.AddLocationDirectory(new LocationDirectoryId(dirId), name);
            Assert.AreEqual(1, dataAccess.AddLocationFolderCallCount);
            Assert.AreEqual(dirId, dataAccess.AddLocationFolderParameterId.ToLong());
            Assert.AreEqual(name, dataAccess.AddLocationFolderParameterName);
            Assert.AreEqual(userid, dataAccess.AddLocationDirectoyParameterUser.UserId.ToLong());
        }

        [Test]
        [TestCase(15)]
        [TestCase(26)]
        public void AddLocationDirectoryCallsGuiAddLocationWithCorrectLocationDirectory(long id)
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess);
            dataAccess.AddLocationFolderReturn = new LocationDirectory {Id = new LocationDirectoryId(id)};
            useCase.AddLocationDirectory(new LocationDirectoryId(0), String.Empty);
            Assert.AreEqual(1, gui.AddLocationDirectoryCallCount);
            Assert.AreEqual(1, gui.AddLocationDirectoryCallCount);
            Assert.AreEqual(id, gui.AddLocationDirectoryParameter.Id.ToLong());
        }

        [Test]
        public void AddLocationDirectoryDataAccessErrorCallsGuiShowAddLocationError()
        {
            var dataAccess = CreateDataAccessMock();
            var gui = CreateGuiMock();
            var useCase = CreateLocationUseCaseParametrized(gui, dataAccess);
            dataAccess.AddLocationFolderThrowsException = true;
            useCase.AddLocationDirectory(new LocationDirectoryId(0), String.Empty);
            Assert.AreEqual(1, gui.ShowAddLocationDirectoryErrorCallCount);
        }
        
        [Test]
        public void RequestLocationUpdateCallsUpdateLocationOnDataAccessIfVerifyIsTrueWidthCorrectParameter()
        {
            var gui = CreateGuiMock();
            var data = CreateDataAccessMock();
            var useCase = CreateLocationUseCaseParametrized(gui, data);
            gui.VerifyLocationDiffReturnValue = true;
            var oldLocation = CreateLocation.Anonymous();
            var newLocation = CreateLocation.Anonymous();
            newLocation.Description = new LocationDescription("gthzur8i943o0wpleödfkjhvbjgksefoiaß0rrt8pu8gjke,d");

            useCase.UpdateLocation(new LocationDiff() { OldLocation = oldLocation, NewLocation = newLocation });
            Assert.AreEqual(1, data.UpdateLocationCallCount);
            Assert.AreEqual(0, gui.UpdateLocationErrorCallCount);
            Assert.AreEqual(newLocation, data.UpdateLocationParameter);
        }

        [Test]
        public void RequestLocationUpdateCallsMethodOnGuiWidthCorrectParameter()
        {
            var gui = CreateGuiMock();
            var data = CreateDataAccessMock();
            var useCase = CreateLocationUseCaseParametrized(gui, data);
            gui.VerifyLocationDiffReturnValue = true;
            data.UpdateLocationReturnValue = CreateLocation.NumberOnly("789456");

            var newLocation = CreateLocation.Anonymous();
            newLocation.Number = new LocationNumber("hzgutrioel");
            newLocation.Description = new LocationDescription("trueiowp");
            newLocation.ToleranceClassTorque = new ToleranceClass();
            newLocation.ToleranceClassAngle = new ToleranceClass();

            useCase.UpdateLocation(new LocationDiff() { NewLocation = newLocation, OldLocation = CreateLocation.Anonymous()});
            Assert.AreEqual(1, gui.UpdateLocationCallCount);
            Assert.AreEqual(0, gui.UpdateLocationErrorCallCount);
            Assert.AreEqual(data.UpdateLocationReturnValue, gui.UpdateLocationParameter);
        }

        [Test]
        public void UpdateLocationUpdatesToleranceLimits()
        {
            var gui = CreateGuiMock();
            var data = CreateDataAccessMock();
            var useCase = CreateLocationUseCaseParametrized(gui, data);
            var location = new LocationMock()
            {
                Number = new LocationNumber("874654"),
                Description = new LocationDescription("9587zhugjfmkd,l")
            };
            useCase.UpdateLocation(new LocationDiff() { NewLocation = location, OldLocation = location});
            Assert.AreEqual(1, location.UpdateToleranceLimitsCallCount);
            Assert.AreEqual(0, gui.UpdateLocationErrorCallCount);
            Assert.Pass();
        }

        [Test]
        public void UpdateLocationCallsMethodOnGuiOnError()
        {
            var gui = CreateGuiMock();
            var data = CreateDataAccessMock();
            var useCase = CreateLocationUseCaseParametrized(gui, data);
            data.ThrowsUpdateLocationError = true;
            gui.VerifyLocationDiffReturnValue = true;

            useCase.UpdateLocation(new LocationDiff() { NewLocation = CreateLocation.Anonymous() });
            Assert.AreEqual(1, gui.UpdateLocationErrorCallCount);
            Assert.AreEqual(0, gui.UpdateLocationCallCount);
            Assert.Pass();
        }

        [Test]
        public void UpdateLocationCallsMethodOnGuiIfNewNumberIsNotUnique()
        {
            var gui = CreateGuiMock();
            var data = CreateDataAccessMock();
            var useCase = CreateLocationUseCaseParametrized(gui, data);
            data.IsNumberUniqueReturnValue = false;

            useCase.UpdateLocation(new LocationDiff()
            {
                OldLocation = new LocationMock() { Number = new LocationNumber("48fjicmkx ") },
                NewLocation = new LocationMock() { Number = new LocationNumber("ghtfrj") }
            });
            Assert.AreEqual(1, gui.LocationAlreadyExistsCallCount);
        }

        [Test]
        public void UpdateLocationSetsUserOfDiff()
        {
            var gui = CreateGuiMock();
            var data = CreateDataAccessMock();
            var userGetter = new UserGetterMock();
            var useCase = CreateLocationUseCaseParametrized(gui, data, userGetter);
            var diff = new LocationDiff()
            {
                OldLocation = new LocationMock()
                {
                    Number = new LocationNumber("wehjr"),
                    Description = new LocationDescription("987654"),
                },
                NewLocation = new LocationMock()
                {
                    Number = new LocationNumber("ghtfrj"),
                    Description = new LocationDescription("tzuri9eold"),
                }
            };

            userGetter.NextReturnedUser = CreateUser.Anonymous();
            useCase.UpdateLocation(diff);
            Assert.AreEqual(1, data.UpdateLocationCallCount);
            Assert.AreEqual(userGetter.NextReturnedUser, diff.User);
        }


        [Test]
        [TestCase(15, 26, 6)]
        [TestCase(35, 46, 11)]
        public void ChangeLocationParentCallsDataAccessWithCorrectParameters(long locationId, long newParentId, long userid)
        {
            var gui = CreateGuiMock();
            var data = CreateDataAccessMock();
            var userMock = CreateUserGetterWithUser(userid);
            var useCase = CreateLocationUseCaseParametrized(gui, data, userMock);
            useCase.ChangeLocationParent(CreateLocation.IdOnly(locationId), new LocationDirectoryId(newParentId));
            Assert.AreEqual(1, data.ChangeLocationParentCallCount);
            Assert.AreEqual(locationId, data.ChangeLocationParentParameterLocation.Id.ToLong());
            Assert.AreEqual(newParentId, data.ChangeLocationParentParameterNewParentId);
            Assert.AreEqual(userid, data.ChangeLocationParentParameterUser.UserId.ToLong());
            
        }

        [Test]
        [TestCase(15, 26)]
        [TestCase(35, 46)]
        public void ChangeLocationParentCallsGui(long locationId, long newParentId)
        {
            var gui = CreateGuiMock();
            var data = CreateDataAccessMock();
            var userGetter = new UserGetterMock();
            var useCase = CreateLocationUseCaseParametrized(gui, data, userGetter);
            useCase.ChangeLocationParent(CreateLocation.IdOnly(locationId), new LocationDirectoryId(newParentId));
            Assert.AreEqual(1, gui.ChangeLocationParentCallCount);
        }

        [Test]
        public void ChangeLocationCallsChangeLocationParentError()
        {
            var gui = CreateGuiMock();
            var data = CreateDataAccessMock();
            var userGetter = new UserGetterMock();
            data.ChangeLocationParentThrowsException = true;
            var useCase = CreateLocationUseCaseParametrized(gui, data, userGetter);
            useCase.ChangeLocationParent(CreateLocation.IdOnly(15), new LocationDirectoryId(26));
            Assert.AreEqual(1, gui.ChangeLocationParentErrorCallCount);
        }

        [Test]
        [TestCase(15, 26, 5)]
        [TestCase(35, 46, 9)]
        public void ChangeLocationDirectoryParentCallsDataAccessWithCorrectParameters(long directoryId, long newParentId, long userid)
        {
            var gui = CreateGuiMock();
            var data = CreateDataAccessMock();
            var userMock = CreateUserGetterWithUser(userid);
            var useCase = CreateLocationUseCaseParametrized(gui, data, userMock);
            useCase.ChangeLocationDirectoryParent(CreateLocationDirectory.WithId(directoryId), new LocationDirectoryId(newParentId));
            Assert.AreEqual(1, data.ChangeLocationDirectoryParentCallCount);
            Assert.AreEqual(directoryId, data.ChangeLocationDirectoryParentParameterLocationDirectory.Id.ToLong());
            Assert.AreEqual(newParentId, data.ChangeLocationDirectoryParentParameterNewParentId);
            Assert.AreEqual(userid, data.ChangeLocationDirectoryParentParameterUser.UserId.ToLong());
        }

        [Test]
        [TestCase(15, 26)]
        [TestCase(35, 46)]
        public void ChangeLocationDirectoryParentCallsGui(long directoryId, long newParentId)
        {
            var gui = CreateGuiMock();
            var data = CreateDataAccessMock();
            var userGetter = new UserGetterMock();
            var useCase = CreateLocationUseCaseParametrized(gui, data, userGetter);
            useCase.ChangeLocationDirectoryParent(CreateLocationDirectory.WithId(directoryId), new LocationDirectoryId(newParentId));
            Assert.AreEqual(1, gui.ChangeLocationDirectoryParentCallCount);
        }

        [Test]
        public void ChangeLocationDirectoryCallsChangeLocationDirectoryParentError()
        {
            var gui = CreateGuiMock();
            var data = CreateDataAccessMock();
            var userGetter = new UserGetterMock();
            data.ChangeLocationDirectoryParentThrowsException = true;
            var useCase = CreateLocationUseCaseParametrized(gui, data, userGetter);
            useCase.ChangeLocationDirectoryParent(CreateLocationDirectory.WithId(15), new LocationDirectoryId(26));
            Assert.AreEqual(1, gui.ChangeLocationDirectoryParentErrorCallCount);
        }

        [Test]
        public void AddLocationDirectoryWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var useCase = CreateLocationUseCaseParametrized(CreateGuiMock(), CreateDataAccessMock(), null,null, null, null, notificationManager);
            useCase.AddLocationDirectory(new LocationDirectoryId(1), "Folder 1");
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void RemoveDirectoryWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var useCase = CreateLocationUseCaseParametrized(CreateGuiMock(), CreateDataAccessMock(), null, null, null, null, notificationManager);
            useCase.RemoveDirectory(CreateLocationDirectory.WithId(1));
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void AddLocationWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var useCase = CreateLocationUseCaseParametrized(CreateGuiMock(), CreateDataAccessMock(), null, null, null, null, notificationManager);
            useCase.AddLocation(CreateLocation.Anonymous());
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void RemoveLocationWithoutToolAssigmentsWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var useCase = CreateLocationUseCaseParametrized(CreateGuiMock(), CreateDataAccessMock(), null, null, new LocationToolAssignmentDataMock(), null, notificationManager);
            useCase.RemoveLocation(CreateLocation.Anonymous());
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void RemoveLocationWithToolAssigmentsWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var locationToolAssigmentDataAccess = new LocationToolAssignmentDataMock();
            var location = CreateLocation.IdOnly(1);
            var gui = CreateGuiMock();
            gui.ShowChangeToolStatusDialogCallsOnSuccess = true;
            locationToolAssigmentDataAccess.LoadAssignedToolsForLocationReturn = new List<LocationToolAssignment> { CreateLocationToolAssignment.WithLocation(location) };
            var useCase = CreateLocationUseCaseParametrized(gui, CreateDataAccessMock(), null, null, locationToolAssigmentDataAccess, null, notificationManager);
            useCase.RemoveLocation(location);
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void UpdateLocationWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var useCase = CreateLocationUseCaseParametrized(CreateGuiMock(), CreateDataAccessMock(), null, null, null, null, notificationManager);
            useCase.UpdateLocation(new LocationDiff { NewLocation = CreateLocation.IdOnly(1), OldLocation = CreateLocation.IdOnly(1) });
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void ChangeLocationDirectoryParentWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var useCase = CreateLocationUseCaseParametrized(CreateGuiMock(), CreateDataAccessMock(), null, null, new LocationToolAssignmentDataMock(), null, notificationManager);
            useCase.ChangeLocationDirectoryParent(CreateLocationDirectory.WithId(1),new LocationDirectoryId(1));
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void ChangeLocationParentWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var useCase = CreateLocationUseCaseParametrized(CreateGuiMock(), CreateDataAccessMock(), null, null, new LocationToolAssignmentDataMock(), null, notificationManager);
            useCase.ChangeLocationParent(CreateLocation.IdOnly(1), new LocationDirectoryId(1));
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void LoadTreePathForLocationsCallsDataAccessLoadDirectories()
        {
            var dataAccess = CreateDataAccessMock();
            var useCase = CreateLocationUseCaseParametrized(CreateGuiMock(), dataAccess);
            useCase.LoadTreePathForLocations(new List<Location>());
            Assert.IsTrue(dataAccess.LoadDirectoriesWasCalled);
        }

        private static IEnumerable<List<(Location, List<long>)>> LoadTreePathForLocationTestSource =
            new List<List<(Location, List<long>)>>
            {
                new List<(Location, List<long>)>
                {
                    (CreateLocation.ParentIdOnly(1), new List<long> {1}),
                    (CreateLocation.ParentIdOnly(2), new List<long> {1, 2}),
                },
                new List<(Location, List<long>)>
                {
                    (CreateLocation.ParentIdOnly(5), new List<long> {1, 2, 5}),
                    (CreateLocation.ParentIdOnly(10), new List<long> {1, 4, 7, 8, 10})
                }
            };
  

        [TestCaseSource(nameof(LoadTreePathForLocationTestSource))]
        public void LoadTreePathForLocationsSetsLocationTreePathCorrect(List<(Location location, List<long> treePathIds)> datas)
        {
            var dataAccess = CreateDataAccessMock();
            dataAccess.LoadDirectoriesReturnValue = new List<LocationDirectory>()
            {
                CreateLocationDirectory.Parameterized(1, "", 0),
                CreateLocationDirectory.Parameterized(2, "", 1),
                CreateLocationDirectory.Parameterized(3, "", 1),
                CreateLocationDirectory.Parameterized(4, "", 1),
                CreateLocationDirectory.Parameterized(5, "", 2),
                CreateLocationDirectory.Parameterized(6, "", 2),
                CreateLocationDirectory.Parameterized(7, "", 4),
                CreateLocationDirectory.Parameterized(8, "", 7),
                CreateLocationDirectory.Parameterized(9, "", 8),
                CreateLocationDirectory.Parameterized(10, "", 8)
            };

            var locations = datas.Select(x => x.location).ToList();

            var useCase = CreateLocationUseCaseParametrized(CreateGuiMock(), dataAccess);
            useCase.LoadTreePathForLocations(locations);

            for (var i = 0; i < datas.Count; i++)
            {
                Assert.AreEqual(datas[i].treePathIds, locations[i].LocationDirectoryPath.Select(x => x.Id.ToLong()));
            }
        }

        [Test]
        public void RemoveLocationCallsLoadProcessControlForLocation()
        {
            var processControlData = new ProcessControlDataMock();
            var useCase = CreateLocationUseCaseParametrized(CreateGuiMock(), CreateDataAccessMock(), null ,null , null, null, null,processControlData);
            var location = CreateLocation.Anonymous();
            useCase.RemoveLocation(location);
            Assert.AreSame(location, processControlData.LoadProcessControlConditionForLocationParameter);
        }

        [Test]
        public void RemoveLocationWithNoProcessControlDontCallRemoveProcessControlIfLoadLocationToolAssignmentsForLocationReturnsNull()
        {
            var processControlData = new ProcessControlDataMock();
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataMock();
            var useCase =
                CreateLocationUseCaseParametrized(CreateGuiMock(), CreateDataAccessMock(), null, null,
                    locationToolAssignmentDataAccess, null, null, processControlData);
            locationToolAssignmentDataAccess.LoadAssignedToolsForLocationReturn = null;

            useCase.RemoveLocation(CreateLocation.Anonymous());
            Assert.IsNull(processControlData.RemoveProcessControlConditionParameterCondition);
        }

        [Test]
        public void RemoveLocationWithProcessControlCallsRemoveProcessControlIfLoadLocationToolAssignmentsForLocationReturnsNull()
        {
            var processControlData = new ProcessControlDataMock();
            var processControl = CreateProcessControlCondition.Anonymous();
            processControlData.LoadProcessControlConditionForLocationReturnValue = processControl;
            
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataMock();
            var gui = CreateGuiMock();
            var user = new User();
            var userMock = new UserGetterMock {NextReturnedUser = user};
            var useCase =
                CreateLocationUseCaseParametrized(gui, CreateDataAccessMock(), userMock, null, locationToolAssignmentDataAccess, null, null, processControlData);
            locationToolAssignmentDataAccess.LoadAssignedToolsForLocationReturn = null;
            useCase.RemoveLocation(CreateLocation.Anonymous());
            Assert.AreSame(processControl, processControlData.RemoveProcessControlConditionParameterCondition);
            Assert.AreSame(user, processControlData.RemoveProcessControlConditionParameterUser);
        }

        [Test]
        public void RemoveLocationWithNoProcessControlDontCallRemoveProcessControlIfLoadLocationToolAssignmentsForLocationReturnsData()
        {
            var processControlData = new ProcessControlDataMock();
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataMock();
            var useCase =
                CreateLocationUseCaseParametrized(CreateGuiMock(), CreateDataAccessMock(), null, null,
                    locationToolAssignmentDataAccess, null, null, processControlData);
            var locationToolAssignments = new List<LocationToolAssignment>
            {
                CreateLocationToolAssignment.Anonymous(),
                CreateLocationToolAssignment.Anonymous()
            };
            locationToolAssignmentDataAccess.LoadAssignedToolsForLocationReturn = locationToolAssignments;

            useCase.RemoveLocation(CreateLocation.Anonymous());
            Assert.IsNull(processControlData.RemoveProcessControlConditionParameterCondition);
        }

        [Test]
        public void RemoveLocationWithProcessControlCallsRemoveProcessControlIfLoadLocationToolAssignmentsForLocationReturnsData()
        {
            var processControlData = new ProcessControlDataMock();
            var processControl = CreateProcessControlCondition.Anonymous();
            processControlData.LoadProcessControlConditionForLocationReturnValue = processControl;

            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataMock();
            var gui = CreateGuiMock();
            var user = new User();
            var userMock = new UserGetterMock { NextReturnedUser = user };
            var useCase =
                CreateLocationUseCaseParametrized(gui, CreateDataAccessMock(), userMock, null, locationToolAssignmentDataAccess, null, null, processControlData);
            gui.ShowChangeToolStatusDialogCallsOnSuccess = true;
            var locationToolAssignments = new List<LocationToolAssignment>
            {
                CreateLocationToolAssignment.Anonymous(),
                CreateLocationToolAssignment.Anonymous()
            };
            locationToolAssignmentDataAccess.LoadAssignedToolsForLocationReturn = locationToolAssignments;
            useCase.RemoveLocation(CreateLocation.Anonymous());
            Assert.AreSame(processControl, processControlData.RemoveProcessControlConditionParameterCondition);
            Assert.AreSame(user, processControlData.RemoveProcessControlConditionParameterUser);
        }

        class LocationMock : Location
        {
            public int UpdateToleranceLimitsCallCount { get; set; }

            public override void UpdateToleranceLimits()
            {
                UpdateToleranceLimitsCallCount++;
            }
        }

        class GuiMock : ILocationGui
        {
            public Location AddLocationParameter;

            public string ShowCommentForLocationCommentParameter { get; set; }
            public LocationId ShowCommentForLocationLocationIdParameter { get; set; }
            public Location RemoveLocationParameter { get; set; }
            public LocationId ShowPictureForLocationLocationIdParameter = null;
            public Picture ShowPictureForLocationPictureParameter = null;
            public LocationDiff VerifyLocationDiffParameter { get; set; }
            public bool VerifyLocationDiffReturnValue { get; set; }
            public Location UpdateLocationParameter { get; set; }
            public int AddLocationDirectoryCallCount { get; set; }
            public LocationDirectory AddLocationDirectoryParameter { get; set; }
            public int ShowAddLocationDirectoryErrorCallCount { get; set; }
            public LocationDirectoryId RemoveDirectoryParameter { get; set; }
            public int ShowLocationTreeCallCount { get; set; }
            public int ShowLocationTreeErrorCallCount { get; set; }
            

            public int RemoveLocationCallCount;
            public int ShowLoadingLocationTreeFinishedCallCount { get; set; }
            public int ShowRemoveLocationErrorCallCount { get; set; }
            public int AddLocationCallCount { get; set; }
            public int AddLocationErrorCallCount { get; set; }
            public int ShowPictureForLocationCallCount { get; set; }
            public int ShowCommentForLocationCallCount { get; set; }
            public int UpdateLocationErrorCallCount { get; set; }
            public int UpdateLocationCallCount { get; set; }
            public int RemoveDirectoryCallCount { get; set; }
            public int LocationAlreadyExistsCallCount { get; set; }

            public int ShowRemoveDirectoryErrorCallCount { get; set; }
            public int ChangeLocationParentCallCount { get; set; }
            public int ChangeLocationParentErrorCallCount { get; set; }
            public List<LocationDirectory> ShowLocationTreeParameter { get; set; }
            public int ShowLocationCallCount { get; set; }
            public Location ShowLocationParameter { get; set; }
            public int ChangeLocationDirectoryParentCallCount { get; set; }
            public int ChangeLocationDirectoryParentErrorCallCount { get; set; }
            public int ShowChangeLocationToolAssignmentNoticeCallCount { get; set; }
            public int ShowChangeToolStatusDialogCallCount { get; set; }

            public List<LocationToolAssignment> ShowChangeToolStatusDialogParameter { get; set; }
            public bool ShowChangeToolStatusDialogCallsOnSuccess { get; set; }

            public bool ShowChangeToolStatusDialogThrowsException { get; set; }

            public void ShowLocationTree(List<LocationDirectory> directories)
            {
                ShowLocationTreeParameter = directories;
                ShowLocationTreeCallCount++;
            }


            public void ShowLocationTreeError()
            {
                ShowLocationTreeErrorCallCount++;
            }

            public void ShowLoadingLocationTreeFinished()
            {
                ShowLoadingLocationTreeFinishedCallCount++;
            }

            public void AddLocation(Location location)
            {
                AddLocationParameter = location;
                AddLocationCallCount++;
            }

            public void AddLocationError()
            {
                AddLocationErrorCallCount++;
            }

            public void RemoveLocation(Location location)
            {
                RemoveLocationCallCount++;
                RemoveLocationParameter = location;
            }

            public void ShowRemoveLocationError()
            {
                ShowRemoveLocationErrorCallCount++;
            }

            public void ShowPictureForLocation(Picture picture, LocationId locationId)
            {
                ShowPictureForLocationLocationIdParameter = locationId;
                ShowPictureForLocationPictureParameter = picture;
                ShowPictureForLocationCallCount++;
            }

            public void ShowCommentForLocation(string comment, LocationId locationId)
            {
                ShowCommentForLocationLocationIdParameter = locationId;
                ShowCommentForLocationCommentParameter = comment;
                ShowCommentForLocationCallCount++;
            }

            public bool VerifyLocationDiff(LocationDiff diff)
            {
                VerifyLocationDiffParameter = diff;
                return VerifyLocationDiffReturnValue;
            }

            public void UpdateLocation(Location location)
            {
                UpdateLocationParameter = location;
                UpdateLocationCallCount++;
            }

            public void UpdateLocationError()
            {
                UpdateLocationErrorCallCount++;
            }

            public void LocationAlreadyExists()
            {
                LocationAlreadyExistsCallCount++;
            }

            public void AddLocationDirectory(LocationDirectory locationDirectory)
            {
                AddLocationDirectoryParameter = locationDirectory;
                AddLocationDirectoryCallCount++;
            }

            public void ShowAddLocationDirectoryError(string name)
            {
                ShowAddLocationDirectoryErrorCallCount++;
            }

            public void RemoveDirectory(LocationDirectoryId selectedDirectoryId)
            {
                RemoveDirectoryParameter = selectedDirectoryId;
                RemoveDirectoryCallCount++;
            }

            public void ShowRemoveDirectoryError()
            {
                ShowRemoveDirectoryErrorCallCount++;
            }

            public void ChangeLocationParent(Location location, LocationDirectoryId newParentId)
            {
                ChangeLocationParentCallCount++;
            }

            public void ChangeLocationParentError()
            {
                ChangeLocationParentErrorCallCount++;
            }

            public void ShowLocation(Location location)
            {
                ShowLocationCallCount++;
                ShowLocationParameter = location;
            }

            public void ChangeLocationDirectoryParent(LocationDirectory directory, LocationDirectoryId newParentId)
            {
                ChangeLocationDirectoryParentCallCount++;
            }

            public void ChangeLocationDirectoryParentError()
            {
                ChangeLocationDirectoryParentErrorCallCount++;
            }

            public void ShowChangeLocationToolAssignmentNotice()
            {
                ShowChangeLocationToolAssignmentNoticeCallCount++;
            }

            public void ShowChangeToolStatusDialog(Action onSuccess, List<LocationToolAssignment> locationToolAssignments)
            {
                if (ShowChangeToolStatusDialogThrowsException)
                {
                    throw new Exception();
                }
                ShowChangeToolStatusDialogCallCount++;
                ShowChangeToolStatusDialogParameter = locationToolAssignments;
                if (ShowChangeToolStatusDialogCallsOnSuccess)
                {
                    onSuccess.Invoke();
                }
            }
        }

        private static LocationUseCase CreateLocationUseCaseParametrized(GuiMock gui, LocationDataAccessMock dataAccess, ISessionInformationUserGetter userGetter = null, ToleranceClassUseCase toleranceClassUseCase = null, LocationToolAssignmentDataMock locationToolAssignmentData = null, IToolData toolData = null, INotificationManager notificationManager = null, ProcessControlDataMock processControlData = null)
        {
            return new LocationUseCase(gui, dataAccess, locationToolAssignmentData, toolData, userGetter ?? new UserGetterMock(), toleranceClassUseCase, notificationManager ?? new NotificationManagerMock(), processControlData ?? new ProcessControlDataMock());
        }

        private static UserGetterMock CreateUserGetterWithUser(long userid)
        {
            return new UserGetterMock {NextReturnedUser = CreateUser.IdOnly(userid)};
        }

        private static GuiMock CreateGuiMock()
        {
            return new GuiMock();
        }

        private static LocationDataAccessMock CreateDataAccessMock()
        {
            return new LocationDataAccessMock();
        }
    }
}
