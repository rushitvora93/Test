using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.TestHelper.Factories;
using Server.TestHelper.Mocks;
using Server.UseCases.UseCases;
using State;
using TestHelper.Checker;

namespace Server.UseCases.Test.UseCases
{
    public class LocationDataAccessMock : ILocationDataAccess
    {
        public enum LocationDataAccessFunction
        {
            InsertLocationsWithHistory,
            UpdateLocationsWithHistory,
            Commit
        }

        public List<long> GetReferencedLocPowIdsForLocationIdReturnValue { get; set; } = new List<long>();
        public LocationId GetReferencedLocPowIdsForLocationIdParameter { get; set; }
        public List<LocationDiff> InsertLocationsWithHistoryParameterDiff { get; set; }
        public bool InsertLocationsWithHistoryParameterReturnList { get; set; }
        public List<Location> InsertLocationsWithHistoryReturnValue { get; set; } = new List<Location>();
        public string IsUserIdUniqueParameter { get; set; }
        public bool IsUserIdUniqueReturnValue { get; set; }
        public int LoadLocationsParameterIndex { get; set; }
        public int LoadLocationsParameterSize { get; set; }
        public List<Location> LoadLocationsReturnValue { get; set; } = new List<Location>();
        public List<LocationDiff> UpdateLocationsWithHistoryParameter { get; set; }
        public List<Location> UpdateLocationsWithHistoryReturnValue { get; set; } = new List<Location>();
        public bool LoadLocationsCalled { get; set; }
        public List<LocationDataAccessFunction> CalledFunctions { get; set; } = new List<LocationDataAccessFunction>();
        public List<Location> LoadLocationsByIdsReturnValue { get; set; }
        public List<LocationId> LoadLocationsByIdsParameter { get; set; }

        public void Commit()
        {
            CalledFunctions.Add(LocationDataAccessFunction.Commit);
        }

        public List<long> GetReferencedLocPowIdsForLocationId(LocationId id)
        {
            GetReferencedLocPowIdsForLocationIdParameter = id;
            return GetReferencedLocPowIdsForLocationIdReturnValue;
        }

        public List<Location> LoadLocationsByIds(List<LocationId> ids)
        {
            LoadLocationsByIdsParameter = ids;
            return LoadLocationsByIdsReturnValue;
        }

        public List<Location> InsertLocationsWithHistory(List<LocationDiff> locationDiff, bool returnList)
        {
            CalledFunctions.Add(LocationDataAccessFunction.InsertLocationsWithHistory);
            InsertLocationsWithHistoryParameterDiff = locationDiff;
            InsertLocationsWithHistoryParameterReturnList = returnList;
            return InsertLocationsWithHistoryReturnValue;
        }

        public bool IsUserIdUnique(string userId)
        {
            IsUserIdUniqueParameter = userId;
            return IsUserIdUniqueReturnValue;
        }      

        public List<Location> LoadLocations(int index, int size)
        {
            LoadLocationsCalled = true;
            LoadLocationsParameterIndex = index;
            LoadLocationsParameterSize = size;
            return LoadLocationsReturnValue;
        }

        public List<Location> UpdateLocationsWithHistory(List<LocationDiff> locationDiff)
        {
            CalledFunctions.Add(LocationDataAccessFunction.UpdateLocationsWithHistory);
            UpdateLocationsWithHistoryParameter = locationDiff;
            return UpdateLocationsWithHistoryReturnValue;
        }

        public List<Location> LoadDeletedLocations(int index, int size)
        {
            throw new NotImplementedException();
        }
    }

