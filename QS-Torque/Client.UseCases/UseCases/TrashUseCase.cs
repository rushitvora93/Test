using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Client.UseCases.UseCases;
using Core.Diffs;
using Core.Entities;
using Core.Entities.ReferenceLink;
using log4net;

namespace Core.UseCases
{
    public interface ITrashGui
    {
        void ShowLocationTree(List<LocationDirectory> directorys);
        void ShowLocationTreeError();
        void ShowLoadingLocationTreeFinished();
        void RestoreLocation(Location location);
        void RestoreDirectory(LocationDirectoryId selectedDirectoryId);
        void ShowRestoreLocationError();
        void UpdateLocation(Location location);
        void ShowLocation(Location location);
        void ShowDeletedExtensions(List<Extension> extensions);
        void ShowDeletedToolModels(List<ToolModel> toolModels);
        void ShowDeletedModelsWithAtLeastOneTool(List<ToolModel> models);
        void RestoreTool(Tool tool);
        void RestoreExtension(Extension extension);
    }

    public interface ITrashData
    {
        IEnumerable<Location> LoadDeletedLocations();
        List<LocationDirectory> LoadAllDirectories();
        List<Extension> LoadDeletedExtensions();
        List<ToolModel> LoadDeletedModelsWithAtLeastOneTool();
        List<ToolModel> LoadDeletedToolModels();
        List<LocationToolAssignmentReferenceLink> LoadLocationToolAssignmentLinksForToolId(ToolId id);
        void RestoreTool(Tool tool, User user);
        List<LocationReferenceLink> LoadReferencedLocations(ExtensionId id);
        void RestoreExtension(Extension extension, User user);
    }

    public interface ITrashUseCase
    {
        void LoadTree(ITrashGui trash);
        void RestoreLocation(Location location);
        void RestoreDirectory(LocationDirectory locationDirectory);
        bool IsLocationNumberUnique(string number);
        void UpdateLocation(LocationDiff locationDiff);
        void LoadTreePathForLocations(List<Location> locations);
        void RestoreExtension(Extension extension);
        void RestoreToolModels(ToolModel toolModel);
    }

    public class TrashUseCase : ITrashUseCase
    {
        private ITrashGui _gui;
        private ITrashData _dataAccess;
        private ILocationData _locationDataAccess;
        private ILocationToolAssignmentData _locationToolAssignmentDataAccess;
        private ISessionInformationUserGetter _userGetter;
        private readonly INotificationManager _notificationManager;
        private readonly IProcessControlData _processControlData;
        private List<Location> _locations;
        private List<LocationDirectory> _directories;
        private object _locationsLock = new object();
        private object _directorysLock = new object();

        private static readonly ILog Log = LogManager.GetLogger(typeof(TrashUseCase));

        public TrashUseCase(ITrashGui gui, ITrashData dataAccess, ILocationData locationDataAccess,
            ISessionInformationUserGetter userGetter, INotificationManager notificationManager,
            IProcessControlData processControlData, ILocationToolAssignmentData locationToolAssignmentDataAccess
            )
        {
            _gui = gui;
            _dataAccess = dataAccess;
            _locationDataAccess = locationDataAccess;
            _locationToolAssignmentDataAccess = locationToolAssignmentDataAccess;
            _userGetter = userGetter;
            _notificationManager = notificationManager;
            _processControlData = processControlData;
            _locations = new List<Location>();
            _directories = new List<LocationDirectory>();
        }

