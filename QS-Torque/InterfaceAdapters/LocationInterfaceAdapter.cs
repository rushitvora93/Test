using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using Core.Entities;
using Core.UseCases;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;

namespace InterfaceAdapters
{
    public interface ILocationInterface : INotifyPropertyChanged
    {
        LocationTreeModel LocationTree { get; }
        void SetGuiDispatcher(Dispatcher guiDispatcher);
    }


    public class LocationInterfaceAdapter : InterfaceAdapter<ILocationGui>, ILocationGui, ILocationInterface
    {
        private ILocalizationWrapper _localization;
        private Dispatcher _guiDispatcher;

        private LocationTreeModel _locationTree = new LocationTreeModel();
        public LocationTreeModel LocationTree
        {
            get { return _locationTree; }
            set
            {
                _locationTree = value;
                RaisePropertyChanged();
            }
        }

        public LocationInterfaceAdapter(ILocalizationWrapper localization)
        {
            _localization = localization;
        }

        public void SetGuiDispatcher(Dispatcher guiDispatcher)
        {
            _guiDispatcher = guiDispatcher;
        }

        public void ShowLocationTree(List<LocationDirectory> directories)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowLocationTree(directories));
            LocationTree = new LocationTreeModel();
            directories.ForEach(x => LocationTree.LocationDirectoryModels.Add(LocationDirectoryHumbleModel.GetModelFor(x, null)));
        }

        public void ShowLocationTreeError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowLocationTreeError());
        }

        public void ShowLoadingLocationTreeFinished()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowLoadingLocationTreeFinished());
        }

        public void AddLocation(Location location)
        {
            InvokeActionOnGuiInterfaces(gui => gui.AddLocation(location));
            _guiDispatcher?.Invoke(() => LocationTree.LocationModels.Add(LocationModel.GetModelFor(location, _localization, null)));
        }

        public void AddLocationError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.AddLocationError());
        }

        public void RemoveLocation(Location location)
        {
            InvokeActionOnGuiInterfaces(gui => gui.RemoveLocation(location));
            _guiDispatcher?.Invoke(() =>
            {
                var foundLoc = LocationTree.LocationModels.FirstOrDefault(x => x?.Entity.EqualsById(location) ?? location == null);
                if (foundLoc != null)
                {
                    LocationTree.LocationModels.Remove(foundLoc);
                }
            });
        }

        public void ShowRemoveLocationError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowRemoveLocationError());
        }

        public void ShowPictureForLocation(Picture picture, LocationId locationId)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowPictureForLocation(picture, locationId));
        }

        public void ShowCommentForLocation(string comment, LocationId locationId)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowCommentForLocation(comment, locationId));
        }

        public void UpdateLocation(Location location)
        {
            InvokeActionOnGuiInterfaces(gui => gui.UpdateLocation(location));
        }

        public void UpdateLocationError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.UpdateLocationError());
        }

        public void LocationAlreadyExists()
        {
            InvokeActionOnGuiInterfaces(gui => gui.LocationAlreadyExists());
        }

        public void AddLocationDirectory(LocationDirectory locationDirectory)
        {
            InvokeActionOnGuiInterfaces(gui => gui.AddLocationDirectory(locationDirectory));
            _guiDispatcher?.Invoke(() => LocationTree.LocationDirectoryModels.Add(LocationDirectoryHumbleModel.GetModelFor(locationDirectory, null)));
        }

        public void ShowAddLocationDirectoryError(string name)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowAddLocationDirectoryError(name));
        }

        public void RemoveDirectory(LocationDirectoryId selectedDirectoryId)
        {
            InvokeActionOnGuiInterfaces(gui => gui.RemoveDirectory(selectedDirectoryId));
            _guiDispatcher?.Invoke(() =>
            {
                if (LocationTree.LocationDirectoryModels.FirstOrDefault(x => x.Id == selectedDirectoryId.ToLong()) is LocationDirectoryHumbleModel locDir)
                {
                    LocationTree.LocationDirectoryModels.Remove(locDir);
                }
            });
        }

        public void ShowRemoveDirectoryError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowRemoveDirectoryError());
        }

        public void ChangeLocationParent(Location location, LocationDirectoryId newParentId)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ChangeLocationParent(location, newParentId));
            _guiDispatcher?.Invoke(() =>
            {
                var locationModel = LocationTree.LocationModels.FirstOrDefault(x => x.Entity.EqualsById(location));
                if (locationModel != null)
                {
                    locationModel.ParentId = newParentId.ToLong();
                }
            });
        }

        public void ChangeLocationParentError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ChangeLocationParentError());
        }

        public void ShowLocation(Location location)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowLocation(location));
            _guiDispatcher?.Invoke(() => LocationTree.LocationModels.Add(LocationModel.GetModelFor(location, _localization, null)));
        }

        public void ChangeLocationDirectoryParent(LocationDirectory directory, LocationDirectoryId newParentId)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ChangeLocationDirectoryParent(directory, newParentId));
            _guiDispatcher?.Invoke(() =>
            {
                var locationDirectoryModel = LocationTree.LocationDirectoryModels.FirstOrDefault(x => x.Entity.EqualsById(directory));
                if (locationDirectoryModel != null)
                {
                    locationDirectoryModel.ParentId = newParentId.ToLong();
                }
            });
        }

        public void ChangeLocationDirectoryParentError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ChangeLocationDirectoryParentError());
        }

        public void ShowChangeLocationToolAssignmentNotice()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowChangeLocationToolAssignmentNotice());
        }

        public void ShowChangeToolStatusDialog(Action onSuccess, List<LocationToolAssignment> locationToolAssignments)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowChangeToolStatusDialog(onSuccess, locationToolAssignments));
        }
    }
}
