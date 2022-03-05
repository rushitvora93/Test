using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using Core;
using Core.Entities.ReferenceLink;
using FrameworksAndDrivers.Gui.Wpf.View.Dialogs;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public interface IHelperTableViewModel : INotifyPropertyChanged
    {
        bool AreReferencesShown { get; }
        void RegisterMessageBoxEventHandler(EventHandler<MessageBoxEventArgs> handler);
        void RegisterReferencesDialogRequest(EventHandler<List<ReferenceList>> handler);
        void RegisterSelectAndFocusInputField(EventHandler handler);
        void SetGuiDispatcher(Dispatcher guiDispatcher);
    }

    /// <typeparam name="T">Entity that should be shown with the HelperTable (e. g. ToolType, Status, ...)</typeparam>
    /// <typeparam name="V">Type of the representing Value in the HelperTableItemModel</typeparam>
    public class HelperTableViewModel<T, U>
        : BindableBase
        , ICanClose
        , IGetUpdatedByLanguageChanges
        , IHelperTableViewModel
        , IHelperTableGui<T>
        , IHelperTableErrorGui<T>
        , IHelperTableShowReferencesGui
        where T : HelperTableEntity, IQstEquality<T>, IUpdate<T>, ICopy<T>
    {
        #region Properties
        private readonly IStartUp _startUp;
        private readonly IHelperTableInterface<T, U> _interface;
        private IHelperTableUseCase<T> _useCase;
        private bool _selectedItemChanged;
        private bool _canSelectionBeChanged = true;
        private U _itemBeforeChange;
        private Func<string> _getHelperTableName;

        // Mapping Functions
        private Func<T, HelperTableItemModel<T, U>> _mapEntityToModel;
        private Func<HelperTableItemModel<T, U>, T> _mapModelToEntity;
        private Action<HelperTableItemModel<T, U>, T> _updateModelByEntity;
        private Func<T> _createNewHelperTableItem;

        // List/ListView that is shown in the View
		private ObservableCollection<HelperTableItemModel<T, U>> _helperTableList; // Delete with feature toggle HelperTablesWithInterfaceAdapter
        public ListCollectionView HelperTableCollectionView { get; private set; }

        private ObservableCollection<ToolModelReferenceLink> _referencedToolModels;
        public ListCollectionView ReferencedToolModels { get; private set; }

        private ObservableCollection<ToolReferenceLink> _referencedTools;
        public ListCollectionView ReferencedTools { get; private set; }

        public ObservableCollection<LocationToolAssignmentModel> ReferencedLocationToolAssignments { get; private set; }

        private bool _hasReferencedToolModels;
        public bool HasReferencedToolModels
        {
            get => _hasReferencedToolModels;
            set => Set(ref _hasReferencedToolModels, value);
        }

        private bool _hasReferencedTools;
        public bool HasReferencedTools
        {
            get => _hasReferencedTools;
            set => Set(ref _hasReferencedTools, value);
        }

        private bool _hasReferencedLocationToolAssignments;
        public bool HasReferencedLocationToolAssignments
        {
            get => _hasReferencedLocationToolAssignments;
            set => Set(ref _hasReferencedLocationToolAssignments, value);
        }

        private bool _areReferencesShown;
        public bool AreReferencesShown
        {
            get => _areReferencesShown;
            set => Set(ref _areReferencesShown, value);
        }

        private HelperTableItemModel<T, U> _selectedItem;
        public HelperTableItemModel<T, U> SelectedItem
        {
            get => _selectedItem;
            set
            {
                Set(ref _selectedItem, value);
                _referencedToolModels.Clear();
            }
        }

        private string _helperTableName;
        public string HelperTableName
        {
            get => _helperTableName;
            set => Set(ref _helperTableName, value);
        }

        private ILocalizationWrapper _localization;
        public Dispatcher _guiDispatcher;
        #endregion


        #region Events
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler<List<ReferenceList>> ReferencesDialogRequest;
        public event EventHandler SelectAndFocusInputField;
        #endregion


        #region Commands
        public RelayCommand AddItemCommand { get; private set; }
        public RelayCommand RemoveItemCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand SelectionChangedCommand { get; private set; }
        public RelayCommand PrintListCommand { get; private set; }
        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand LoadReferencedToolModelsCommand { get; private set; }


        // CanExecutes
        private bool AddItemCanExecute(object arg) { return true; }
        private bool RemoveItemCanExecute(object arg) { return SelectedItem != null; }
        private bool SaveCanExecute(object arg) { return _selectedItemChanged; }
        private bool SelectionChangedCanExecute(object arg) { return _canSelectionBeChanged; }
        private bool PrintListCanExecute(object arg) { return true; }
        private bool LoadedCanExecute(object arg) { return true; }
        public bool LoadReferencedToolModelsCanExecute(object arg) { return SelectedItem != null; }

        // Executes
        private void AddItemExecute(object obj)
        {
			var itemList = FeatureToggles.FeatureToggles.HelperTablesWithInterfaceAdapter
				? _interface.HelperTableItems
				: _helperTableList;

            _startUp.RaiseShowLoadingControl(true);

            // Ask for saving if possible
            SaveCommand.Invoke(null);

            HelperTableItemModel<T, U> newItem;

            // Check if the Entities contain strings
            if (typeof(U) == typeof(string))
            {
                // Get unique value as default
                int counter = 0;
                string value;
                while (true)
                {
                    value = $"{_getHelperTableName()} {itemList.Count + counter + 1}";
                    if (itemList.FirstOrDefault(i => (i.Value as string) == value) == null)
                    {
                        // Leave loop if Value is unique
                        break;
                    }
                    else
                    {
                        counter++;
                    }
                }

                newItem = _mapEntityToModel(_createNewHelperTableItem());
                newItem.Value = (U)Convert.ChangeType(value, typeof(U));
            }
            else
            {
                // Else create a new Model without a default value
                newItem = _mapEntityToModel(_createNewHelperTableItem());
            }

            newItem.ListId = 0;
            newItem.HelperTableItemChanged += HelperTableItemModel_HelperTableItemChanged;
            itemList.Add(newItem);
            _useCase.AddItem(_mapModelToEntity(newItem), this);
            _selectedItemChanged = false;
        }

        private void RemoveItemExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);

            // Ask if the item should reallay be removed
            // Define reuslt action
            Action<MessageBoxResult> resultAction = (r) =>
            {
                if (r == MessageBoxResult.Yes)
                {
                    SelectedItem.Value = _itemBeforeChange;
                    _selectedItemChanged = false;
                    _useCase.RemoveItem(_mapModelToEntity(SelectedItem), this);
                }
                else
                {
                    _startUp.RaiseShowLoadingControl(false);
                }
            };

            // Define EventArgs
            var args = new MessageBoxEventArgs(resultAction,
                                               _localization.Strings.GetString("Do you really want to remove this item?"),
                                               _localization.Strings.GetParticularString("Window Title", "Warning"),
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Exclamation);

            // Request MessageBox
            MessageBoxRequest?.Invoke(this, args);
        }

        private void SaveExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);

            // Notify user if you cannot save
            if (SelectedItem.Value as string == "" || IsItemADuplicate(SelectedItem))
            {
                var warningArgs = new MessageBoxEventArgs(delegate { },
                                                          SelectedItem.Value as string == ""
                                                            ? _localization.Strings.GetString("An entry can not be empty!")
                                                            : _localization.Strings.GetString("An item with the same name already exists!"),
                                                          "",
                                                          MessageBoxButton.OK,
                                                          MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, warningArgs);
                _canSelectionBeChanged = false;
                SelectedItem.Value = _itemBeforeChange;

                _selectedItemChanged = false;
                _canSelectionBeChanged = true;
                return;
            }

            Action<MessageBoxResult> resultAction = (r) =>
            {
                if (r == MessageBoxResult.Yes)
                {
                    var model = _mapEntityToModel(_createNewHelperTableItem());
                    model.ListId = SelectedItem.ListId;
                    model.Value = _itemBeforeChange;
                    _useCase.SaveItem(_mapModelToEntity(model), _mapModelToEntity(SelectedItem), this);
                    _itemBeforeChange = SelectedItem.Value;
                    _selectedItemChanged = false;
                }
                else
                {
                    SelectedItem.Value = _itemBeforeChange;
                    _selectedItemChanged = false;
                    _startUp.RaiseShowLoadingControl(false);
                }
            };

            var args = new MessageBoxEventArgs(resultAction,
                _localization.Strings.GetString("Do you want to save your changes? This change will affect the references."),
                "",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            // Request MessageBox
            MessageBoxRequest?.Invoke(this, args);
        }

        private void SelectionChangedExecute(object obj)
        {
            // Notify user if you cannot save the last selected item
            if ((obj as SelectionChangedEventArgs).RemovedItems.Count > 0)
            {
                var lastSelectedItem = (HelperTableItemModel<T, U>)(obj as SelectionChangedEventArgs).RemovedItems[0];

                if (lastSelectedItem.Value as string == "" || IsItemADuplicate(lastSelectedItem))
                {
                    var args = new MessageBoxEventArgs(delegate { },
                                                       lastSelectedItem.Value as string == ""
                                                        ? _localization.Strings.GetString("An entry can not be empty!")
                                                        : _localization.Strings.GetString("An item with the same name already exists!"),
                                                       "",
                                                       MessageBoxButton.OK,
                                                       MessageBoxImage.Error);
                    MessageBoxRequest?.Invoke(this, args);
                    _canSelectionBeChanged = false;
                    lastSelectedItem.Value = _itemBeforeChange;

                    _selectedItemChanged = false;
                    _canSelectionBeChanged = true;
                    return;
                }
            }

            if (_selectedItemChanged)
            {
                // Ask if the change should be saved

                // Define result action
                Action<MessageBoxResult> resultAction = (r) =>
                {
                    if (r == MessageBoxResult.Yes)
                    {
                        // Save Item
                        var lastSelectedItem = (HelperTableItemModel<T, U>)(obj as SelectionChangedEventArgs).RemovedItems[0];
                        var model = _mapEntityToModel(_createNewHelperTableItem());
                        model.ListId = lastSelectedItem.ListId;
                        model.Value = _itemBeforeChange;
                        _useCase.SaveItem(_mapModelToEntity(model), _mapModelToEntity(lastSelectedItem), this);
                    }
                    else if (r == MessageBoxResult.No)
                    {
                        // Reset value
                        ((obj as SelectionChangedEventArgs).RemovedItems[0] as HelperTableItemModel<T, U>).Value = _itemBeforeChange;
                    }
                };

                // Define EventArgs
                var args = new MessageBoxEventArgs(resultAction,
                                                   _localization.Strings.GetString("Do you want to save your changes? This change will affect the references."),
                                                   "",
                                                   MessageBoxButton.YesNo,
                                                   MessageBoxImage.Question);

                // Request MessageBox
                MessageBoxRequest?.Invoke(this, args);
            }

            // Update helper properties
            _selectedItemChanged = false;

            if ((obj as SelectionChangedEventArgs).AddedItems.Count > 0)
            {
                _itemBeforeChange = ((obj as SelectionChangedEventArgs).AddedItems[0] as HelperTableItemModel<T, U>).Value;
            }

            AreReferencesShown = false;
        }

        private void PrintListExecute(object obj)
        {

        }

        private void LoadedExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);
            _useCase.LoadItems(this);
        }

        private void LoadReferencedToolModelsExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);
            _referencedToolModels.Clear();
            _referencedTools.Clear();
            _useCase.LoadReferences(new HelperTableEntityId(SelectedItem.ListId), this);
        }
        #endregion


        #region Methods
        private void AskForSaving(CancelEventArgs e)
        {
            // Notify user if you cannot save
            if (SelectedItem != null && (SelectedItem.Value as string == "" || IsItemADuplicate(SelectedItem)))
            {
                var warningArgs = new MessageBoxEventArgs((r) => { },
                                                          SelectedItem.Value as string == ""
                                                            ? _localization.Strings.GetString("An entry can not be empty!")
                                                            : _localization.Strings.GetString("An item with the same name already exists!"),
                                                          messageBoxImage: MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, warningArgs);
                return;
            }

            if (!_selectedItemChanged)
            {
                return;
            }

            // Define result action
            Action<MessageBoxResult> resultAction = (r) =>
            {
                if (r == MessageBoxResult.Yes)
                {
                    // Save Item
                    var model = _mapEntityToModel(_createNewHelperTableItem());
                    model.ListId = SelectedItem.ListId;
                    model.Value = _itemBeforeChange;
                    _useCase.SaveItem(_mapModelToEntity(model), _mapModelToEntity(SelectedItem), this);
                }
                else if (r == MessageBoxResult.No)
                {
                    SelectedItem.Value = _itemBeforeChange;
                    _selectedItemChanged = false;
                }
                else if (r == MessageBoxResult.Cancel)
                {
                    // Cancel the the window closing action
                    e.Cancel = true;
                }
            };

            // Define EventArgs
            var args = new MessageBoxEventArgs(resultAction,
                                               _localization.Strings.GetString("Do you want to save your changes? This change will affect the references."),
                                               "",
                                               MessageBoxButton.YesNoCancel,
                                               MessageBoxImage.Question);

            // Request MessageBox
            MessageBoxRequest?.Invoke(this, args);
        }

        private bool IsItemADuplicate(HelperTableItemModel<T, U> item)
        {
			var itemList = FeatureToggles.FeatureToggles.HelperTablesWithInterfaceAdapter
				? _interface.HelperTableItems
				: _helperTableList;
            // One item has to be found -> the changed/selected item itslef
            return itemList.Where(x => x.Value.Equals(item.Value)).ToList().Count > 1;
        }
        #endregion


        #region Interface
        public void SetGuiDispatcher(Dispatcher guiDispatcher)
        {
            _guiDispatcher = guiDispatcher;
        }

        public void ShowItems(List<T> items)
        {
            // Refill _helperTableList

            _guiDispatcher.Invoke(() =>
            {
				_helperTableList.Clear(); // remove with HelperTablesWithInterfaceAdapter

                foreach (var i in items)
                {
                    var newModel = _mapEntityToModel(i);
                    newModel.HelperTableItemChanged += HelperTableItemModel_HelperTableItemChanged;
					_helperTableList.Add(newModel); // remove with HelperTablesWithInterfaceAdapter
                }
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowErrorMessage()
        {
            _guiDispatcher.Invoke(() =>
            {
                var args = new MessageBoxEventArgs(delegate { },
                _localization.Strings.GetString("The action failed.\nTry again or contact our support team"),
                "",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

                // Request MessageBox
                MessageBoxRequest?.Invoke(this, args);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowEntryAlreadyExists(T newItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var args = new MessageBoxEventArgs(delegate { },
                _localization.Strings.GetString("An item with the same name already exists!"),
                "",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, args);
                _canSelectionBeChanged = false;
				var itemList = FeatureToggles.FeatureToggles.HelperTablesWithInterfaceAdapter
					? _interface.HelperTableItems
					: _helperTableList;
                var current = itemList.FirstOrDefault(x => x.Entity.EqualsById(newItem));
                if (current != null)
                {
                    current.Value = _itemBeforeChange;
                }

                _canSelectionBeChanged = true;
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void Add(T newItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var newModel = _mapEntityToModel(newItem);
				var itemList = FeatureToggles.FeatureToggles.HelperTablesWithInterfaceAdapter
					? _interface.HelperTableItems
					: _helperTableList;
                var alreadyAddedItem = itemList.FirstOrDefault((i) => i.Value.Equals(newModel.Value));

                if (alreadyAddedItem == null)
                {
                    // If the items isn't added yet 
                    _helperTableList.Add(newModel); // remove with HelperTablesWithInterfaceAdapter
                }
                else
                {
                    // If the item is added yet
                    alreadyAddedItem.ListId = newItem.ListId.ToLong();
                }

                SelectedItem = itemList.FirstOrDefault(x => x.Entity.EqualsById(newItem));
                SelectAndFocusInputField?.Invoke(this, System.EventArgs.Empty);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void Remove(T removeItem)
        {
            _guiDispatcher.Invoke(() =>
            {
				var itemList = FeatureToggles.FeatureToggles.HelperTablesWithInterfaceAdapter
					? _interface.HelperTableItems
					: _helperTableList;
                var itemToRemove = itemList.First((item) => item.Entity.EqualsById(removeItem));
                _selectedItemChanged = false;
                itemToRemove.HelperTableItemChanged -= HelperTableItemModel_HelperTableItemChanged;
                _helperTableList.Remove(itemToRemove); // remove with HelperTablesWithInterfaceAdapter
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void Save(T savedItem)
        {
            _guiDispatcher.Invoke(() =>
            {
				var itemList = FeatureToggles.FeatureToggles.HelperTablesWithInterfaceAdapter
					? _interface.HelperTableItems
					: _helperTableList;

                if (itemList.Count != 0)
                {
                    var itemToUpdate = itemList.First((item) => item.Entity.EqualsById(savedItem));
                    _updateModelByEntity(itemToUpdate, savedItem);
                    _selectedItemChanged = false;
                    _startUp.RaiseShowLoadingControl(false);
                }
            });
        }

        public void ShowReferencedToolModels(List<ToolModelReferenceLink> toolModelReferenceLinks)
        {
            _guiDispatcher.Invoke(() =>
            {
                _referencedToolModels.Clear();

                foreach (var toolModelReferenceLink in toolModelReferenceLinks)
                {
                    _referencedToolModels.Add(toolModelReferenceLink);
                }
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowToolReferenceLinks(List<ToolReferenceLink> toolReferenceLinks)
        {
            _guiDispatcher.Invoke(() =>
            {
                _referencedTools.Clear();

                foreach (var toolReferenceLink in toolReferenceLinks)
                {
                    _referencedTools.Add(toolReferenceLink);
                }
                _startUp.RaiseShowLoadingControl(false);
            });
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

        public void ShowReferencesError()
        {
            _guiDispatcher.Invoke(() =>
            {
                var args = new MessageBoxEventArgs((r) => { },
                    _localization.Strings.GetString("An error occured while loading the handler"),
                    _localization.Strings.GetString("Error"),
                    messageBoxButton: MessageBoxButton.OK,
                    messageBoxImage: MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, args);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowRemoveHelperTableItemPreventingReferences(List<ToolModelReferenceLink> toolModels, List<ToolReferenceLink> tools, List<LocationToolAssignment> locationToolAssignments)
        {
            _guiDispatcher.Invoke(() =>
            {
                var list = new List<ReferenceList>();
                list.Add(new ReferenceList { Header = _localization.Strings.GetParticularString("HelperTableViewModel", "Tool model"), References = toolModels.Select(x => x.DisplayName).ToList() });
                list.Add(new ReferenceList { Header = _localization.Strings.GetParticularString("HelperTableViewModel", "Tool"), References = tools.Select(x => x.DisplayName).ToList() });
                list.Add(new ReferenceList
                {
                    Header = _localization.Strings.GetParticularString("HelperTableViewModel", "Location tool assignments"),
                    References = locationToolAssignments
                    .Select(x => $"{x.AssignedLocation.Number.ToDefaultString()}/{x.AssignedLocation.Description.ToDefaultString()} - {x.AssignedTool.InventoryNumber}/{x.AssignedTool.SerialNumber}").ToList()
                });
                ReferencesDialogRequest?.Invoke(this, list);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public bool CanClose()
        {
            var eventArgs = new CancelEventArgs();
            AskForSaving(eventArgs);
            return !eventArgs.Cancel;
        }
        #endregion


        #region Event-Handler
        private void HelperTableItemModel_HelperTableItemChanged(object sender, RoutedPropertyChangedEventArgs<U> e)
        {
            _selectedItemChanged = true;
        }

        public void LanguageUpdate()
        {
            HelperTableName = string.Format(_localization.Strings.GetParticularString("Auxiliary Master Data Name", "{0} - Labeling:"), _getHelperTableName());
        }

        public void RegisterMessageBoxEventHandler(EventHandler<MessageBoxEventArgs> handler)
        {
            MessageBoxRequest += handler;
        }

        public void RegisterReferencesDialogRequest(EventHandler<List<ReferenceList>> handler)
        {
            ReferencesDialogRequest += handler;
        }

        public void RegisterSelectAndFocusInputField(EventHandler handler)
        {
            SelectAndFocusInputField += handler;
        }

        #endregion


        public HelperTableViewModel(IStartUp starUp,
                                    IHelperTableUseCase<T> useCase,
                                    IHelperTableInterface<T, U> interfaceAdapter,
                                    Func<string> getHelperTableName,
                                    Func<T, HelperTableItemModel<T, U>> mapEntityToModel,
                                    Func<HelperTableItemModel<T, U>, T> mapModelToEntity,
                                    Action<HelperTableItemModel<T, U>, T> updateModelByEntity,
                                    Func<T> createNewHelperTableItem,
                                    ILocalizationWrapper localization)
        {
            _startUp = starUp;
            _useCase = useCase;
            _interface = interfaceAdapter;
            _getHelperTableName = getHelperTableName;
            _mapEntityToModel = mapEntityToModel;
            _mapModelToEntity = mapModelToEntity;
            _updateModelByEntity = updateModelByEntity;
            _createNewHelperTableItem = createNewHelperTableItem;
            _localization = localization;

            LanguageUpdate();

			_helperTableList = new ObservableCollection<HelperTableItemModel<T, U>>();
            _referencedToolModels = new ObservableCollection<ToolModelReferenceLink>();
            ReferencedToolModels = new ListCollectionView(_referencedToolModels);

            _referencedTools = new ObservableCollection<ToolReferenceLink>();
            ReferencedTools = new ListCollectionView(_referencedTools);

            ReferencedLocationToolAssignments = new ObservableCollection<LocationToolAssignmentModel>();

            // ListItems are sorted by the Value-Attribute (nameof is not possible here, because T does not have a attribute Value by default) -> worst case: the list is not sorted
			var itemList = FeatureToggles.FeatureToggles.HelperTablesWithInterfaceAdapter
				? _interface.HelperTableItems
				: _helperTableList;
			HelperTableCollectionView = new ListCollectionView(itemList);
            HelperTableCollectionView.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
            HelperTableCollectionView.IsLiveSorting = true;

            AddItemCommand = new RelayCommand(AddItemExecute, AddItemCanExecute);
            RemoveItemCommand = new RelayCommand(RemoveItemExecute, RemoveItemCanExecute);
            SaveCommand = new RelayCommand(SaveExecute, SaveCanExecute);
            SelectionChangedCommand = new RelayCommand(SelectionChangedExecute, SelectionChangedCanExecute);
            PrintListCommand = new RelayCommand(PrintListExecute, PrintListCanExecute);
            LoadedCommand = new RelayCommand(LoadedExecute, LoadedCanExecute);
            LoadReferencedToolModelsCommand = new RelayCommand(LoadReferencedToolModelsExecute, LoadReferencedToolModelsCanExecute);
        }
    }

/*
    public class HelperTableViewModelNewA<T, U>
        : BindableBase
        , ICanClose
        , IGetUpdatedByLanguageChanges
        , IHelperTableViewModel
        , IHelperTableErrorGui<T>
        , IHelperTableShowReferencesGui
        where T
            : HelperTableEntity
            , IQstEquality<T>
            , IUpdate<T>, ICopy<T>
    {
        public HelperTableViewModelNewA(
            IHelperTableUseCase<T> helperTableUseCase,
            IHelperTableInterface<T, U> interfaceAdapter)
        {
            _useCase = helperTableUseCase;

            HelperTableCollectionView = new ListCollectionView(interfaceAdapter.HelperTableItems);
            // ListItems are sorted by the Value-Attribute (nameof is not possible here, because T does not have a attribute Value by default) -> worst case: the list is not sorted
            HelperTableCollectionView.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
            HelperTableCollectionView.IsLiveSorting = true;

            LoadedCommand = new RelayCommand(LoadedExecute, (sender) => { return true; });
        }

        public bool CanClose()
        {
            //throw new NotImplementedException();
            return true;
        }

        public void LanguageUpdate()
        {
            //throw new NotImplementedException();
        }

        public void RegisterMessageBoxEventHandler(EventHandler<MessageBoxEventArgs> handler)
        {
            //throw new NotImplementedException();
        }

        public void RegisterReferencesDialogRequest(EventHandler<List<ReferenceList>> handler)
        {
            //throw new NotImplementedException();
        }

        public void RegisterSelectAndFocusInputField(EventHandler handler)
        {
            //throw new NotImplementedException();
        }

        public void SetGuiDispatcher(Dispatcher guiDispatcher)
        {
            //throw new NotImplementedException();
        }

        public void ShowEntryAlreadyExists(T newItem)
        {
            //throw new NotImplementedException();
        }

        public void ShowErrorMessage()
        {
            //throw new NotImplementedException();
        }

        public void ShowReferencedLocationToolAssignments(List<LocationToolAssignment> assignments)
        {
            //throw new NotImplementedException();
        }

        public void ShowReferencedToolModels(List<ToolModelReferenceLink> toolModelReferenceLinks)
        {
            //throw new NotImplementedException();
        }

        public void ShowReferencesError()
        {
            //throw new NotImplementedException();
        }

        public void ShowRemoveHelperTableItemPreventingReferences(
            List<ToolModelReferenceLink> toolModels,
            List<ToolReferenceLink> tools, 
            List<LocationToolAssignment> locationToolAssignments)
        {
            //throw new NotImplementedException();
        }

        public void ShowToolReferenceLinks(List<ToolReferenceLink> toolReferenceLinks)
        {
            //throw new NotImplementedException();
        }

        private void LoadedExecute(object obj)
        {
            //_startUp.RaiseShowLoadingControl(true); <-- put back in?
            _useCase.LoadItems(this);
        }

        public bool AreReferencesShown { get; set; }
        public ListCollectionView HelperTableCollectionView { get; private set; } // <--- needs tests
        public RelayCommand LoadedCommand { get; private set; } // <--- needs tests
        public HelperTableItemModel<T, U> SelectedItem // <-- needs tests
        {
            get => _selectedItem;
            set
            {
                Set(ref _selectedItem, value);
            }
        }

        private IHelperTableUseCase<T> _useCase;
        private HelperTableItemModel<T, U> _selectedItem;
    }*/
}
