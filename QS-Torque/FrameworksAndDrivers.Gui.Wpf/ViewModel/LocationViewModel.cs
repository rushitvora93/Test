using Core.Diffs;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Core.Entities.ReferenceLink;
using Core.Enums;
using FrameworksAndDrivers.Threads;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;
using Client.Core.ChangesGenerators;
using Core;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class LocationValidator
    {
        /// <returns>true if SelectedLocation is valid</returns>
        public virtual bool ValidateLocation(LocationModel location)
        {
            if (string.IsNullOrEmpty(location?.Number) || string.IsNullOrEmpty(location?.Description))
            {
                return false;
            }

            if (location.ControlledBy == LocationControlledBy.Torque)
            {
                if (location.SetPointTorque <= 0 || location.SetPointAngle < 0)
                {
                    return false;
                }
            }

            if (location.ControlledBy == LocationControlledBy.Angle)
            {
                if (location.SetPointAngle <= 0 || location.SetPointTorque < 0)
                {
                    return false;
                }
            }

            if (location.SetPointTorque > 0 && location.ThresholdTorque > location.SetPointTorque)
            {
                return false;
            }

            if (location.SetPointTorque < location.MinimumTorque || location.SetPointTorque > location.MaximumTorque)
            {
                return false;
            }

            if (location.MinimumTorque < 0 || location.MaximumTorque < 0)
            {
                return false;
            }

            if (location.SetPointAngle < location.MinimumAngle || location.SetPointAngle > location.MaximumAngle)
            {
                return false;
            }

            if (location.MinimumAngle < 0 || location.MaximumAngle < 0)
            {
                return false;
            }
            

            if (location.ThresholdTorque <= 0)
            {
                return false;
            }

            return true;
        }
    }

    public class LocationViewModel : BindableBase, ILocationGui, IToleranceClassGui, IClearShownChanges, ICanClose
    {
        public EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler<ICanShowDialog> ShowDialogRequest;
        public event EventHandler<LocationModel> SelectionRequest;
        public event EventHandler<LocationDirectoryHumbleModel> FolderSelectionRequest;
        public event EventHandler ClearShownChanges;
        public EventHandler<(Action<MessageBoxResult,string>, Predicate<string>)> LocationDirectoryNameRequest;
        public event EventHandler<VerifyChangesEventArgs> RequestChangesVerification;
        public event EventHandler InitializeLocationTreeRequest;
        public event EventHandler<LocationDirectoryHumbleModel> CutLocationDirectory;
        public event EventHandler<LocationModel> CutLocation;
        public event EventHandler ResetCutMarking;

        private ILocationDisplayFormatter _locationDisplayFormatter;
        private ILocationUseCase _useCase;
        private IStartUp _startUp;
        private static readonly ILog Log = LogManager.GetLogger(typeof(LocationUseCase));
        private IMovableTreeItem cutItem;

        #region Properties

        public RelayCommand LoadTreeCommand { get; private set; }
        public RelayCommand AddLocationCommand { get; private set; }
        public RelayCommand RemoveLocationOrDirectoryCommand { get; private set; }
        public RelayCommand AddLocationDirectoryCommand { get; private set; }
        public RelayCommand SaveLocationCommand { get; private set; }
        public RelayCommand CutCommand { get; private set; }
        public RelayCommand PasteCommand { get; private set; }

        private LocationTreeModel _locationTree;
        public LocationTreeModel LocationTree
        {
            get => _locationTree;
            set => Set(ref _locationTree, value);
        }

        private LocationModel _selectedLocationBeforeChange;

        private LocationModel _selectedLocation;
        public LocationModel SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                if (_selectedLocationBeforeChange != null && _selectedLocation != null)
                {
                    if (_selectedLocation == value)
                    {
                        return;
                    }

                    if (!_locationValidator.ValidateLocation(_selectedLocation))
                    {
                        bool continueEditing = true;
                        MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs(r =>
                            {
                                if (r == MessageBoxResult.No)
                                {
                                    // Reset selected location if user does not want to continue editing
                                    SelectedLocation.UpdateWith(_selectedLocationBeforeChange?.Entity);
                                    continueEditing = false;
                                }
                            },
                            _localization.Strings.GetParticularString("Location", "The location is not valid, do you want to continue editing? (If not, the location is reseted to the last saved value)"),
                            _localization.Strings.GetParticularString("Location", "Location not valid"),
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Error));

                        if (continueEditing)
                        {
                            SelectionRequest?.Invoke(this, _selectedLocation);
                            return;
                        }
                    }

                    if (!_selectedLocation.EqualsByContent(_selectedLocationBeforeChange))
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
                                break;
                            case MessageBoxResult.No:
                                SelectedLocation.UpdateWith(_selectedLocationBeforeChange?.Entity);
                                break;
                            case MessageBoxResult.Cancel:
                                SelectionRequest?.Invoke(this, _selectedLocation);
                                return;
                        }
                    }
                }

                Set(ref _selectedLocation, value);
                if(value != null)
                {
                    _useCase.LoadPictureForLocation(new LocationId(_selectedLocation.Id));
                    _useCase.LoadCommentForLocation(new LocationId(_selectedLocation.Id));
                    _selectedLocationBeforeChange = _selectedLocation.CopyDeep();
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
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private LocationValidator _locationValidator;
        private ILocalizationWrapper _localization;

        private Dispatcher _guiDispatcher;
        private IThreadCreator _threadCreator;

        public ObservableCollection<ToleranceClassModel> ToleranceClasses { get; set; }
        
        #endregion

        public LocationViewModel(ILocationUseCase useCase, IStartUp startUp, LocationValidator locationValidator, ILocalizationWrapper localization, IThreadCreator threadCreator, ILocationDisplayFormatter locationDisplayFormatter)
        {
            _useCase = useCase;
            _startUp = startUp;
            _locationValidator = locationValidator;
            _localization = localization;
            _threadCreator = threadCreator;
            _locationDisplayFormatter = locationDisplayFormatter;
            LoadTreeCommand = new RelayCommand(LoadTreeExecute, x => true);
            AddLocationCommand = new RelayCommand(AddLocationExecute, arg => true);
            RemoveLocationOrDirectoryCommand = new RelayCommand(RemoveLocationOrDirectoryExecute, CanExecuteRemoveLocationOrDirectory);
            AddLocationDirectoryCommand = new RelayCommand(AddLocationFolderExecute, CanExecuteAddLocationFolder);
            SaveLocationCommand = new RelayCommand(SaveLocationExecute, SaveLocationCanExecute);
            CutCommand = new RelayCommand(CutExecute, CutCanExecute);
            PasteCommand = new RelayCommand(PasteExecute, PasteCanExecute);
            ToleranceClasses = new ObservableCollection<ToleranceClassModel>();
            LocationTree = new LocationTreeModel();
        }

        private bool PasteCanExecute(object arg)
        {
            if (cutItem == null)
            {
                return false;
            }

            if (!(cutItem is LocationDirectoryHumbleModel ldm))
            {
                return true;
            }

            //Check if same folder is selected
            var parentId = SelectedLocation?.ParentId ?? SelectedDirectoryHumble?.Id;
            if (parentId == ldm.Id)
            {
                return false;
            }
            var canPasteExecute = false;
            //Check if there is a Directory with same name
            var sameId = LocationTree.LocationDirectoryModels.Where(x => x.ParentId == parentId);
            var count = sameId.Count(x => x.Name == ldm.Name.Trim());
            canPasteExecute = count == 0;
            //Check if SelectedDirectory is subfolder of himself
            if (SelectedDirectoryHumble != null)
            {
                canPasteExecute = canPasteExecute && !IsFolderSubfolderOfHimself(SelectedDirectoryHumble, ldm.Id);
            }
            else if (SelectedLocation != null)
            {
                canPasteExecute = canPasteExecute && !IsFolderSubfolderOfHimself(
                                      LocationTree.LocationDirectoryModels.FirstOrDefault(
                                          x => x.Id == SelectedLocation.ParentId), ldm.Id);
            }
            return canPasteExecute;
        }

        private bool IsFolderSubfolderOfHimself(LocationDirectoryHumbleModel currentItem, long searchId)
        {
            if (currentItem == null)
            {
                return false;
            }
            if (currentItem.ParentId == 0)
            {
                return false;
            }
            if (currentItem.ParentId == searchId)
            {
                return true;
            }
            var directory = LocationTree.LocationDirectoryModels.FirstOrDefault(x => x.Id == currentItem.ParentId);
            return IsFolderSubfolderOfHimself(directory, searchId);
        }

        private void PasteExecute(object obj)
        {
            long parentId = 0;
            if (SelectedDirectoryHumble != null)
            {
                parentId = SelectedDirectoryHumble.Id;
            }
            else if (SelectedLocation != null)
            {
                parentId = SelectedLocation.ParentId;
            }

            cutItem.ChangeParent(new LocationDirectoryId(parentId));
        }

        private bool CutCanExecute(object arg)
        {
            var canExecuteCut = false;
            if (SelectedLocation != null)
            {
                canExecuteCut = true;
            }
            else if (SelectedDirectoryHumble != null)
            {
                canExecuteCut = SelectedDirectoryHumble.ParentId != 0;
            }
            return canExecuteCut;
        }

        private void CutExecute(object obj)
        {
            if (SelectedLocation != null)
            {
                CutLocation?.Invoke(this,SelectedLocation);
                cutItem = SelectedLocation;
            }
            else if (SelectedDirectoryHumble != null)
            {
                CutLocationDirectory?.Invoke(this, SelectedDirectoryHumble);
                cutItem = SelectedDirectoryHumble;
            }
            
        }

        private bool CanExecuteAddLocationFolder(object arg)
        {
            return true;
        }

        private void AddLocationFolderExecute(object obj)
        {
            LocationDirectoryId parentDirectoryId = null;
            if (SelectedDirectoryHumble != null)
            {
                parentDirectoryId = new LocationDirectoryId(SelectedDirectoryHumble.Id);
            }
            else if (SelectedLocation != null)
            {
                parentDirectoryId = new LocationDirectoryId(SelectedLocation.ParentId);
            }
            else
            {
                if (LocationTree.LocationDirectoryModels.FirstOrDefault(x =>
                    x.ParentId == 0) is LocationDirectoryHumbleModel rootDirectory)
                {
                    parentDirectoryId = new LocationDirectoryId(rootDirectory.Id);
                }
                else
                {
                    parentDirectoryId = new LocationDirectoryId(0);
                }
            }

            void ResultAction(MessageBoxResult result, string s)
            {
                if (result == MessageBoxResult.OK)
                {
                    _useCase.AddLocationDirectory(parentDirectoryId, s.Trim());
                }
            }

            bool CheckIfUnique(string s)
            {
                var sameId = LocationTree.LocationDirectoryModels.Where(x => x.ParentId == parentDirectoryId.ToLong());
                var count = sameId.Count(x => x.Name == s.Trim());
                return count == 0;
            }

            LocationDirectoryNameRequest?.Invoke(this,(ResultAction, CheckIfUnique));
        }
        private bool CanExecuteRemoveLocationOrDirectory(object arg)
        {
            if (SelectedLocation != null)
            {
                return true;
            }
            if (SelectedDirectoryHumble == null || SelectedDirectoryHumble.ParentId == 0)
            {
                return false;
            }
            return true;
        }

        private void RemoveLocationOrDirectoryExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);

            if (SelectedLocation != null)
            {
                Action<MessageBoxResult> resultAction = (r) =>
                {
                    if (r != MessageBoxResult.Yes)
                    {
                        _startUp.RaiseShowLoadingControl(false);
                        return;
                    }

                    _useCase.RemoveLocation(_selectedLocationBeforeChange?.Entity);
                };

                var args = new MessageBoxEventArgs(resultAction,
                    _localization.Strings.GetString("Do you really want to remove this item?"),
                    _localization.Strings.GetParticularString("Window Title", "Warning"),
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                MessageBoxRequest?.Invoke(this, args);
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

                        _useCase.RemoveDirectory(SelectedDirectoryHumble.Entity);
                    },
                    _localization.Strings.GetParticularString("LocationViewModel", "Do you  want this folder and all assigned Locations to be moved to the trash?"),
                    "",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                MessageBoxRequest?.Invoke(this, args);
            }
        }

        public void SetDispatcher(Dispatcher dispatcher)
        {
            _guiDispatcher = dispatcher;
        }

        private void LoadTreeExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true, 3);
            _useCase.ToleranceClassUseCase.LoadToleranceClasses();
            _useCase.LoadTree(this);
        }

        private void AddLocationExecute(object obj)
        {
            var previousSelectedLocation = SelectedLocation;
            SelectedLocation = null;
            AssistentView assistent;

            _startUp.RaiseShowLoadingControl(true, 5);

            if (previousSelectedLocation == null)
            {
                assistent = _startUp.OpenAddLocationAssistent();
            }
            else
            {
                assistent = _startUp.OpenAddLocationAssistent(previousSelectedLocation?.Entity);
            }

            assistent.EndOfAssistent += (s, e) =>
            {
                var location = (Location)(assistent.DataContext as AssistentViewModel).FillResultObject(new Location());

                if (previousSelectedLocation != null)
                {
                    location.ParentDirectoryId = new LocationDirectoryId(previousSelectedLocation.ParentId);
                }
                else if (SelectedDirectoryHumble != null)
                {
                    location.ParentDirectoryId = new LocationDirectoryId(SelectedDirectoryHumble.Id);
                }
                else
                {
                    var idOfRoot = LocationTree.LocationDirectoryModels.FirstOrDefault(x => x.ParentId == 0)?.Id;

                    if (idOfRoot != null)
                    {
                        location.ParentDirectoryId = new LocationDirectoryId(idOfRoot.Value);
                    }
                    else
                    {
                        Log.Error("AddLocation was called with no root node");
                        AddLocationError();
                        return;
                    }
                }

                location.Id = new LocationId(0);
                _useCase.AddLocation(location);
            };
            assistent.Closed += (s, e) =>
            {
                if (assistent.DialogResult != true)
                {
                    _startUp.RaiseShowLoadingControl(false); 
                }
                SelectedLocation = previousSelectedLocation;
            };

            ShowDialogRequest?.Invoke(this, assistent as ICanShowDialog);
        }

        private bool SaveLocationCanExecute(object arg)
        {
            if (SelectedLocation == null)
            {
                return false;
            }

            if (!_locationValidator.ValidateLocation(_selectedLocation))
            {
                return false;
            }

            return !SelectedLocation.EqualsByContent(_selectedLocationBeforeChange);
        }

        private void SaveLocationExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);

            var diff = new LocationDiff()
            {
                OldLocation = _selectedLocationBeforeChange?.Entity,
                NewLocation = SelectedLocation?.Entity
            };

            var result = VerifyLocationDiff(diff);

            if (result == MessageBoxResult.Yes)
            {
                _useCase.UpdateLocation(diff);
            }
            if (result == MessageBoxResult.No)
            {
                SelectedLocation.UpdateWith(_selectedLocationBeforeChange?.Entity);
                ClearShownChanges?.Invoke(this, null);
            }
            if (result != MessageBoxResult.Yes)
            {
                _startUp.RaiseShowLoadingControl(false);
            }
        }

        public void ShowLocationTree(List<LocationDirectory> directories)
        {
            _guiDispatcher.Invoke(() =>
            {
                LocationTree = new LocationTreeModel();
                directories.ForEach(x => LocationTree.LocationDirectoryModels.Add(LocationDirectoryHumbleModel.GetModelFor(x, _useCase)));
                InitializeLocationTreeRequest?.Invoke(this, null);
            });
        }

        public void ShowLocationTreeError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Location", "An error occured while loading the tree"),
                messageBoxImage:MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void ShowLoadingLocationTreeFinished()
        {
            _startUp.RaiseShowLoadingControl(false, 2);
        }

        public void AddLocation(Location location)
        {
            _guiDispatcher.Invoke(() =>
            {
                var locationModel = LocationModel.GetModelFor(location, _localization, _useCase);
                LocationTree.LocationModels.Add(locationModel);
                SelectionRequest?.Invoke(this, locationModel);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowLocation(Location location)
        {
            _guiDispatcher.Invoke(() =>
            {
                var locationModel = LocationModel.GetModelFor(location, _localization, _useCase);
                LocationTree.LocationModels.Add(locationModel);
            });
        }

        public void ChangeLocationDirectoryParent(LocationDirectory directory, LocationDirectoryId newParentId)
        {
            _guiDispatcher.Invoke(() =>
            {
                var locationDirectoryModel = LocationTree.LocationDirectoryModels.FirstOrDefault(x => x.Entity.EqualsById(directory));
                if (locationDirectoryModel != null)
                {
                    locationDirectoryModel.ParentId = newParentId.ToLong();
                    cutItem = null;
                    ResetCutMarking?.Invoke(this,System.EventArgs.Empty);
                }
            });
        }

        public void ChangeLocationDirectoryParentError()
        {
            throw new NotImplementedException();
        }

        public void ShowChangeLocationToolAssignmentNotice()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Location", "Test conditions are attached to this Location which may have to be changed"),
                _localization.Strings.GetParticularString("Location", "Warning"),
                messageBoxImage: MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowChangeToolStatusDialog(Action onSuccess, List<LocationToolAssignment> locationToolAssignments)
        {
            _guiDispatcher.Invoke(() =>
            {
                var assistant = _startUp.OpenChangeToolStatusAssistant(locationToolAssignments);
                assistant.EndOfAssistent += (sender, args) =>
                {
                    (assistant.DataContext as ChangeToolStateViewModel).SaveNewToolStates();
                    _threadCreator.Run(() =>
                    {
                        onSuccess.Invoke();
                    });
                };
                ShowDialogRequest?.Invoke(this, assistant as ICanShowDialog);
            });
        }

        public void AddLocationError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Location", "An error occured while adding the location"),
                messageBoxImage: MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void RemoveLocation(Location location)
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

                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowRemoveLocationError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("LocationViewModel", "An error occured in Location"),
                "",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void ShowPictureForLocation(Picture picture, LocationId locationId)
        {
            _guiDispatcher.Invoke(() =>
            {
                if (picture is null || locationId is null)
                {
                    return;
                }
                var locationModel = LocationTree.LocationModels.FirstOrDefault(x => x.Id == locationId.ToLong());
                if (locationModel is null)
                {
                    return;
                }
                locationModel.Picture = PictureModel.GetModelFor(picture);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowCommentForLocation(string comment, LocationId locationId)
        {
            _guiDispatcher.Invoke(() =>
            {
                if (locationId is null || comment is null)
                {
                    return;
                }

                var locationModel = LocationTree.LocationModels.FirstOrDefault(x => x.Id == locationId.ToLong());
                if (locationModel is null)
                {
                    return;
                }

                locationModel.Comment = comment;

                if (SelectedLocation?.EqualsById(locationModel) == true && _selectedLocationBeforeChange != null)
                {
                    _selectedLocationBeforeChange.Comment = comment;
                }

                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public MessageBoxResult VerifyLocationDiff(LocationDiff diff)
        {
            if (diff == null)
            {
                return MessageBoxResult.None;
            }

            var changesGenerator = new LocationChangesGenerator(_localization.Strings, _locationDisplayFormatter);
            var changes = changesGenerator.GetLocationChanges(diff).ToList();
            if (changes.Count == 0)
            {
                return MessageBoxResult.OK;
            }
            var args = new VerifyChangesEventArgs(changes.Select(x => SingleValueChangeModel.GetModelFor(x)).ToList());
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

        public void UpdateLocationError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Location", "An error occured while updating the location"),
                messageBoxImage: MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void LocationAlreadyExists()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Location", "A location with this number or description already exists"),
                messageBoxImage: MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void AddLocationDirectory(LocationDirectory locationDirectory)
        {
            _guiDispatcher.Invoke(() =>
            {
                var locationDirectoryModel = LocationDirectoryHumbleModel.GetModelFor(locationDirectory, _useCase);
                LocationTree.LocationDirectoryModels.Add(locationDirectoryModel);

                FolderSelectionRequest?.Invoke(this, locationDirectoryModel);
            });
        }

        public void ShowAddLocationDirectoryError(string name)
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("LocationViewModel", "Adding a Directory in Location failed. Please try again or contact admin or support"),
                "",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void RemoveDirectory(LocationDirectoryId selectedDirectoryId)
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

        public void ShowRemoveDirectoryError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("LocationViewModel",
                    "Removing a Directory failed. Pls try again or contact admin or support"),
                "",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void ChangeLocationParent(Location location, LocationDirectoryId newParentId)
        {
            _guiDispatcher.Invoke(() =>
            {
                var locationModel = LocationTree.LocationModels.FirstOrDefault(x => x.Entity.EqualsById(location));
                if (locationModel != null)
                {
                    locationModel.ParentId = newParentId.ToLong();
                    cutItem = null;
                    ResetCutMarking?.Invoke(this, System.EventArgs.Empty);
                }
            });
        }

        public void ChangeLocationParentError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("LocationViewModel",
                    "Moving Location failed. Pls reload, try again or contact admin or support"),
                "",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void ShowToleranceClasses(List<ToleranceClass> toleranceClasses)
        {
            _guiDispatcher.Invoke(() =>
            {
                ToleranceClasses.Clear();
                toleranceClasses.ForEach(x => ToleranceClasses.Add(ToleranceClassModel.GetModelFor(x)));
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void AddToleranceClass(ToleranceClass toleranceClass)
        {
            _guiDispatcher.Invoke(() => { ToleranceClasses.Add(ToleranceClassModel.GetModelFor(toleranceClass)); });
        }

        public void ShowToleranceClassesError()
        {
            //Intentioanaly empty
        }

        public void RemoveToleranceClass(ToleranceClass toleranceClass)
        {
           //Intentiaonaly empty
        }

        public void RemoveToleranceClassError()
        {
            //Intentiaonaly empty
        }

        public void AddToleranceClassError()
        {
            //Intentiaonaly empty
        }

        public void UpdateToleranceClass(ToleranceClass toleranceClass)
        {
            //Intentiaonaly empty
        }

        public void SaveToleranceClassError()
        {
            //Intentiaonaly empty
        }

        public void ShowReferencedLocations(List<LocationReferenceLink> locationReferenceLinks)
        {
            // Do nothing
        }

        public void ShowReferencesError()
        {
            // Do nothing
        }

        public void ShowReferencedLocationToolAssignments(List<LocationToolAssignment> assignments)
        {
            // Do nothing
        }

        public void ShowRemoveToleranceClassPreventingReferences(List<LocationReferenceLink> referencedLocations, List<LocationToolAssignment> referencedLocationToolAssignments)
        {
            // Do nothing
        }

        public bool CanClose()
        {
            if (SelectedLocation == null)
            {
                return true;
            }

            if (!_locationValidator.ValidateLocation(_selectedLocation))
            {
                bool continueEditing = true;
                MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs(r =>
                    {
                        if (r == MessageBoxResult.No)
                        {
                            // Reset selected location if user does not want to continue editing
                            SelectedLocation.UpdateWith(_selectedLocationBeforeChange?.Entity);
                            continueEditing = false;
                        }
                    },
                    _localization.Strings.GetParticularString("Location", "The location is not valid, do you want to continue editing? (If not, the location is reseted to the last saved value)"),
                    _localization.Strings.GetParticularString("Location", "Location not valid"),
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Error));
                return !continueEditing;
            }

            if (_selectedLocation != null && _selectedLocationBeforeChange!= null && !_selectedLocation.EqualsByContent(_selectedLocationBeforeChange))
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
    }
}
