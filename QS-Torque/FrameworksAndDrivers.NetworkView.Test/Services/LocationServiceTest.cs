using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using DtoTypes;
using LocationService;
using NUnit.Framework;
using Server.Core.Entities;
using Server.Core.Enums;
using Server.TestHelper.Factories;
using Server.UseCases.UseCases;
using TestHelper.Checker;
using Location = Server.Core.Entities.Location;
using LocationDiff = Server.Core.Diffs.LocationDiff;
using LocationDirectory = Server.Core.Entities.LocationDirectory;
using LocationDirectoryDiff = Server.Core.Diffs.LocationDirectoryDiff;
using Picture = Server.Core.Entities.Picture;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    public class LocationUseCaseMock : ILocationUseCase
    {
        public List<long> GetReferencedLocPowIdsForLocationIdReturnValue { get; set; } = new List<long>();
        public LocationId GetReferencedLocPowIdsForLocationIdParameter { get; set; }
        public List<LocationDirectoryDiff> InsertLocationDirectoriesWithHistoryParameterDiff { get; set; }
        public bool InsertLocationDirectoriesWithHistoryParameterReturnList { get; set; }
        public List<LocationDirectory> InsertLocationDirectoriesWithHistoryReturnValue { get; set; } = new List<LocationDirectory>();
        public List<LocationDiff> InsertLocationsWithHistoryParameterDiff { get; set; }
        public bool InsertLocationsWithHistoryParameterReturnList { get; set; }
        public List<Location> InsertLocationsWithHistoryReturnValue { get; set; } = new List<Location>();
        public string IsUserIdUniqueParameter { get; set; }
        public bool IsUserIdUniqueReturnValue { get; set; }
        public bool LoadLocationDirectoriesCalled { get; set; }
        public List<LocationDirectory> LoadLocationDirectoriesReturnValue { get; set; } = new List<LocationDirectory>();
        public int LoadLocationsParameterIndex { get; set; }
        public int LoadLocationsParameterSize { get; set; }
        public List<Location> LoadLocationsReturnValue { get; set; } = new List<Location>();
        public List<LocationDirectoryDiff> UpdateLocationDirectoriesWithHistoryParameter { get; set; }
        public List<LocationDirectory> UpdateLocationDirectoriesWithHistoryReturnValue { get; set; } = new List<LocationDirectory>();
        public List<LocationDiff> UpdateLocationsWithHistoryParameter { get; set; }
        public List<Location> UpdateLocationsWithHistoryReturnValue { get; set; } = new List<Location>();
        public string GetLocationCommentReturnValue { get; set; }
        public LocationId GetLocationCommentParameter { get; set; }
        public Picture LoadPictureForLocationReturnValue { get; set; } = new Picture();
        public int LoadPictureForLocationParameterFileType { get; set; }
        public LocationId LoadPictureForLocationParameterLocationId { get; set; }
        public List<Location> LoadLocationsByIdsReturnValue { get; set; } = new List<Location>();
        public List<LocationId> LoadLocationsByIdsParameter { get; set; }

        public List<long> GetReferencedLocPowIdsForLocationId(LocationId id)
        {
            GetReferencedLocPowIdsForLocationIdParameter = id;
            return GetReferencedLocPowIdsForLocationIdReturnValue;
        }

        public string GetLocationComment(LocationId locationId)
        {
            GetLocationCommentParameter = locationId;
            return GetLocationCommentReturnValue;
        }

        public Picture LoadPictureForLocation(LocationId locationId, int fileType)
        {
            LoadPictureForLocationParameterLocationId = locationId;
            LoadPictureForLocationParameterFileType = fileType;
            return LoadPictureForLocationReturnValue;
        }

        public List<Location> LoadLocationsByIds(List<LocationId> ids)
        {
            LoadLocationsByIdsParameter = ids;
            return LoadLocationsByIdsReturnValue;
        }

        public List<LocationDirectory> InsertLocationDirectoriesWithHistory(List<LocationDirectoryDiff> locationDirectoryDiff, bool returnList)
        {
            InsertLocationDirectoriesWithHistoryParameterDiff = locationDirectoryDiff;
            InsertLocationDirectoriesWithHistoryParameterReturnList = returnList;
            return InsertLocationDirectoriesWithHistoryReturnValue;
        }

        public List<Location> InsertLocationsWithHistory(List<LocationDiff> locationDiff, bool returnList)
        {
            InsertLocationsWithHistoryParameterDiff = locationDiff;
            InsertLocationsWithHistoryParameterReturnList = returnList;
            return InsertLocationsWithHistoryReturnValue;
        }

        public bool IsUserIdUnique(string userId)
        {
            IsUserIdUniqueParameter = userId;
            return IsUserIdUniqueReturnValue;
        }

        public List<LocationDirectory> LoadLocationDirectories()
        {
            LoadLocationDirectoriesCalled = true;
            return LoadLocationDirectoriesReturnValue;
        }

        public List<Location> LoadLocations(int index, int size)
        {
            LoadLocationsParameterIndex = index;
            LoadLocationsParameterSize = size;
            return LoadLocationsReturnValue;
        }

        public List<LocationDirectory> UpdateLocationDirectoriesWithHistory(List<LocationDirectoryDiff> locationDirectoryDiff)
        {
            UpdateLocationDirectoriesWithHistoryParameter = locationDirectoryDiff;
            return UpdateLocationDirectoriesWithHistoryReturnValue;
        }

        public List<Location> UpdateLocationsWithHistory(List<LocationDiff> locationDiff)
        {
            UpdateLocationsWithHistoryParameter = locationDiff;
            return UpdateLocationsWithHistoryReturnValue;
        }

        public List<LocationDirectory> LoadAllLocationDirectories()
        {
            LoadLocationDirectoriesCalled = true;
            return LoadLocationDirectoriesReturnValue;
        }

        public List<Location> LoadDeletedLocations(int index, int size)
        {
            throw new NotImplementedException();
        }
    }

    public class LocationServiceTest
    {
        [Test]
        public void LoadLocationDirectoriesCallsUseCase()
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);

            service.LoadLocationDirectories(new NoParams(), null);

            Assert.IsTrue(useCase.LoadLocationDirectoriesCalled);
        }

        private static IEnumerable<List<LocationDirectory>> locationDirectoryData = new List<List<LocationDirectory>>()
        {
            new List<LocationDirectory>()
            {
                Server.TestHelper.Factories.CreateLocationDirectory.Parameterized(1, "test 1", 3, true),
                Server.TestHelper.Factories.CreateLocationDirectory.Parameterized(3, "abc", 5, false)

            },
            new List<LocationDirectory>()
            {
                Server.TestHelper.Factories.CreateLocationDirectory.Parameterized(9, "cxd", 53, false)
            }
        };

        [TestCaseSource(nameof(locationDirectoryData))]
        public void LoadLocationDirectoriesReturnsCorrectValue(List<LocationDirectory> locationDirectories)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);
            useCase.LoadLocationDirectoriesReturnValue = locationDirectories;

            var result = service.LoadLocationDirectories(new NoParams(), null);

            var comparer = new Func<LocationDirectory, DtoTypes.LocationDirectory, bool>((locationDirectory, dtoLocationDirectory) =>
                EqualityChecker.CompareLocationDirectoryDtoWithLocationDirectory(dtoLocationDirectory, locationDirectory)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(locationDirectories, result.Result.LocationDirectories, comparer);
        }

        static IEnumerable<(ListOfLocationDirectoryDiff, bool)> InsertUpdateLocationDirectoryWithHistoryData = new List<(ListOfLocationDirectoryDiff, bool)>
        {
            (
                new ListOfLocationDirectoryDiff()
                {
                    LocationDirectoyDiffs =
                    {
                        new DtoTypes.LocationDirectoryDiff()
                        {
                            UserId = 1,
                            Comment = "",
                            OldLocationDirectory = DtoFactory.CreateLocationDirectory(1, "test", 9, true),
                            NewLocationDirectory = DtoFactory.CreateLocationDirectory(1, "abc", 91, false)
                        },
                        new DtoTypes.LocationDirectoryDiff()
                        {
                            UserId = 2,
                            Comment = "ABCDEFG",
                            OldLocationDirectory =  DtoFactory.CreateLocationDirectory(1, "de", 93, false),
                            NewLocationDirectory = DtoFactory.CreateLocationDirectory(13, "ho", 9, true)
                        }
                    }
                },
                true
             ),
            (
                new ListOfLocationDirectoryDiff()
                {
                    LocationDirectoyDiffs =
                    {
                        new DtoTypes.LocationDirectoryDiff()
                        {
                            UserId = 1,
                            Comment = "",
                            OldLocationDirectory =  DtoFactory.CreateLocationDirectory(1, "Halle 1", 93, false),
                            NewLocationDirectory = DtoFactory.CreateLocationDirectory(13, "Halle X", 922, true)
                        }
                    }
                },
                false
            )
        };

        [TestCaseSource(nameof(InsertUpdateLocationDirectoryWithHistoryData))]
        public void InsertLocationDirectoriesWithHistoryCallsUseCase((ListOfLocationDirectoryDiff locationDirectoryDiffs, bool returnList) data)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);

            var request = new InsertLocationDirectoriesWithHistoryRequest()
            {
                LocationDirectoryDiffs = data.locationDirectoryDiffs,
                ReturnList = data.returnList
            };

            service.InsertLocationDirectoriesWithHistory(request, null);

            Assert.AreEqual(data.returnList, useCase.InsertLocationDirectoriesWithHistoryParameterReturnList);

            var comparer = new Func<DtoTypes.LocationDirectoryDiff, LocationDirectoryDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareLocationDirectoryDtoWithLocationDirectory(dtoDiff.OldLocationDirectory, diff.GetOldLocationDirectory()) &&
                EqualityChecker.CompareLocationDirectoryDtoWithLocationDirectory(dtoDiff.NewLocationDirectory, diff.GetNewLocationDirectory())
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.locationDirectoryDiffs.LocationDirectoyDiffs, useCase.InsertLocationDirectoriesWithHistoryParameterDiff, comparer);
        }

        [TestCaseSource(nameof(locationDirectoryData))]
        public void InsertLocationDirectoriesWithHistoryReturnsCorrectValue(List<LocationDirectory> locationDirectories)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);

            useCase.InsertLocationDirectoriesWithHistoryReturnValue = locationDirectories;

            var request = new InsertLocationDirectoriesWithHistoryRequest()
            {
                LocationDirectoryDiffs = new ListOfLocationDirectoryDiff()
            };

            var result = service.InsertLocationDirectoriesWithHistory(request, null).Result;

            var comparer = new Func<LocationDirectory, DtoTypes.LocationDirectory, bool>((locationDirectory, dtoLocationDirectory) =>
                EqualityChecker.CompareLocationDirectoryDtoWithLocationDirectory(dtoLocationDirectory, locationDirectory)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(locationDirectories, result.LocationDirectories, comparer);
        }

        [TestCaseSource(nameof(InsertUpdateLocationDirectoryWithHistoryData))]
        public void UpdateLocationDirectoriesWithHistoryCallsUseCase((ListOfLocationDirectoryDiff locationDirectoryDiffs, bool returnList) data)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);

            var request = new UpdateLocationDirectoriesWithHistoryRequest()
            {
                LocationDirectoryDiffs = data.locationDirectoryDiffs
            };

            service.UpdateLocationDirectoriesWithHistory(request, null);

            var comparer = new Func<DtoTypes.LocationDirectoryDiff, LocationDirectoryDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareLocationDirectoryDtoWithLocationDirectory(dtoDiff.OldLocationDirectory, diff.GetOldLocationDirectory()) &&
                EqualityChecker.CompareLocationDirectoryDtoWithLocationDirectory(dtoDiff.NewLocationDirectory, diff.GetNewLocationDirectory())
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.locationDirectoryDiffs.LocationDirectoyDiffs,
                useCase.UpdateLocationDirectoriesWithHistoryParameter, comparer);
        }

        [TestCaseSource(nameof(locationDirectoryData))]
        public void UpdateLocationDirectoriesWithHistoryReturnsCorrectValue(List<LocationDirectory> locationDirectories)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);

            useCase.UpdateLocationDirectoriesWithHistoryReturnValue = locationDirectories;

            var request = new UpdateLocationDirectoriesWithHistoryRequest()
            {
                LocationDirectoryDiffs = new ListOfLocationDirectoryDiff()
            };

            var result = service.UpdateLocationDirectoriesWithHistory(request, null).Result;

            var comparer = new Func<LocationDirectory, DtoTypes.LocationDirectory, bool>((locationDirectory, dtoLocationDirectory) =>
                EqualityChecker.CompareLocationDirectoryDtoWithLocationDirectory(dtoLocationDirectory, locationDirectory)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(locationDirectories, result.LocationDirectories, comparer);
        }

        [TestCase(1, 6)]
        [TestCase(12, 36)]
        public void LoadLocationsCallsUseCase(int index, int size)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);

            service.LoadLocations(new LoadLocationsRequest() { Index = index, Size = size }, null);

            Assert.AreEqual(index, useCase.LoadLocationsParameterIndex);
            Assert.AreEqual(size, useCase.LoadLocationsParameterSize);
        }

        [TestCaseSource(nameof(locationData))]
        public void LoadLocationsReturnsCorrectValue(List<Location> locations)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);
            useCase.LoadLocationsReturnValue = locations;

            var result = service.LoadLocations(new LoadLocationsRequest(), null);

            var comparer = new Func<Location, DtoTypes.Location, bool>((location, dtoLocation) =>
                EqualityChecker.CompareLocationDtoWithLocation(dtoLocation, location)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(locations, result.Result.Locations, comparer);
        }


        private static IEnumerable<ListOfLongs> LoadLocationsCallsUseCaseData =
            new List<ListOfLongs>()
            {
                new ListOfLongs()
                {
                    Values =
                    {
                        new Long() {Value = 1},
                        new Long() {Value = 1113},
                        new Long() {Value = 14},
                        new Long() {Value = 13},
                    }
                },
                new ListOfLongs()
                {
                    Values =
                    {
                        new Long() {Value = 41},
                        new Long() {Value = 24},
                    }
                }
            };

        [TestCaseSource(nameof(LoadLocationsCallsUseCaseData))]
        public void LoadLocationsByIdsCallsUseCase(ListOfLongs ids)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);

            service.LoadLocationsByIds(ids, null);

            Assert.AreEqual(ids.Values.Select(x => x.Value).ToList(), useCase.LoadLocationsByIdsParameter.Select(x => x.ToLong()));
        }

        [TestCaseSource(nameof(locationData))]
        public void LoadLocationsByIdsReturnsCorrectValue(List<Location> locations)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);
            useCase.LoadLocationsByIdsReturnValue = locations;

            var result = service.LoadLocationsByIds(new ListOfLongs(), null);

            var comparer = new Func<Location, DtoTypes.Location, bool>((location, dtoLocation) =>
                EqualityChecker.CompareLocationDtoWithLocation(dtoLocation, location)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(locations, result.Result.Locations, comparer);
        }

        static IEnumerable<(ListOfLocationDiff, bool)> InsertUpdateLocationWithHistoryData = new List<(ListOfLocationDiff, bool)>
        {
            (
                new ListOfLocationDiff()
                {
                    LocationDiffs =
                    {
                        new DtoTypes.LocationDiff()
                        {
                            User = new DtoTypes.User() { UserId = 1 },
                            Comment = "",
                            OldLocation = DtoFactory.CreateLocation(1, "test", "sst", 1,"A", "B", false,
                                0,DtoFactory.CreateToleranceClassDto(1, "tol1", true,1,2, false),
                                DtoFactory.CreateToleranceClassDto(13, "tol3", true,1,2, false),10,100,20,200,
                                5,50,3,"Kommentar", true),
                            NewLocation = DtoFactory.CreateLocation(1, "test 1", "sst 1", 11,"D", "C", true,
                                1,DtoFactory.CreateToleranceClassDto(1, "tol1", true,1,2, false),
                                DtoFactory.CreateToleranceClassDto(13, "tol5", false,1,2, false),101,1001,201,2001,
                                51,501,31,"Kommentar A", false)
                        },
                        new DtoTypes.LocationDiff()
                        {
                            User = new DtoTypes.User() { UserId = 2 },
                            Comment = "ABCDEFG",
                            OldLocation =  DtoFactory.CreateLocation(12, "test 2", "x", 1,"L", "Z", false,
                                0,DtoFactory.CreateToleranceClassDto(1, "tol1", true,1,2, false),
                                DtoFactory.CreateToleranceClassDto(14, "freie Eingabe", true,1,8, false),
                                102,1002,202,2002,
                                52,502,32,"XXX", true),
                            NewLocation = DtoFactory.CreateLocation(12, " aaas s", "ddf", 13,"A", "B", true,
                                3,DtoFactory.CreateToleranceClassDto(15, "freie Eingabe", true,1,2, false),
                                DtoFactory.CreateToleranceClassDto(1, "tol1", true,1,2, false),105,1005,205,2005,
                                55,505,35,"Test", true)
                        }
                    }
                },
                true
             ),
            (
                new ListOfLocationDiff()
                {
                    LocationDiffs =
                    {
                        new DtoTypes.LocationDiff()
                        {
                            User = new DtoTypes.User() { UserId = 1 },
                            Comment = "",
                            OldLocation =  DtoFactory.CreateLocation(12, "test 2", "x", 1,"L", "Z", false,
                                0,DtoFactory.CreateToleranceClassDto(1, "tol1", true,1,2, false),
                                DtoFactory.CreateToleranceClassDto(19, "freie Eingabe", false,14,26, false),102,1002,202,2002,
                                52,502,32,"X", true),
                            NewLocation = DtoFactory.CreateLocation(12, " aaas s", "ddf", 1323,"A", "B", true,
                                3,DtoFactory.CreateToleranceClassDto(1, "XXXX", false,1,2, false),
                                DtoFactory.CreateToleranceClassDto(14, "tol1", true,1,2, false),105,1005,205,2005,
                                55,505,35,"W", false)
                        }
                    }
                },
                false
            )
        };

        [TestCaseSource(nameof(InsertUpdateLocationWithHistoryData))]
        public void InsertLocationsWithHistoryCallsUseCase((ListOfLocationDiff locationDiffs, bool returnList) data)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);

            var request = new InsertLocationsWithHistoryRequest()
            {
                LocationDiffs = data.locationDiffs,
                ReturnList = data.returnList
            };

            service.InsertLocationsWithHistory(request, null);

            Assert.AreEqual(data.returnList, useCase.InsertLocationsWithHistoryParameterReturnList);

            var comparer = new Func<DtoTypes.LocationDiff, LocationDiff, bool>((dtoDiff, diff) =>
                dtoDiff.User.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareLocationDtoWithLocation(dtoDiff.OldLocation, diff.GetOldLocation()) &&
                EqualityChecker.CompareLocationDtoWithLocation(dtoDiff.NewLocation, diff.GetNewLocation())
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.locationDiffs.LocationDiffs,
                useCase.InsertLocationsWithHistoryParameterDiff, comparer);
        }

        [TestCaseSource(nameof(locationData))]
        public void InsertLocationsWithHistoryReturnsCorrectValue(List<Location> locations)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);

            useCase.InsertLocationsWithHistoryReturnValue = locations;

            var request = new InsertLocationsWithHistoryRequest()
            {
                LocationDiffs = new ListOfLocationDiff()
            };

            var result = service.InsertLocationsWithHistory(request, null).Result;

            var comparer = new Func<Location, DtoTypes.Location, bool>((location, dtoLocation) =>
                EqualityChecker.CompareLocationDtoWithLocation(dtoLocation, location)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(locations, result.Locations, comparer);
        }

        [TestCaseSource(nameof(InsertUpdateLocationWithHistoryData))]
        public void UpdateLocationsWithHistoryCallsUseCase((ListOfLocationDiff locationDiffs, bool returnList) data)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);

            var request = new UpdateLocationsWithHistoryRequest()
            {
                LocationDiffs = data.locationDiffs
            };

            service.UpdateLocationsWithHistory(request, null);

            var comparer = new Func<DtoTypes.LocationDiff, LocationDiff, bool>((dtoDiff, diff) =>
                dtoDiff.User.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareLocationDtoWithLocation(dtoDiff.OldLocation, diff.GetOldLocation()) &&
                EqualityChecker.CompareLocationDtoWithLocation(dtoDiff.NewLocation, diff.GetNewLocation())
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.locationDiffs.LocationDiffs,
                useCase.UpdateLocationsWithHistoryParameter, comparer);
        }

        [TestCaseSource(nameof(locationData))]
        public void UpdateLocationsWithHistoryReturnsCorrectValue(List<Location> locations)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);

            useCase.UpdateLocationsWithHistoryReturnValue = locations;

            var request = new UpdateLocationsWithHistoryRequest()
            {
                LocationDiffs = new ListOfLocationDiff()
            };

            var result = service.UpdateLocationsWithHistory(request, null).Result;

            var comparer = new Func<Location, DtoTypes.Location, bool>((location, dtoLocation) =>
                EqualityChecker.CompareLocationDtoWithLocation(dtoLocation, location)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(locations, result.Locations, comparer);
        }

        [TestCase(10)]
        [TestCase(99)]
        public void GetLocationCommentCallsUseCase(long locationId)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);
            useCase.GetLocationCommentReturnValue = "";

            service.GetLocationComment(new Long() { Value = locationId }, null);

            Assert.AreEqual(locationId, useCase.GetLocationCommentParameter.ToLong());
        }

        [TestCase("blub2020")]
        [TestCase("Testkommentar")]
        public void GetLocationCommentReturnsCorrectValue(string comment)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);
            useCase.GetLocationCommentReturnValue = comment;

            var result = service.GetLocationComment(new Long() { Value = 1 }, null);

            Assert.AreEqual(comment, result.Result.Value);
        }

        [TestCase("1234")]
        [TestCase("Schraubstelle")]
        public void IsUserIdUniqueCallsUseCase(string userId)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);

            service.IsUserIdUnique(new BasicTypes.String() { Value = userId }, null);

            Assert.AreEqual(userId, useCase.IsUserIdUniqueParameter);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsUserIdUniqueReturnsCorrectValue(bool isUnique)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);
            useCase.IsUserIdUniqueReturnValue = isUnique;

            var result = service.IsUserIdUnique(new BasicTypes.String() { Value = "" }, null);

            Assert.AreEqual(isUnique, result.Result.Value);
        }

        [TestCase(1)]
        [TestCase(99)]
        public void GetReferencedLocPowIdsForLocationIdCallsUseCase(long locId)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);

            service.GetReferencedLocPowIdsForLocationId(new Long() { Value = locId }, null);

            Assert.AreEqual(locId, useCase.GetReferencedLocPowIdsForLocationIdParameter.ToLong());
        }

        static IEnumerable<List<long>> GetReferencedLocPowIdsForLocationIdParameterData = new List<List<long>>()
        {
            new List<long>{1,2,3,4},
            new List<long>{5,8,9,11}
        };

        [TestCaseSource(nameof(GetReferencedLocPowIdsForLocationIdParameterData))]
        public void GetReferencedLocPowIdsForLocationIdReturnsCorrectValue(List<long> data)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);
            useCase.GetReferencedLocPowIdsForLocationIdReturnValue = data;

            var result = service.GetReferencedLocPowIdsForLocationId(new Long(), null);

            var comparer = new Func<long, Long, bool>((val, dtoVal) =>
                val == dtoVal.Value
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data, result.Result.Values, comparer);
        }


        private static IEnumerable<List<Location>> locationData = new List<List<Location>>()
        {
            new List<Location>()
            {
                Server.TestHelper.Factories.CreateLocation.Parameterized(1,"2342345", "abcd",
                    "Kommentar 1", "A", "B", true,10,
                    15, 12,LocationControlledBy.Angle,5,12,56,
                    6,CreateToleranceClass.WithId(1), 10, CreateToleranceClass.WithId(4), true),
                Server.TestHelper.Factories.CreateLocation.Parameterized(11,"SST", "SSTNAME",
                    "Kommentar DE", "A", "B", true,10,
                    151, 121,LocationControlledBy.Angle,51,121,561,
                    61,CreateToleranceClass.WithId(11), 101, CreateToleranceClass.WithId(41), false)

            },
            new List<Location>()
            {
                Server.TestHelper.Factories.CreateLocation.Parameterized(16,"1112", "cxy",
                    "Kommentar 17", "X", "D", false,107,
                    157, 127,LocationControlledBy.Torque,57,127,567,
                    670, CreateToleranceClass.WithId(17), 107, CreateToleranceClass.WithId(47), false)
            }
        };

        [TestCase(12,45)]
        [TestCase(132, 4445)]
        public void LoadPictureForLocationCallsUseCase(long locationId, int fileType)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);
            useCase.LoadPictureForLocationReturnValue = new Picture()
            {
                FileName = ""
            };

            var request = new LoadPictureForLocationRequest()
            {
                LocationId = locationId,
                FileType = fileType,
            };
            service.LoadPictureForLocation(request, null);

            Assert.AreEqual(locationId, useCase.LoadPictureForLocationParameterLocationId.ToLong());
            Assert.AreEqual(fileType, useCase.LoadPictureForLocationParameterFileType);
        }

        private static IEnumerable<Picture> LoadPictureForLocationReturnsCorrectValueData =
            new List<Picture>()
            {
                new Picture()
                {
                    NodeId = 12,
                    FileName = "234536",
                    FileType = 1,
                    NodeSeqId = 2,
                    SeqId = 1,
                    PictureBytes = new byte[]{12,23,5,6,7}
                },
                new Picture()
                {
                    NodeId = 88,
                    FileName = "path",
                    FileType = 12,
                    NodeSeqId = 32,
                    SeqId = 14,
                    PictureBytes = new byte[]{1,2,3,5,6,12,23,5,6,7}
                }
            };

        [TestCaseSource(nameof(LoadPictureForLocationReturnsCorrectValueData))]
        public void LoadPictureForLocationReturnsCorrectValue(Picture picture)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);
            useCase.LoadPictureForLocationReturnValue = picture;

            var result = service.LoadPictureForLocation(new LoadPictureForLocationRequest(), null);

            Assert.AreEqual(picture.SeqId, result.Result.Picture.Id);
            Assert.AreEqual(picture.NodeId, result.Result.Picture.Nodeid);
            Assert.AreEqual(picture.FileName, result.Result.Picture.FileName.Value);
            Assert.AreEqual(picture.FileType, result.Result.Picture.FileType);
            Assert.AreEqual(picture.NodeSeqId, result.Result.Picture.Nodeseqid);
            Assert.AreEqual(picture.PictureBytes, result.Result.Picture.Image.ToByteArray());
        }
    }
}
