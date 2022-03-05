using Client.UseCases.UseCases;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Model;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class LocationHistoryViewModel : BindableBase, IHistoryErrorHandler
    {
        private IStartUp _startUp;
        private ILocalizationWrapper _localization;
        private IHistoryUseCase _historyUseCase;
        private IHistoryInterface _historyInterface;
        private ILocationUseCase _locationUseCase;
        private ILocationInterface _locationInterface;

        public LocationTreeModel LocationTree { get => _locationInterface.LocationTree; }

        private LocationModel _selectedLocation;
        public LocationModel SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                RaisePropertyChanged();
                _historyUseCase.LoadLocationHistoryFor(value.Entity.Id, _localization.Strings, this);
                _startUp.RaiseShowLoadingControl(true);
            }
        }

        private LocationDirectoryHumbleModel _selectedDirectory;
        public LocationDirectoryHumbleModel SelectedDirectory
        {
            get => _selectedDirectory;
            set
            {
                _selectedDirectory = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ValueChangesContainerModel> LocationChanges { get => _historyInterface.LocationChanges; }

        public RelayCommand LoadedCommand { get; private set; }

        public event EventHandler InitializeLocationTreeRequest;
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;


        public LocationHistoryViewModel(IHistoryUseCase historyUseCase, IHistoryInterface historyInterfaceAdapter, ILocationUseCase locationUseCase, ILocationInterface locationInterfaceAdapter, ILocalizationWrapper localization, IStartUp startUp)
        {
            _historyUseCase = historyUseCase;
            _historyInterface = historyInterfaceAdapter;
            _locationUseCase = locationUseCase;
            _locationInterface = locationInterfaceAdapter;
            _localization = localization;
            _startUp = startUp;

            WireViewModelToInterfaceAdapter();

            LoadedCommand = new RelayCommand(LoadedExecute, LoadedCanExecute);
        }

        private void WireViewModelToInterfaceAdapter()
        {
            PropertyChangedEventManager.AddHandler(
                _locationInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(LocationTree));
                    InitializeLocationTreeRequest?.Invoke(this, null);
                    _startUp.RaiseShowLoadingControl(false);
                },
                nameof(ILocationInterface.LocationTree));
            PropertyChangedEventManager.AddHandler(
                _historyInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(LocationChanges));
                    _startUp.RaiseShowLoadingControl(false);
                },
                nameof(IHistoryInterface.LocationChanges));
        }

        public void SetGuiDospatcher(Dispatcher guiDispatcher)
        {
            _locationInterface.SetGuiDispatcher(guiDispatcher);
        }
        private bool LoadedCanExecute(object arg) { return true; }
        private void LoadedExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);
            _locationUseCase.LoadTree(_locationInterface as LocationInterfaceAdapter);
        }

        public void LocationHistoryError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Location history", "Some errors occurred while loading the locations history"),
                _localization.Strings.GetParticularString("Location history", "Warning"),
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
        }
    }
}