    public class LocationDirectoryDataAccessMock : ILocationDirectoryDataAccess
    {
        public bool CommitCalled { get; set; }
        public List<LocationDirectoryDiff> InsertLocationDirectoriesWithHistoryParameterDiff { get; set; }
        public bool InsertLocationDirectoriesWithHistoryParameterReturnList { get; set; }
        public List<LocationDirectory> InsertLocationDirectoriesWithHistoryReturnValue { get; set; } = new List<LocationDirectory>();
        public bool LoadLocationDirectoriesCalled { get; set; }
        public List<LocationDirectory> LoadLocationDirectoriesReturnValue { get; set; } = new List<LocationDirectory>();
        public List<LocationDirectoryDiff> UpdateLocationDirectoriesWithHistoryParameter { get; set; }
        public List<LocationDirectory> UpdateLocationDirectoriesWithHistoryReturnValue { get; set; } = new List<LocationDirectory>();
        public bool UpdateLocationDirectoriesWithHistoryCalled { get; set; }
        public bool InsertLocationDirectoriesWithHistoryCalled { get; set; }

        public void Commit()
        {
            CommitCalled = true;
        }

        public List<LocationDirectory> LoadLocationDirectories()
        {
            LoadLocationDirectoriesCalled = true;
            return LoadLocationDirectoriesReturnValue;
        }
        public List<LocationDirectory> UpdateLocationDirectoriesWithHistory(List<LocationDirectoryDiff> locationDirectoryDiffs)
        {
            UpdateLocationDirectoriesWithHistoryCalled = true;
            UpdateLocationDirectoriesWithHistoryParameter = locationDirectoryDiffs;
            return UpdateLocationDirectoriesWithHistoryReturnValue;
        }

        public List<LocationDirectory> InsertLocationDirectoriesWithHistory(List<LocationDirectoryDiff> locationDirectoryDiffs, bool returnList)
        {
            InsertLocationDirectoriesWithHistoryCalled = true;
            InsertLocationDirectoriesWithHistoryParameterDiff = locationDirectoryDiffs;
            InsertLocationDirectoriesWithHistoryParameterReturnList = returnList;
            return InsertLocationDirectoriesWithHistoryReturnValue;
        }

        public List<LocationDirectory> LoadAllLocationDirectories()
        {
            throw new NotImplementedException();
        }
    }

    public class PictureDataAccessMock : IPictureDataAccess
    {
        public Picture GetQstPicture(int fileType, NodeId nodeId, long nodeseqid)
        {
            GetQstPictureParameterFileType = fileType;
            GetQstPictureParameterNodeId = nodeId;
            GetQstPictureParameterFileTypeNodeSeqId = nodeseqid;
            return GetQstPictureReturnValue;
        }

        public int GetQstPictureParameterFileType { get; set; }
        public NodeId GetQstPictureParameterNodeId { get; set; }
        public long GetQstPictureParameterFileTypeNodeSeqId { get; set; }
        public Picture GetQstPictureReturnValue { get; set; }
    }

    public class LocationUseCaseTest
    {
        [Test]
        public void LoadLocationDirectoriesCallsDataAccess()
        {
            var environment = new Environment();

            environment.useCase.LoadLocationDirectories();

            Assert.IsTrue(environment.mocks.locationDirectoryDataAccess.LoadLocationDirectoriesCalled);
        }

        [Test]
        public void LoadLocationDirectoriesReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<LocationDirectory>();
            environment.mocks.locationDirectoryDataAccess.LoadLocationDirectoriesReturnValue = entities;

            Assert.AreSame(entities, environment.useCase.LoadLocationDirectories());
        }

        [TestCase(1, 5)]
        [TestCase(132, 435)]
        public void LoadLocationsCallsDataAccess(int index, int size)
        {
            var environment = new Environment();

            environment.useCase.LoadLocations(index, size);

            Assert.IsTrue(environment.mocks.locationDataAccess.LoadLocationsCalled);
            Assert.AreEqual(index, environment.mocks.locationDataAccess.LoadLocationsParameterIndex);
            Assert.AreEqual(size, environment.mocks.locationDataAccess.LoadLocationsParameterSize);
        }

        [Test]
        public void LoadLocationsReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<Location>();
            environment.mocks.locationDataAccess.LoadLocationsReturnValue = entities;

