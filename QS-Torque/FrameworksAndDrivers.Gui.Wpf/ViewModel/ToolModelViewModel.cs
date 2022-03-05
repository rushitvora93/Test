using Core;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.Entities.ToolTypes;
using Core.Enums;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.Translation;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Gui.Wpf.View.Dialogs;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using InterfaceAdapters;
using ToolModel = Core.Entities.ToolModel;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class CmCmkModel
    {
        public double Cm { get; set; }
        public double Cmk { get; set; }
    }

    public class  ToolModelViewModel
        : BindableBase
        , IToolModelGui
        , IManufacturerGui
        , IHelperTableGui<ToolType>
        , IHelperTableReadOnlyErrorGui<ToolType>
        , IHelperTableGui<DriveSize>
        , IHelperTableReadOnlyErrorGui<DriveSize>
        , IHelperTableGui<DriveType>
        , IHelperTableReadOnlyErrorGui<DriveType>
        , IHelperTableGui<SwitchOff>
        , IHelperTableReadOnlyErrorGui<SwitchOff>
        , IHelperTableGui<ShutOff>
        , IHelperTableReadOnlyErrorGui<ShutOff>
        , IHelperTableGui<ConstructionType>
        , IHelperTableReadOnlyErrorGui<ConstructionType>
        , ISaveColumnsGui
        , ICanClose
        , IClearShownChanges
    {
        #region Properties

        public event EventHandler<List<(string mappingName, double columnWidth)>> SetColumnWidths;
        public event EventHandler UpdateColumnData;


        private IStartUp _startUp;
        private IToolModelUseCase _useCase;
        private ISaveColumnsUseCase _saveColumnsUseCase;
        private Dispatcher _guiDispatcher;
        private ILocalizationWrapper _localization;
        private IToolDisplayFormatter _toolDisplayFormatter;

        public RelayCommand LoadedCommand { get; private set; }

        private bool CanExecuteLoaded(object arg)
        {
            return true;
        }

        private void ExecuteLoaded(object arg)
        {
            LoadItems();
            _saveColumnsUseCase.LoadColumnWidths(GridName, ColumnData.Select(x => x.mappingName).ToList());
        }

        private List<(string mappingName, double columnWidth)> _columnData;
        public List<(string mappingName, double columnWidth)> ColumnData
        {
            get
            {
                UpdateColumnData.Invoke(this, System.EventArgs.Empty);
                return _columnData;
            }
            set
            {
                _columnData = value;
            }
        }

        private ToolModelModel _toolModelBeforeChange;
        public ToolModelModel SelectedToolModelAfterDiffDialog { get; set; }
        
        private ToolModelModel _selectedToolModel;
        public ToolModelModel SelectedToolModel
        {
            get => _selectedToolModel;
            set
            {
                Set(ref _selectedToolModel, value);
                _toolModelBeforeChange = _selectedToolModel?.CopyDeep();

                if (value != null)
                {
                    SetToolModelClasses(value);
                }

                RaisePropertyChanged(nameof(SelectedModelType));

                if (value != null)
                {
                    _startUp.RaiseShowLoadingControl(true);
                    _useCase.LoadPictureForToolModel(value.Id);
                    _useCase.LoadCmCmk();
                }

                ClearShownChanges?.Invoke(this, null);
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public AbstractToolTypeModel SelectedModelType
        {
            get => SelectedToolModel?.ModelType;
            set
            {
                if (SelectedToolModel != null)
                {
                    SelectedToolModel.ModelType = value;
                    SetToolModelClasses(SelectedToolModel);
                    if (SelectedToolModel.Entity.ModelType.GetType() == _toolModelBeforeChange.Entity.ModelType.GetType())
                    {
                        SelectedToolModel.Entity.Class = _toolModelBeforeChange.Entity.Class;
                    }
                    else
                    {
                        SelectedToolModel.Class = AllToolModelClassesCollectionView.SourceCollection.ToList<ToolModelClassModel>().FirstOrDefault();
                    }
                    
                }
                
                RaisePropertyChanged(nameof(SelectedModelType));
            }
        }

        private void SetToolModelClasses(ToolModelModel model)
        {
            var allToolModelClasses = new ObservableCollection<ToolModelClassModel>();

            switch (model.Entity.ModelType)
            {
                case ClickWrench clickWrenchModel:
                    allToolModelClasses.Add(ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.WrenchScale, _localization));
                    allToolModelClasses.Add(ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.WrenchFixSet, _localization));
                    allToolModelClasses.Add(ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.WrenchWithoutScale, _localization));
                    allToolModelClasses.Add(ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.DriverScale, _localization));
                    allToolModelClasses.Add(ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.DriverFixSet, _localization));
                    allToolModelClasses.Add(ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.DriverWithoutScale, _localization));
                    allToolModelClasses.Add(ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.WrenchWithBendingSteelLever, _localization));
                    break;

                case MDWrench mdWrenchModel:
                case ProductionWrench productionWrenchModel:
                    allToolModelClasses.Add(ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.WrenchBendingSteelLever, _localization));
                    allToolModelClasses.Add(ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.WrenchWithDialIndicator, _localization));
                    allToolModelClasses.Add(ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.WrenchElectronic, _localization));
                    allToolModelClasses.Add(ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.DriverWithDialIndicator, _localization));
                    allToolModelClasses.Add(ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.DriverElectronic, _localization));
                    break;
            }

            if (allToolModelClasses.Count > 0)
            {
                var old = model.Class;
                AllToolModelClassesCollectionView = new ListCollectionView(allToolModelClasses);
                RaisePropertyChanged(nameof(AllToolModelClassesCollectionView));
                model.Class = old;
            }
        }

        public ObservableCollection<ToolModelModel> AllToolModelModels { get; private set; }

        // HelperTableCollectionViews
        private ObservableCollection<ManufacturerModel> _manufacturers;
        public ListCollectionView ManufacturerCollectionView { get; private set; }

        private ObservableCollection<HelperTableItemModel<ToolType, string>> _toolTypes;
        public ListCollectionView ToolTypeCollectionView { get; private set; }

        private ObservableCollection<HelperTableItemModel<DriveSize, string>> _driveSizes;
        public ListCollectionView DriveSizeCollectionView { get; private set; }

        private ObservableCollection<HelperTableItemModel<DriveType, string>> _driveTypes;
        public ListCollectionView DriveTypeCollectionView { get; private set; }

        private ObservableCollection<HelperTableItemModel<SwitchOff, string>> _switchOffs;
        public ListCollectionView SwitchOffCollectionView { get; private set; }

        private ObservableCollection<HelperTableItemModel<ShutOff, string>> _shutOffs;
        public ListCollectionView ShutOffCollectionView { get; private set; }

        private ObservableCollection<HelperTableItemModel<ConstructionType, string>> _constructionTypes;
        public ListCollectionView ConstructionTypeCollectionView { get; private set; }

        // Enum Collections
        private ObservableCollection<AbstractToolTypeModel> _allToolModelTypes;
        public ListCollectionView AllToolModelTypesCollectionView { get; private set; }
        public ListCollectionView AllToolModelClassesCollectionView { get; private set; }

        public ObservableCollection<ToolModelModel> SelectedToolModels { get; private set; }
        public ListCollectionView SelectedToolModelsListCollectionView { get; private set; }

        private const string GridName = "ToolModel";

        private bool _listViewWasShown;
        private bool _isListViewVisible;
        public bool IsListViewVisible
        {
            get => _isListViewVisible;
            set
            {
                _isListViewVisible = value;
                if (value)
                {
                    _listViewWasShown = true;
                }
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<CmCmkModel> _cmCmkTuples;

        public ObservableCollection<CmCmkModel> CmCmkTuples
        {
            get => _cmCmkTuples;
            set => Set(ref _cmCmkTuples, value);
        }
        #endregion


        #region Commands
        public RelayCommand RemoveToolModelsCommand { get; private set; }
        private bool RemoveToolModelsCanExecute(object arg)
        {
            return SelectedToolModels.Count > 0;
        }
        private void RemoveToolModelsExecute(object arg)
        {
            Action<MessageBoxResult> resultAction = (r) =>
            {
                if (r != MessageBoxResult.Yes)
                {
                    _startUp.RaiseShowLoadingControl(false);
                    return;
                }

                SelectedToolModel?.UpdateWith(_toolModelBeforeChange?.Entity);

                _useCase.RemoveToolModels(SelectedToolModels.Select(x => x.Entity).ToList(), this);
            };

            var args = new MessageBoxEventArgs(resultAction,
                _localization.Strings.GetString("Do you really want to remove this item?"),
                _localization.Strings.GetParticularString("Window Title", "Warning"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            _startUp.RaiseShowLoadingControl(true);
            MessageBoxRequest?.Invoke(this, args);
        }

        public RelayCommand AddToolModelCommand { get; private set; }
        private bool CanExecuteAddToolModel(object arg) { return true; }
        private void ExecuteAddToolModel(object obj)
        {
            var previousSelectedToolModel = SelectedToolModel;
            AssistentView assistent;
            
            _startUp.RaiseShowLoadingControl(true);

            if (SelectedToolModel == null)
            {
                assistent = _startUp.OpenAddToolModelAssistent();
            }
            else
            {
                assistent = _startUp.OpenAddToolModelAssistent(SelectedToolModel?.Entity); 
            }

            assistent.EndOfAssistent += (s, e) =>
            {
                var toolModel = (Core.Entities.ToolModel)(assistent.DataContext as AssistentViewModel).FillResultObject(new Core.Entities.ToolModel());
                _useCase.AddToolModel(toolModel);
            };
            assistent.Closed += (s, e) =>
            {
                SelectedToolModel = previousSelectedToolModel;

                if (assistent.DialogResult != true)
                {
                    _startUp.RaiseShowLoadingControl(false); 
                }
            };

            ShowDialogRequest?.Invoke(this, assistent);
        }

        public RelayCommand SaveToolModelCommand { get; private set; }
        private bool SaveToolModelCanExecute(object arg)
        {
            if(_toolModelBeforeChange == null || SelectedToolModel == null)
            {
                return false;
            }

            return !_toolModelBeforeChange.EqualsByContent(SelectedToolModel);
        }
        private void SaveToolModelExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);
            _useCase.RequestToolModelUpdate(_toolModelBeforeChange?.Entity, SelectedToolModel?.Entity, this);
        }

        public RelayCommand RefreshSelectedToolModelBindingsCommand { get; private set; }
        private bool RefreshSelectedToolModelBindingsCanExecute(object arg) { return SelectedToolModel != null; }
        private void RefreshSelectedToolModelBindingsExecute(object obj)
        {
            RaisePropertyChanged(nameof(SelectedToolModel));
        }
        #endregion


        #region Events
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler<Window> ShowDialogRequest;
        public event EventHandler ClearShownChanges;
        public event EventHandler<ToolModelModel> SelectionRequest;
        public event EventHandler<ReferenceList> ReferencesDialogRequest;

        /// <summary>
        /// Tuple: Item1 -> List of changes for the VerifyChangesView      Item2 -> Action to set the result of the VerifyChangesView       Item3 -> Action to set the history comment
        /// </summary>
        public event EventHandler<VerifyChangesEventArgs> RequestVerifyChangesView;
        #endregion


        #region Interface Implementations

        // IToolModelGui
        public void ShowToolModels(List<Core.Entities.ToolModel> toolModels)
        {
            _guiDispatcher.Invoke(() => 
            {
                RefillTreeItems(toolModels);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowLoadingErrorMessage()
        {
            var args = new MessageBoxEventArgs((r) => { },
                                               _localization?.Strings.GetParticularString("Tool Model", "An error has occured while loading the tool models!") ?? "LoadingError",
                                               "",
                                               MessageBoxButton.OK,
                                               MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void SetPictureForToolModel(long toolModelId, Picture picture)
        {
            var model = AllToolModelModels.FirstOrDefault(x => x.Id == toolModelId);

            if (model == null)
            {
                return;
            }
            _guiDispatcher.Invoke(() =>
            {
                var pictureModel = PictureModel.GetModelFor(picture);
                model.Picture = pictureModel;

                if(SelectedToolModel == model)
                {
                    _toolModelBeforeChange.Picture = pictureModel;
                }
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowRemoveToolModelsErrorMessage()
        {
            var args = new MessageBoxEventArgs((r) => { },
                                               _localization.Strings.GetParticularString("Tool Model", "An error has occured while removing the tool models"),
                                               "",
                                               MessageBoxButton.OK,
                                               MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void RemoveToolModels(List<Core.Entities.ToolModel> toolModels)
        {
            _guiDispatcher.Invoke(() =>
            {
                foreach (var tm in toolModels)
                {
                    ToolModelModel removedTool = null;
                    foreach (var tmm in AllToolModelModels)
                    {
                        if (tmm.Entity.EqualsById(tm))
                        {
                            removedTool = tmm;
                            break;
                        }
                    }

                    AllToolModelModels.Remove(removedTool);
                    SelectedToolModels.Remove(removedTool);
                }

                SelectedToolModel = null;
                ClearShownChanges?.Invoke(this, null);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowCmCmk(double cm, double cmk)
        {
            _guiDispatcher.Invoke(
                () =>
                {
                    CmCmkTuples.Clear();
                    CmCmkTuples.Add(new CmCmkModel {Cm = cm, Cmk = cmk});
                    AllToolModelModels.ForEach(
                        toolModelModel =>
                        {
                            toolModelModel.CmLimit = cm;
                            toolModelModel.CmkLimit = cmk;
                            if (_toolModelBeforeChange == null)
                            {
                                return;
                            }
                            _toolModelBeforeChange.CmLimit = cm;
                            _toolModelBeforeChange.CmkLimit = cmk;
                        });
                    _startUp.RaiseShowLoadingControl(false);
                });
        }

        public void ShowCmCmkError()
        {
            _guiDispatcher.Invoke(() =>
            {
                var args = new MessageBoxEventArgs((r) => { },
                    caption: _localization.Strings.GetString("An unknown error occured.\nPlease contact our Support Team"),
                    text: _localization.Strings.GetString("Unknown Error!"),
                    messageBoxButton: MessageBoxButton.OK,
                    messageBoxImage: MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, args);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void AddToolModel(ToolModel toolModel)
        {
            _guiDispatcher.Invoke(() =>
            {
                var model = ToolModelModel.GetModelFor(toolModel, _localization);
                AllToolModelModels.Add(model);
                SelectedToolModel = model;
                SelectedToolModels.Clear();
                SelectedToolModels?.Add(model);
                SelectionRequest?.Invoke(this, model);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void UpdateToolModel(ToolModel toolModel)
        {
            _guiDispatcher.Invoke(() =>
            {
                var oldToolModel = AllToolModelModels.FirstOrDefault(x => x.Entity.EqualsById(toolModel));
                var newToolModel = ToolModelModel.GetModelFor(toolModel, _localization);
                oldToolModel.UpdateWith(newToolModel.Entity);

                if (oldToolModel.EqualsById(_toolModelBeforeChange))
                {
                    _toolModelBeforeChange = oldToolModel?.CopyDeep(); 
                }

                ClearShownChanges?.Invoke(this, null);
                CommandManager.InvalidateRequerySuggested();
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowEntryAlreadyExistsMessage(ToolModel toolModel)
        {
            var args = new MessageBoxEventArgs((r) => { },
                _localization.Strings.GetParticularString("Tool Model", "An equal tool model is existing already"),
                "",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public bool ShowDiffDialog(ToolModelDiff diff)
        {
            var changes = GetChangesFromDiff(diff).ToList();

            if(changes.Count == 0)
            {
                return false;
            }

            var args = new VerifyChangesEventArgs(changes);
            RequestVerifyChangesView?.Invoke(this, args);
            diff.Comment = new HistoryComment(args.Comment);
            
            _guiDispatcher.Invoke(() =>
            {
                if(args.Result == MessageBoxResult.No)
                {
                    UndoChanges();
                    ClearShownChanges?.Invoke(this, null);
                    _startUp.RaiseShowLoadingControl(false);
                }

                if (SelectedToolModelAfterDiffDialog != null)
                {
                    if (args.Result != MessageBoxResult.Cancel)
                    {
                        SelectedToolModel = SelectedToolModelAfterDiffDialog;
                        SelectedToolModelAfterDiffDialog = null; 
                    } 
                    else
                    {
                        SelectionRequest?.Invoke(this, SelectedToolModel);
                        _startUp.RaiseShowLoadingControl(false);
                    }
                }
                else
                {
                    _startUp.RaiseShowLoadingControl(false);
                }
            });

            return args.Result == MessageBoxResult.Yes;
        }

        // IManufanufacturerGui
        public void ShowManufacturer(List<Manufacturer> manufacturer)
        {
            _guiDispatcher.Invoke(() =>
            {
                List<ManufacturerModel> newManufacturers = new List<ManufacturerModel>();

                foreach (var m in manufacturer)
                {
                    newManufacturers.Add(ManufacturerModel.GetModelFor(m));
                }

                foreach (var m in newManufacturers.Except(_manufacturers))
                {
                    _manufacturers.Add(m);
                }
                
                var manufacturersToRemove = _manufacturers.Except(newManufacturers, new ManufacturerModelComparer()).ToList();
                foreach (var m in manufacturersToRemove)
                {
                    _manufacturers.Remove(m);
                }
            });
        }

        public void AddManufacturer(Manufacturer manufacturer)
        {
            _guiDispatcher.Invoke(() => {  _manufacturers.Add(ManufacturerModel.GetModelFor(manufacturer)); });
        }

        public void SaveManufacturer(Manufacturer manufacturer)
        {
            _guiDispatcher.Invoke(() =>
            {
                var selectedModel = SelectedToolModel?.Manufacturer;
                var oldModel = _manufacturers.FirstOrDefault(x => x.Entity.EqualsById(manufacturer));
                var newModel = ManufacturerModel.GetModelFor(manufacturer);

                if (oldModel != null)
                {
                    var index = _manufacturers.IndexOf(oldModel);
                    _manufacturers.Remove(oldModel);
                    _manufacturers.Insert(index, newModel);
                }

                if(newModel.EqualsById(selectedModel))
                {
                    SelectedToolModel.Manufacturer = newModel;
                }
            });
        }

        public void RemoveManufacturer(Manufacturer manufacturer)
        {
            _guiDispatcher.Invoke(() =>
            {
                var models = _manufacturers.Where(x => x.Entity.EqualsById(manufacturer)).ToList();

                foreach (var m in models)
                {
                    _manufacturers.Remove(m);
                }
            });
        }

        public void ShowReferencedToolModels(List<ToolModelReferenceLink> toolModels)
        {
            //Intentionally empty
        }

        public void ShowErrorMessage()
        {
            var args = new MessageBoxEventArgs((r) => { },
                _localization.Strings.GetParticularString("Tool Model", "An error occured"),
                "",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void ShowRemoveToolModelPreventingReferences(List<ToolReferenceLink> references)
        {
            _guiDispatcher.Invoke(() =>
            {
                ReferencesDialogRequest?.Invoke(this, new ReferenceList
                {
                    Header = _localization.Strings.GetParticularString("ToolModelViewModel", "Tool"),
                    References = references.Select(x => _toolDisplayFormatter.Format(x)).ToList()
                });
            });
        }

        public void ShowComment(Manufacturer manufacturer, string comment)
        {
            // Intentionally empty
        }

        public void ShowCommentError()
        {
            // Intentionally empty
        }

        public void ShowToolReferenceLinks(List<ToolReferenceLink> tools)
        {
            // Intentionally empty
        }

        public void ShowReferencedLocationToolAssignments(List<LocationToolAssignment> assignments)
        {
            // Intentionally empty
        }

        public void ShowReferencesError()
        {
            // Intentionally empty
        }

        public void ShowRemoveManufacturerPreventingReferences(List<ToolModelReferenceLink> references)
        {
            // Intentionally empty
        }


        // IHelperTableGui<ToolType, string>
        public void ShowItems(List<ToolType> items)
        {
            _guiDispatcher.Invoke(() =>
            {
                List<HelperTableItemModel<ToolType, string>> newItemModels = new List<HelperTableItemModel<ToolType, string>>();

                foreach (var i in items)
                {
                    newItemModels.Add(HelperTableItemModel.GetModelForToolType(i));
                }

                foreach (var i in newItemModels.Except(_toolTypes))
                {
                    _toolTypes.Add(i);
                }

                var itemsToRemove = _toolTypes.Except(newItemModels, new HelperTableItemModelComparer<ToolType, string>()).ToList();
                foreach (var i in itemsToRemove)
                {
                    _toolTypes.Remove(i);
                }
            });
        }

        public void Save(ToolType savedItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var selectedToolType = SelectedToolModel?.ToolType;
                var model = HelperTableItemModel.GetModelForToolType(savedItem);

                UpdateItemFromHelperTable(_toolTypes, savedItem.ListId.ToLong(), model);

                if (selectedToolType?.EqualsById(model) ?? false)
                {
                    SelectedToolModel.ToolType = model;
                }
            });
        }

        public void Add(ToolType newItem) { AddToHelperTable(_toolTypes, HelperTableItemModel.GetModelForToolType(newItem)); }
        public void Remove(ToolType removeItem) { RemoveFromHelpterTable(_toolTypes, removeItem.ListId.ToLong()); }

        // IHelperTableGui<DriveSize, string>
        public void ShowItems(List<DriveSize> items)
        {
            _guiDispatcher.Invoke(() =>
            {
                List<HelperTableItemModel<DriveSize, string>> newItemModels = new List<HelperTableItemModel<DriveSize, string>>();

                foreach (var i in items)
                {
                    newItemModels.Add(HelperTableItemModel.GetModelForDriveSize(i));
                }

                foreach (var i in newItemModels.Except(_driveSizes))
                {
                    _driveSizes.Add(i);
                }

                var itemsToRemove = _driveSizes.Except(newItemModels, new HelperTableItemModelComparer<DriveSize, string>()).ToList();
                foreach (var i in itemsToRemove)
                {
                    _driveSizes.Remove(i);
                }
            });
        }

        public void Save(DriveSize savedItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var selectedDriveSize = SelectedToolModel?.DriveSize;
                var model = HelperTableItemModel.GetModelForDriveSize(savedItem);

                UpdateItemFromHelperTable(_driveSizes, savedItem.ListId.ToLong(), model);

                if (selectedDriveSize?.EqualsById(model) ?? false)
                {
                    SelectedToolModel.DriveSize = model;
                }
            });
        }

        public void Add(DriveSize newItem) { AddToHelperTable(_driveSizes, HelperTableItemModel.GetModelForDriveSize(newItem)); }
        public void Remove(DriveSize removeItem) { RemoveFromHelpterTable(_driveSizes, removeItem.ListId.ToLong()); }

        // IHelperTableGui<DriveType, string>
        public void ShowItems(List<DriveType> items)
        {
            _guiDispatcher.Invoke(() =>
            {
                List<HelperTableItemModel<DriveType, string>> newItemModels = new List<HelperTableItemModel<DriveType, string>>();

                foreach (var i in items)
                {
                    newItemModels.Add(HelperTableItemModel.GetModelForDriveType(i));
                }

                foreach (var i in newItemModels.Except(_driveTypes))
                {
                    _driveTypes.Add(i);
                }

                var itemsToRemove = _driveTypes.Except(newItemModels, new HelperTableItemModelComparer<DriveType, string>()).ToList();
                foreach (var i in itemsToRemove)
                {
                    _driveTypes.Remove(i);
                }
            });
        }

        public void Save(DriveType savedItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var selectedDriveType = SelectedToolModel?.DriveType;
                var model = HelperTableItemModel.GetModelForDriveType(savedItem);

                UpdateItemFromHelperTable(_driveTypes, savedItem.ListId.ToLong(), model);

                if (selectedDriveType?.EqualsById(model) ?? false)
                {
                    SelectedToolModel.DriveType = model;
                }
            });
        }

        public void Add(DriveType newItem) { AddToHelperTable(_driveTypes, HelperTableItemModel.GetModelForDriveType(newItem)); }
        public void Remove(DriveType removeItem) { RemoveFromHelpterTable(_driveTypes, removeItem.ListId.ToLong()); }


        // IHelperTableGui<SwitchOff, string>
        public void ShowItems(List<SwitchOff> items)
        {
            _guiDispatcher.Invoke(() =>
            {
                List<HelperTableItemModel<SwitchOff, string>> newItemModels = new List<HelperTableItemModel<SwitchOff, string>>();

                foreach (var i in items)
                {
                    newItemModels.Add(HelperTableItemModel.GetModelForSwitchOff(i));
                }

                foreach (var i in newItemModels.Except(_switchOffs))
                {
                    _switchOffs.Add(i);
                }

                var itemsToRemove = _switchOffs.Except(newItemModels, new HelperTableItemModelComparer<SwitchOff, string>()).ToList();
                foreach (var i in itemsToRemove)
                {
                    _switchOffs.Remove(i);
                }
            });
        }

        public void Save(SwitchOff savedItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var selectedSwitchOff = SelectedToolModel?.SwitchOff;
                var model = HelperTableItemModel.GetModelForSwitchOff(savedItem);

                UpdateItemFromHelperTable(_switchOffs, savedItem.ListId.ToLong(), model);

                if (selectedSwitchOff?.EqualsById(model) ?? false)
                {
                    SelectedToolModel.SwitchOff = model;
                }
            });
        }

        public void Add(SwitchOff newItem) { AddToHelperTable(_switchOffs, HelperTableItemModel.GetModelForSwitchOff(newItem)); }
        public void Remove(SwitchOff removeItem) { RemoveFromHelpterTable(_switchOffs, removeItem.ListId.ToLong()); }

        // IHelperTableGui<ShutOff, string>
        public void ShowItems(List<ShutOff> items)
        {
            _guiDispatcher.Invoke(() =>
            {
                List<HelperTableItemModel<ShutOff, string>> newItemModels = new List<HelperTableItemModel<ShutOff, string>>();

                foreach (var i in items)
                {
                    newItemModels.Add(HelperTableItemModel.GetModelForShutOff(i));
                }

                foreach (var i in newItemModels.Except(_shutOffs))
                {
                    _shutOffs.Add(i);
                }

                var itemsToRemove = _shutOffs.Except(newItemModels, new HelperTableItemModelComparer<ShutOff, string>()).ToList();
                foreach (var i in itemsToRemove)
                {
                    _shutOffs.Remove(i);
                }
            });
        }

        public void Save(ShutOff savedItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var selectedShutOff = SelectedToolModel?.ShutOff;
                var model = HelperTableItemModel.GetModelForShutOff(savedItem);

                UpdateItemFromHelperTable(_shutOffs, savedItem.ListId.ToLong(), model);

                if (selectedShutOff?.EqualsById(model) ?? false)
                {
                    SelectedToolModel.ShutOff = model;
                }
            });
        }

        public void Add(ShutOff newItem) { AddToHelperTable(_shutOffs, HelperTableItemModel.GetModelForShutOff(newItem)); }
        public void Remove(ShutOff removeItem) { RemoveFromHelpterTable(_shutOffs, removeItem.ListId.ToLong()); }


        // HelperTableGui<ConstructionType, string>
        public void ShowItems(List<ConstructionType> items)
        {
            _guiDispatcher.Invoke(() =>
            {
                List<HelperTableItemModel<ConstructionType, string>> newItemModels = new List<HelperTableItemModel<ConstructionType, string>>();

                foreach (var i in items)
                {
                    newItemModels.Add(HelperTableItemModel.GetModelForConstructionType(i));
                }

                foreach (var i in newItemModels.Except(_constructionTypes))
                {
                    _constructionTypes.Add(i);
                }

                var itemsToRemove = _constructionTypes.Except(newItemModels, new HelperTableItemModelComparer<ConstructionType, string>()).ToList();
                foreach (var i in itemsToRemove)
                {
                    _constructionTypes.Remove(i);
                }
            });
        }

        public void Save(ConstructionType savedItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var selectedConstructionType = SelectedToolModel?.ConstructionType;
                var model = HelperTableItemModel.GetModelForConstructionType(savedItem);

                UpdateItemFromHelperTable(_constructionTypes, savedItem.ListId.ToLong(), model);

                if (selectedConstructionType?.EqualsById(model) ?? false)
                {
                    SelectedToolModel.ConstructionType = model;
                }
            });
        }

        public void Add(ConstructionType newItem) { AddToHelperTable(_constructionTypes, HelperTableItemModel.GetModelForConstructionType(newItem)); }
        public void Remove(ConstructionType removeItem) { RemoveFromHelpterTable(_constructionTypes, removeItem.ListId.ToLong()); }

        #endregion


        #region Methods
        private void RefillTreeItems(List<Core.Entities.ToolModel> toolModels)
        {
            AllToolModelModels.Clear();
            foreach (var tm in toolModels)
            {
                var toolModelModel = ToolModelModel.GetModelFor(tm, _localization);
                AllToolModelModels.Add(toolModelModel);
            }
        }

        private void AddToHelperTable<T>(ObservableCollection<HelperTableItemModel<T, string>> list, HelperTableItemModel<T, string> addedItem) 
            where T : HelperTableEntity, IQstEquality<T>, IUpdate<T>, ICopy<T>
        {
            _guiDispatcher.Invoke(() =>
            {
                list.Add(addedItem);
            });
        }

        private void RemoveFromHelpterTable<T>(ObservableCollection<HelperTableItemModel<T, string>> list, long id) 
            where T : HelperTableEntity, IQstEquality<T>, IUpdate<T>, ICopy<T>
        {
            _guiDispatcher.Invoke(() =>
            {
                var models = list.Where(x => x.ListId == id).ToList();

                foreach (var m in models)
                {
                    list.Remove(m);
                }
            });
        }

        private void UpdateItemFromHelperTable<T>(ObservableCollection<HelperTableItemModel<T, string>> list, long id, HelperTableItemModel<T, string> updatedItem)
            where T : HelperTableEntity, IQstEquality<T>, IUpdate<T>, ICopy<T>
        {
            var model = list.FirstOrDefault(x => x.ListId == id);

            if (model != null)
            {
                var index = list.IndexOf(model);
                list.Remove(model);
                list.Insert(index, updatedItem);
            }
        }

        private void FillToolModelEnumLists()
        {
            _allToolModelTypes.Add(new PulseDriverModel(_localization, new PulseDriver()));
            _allToolModelTypes.Add(new PulseDriverShutOffModel(_localization, new PulseDriverShutOff()));
            _allToolModelTypes.Add(new GeneralModel(_localization, new General()));
            _allToolModelTypes.Add(new ECDriverModel(_localization, new ECDriver()));
            _allToolModelTypes.Add(new ClickWrenchModel(_localization, new ClickWrench()));
            _allToolModelTypes.Add(new MDWrenchModel(_localization, new MDWrench()));
            _allToolModelTypes.Add(new ProductionWrenchModel(_localization, new ProductionWrench()));
        }

        private IEnumerable<SingleValueChangeModel> GetChangesFromDiff(ToolModelDiff diff)
        {
            string entity = diff.NewToolModel.Description?.ToDefaultString();

            if(diff.OldToolModel.Description?.ToDefaultString() != diff.NewToolModel.Description?.ToDefaultString())
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Description"),
                    OldValue = diff.OldToolModel?.Description?.ToDefaultString(),
                    NewValue = diff.NewToolModel?.Description?.ToDefaultString()
                };
            }

            if (diff.OldToolModel.ModelType.Name != diff.NewToolModel.ModelType.Name)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Tool model type"),
                    OldValue = AbstractToolTypeModel.MapToolTypeToToolTypeModel(diff.OldToolModel.ModelType, _localization).TranslatedName,
                    NewValue = AbstractToolTypeModel.MapToolTypeToToolTypeModel(diff.NewToolModel.ModelType, _localization).TranslatedName
                };
            }

            if (!diff.OldToolModel.Class.Equals(diff.NewToolModel.Class) || 
                diff.OldToolModel.ModelType.DoesToolTypeHasProperty(nameof(Core.Entities.ToolModel.Class)) != diff.NewToolModel.ModelType.DoesToolTypeHasProperty(nameof(Core.Entities.ToolModel.Class)))
            {
                var oldValue = "";

                if (diff.OldToolModel.ModelType.DoesToolTypeHasProperty(nameof(Core.Entities.ToolModel.Class)) && (long)diff.OldToolModel.Class != -1)
                {
                    oldValue = ToolModelClassTranslation.GetTranlationForToolModelClass(diff.OldToolModel.Class, _localization);
                }

                var newValue = "";

                if (diff.NewToolModel.ModelType.DoesToolTypeHasProperty(nameof(Core.Entities.ToolModel.Class)) && (long)diff.NewToolModel.Class != -1)
                {
                    newValue = ToolModelClassTranslation.GetTranlationForToolModelClass(diff.NewToolModel.Class, _localization);
                }

                if (oldValue != newValue)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Class"),
                        OldValue = oldValue,
                        NewValue = newValue
                    };
                }
            }

            if ((diff.OldToolModel?.Manufacturer == null && diff.NewToolModel?.Manufacturer != null) 
                || (!diff.OldToolModel?.Manufacturer?.EqualsById(diff.NewToolModel?.Manufacturer) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Manufacturer"),
                    OldValue = diff.OldToolModel.Manufacturer?.Name?.ToDefaultString(),
                    NewValue = diff.NewToolModel.Manufacturer?.Name?.ToDefaultString()
                };
            }

            if (!diff.OldToolModel.MinPower.Equals(diff.NewToolModel.MinPower))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Min power"),
                    OldValue = diff.OldToolModel.MinPower.ToString(),
                    NewValue = diff.NewToolModel.MinPower.ToString()
                };
            }

            if (!diff.OldToolModel.MaxPower.Equals(diff.NewToolModel.MaxPower))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Max power"),
                    OldValue = diff.OldToolModel.MaxPower.ToString(),
                    NewValue = diff.NewToolModel.MaxPower.ToString()
                };
            }

            if (!diff.OldToolModel.AirPressure.Equals(diff.NewToolModel.AirPressure))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Air pressure"),
                    OldValue = diff.OldToolModel.AirPressure.ToString(),
                    NewValue = diff.NewToolModel.AirPressure.ToString()
                };
            }

            if ((diff.OldToolModel?.ToolType == null && diff.NewToolModel?.ToolType != null)
                || (!diff.OldToolModel?.ToolType?.EqualsById(diff.NewToolModel?.ToolType) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Tool type"),
                    OldValue = diff.OldToolModel.ToolType?.Value?.ToDefaultString(),
                    NewValue = diff.NewToolModel.ToolType?.Value?.ToDefaultString()
                };
            }

            if (!diff.OldToolModel.Weight.Equals(diff.NewToolModel.Weight))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Weight"),
                    OldValue = diff.OldToolModel.Weight.ToString(),
                    NewValue = diff.NewToolModel.Weight.ToString()
                };
            }

            if (!diff.OldToolModel.BatteryVoltage.Equals(diff.NewToolModel.BatteryVoltage))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Battery voltage"),
                    OldValue = diff.OldToolModel.BatteryVoltage.ToString(),
                    NewValue = diff.NewToolModel.BatteryVoltage.ToString()
                };
            }

            if (!diff.OldToolModel.MaxRotationSpeed.Equals(diff.NewToolModel.MaxRotationSpeed))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Max rotation speed"),
                    OldValue = diff.OldToolModel.MaxRotationSpeed.ToString(),
                    NewValue = diff.NewToolModel.MaxRotationSpeed.ToString()
                };
            }

            if (!diff.OldToolModel.AirConsumption.Equals(diff.NewToolModel.AirConsumption))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Air consumption"),
                    OldValue = diff.OldToolModel.AirConsumption.ToString(),
                    NewValue = diff.NewToolModel.AirConsumption.ToString()
                };
            }

            if ((diff.OldToolModel?.SwitchOff == null && diff.NewToolModel?.SwitchOff != null)
                || (!diff.OldToolModel?.SwitchOff?.EqualsById(diff.NewToolModel?.SwitchOff) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Switch off"),
                    OldValue = diff.OldToolModel.SwitchOff?.Value?.ToDefaultString(),
                    NewValue = diff.NewToolModel.SwitchOff?.Value?.ToDefaultString()
                };
            }

            if ((diff.OldToolModel?.DriveSize == null && diff.NewToolModel?.DriveSize != null)
                || (!diff.OldToolModel?.DriveSize?.EqualsById(diff.NewToolModel?.DriveSize) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Drive size"),
                    OldValue = diff.OldToolModel.DriveSize?.Value?.ToDefaultString(),
                    NewValue = diff.NewToolModel.DriveSize?.Value?.ToDefaultString()
                };
            }

            if ((diff.OldToolModel?.ShutOff == null && diff.NewToolModel?.ShutOff != null)
                || (!diff.OldToolModel?.ShutOff?.EqualsById(diff.NewToolModel?.ShutOff) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Shut off"),
                    OldValue = diff.OldToolModel.ShutOff?.Value?.ToDefaultString(),
                    NewValue = diff.NewToolModel.ShutOff?.Value?.ToDefaultString()
                };
            }

            if ((diff.OldToolModel?.DriveType == null && diff.NewToolModel?.DriveType != null)
                || (!diff.OldToolModel?.DriveType?.EqualsById(diff.NewToolModel?.DriveType) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Drive type"),
                    OldValue = diff.OldToolModel.DriveType?.Value?.ToDefaultString(),
                    NewValue = diff.NewToolModel.DriveType?.Value?.ToDefaultString()
                };
            }

            if ((diff.OldToolModel?.ConstructionType == null && diff.NewToolModel?.ConstructionType != null)
                || (!diff.OldToolModel?.ConstructionType?.EqualsById(diff.NewToolModel?.ConstructionType) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolModelAttribute", "Construction type"),
                    OldValue = diff.OldToolModel.ConstructionType?.Value?.ToDefaultString(),
                    NewValue = diff.NewToolModel.ConstructionType?.Value?.ToDefaultString()
                };
            }
        }

        private MessageBoxResult AskForSaving()
        {
            MessageBoxResult messageBoxResult = MessageBoxResult.None;
            
            var args = new MessageBoxEventArgs(r => messageBoxResult = r,
                _localization.Strings.GetString("Do you want to save your changes? This change will affect the references."),
                "",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);

            // Request MessageBox
            MessageBoxRequest?.Invoke(this, args);

            return messageBoxResult;
        }

        public void UndoChanges()
        {
            _guiDispatcher.Invoke(() =>
            {
                SelectedToolModel = _toolModelBeforeChange?.CopyDeep();
                RaisePropertyChanged(nameof(SelectedModelType));
            });
        }
        #endregion


        public void SetDispatcher(Dispatcher dispatcher)
        {
            RemoveToolModelsCommand = new RelayCommand(RemoveToolModelsExecute, RemoveToolModelsCanExecute);

            _guiDispatcher = dispatcher;
        }

        public ToolModelViewModel(IStartUp startUp, IToolModelUseCase toolModelUseCase, ISaveColumnsUseCase saveColumnsUseCase, ILocalizationWrapper localization, IToolDisplayFormatter toolDisplayFormatter)
        {
            _startUp = startUp;
            _useCase = toolModelUseCase;
            _saveColumnsUseCase = saveColumnsUseCase;
            _toolDisplayFormatter = toolDisplayFormatter;
            ConstructionHelper(localization);
        }

        private void ConstructionHelper(ILocalizationWrapper localization)
        {
            _localization = localization;

            AllToolModelModels = new ObservableCollection<ToolModelModel>();

            _manufacturers = new ObservableCollection<ManufacturerModel>();
            ManufacturerCollectionView = new ListCollectionView(_manufacturers);

            _toolTypes = new ObservableCollection<HelperTableItemModel<ToolType, string>>();
            ToolTypeCollectionView = new ListCollectionView(_toolTypes);

            _driveSizes = new ObservableCollection<HelperTableItemModel<DriveSize, string>>();
            DriveSizeCollectionView = new ListCollectionView(_driveSizes);

            _driveTypes = new ObservableCollection<HelperTableItemModel<DriveType, string>>();
            DriveTypeCollectionView = new ListCollectionView(_driveTypes);

            _switchOffs = new ObservableCollection<HelperTableItemModel<SwitchOff, string>>();
            SwitchOffCollectionView = new ListCollectionView(_switchOffs);

            _shutOffs = new ObservableCollection<HelperTableItemModel<ShutOff, string>>();
            ShutOffCollectionView = new ListCollectionView(_shutOffs);

            _constructionTypes = new ObservableCollection<HelperTableItemModel<ConstructionType, string>>();
            ConstructionTypeCollectionView = new ListCollectionView(_constructionTypes);

            _allToolModelTypes = new ObservableCollection<AbstractToolTypeModel>();
            AllToolModelTypesCollectionView = new ListCollectionView(_allToolModelTypes);
           

            SelectedToolModels = new ObservableCollection<ToolModelModel>();
            SelectedToolModelsListCollectionView = new ListCollectionView(SelectedToolModels);
            _cmCmkTuples = new ObservableCollection<CmCmkModel>();

            FillToolModelEnumLists();

            LoadedCommand = new RelayCommand(ExecuteLoaded, CanExecuteLoaded);
            AddToolModelCommand = new RelayCommand(ExecuteAddToolModel, CanExecuteAddToolModel);
            SaveToolModelCommand = new RelayCommand(SaveToolModelExecute, SaveToolModelCanExecute);
            RefreshSelectedToolModelBindingsCommand = new RelayCommand(RefreshSelectedToolModelBindingsExecute, RefreshSelectedToolModelBindingsCanExecute);
        }

        
        private void LoadItems()
        {
            _startUp.RaiseShowLoadingControl(true);
            _useCase?.ManufacturerUseCase?.LoadManufacturer();
            _useCase?.ToolTypeUseCase?.LoadItems(this);
            _useCase?.DriveSizeUseCase?.LoadItems(this);
            _useCase?.DriveTypeUseCase?.LoadItems(this);
            _useCase?.SwitchOffUseCase?.LoadItems(this);
            _useCase?.ShutOffUseCase?.LoadItems(this);
            _useCase?.ConstructionTypeUseCase?.LoadItems(this);
            _useCase?.ShowToolModels();
        }

        public void UpdateColumnWidths(string gridName, List<(string, double)> columns)
        {
            ShowColumnWidths(gridName, columns);
        }

        public void ShowSaveColumnError(string gridName)
        {
            if (gridName != GridName)
            {
                return;
            }
            _guiDispatcher.Invoke(() =>
            {
                var args = new MessageBoxEventArgs((r) => { },
                    caption: _localization.Strings.GetString("An unknown error occured.\nPlease contact our Support Team"),
                    text: _localization.Strings.GetString("Unknown Error!"),
                    messageBoxButton: MessageBoxButton.OK,
                    messageBoxImage: MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, args);
            });
        }

        public void ShowColumnWidths(string gridName, List<(string, double)> columns)
        {
            if (gridName != GridName)
            {
                return;
            }

            _guiDispatcher.Invoke(() =>
            {
                SetColumnWidths.Invoke(this, columns);
            });
        }

        public void ShowLoadColumnWidthsError(string gridName)
        {
            if (gridName != GridName)
            {
                return;
            }
            _guiDispatcher.Invoke(() =>
            {
                var args = new MessageBoxEventArgs((r) => { },
                    caption: _localization.Strings.GetString("An unknown error occured.\nPlease contact our Support Team"),
                    text: _localization.Strings.GetString("Unknown Error!"),
                    messageBoxButton: MessageBoxButton.OK,
                    messageBoxImage: MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, args);
            });
        }

        public bool CanClose()
        {
            if (_listViewWasShown)
            {
                _saveColumnsUseCase.SaveColumnWidths(GridName, ColumnData);
            }

            if (SaveToolModelCanExecute(null))
            {
                switch(AskForSaving())
                {
                    case MessageBoxResult.Yes:
                        SaveToolModelCommand.Invoke(null);
                        return true;
                    case MessageBoxResult.Cancel:
                        return false;
                }
            }

            return true;
        }
    }
}
