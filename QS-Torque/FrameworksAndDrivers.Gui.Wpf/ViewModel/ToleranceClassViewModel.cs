using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using Syncfusion.Data.Extensions;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Core.Entities.ReferenceLink;
using FrameworksAndDrivers.Gui.Wpf.View.Dialogs;
using FrameworksAndDrivers.Gui.Wpf.ViewModel.GridColumns;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;
using SelectionChangedEventArgs = System.Windows.Controls.SelectionChangedEventArgs;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class ToleranceClassViewModel : BindableBase, IToleranceClassGui, ICanClose, IGetUpdatedByLanguageChanges, ISaveColumnsGui, IClearShownChanges
    {

        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler<List<ReferenceList>> ReferencesDialogRequest;
        public event EventHandler ClearShownChanges;
        private void RaiseClearShowChanges() { ClearShownChanges?.Invoke(this, null); }

        public RelayCommand AddToleranceClassCommand { get; private set; }
        public RelayCommand SaveToleranceClassCommand { get; private set; }
        public RelayCommand RemoveToleranceClassCommand { get; private set; }
        public RelayCommand SelectionChanged { get; private set; }
        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand LoadReferencedLocationsCommand { get; private set; }
        public RelayCommand LoadReferencedLocationToolAssignmentsCommand { get; private set; }

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

        private bool _areLocationReferencesShown;
        public bool AreLocationReferencesShown
        {
            get => _areLocationReferencesShown;
            set
            {
                Set(ref _areLocationReferencesShown, value);

                if (!value)
                {
                    ReferencedLocations.Clear();
                }
            }
        }

        private bool _areLocationToolAssignmentReferencesShown;
        public bool AreLocationToolAssignmentReferencesShown
        {
            get => _areLocationToolAssignmentReferencesShown;
            set
            {
                Set(ref _areLocationToolAssignmentReferencesShown, value);

                if (!value)
                {
                    ReferencedLocationToolAssignments.Clear();
                }
            }
        }

        private const string GridName = "ToleranceClass";
        private readonly IStartUp _startUp;
        private readonly IToleranceClassUseCase _toleranceClassUseCase;
        private readonly ISaveColumnsUseCase _saveColumnsUseCase;
        private ILocalizationWrapper _localization;
        private Dispatcher _guiDispatcher;

        private ObservableCollection<ToleranceClassModel> _toleranceClasses;
        public ListCollectionView ToleranceClasses { get; private set; }

        private ObservableCollection<ToleranceClassModel> _selectedToleranceClassModels;
        public ListCollectionView SelectedToleranceClassListCollectionView { get; private set; }

        public ObservableCollection<LocationReferenceLink> ReferencedLocations { get; set; }
        public ObservableCollection<LocationToolAssignmentModel> ReferencedLocationToolAssignments { get; set; }

        private Columns _listViewColumns;

        public Columns ListViewColumns
        {
            get => _listViewColumns;
            set => Set(ref _listViewColumns, value);
        }

        private ToleranceClassModel _selectedToleranceClassModel;
        private ToleranceClassModel _originalToleranceClassModel;
        public ToleranceClassModel SelectedToleranceClassModel
        {
            get => _selectedToleranceClassModel;
            set
            {
                var changedToleranceClassModel = _selectedToleranceClassModel?.CopyDeep();
                if (!AskForSaving(changedToleranceClassModel))
                {
                    return;
                }
                Set(ref _selectedToleranceClassModel, value);
                CommandManager.InvalidateRequerySuggested();
                _originalToleranceClassModel = _selectedToleranceClassModel?.CopyDeep();
                AreLocationReferencesShown = false;
                AreLocationToolAssignmentReferencesShown = false;
                RaiseClearShowChanges();
            }
        }

        private bool AskForSaving(ToleranceClassModel toleranceClassWithChanges)
        {
            if (_originalToleranceClassModel?.EqualsByContent(toleranceClassWithChanges) ?? true)
            {
                return true;
            }

            var shouldSetSelectedToleranceClass = true;

            Action<MessageBoxResult> action = (r) =>
            {
                switch (r)
                {
                    case MessageBoxResult.Yes:
                        if (IsEmptyOrDuplicae(toleranceClassWithChanges))
                        {
                            shouldSetSelectedToleranceClass = false;
                            return;
                        }

                        _toleranceClassUseCase.SaveToleranceClass(
                            toleranceClassWithChanges.Entity, _originalToleranceClassModel.Entity);
                        break;
                    case MessageBoxResult.No:
                        _selectedToleranceClassModel.UpdateWith(_originalToleranceClassModel.Entity);
                        
                        var index = _toleranceClasses.ToList().FindIndex(x => x.EqualsById(_originalToleranceClassModel));
                        if (index != -1)
                        {
                            _toleranceClasses[index].UpdateWith(_originalToleranceClassModel.Entity);
                        }
                        break;
                    case MessageBoxResult.Cancel:
                        var indexOfFindIndex = _toleranceClasses.ToList().FindIndex(x => x.EqualsById(_originalToleranceClassModel));
                        if (indexOfFindIndex != -1)
                        {
                            _selectedToleranceClassModel = _toleranceClasses[indexOfFindIndex];
                        }
                        shouldSetSelectedToleranceClass = false;
                        break;
                }
            };

            var args = new MessageBoxEventArgs(action,
                _localization.Strings.GetString(
                    "Do you want to save your changes? This change will affect the references."),
                "",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);
            MessageBoxRequest?.Invoke(this, args);
            return shouldSetSelectedToleranceClass;
        }

        private bool IsEmptyOrDuplicae(ToleranceClassModel toleranceClass)
        {
            if (toleranceClass is null)
            {
                return true;
            }

            if (!string.IsNullOrEmpty(toleranceClass.Name) &&
                _toleranceClasses.Count(x => x.Name == toleranceClass.Name) <= 1)
            {
                return false;
            }

            var args2 = new MessageBoxEventArgs(delegate { },
                string.IsNullOrEmpty(toleranceClass.Name) ? _localization.Strings.GetString("An entry can not be empty!") : _localization.Strings.GetString("An item with the same name already exists!"),
                "",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args2);
            return true;
        }

        public ToleranceClassViewModel(IStartUp starUp, IToleranceClassUseCase toleranceClassUseCase, ISaveColumnsUseCase saveColumnsUseCase, ILocalizationWrapper localization)
        {
            _startUp = starUp;
            _toleranceClassUseCase = toleranceClassUseCase;
            _saveColumnsUseCase = saveColumnsUseCase;
            _localization = localization;
            _localization.Subscribe(this);
            _toleranceClasses = new ObservableCollection<ToleranceClassModel>();
            ToleranceClasses = new ListCollectionView(_toleranceClasses);
            _selectedToleranceClassModels = new ObservableCollection<ToleranceClassModel>();
            SelectedToleranceClassListCollectionView = new ListCollectionView(_selectedToleranceClassModels);
            ReferencedLocations = new ObservableCollection<LocationReferenceLink>();
            ReferencedLocationToolAssignments = new ObservableCollection<LocationToolAssignmentModel>();

            RemoveToleranceClassCommand = new RelayCommand(ExecuteRemoveToleranceClass, CanExecuteRemoveToleranceClass);
            AddToleranceClassCommand = new RelayCommand(ExecuteAddToleranceClass, CanExecuteAddToleranceClass);
            SaveToleranceClassCommand = new RelayCommand(ExecuteSaveToleranceClass, CanExecuteSaveToleranceClass);
            LoadedCommand = new RelayCommand(ExecuteLoaded, CanExecuteLoaded);
            SelectionChanged = new RelayCommand(ExecuteSelectionChanged, o => true);
            LoadReferencedLocationsCommand = new RelayCommand(LoadReferencedLocationsExecute, LoadReferencedLocationsCanExecute);
            LoadReferencedLocationToolAssignmentsCommand = new RelayCommand(LoadReferencedLocationToolAssignmentsExecute, LoadReferencedLocationToolAssignmentsCanExecute);
            SetupListViewColumns();
        }

        private void SetupListViewColumns()
        {
            ListViewColumns = new Syncfusion.UI.Xaml.Grid.Columns
            {
                new GridTextColumn
                {
                    MappingName = nameof(ToleranceClassModel.Name)
                },
                new GridAbsoluteOrRelativeColumn(_localization)
                {
                    MappingName = nameof(ToleranceClassModel.Relative)
                },
                new GridCheckBoxColumn
                {
                    MappingName = nameof(ToleranceClassModel.SymmetricalLimits)
                },
                new GridNumericColumn
                {
                    MappingName = nameof(ToleranceClassModel.LowerLimit),
                    NumberDecimalDigits = 3
                },
                new GridNumericColumn
                {
                    MappingName = nameof(ToleranceClassModel.UpperLimit),
                    NumberDecimalDigits = 3
                },
            };
            ListViewColumns.ForEach(x => x.MinimumWidth = 50);
            UpdateColumnTranslation();
        }

        private void UpdateColumnTranslation()
        {
            const string tc = "Tolerance class";
            ListViewColumns.SingleOrDefault(x => x.MappingName != null && x.MappingName == nameof(ToleranceClassModel.Name)).HeaderText =
                _localization.Strings.GetParticularString(tc, "Name");
            ListViewColumns.Remove(
                ListViewColumns.SingleOrDefault(x => x.MappingName == nameof(ToleranceClassModel.Relative)));
            ListViewColumns.Insert(ListViewColumns.IndexOf(ListViewColumns.SingleOrDefault(x => x.MappingName != null && x.MappingName == nameof(ToleranceClassModel.Name))) + 1, new GridAbsoluteOrRelativeColumn(_localization)
            {
                MappingName = nameof(ToleranceClassModel.Relative)
            });
            ListViewColumns.SingleOrDefault(x => x.MappingName == nameof(ToleranceClassModel.Relative)).HeaderText =
                _localization.Strings.GetParticularString(tc, "Tolerance");
            ListViewColumns.SingleOrDefault(x => x.MappingName == nameof(ToleranceClassModel.SymmetricalLimits)).HeaderText =
                _localization.Strings.GetParticularString(tc, "Symmetrical limits");
            ListViewColumns.SingleOrDefault(x => x.MappingName == nameof(ToleranceClassModel.LowerLimit)).HeaderText =
                _localization.Strings.GetParticularString(tc, "Lower");
            ListViewColumns.SingleOrDefault(x => x.MappingName == nameof(ToleranceClassModel.UpperLimit)).HeaderText =
                _localization.Strings.GetParticularString(tc, "Upper");
        }

        private void ExecuteSelectionChanged(object obj)
        {
            foreach (ToleranceClassModel toleranceClassModel in (obj as SelectionChangedEventArgs)?.RemovedItems)
            {
                _selectedToleranceClassModels.Remove(toleranceClassModel);
            }

            foreach (ToleranceClassModel toleranceClassModel in (obj as SelectionChangedEventArgs)?.AddedItems)
            {
                _selectedToleranceClassModels.Add(toleranceClassModel);
            }

            IsListViewVisible = _selectedToleranceClassModels.Count > 1;
        }

        private bool CanExecuteSaveToleranceClass(object arg)
        {
            if (SelectedToleranceClassModel == null)
            {
                return false;
            }
            return !_originalToleranceClassModel?.EqualsByContent(SelectedToleranceClassModel) ?? false;
        }

        private void ExecuteSaveToleranceClass(object obj)
        {
            if (!IsEmptyOrDuplicae(SelectedToleranceClassModel))
            {
                _startUp.RaiseShowLoadingControl(true);
                _toleranceClassUseCase.SaveToleranceClass(SelectedToleranceClassModel.Entity, _originalToleranceClassModel.Entity);
                _originalToleranceClassModel = SelectedToleranceClassModel.CopyDeep();
            }
        }

        private bool CanExecuteLoaded(object arg)
        {
            return true;
        }

        private void ExecuteLoaded(object arg)
        {
            _startUp.RaiseShowLoadingControl(true);
            _saveColumnsUseCase.LoadColumnWidths(GridName, ListViewColumns.Select(x => x.MappingName).ToList());
            LoadToleranceClasses();
        }

        private bool CanExecuteAddToleranceClass(object arg)
        {
            return true;
        }

        private void ExecuteAddToleranceClass(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);

            int counter = 0;
            string name;
            while (true)
            {
                name = $"{_localization.Strings.GetString("Tolerance class")} {_toleranceClasses.Count + counter + 1}";
                if (_toleranceClasses.FirstOrDefault(x => x.Name == name) == null)
                {
                    // Leave loop if Value is unique
                    break;
                }
                else
                {
                    counter++;
                }
            }

            _toleranceClassUseCase.AddToleranceClass(new ToleranceClass { Name = name });
        }

        private bool CanExecuteRemoveToleranceClass(object arg)
        {
            return SelectedToleranceClassModel != null;
        }

        private void ExecuteRemoveToleranceClass(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);

            Action<MessageBoxResult> resultAction = (r) =>
            {
                if (r == MessageBoxResult.Yes)
                {
                    SelectedToleranceClassModel?.UpdateWith(_originalToleranceClassModel?.Entity);
                    _toleranceClassUseCase.RemoveToleranceClass(SelectedToleranceClassModel?.Entity, this);
                }
                else
                {
                    _startUp.RaiseShowLoadingControl(false);
                }
            };
            var args = new MessageBoxEventArgs(resultAction,
            _localization.Strings.GetString("Do you really want to remove this item?"),
            _localization.Strings.GetParticularString("Window Title", "Warning"),
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

            MessageBoxRequest?.Invoke(this, args);
        }

        private bool LoadReferencedLocationsCanExecute(object arg)
        {
            return SelectedToleranceClassModel != null;
        }

        private void LoadReferencedLocationsExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);
            _toleranceClassUseCase.LoadReferencedLocations(new ToleranceClassId(SelectedToleranceClassModel.Id));
        }

        private bool LoadReferencedLocationToolAssignmentsCanExecute(object arg)
        {
            return SelectedToleranceClassModel != null;
        }

        private void LoadReferencedLocationToolAssignmentsExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);
            _toleranceClassUseCase.LoadReferencedLocationToolAssignments(new ToleranceClassId(SelectedToleranceClassModel.Id));
        }


        public bool CanClose()
        {
            var canClose = AskForSaving(SelectedToleranceClassModel);
            if (canClose && _listViewWasShown)
            {
                _saveColumnsUseCase.SaveColumnWidths(GridName, ListViewColumns.Where(x => !double.IsNaN(x.Width)).Select(x => (x.MappingName, x.Width)).ToList());
            }

            return canClose;
        }

        public void ShowToleranceClasses(List<ToleranceClass> toleranceClasses)
        {
            _guiDispatcher.Invoke(() =>
            {
                _toleranceClasses.Clear();
                foreach (var toleranceClass in toleranceClasses)
                {
                    _toleranceClasses.Add(ToleranceClassModel.GetModelFor(toleranceClass));
                }
                RaiseClearShowChanges();
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowToleranceClassesError()
        {
            _guiDispatcher.Invoke(() =>
            {
                var args = new MessageBoxEventArgs((r) => { },
                    caption: _localization.Strings.GetString("Unknown Error!"),
                    text: _localization.Strings.GetString("An unknown error occured.\nPlease contact our Support Team"),
                    messageBoxButton: MessageBoxButton.OK,
                    messageBoxImage: MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, args);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void RemoveToleranceClass(ToleranceClass toleranceClass)
        {
            _guiDispatcher.Invoke(() =>
            {
                var foundToleranceClass = _toleranceClasses.FirstOrDefault(x => x.Entity.EqualsById(toleranceClass));
                if (foundToleranceClass != null)
                {
                    _toleranceClasses.Remove(foundToleranceClass);

                    if(_originalToleranceClassModel?.Entity.EqualsById(toleranceClass) == true)
                    {
                        _originalToleranceClassModel = null;
                    }

                    RaiseClearShowChanges();
                }
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void RemoveToleranceClassError()
        {
            _guiDispatcher.Invoke(() =>
            {
                var args = new MessageBoxEventArgs((r) => { },
                    caption: _localization.Strings.GetString("Unknown Error!"),
                    text: _localization.Strings.GetString("An unknown error occured.\nPlease contact our Support Team"),
                    messageBoxButton: MessageBoxButton.OK,
                    messageBoxImage: MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, args);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void AddToleranceClass(ToleranceClass toleranceClass)
        {
            _guiDispatcher.Invoke(() =>
            {
                var model = ToleranceClassModel.GetModelFor(toleranceClass);
                _toleranceClasses.Add(model);
                SelectedToleranceClassModel = model;
                RaiseClearShowChanges();
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void AddToleranceClassError()
        {
            _guiDispatcher.Invoke(() =>
            {
                var args = new MessageBoxEventArgs((r) => { },
                    caption: _localization.Strings.GetString("Unknown Error!"),
                    text: _localization.Strings.GetString("An unknown error occured.\nPlease contact our Support Team"),
                    messageBoxButton: MessageBoxButton.OK,
                    messageBoxImage: MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, args);
                _startUp.RaiseShowLoadingControl(false);
            });
        }
        public void UpdateToleranceClass(ToleranceClass toleranceClass)
        {
            _guiDispatcher.Invoke(() =>
            {
                var toleranceClassToUpdate = _toleranceClasses.FirstOrDefault(x => x.Entity.EqualsById(toleranceClass));
                toleranceClassToUpdate?.UpdateWith(toleranceClass);
                RaiseClearShowChanges();
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void SaveToleranceClassError()
        {
            _guiDispatcher.Invoke(() =>
            {
                var args = new MessageBoxEventArgs((r) => { },
                    caption: _localization.Strings.GetString("Unknown Error!"),
                    text: _localization.Strings.GetString("An unknown error occured.\nPlease contact our Support Team"),
                    messageBoxButton: MessageBoxButton.OK,
                    messageBoxImage: MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, args);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowReferencedLocations(List<LocationReferenceLink> locationReferenceLinks)
        {
            _guiDispatcher.Invoke(() =>
            {
                ReferencedLocations.Clear();
                locationReferenceLinks.ForEach(x => ReferencedLocations.Add(x));
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowReferencesError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("ToleranceClassViewModel", "An error occured while loading the references"),
                "",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void ShowReferencedLocationToolAssignments(List<LocationToolAssignment> assignments)
        {
            _guiDispatcher.Invoke(() =>
            {
                ReferencedLocationToolAssignments.Clear();
                assignments.ForEach(x => ReferencedLocationToolAssignments.Add(LocationToolAssignmentModel.GetModelFor(x, _localization)));
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowRemoveToleranceClassPreventingReferences(List<LocationReferenceLink> referencedLocations, List<LocationToolAssignment> referencedLocationToolAssignments)
        {
            _guiDispatcher.Invoke(() =>
            {
                var referenceLists = new List<ReferenceList>();

                if (referencedLocations != null && referencedLocations.Count > 0)
                {
                    referenceLists.Add(new ReferenceList
                    {
                        Header = _localization.Strings.GetParticularString("ToleranceClassViewModel", "Location"),
                        References = referencedLocations.Select(x => x.DisplayName).ToList()
                    });
                }

                if (referencedLocationToolAssignments != null && referencedLocationToolAssignments.Count > 0)
                {
                    referenceLists.Add(new ReferenceList
                    {
                        Header = _localization.Strings.GetParticularString("ToleranceClassViewModel", "Location tool assignment"),
                        References = referencedLocationToolAssignments.Select(x => $"{x.AssignedLocation.Number.ToDefaultString()}/{x.AssignedLocation.Description.ToDefaultString()} - {x.AssignedTool.InventoryNumber}/{x.AssignedTool.SerialNumber}")
                            .ToList()
                    });
                }

                ReferencesDialogRequest?.Invoke(this, referenceLists);
                _startUp.RaiseShowLoadingControl(false);
            });
        }


        public void LoadToleranceClasses()
        {
            _toleranceClassUseCase.LoadToleranceClasses();
        }

        public void SetDispatcher(Dispatcher dispatcher)
        {
            _guiDispatcher = dispatcher;
        }

        public void RegisterEventHandler(EventHandler<MessageBoxEventArgs> handler)
        {
            MessageBoxRequest += handler;
        }

        public void LanguageUpdate()
        {
            UpdateColumnTranslation();

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
                foreach (var (mappingName, width) in columns)
                {
                    if (ListViewColumns.SingleOrDefault(x => x.MappingName == mappingName) is GridColumn column)
                    {
                        column.Width = width;
                    }
                }
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
    }
}