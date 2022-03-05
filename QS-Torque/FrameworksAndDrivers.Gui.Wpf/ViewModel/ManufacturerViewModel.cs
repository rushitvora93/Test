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
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Core.Entities.ReferenceLink;
using FrameworksAndDrivers.Gui.Wpf.View.Dialogs;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using Syncfusion.Data.Extensions;
using Syncfusion.UI.Xaml.Grid;
using SelectionChangedEventArgs = System.Windows.Controls.SelectionChangedEventArgs;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class ManufacturerViewModel : BindableBase, IManufacturerGui, ICanClose, ISaveColumnsGui, IGetUpdatedByLanguageChanges, IClearShownChanges
    {
        #region Properties
        private const string GridName = "Manu";

        private readonly IStartUp _startUp;
        private readonly IManufacturerUseCase _manufacturerUseCase;
        private readonly ISaveColumnsUseCase _saveColumnsUseCase;
        private ILocalizationWrapper _localization;
        private Dispatcher _guiDispatcher;

        private ObservableCollection<ManufacturerModel> _manufacturers;
        public ListCollectionView Manufacturers { get; set; }
        
        private ObservableCollection<ManufacturerModel> _selectedManufacturers;
        public ListCollectionView SelectedManufacturersCollectionView { get; private set; }

        private ObservableCollection<ToolModelReferenceLink> _referencedToolModels;
        public ListCollectionView ReferencedToolModels { get; private set; }

        private Columns _listViewColumns;

        public Columns ListViewColumns
        {
            get => _listViewColumns;
            set
            {
                _listViewColumns = value;
                RaisePropertyChanged();
            }
        }

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

        private ManufacturerModel _manufacturerBeforeChange;
        private ManufacturerModel _selectedManufacturer;

        public ManufacturerModel SelectedManufacturer
        {
            get => _selectedManufacturer;
            set
            {
                ManufacturerModel oldManufacturerModel = null;
                if (_selectedManufacturer != null)
                {
                    oldManufacturerModel = _selectedManufacturer.CopyDeep();
                }
                _selectedManufacturer = value;
                SelectedManufacturerChanged(oldManufacturerModel);
                LoadComment();
                RaisePropertyChanged(nameof(SelectedManufacturer));
                CommandManager.InvalidateRequerySuggested();

                if(oldManufacturerModel == null || _manufacturerBeforeChange?.EqualsByContent(_selectedManufacturer) == true || _selectedManufacturer == null)
                {
                    RaiseClearShowChanges();
                }

                AreReferencesShown = false;
                _referencedToolModels.Clear();
            }
        }

        public bool _areReferencesShown = false;
        public bool AreReferencesShown
        {
            get => _areReferencesShown;
            set => Set(ref _areReferencesShown, value);
        }
        #endregion


        #region Events
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler ClearShownChanges;
        public event EventHandler<ReferenceList> ReferencesDialogRequest;

        private void RaiseClearShowChanges()
        {
            ClearShownChanges?.Invoke(this, null);
        }
        #endregion


        #region Interface-Implementations
        public void ShowManufacturer(List<Manufacturer> manufacturer)
        {
            _guiDispatcher.Invoke(() =>
            {
                _manufacturers.Clear();
                foreach (var manu in manufacturer)
                {
                    _manufacturers.Add(ManufacturerModel.GetModelFor(manu));
                }
                RaiseClearShowChanges();
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void AddManufacturer(Manufacturer manufacturer)
        {
            _guiDispatcher.Invoke(() =>
            {
                var model = ManufacturerModel.GetModelFor(manufacturer);
                _manufacturers.Add(model);
                SelectedManufacturer = model;
                RaiseClearShowChanges();
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowErrorMessage()
        {
            _guiDispatcher.Invoke(() =>
            {
                var args = new MessageBoxEventArgs((r) => { },
                    caption: _localization.Strings.GetString("Unknown Error!"),
                    text: _localization.Strings.GetString("An unknown error occured.\nPlease contact our Support Team") ,
                    messageBoxButton: MessageBoxButton.OK,
                    messageBoxImage: MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, args);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowComment(Manufacturer manufacturer, string comment)
        {
            _guiDispatcher.Invoke(() =>
            {
                var manu = _manufacturers.FirstOrDefault(x => x.Entity.EqualsById(manufacturer));
                if (manu != null)
                {
                    manu.Comment = comment;
                }
                if (_manufacturerBeforeChange != null)
                {
                    _manufacturerBeforeChange.Comment = comment; 
                }
                RaisePropertyChanged();
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowCommentError()
        {
            _guiDispatcher.Invoke(() =>
            {
                var args = new MessageBoxEventArgs((r) => { },
                    _localization.Strings.GetString("Unknown Error!"),
                    _localization.Strings.GetString("An unknown error occured.\nPlease contact our Support Team"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, args);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void SaveManufacturer(Manufacturer manufacturer)
        {
            _guiDispatcher.Invoke(() =>
            {
                var manufacturerToUpdate = _manufacturers.FirstOrDefault(x => x.Entity.EqualsById(manufacturer));
                manufacturerToUpdate?.UpdateWith(manufacturer);
                RaiseClearShowChanges();
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        void IManufacturerGui.RemoveManufacturer(Manufacturer manufacturer)
        {
            var manu = _manufacturers.FirstOrDefault(x => x.Entity.EqualsById(manufacturer));
            if (manu != null)
            {
                if(manufacturer.EqualsById(_selectedManufacturer?.Entity))
                {
                    _selectedManufacturer = null;
                }
                _guiDispatcher.Invoke(() => _manufacturers.Remove(manu));
                RaiseClearShowChanges();
            }
            _startUp.RaiseShowLoadingControl(false);
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

        public void ShowReferencesError()
        {
            _guiDispatcher.Invoke(() =>
            {
                var args = new MessageBoxEventArgs((r) => { },
                    caption: _localization.Strings.GetString("An error occured while loading the referenced tool models"),
                    text: _localization.Strings.GetString("Error"),
                    messageBoxButton: MessageBoxButton.OK,
                    messageBoxImage: MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, args);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowRemoveManufacturerPreventingReferences(List<ToolModelReferenceLink> references)
        {
            _guiDispatcher.Invoke(() =>
            {
                ReferencesDialogRequest?.Invoke(this, 
                    new ReferenceList
                    {
                        Header = _localization.Strings.GetParticularString("ManufacturerViewModel", "Tool model"),
                        References = references.Select(x => x.DisplayName).ToList()
                    }
                );
                _startUp.RaiseShowLoadingControl(false);
            });
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

        public void LanguageUpdate()
        {
            UpdateColumnTranslation();
        }

        public bool CanClose()
        {
            var eventArgs = new CancelEventArgs();
            AskForSaving(eventArgs);
            if (!eventArgs.Cancel && _listViewWasShown)
            {
                _saveColumnsUseCase.SaveColumnWidths(GridName, ListViewColumns.Select(x => (x.MappingName,x.ActualWidth)).ToList());
            }
            return !eventArgs.Cancel;
        }

        #endregion


        #region Commands
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand RemoveManufacturer { get; private set; }
        public RelayCommand SelectionChanged { get; private set; }
        public RelayCommand AddManufacturerCommand { get; private set; }
		public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand LoadReferencedToolModelsCommand { get; private set; }

		private bool LoadedCanExeute(object arg)
		{
			return true;
		}

		private void LoadedExecute(object arg)
		{
            _startUp.RaiseShowLoadingControl(true);
			_saveColumnsUseCase.LoadColumnWidths(GridName, ListViewColumns.Select(x => x.MappingName).ToList());
            LoadManufacturers();
        }

        private bool AddManufacturerCanExecute(object arg)
        {
            return true;
        }

        private bool RemoveManufacturerCanExecute(object arg) { return SelectedManufacturer != null; }

        private bool SaveCanExecute(object arg)
        {
            if (SelectedManufacturer == null)
            {
                return false;
            }
            return !_manufacturerBeforeChange.EqualsByContent(SelectedManufacturer);
        }

        public bool LoadReferencedToolModelsCanExecute(object arg) { return SelectedManufacturer != null; }



        private void AddManufacturerExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);

            AskForSaving(new CancelEventArgs());

            // Get unique name as default
            int counter = 0;
            string name;
            while (true)
            {
                name = $"{_localization.Strings.GetString("Manufacturer")} {_manufacturers.Count + counter + 1}";
                if (_manufacturers.FirstOrDefault(x => x.Name == name) == null)
                {
                    // Leave loop if Value is unique
                    break;
                }
                else
                {
                    counter++;
                }
            }

            _manufacturerUseCase.AddManufacturer(new Manufacturer() { Name = new ManufacturerName(name) });
        }

        private void RemoveManufacturerExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);

            Action<MessageBoxResult> resultAction = (r) =>
            {
                if (r != MessageBoxResult.Yes)
                {
                    _startUp.RaiseShowLoadingControl(false);
                    return;
                }

                SelectedManufacturer?.UpdateWidth(_manufacturerBeforeChange);
                foreach (var i in _selectedManufacturers)
                {
                    _manufacturerUseCase.RemoveManufacturer(i.Entity, this);
                }
            };

            var args = new MessageBoxEventArgs(resultAction,
                                               _localization.Strings.GetString("Do you really want to remove this item?"),
                                               _localization.Strings.GetParticularString("Window Title", "Warning"),
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Warning);

            MessageBoxRequest?.Invoke(this, args);
        }

        private void SaveExecute(object obj)
        {
            if (!IsEmptyOrDuplicate(SelectedManufacturer))
            {
                _startUp.RaiseShowLoadingControl(true);

                _manufacturerUseCase.SaveManufacturer(
					_manufacturerBeforeChange.Entity,
					SelectedManufacturer.Entity);
                _manufacturerBeforeChange = SelectedManufacturer.CopyDeep();
            }
        }

        private void SelectionChangedExecute(object obj)
        {
            // Update _selectedManufacturers and SelectedManufacturersCollectionView
            foreach (ManufacturerModel i in (obj as SelectionChangedEventArgs).RemovedItems)
            {
                _selectedManufacturers.Remove(i);
            }

            foreach (ManufacturerModel i in (obj as SelectionChangedEventArgs).AddedItems)
            {
                _selectedManufacturers.Add(i);
            }

            IsListViewVisible = _selectedManufacturers.Count > 1;
        }

        private void LoadReferencedToolModelsExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);
            _manufacturerUseCase.LoadReferencedToolModels(SelectedManufacturer.Id);
        }
        #endregion


        #region Methods
        public void SetDispatcher(Dispatcher dispatcher)
		{
			_guiDispatcher = dispatcher;
		}

        public void LoadManufacturers()
        {
            _manufacturerUseCase.LoadManufacturer();
        }

        private void SelectedManufacturerChanged(ManufacturerModel oldManufacturerModel)
        {
            if (oldManufacturerModel == null || _manufacturerBeforeChange?.EqualsByContent(oldManufacturerModel) == true)
            {
                if (_selectedManufacturer != null)
                {
                    _manufacturerBeforeChange = _selectedManufacturer.CopyDeep();
                }
                return;
            }
            
            Action<MessageBoxResult> action = (r) =>
            {
                var setManufacturerBeforeChange = true;
                switch (r)
                {
                    case MessageBoxResult.Yes:
                    {
                        if (IsEmptyOrDuplicate(oldManufacturerModel))
                        {
                            var index = _manufacturers.ToList().FindIndex(x => x.EqualsById(oldManufacturerModel));
                            if (index != -1)
                            {
                                _manufacturers[index] = _manufacturerBeforeChange;
                            }
                        }
                        else
                        {
                            _startUp.RaiseShowLoadingControl(true);

                            _manufacturerUseCase.SaveManufacturer(_manufacturerBeforeChange.Entity, oldManufacturerModel.Entity);
                        }
						RaiseClearShowChanges();
                        break;
                    }
                    case MessageBoxResult.No:
                    {
                        var index = _manufacturers.ToList().FindIndex(x => x.EqualsById(_manufacturerBeforeChange));
                        if (index != -1)
                        {
                            _manufacturers[index] = _manufacturerBeforeChange;
                        }
                            RaiseClearShowChanges();
                        break;
                    }
                    case MessageBoxResult.Cancel:
                    {
                        var index = _manufacturers.ToList().FindIndex(x => x.EqualsById(_manufacturerBeforeChange));
                        if (index != -1)
                        {
                            _selectedManufacturer = _manufacturers[index];
                        }

                        setManufacturerBeforeChange = false;
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(r), r, null);
                }

                if (setManufacturerBeforeChange)
                {
                    if (_selectedManufacturer != null)
                    {
                        _manufacturerBeforeChange = _selectedManufacturer.CopyDeep();
                    }
                }
            };
            var args = new MessageBoxEventArgs(action,
                _localization.Strings.GetString("Do you want to save your changes? This change will affect the references."),
                "",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);

            // Request MessageBox
            MessageBoxRequest?.Invoke(this, args);
        }


        private void LoadComment()
        {
            if (_selectedManufacturer != null)
            {
                _startUp.RaiseShowLoadingControl(true);
                _manufacturerUseCase.LoadCommentForManufacturer(SelectedManufacturer.Entity);
            }
        }


        /// <summary>
        /// Checks if the manufacturerModel is empty
        /// or a ManufacturerModel with the same name already exists
        /// </summary>
        /// <param name="manufacturerModel"></param>
        /// <returns>true if empty or same name alredy exist</returns>
        private bool IsEmptyOrDuplicate(ManufacturerModel manufacturerModel)
        {
            if (manufacturerModel is null)
            {
                return true;
            }

            if (!string.IsNullOrEmpty(manufacturerModel.Name) &&
                _manufacturers.Count(x => x.Name == manufacturerModel.Name) <= 1)
            {
                return false;
            }
            var args2 = new MessageBoxEventArgs(delegate { },
                string.IsNullOrEmpty(manufacturerModel.Name) ? _localization.Strings.GetString("An entry can not be empty!") : _localization.Strings.GetString("An item with the same name already exists!"),
                "",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args2);
            return true;
        }

        private void AskForSaving(CancelEventArgs eventArgs)
        {
            if (SelectedManufacturer == null || _manufacturerBeforeChange?.EqualsByContent(SelectedManufacturer) == true)
            {
                return;
            }

            Action<MessageBoxResult> action = (r) =>
            {

                if (r == MessageBoxResult.Yes)
                {
                    if (IsEmptyOrDuplicate(SelectedManufacturer))
                    {
                        var index = _manufacturers.ToList().FindIndex(x => x.EqualsById(SelectedManufacturer));
                        if (index != -1)
                        {
                            _manufacturers[index] = _manufacturerBeforeChange;
                            return;
                        }
                    }
					SaveExecute(null);
                }
                else if (r == MessageBoxResult.No)
                {
                }
                else
                {
                    eventArgs.Cancel = true;
                }
            };
            var args = new MessageBoxEventArgs(action,
                _localization.Strings.GetString("Do you want to save your changes? This change will affect the references."),
                "",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);

            // Request MessageBox
            MessageBoxRequest?.Invoke(this, args);
        }

        private void SetupListViewColumns()
        {
            ListViewColumns = new Columns
            {
                new GridTextColumn
                {
                    MappingName = nameof(ManufacturerModel.Name),
                },
                new GridTextColumn
                {
                    MappingName = nameof(ManufacturerModel.Person)
                },
                new GridTextColumn
                {
                    MappingName = nameof(ManufacturerModel.PhoneNumber)
                },
                new GridTextColumn
                {
                    MappingName = nameof(ManufacturerModel.Fax)
                },
                new GridTextColumn
                {
                    MappingName = nameof(ManufacturerModel.Street)
                },
                new GridNumericColumn()
                {
                    MappingName = nameof(ManufacturerModel.HouseNumber),
                    NumberDecimalDigits = 0
                },
                new GridNumericColumn
                {
                    MappingName = nameof(ManufacturerModel.Plz),
                    NumberDecimalDigits = 0
                },
                new GridTextColumn
                {
                    MappingName = nameof(ManufacturerModel.City),
                },
                new GridTextColumn
                {
                    MappingName = nameof(ManufacturerModel.Country),
                }
            };
            ListViewColumns.ForEach(x => x.MinimumWidth = 50);
            UpdateColumnTranslation();
        }

        private void UpdateColumnTranslation()
        {
            ListViewColumns.SingleOrDefault(x => x.MappingName == nameof(ManufacturerModel.Name)).HeaderText =
                _localization.Strings.GetParticularString("Manufacturer", "Name");
            ListViewColumns.SingleOrDefault(x => x.MappingName == nameof(ManufacturerModel.Person)).HeaderText =
                _localization.Strings.GetParticularString("Manufacturer", "Contact Person");
            ListViewColumns.SingleOrDefault(x => x.MappingName == nameof(ManufacturerModel.PhoneNumber)).HeaderText =
                _localization.Strings.GetParticularString("Manufacturer", "Phone number");
            ListViewColumns.SingleOrDefault(x => x.MappingName == nameof(ManufacturerModel.Street)).HeaderText =
                _localization.Strings.GetParticularString("Manufacturer", "Street");
            ListViewColumns.SingleOrDefault(x => x.MappingName == nameof(ManufacturerModel.HouseNumber)).HeaderText =
                _localization.Strings.GetParticularString("Manufacturer", "House number");
            ListViewColumns.SingleOrDefault(x => x.MappingName == nameof(ManufacturerModel.Plz)).HeaderText =
                _localization.Strings.GetParticularString("Manufacturer", "Zip/Postal Code");
            ListViewColumns.SingleOrDefault(x => x.MappingName == nameof(ManufacturerModel.City)).HeaderText =
                _localization.Strings.GetParticularString("Manufacturer", "City");
            ListViewColumns.SingleOrDefault(x => x.MappingName == nameof(ManufacturerModel.Country)).HeaderText =
                _localization.Strings.GetParticularString("Manufacturer", "Country");
        }
        #endregion

		public void registerEventHandler(EventHandler<MessageBoxEventArgs> handler)
		{
			MessageBoxRequest += handler;
		}


        public ManufacturerViewModel(IStartUp startUp, IManufacturerUseCase manufacturerUseCase, ISaveColumnsUseCase saveColumnsUseCase, ILocalizationWrapper localization)
		{
            _startUp = startUp;
			_manufacturerUseCase = manufacturerUseCase;
			_saveColumnsUseCase = saveColumnsUseCase;
			_localization = localization;
            localization.Subscribe(this);
            _manufacturers = new ObservableCollection<ManufacturerModel>();
			Manufacturers = new ListCollectionView(_manufacturers);
			Manufacturers.SortDescriptions.Add(new SortDescription(nameof(ManufacturerModel.Name), ListSortDirection.Ascending));
			Manufacturers.IsLiveSorting = true;

			_selectedManufacturers = new ObservableCollection<ManufacturerModel>();
			SelectedManufacturersCollectionView = new ListCollectionView(_selectedManufacturers);
			SelectedManufacturersCollectionView.SortDescriptions.Add(new SortDescription(nameof(ManufacturerModel.Name), ListSortDirection.Ascending));
			SelectedManufacturersCollectionView.IsLiveSorting = true;

            _referencedToolModels = new ObservableCollection<ToolModelReferenceLink>();
            ReferencedToolModels = new ListCollectionView(_referencedToolModels);

			SetupListViewColumns();

			// Initalize Commands
			AddManufacturerCommand = new RelayCommand(AddManufacturerExecute, AddManufacturerCanExecute);
			RemoveManufacturer = new RelayCommand(RemoveManufacturerExecute, RemoveManufacturerCanExecute);
			SelectionChanged = new RelayCommand(SelectionChangedExecute, (obj) => true);
			SaveCommand = new RelayCommand(SaveExecute, SaveCanExecute);
			LoadedCommand = new RelayCommand(LoadedExecute, LoadedCanExeute);
            LoadReferencedToolModelsCommand = new RelayCommand(LoadReferencedToolModelsExecute, LoadReferencedToolModelsCanExecute);
		}
    }
}