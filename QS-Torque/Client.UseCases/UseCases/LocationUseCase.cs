using Core.Diffs;
using Core.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Client.UseCases.UseCases;

namespace Core.UseCases
{
    public interface ILocationGui
    {
        void ShowLocationTree(List<LocationDirectory> directorys);
        void ShowLocationTreeError();
        void ShowLoadingLocationTreeFinished();
        void AddLocation(Location location);
        void AddLocationError();
        void RemoveLocation(Location location);
        void ShowRemoveLocationError();
        void ShowPictureForLocation(Picture picture, LocationId locationId);
        void ShowCommentForLocation(string comment, LocationId locationId);
        void UpdateLocation(Location location);
        void UpdateLocationError();
        void LocationAlreadyExists();
        void AddLocationDirectory(LocationDirectory locationDirectory);
        void ShowAddLocationDirectoryError(string name);
        void RemoveDirectory(LocationDirectoryId selectedDirectoryId);
        void ShowRemoveDirectoryError();
        void ChangeLocationParent(Location location, LocationDirectoryId newParentId);
        void ChangeLocationParentError();
        void ShowLocation(Location location);
        void ChangeLocationDirectoryParent(LocationDirectory directory, LocationDirectoryId newParentId);
        void ChangeLocationDirectoryParentError();
        void ShowChangeLocationToolAssignmentNotice();
        void ShowChangeToolStatusDialog(Action onSuccess, List<LocationToolAssignment> locationToolAssignments);
    }

    public interface ILocationData
    {
        IEnumerable<Location> LoadLocations();
        List<Location> LoadLocationsByIds(List<LocationId> ids);
        List<LocationDirectory> LoadDirectories();
        Location AddLocation(Location location, User byUser);
        void RemoveLocation(Location location, User byUser);
        void RestoreLocation(Location location, User byUser);
        Picture LoadPictureForLocation(LocationId locationId);
        string LoadCommentForLocation(LocationId locationId);
        Location UpdateLocation(LocationDiff diff);
        bool IsNumberUnique(string number);
        LocationDirectory AddLocationDirectory(LocationDirectoryId parentId, string name, User byUser);  
        void RemoveDirectory(LocationDirectory selectedDirectory, User byUser);
        void RestoreDirectory(LocationDirectory locationDirectory, User byUser);
        void ChangeLocationParent(Location location, LocationDirectoryId newParentId, User byUser);
        void ChangeLocationDirectoryParent(LocationDirectory directory, LocationDirectoryId newParentId, User byUser);
        List<long> GetLocationToolAssignmentIdsByLocationId(LocationId locationId);
    }


    public interface ILocationUseCase
    {
        IToleranceClassUseCase ToleranceClassUseCase { get; }
        void LoadTree(ILocationGui active);
        void AddLocation(Location location);
        void RemoveLocation(Location location);
        bool IsNumberUnique(string number);
        void LoadPictureForLocation(LocationId locationId);
        void LoadCommentForLocation(LocationId locationId);
        void UpdateLocation(LocationDiff locationDiff);
        void AddLocationDirectory(LocationDirectoryId parentLocationDirectoryId, string name);
        void RemoveDirectory(LocationDirectory directory);
        void ChangeLocationParent(Location location, LocationDirectoryId newParentId);
        void ChangeLocationDirectoryParent(LocationDirectory directory, LocationDirectoryId parentId);
        void LoadTreePathForLocations(List<Location> locations);
    }

    public class LocationUseCase : ILocationUseCase
    {
        private ILocationGui _gui;
        private ILocationData _dataAccess;
        private ILocationToolAssignmentData _locationToolAssignmentDataAccess;
        private ISessionInformationUserGetter _userGetter;
        private readonly INotificationManager _notificationManager;
        private readonly IProcessControlData _processControlData;
        private List<Location> _locations;
        private List<LocationDirectory> _directories;
        private object _locationsLock = new object();
        private object _directorysLock = new object();

        private static readonly ILog Log = LogManager.GetLogger(typeof(LocationUseCase));
        private IToolData _toolDataAccess;

        public IToleranceClassUseCase ToleranceClassUseCase { get; private set; }

