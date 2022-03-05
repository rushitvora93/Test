using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Client.Core.Diffs;
using Common.Types.Enums;
using Core.Diffs;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.View.Dialogs;
using FrameworksAndDrivers.Threads;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;
using log4net;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class TrashViewModel : BindableBase, ITrashGui, IClearShownChanges, ICanClose
    {
        private readonly IThreadCreator _threadCreator;
        public EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler<LocationModel> SelectionRequest;
        public event EventHandler ClearShownChanges;
        public EventHandler<(Action<MessageBoxResult, string>, Predicate<string>)> LocationDirectoryNameRequest;
        public event EventHandler<VerifyChangesEventArgs> RequestChangesVerification;
        public event EventHandler InitializeLocationTreeRequest;

        private ITrashUseCase _useCase;
        private ITrashInterface _trashInterface;
        private ILocationUseCase _locationUseCase;
        private IStartUp _startUp;
        private static readonly ILog Log = LogManager.GetLogger(typeof(TrashUseCase));

        private readonly IExtensionUseCase _extensionUseCase;
        private readonly IExtensionInterface _extensionInterface;

        #region Properties

        public RelayCommand LoadTreeCommand { get; private set; }

        public RelayCommand RestoreLocationOrDirectoryCommand { get; private set; }

        private LocationTreeModel _locationTree;
        public LocationTreeModel LocationTree
        {
            get => _locationTree;
            set => Set(ref _locationTree, value);
        }

        private ExtensionModel _extensionTree;
        public ExtensionModel ExtensionTree
        {
            get => _extensionTree;
            set => Set(ref _extensionTree, value);
        }

        private LocationModel _selectedLocationBeforeChange;
        public ObservableCollection<LocationModel> SelectedLocations { get; private set; }

        private LocationModel _selectedLocation;
        public LocationModel SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                Set(ref _selectedLocation, value);
                if (value != null)
                {
                    _selectedLocationBeforeChange = _selectedLocation.CopyDeep();
                    SelectedLocations.Clear();
                    SelectedLocations.Add(value);
                }
                CommandManager.InvalidateRequerySuggested();
                ClearShownChanges?.Invoke(this, null);
            }
        }

        private LocationDirectoryHumbleModel _selectedDirectoryHumble;

        public LocationDirectoryHumbleModel SelectedDirectoryHumble
        {
            get => _selectedDirectoryHumble;
            set
            {
                Set(ref _selectedDirectoryHumble, value);
                if (value != null)
                {
                    SelectedLocations.Clear();
                    foreach (var location in _locationTree.LocationModels)
                    {
                        if (location.ParentId == value.Entity.Id.ToLong())
                        {
                            SelectedLocations.Add(location);
                        }
                    }
                }
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private ILocalizationWrapper _localization;

        private Dispatcher _guiDispatcher;

        private IFilteredObservableCollectionExtension<ExtensionModel> _extensions;

        public IFilteredObservableCollectionExtension<ExtensionModel> Extensions
        {
            get => _extensions;
            set
            {
                _extensions = value;
                RaisePropertyChanged();
            }
        }

        public ExtensionModel SelectedExtension
        {
            get => _extensionInterface.SelectedExtension;
            set
            {
                if (_extensionInterface.SelectedExtension == value)
                {
                    return;
                }

                if (_extensionInterface.SelectedExtension != null &&
                    _extensionInterface.SelectedExtensionWithoutChanges != null &&
                    !_extensionInterface.SelectedExtension.EqualsByContent(_extensionInterface.SelectedExtensionWithoutChanges))
                {
                    var diff = new ExtensionDiff(null, null,
                        _extensionInterface.SelectedExtensionWithoutChanges?.Entity,
                        SelectedExtension?.Entity);
                }


                _extensionInterface.SelectedExtension = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        public TrashViewModel(ITrashUseCase useCase, IStartUp startUp, ILocalizationWrapper localization, IThreadCreator threadCreator,
            ILocationUseCase locationUseCase, ITrashInterface TrashInterface, IExtensionUseCase extensionUseCase, IExtensionInterface extensionInterface,
            IFilteredObservableCollectionExtension<ExtensionModel> extensions = null)
        {
            _useCase = useCase;
            _locationUseCase = locationUseCase;
            _startUp = startUp;
            _localization = localization;
            _threadCreator = threadCreator;
            _extensionUseCase = extensionUseCase;
            _extensionInterface = extensionInterface;
            _trashInterface = TrashInterface;

            LoadTreeCommand = new RelayCommand(LoadTreeExecute, x => true);
            
            RestoreLocationOrDirectoryCommand = new RelayCommand(RestoreLocationOrDirectoryExecute, CanExecuteRestoreLocationOrDirectory);
            SelectedLocations = new ObservableCollection<LocationModel>();

        }

        public void SetDispatcher(Dispatcher dispatcher)
        {
            _guiDispatcher = dispatcher;
            _trashInterface.SetGuiDispatcher(dispatcher);
        }

        public MessageBoxResult VerifyLocationDiff(LocationDiff diff)
        {
            if (diff == null)
            {
                return MessageBoxResult.None;
            }

            var changes = GetChangesFromDiff(diff).ToList();
            if (changes.Count == 0)
            {
                return MessageBoxResult.OK;
            }
            var args = new VerifyChangesEventArgs(changes);
            RequestChangesVerification?.Invoke(this, args);
            diff.Comment = new HistoryComment(args.Comment);
            return args.Result;
        }

        public void UpdateLocation(Location location)
        {
            if (_selectedLocationBeforeChange?.Entity.EqualsById(location) ?? location == null)
            {
                _selectedLocationBeforeChange.UpdateWith(location);
            }

            _guiDispatcher.Invoke(() =>
            {
                LocationTree.LocationModels.FirstOrDefault(x => x.Entity.EqualsById(location))?.UpdateWith(location);
                ClearShownChanges?.Invoke(this, null);
                _startUp.RaiseShowLoadingControl(false);
                CommandManager.InvalidateRequerySuggested();
            });
        }

        public bool CanClose()
        {
            if (SelectedLocation == null)
            {
                return true;
            }

            if (_selectedLocation != null && _selectedLocationBeforeChange != null && !_selectedLocation.EqualsByContent(_selectedLocationBeforeChange))
            {
                var diff = new LocationDiff()
                {
                    OldLocation = _selectedLocationBeforeChange?.Entity,
                    NewLocation = _selectedLocation?.Entity
                };

                var result = VerifyLocationDiff(diff);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        _startUp.RaiseShowLoadingControl(true);
                        _useCase.UpdateLocation(diff);
                        return true;
                    case MessageBoxResult.No:
                        SelectedLocation.UpdateWith(_selectedLocationBeforeChange?.Entity);
                        return true;
                    case MessageBoxResult.Cancel:
                        SelectionRequest?.Invoke(this, _selectedLocation);
                        return false;
                }
            }
            return true;
        }

        public void ShowLocationTree(List<LocationDirectory> directories)
        {
            _guiDispatcher.Invoke(() =>
            {
                if (LocationTree == null)
                {
                    LocationTree = new LocationTreeModel();
                }

                directories.ForEach(x => LocationTree.LocationDirectoryModels.Add(LocationDirectoryHumbleModel.GetModelFor(x, _locationUseCase)));
                InitializeLocationTreeRequest?.Invoke(this, null);
            });
        }

        public void ShowLocationTreeError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Location", "An error occured while loading the tree"),
                messageBoxImage: MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void ShowLoadingLocationTreeFinished()
        {
            _startUp.RaiseShowLoadingControl(false, 2);
        }

        public void ShowLocation(Location location)
        {
            _guiDispatcher.Invoke(() =>
            {
                if (LocationTree == null)
                {
                    LocationTree = new LocationTreeModel();
                }

                var locationModel = LocationModel.GetModelFor(location, _localization, _locationUseCase);
                LocationTree.LocationModels.Add(locationModel);
            });
        }

        private IEnumerable<SingleValueChangeModel> GetChangesFromDiff(LocationDiff diff)
        {
            string entity = $"{diff.NewLocation.Number.ToDefaultString()} - {diff.NewLocation.Description.ToDefaultString()}";

            if (diff.OldLocation.Number?.ToDefaultString() != diff.NewLocation.Number?.ToDefaultString())
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationAttribute", "Number"),
                    OldValue = diff.OldLocation.Number?.ToDefaultString(),
                    NewValue = diff.NewLocation.Number?.ToDefaultString()
                };
            }
            if (diff.OldLocation.Description?.ToDefaultString() != diff.NewLocation.Description?.ToDefaultString())
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationAttribute", "Description"),
                    OldValue = diff.OldLocation.Description?.ToDefaultString(),
                    NewValue = diff.NewLocation.Description?.ToDefaultString()
                };
            }
            if (!diff.OldLocation.ControlledBy.Equals(diff.NewLocation.ControlledBy))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationAttribute", "Controlled by"),
                    OldValue = _localization.Strings.GetParticularString("LocationAttribute", diff.OldLocation.ControlledBy.ToString()),
                    NewValue = _localization.Strings.GetParticularString("LocationAttribute", diff.NewLocation.ControlledBy.ToString())
                };
            }
            if (!diff.OldLocation.SetPointTorque.Equals(diff.NewLocation.SetPointTorque))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationAttribute", "Setpoint torque"),
                    OldValue = diff.OldLocation.SetPointTorque.Nm.ToString(CultureInfo.CurrentCulture),
                    NewValue = diff.NewLocation.SetPointTorque.Nm.ToString(CultureInfo.CurrentCulture)
                };
            }
            if ((diff?.OldLocation?.ToleranceClassTorque == null && diff.OldLocation?.ToleranceClassTorque != null)
                || (!diff.OldLocation?.ToleranceClassTorque?.EqualsById(diff.NewLocation?.ToleranceClassTorque) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationAttribute", "Tolerance class torque"),
                    OldValue = diff.OldLocation.ToleranceClassTorque.Name,
                    NewValue = diff.NewLocation?.ToleranceClassTorque?.Name
                };
            }

            if ((diff?.OldLocation?.ToleranceClassAngle == null && diff.OldLocation?.ToleranceClassAngle != null)
                || (!diff.OldLocation?.ToleranceClassAngle?.EqualsById(diff.NewLocation?.ToleranceClassAngle) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationAttribute", "Tolerance class angle"),
                    OldValue = diff.OldLocation.ToleranceClassAngle.Name,
                    NewValue = diff.NewLocation?.ToleranceClassAngle?.Name
                };
            }
            if (!diff.OldLocation.MinimumTorque.Equals(diff.NewLocation.MinimumTorque))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationAttribute", "Minimum torque"),
                    OldValue = diff.OldLocation.MinimumTorque.Nm.ToString(CultureInfo.CurrentCulture),
                    NewValue = diff.NewLocation.MinimumTorque.Nm.ToString(CultureInfo.CurrentCulture)
                };
            }
            if (!diff.OldLocation.MaximumTorque.Equals(diff.NewLocation.MaximumTorque))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationAttribute", "Maximum torque"),
                    OldValue = diff.OldLocation.MaximumTorque.Nm.ToString(CultureInfo.CurrentCulture),
                    NewValue = diff.NewLocation.MaximumTorque.Nm.ToString(CultureInfo.CurrentCulture)
                };
            }
            if (!diff.OldLocation.ThresholdTorque.Equals(diff.NewLocation.ThresholdTorque))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationAttribute", "Threshold torque"),
                    OldValue = diff.OldLocation.ThresholdTorque.Nm.ToString(CultureInfo.CurrentCulture),
                    NewValue = diff.NewLocation.ThresholdTorque.Nm.ToString(CultureInfo.CurrentCulture)
                };
            }
            if (!diff.OldLocation.SetPointAngle.Equals(diff.NewLocation.SetPointAngle))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationAttribute", "Setpoint angle"),
                    OldValue = diff.OldLocation.SetPointAngle.Degree.ToString(CultureInfo.CurrentCulture),
                    NewValue = diff.NewLocation.SetPointAngle.Degree.ToString(CultureInfo.CurrentCulture)
                };
            }
            if (!diff.OldLocation.MinimumAngle.Equals(diff.NewLocation.MinimumAngle))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationAttribute", "Minimum angle"),
                    OldValue = diff.OldLocation.MinimumAngle.Degree.ToString(CultureInfo.CurrentCulture),
                    NewValue = diff.NewLocation.MinimumAngle.Degree.ToString(CultureInfo.CurrentCulture)
                };
            }
            if (!diff.OldLocation.MaximumAngle.Equals(diff.NewLocation.MaximumAngle))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationAttribute", "Maximum angle"),
                    OldValue = diff.OldLocation.MaximumAngle.Degree.ToString(CultureInfo.CurrentCulture),
                    NewValue = diff.NewLocation.MaximumAngle.Degree.ToString(CultureInfo.CurrentCulture)
                };
            }
            if (diff.OldLocation.ConfigurableField1?.ToDefaultString() != diff.NewLocation.ConfigurableField1?.ToDefaultString())
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationAttribute", "Cost center"),
                    OldValue = diff.OldLocation.ConfigurableField1?.ToDefaultString(),
                    NewValue = diff.NewLocation.ConfigurableField1?.ToDefaultString()
                };
            }
            if (diff.OldLocation.ConfigurableField2?.ToDefaultString() != diff.NewLocation.ConfigurableField2?.ToDefaultString())
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationAttribute", "Category"),
                    OldValue = diff.OldLocation.ConfigurableField2?.ToDefaultString(),
                    NewValue = diff.NewLocation.ConfigurableField2?.ToDefaultString()
                };
            }
            if (!diff.OldLocation.ConfigurableField3.Equals(diff.NewLocation.ConfigurableField3))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationAttribute", "Documentation"),
                    OldValue = _localization.Strings.GetString(diff.OldLocation.ConfigurableField3 ? "Yes" : "No"),
                    NewValue = _localization.Strings.GetString(diff.NewLocation.ConfigurableField3 ? "Yes" : "No")
                };
            }
            if (diff.OldLocation.Comment != diff.NewLocation.Comment)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationAttribute", "Comment"),
                    OldValue = diff.OldLocation.Comment,
                    NewValue = diff.NewLocation.Comment
                };
            }
        }

        public void RestoreLocation(Location location)
        {
            if (location is null)
            {
                return;
            }

            _guiDispatcher.Invoke(() =>
            {
                var foundLoc = LocationTree.LocationModels.FirstOrDefault(x => x?.Entity.EqualsById(location) ?? location == null);
                if (foundLoc != null)
                {
                    LocationTree.LocationModels.Remove(foundLoc);
                }

                if (location.EqualsById(_selectedLocation?.Entity))
                {
                    _selectedLocation = null;
                }

                var currentDirectoryModels = new List<LocationDirectoryHumbleModel>(LocationTree.LocationDirectoryModels);
                foreach (var directory in currentDirectoryModels)
                {
                    if (!LocationTree.LocationModels.Any(x => x.ParentId.Equals(directory.Id)))
                    {
                        LocationTree.LocationDirectoryModels.Remove(directory);
                    }
                }

                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowRestoreLocationError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Trash", "An error occured while updating the location"),
                messageBoxImage: MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void RestoreDirectory(LocationDirectoryId selectedDirectoryId)
        {
            _guiDispatcher.Invoke(() =>
            {
                if (LocationTree.LocationDirectoryModels.FirstOrDefault(x => x.Id == selectedDirectoryId.ToLong()) is LocationDirectoryHumbleModel
                    locDir)
                {
                    LocationTree.LocationDirectoryModels.Remove(locDir);
                }
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        private void LoadTreeExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true, 3);
            _useCase.LoadTree(this);
        }

        private void RestoreLocationOrDirectoryExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);
            if (SelectedLocation != null)
            {
                if (!_locationUseCase.IsNumberUnique(_selectedLocationBeforeChange?.Number))
                {
                    Action<MessageBoxResult> resultAction = (r) =>
                    {
                        _startUp.RaiseShowLoadingControl(false);
                        return;
                    };

                    var messageBoxTitle = _localization.Strings.GetParticularString("TrashViewModel", "Location with same name exists");
                    var messageBoxMessage = _localization.Strings.GetParticularString("TrashViewModel", "A location with the same name already exists in this folder!");
                    var uniqueLocationIdResultActionMessageBoxArgs = new MessageBoxEventArgs(resultAction, messageBoxMessage, messageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Error);

                    MessageBoxRequest?.Invoke(this, uniqueLocationIdResultActionMessageBoxArgs);
                }
                else
                {
                    _useCase.RestoreLocation(_selectedLocationBeforeChange?.Entity);
                }
            }
            else if (SelectedDirectoryHumble != null)
            {
                var args = new MessageBoxEventArgs(result =>
                {
                    if (result != MessageBoxResult.Yes)
                    {
                        _startUp.RaiseShowLoadingControl(false);
                        return;
                    }

                    _useCase.RestoreDirectory(SelectedDirectoryHumble.Entity);
                },
                    _localization.Strings.GetParticularString("LocationViewModel", "Do you  want this folder and all assigned Locations to be restored?"),
                    "",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                MessageBoxRequest?.Invoke(this, args);
            }
            //else if (SelectedExtension != null)
            //{
            //    if (!_extensionUseCase.IsInventoryNumberUnique(_extensionTree?.Entity.InventoryNumber))
            //    {
            //        Action<MessageBoxResult> resultAction = (r) =>
            //        {
            //            _startUp.RaiseShowLoadingControl(false);
            //            return;
            //        };

            //        var messageBoxTitle = _localization.Strings.GetParticularString("TrashViewModel", "Test instrument could not be restored because the inventory number is already assigned. Please recreate the tester under a new inventory number.");
            //        var messageBoxMessage = _localization.Strings.GetParticularString("TrashViewModel", "Test instrument could not be restored because the inventory number is already assigned. Please recreate the tester under a new inventory number.");
            //        var uniqueExtensionResultActionMessageBoxArgs = new MessageBoxEventArgs(resultAction, messageBoxMessage, messageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Error);

            //        MessageBoxRequest?.Invoke(this, uniqueExtensionResultActionMessageBoxArgs);
            //    }
            //    //else if (!_extensionUseCase.IsInventoryNumberUnique(_extensionTree?.Entity))
            //    //{
            //    //    Action<MessageBoxResult> resultAction = (r) =>
            //    //    {
            //    //        _startUp.RaiseShowLoadingControl(false);
            //    //        return;
            //    //    };

            //    //    var messageBoxTitle = _localization.Strings.GetParticularString("TrashViewModel", "Test instrument could not be restored because the inventory number is already assigned. Please recreate the tester under a new inventory number.");
            //    //    var messageBoxMessage = _localization.Strings.GetParticularString("TrashViewModel", "Test instrument could not be restored because the inventory number is already assigned. Please recreate the tester under a new inventory number.");
            //    //    var uniqueExtensionResultActionMessageBoxArgs = new MessageBoxEventArgs(resultAction, messageBoxMessage, messageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Error);

            //    //    MessageBoxRequest?.Invoke(this, uniqueExtensionResultActionMessageBoxArgs);
            //    //}
            //    else
            //    {
            //        _useCase.RestoreLocation(_extensionTree?.Entity);
            //    }
            //}
        }

        private bool CanExecuteRestoreLocationOrDirectory(object arg)
        {
            if (SelectedLocation != null)
            {
                return true;
            }

            if (SelectedDirectoryHumble != null)
            {
                return true;
            }

            return false;
        }

        public void ShowDeletedExtensions(List<Extension> extensions)
        {
            throw new NotImplementedException();
        }

        public void ShowDeletedModelsWithAtLeastOneTool(List<Core.Entities.ToolModel> models)
        {
            throw new NotImplementedException();
        }

        public void RestoreTool(Tool tool)
        {
            throw new NotImplementedException();
        }

        public void RestoreExtension(Extension extension)
        {
            throw new NotImplementedException();
        }

        public void ShowDeletedToolModels(List<Core.Entities.ToolModel> toolModels)
        {
            throw new NotImplementedException();
        }

        //public bool IsListViewVisible => SelectedExtension != null;

        //public ExtensionModel SelectedExtensionWithoutChanges => _extensionInterface.SelectedExtensionWithoutChanges;
        //private void WireViewModelToExtensionInterface()
        //{
        //    PropertyChangedEventManager.AddHandler(
        //        _extensionInterface,
        //        (s, e) =>
        //        {
        //            RaisePropertyChanged(nameof(SelectedExtension));
        //            RaisePropertyChanged(nameof(IsListViewVisible));
        //            CommandManager.InvalidateRequerySuggested();
        //        },
        //        nameof(ExtensionInterfaceAdapter.SelectedExtension));


        //    PropertyChangedEventManager.AddHandler(
        //        _extensionInterface,
        //        (s, e) =>
        //        {
        //            Extensions.SetNewSourceCollection(_extensionInterface.Extensions);
        //            RaisePropertyChanged(nameof(Extensions));
        //            CommandManager.InvalidateRequerySuggested();
        //        },
        //        nameof(ExtensionInterfaceAdapter.Extensions));


        //    PropertyChangedEventManager.AddHandler(
        //        _extensionInterface,
        //        (s, e) => RaisePropertyChanged(nameof(SelectedExtensionWithoutChanges)),
        //        nameof(ExtensionInterfaceAdapter.SelectedExtensionWithoutChanges));
        //}

        //private void ExtensionInterface_ShowLoadingControlRequest(object sender, bool e)
        //{
        //    _startUp.RaiseShowLoadingControl(e);
        //}
    }
}