        public virtual void LoadTree(ITrashGui trash)
        {
            #region Location bind
            var locations = new ObservableCollection<Location>();
            try
            {
                List<LocationDirectory> directories = _dataAccess.LoadAllDirectories();
                List<LocationDirectory> activeDirectories = new List<LocationDirectory>();
                var deletedLocations = _dataAccess.LoadDeletedLocations();
                foreach (var directory in directories)
                {
                    if (deletedLocations.Any(x => x.ParentDirectoryId.Equals(directory.Id)))
                    {
                        activeDirectories.Add(directory);
                    }
                }

                lock (_directorysLock)
                {
                    _directories = new List<LocationDirectory>(activeDirectories);
                }

                lock (_locationsLock)
                {
                    _locations = new List<Location>();
                }
                trash.ShowLocationTree(activeDirectories);

                foreach (var x in _dataAccess.LoadDeletedLocations())
                {
                    trash.ShowLocation(x);
                    lock (_locationsLock)
                    {
                        _locations.Add(x);
                    }
                }

                trash.ShowLoadingLocationTreeFinished();
            }
            catch (Exception e)
            {
                Log.Error($"Error while loading the tree after {locations.Count} locations", e);
                trash.ShowLocationTreeError();
            }
            #endregion

            #region Extension Bind
            var extensions = _dataAccess.LoadDeletedExtensions();
            Log.Debug($"LoadExtensions call with List of Extensions with size of {extensions?.Count}");
            _gui.ShowDeletedExtensions(extensions);
            #endregion

            #region Tool Model Bind
            var toolModels = _dataAccess.LoadDeletedToolModels();
            Log.Debug($"LoadExtensions call with List of Extensions with size of {toolModels?.Count}");
            _gui.ShowDeletedExtensions(extensions);
            #endregion

            #region Tool Bind
            Log.Info("LoadToolModels started");
            var models = _dataAccess.LoadDeletedModelsWithAtLeastOneTool();
            Log.Debug($"ShowDeletedModelsWithAtLeastOneTool call with List of ToolModels Size of {models?.Count}");
            _gui.ShowDeletedModelsWithAtLeastOneTool(models);

            //Log.Info("LoadToolModels started");
            //var toolModels = _dataAccess.LoadDeletedToolModels();
            //FillToolModelDescriptionCache(toolModels);

            Log.Debug($"ShowToolModels call with List of ToolModels Size of {toolModels?.Count}");
            //_gui.ShowToolModels(toolModels);

            #endregion
        }

        public void UpdateLocation(LocationDiff locationDiff)
        {
            throw new NotImplementedException();
        }

        public virtual void LoadTreePathForLocations(List<Location> locations)
        {
            if (locations == null)
            {
                return;
            }
            var directories = _dataAccess.LoadAllDirectories();
            foreach (var location in locations)
            {
                location.LocationDirectoryPath = GetLocationDirectoryPath(directories, location.ParentDirectoryId);
            }
        }

        public virtual void RestoreLocation(Location location)
        {
            if (location is null)
            {
                throw new ArgumentNullException("location is null", nameof(location));
            }
            try
            {
                Log.Info("RestoreLocation started");
                var processControl = _processControlData.LoadProcessControlConditionForLocation(location);
                var locationToolAssignments =
                    _locationToolAssignmentDataAccess.LoadAssignedToolsForLocation(location.Id);
                Log.Debug($"LocationToolAssignments Loaded with count {locationToolAssignments?.Count ?? 0}");
                if (locationToolAssignments is null || locationToolAssignments.Count == 0)
                {
                    Log.Debug($"Gui.RestoreLocation called with Location id:{location?.Id?.ToLong()} and decription: {location?.Description?.ToDefaultString()}");
                    if (processControl != null)
                    {
                        _processControlData.RestoreProcessControlCondition(processControl, _userGetter.GetCurrentUser());
                    }

                    _locationDataAccess.RestoreLocation(location, _userGetter.GetCurrentUser());
                    lock (_locationsLock)
                    {
                        if (_locations.FirstOrDefault(x => x.EqualsById(location)) is Location loc)
                        {
                            _locations.Remove(loc);
                        }
                    }

                    _gui.RestoreLocation(location);
                    _notificationManager.SendSuccessNotification();
                    return;
                }

                Action onSuccess = () =>
                {
                    if (processControl != null)
                    {
                        _processControlData.RestoreProcessControlCondition(processControl, _userGetter.GetCurrentUser());
                    }
                    locationToolAssignments.ForEach(locationToolAssignment => _locationToolAssignmentDataAccess.RestoreLocationToolAssignment(locationToolAssignment, _userGetter.GetCurrentUser()));
                    _locationDataAccess.RestoreLocation(location, _userGetter.GetCurrentUser());
                    _gui.RestoreLocation(location);
                    _notificationManager.SendSuccessNotification();
                };
            }
            catch (Exception exception)
            {
                Log.Error("Exception in RestoreLocation", exception);
                _gui.ShowRestoreLocationError();
            }
        }

        public void RestoreExtension(Extension extension, IExtensionErrorGui errorHandler, IExtensionDependencyGui dependencyGui)
        {
            try
            {
                Log.Info("RestoreExtension started");

                var references = _dataAccess.LoadReferencedLocations(extension.Id);
                if (references != null && references.Count > 0)
                {
                    dependencyGui.ShowRemoveExtensionPreventingReferences(references);
                    return;
        }

                _dataAccess.RestoreExtension(extension, _userGetter?.GetCurrentUser());
                _gui.RestoreExtension(extension);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception e)
            {
                errorHandler.ShowProblemRemoveExtension();
                Log.Error("Error in RemoveExtension", e);
            }
        }