        public LocationUseCase(ILocationGui gui, ILocationData dataAccess, 
            ILocationToolAssignmentData locationToolAssignmentDataAccess,
            IToolData toolDataAccess, ISessionInformationUserGetter userGetter,
            IToleranceClassUseCase toleranceClassUseCase, INotificationManager notificationManager,
            IProcessControlData processControlData)
        {
            _gui = gui;
            _dataAccess = dataAccess;
            _locationToolAssignmentDataAccess = locationToolAssignmentDataAccess;
            _toolDataAccess = toolDataAccess;
            _userGetter = userGetter;
            _notificationManager = notificationManager;
            _processControlData = processControlData;
            ToleranceClassUseCase = toleranceClassUseCase;
            _locations = new List<Location>();
            _directories = new List<LocationDirectory>();
        }


        public virtual void LoadTreePathForLocations(List<Location> locations)
        {
            if (locations == null)
            {
                return;
            }
            var directories = _dataAccess.LoadDirectories();
            foreach (var location in locations)
            {
                location.LocationDirectoryPath = GetLocationDirectoryPath(directories, location.ParentDirectoryId);
            }
        }

        private List<LocationDirectory> GetLocationDirectoryPath(List<LocationDirectory> directories,
            LocationDirectoryId directoryId)
        {
            var locationDirectoryPath = new List<LocationDirectory>();
            var directory = directories.SingleOrDefault(x => x.Id.ToLong() == directoryId.ToLong());
            if (directory == null)
            {
                return locationDirectoryPath;
            }

            locationDirectoryPath.Add(directory);

            while (directory != null)
            {
                directory = directories.SingleOrDefault(x => x.Id.ToLong() == directory.ParentId.ToLong());
                if (directory!= null)
                {
                    locationDirectoryPath.Add(directory);
                }
            }

            locationDirectoryPath.Reverse();
            return locationDirectoryPath;
        }

        public virtual void LoadTree(ILocationGui active)
        {
            
            var locations = new ObservableCollection<Location>();
            try
            {
                List<LocationDirectory> directories = _dataAccess.LoadDirectories();
                lock (_directorysLock)
                {
                    _directories = new List<LocationDirectory>(directories);
                }

                lock (_locationsLock)
                {
                    _locations = new List<Location>();
                }
                active.ShowLocationTree(directories);

                foreach(var x in _dataAccess.LoadLocations())
                {
                    active.ShowLocation(x);
                    lock (_locationsLock)
                    {
                        _locations.Add(x);
                    }
                }

                active.ShowLoadingLocationTreeFinished();
            }
            catch (Exception e)
            {
                Log.Error($"Error while loading the tree after {locations.Count} locations", e);
                active.ShowLocationTreeError();
            }
        }

        public void AddLocation(Location location)
        {
            try
            {
                location.UpdateToleranceLimits();
                var addedLocation = _dataAccess.AddLocation(location, _userGetter.GetCurrentUser());
                lock (_locationsLock)
                {
                    _locations.Add(addedLocation);
                }
                _gui.AddLocation(addedLocation);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception e)
            {
                Log.Error($"Error while Adding Location with Id: {location?.Id?.ToLong()} and Description:{location?.Description?.ToDefaultString()}", e);
                _gui.AddLocationError();
            }
        }

