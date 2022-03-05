using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BasicTypes;
using Client.TestHelper.Mock;
using Core.Entities;
using Core.Enums;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Google.Protobuf;
using LocationService;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;
using Location = Core.Entities.Location;
using LocationDirectory = DtoTypes.LocationDirectory;
using String = BasicTypes.String;
using User = Core.Entities.User;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    public class LocationClientMock : ILocationClient
    {
        public ListOfLocationDirectory LoadLocationDirectoriesReturnValue { get; set; } = new ListOfLocationDirectory();
        public bool LoadLocationDirectoriesCalled { get; set; }
        public InsertLocationDirectoriesWithHistoryRequest InsertLocationDirectoriesWithHistoryParameter { get; set; }
        public ListOfLocationDirectory InsertLocationDirectoriesWithHistoryReturnValue { get; set; } = new ListOfLocationDirectory();
        public ListOfLocationDirectory UpdateLocationDirectoriesWithHistoryReturnValue { get; set; } = new ListOfLocationDirectory();
        public UpdateLocationDirectoriesWithHistoryRequest UpdateLocationDirectoriesWithHistoryParameter { get; set; }
        public LoadLocationsRequest LoadLocationsParameter { get; set; }
        public ListOfLocation LoadLocationsReturnValue { get; set; } = new ListOfLocation();
        public ListOfLocation InsertLocationsWithHistoryReturnValue { get; set; } = new ListOfLocation();
        public InsertLocationsWithHistoryRequest InsertLocationsWithHistoryParameter { get; set; }
        public ListOfLocation UpdateLocationsWithHistoryReturnValue { get; set; } = new ListOfLocation();
        public UpdateLocationsWithHistoryRequest UpdateLocationsWithHistoryParameter { get; set; }
        public Bool IsUserIdUniqueReturnValue { get; set; } = new Bool();
        public Long GetReferencedLocPowIdsForLocationIdParameter { get; set; }
        public ListOfLongs GetReferencedLocPowIdsForLocationIdReturnValue { get; set; } = new ListOfLongs();
        public String IsUserIdUniqueParameter { get; set; }
        public String GetLocationCommentReturnValue { get; set; } = new String();
        public Long GetLocationCommentParameter { get; set; }
        public bool LoadLocationsCalled { get; set; }
        public LoadPictureForLocationRequest LoadPictureForLocationParameter { get; set; }
        public LoadPictureForLocationResponse LoadPictureForLocationReturnValue { get; set; } = new LoadPictureForLocationResponse();
        public ListOfLocation LoadLocationsByIdsReturnValue { get; set; } = new ListOfLocation();
        public ListOfLongs LoadLocationsByIdsParameter { get; set; }

        public ListOfLocationDirectory LoadLocationDirectories()
        {
            LoadLocationDirectoriesCalled = true;
            return LoadLocationDirectoriesReturnValue;
        }

        public ListOfLocationDirectory InsertLocationDirectoriesWithHistory(InsertLocationDirectoriesWithHistoryRequest request)
        {
            InsertLocationDirectoriesWithHistoryParameter = request;
            return InsertLocationDirectoriesWithHistoryReturnValue;
        }

        public ListOfLocationDirectory UpdateLocationDirectoriesWithHistory(UpdateLocationDirectoriesWithHistoryRequest request)
        {
            UpdateLocationDirectoriesWithHistoryParameter = request;
            return UpdateLocationDirectoriesWithHistoryReturnValue;
        }

        public ListOfLocation LoadLocations(LoadLocationsRequest request)
        {
            LoadLocationsCalled = true;
            LoadLocationsParameter = request;
            return request.Index > LoadLocationsReturnValue.Locations.Count
                ? new ListOfLocation()
                : LoadLocationsReturnValue;
        }

        public ListOfLocation LoadLocationsByIds(ListOfLongs ids)
        {
            LoadLocationsByIdsParameter = ids;
            return LoadLocationsByIdsReturnValue;
        }

        public ListOfLocation InsertLocationsWithHistory(InsertLocationsWithHistoryRequest request)
        {
            InsertLocationsWithHistoryParameter = request;
            return InsertLocationsWithHistoryReturnValue;
        }

        public ListOfLocation UpdateLocationsWithHistory(UpdateLocationsWithHistoryRequest request)
        {
            UpdateLocationsWithHistoryParameter = request;
            return UpdateLocationsWithHistoryReturnValue;
        }

        public Bool IsUserIdUnique(String name)
        {
            IsUserIdUniqueParameter = name;
            return IsUserIdUniqueReturnValue;
        }

        public ListOfLongs GetReferencedLocPowIdsForLocationId(Long locationId)
        {
            GetReferencedLocPowIdsForLocationIdParameter = locationId;
            return GetReferencedLocPowIdsForLocationIdReturnValue;
        }

        public String GetLocationComment(Long locationId)
        {
            GetLocationCommentParameter = locationId;
            return GetLocationCommentReturnValue;
        }

        public LoadPictureForLocationResponse LoadPictureForLocation(LoadPictureForLocationRequest request)
        {
            LoadPictureForLocationParameter = request;
            return LoadPictureForLocationReturnValue;
        }

        public ListOfLocationDirectory LoadAllLocationDirectories()
        {
            throw new NotImplementedException();
        }

        public ListOfLocation LoadDeletedLocations(LoadLocationsRequest request)
        {
            throw new NotImplementedException();
        }
    }

    public class PictureFromZipLoaderMock : IPictureFromZipLoader
    {
        public Core.Entities.Picture LoadPictureFromZipBytes(byte[] zip)
        {
            LoadPictureFromZipBytesParameter = zip;
            return LoadPictureFromZipBytesReturnValue;
        }

        public byte[] LoadPictureFromZipBytesParameter { get; set; }

        public Core.Entities.Picture LoadPictureFromZipBytesReturnValue { get; set; } = new Core.Entities.Picture();
    }

    public class LocationDataAccessTest
    {

        [Test]
        public void LoadDirectoriesCallsClient()
        {
            var environment = new Environment();

            environment.dataAccess.LoadDirectories();

            Assert.IsTrue(environment.mocks.locationClient.LoadLocationDirectoriesCalled);
        }

        private static IEnumerable<ListOfLocationDirectory> LoadLocationDirectoriesData = new List<ListOfLocationDirectory>()
        {
            new ListOfLocationDirectory()
            {
                LocationDirectories =
                {
                    DtoFactory.CreateLocationDirectory(133, "Test", 9, true),
                    DtoFactory.CreateLocationDirectory(12, "ABC", 19, false)
                }
            },
            new ListOfLocationDirectory()
            {
                LocationDirectories =
                {
                    DtoFactory.CreateLocationDirectory(1, "Halle", 1119, false)
                }
            }
        };

        [TestCaseSource(nameof(LoadLocationDirectoriesData))]
        public void LoadDirectoriesReturnsCorrectValue(ListOfLocationDirectory listOfLocationDirectories)
        {
            var environment = new Environment();
            environment.mocks.locationClient.LoadLocationDirectoriesReturnValue = listOfLocationDirectories;

            var result = environment.dataAccess.LoadDirectories();

            var comparer =
                new Func<LocationDirectory, Core.Entities.LocationDirectory, bool>((dto, entity) =>
                    EqualityChecker.CompareLocationDirectoryDtoWithLocationDirectory(dto,entity));

            CheckerFunctions.CollectionAssertAreEquivalent(listOfLocationDirectories.LocationDirectories, result, comparer);
        }

        [Test]
        public void AddLocationDirectoryWithoutUserThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.AddLocationDirectory(new LocationDirectoryId(1), "Test", null); });
        }

        [Test]
        public void AddLocationDirectoryWithoutNameThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.AddLocationDirectory(new LocationDirectoryId(1), null, CreateUser.Anonymous()); });
        }

        [Test]
        public void AddLocationDirectoryWithoutParentThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.AddLocationDirectory(null, "Test", CreateUser.Anonymous()); });
        }

        static IEnumerable<(Core.Entities.LocationDirectory, Core.Entities.User)> AddAndRemoveLocationDirectoryData = new List<(Core.Entities.LocationDirectory, Core.Entities.User)>()
        {
            (
                TestHelper.Factories.CreateLocationDirectory.Parameterized(0, "Test", 6),
                CreateUser.IdOnly(2)
            ),
            (
                TestHelper.Factories.CreateLocationDirectory.Parameterized(0, "Halle", 9),
                CreateUser.IdOnly(32)
            )
        };

        [TestCaseSource(nameof(AddAndRemoveLocationDirectoryData))]
        public void AddLocationDirectoryCallsClient((Core.Entities.LocationDirectory locationDirectory, Core.Entities.User user) data)
        {
            var environment = new Environment();
            environment.mocks.locationClient.InsertLocationDirectoriesWithHistoryReturnValue = new ListOfLocationDirectory() { LocationDirectories = { new LocationDirectory() } };
            environment.dataAccess.AddLocationDirectory(data.locationDirectory.ParentId, data.locationDirectory.Name.ToDefaultString(), data.user);

            var clientParam = environment.mocks.locationClient.InsertLocationDirectoriesWithHistoryParameter;
            var clientDiff = clientParam.LocationDirectoryDiffs.LocationDirectoyDiffs.First();

            Assert.AreEqual(1, clientParam.LocationDirectoryDiffs.LocationDirectoyDiffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.AreEqual("", clientDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareLocationDirectoryDtoWithLocationDirectory(clientDiff.NewLocationDirectory, data.locationDirectory));
            Assert.IsNull(clientDiff.OldLocationDirectory);
            Assert.IsTrue(clientParam.ReturnList);
        }

        static IEnumerable<DtoTypes.LocationDirectory> LocationDirectoryDtoData = new List<DtoTypes.LocationDirectory>()
        {
            DtoFactory.CreateLocationDirectory(1, "Test", 9, true),
            DtoFactory.CreateLocationDirectory(19, "Plant A", 911, false),
        };

        [TestCaseSource(nameof(LocationDirectoryDtoData))]
        public void AddLocationDirectoryReturnsCorrectValue(DtoTypes.LocationDirectory locationDirectoryDto)
        {
            var environment = new Environment();
            environment.mocks.locationClient.InsertLocationDirectoriesWithHistoryReturnValue =
                new ListOfLocationDirectory() { LocationDirectories = { locationDirectoryDto } };

            var result = environment.dataAccess.AddLocationDirectory(new LocationDirectoryId(1), "Test", CreateUser.Anonymous());

            Assert.IsTrue(EqualityChecker.CompareLocationDirectoryDtoWithLocationDirectory(locationDirectoryDto, result));
        }

        [Test]
        public void AddLocationDirectoryReturnsNullThrowsException()
        {
            var environment = new Environment();
            Assert.Throws<NullReferenceException>(() =>
            {
                environment.dataAccess.AddLocationDirectory(new LocationDirectoryId(1), "Test", CreateUser.Anonymous());
            });
        }

        [Test]
        public void RemoveDirectoryWithoutUserThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.RemoveDirectory(TestHelper.Factories.CreateLocationDirectory.Anonymous(), null); });
        }

        [Test]
        public void RemoveDirectoryWithoutDirectoryThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.RemoveDirectory(null, CreateUser.Anonymous()); });
        }

        [TestCaseSource(nameof(AddAndRemoveLocationDirectoryData))]
        public void RemoveDirectoryCallsClient((Core.Entities.LocationDirectory locationDirectory, Core.Entities.User user) data)
        {
            var environment = new Environment();
            environment.mocks.locationClient.UpdateLocationDirectoriesWithHistoryReturnValue = new ListOfLocationDirectory() { LocationDirectories = { new LocationDirectory() } };
            environment.dataAccess.RemoveDirectory(data.locationDirectory, data.user);

            var clientParam = environment.mocks.locationClient.UpdateLocationDirectoriesWithHistoryParameter;
            var clientDiff = clientParam.LocationDirectoryDiffs.LocationDirectoyDiffs.First();

            Assert.AreEqual(1, clientParam.LocationDirectoryDiffs.LocationDirectoyDiffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.AreEqual("", clientDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareLocationDirectoryDtoWithLocationDirectory(clientDiff.NewLocationDirectory, data.locationDirectory));
            Assert.IsTrue(EqualityChecker.CompareLocationDirectoryDtoWithLocationDirectory(clientDiff.OldLocationDirectory, data.locationDirectory));
            Assert.AreEqual(true, clientDiff.OldLocationDirectory.Alive);
            Assert.AreEqual(false, clientDiff.NewLocationDirectory.Alive);
        }

        [Test]
        public void AddLocationWithoutUserThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.AddLocation(TestHelper.Factories.CreateLocation.Anonymous(), null); });
        }

        static IEnumerable<(Core.Entities.Location, Core.Entities.User)> AddAndRemoveLocationData = new List<(Core.Entities.Location, Core.Entities.User)>()
        {
            (
                TestHelper.Factories.CreateLocation.Parameterized(1,"2342345", "abcd",
                    "Kommentar 1", "A", "B", true,10,
                    15, 12, LocationControlledBy.Angle,5,12,56,
                    6,CreateToleranceClass.WithId(1), 10, CreateToleranceClass.WithId(4)),
                CreateUser.IdOnly(2)
            ),
            (
                TestHelper.Factories.CreateLocation.Parameterized(11,"SST", "SSTNAME",
                    "Kommentar DE", "A", "B", true,10,
                    151, 121,LocationControlledBy.Angle,51,121,561,
                    61,CreateToleranceClass.WithId(11), 101, CreateToleranceClass.WithId(41)),
                CreateUser.IdOnly(32)
            )
        };

        [TestCaseSource(nameof(AddAndRemoveLocationData))]
        public void AddLocationCallsClient((Core.Entities.Location location, Core.Entities.User user) data)
        {
            var environment = new Environment();
            environment.mocks.locationClient.InsertLocationsWithHistoryReturnValue = new ListOfLocation() { Locations = { new DtoTypes.Location() } };
            environment.dataAccess.AddLocation(data.location, data.user);

            var clientParam = environment.mocks.locationClient.InsertLocationsWithHistoryParameter;
            var clientDiff = clientParam.LocationDiffs.LocationDiffs.First();

            Assert.AreEqual(1, clientParam.LocationDiffs.LocationDiffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.User.UserId);
            Assert.AreEqual("", clientDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareLocationDtoWithLocation(clientDiff.NewLocation, data.location));
            Assert.IsNull(clientDiff.OldLocation);
            Assert.IsTrue(clientParam.ReturnList);
        }

        static IEnumerable<DtoTypes.Location> LocationDtoData = new List<DtoTypes.Location>()
        {
            DtoFactory.CreateLocation(1, "test", "sst", 1,"A", "B", false,
                0,DtoFactory.CreateToleranceClass(1, "tol1", true,0,0,true),
                DtoFactory.CreateToleranceClass(13, "tol2", true,0,0,false),10,100,20,200,
                5,50,3,"Kommentar", true),
            DtoFactory.CreateLocation(12, "test 2", "x", 1,"L", "Z", false,
                0,DtoFactory.CreateToleranceClass(13, "tol2", true,0,0,false),
                DtoFactory.CreateToleranceClass(1, "tol1", true,0,0,true),102,1002,202,2002,
                52,502,32,"XXX", false)
        };

        [TestCaseSource(nameof(LocationDtoData))]
        public void AddLocationReturnsCorrectValue(DtoTypes.Location locationDto)
        {
            var environment = new Environment();
            environment.mocks.locationClient.InsertLocationsWithHistoryReturnValue =
                new ListOfLocation() { Locations = { locationDto } };

            var location = TestHelper.Factories.CreateLocation.Anonymous();
            location.ToleranceClassTorque = DtoFactory.CreateToleranceClassFromDto(locationDto.ToleranceClass1);
            location.ToleranceClassAngle = DtoFactory.CreateToleranceClassFromDto(locationDto.ToleranceClass2);

            var result = environment.dataAccess.AddLocation(location, CreateUser.Anonymous());

            Assert.IsTrue(EqualityChecker.CompareLocationDtoWithLocation(locationDto, result));
        }

        [Test]
        public void AddLocationReturnsNullThrowsException()
        {
            var environment = new Environment();
            Assert.Throws<NullReferenceException>(() =>
            {
                environment.dataAccess.AddLocation(TestHelper.Factories.CreateLocation.Anonymous(), CreateUser.Anonymous());
            });
        }

        [Test]
        public void LoadLocationsDontLoadLocationsWithError()
        {
            var environment = new Environment();
            environment.mocks.locationClient.LoadLocationsReturnValue = new ListOfLocation()
            {
                Locations = { new DtoTypes.Location()
                {
                    Id = 1,
                    ParentDirectoryId = 1,
                    Number = "ghtz390owöle,fmkgnjw0\nw3o49e"
                }}
            };

            var result = environment.dataAccess.LoadLocations().ToList();

            Assert.IsTrue(environment.mocks.locationClient.LoadLocationsCalled);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void LoadLocationsCallsClient()
        {
            var environment = new Environment();

            var result = environment.dataAccess.LoadLocations().ToList();

            Assert.IsTrue(environment.mocks.locationClient.LoadLocationsCalled);
        }

        private static IEnumerable<ListOfLocation> LoadLocationsData = new List<ListOfLocation>()
        {
            new ListOfLocation()
            {
                Locations =
                {
                    DtoFactory.CreateLocation(1, "test", "sst", 1,"A", "B", false,
                        0,DtoFactory.CreateToleranceClass(2,"tol1",true,0,0,true),
                        DtoFactory.CreateToleranceClass(4,"tol88",false,0,0,false),10,100,20,200,
                        5,50,3,"Kommentar", true),
                    DtoFactory.CreateLocation(12, "test 2", "x", 1,"L", "Z", false,
                        0,DtoFactory.CreateToleranceClass(2,"tol1",true,0,0,true),
                        DtoFactory.CreateToleranceClass(24,"tol99",false,0,0,true),102,1002,202,2002,
                        52,502,32,"XXX", false)
                }
            },
            new ListOfLocation()
            {
                Locations =
                {
                    DtoFactory.CreateLocation(14, "Halle", "Desc", 11,"F", "M", false,
                        1,DtoFactory.CreateToleranceClass(2,"tol1",true,0,0,true),
                        DtoFactory.CreateToleranceClass(25,"tol9",false,0,0,true),106,1008,209,2000,
                        56,503,31,"Test", true)
                }
            }
        };

        [TestCaseSource(nameof(LoadLocationsData))]
        public void LoadLocationsReturnsCorrectValue(ListOfLocation listOfLocations)
        {
            var environment = new Environment();
            environment.mocks.locationClient.LoadLocationsReturnValue = listOfLocations;

            var result = environment.dataAccess.LoadLocations().ToList();

            var comparer =
                new Func<DtoTypes.Location, Core.Entities.Location, bool>((dto, entity) => EqualityChecker.CompareLocationDtoWithLocation(dto, entity));

            CheckerFunctions.CollectionAssertAreEquivalent(listOfLocations.Locations, result, comparer);
        }

        static IEnumerable<List<LocationId>> LoadLocationsByIdsData = new List<List<LocationId>>()
        {
            new List<LocationId>()
            {
                new LocationId(1),
                new LocationId(14),
                new LocationId(156)
            },
            new List<LocationId>()
            {
                new LocationId(6),
                new LocationId(654),
                new LocationId(567)
            }
        };

        [TestCaseSource(nameof(LoadLocationsByIdsData))]
        public void LoadLocationsByIdsCallsClient(List<LocationId> ids)
        {
            var environment = new Environment();

            environment.dataAccess.LoadLocationsByIds(ids);

            Assert.AreEqual(ids.Select(x => x.ToLong()).ToList(), environment.mocks.locationClient.LoadLocationsByIdsParameter.Values.Select(x => x.Value).ToList());
        }

        [TestCaseSource(nameof(LoadLocationsData))]
        public void LoadLocationsByIdsReturnsCorrectValue(ListOfLocation listOfLocations)
        {
            var environment = new Environment();
            environment.mocks.locationClient.LoadLocationsByIdsReturnValue = listOfLocations;

            var result = environment.dataAccess.LoadLocationsByIds(new List<LocationId>());

            var comparer =
                new Func<DtoTypes.Location, Core.Entities.Location, bool>((dto, entity) => EqualityChecker.CompareLocationDtoWithLocation(dto, entity));

            CheckerFunctions.CollectionAssertAreEquivalent(listOfLocations.Locations, result, comparer);
        }


        [Test]
        public void RemoveLocationWithoutUserThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.RemoveLocation(TestHelper.Factories.CreateLocation.Anonymous(), null); });
        }

        [Test]
        public void RemoveLocationWithoutThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.RemoveLocation(null, CreateUser.Anonymous()); });
        }

        [TestCaseSource(nameof(AddAndRemoveLocationData))]
        public void RemoveLocationCallsClient((Core.Entities.Location location, Core.Entities.User user) data)
        {
            var environment = new Environment();
            environment.mocks.locationClient.UpdateLocationsWithHistoryReturnValue = new ListOfLocation() { Locations = { new DtoTypes.Location() } };
            environment.dataAccess.RemoveLocation(data.location, data.user);

            var clientParam = environment.mocks.locationClient.UpdateLocationsWithHistoryParameter;
            var clientDiff = clientParam.LocationDiffs.LocationDiffs.First();

            Assert.AreEqual(1, clientParam.LocationDiffs.LocationDiffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.User.UserId);
            Assert.AreEqual("", clientDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareLocationDtoWithLocation(clientDiff.NewLocation, data.location));
            Assert.IsTrue(EqualityChecker.CompareLocationDtoWithLocation(clientDiff.OldLocation, data.location));
            Assert.AreEqual(true, clientDiff.OldLocation.Alive);
            Assert.AreEqual(false, clientDiff.NewLocation.Alive);
        }

        [Test]
        public void UpdateLocationWithMismatchingIdsThrowsArgumentException()
        {
            var environment = new Environment();

            var locationDiff = new Core.Diffs.LocationDiff(CreateUser.Anonymous(), TestHelper.Factories.CreateLocation.IdOnly(1), TestHelper.Factories.CreateLocation.IdOnly(2));
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.UpdateLocation(locationDiff); });
        }

        static IEnumerable<(Core.Entities.Location, Core.Entities.Location, Core.Entities.User, string)> UpdateLocationData =
            new List<(Core.Entities.Location, Core.Entities.Location, Core.Entities.User, string)>()
        {
            (
                TestHelper.Factories.CreateLocation.Parameterized(1,"2342345", "abcd",
                    "Kommentar 1", "A", "B", true,10,
                    15, 12, LocationControlledBy.Angle,5,12,56,
                    6,CreateToleranceClass.WithId(1), 10, CreateToleranceClass.WithId(4)),
                TestHelper.Factories.CreateLocation.Parameterized(1,"AA", "BB",
                    "Kommentar XY", "L", "T", false,101,
                    151, 121, LocationControlledBy.Torque,51,121,561,
                    61, CreateToleranceClass.WithId(11), 101, CreateToleranceClass.WithId(41)),
                CreateUser.IdOnly(2),
                "Komentar A"
            ),
            (
                TestHelper.Factories.CreateLocation.Parameterized(11,"SST", "SSTNAME",
                    "Kommentar DE", "A", "B", true,10,
                    151, 121,LocationControlledBy.Angle,51,121,561,
                    61,CreateToleranceClass.WithId(11), 101, CreateToleranceClass.WithId(41)),
                TestHelper.Factories.CreateLocation.Parameterized(11,"SST X", "AB",
                    "Kommentar ??", "Ä", "Z", true,102,
                    1514, 1215, LocationControlledBy.Angle,517,1218,5619,
                    613, CreateToleranceClass.WithId(141), 1011, CreateToleranceClass.WithId(481)),
                CreateUser.IdOnly(2),
                "Komentar B"
            )
        };

        [TestCaseSource(nameof(UpdateLocationData))]
        public void UpdateLocationCallsClient((Core.Entities.Location oldLocation, Core.Entities.Location newLocation, Core.Entities.User user, string comment) data)
        {
            var environment = new Environment();

            var locationDiff = new Core.Diffs.LocationDiff(data.user, data.oldLocation, data.newLocation, new HistoryComment(data.comment));
            environment.dataAccess.UpdateLocation(locationDiff);

            var clientParam = environment.mocks.locationClient.UpdateLocationsWithHistoryParameter;
            var clientDiff = clientParam.LocationDiffs.LocationDiffs.First();

            Assert.AreEqual(1, clientParam.LocationDiffs.LocationDiffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.User.UserId);
            Assert.AreEqual(data.comment, clientDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareLocationDtoWithLocation(clientDiff.NewLocation, data.newLocation));
            Assert.IsTrue(EqualityChecker.CompareLocationDtoWithLocation(clientDiff.OldLocation, data.oldLocation));
            Assert.AreEqual(true, clientDiff.OldLocation.Alive);
            Assert.AreEqual(true, clientDiff.NewLocation.Alive);
        }

        [Test]
        public void UpdateLocationReturnsCorrectValue()
        {
            var environment = new Environment();
            var diff = new Core.Diffs.LocationDiff()
            {
                OldLocation = TestHelper.Factories.CreateLocation.Anonymous(),
                NewLocation = TestHelper.Factories.CreateLocation.Anonymous(),
                User = TestHelper.Factories.CreateUser.Anonymous(),
                Comment = new HistoryComment("")
            };

            Assert.AreSame(diff.NewLocation, environment.dataAccess.UpdateLocation(diff));
        }

        [TestCase(145)]
        [TestCase(678)]
        public void LoadCommentForLocationCallsClient(long locId)
        {
            var environment = new Environment();
            environment.mocks.locationClient.GetLocationCommentReturnValue = new String();
            environment.dataAccess.LoadCommentForLocation(new LocationId(locId));

            Assert.AreEqual(locId, environment.mocks.locationClient.GetLocationCommentParameter.Value);
        }

        [TestCase("Kommentar 7364")]
        [TestCase("Schraubstelle 1")]
        public void LoadCommentForLocationReturnsCorrectValue(string comment)
        {
            var environment = new Environment();
            environment.mocks.locationClient.GetLocationCommentReturnValue = new String() { Value = comment };

            var result = environment.dataAccess.LoadCommentForLocation(new LocationId(1));

            Assert.AreEqual(comment, result);
        }

        [Test]
        public void LoadCommentForLocationWithoutLocationIdThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.LoadCommentForLocation(null); });
        }

        [TestCase(1)]
        [TestCase(100)]
        public void GetLocationToolAssignmentIdsByLocationIdCallsClient(long id)
        {
            var environment = new Environment();
            environment.dataAccess.GetLocationToolAssignmentIdsByLocationId(new LocationId(id));
            Assert.AreEqual(id, environment.mocks.locationClient.GetReferencedLocPowIdsForLocationIdParameter.Value);
        }

        static IEnumerable<ListOfLongs> GetLocationToolAssignmentIdsByLocationIdData = new List<ListOfLongs>()
        {
            new ListOfLongs()
            {
                Values =
                {
                    new Long{Value = 1},
                    new Long{Value = 2},
                    new Long{Value = 3},
                }
            },
            new ListOfLongs()
            {
                Values =
                {
                    new Long{Value = 5},
                    new Long{Value = 7},
                    new Long{Value = 9},
                }
            }
        };

        [TestCaseSource(nameof(GetLocationToolAssignmentIdsByLocationIdData))]
        public void GetLocationToolAssignmentIdsByLocationIdReturnsCorrectValue(ListOfLongs longs)
        {
            var environment = new Environment();
            environment.mocks.locationClient.GetReferencedLocPowIdsForLocationIdReturnValue = longs;
            var result = environment.dataAccess.GetLocationToolAssignmentIdsByLocationId(new LocationId(1));

            var comparer = new Func<long, Long, bool>((val, valDto) =>
                val == valDto.Value
            );

            CheckerFunctions.CollectionAssertAreEquivalent(result.ToList(), longs.Values, comparer);
        }

        [TestCase("12345")]
        [TestCase("abcd")]
        public void IsNumberUniqueCallsClient(string number)
        {
            var environment = new Environment();
            environment.dataAccess.IsNumberUnique(number);

            Assert.AreEqual(number, environment.mocks.locationClient.IsUserIdUniqueParameter.Value);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsNumberUniqueReturnsCorrectValue(bool isUnique)
        {
            var environment = new Environment();
            environment.mocks.locationClient.IsUserIdUniqueReturnValue = new Bool() { Value = isUnique };
            environment.dataAccess.IsNumberUnique("");

            Assert.AreEqual(isUnique, environment.dataAccess.IsNumberUnique(""));
        }

        private static IEnumerable<(Location, Core.Entities.LocationDirectory, User)> ChangeLocationParentCallsClientData =
                new List<(Location, Core.Entities.LocationDirectory, User)>()
                {
                   (
                       TestHelper.Factories.CreateLocation.Parameterized(1,"2342345", "abcd",
                           "Kommentar 1", "A", "B", true,10,
                           15, 12, LocationControlledBy.Angle,5,12,56,
                           6,CreateToleranceClass.WithId(1), 10, CreateToleranceClass.WithId(4)),
                       TestHelper.Factories.CreateLocationDirectory.WithId(12),
                       TestHelper.Factories.CreateUser.IdOnly(12)
                    ),
                   (
                       TestHelper.Factories.CreateLocation.Parameterized(12,"3245", "xxxx",
                           "Test", "D", "L", true,101,
                           152, 123, LocationControlledBy.Torque,53,125,566,
                           67,CreateToleranceClass.WithId(71), 107, CreateToleranceClass.WithId(74)),
                       TestHelper.Factories.CreateLocationDirectory.WithId(128),
                       TestHelper.Factories.CreateUser.IdOnly(128)
                   )    
                };

        [TestCaseSource(nameof(ChangeLocationParentCallsClientData))]
        public void ChangeLocationParentCallsClient((Core.Entities.Location location, Core.Entities.LocationDirectory newParent, Core.Entities.User user) data)
        {
            var environment = new Environment();

            environment.dataAccess.ChangeLocationParent(data.location, data.newParent.Id, data.user);

            var clientParam = environment.mocks.locationClient.UpdateLocationsWithHistoryParameter;
            var clientDiff = clientParam.LocationDiffs.LocationDiffs.First();

            Assert.AreEqual(1, clientParam.LocationDiffs.LocationDiffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.User.UserId);
            Assert.AreEqual("", clientDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareLocationDtoWithLocation(clientDiff.NewLocation, data.location, false));
            Assert.IsTrue(EqualityChecker.CompareLocationDtoWithLocation(clientDiff.OldLocation, data.location, false));
            Assert.AreEqual(true, clientDiff.OldLocation.Alive);
            Assert.AreEqual(true, clientDiff.NewLocation.Alive);
            Assert.AreEqual(data.location.ParentDirectoryId.ToLong(), clientDiff.OldLocation.ParentDirectoryId);
            Assert.AreEqual(data.newParent.Id.ToLong(), clientDiff.NewLocation.ParentDirectoryId);
        }

        private static IEnumerable<(Core.Entities.LocationDirectory, Core.Entities.LocationDirectory, User)> ChangeLocationDirectoryParentCallsClientData =
                new List<(Core.Entities.LocationDirectory, Core.Entities.LocationDirectory, User)>()
                {
                   (
                       TestHelper.Factories.CreateLocationDirectory.Parameterized(12, "Halle X", 6),
                       TestHelper.Factories.CreateLocationDirectory.WithId(12),
                       TestHelper.Factories.CreateUser.IdOnly(1243)
                    ),
                   (
                       TestHelper.Factories.CreateLocationDirectory.Parameterized(132, "ABCD", 61),
                       TestHelper.Factories.CreateLocationDirectory.WithId(128),
                       TestHelper.Factories.CreateUser.IdOnly(1282)
                   )
                };

        [TestCaseSource(nameof(ChangeLocationDirectoryParentCallsClientData))]
        public void ChangeLocationDirectoryParentCallsClient((Core.Entities.LocationDirectory directory, Core.Entities.LocationDirectory newParent, Core.Entities.User user) data)
        {
            var environment = new Environment();

            environment.dataAccess.ChangeLocationDirectoryParent(data.directory, data.newParent.Id, data.user);

            var clientParam = environment.mocks.locationClient.UpdateLocationDirectoriesWithHistoryParameter;
            var clientDiff = clientParam.LocationDirectoryDiffs.LocationDirectoyDiffs.First();

            Assert.AreEqual(1, clientParam.LocationDirectoryDiffs.LocationDirectoyDiffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.AreEqual("", clientDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareLocationDirectoryDtoWithLocationDirectory(clientDiff.NewLocationDirectory, data.directory, false));
            Assert.IsTrue(EqualityChecker.CompareLocationDirectoryDtoWithLocationDirectory(clientDiff.OldLocationDirectory, data.directory, false));
            Assert.AreEqual(true, clientDiff.OldLocationDirectory.Alive);
            Assert.AreEqual(true, clientDiff.NewLocationDirectory.Alive);
            Assert.AreEqual(data.directory.ParentId.ToLong(), clientDiff.OldLocationDirectory.ParentId);
            Assert.AreEqual(data.newParent.Id.ToLong(), clientDiff.NewLocationDirectory.ParentId);
        }

        [Test]
        public void LoadPictureForLocationWithoutLocationIdThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.LoadPictureForLocation(null); });
        }

        [TestCase(1)]
        [TestCase(13)]
        public void LoadPictureForLocationCallsClient(long locationId)
        {
            var environment = new Environment();
            environment.dataAccess.LoadPictureForLocation(new LocationId(locationId));

            Assert.AreEqual(locationId, environment.mocks.locationClient.LoadPictureForLocationParameter.LocationId);
            Assert.AreEqual(0, environment.mocks.locationClient.LoadPictureForLocationParameter.FileType);
        }

        static IEnumerable<(byte[], DtoTypes.Picture)> LoadPictureForLocationReturnsCorrectValueData = new List<(byte[], DtoTypes.Picture)>()
        {
            (
                new byte[] {1,2,56,34,67,76},
                new DtoTypes.Picture()
                {
                    Id = 1,
                    FileName = new NullableString() {IsNull = false, Value = "Test1"},
                    FileType = 0,
                    Nodeid = 12,
                    Nodeseqid = 123
                }
            ),
            (
                new byte[] {13,42,56,33,22,16},
                new DtoTypes.Picture()
                {
                    Id = 14,
                    FileName = new NullableString() {IsNull = false, Value = "Path"},
                    FileType = 10,
                    Nodeid = 12222,
                    Nodeseqid = 12
                }
            )
        };

        [TestCaseSource(nameof(LoadPictureForLocationReturnsCorrectValueData))]
        public void LoadPictureForLocationReturnsCorrectValue((byte[] pict, DtoTypes.Picture pictDto) data)
        {
            var environment = new Environment();

            var picture = new Core.Entities.Picture {ImageStream = new MemoryStream(data.pict)};
            environment.mocks.pictureFromZipLoader.LoadPictureFromZipBytesReturnValue = picture;
            environment.mocks.locationClient.LoadPictureForLocationReturnValue.Picture = data.pictDto;
            var result = environment.dataAccess.LoadPictureForLocation(new LocationId(1));
            Assert.AreSame(picture, result);
            Assert.AreEqual(data.pictDto.Id, result.SeqId);
            Assert.AreEqual(data.pictDto.FileName.Value, result.FileName);
            Assert.AreEqual(data.pictDto.FileType, result.FileType);
            Assert.AreEqual(data.pictDto.Nodeid, result.NodeId);
            Assert.AreEqual(data.pictDto.Nodeseqid, result.NodeSeqId);
            Assert.AreEqual(data.pict, ((MemoryStream)result.ImageStream).ToArray());
        }

        static IEnumerable<byte[]> LoadPictureForLocationCallsZipLoaderData = new List<byte[]>()
        {
            new byte[] {1,2,56,34,67,76},
            new byte[] {13,42,56,33,22,16}
        };

        [TestCaseSource(nameof(LoadPictureForLocationCallsZipLoaderData))]
        public void LoadPictureForLocationCallsZipLoader(byte[] pict)
        {
            var environment = new Environment();
            var picture = new DtoTypes.Picture()
            {
                Image = ByteString.CopyFrom(pict)
            };

            environment.mocks.locationClient.LoadPictureForLocationReturnValue.Picture = picture;

            environment.dataAccess.LoadPictureForLocation(new LocationId(1));

            Assert.AreEqual(pict, environment.mocks.pictureFromZipLoader.LoadPictureFromZipBytesParameter);
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    clientFactory = new ClientFactoryMock();
                    channelWrapper = new ChannelWrapperMock();
                    locationClient = new LocationClientMock();
                    channelWrapper.GetLocationClientReturnValue = locationClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                    pictureFromZipLoader = new PictureFromZipLoaderMock();
                }
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
                public LocationClientMock locationClient;
                public PictureFromZipLoaderMock pictureFromZipLoader;
            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new LocationDataAccess(mocks.clientFactory, mocks.pictureFromZipLoader);
            }

            public readonly Mocks mocks;
            public readonly LocationDataAccess dataAccess;
        }
    }
}
