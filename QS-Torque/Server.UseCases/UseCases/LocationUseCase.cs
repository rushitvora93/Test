using System.Collections.Generic;
using Server.Core.Diffs;
using Server.Core.Entities;
using State;

namespace Server.UseCases.UseCases
{
    public interface ILocationUseCase
    {
        List<LocationDirectory> LoadLocationDirectories();
        List<LocationDirectory> LoadAllLocationDirectories();
        List<LocationDirectory> InsertLocationDirectoriesWithHistory(List<LocationDirectoryDiff> locationDirectoryDiff, bool returnList);
        List<LocationDirectory> UpdateLocationDirectoriesWithHistory(List<LocationDirectoryDiff> locationDirectoryDiff);

        List<Location> LoadLocations(int index, int size);
        List<Location> LoadDeletedLocations(int index, int size);
        List<Location> LoadLocationsByIds(List<LocationId> ids);
        List<Location> InsertLocationsWithHistory(List<LocationDiff> locationDiff, bool returnList);
        List<Location> UpdateLocationsWithHistory(List<LocationDiff> locationDiff);
        bool IsUserIdUnique(string userId);
        List<long> GetReferencedLocPowIdsForLocationId(LocationId id);
        string GetLocationComment(LocationId locationId);
        Picture LoadPictureForLocation(LocationId locationId, int fileType);
    }

    public interface ILocationDataAccess
    {
        void Commit();
        List<Location> LoadLocations(int index, int size);
        List<Location> LoadLocationsByIds(List<LocationId> ids);
        List<Location> InsertLocationsWithHistory(List<LocationDiff> locationDiff, bool returnList);
        List<Location> UpdateLocationsWithHistory(List<LocationDiff> locationDiff);
        bool IsUserIdUnique(string userId);
        List<long> GetReferencedLocPowIdsForLocationId(LocationId id);
        List<Location> LoadDeletedLocations(int index, int size);
    }


    public interface ILocationDirectoryDataAccess
    {
        void Commit();
        List<LocationDirectory> LoadLocationDirectories();
        List<LocationDirectory> LoadAllLocationDirectories();
        List<LocationDirectory> InsertLocationDirectoriesWithHistory(List<LocationDirectoryDiff> locationDirectoryDiffs, bool returnList);
        List<LocationDirectory> UpdateLocationDirectoriesWithHistory(List<LocationDirectoryDiff> locationDirectoryDiffs);
    }

    public interface IPictureDataAccess
    {
        Picture GetQstPicture(int fileType, NodeId nodeId, long nodeseqid);
    }

    public class LocationUseCase : ILocationUseCase
    {
        private readonly ILocationDataAccess _locationDataAccess;
        private readonly ILocationDirectoryDataAccess _locationDirectoryDataAccess;
        private readonly IQstCommentDataAccess _qstCommentDataAccess;
        private readonly IPictureDataAccess _pictureDataAccess;

        public LocationUseCase(ILocationDataAccess locationDataAccess, ILocationDirectoryDataAccess locationDirectoryDataAccess,
            IQstCommentDataAccess qstCommentDataAccess, IPictureDataAccess pictureDataAccess)
        {
            _locationDataAccess = locationDataAccess;
            _locationDirectoryDataAccess = locationDirectoryDataAccess;
            _qstCommentDataAccess = qstCommentDataAccess;
            _pictureDataAccess = pictureDataAccess;
        }

        public List<LocationDirectory> LoadLocationDirectories()
        {
            return _locationDirectoryDataAccess.LoadLocationDirectories();
        }

        public List<LocationDirectory> LoadAllLocationDirectories()
        {
            return _locationDirectoryDataAccess.LoadAllLocationDirectories();
        }

        public List<LocationDirectory> InsertLocationDirectoriesWithHistory(List<LocationDirectoryDiff> locationDirectoryDiff, bool returnList)
        {
            var directories =
                _locationDirectoryDataAccess.InsertLocationDirectoriesWithHistory(locationDirectoryDiff, returnList);
            _locationDirectoryDataAccess.Commit();
            return directories;
        }

        public List<LocationDirectory> UpdateLocationDirectoriesWithHistory(List<LocationDirectoryDiff> locationDirectoryDiff)
        {
            var directories =
                _locationDirectoryDataAccess.UpdateLocationDirectoriesWithHistory(locationDirectoryDiff);
            _locationDirectoryDataAccess.Commit();
            return directories;
        }

        public List<Location> LoadLocations(int index, int size)
        {
            return _locationDataAccess.LoadLocations(index, size);
        }


        public List<Location> LoadDeletedLocations(int index, int size)
        {
            return _locationDataAccess.LoadDeletedLocations(index, size);
        }


        public List<Location> LoadLocationsByIds(List<LocationId> ids)
        {
            return _locationDataAccess.LoadLocationsByIds(ids);
        }


        public List<Location> InsertLocationsWithHistory(List<LocationDiff> locationDiff, bool returnList)
        {
            var locations = _locationDataAccess.InsertLocationsWithHistory(locationDiff, returnList);
            _locationDataAccess.Commit();
            return locations;
        }

        public List<Location> UpdateLocationsWithHistory(List<LocationDiff> locationDiff)
        {
            var locations = _locationDataAccess.UpdateLocationsWithHistory(locationDiff);
            _qstCommentDataAccess.InsertQstComments(GetQstComments(locationDiff));
            _locationDataAccess.Commit();
            return locations;
        }

        public bool IsUserIdUnique(string userId)
        {
            return _locationDataAccess.IsUserIdUnique(userId);
        }

        public List<long> GetReferencedLocPowIdsForLocationId(LocationId id)
        {
            return _locationDataAccess.GetReferencedLocPowIdsForLocationId(id);
        }

        public string GetLocationComment(LocationId locationId)
        {
            var comment = _qstCommentDataAccess.GetQstCommentByNodeIdAndNodeSeqId(NodeId.Location,
                locationId.ToLong());

            return comment == null ? "" : comment.ToDefaultString();
        }

        public Picture LoadPictureForLocation(LocationId locationId, int fileType)
        {
            return _pictureDataAccess.GetQstPicture(fileType, NodeId.Location, locationId.ToLong());
        }

        private List<QstComment> GetQstComments(List<LocationDiff> diffs)
        {
            var comments = new List<QstComment>();
            if (diffs == null)
            {
                return comments;
            }

            foreach (var diff in diffs)
            {
                if (diff.GetOldLocation().Comment == diff.GetNewLocation().Comment)
                {
                    continue;
                }

                var comment = new QstComment()
                {
                    Comment = new HistoryComment(diff.GetNewLocation().Comment),
                    UserId = diff.GetUser().UserId,
                    NodeId = (long)NodeId.Location,
                    NodeSeqid = diff.GetNewLocation().Id.ToLong()
                };

                comments.Add(comment);
            }

            return comments;
        }
    }
}