        public virtual void RemoveLocation(Location location)
        {
            if (location is null)
            {
                throw new ArgumentNullException("location is null", nameof(location));
            }
            try
            {
                Log.Info("RemoveLocation started");
                var processControl = _processControlData.LoadProcessControlConditionForLocation(location);
                var locationToolAssignments =
                    _locationToolAssignmentDataAccess.LoadAssignedToolsForLocation(location.Id);
                Log.Debug($"LocationToolAssignments Loaded with count {locationToolAssignments?.Count ?? 0}");
                if (locationToolAssignments is null || locationToolAssignments.Count == 0)
                {
                    Log.Debug($"Gui.RemoveLocation called with Location id:{location?.Id?.ToLong()} and decription: {location?.Description?.ToDefaultString()}");
                    if (processControl != null)
                    {
                        _processControlData.RemoveProcessControlCondition(processControl, _userGetter.GetCurrentUser());
                    }
                    _dataAccess.RemoveLocation(location, _userGetter.GetCurrentUser());
                    _gui.RemoveLocation(location);
                    _notificationManager.SendSuccessNotification();
                    return;
                }

                Action onSuccess = () =>
                {
                    if (processControl != null)
                    {
                        _processControlData.RemoveProcessControlCondition(processControl, _userGetter.GetCurrentUser());
                    }
                    locationToolAssignments.ForEach(locationToolAssignment => _locationToolAssignmentDataAccess.RemoveLocationToolAssignment(locationToolAssignment, _userGetter.GetCurrentUser()));
                    _dataAccess.RemoveLocation(location, _userGetter.GetCurrentUser());
                    _gui.RemoveLocation(location);
                    _notificationManager.SendSuccessNotification();
                };
                _gui.ShowChangeToolStatusDialog(onSuccess, locationToolAssignments);
            }
            catch (Exception exception)
            {
                Log.Error("Exception in RemoveLocation", exception);
                _gui.ShowRemoveLocationError();
            }
        }

        public virtual void RemoveDirectory(LocationDirectory directory)
        {
            if (directory is null)
            {
                throw new ArgumentNullException("directory is null", nameof(directory));
            }

            try
            {
                List<Location> locations = new List<Location>();
                GetLocationsForLocationDirectory(directory, locations);
                List<LocationToolAssignment> locationToolAssignments = new List<LocationToolAssignment>();
                locations.ForEach(location =>
                {
                    var loadedAssignedToolsForLocation =
                        _locationToolAssignmentDataAccess.LoadAssignedToolsForLocation(location.Id);
                    if (loadedAssignedToolsForLocation != null)
                    {
                        locationToolAssignments.AddRange(loadedAssignedToolsForLocation);
                    }
                });
                Action onSuccess = () =>
                {
                    locationToolAssignments.ForEach(locationToolAssignment =>
                        _locationToolAssignmentDataAccess.RemoveLocationToolAssignment(locationToolAssignment, _userGetter.GetCurrentUser()));
                    locations.ForEach(location =>
                    {
                        _dataAccess.RemoveLocation(location, _userGetter.GetCurrentUser());
                        _gui.RemoveLocation(location);
                    });

                    List<LocationDirectory> directorys = new List<LocationDirectory>();
                    GetNestedDirecotries(directorys, directory);
                    foreach (var locationDirectory in directorys)
                    {
                        _dataAccess.RemoveDirectory(locationDirectory, _userGetter.GetCurrentUser());
                        _gui.RemoveDirectory(locationDirectory.Id);
                    }
                    _dataAccess.RemoveDirectory(directory, _userGetter.GetCurrentUser());
                    _gui.RemoveDirectory(directory.Id);
                    _notificationManager.SendSuccessNotification();
                };
                if (locationToolAssignments is null || locationToolAssignments.Count <= 0)
                {
                    onSuccess.Invoke();
                    return;
                }
                _gui.ShowChangeToolStatusDialog(onSuccess, locationToolAssignments);
            }
            catch (Exception exception)
            {
                Log.Error($"Error while removing directory with id: {directory?.Id?.ToLong()} and Name:{directory.Name?.ToDefaultString()}", exception);
                _gui.ShowRemoveDirectoryError();
            }
            
        }

        private void GetNestedDirecotries(List<LocationDirectory> foundDirectories, LocationDirectory directory)
        {
            List <LocationDirectory> currentLevelDirectories = new List<LocationDirectory>();
            lock (_directorysLock)
            {
                currentLevelDirectories = _directories.Where(x => x?.ParentId?.ToLong() == directory?.Id?.ToLong())
                    .ToList();
                foundDirectories.AddRange(currentLevelDirectories);
            }

            foreach (var currentLevelDirectory in currentLevelDirectories)
            {
                GetNestedDirecotries(foundDirectories, currentLevelDirectory);
            }
        }

        public virtual void RemoveLocation(Location location, List<LocationToolAssignment> locationToolAssignment)
        {
            RemoveLocation(location, locationToolAssignment, false);
        }