        public virtual void RestoreTool(Tool tool, IToolGui active)
        {
            try
            {
                Log.Info($"Remove Tool started");
                var references = _dataAccess.LoadLocationToolAssignmentLinksForToolId(tool.Id);
                if (references != null && references.Count > 0)
                {
                    active.ShowRemoveToolPreventingReferences(references);
                    return;
                }
                Log.Debug($"RemoveTool called with tool id{tool?.Id?.ToLong()}");
                _dataAccess.RestoreTool(tool, _userGetter.GetCurrentUser());
                _gui.RestoreTool(tool);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception exception)
            {
                Log.Error("RemoveTool error", exception);
            }
        }

        public virtual void RestoreDirectory(LocationDirectory directory)
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
                        _locationToolAssignmentDataAccess.RestoreLocationToolAssignment(locationToolAssignment, _userGetter.GetCurrentUser()));
                    locations.ForEach(location =>
                    {
                        _locationDataAccess.RestoreLocation(location, _userGetter.GetCurrentUser());
                        _gui.RestoreLocation(location);
                    });

                    List<LocationDirectory> directorys = new List<LocationDirectory>();
                    GetNestedDirecotries(directorys, directory);
                    foreach (var locationDirectory in directorys)
                    {
                        _locationDataAccess.RestoreDirectory(locationDirectory, _userGetter.GetCurrentUser());
                        _gui.RestoreDirectory(locationDirectory.Id);
                    }
                    _locationDataAccess.RestoreDirectory(directory, _userGetter.GetCurrentUser());
                    _gui.RestoreDirectory(directory.Id);
                    _notificationManager.SendSuccessNotification();
                };
                if (locationToolAssignments is null || locationToolAssignments.Count <= 0)
                {
                    onSuccess.Invoke();
                    return;
                }
            }
            catch (Exception exception)
            {
                Log.Error($"Error while removing directory with id: {directory?.Id?.ToLong()} and Name:{directory.Name?.ToDefaultString()}", exception);
            }
        }

        public bool IsLocationNumberUnique(string number)
        {
            return _locationDataAccess.IsNumberUnique(number);
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
                if (directory != null)
                {
                    locationDirectoryPath.Add(directory);
                }
            }

            locationDirectoryPath.Reverse();
            return locationDirectoryPath;
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

        private void GetNestedDirecotries(List<LocationDirectory> foundDirectories, LocationDirectory directory)
        {
            List<LocationDirectory> currentLevelDirectories = new List<LocationDirectory>();
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
        public void RestoreExtension(Extension extension)
        {
            if (extension is null)
            {
                throw new ArgumentNullException("extension is null", nameof(extension));
            }
            try
            {
                Log.Info("Restore extension started");
            }
            catch (Exception exception)
            {
                Log.Error("Exception in RestoreLocation", exception);
                _gui.ShowRestoreLocationError();
            }
        }
        public void RestoreToolModels(ToolModel toolModel)
        {
            if (toolModel is null)
            {
                throw new ArgumentNullException("extension is null", nameof(toolModel));
            }
            try
            {
                Log.Info("Restore extension started");
            }
            catch (Exception exception)
            {
                Log.Error("Exception in RestoreLocation", exception);
                _gui.ShowRestoreLocationError();
            }
        }
    }

    public class TrashUseCaseHumbleAsyncRunner : ITrashUseCase
    {
        private readonly ITrashUseCase _real;

        public TrashUseCaseHumbleAsyncRunner(ITrashUseCase real)
        {
            _real = real;
        }

        public bool IsLocationNumberUnique(string number)
        {
            throw new NotImplementedException();
        }

        public void LoadTree(ITrashGui trash)
        {
            Task.Run(() => _real.LoadTree(trash));
        }

        public void LoadTreePathForLocations(List<Location> locations)
        {
            Task.Run(() => _real.LoadTreePathForLocations(locations));
        }

        public void RestoreDirectory(LocationDirectory locationDirectory)
        {
            Task.Run(() => _real.RestoreDirectory(locationDirectory));
        }

        public void RestoreLocation(Location location)
        {
            Task.Run(() => _real.RestoreLocation(location));
        }

        public void RestoreExtension(Extension extension)
        {
            Task.Run(() => _real.RestoreExtension(extension));
        }
        public void RestoreToolModels(ToolModel toolModel)
        {
            Task.Run(() => _real.RestoreToolModels(toolModel));
        }
        public void UpdateLocation(LocationDiff locationDiff)
        {
            Task.Run(() => _real.UpdateLocation(locationDiff));
        }
    }
}
