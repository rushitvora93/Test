using System;
using System.Collections.Generic;
using Core.Diffs;
using Core.Entities;
using Core.UseCases;

namespace TestHelper.Mock
{
    public class LocationDataAccessMock : ILocationData
    {
        public bool LoadLocationWasCalled = false;
        public List<Location> LoadLocationsReturnValue;

        public bool ThrowLoadDirectoriesException = false;
        public bool LoadDirectoriesWasCalled = false;
        public List<LocationDirectory> LoadDirectoriesReturnValue;

        public Location RemovedLocation { get; set; }
        public bool RemoveLocationThrowsException = false;

        public bool ThrowAddLocationException;
        public Location AddLocationParameter;
        public Location AddLocationReturnValue;
        public LocationId LoadPictureForLocationParameter { get; set; }

        public bool IsNumberUniqueReturnValue { get; set; } = true;
        public bool WasIsNumberUniqueInvoked { get; private set; }
        public string IsNumberUniqueParameter { get; private set; }
        public LocationId LoadCommentForLocationParameter { get; set; }
        public string LoadCommentForLocationReturn = null;

        public Picture LoadPictureForLocationReturn = null;
        public bool IsDescriptionUniqueReturnValue { get; set; } = true;
        public bool WasIsDescriptionUniqueInvoked { get; private set; }
        public string IsDescriptionUniqueParameter { get; private set; }
        public int AddLocationFolderCallCount { get; set; }
        public LocationDirectoryId AddLocationFolderParameterId { get; set; }
        public string AddLocationFolderParameterName { get; set; }
        public User AddLocationDirectoyParameterUser { get; set; }
        public LocationDirectory AddLocationFolderReturn { get; set; }
        public bool AddLocationFolderThrowsException { get; set; }
        public LocationDirectory RemoveDirectoryParameter { get; set; }
        public int AddLocationCallCount { get; set; }
        public User AddLocationParameterUser { get; set; }
        public int LoadPictureForLocationCallCount { get; set; }
        public int LoadCommentForLocationCallCount { get; set; }
        public int UpdateLocationCallCount { get; set; }
        public int RemoveDirectoryCallCount { get; set; }
        public User RemoveDirectoryUserParameter { get; set; }
        public int RemoveLocationCallCount { get; set; }
        public User RemovedLocationParameterUser { get; set; }
        public bool RemoveDirectoryThrowsException { get; set; }
        public int ChangeLocationParentCallCount { get; set; }
        public Location ChangeLocationParentParameterLocation { get; set; }
        public long ChangeLocationParentParameterNewParentId { get; set; }
        public User ChangeLocationParentParameterUser { get; set; }
        public bool ChangeLocationParentThrowsException { get; set; }
        public LocationDirectory ChangeLocationDirectoryParentParameterLocationDirectory { get; set; }
        public long ChangeLocationDirectoryParentParameterNewParentId { get; set; }
        public User ChangeLocationDirectoryParentParameterUser { get; set; }
        public bool ChangeLocationDirectoryParentThrowsException { get; set; }
        public int ChangeLocationDirectoryParentCallCount { get; set; }
        public int GetLocationToolAssignmentIdsByLocationIdCallCounter { get; set; }
        public LocationId GetLocationToolAssignmentIdsByLocationIdParameter { get; set; }
        public List<long> GetLocationToolAssignmentIdsByLocationIdReturn { get; set; } = new List<long>();
        public List<LocationId> LastLoadLocationsByIdsParameterIds;
        public List<Location> NextLoadLocationsByIdsReturn;

        public Location UpdateLocationParameter;
        public Location UpdateLocationReturnValue;
        public bool ThrowsUpdateLocationError = false;


        public IEnumerable<Location> LoadLocations()
        {
            LoadLocationWasCalled = true;
            return LoadLocationsReturnValue;
        }

        public List<Location> LoadLocationsByIds(List<LocationId> ids)
        {
            LastLoadLocationsByIdsParameterIds = ids;
            return NextLoadLocationsByIdsReturn;
        }