        private void RemoveLocation(Location location, List<LocationToolAssignment> locationToolAssignment, bool throwException = false)
        {
            Log.Info("RemoveLocation started");
            try
            {
                if (locationToolAssignment != null)
                {
                    foreach (var toolAssignment in locationToolAssignment)
                    {
                        _locationToolAssignmentDataAccess.RemoveLocationToolAssignment(toolAssignment, _userGetter.GetCurrentUser());
                    }
                }
                
                _dataAccess.RemoveLocation(location, _userGetter.GetCurrentUser());
                lock (_locationsLock)
                {
                    if (_locations.FirstOrDefault(x => x.EqualsById(location)) is Location loc)
                    {
                        _locations.Remove(loc);
                    }
                }
                Log.Debug($"Gui.RemoveLocation called with Location id:{location?.Id?.ToLong()} and decription: {location?.Description?.ToDefaultString()}");
                _gui.RemoveLocation(location);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception e)
            {
                Log.Error($"Error while Removing Location with Id: {location?.Id?.ToLong()} and Description:{location?.Description?.ToDefaultString()}", e);
                if (throwException)
                {
                    throw;
                }
                _gui.ShowRemoveLocationError();
            }
            Log.Info("RemoveLocation ended");
        }

        private void GetLocationsForLocationDirectory(LocationDirectory directory, List<Location> locations)
        {
            IEnumerable<LocationDirectory> directorys = new List<LocationDirectory>();
            lock (_directorysLock)
            {
                directorys = _directories.Where(dir => dir.ParentId?.Equals(directory.Id) ?? false);
            }
            foreach (var locationDirectory in directorys)
            {
                GetLocationsForLocationDirectory(locationDirectory, locations);
            }
            locations.AddRange(_locations.Where(location => location.ParentDirectoryId?.Equals(directory?.Id) ?? false));
        }

        public bool IsNumberUnique(string number)
        {
            return _dataAccess.IsNumberUnique(number);
        }

        public virtual void LoadPictureForLocation(LocationId locationId)
        {
            try
            {
                var picture = _dataAccess.LoadPictureForLocation(locationId);
                _gui.ShowPictureForLocation(picture, locationId);
            }
            catch (Exception e)
            {
                Log.Error($"Error while Loading Picture for Location with id: {locationId?.ToLong()}", e);
            }
        }

        public virtual void LoadCommentForLocation(LocationId locationId)
        {
            try
            {
                var comment = _dataAccess.LoadCommentForLocation(locationId);
                _gui.ShowCommentForLocation(comment, locationId);
            }
            catch (Exception e)
            {
                Log.Error($"Error while loading Comment for Location with Id: {locationId?.ToLong()}", e);
            }
        }

        public virtual void UpdateLocation(LocationDiff locationDiff)
        {
            try
            {
                if ((!IsNumberUnique(locationDiff.NewLocation.Number.ToDefaultString()) && locationDiff.OldLocation.Number.ToDefaultString() != locationDiff.NewLocation.Number.ToDefaultString()))
                {
                    _gui.LocationAlreadyExists();
                    return;
                }

                if (_dataAccess.GetLocationToolAssignmentIdsByLocationId(locationDiff.OldLocation.Id).Count > 0)
                {
                    _gui.ShowChangeLocationToolAssignmentNotice();
                }
                locationDiff.NewLocation.UpdateToleranceLimits();
                locationDiff.User = _userGetter.GetCurrentUser();

                var updatedLocation = _dataAccess.UpdateLocation(locationDiff);
                lock (_locationsLock)
                {
                    if (_locations.FirstOrDefault(x => x.EqualsById(updatedLocation)) is Location loc)
                    {
                        _locations.Remove(loc);
                        _locations.Add(updatedLocation);
                    }
                }
                _gui.UpdateLocation(updatedLocation);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception e)
            {
                Log.Error($"Error while updating location width id {locationDiff.NewLocation?.Id?.ToLong()}", e);
                _gui.UpdateLocationError();
            }
        }