            Assert.AreSame(entities, environment.useCase.LoadLocations(1, 1));
        }

        [TestCase]
        public void LoadLocationsByIdsCallsDataAccess()
        {
            var environment = new Environment();

            var ids = new List<LocationId>();
            environment.useCase.LoadLocationsByIds(ids);

            Assert.AreSame(ids, environment.mocks.locationDataAccess.LoadLocationsByIdsParameter);
        }

        [Test]
        public void LoadLocationsByIdsReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<Location>();
            environment.mocks.locationDataAccess.LoadLocationsByIdsReturnValue = entities;

            var result = environment.useCase.LoadLocationsByIds(new List<LocationId>());
            Assert.AreSame(entities, result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InsertLocationDirectoriesWithHistoryCallsDataAccess(bool returnList)
        {
            var environment = new Environment();

            var diffs = new List<LocationDirectoryDiff>();
            environment.useCase.InsertLocationDirectoriesWithHistory(diffs, returnList);

            Assert.AreSame(diffs, environment.mocks.locationDirectoryDataAccess.InsertLocationDirectoriesWithHistoryParameterDiff);
            Assert.AreEqual(returnList, environment.mocks.locationDirectoryDataAccess.InsertLocationDirectoriesWithHistoryParameterReturnList);
        }

        [Test]
        public void InsertLocationDirectoriesWithHistoryCallsCommitAfterWork()
        {
            var environment = new Environment();

            environment.useCase.InsertLocationDirectoriesWithHistory(new List<LocationDirectoryDiff>(), false);

            Assert.IsTrue(environment.mocks.locationDirectoryDataAccess.InsertLocationDirectoriesWithHistoryCalled);
            Assert.IsTrue(environment.mocks.locationDirectoryDataAccess.CommitCalled);
        }

        [Test]
        public void InsertLocationDirectoriesWithHistoryReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<LocationDirectory>();
            environment.mocks.locationDirectoryDataAccess.InsertLocationDirectoriesWithHistoryReturnValue = entities;

            var returnValue = environment.useCase.InsertLocationDirectoriesWithHistory(null, true);

            Assert.AreSame(entities, returnValue);
        }

        [Test]
        public void UpdateLocationDirectoriesWithHistoryCallsDataAccess()
        {
            var environment = new Environment();

            var diffs = new List<LocationDirectoryDiff>();
            environment.useCase.UpdateLocationDirectoriesWithHistory(diffs);

            Assert.AreSame(diffs, environment.mocks.locationDirectoryDataAccess.UpdateLocationDirectoriesWithHistoryParameter);
        }

        [Test]
        public void UpdateLocationDirectoriesWithHistoryCallsCommitAfterWork()
        {
            var environment = new Environment();

            environment.useCase.UpdateLocationDirectoriesWithHistory(new List<LocationDirectoryDiff>());

            Assert.IsTrue(environment.mocks.locationDirectoryDataAccess.UpdateLocationDirectoriesWithHistoryCalled);
            Assert.IsTrue(environment.mocks.locationDirectoryDataAccess.CommitCalled);
        }

        [Test]
        public void UpdateLocationDirectoriesWithHistoryReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<LocationDirectory>();
            environment.mocks.locationDirectoryDataAccess.UpdateLocationDirectoriesWithHistoryReturnValue = entities;

            var returnValue = environment.useCase.UpdateLocationDirectoriesWithHistory(null);

            Assert.AreSame(entities, returnValue);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InsertLocationsWithHistoryCallsDataAccess(bool returnList)
        {
            var environment = new Environment();

            var diffs = new List<LocationDiff>();
            environment.useCase.InsertLocationsWithHistory(diffs, returnList);

            Assert.AreSame(diffs, environment.mocks.locationDataAccess.InsertLocationsWithHistoryParameterDiff);
            Assert.AreEqual(returnList, environment.mocks.locationDataAccess.InsertLocationsWithHistoryParameterReturnList);
        }

        [Test]
        public void InsertLocationsWithHistoryCallsCommitAfterWork()
        {
            var environment = new Environment();

            environment.useCase.InsertLocationsWithHistory(new List<LocationDiff>(), false);

            Assert.AreEqual(LocationDataAccessMock.LocationDataAccessFunction.Commit, environment.mocks.locationDataAccess.CalledFunctions.Last());
        }

        [Test]
        public void InsertLocationsWithHistoryReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<Location>();
            environment.mocks.locationDataAccess.InsertLocationsWithHistoryReturnValue = entities;

            var returnValue = environment.useCase.InsertLocationsWithHistory(null, true);

            Assert.AreSame(entities, returnValue);
        }

        [Test]
        public void UpdateLocationsWithHistoryCallsDataAccess()
        {
            var environment = new Environment();

            var diffs = new List<LocationDiff>();
            environment.useCase.UpdateLocationsWithHistory(diffs);

            Assert.AreSame(diffs, environment.mocks.locationDataAccess.UpdateLocationsWithHistoryParameter);
        }

        [Test]
        public void UpdateLocationsWithHistoryCallsCommitAfterWork()
        {
            var environment = new Environment();

            environment.useCase.UpdateLocationsWithHistory(new List<LocationDiff>());

            Assert.AreEqual(LocationDataAccessMock.LocationDataAccessFunction.Commit, environment.mocks.locationDataAccess.CalledFunctions.Last());
        }

        [Test]
        public void UpdateLocationsWithHistoryReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<Location>();
            environment.mocks.locationDataAccess.UpdateLocationsWithHistoryReturnValue = entities;

            var returnValue = environment.useCase.UpdateLocationsWithHistory(null);

            Assert.AreSame(entities, returnValue);
        }

        static IEnumerable<List<(long, string, long)>> UpdateLocationsWithHistoryCallsInsertQstCommentsData = new List<List<(long, string, long)>>()
        {
            new List<(long, string, long)>()
            {
                (1, "Kommentar1", 88),
                (2, "Kommentar2", 77)
            },
            new List<(long, string, long)>()
            {
                (3, "Kommentar3", 55)
            }
        };

        [TestCaseSource(nameof(UpdateLocationsWithHistoryCallsInsertQstCommentsData))]
        public void UpdateLocationsWithHistoryCallsInsertQstComments(List<(long userId, string comment, long id)> datas)
        {
            var environment = new Environment();

            var diffs = new List<LocationDiff>();
            foreach (var data in datas)
            {
                diffs.Add(new LocationDiff(CreateUser.IdOnly(data.userId), new HistoryComment(""),
                    CreateLocation.IdOnly(data.id),
                    CreateLocation.IdAndCommentOnly(data.id, data.comment)));
            }

            environment.useCase.UpdateLocationsWithHistory(diffs);

            var comparer = new Func<(long userId, string comment, long id), QstComment, bool>((data, parameter) =>
                data.userId == parameter.UserId.ToLong() &&
                data.comment == parameter.Comment.ToDefaultString() &&
                (long)NodeId.Location == parameter.NodeId &&
                data.id == parameter.NodeSeqid);

            CheckerFunctions.CollectionAssertAreEquivalent(datas, environment.mocks.qstCommentDataAccessMock.InsertQstCommentsParameter, comparer);
        }

        [TestCase(10)]
        [TestCase(20)]
        public void GetLocationCommentCallsDataAccess(long locId)
        {
            var environment = new Environment();

            environment.useCase.GetLocationComment(new LocationId(locId));

            Assert.AreEqual(NodeId.Location, environment.mocks.qstCommentDataAccessMock.GetQstCommentByNodeIdAndNodeSeqIdParameterNodeId);
            Assert.AreEqual(locId, environment.mocks.qstCommentDataAccessMock.GetQstCommentByNodeIdAndNodeSeqIdParameterNodeSeqId);
        }

        [TestCase("Kommentar A")]
        [TestCase("blub2020")]
        public void GetLocationCommentCommentReturnsCorrectValue(string comment)
        {
            var environment = new Environment();
            environment.mocks.qstCommentDataAccessMock.GetQstCommentByNodeIdAndNodeSeqIdReturnValue = new HistoryComment(comment);

            environment.useCase.GetLocationComment(new LocationId(1));

            Assert.AreEqual(comment, environment.mocks.qstCommentDataAccessMock.GetQstCommentByNodeIdAndNodeSeqIdReturnValue.ToDefaultString());
        }

        [Test]
        public void GetLocationCommentReturnsEmptyStringIfCommentIsNull()
        {
            var environment = new Environment();
            environment.mocks.qstCommentDataAccessMock.GetQstCommentByNodeIdAndNodeSeqIdReturnValue = null;

            Assert.AreEqual("", environment.useCase.GetLocationComment(new LocationId(1)));
        }

        [TestCase(1)]
        [TestCase(23)]
        public void GetReferencedLocPowIdsForLocationIdCallsDataAccess(long id)
        {
            var environment = new Environment();

            environment.useCase.GetReferencedLocPowIdsForLocationId(new LocationId(id));

            Assert.AreEqual(id, environment.mocks.locationDataAccess.GetReferencedLocPowIdsForLocationIdParameter.ToLong());
        }

        [Test]
        public void GetReferencedLocPowIdsForLocationIdReturnsCorrectValue()
        {
            var environment = new Environment();

            var longs = new List<long>();
            environment.mocks.locationDataAccess.GetReferencedLocPowIdsForLocationIdReturnValue = longs;

            Assert.AreSame(longs, environment.useCase.GetReferencedLocPowIdsForLocationId(new LocationId(1)));
        }

        [TestCase("234345")]
        [TestCase("abcdef")]
        public void IsUserIdUniqueCallsDataAccess(string userId)
        {
            var environment = new Environment();

            environment.useCase.IsUserIdUnique(userId);

            Assert.AreEqual(userId, environment.mocks.locationDataAccess.IsUserIdUniqueParameter);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void IsUserIdUniqueReturnsCorrectValue(bool isUnique)
        {
            var environment = new Environment();

            environment.mocks.locationDataAccess.IsUserIdUniqueReturnValue = isUnique;

            Assert.AreEqual(isUnique, environment.useCase.IsUserIdUnique(""));
        }

        [TestCase(1, 6)]
        [TestCase(14, 56)]
        public void LoadPictureForLocationCallsDataAccess(long locationId, int fileType)
        {
            var environment = new Environment();
            environment.useCase.LoadPictureForLocation(new LocationId(locationId), fileType);

            Assert.AreEqual(NodeId.Location, environment.mocks.pictureDataAccessMock.GetQstPictureParameterNodeId);
            Assert.AreEqual(locationId, environment.mocks.pictureDataAccessMock.GetQstPictureParameterFileTypeNodeSeqId);
            Assert.AreEqual(fileType, environment.mocks.pictureDataAccessMock.GetQstPictureParameterFileType);
        }

        [Test]
        public void LoadPictureForLocationReturnsCorrectValue()
        {
            var environment = new Environment();

            var picture = new Picture();
            environment.mocks.pictureDataAccessMock.GetQstPictureReturnValue = picture;

            Assert.AreSame(picture, environment.useCase.LoadPictureForLocation(new LocationId(1),1));
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    locationDataAccess = new LocationDataAccessMock();
                    locationDirectoryDataAccess = new LocationDirectoryDataAccessMock();
                    qstCommentDataAccessMock = new QstCommentDataAccessMock();
                    pictureDataAccessMock = new PictureDataAccessMock();
                }

                public readonly LocationDataAccessMock locationDataAccess;
                public readonly LocationDirectoryDataAccessMock locationDirectoryDataAccess;
                public readonly QstCommentDataAccessMock qstCommentDataAccessMock;
                public readonly PictureDataAccessMock pictureDataAccessMock;
            }

            public Environment()
            {
                mocks = new Mocks();
                useCase = new LocationUseCase(mocks.locationDataAccess, mocks.locationDirectoryDataAccess, mocks.qstCommentDataAccessMock, mocks.pictureDataAccessMock);
            }

            public readonly Mocks mocks;
            public readonly LocationUseCase useCase;
        }
    }
}