        public List<LocationDirectory> LoadDirectories()
        {
            if (ThrowLoadDirectoriesException)
            {
                throw new Exception();
            }

            LoadDirectoriesWasCalled = true;
            return LoadDirectoriesReturnValue;
        }

        public Location AddLocation(Location location, User byUser)
        {
            if (ThrowAddLocationException)
            {
                throw new Exception();
            }

            AddLocationParameter = location;
            AddLocationCallCount++;
            AddLocationParameterUser = byUser;
            return AddLocationReturnValue;
        }

        public void RemoveLocation(Location location, User byUser)
        {
            if (RemoveLocationThrowsException)
            {
                throw new Exception();
            }
            RemovedLocation = location;
            RemoveLocationCallCount++;
            RemovedLocationParameterUser = byUser;
        }


        public Picture LoadPictureForLocation(LocationId locationId)
        {
            LoadPictureForLocationParameter = locationId;
            LoadPictureForLocationCallCount++;
            return LoadPictureForLocationReturn;
        }

        public string LoadCommentForLocation(LocationId locationId)
        {
            LoadCommentForLocationParameter = locationId;
            LoadCommentForLocationCallCount++;
            return LoadCommentForLocationReturn;
        }

        public Location UpdateLocation(LocationDiff diff)
        {
            if (ThrowsUpdateLocationError)
            {
                throw new Exception();
            }

            UpdateLocationParameter = diff.NewLocation;
            UpdateLocationCallCount++;
            return UpdateLocationReturnValue;
        }

        public bool IsNumberUnique(string number)
        {
            WasIsNumberUniqueInvoked = true;
            IsNumberUniqueParameter = number;
            return IsNumberUniqueReturnValue;
        }


        public bool IsDescriptionUnique(string description)
        {
            WasIsDescriptionUniqueInvoked = true;
            IsDescriptionUniqueParameter = description;
            return IsDescriptionUniqueReturnValue;
        }

        public LocationDirectory AddLocationDirectory(LocationDirectoryId parentId, string name, User byUser)
        {
            if (AddLocationFolderThrowsException)
            {
                throw new Exception();
            }
            AddLocationFolderParameterId = parentId;
            AddLocationFolderParameterName = name;
            AddLocationFolderCallCount++;
            AddLocationDirectoyParameterUser = byUser;
            return AddLocationFolderReturn;
        }

        public void RemoveDirectory(LocationDirectory selectedDirectory, User byUser)
        {
            if (RemoveDirectoryThrowsException)
            {
                throw new Exception();
            }
            RemoveDirectoryParameter = selectedDirectory;
            RemoveDirectoryCallCount++;
            RemoveDirectoryUserParameter = byUser;
        }

        public void ChangeLocationParent(Location location, LocationDirectoryId newParentId, User byUser)
        {
            if (ChangeLocationParentThrowsException)
            {
                throw new Exception();
            }
            ChangeLocationParentCallCount++;
            ChangeLocationParentParameterLocation = location;
            ChangeLocationParentParameterNewParentId = newParentId.ToLong();
            ChangeLocationParentParameterUser = byUser;
        }


        public void ChangeLocationDirectoryParent(LocationDirectory directory, LocationDirectoryId newParentId, User byUser)
        {
            if (ChangeLocationDirectoryParentThrowsException)
            {
                throw new Exception();
            }

            ChangeLocationDirectoryParentCallCount++;
            ChangeLocationDirectoryParentParameterLocationDirectory = directory;
            ChangeLocationDirectoryParentParameterNewParentId = newParentId.ToLong();
            ChangeLocationDirectoryParentParameterUser = byUser;
        }

        public List<long> GetLocationToolAssignmentIdsByLocationId(LocationId locationId)
        {
            GetLocationToolAssignmentIdsByLocationIdParameter = locationId;
            GetLocationToolAssignmentIdsByLocationIdCallCounter++;
            return GetLocationToolAssignmentIdsByLocationIdReturn;
        }

        public void RestoreLocation(Location location, User byUser)
        {
            throw new NotImplementedException();
        }

        public void RestoreDirectory(LocationDirectory locationDirectory, User byUser)
        {
            throw new NotImplementedException();
        }
    }

}