        public virtual void AddLocationDirectory(LocationDirectoryId parentLocationDirectoryId, string name)
        {
            try
            {
                var locationDirectory = _dataAccess.AddLocationDirectory(parentLocationDirectoryId, name, _userGetter.GetCurrentUser());
                lock (_directorysLock)
                {
                    _directories.Add(locationDirectory);
                }
                _gui.AddLocationDirectory(locationDirectory);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception e)
            {
                Log.Error($"Error while Adding LocationFolder with ParentId: {parentLocationDirectoryId?.ToLong()} and Name: {name}", e);
                _gui.ShowAddLocationDirectoryError(name);
            }
        }

        public virtual void ChangeLocationParent(Location location, LocationDirectoryId newParentId)
        {
            try
            {
                _dataAccess.ChangeLocationParent(location, newParentId, _userGetter.GetCurrentUser());
                location.ParentDirectoryId = newParentId;
                lock (_locationsLock)
                {
                    if (_locations?.FirstOrDefault(x => x.EqualsById(location)) is Location loc)
                    {
                        loc.ParentDirectoryId = newParentId;
                    } 
                }
                _gui.ChangeLocationParent(location, newParentId);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception exception)
            {
                Log.Error($"Error in ChangeLocationParent for Location : {location?.Id.ToLong()} and newParentId {newParentId}", exception);
                _gui.ChangeLocationParentError();
            }
        }

        public virtual void ChangeLocationDirectoryParent(LocationDirectory directory, LocationDirectoryId newParentId)
        {
            try
            {
                _dataAccess.ChangeLocationDirectoryParent(directory, newParentId, _userGetter.GetCurrentUser());
                lock (_directorysLock)
                {
                    if (_directories?.FirstOrDefault(x => x.EqualsById(directory)) is LocationDirectory dir)
                    {
                    }
                }
                _gui.ChangeLocationDirectoryParent(directory, newParentId);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception exception)
            {
                Log.Error($"Error in ChangeLocationParent for Location : {directory?.Id.ToLong()} and newParentId {newParentId}", exception);
                _gui.ChangeLocationDirectoryParentError();
            }
        }
    }

    public class LocationUseCaseHumbleAsyncRunner : ILocationUseCase
    {
        private readonly ILocationUseCase _real;

        public LocationUseCaseHumbleAsyncRunner(ILocationUseCase real)
        {
            _real = real;
        }

        public IToleranceClassUseCase ToleranceClassUseCase { get => _real.ToleranceClassUseCase; }

        public void LoadTree(ILocationGui active)
        {
            Task.Run(() => _real.LoadTree(active));
        }

        public void AddLocation(Location location)
        {
            Task.Run(() => _real.AddLocation(location));
        }

        public void RemoveLocation(Location location)
        {
            Task.Run(() => _real.RemoveLocation(location));
        }

        public bool IsNumberUnique(string number)
        {
            return _real.IsNumberUnique(number);
        }

        public void LoadPictureForLocation(LocationId locationId)
        {
            Task.Run(() => _real.LoadPictureForLocation(locationId));
        }

        public void LoadCommentForLocation(LocationId locationId)
        {
            Task.Run(() => _real.LoadCommentForLocation(locationId));
        }

        public void UpdateLocation(LocationDiff locationDiff)
        {
            Task.Run(() => _real.UpdateLocation(locationDiff));
        }

        public void AddLocationDirectory(LocationDirectoryId parentLocationDirectoryId, string name)
        {
            Task.Run(() => _real.AddLocationDirectory(parentLocationDirectoryId, name));
        }

        public void RemoveDirectory(LocationDirectory directory)
        {
            Task.Run(() => _real.RemoveDirectory(directory));
        }

        public void ChangeLocationParent(Location location, LocationDirectoryId newParentId)
        {
            Task.Run(() => _real.ChangeLocationParent(location, newParentId));
        }

        public void ChangeLocationDirectoryParent(LocationDirectory directory, LocationDirectoryId parentId)
        {
            Task.Run(() => _real.ChangeLocationDirectoryParent(directory, parentId));
        }

        public void LoadTreePathForLocations(List<Location> locations)
        {
            Task.Run(() => _real.LoadTreePathForLocations(locations));
        }
    }
}
