using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Diffs;
using Core.Entities;
using Core.UseCases;

namespace TestHelper.Mock
{
    public class LocationUseCaseMock : LocationUseCase
    {
        public TaskCompletionSource<bool> LoadTreeCalled = new TaskCompletionSource<bool>();
        public bool WasLoadTreeCalled;
        public int LoadPictureForLocationCallCount = 0;
        public LocationId LoadPictureForLocationParameter = null;
        public int LoadCommentForLocationCallCount = 0;
        public LocationId LoadCommentForLocationParameter = null;
        public int RemoveLocationCallCount = 0;
        public int AddLocationFolderCallCount { get; set; }
        public LocationDirectoryId AddLocationFolderParameter { get; set; }
        public int RemoveDirectoryCallCount { get; set; }
        public Location RemovedLocation { get; set; }
        public int ChangeLocationParentCallCount { get; set; }
        public long ChangeLocationParentNewParentId { get; set; }
        public Location ChangeLocationParentLocationParameter { get; set; }
        public int ChangeLocationDirectoryParentCallCount { get; set; }
        public LocationDirectory ChangeLocationDirectoryParentLocationDirectoryParameter { get; set; }
        public List<Location> LoadTreePathForLocationsParameter { get; set; }

        public bool WasUpdateLocationCalled = false;
        public LocationDiff UpdateLocationParameter;

        public override void LoadTree(ILocationGui active)
        {
            WasLoadTreeCalled = true;
            LoadTreeCalled.SetResult(true);
        }

        public override void LoadPictureForLocation(LocationId locationId)
        {
            LoadPictureForLocationParameter = locationId;
            LoadPictureForLocationCallCount++;
        }

        public override void LoadCommentForLocation(LocationId locationId)
        {
            LoadCommentForLocationParameter = locationId;
            LoadCommentForLocationCallCount++;
        }

        public override void RemoveLocation(Location location, List<LocationToolAssignment> locationToolAssignments)
        {
            RemovedLocation = location;
            RemoveLocationCallCount++;
        }

        public override void RemoveLocation(Location location)
        {
            RemovedLocation = location;
            RemoveLocationCallCount++;
        }

        public override void UpdateLocation(LocationDiff locationDiff)
        {
            UpdateLocationParameter = locationDiff;
            WasUpdateLocationCalled = true;
        }

        public override void AddLocationDirectory(LocationDirectoryId parentLocationDirectoryId, string name)
        {
            AddLocationFolderParameter = parentLocationDirectoryId;
            AddLocationFolderCallCount++;
        }

        public override void RemoveDirectory(LocationDirectory directory)
        {
            RemoveDirectoryCallCount++;
        }

        public override void ChangeLocationParent(Location location, LocationDirectoryId newParentId)
        {
            ChangeLocationParentLocationParameter = location;
            ChangeLocationParentNewParentId = newParentId.ToLong();
            ChangeLocationParentCallCount++;
        }

        public override void ChangeLocationDirectoryParent(LocationDirectory directory, LocationDirectoryId parentId)
        {
            ChangeLocationDirectoryParentCallCount++;
            ChangeLocationDirectoryParentLocationDirectoryParameter = directory;
        }

        public override void LoadTreePathForLocations(List<Location> locations)
        {
            LoadTreePathForLocationsParameter = locations;
        }

        public LocationUseCaseMock(ToleranceClassUseCaseMock mock, LocationDataAccessMock dataAccessMock = null) : base(
            null, dataAccessMock, null, null, null, mock, null, null)
        {
        }
    }

    public class ToleranceClassUseCaseMock : ToleranceClassUseCase
    {
        public ToleranceClassUseCaseMock(IToleranceClassGui guiInterface, IToleranceClassData dataInterface,
            ISessionInformationUserGetter userGetter, INotificationManager notificationManager) : base(guiInterface, dataInterface, userGetter, null, notificationManager)
        {
        }

        public override void LoadToleranceClasses()
        {
        }
    }
}
