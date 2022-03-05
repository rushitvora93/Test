using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Client.Core.Diffs;
using Client.Core.Validator;
using Common.Types.Enums;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Gui.Wpf.View.Dialogs;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;
using Syncfusion.Data.Extensions;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class ExtensionViewModel : 
        BindableBase, 
        IExtensionErrorGui, 
        ICanClose, 
        IExtensionSaveGuiShower,
        IExtensionDependencyGui,
        IDisposable
    {
        private readonly IExtensionUseCase _extensionUseCase;
        private readonly ILocalizationWrapper _localization;
        private readonly IExtensionValidator _extensionValidator;
        private readonly IStartUp _startUp;
        private readonly IExtensionInterface _extensionInterface;
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler<ICanShowDialog> ShowDialogRequest;
        public event EventHandler<VerifyChangesEventArgs> RequestVerifyChangesView;
        public event EventHandler<ReferenceList> ReferencesDialogRequest;

        public ExtensionViewModel(IStartUp starUp, IExtensionUseCase extensionUseCase, 
            IExtensionInterface extensionInterface, ILocalizationWrapper localization,
            IExtensionValidator extensionValidator,
            IFilteredObservableCollectionExtension<ExtensionModel> extensions = null)
        {
            _startUp = starUp;
            _extensionUseCase = extensionUseCase;
            _extensionInterface = extensionInterface;
            _localization = localization;
            _extensionValidator = extensionValidator;


            Extensions = extensions ?? new FilteredObservableCollectionExtension<ExtensionModel>(_extensionInterface.Extensions)
            {
                Filter = extensionModel => extensionModel.Id != (long) SpecialDbIds.NoEntrySelected
            };


            WireViewModelToExtensionInterface();

            _extensionInterface.ShowLoadingControlRequest += ExtensionInterface_ShowLoadingControlRequest;
            LoadedCommand = new RelayCommand(LoadedCommandExecute, LoadedCommandCanExecute);
            LoadReferencedLocationsCommand = new RelayCommand(LoadReferencedLocationsExecute, LoadReferencedLocationsCanExecute);
            AddExtensionCommand = new RelayCommand(AddExtensionExecute, AddExtensionCanExecute);
            SaveExtensionCommand = new RelayCommand(SaveExtensionExecute, SaveExtensionCanExecute);
            RemoveExtensionCommand = new RelayCommand(RemoveExtensionExecute, RemoveExtensionCanExecute);
        }


        public bool IsListViewVisible => SelectedExtension != null;

        private void WireViewModelToExtensionInterface()
        {
            PropertyChangedEventManager.AddHandler(
                _extensionInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(SelectedExtension));
                    RaisePropertyChanged(nameof(IsListViewVisible));
                    CommandManager.InvalidateRequerySuggested();
                },
                nameof(ExtensionInterfaceAdapter.SelectedExtension));


            PropertyChangedEventManager.AddHandler(
                _extensionInterface,
                (s, e) =>
                {
                    Extensions.SetNewSourceCollection(_extensionInterface.Extensions);
                    RaisePropertyChanged(nameof(Extensions));
                    CommandManager.InvalidateRequerySuggested();
                },
                nameof(ExtensionInterfaceAdapter.Extensions));


            PropertyChangedEventManager.AddHandler(
                _extensionInterface,
                (s, e) => RaisePropertyChanged(nameof(SelectedExtensionWithoutChanges)),
                nameof(ExtensionInterfaceAdapter.SelectedExtensionWithoutChanges));
        }


        public void SetDispatcher(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            _extensionInterface.SetDispatcher(dispatcher);
        }
        private void ExtensionInterface_ShowLoadingControlRequest(object sender, bool e)
        {
            _startUp.RaiseShowLoadingControl(e);
        }

        public RelayCommand LoadedCommand { get; set; }

        private bool LoadedCommandCanExecute(object arg)
        {
            return true;
        }

        private void LoadedCommandExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);
            _extensionUseCase.ShowExtensions(this);
        }

        public RelayCommand AddExtensionCommand { get; set; }

        private bool AddExtensionCanExecute(object arg)
        {
            return true;
        }

        private void AddExtensionExecute(object obj)
        {
            var previousExtension = SelectedExtension;
            SelectedExtension = null;
            if (SelectedExtension != null)
            {
                return;
            }
            SelectedExtension = previousExtension;

            var assistant = _startUp.OpenAddExtensionAssistant(SelectedExtension?.Entity);

            assistant.EndOfAssistent += (s, e) =>
            {
                var extension = (Extension)(assistant.DataContext as AssistentViewModel)?.FillResultObject(new Extension());
                if (extension == null)
                    return;

                extension.Id = new ExtensionId(0);
                _extensionUseCase.AddExtension(extension, this);

            };
            assistant.Closed += (s, e) =>
            {
                _startUp.RaiseShowLoadingControl(false);
            };

            _startUp.RaiseShowLoadingControl(true);
            ShowDialogRequest?.Invoke(this, assistant);
        }


        public RelayCommand LoadReferencedLocationsCommand { get; private set; }

        private bool LoadReferencedLocationsCanExecute(object arg)
        {
            return _extensionInterface.SelectedExtension != null;
        }

        private void LoadReferencedLocationsExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);
            _extensionUseCase.ShowReferencedLocations(this, new ExtensionId(_extensionInterface.SelectedExtension.Id));
        }

        public RelayCommand SaveExtensionCommand { get; set; }

        private bool SaveExtensionCanExecute(object arg)
        {
            if (_extensionInterface.SelectedExtension == null)
            {
                return false;
            }

            if (!_extensionInterface.SelectedExtension.EqualsByContent(_extensionInterface.SelectedExtensionWithoutChanges))
            {
                return _extensionValidator.Validate(_extensionInterface.SelectedExtension?.Entity);
            }

            return false;
        }

        private void SaveExtensionExecute(object obj)
        {
            if (_extensionInterface.SelectedExtension != null)
            {
                _startUp.RaiseShowLoadingControl(true);

                var diff = new ExtensionDiff(null, null,
                    _extensionInterface.SelectedExtensionWithoutChanges.Entity,
                    _extensionInterface.SelectedExtension.Entity);

                _extensionUseCase.SaveExtension(diff, this, this);
            }
        }

        public RelayCommand RemoveExtensionCommand { get; set; }
        private bool RemoveExtensionCanExecute(object arg)
        {
            return SelectedExtension != null;
        }

        private void RemoveExtensionExecute(object obj)
        {
            Action<MessageBoxResult> resultAction = (r) =>
            {
                if (r != MessageBoxResult.Yes)
                {
                    _startUp.RaiseShowLoadingControl(false);
                    return;
                }

                SelectedExtension.UpdateWith(_extensionInterface.SelectedExtensionWithoutChanges?.Entity);
                _extensionUseCase.RemoveExtension(SelectedExtension.Entity, this, this);
            };

            var args = new MessageBoxEventArgs(resultAction,
                _localization.Strings.GetString("Do you really want to remove this item?"),
                _localization.Strings.GetParticularString("Window Title", "Warning"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            _startUp.RaiseShowLoadingControl(true);
            MessageBoxRequest?.Invoke(this, args);
        }


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

        public ObservableCollection<LocationReferenceLink> ReferencedLocations => _extensionInterface.ReferencedLocations;

        private bool _areLocationReferencesShown;
        private Dispatcher _dispatcher;

        public bool AreLocationReferencesShown
        {
            get => _areLocationReferencesShown;
            set
            {
                Set(ref _areLocationReferencesShown, value);

                if (!value)
                {
                    ReferencedLocations?.Clear();
                }
            }
        }

        public void ShowErrorMessageLoadingExentsions()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("ExtensionViewModel", "Some errors occurred while loading extensions"),
                _localization.Strings.GetString("Unknown Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowErrorMessageLoadingReferencedLocations()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("ExtensionViewModel", "Some errors occurred while loading location references for extension"),
                _localization.Strings.GetString("Unknown Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowProblemSavingExtension()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("ExtensionViewModel", "Some errors occurred while adding extension"),
                _localization.Strings.GetString("Unknown Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ExtensionAlreadyExists()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("ExtensionViewModel", "A extenison with this inventory number already exists"),
                messageBoxImage: MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowProblemRemoveExtension()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("ExtensionViewModel", "Some errors occurred while removing extension"),
                _localization.Strings.GetString("Unknown Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public ExtensionModel SelectedExtensionWithoutChanges => _extensionInterface.SelectedExtensionWithoutChanges;
        public ExtensionModel SelectedExtension
        {
            get => _extensionInterface.SelectedExtension;
            set
            {
                if (_extensionInterface.SelectedExtension == value)
                {
                    return;
                }

                if (!_extensionValidator.Validate(SelectedExtension?.Entity))
                {
                    var continueEditing = true;
                    MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs(r =>
                        {
                            if (r != MessageBoxResult.No)
                                return;

                            SelectedExtension.UpdateWith(SelectedExtensionWithoutChanges?.Entity);
                            continueEditing = false;
                        },
                        _localization.Strings.GetParticularString("ExtensionViewModel",
                            "The extension is not valid, do you want to continue editing? (If not, the extension is reseted to the last saved value)"),
                        _localization.Strings.GetParticularString("ExtensionViewModel", "Extension not valid"),
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error));

                    if (continueEditing)
                    {
                        return;
                    }
                }

                if (_extensionInterface.SelectedExtension != null &&
                    _extensionInterface.SelectedExtensionWithoutChanges != null &&
                    !_extensionInterface.SelectedExtension.EqualsByContent(_extensionInterface.SelectedExtensionWithoutChanges))
                {
                    var diff = new ExtensionDiff(null, null,
                        _extensionInterface.SelectedExtensionWithoutChanges?.Entity,
                        SelectedExtension?.Entity);

                    var result = ShowExtensionChangesDialog(diff);
                    if (result != null)
                    {
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                _startUp.RaiseShowLoadingControl(true);
                                _extensionUseCase.UpdateExtension(diff, this);
                                break;
                            case MessageBoxResult.No:
                                SelectedExtension.UpdateWith(SelectedExtensionWithoutChanges.Entity);
                                break;
                            case MessageBoxResult.Cancel:
                                return;
                        }
                    }
                }


                _extensionInterface.SelectedExtension = value;
                AreLocationReferencesShown = false;

                RaisePropertyChanged();
            }
        }

        public bool CanClose()
        {
            if (SelectedExtension != null)
            {
                if (!_extensionValidator.Validate(SelectedExtension?.Entity))
                {
                    var continueEditing = true;
                    MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs(r =>
                    {
                        if (r != MessageBoxResult.No)
                            return;

                        _extensionInterface.SelectedExtension = null;
                        continueEditing = false;
                    },
                        _localization.Strings.GetParticularString("ExtensionViewModel",
                            "The extension is not valid, do you want to continue editing? (If not, the extension is reseted to the last saved value)"),
                        _localization.Strings.GetParticularString("ExtensionViewModel", "Extension not valid"),
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error));
                    return !continueEditing;
                }

                if (_extensionInterface.SelectedExtension != null &&
                    _extensionInterface.SelectedExtensionWithoutChanges != null &&
                    !_extensionInterface.SelectedExtension.EqualsByContent(_extensionInterface.SelectedExtensionWithoutChanges))
                {

                    var diff = new ExtensionDiff(null, null,
                        _extensionInterface.SelectedExtensionWithoutChanges?.Entity,
                        SelectedExtension?.Entity);

                    var result = ShowExtensionChangesDialog(diff);
                    if (result == null)
                    {
                        return true;
                    }

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            _startUp.RaiseShowLoadingControl(true);
                            _extensionUseCase.UpdateExtension(diff, this);
                            _extensionInterface.SelectedExtension = null;
                            return true;
                        case MessageBoxResult.No:
                            _extensionInterface.SelectedExtension = null;
                            return true;
                        case MessageBoxResult.Cancel:
                            return false;
                    }
                }

                _extensionInterface.SelectedExtension = null;
                return true;
            }

            return true;
        }
        

        public void SaveExtension(ExtensionDiff diff, Action saveAction)
        {
            var result = ShowExtensionChangesDialog(diff);
            if (result == null)
            {
                return;
            }

            _dispatcher.Invoke(() =>
            {
                if (result == MessageBoxResult.No)
                {
                    _extensionInterface.SelectedExtension = _extensionInterface.SelectedExtensionWithoutChanges?.CopyDeep();
                    RaisePropertyChanged(null);
                    _startUp.RaiseShowLoadingControl(false);
                }

                if (result == MessageBoxResult.Yes)
                {
                    saveAction();
                }

                if (result == MessageBoxResult.Cancel)
                {
                    _startUp.RaiseShowLoadingControl(false);
                }
            });
        }

        private MessageBoxResult? ShowExtensionChangesDialog(ExtensionDiff diff)
        {
            var changes = GetChangesFromExtensionDiff(diff).ToList();

            if (changes.Count == 0)
            {
                return null;
            }

            var args = new VerifyChangesEventArgs(changes);
            RequestVerifyChangesView?.Invoke(this, args);
            diff.Comment = new HistoryComment(args.Comment);
            return args.Result;
        }

        private IEnumerable<SingleValueChangeModel> GetChangesFromExtensionDiff(ExtensionDiff diff)
        {
            var entity = SelectedExtension?.InventoryNumber;

            if (!diff.OldExtension.InventoryNumber.Equals(diff.NewExtension.InventoryNumber))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ExtensionAttribute", "Inventory number"),
                    OldValue = diff.OldExtension.InventoryNumber.ToDefaultString(),
                    NewValue = diff.NewExtension.InventoryNumber.ToDefaultString()
                });
            }

            if (diff.OldExtension.Description != diff.NewExtension.Description)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ExtensionAttribute", "Description"),
                    OldValue = diff.OldExtension.Description,
                    NewValue = diff.NewExtension.Description
                });
            }

            if (diff.OldExtension.Length != diff.NewExtension.Length)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ExtensionAttribute", "Gauge"),
                    OldValue = diff.OldExtension.Length.ToString(),
                    NewValue = diff.NewExtension.Length.ToString()
                });
            }

            if (diff.OldExtension.FactorTorque != diff.NewExtension.FactorTorque)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ExtensionAttribute", "Factor"),
                    OldValue = diff.OldExtension.FactorTorque.ToString(),
                    NewValue = diff.NewExtension.FactorTorque.ToString(),
                });
            }

            if (diff.OldExtension.Bending != diff.NewExtension.Bending)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ExtensionAttribute", "Bending compensation"),
                    OldValue = diff.OldExtension.Bending.ToString(),
                    NewValue = diff.NewExtension.Bending.ToString()
                });
            }
        }

        public void ShowRemoveExtensionPreventingReferences(List<LocationReferenceLink> references)
        {
            _dispatcher.Invoke(() =>
            {
                ReferencesDialogRequest?.Invoke(this,
                    new ReferenceList
                    {
                        Header = _localization.Strings.GetParticularString("ExtensionViewModel", "Location - process control"),
                        References = references.Select(x => x.DisplayName).ToList()
                    }
                );
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void Dispose()
        {
            _extensionInterface.ShowLoadingControlRequest -= ExtensionInterface_ShowLoadingControlRequest;
            Extensions.Dispose();
        }
    }
}
