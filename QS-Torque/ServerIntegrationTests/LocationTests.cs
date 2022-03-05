using System;
using System.Collections.Generic;
using System.Linq;
using Core.Diffs;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.Enums;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using FrameworksAndDrivers.RemoteData.GRPC.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper.Factories;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ServerIntegrationTests
{
    [TestClass]
    public class LocationTests
    {
        private readonly TestSetup _testSetup;

        public LocationTests()
        {
            _testSetup = new TestSetup();
        }

        [TestMethod]
        public void AddLocationDirectory()
        {
            var dataAccess = new LocationDataAccess(_testSetup.ClientFactory, new PictureFromZipLoader());

            var locationDirectory = AddNewLocationDirectoryForTest(dataAccess);

            var directories = dataAccess.LoadDirectories();
            var result = directories.Find(x => x.Id.ToLong() == locationDirectory.Id.ToLong());

            Assert.IsTrue(locationDirectory.Id.ToLong() != 0);
            Assert.IsTrue(locationDirectory.EqualsByContent(result));
        }

        [TestMethod]
        public void ChangeLocationDirectoryParent()
        {
            var dataAccess = new LocationDataAccess(_testSetup.ClientFactory, new PictureFromZipLoader());
            var newLocationDirectory = AddNewLocationDirectoryForTest(dataAccess);

            var newParentId = new LocationDirectoryId(newLocationDirectory.ParentId.ToLong() + 10);

            dataAccess.ChangeLocationDirectoryParent(newLocationDirectory, newParentId, _testSetup.TestUser);

            var locationDirectories = dataAccess.LoadDirectories();
            var result = locationDirectories.Find(x => x.Id.ToLong() == newLocationDirectory.Id.ToLong());

            Assert.AreEqual(newParentId.ToLong(), result.ParentId.ToLong());
        }

        [TestMethod]
        public void RemoveDirectory()
        {
            var dataAccess = new LocationDataAccess(_testSetup.ClientFactory, new PictureFromZipLoader());
            var newLocationDirectory = AddNewLocationDirectoryForTest(dataAccess);

            var items = dataAccess.LoadDirectories();
            var result = items.Find(x => x.Id.ToLong() == newLocationDirectory.Id.ToLong());
            Assert.IsNotNull(result);

            dataAccess.RemoveDirectory(newLocationDirectory, _testSetup.TestUser);
            items = dataAccess.LoadDirectories();
            result = items.Find(x => x.Id.ToLong() == newLocationDirectory.Id.ToLong());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LoadDirectories()
        {
            var dataAccess = new LocationDataAccess(_testSetup.ClientFactory, new PictureFromZipLoader());

            var newLocationDirectory = AddNewLocationDirectoryForTest(dataAccess);

            var directories = dataAccess.LoadDirectories();
            var result = directories.Find(x => x.Id.ToLong() == newLocationDirectory.Id.ToLong());
            Assert.IsTrue(newLocationDirectory.EqualsByContent(result));
        }


        [TestMethod]
        public void AddLocation()
        {
            var dataAccess = new LocationDataAccess(_testSetup.ClientFactory, new PictureFromZipLoader());
            var newLocation = AddNewLocationForTest(dataAccess);
            
            var locations = dataAccess.LoadLocations().ToList();
            var result = locations.Find(x => x.Id.ToLong() == newLocation.Id.ToLong());

            Assert.IsTrue(newLocation.Id.ToLong() != 0);
            Assert.IsTrue(newLocation.EqualsByContent(result));
        }

        [TestMethod]
        public void UpdateLocation()
        {
            var dataAccess = new LocationDataAccess(_testSetup.ClientFactory, new PictureFromZipLoader());
            var location = AddNewLocationForTest(dataAccess);

            var updatedLocation = CreateLocation.Parameterized(location.Id.ToLong(), "243536", "jsdfh4385",
                null, "A", "B", false, 10, 134,
                50, LocationControlledBy.Angle, 5, 40, 35676, 10,
                location.ToleranceClassTorque, 3245, location.ToleranceClassAngle);

            var locationDiff =
                new LocationDiff(_testSetup.TestUser, location, updatedLocation, new HistoryComment(""));

            dataAccess.UpdateLocation(locationDiff);

            var locations = dataAccess.LoadLocations().ToList();
            var result = locations.Find(x => x.Id.ToLong() == location.Id.ToLong());

            Assert.IsTrue(updatedLocation.EqualsByContent(result));
        }

        [TestMethod]
        public void ChangeLocationParent()
        {
            var dataAccess = new LocationDataAccess(_testSetup.ClientFactory, new PictureFromZipLoader());
            var newLocation = AddNewLocationForTest(dataAccess);

            var newParentId = new LocationDirectoryId(newLocation.ParentDirectoryId.ToLong() + 10);
            
            dataAccess.ChangeLocationParent(newLocation, newParentId, _testSetup.TestUser);

            var locations = dataAccess.LoadLocations().ToList();
            var result = locations.Find(x => x.Id.ToLong() == newLocation.Id.ToLong());

            Assert.AreEqual(newParentId.ToLong(), result.ParentDirectoryId.ToLong());
        }

        [TestMethod]
        public void RemoveLocation()
        {
            var dataAccess = new LocationDataAccess(_testSetup.ClientFactory, new PictureFromZipLoader());

            var newLocation = AddNewLocationForTest(dataAccess);

            var items = dataAccess.LoadLocations().ToList();
            var result = items.Find(x => x.Id.ToLong() == newLocation.Id.ToLong());
            Assert.IsNotNull(result);

            dataAccess.RemoveLocation(newLocation, _testSetup.TestUser);
            items = dataAccess.LoadLocations().ToList();
            result = items.Find(x => x.Id.ToLong() == newLocation.Id.ToLong());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LoadLocations()
        {
            var dataAccess = new LocationDataAccess(_testSetup.ClientFactory, new PictureFromZipLoader());

            var newLocation = AddNewLocationForTest(dataAccess);

            var locations = dataAccess.LoadLocations().ToList();
            var result = locations.Find(x => x.Id.ToLong() == newLocation.Id.ToLong());
            Assert.IsTrue(newLocation.EqualsByContent(result));
        }

        [TestMethod]
        public void LoadLocationsByIds()
        {
            var dataAccess = new LocationDataAccess(_testSetup.ClientFactory, new PictureFromZipLoader());

            var newLocation1 = AddNewLocationForTest(dataAccess);
            var newLocation2 = AddNewLocationForTest(dataAccess);
            var newLocation3 = AddNewLocationForTest(dataAccess);

            var locations = dataAccess.LoadLocationsByIds(new List<LocationId>() {newLocation1.Id, newLocation2.Id});
            Assert.AreEqual(2, locations.Count);
            Assert.IsTrue(newLocation1.EqualsByContent(locations.SingleOrDefault(x => x.Id.ToLong() == newLocation1.Id.ToLong())));
            Assert.IsTrue(newLocation2.EqualsByContent(locations.SingleOrDefault(x => x.Id.ToLong() == newLocation2.Id.ToLong())));
        }

        [TestMethod]
        public void IsNumberUnique()
        {
            var dataAccess = new LocationDataAccess(_testSetup.ClientFactory, new PictureFromZipLoader());

            var newLocation = AddNewLocationForTest(dataAccess);

            Assert.IsFalse(dataAccess.IsNumberUnique(newLocation.Number.ToDefaultString()));
            Assert.IsTrue(dataAccess.IsNumberUnique(newLocation.Number.ToDefaultString() + "X"));
        }

        [TestMethod]
        public void LoadCommentForLocation()
        {
            var dataAccess = new LocationDataAccess(_testSetup.ClientFactory, new PictureFromZipLoader());

            var location = AddNewLocationForTest(dataAccess);

            var updatedLocation = location.CopyDeep();
            updatedLocation.Comment = "Kommentar XYZ";

            var locationDiff =
                new LocationDiff(_testSetup.TestUser, location, updatedLocation, new HistoryComment(""));
            dataAccess.UpdateLocation(locationDiff);

            var result = dataAccess.LoadCommentForLocation(updatedLocation.Id);
            Assert.AreEqual(updatedLocation.Comment, result);
        }

        [TestMethod]
        public void GetLocationToolAssignmentIdsByLocationId()
        {
            var location = TestDataCreator.CreateLocation(_testSetup, "l_" + DateTime.Now.Ticks);
            var dataAccess = new LocationDataAccess(_testSetup.ClientFactory, new PictureFromZipLoader());
            var ids = dataAccess.GetLocationToolAssignmentIdsByLocationId(location.Id).ToList();

            Assert.AreEqual(0, ids.Count);

            var locationToolAssignment = TestDataCreator.CreateLocationToolAssignment(_testSetup, false, location);
            ids = dataAccess.GetLocationToolAssignmentIdsByLocationId(location.Id).ToList();

            Assert.AreEqual(1, ids.Count);
            Assert.AreEqual(locationToolAssignment.Id.ToLong(), ids.First());
        }

        [TestMethod]
        public void LoadPictureForLocation()
        {
            //TODO: Complete the test, as soon AddPicture is implemented with grpc
            Assert.IsTrue(true);
        }

        private Location AddNewLocationForTest(LocationDataAccess dataAccess)
        {
            var userid = "loc_" + DateTime.Now.Ticks;

            var locationItems = dataAccess.LoadLocations().ToList();
            var result = locationItems.Find(x => x.Number.ToDefaultString() == userid);
            Assert.IsNull(result);

            return TestDataCreator.CreateLocation(_testSetup, userid);
        }

        private LocationDirectory AddNewLocationDirectoryForTest(LocationDataAccess dataAccess)
        {
            var name = "dir_" + System.DateTime.Now.Ticks;

            var locationDirectoryItems = dataAccess.LoadDirectories();
            var result = locationDirectoryItems.Find(x => x.Name.ToDefaultString() == name);
            Assert.IsNull(result);

            return TestDataCreator.CreateLocationDirectory(_testSetup, name);
        }

        public static void CheckLocationReferenceLink(List<LocationReferenceLink> locationReferences, Location location)
        {
            Assert.AreEqual(1, locationReferences.Count);
            var locationReference = locationReferences.Single(x => x.Id.ToLong() == location.Id.ToLong());
            Assert.IsNotNull(locationReference);
            Assert.AreEqual(location.Number.ToDefaultString(), locationReference.Number.ToDefaultString());
            Assert.AreEqual(location.Description.ToDefaultString(), locationReference.Description.ToDefaultString());
        }
    }
}
