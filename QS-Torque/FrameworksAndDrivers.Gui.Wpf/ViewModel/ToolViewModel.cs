using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Gui.Wpf.View.Dialogs;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Core;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;
using ToolModel = Core.Entities.ToolModel;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class ToolViewModel
        : BindableBase
        , IToolGui
        , IToolModelGui
        , IHelperTableGui<Status>
        , IHelperTableReadOnlyErrorGui<Status>
        , IHelperTableGui<CostCenter>
        , IHelperTableReadOnlyErrorGui<CostCenter>
        , IHelperTableGui<ConfigurableField>
        , IHelperTableReadOnlyErrorGui<ConfigurableField>
        , IHelperTableGui<ToolType>
        , IHelperTableReadOnlyErrorGui<ToolType>
        , IClearShownChanges
        , ICanClose
    {
        #region Events
        public EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler ClearShownChanges;
        public event EventHandler<Window> ShowDialogRequest;
        public event EventHandler<InterfaceAdapters.Models.ToolModel> SelectionRequest;
        public event EventHandler<VerifyChangesEventArgs> RequestVerifyChangesView;
        public event EventHandler<ReferenceList> ReferencesDialogRequest;
        #endregion

        #region Properties
        private IToolUseCase _useCase;
        private Dispatcher _guiDispatcher;
        private ILocalizationWrapper _localization;
        private IStartUp _startUp;

        private ToolId _idOfSelectedToolAfterLoadTools;

        private InterfaceAdapters.Models.ToolModel _toolBeforeChange;
        private InterfaceAdapters.Models.ToolModel _selectedTool;
        public InterfaceAdapters.Models.ToolModel SelectedTool
        {
            get => _selectedTool;
            set
            {
                if (_toolBeforeChange != null && _selectedTool != null)
                {
                    if (_selectedTool == value)
                    {
                        return;
                    }
                    if (!_selectedTool.EqualsByContent(_toolBeforeChange))
                    {
                        var diff = new ToolDiff(null, _toolBeforeChange.Entity,
                            _selectedTool.Entity);
                        var result = VerifyLocationDiff(diff);

                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                _startUp.RaiseShowLoadingControl(true);
                                _useCase.UpdateTool(diff);
                                break;
                            case MessageBoxResult.No:
                                SelectedTool.UpdateWith(_toolBeforeChange.Entity);
                                break;
                            case MessageBoxResult.Cancel:
                                SelectionRequest?.Invoke(this, _selectedTool);
                                return;
                        }
                    }
                }

                Set(ref _selectedTool, value);
                if (value != null)
                {
                    _startUp.RaiseShowLoadingControl(true, 3);
                    _useCase.LoadCommentForTool(_selectedTool.Entity);
                    _useCase.LoadPictureForTool(_selectedTool.Entity);
                    _useCase.ToolModelUseCase?.LoadCmCmk();
                    _toolBeforeChange = _selectedTool.CopyDeep();
                }
                CommandManager.InvalidateRequerySuggested();
                ClearShownChanges?.Invoke(this, null);
            }
        }

        public ObservableCollection<InterfaceAdapters.Models.ToolModel> AllToolModels { get; private set; }
        public ObservableCollection<ToolModelModel> AllToolModelModels { get; private set; }

        // HelperTableCollectionViews

        public ObservableCollection<ToolModelModel> ToolModelModelsCollectionView { get; private set; }

        public ObservableCollection<HelperTableItemModel<Status, string>> ToolStatusCollectionView { get; private set; }

        public ObservableCollection<HelperTableItemModel<CostCenter, string>> CostCentersCollectionView { get; private set; }

        public ObservableCollection<HelperTableItemModel<ConfigurableField, string>> ConfigurableFieldsCollectionView { get; private set; }

        public ObservableCollection<HelperTableItemModel<ToolType, string>> ToolTypesCollectionView { get; private set; }
        
        private ObservableCollection<CmCmkModel> _cmCmkTuples;
        private IToolDisplayFormatter _displayFormatter;

        public ObservableCollection<CmCmkModel> CmCmkTuples
        {
            get => _cmCmkTuples;
            set => Set(ref _cmCmkTuples, value);
        }

        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand AddToolCommand { get; private set; }
        public RelayCommand RemoveToolsCommand { get; private set; }
        public RelayCommand SaveToolCommand { get; private set; }
        #endregion

        public ToolViewModel(IToolUseCase toolUseCase, ILocalizationWrapper localization, IToolDisplayFormatter displayFormatter, IStartUp startUp)
        {
            _useCase = toolUseCase;
            _localization = localization;
            _startUp = startUp;
            _displayFormatter = displayFormatter;

            AllToolModels = new ObservableCollection<InterfaceAdapters.Models.ToolModel>();
            AllToolModelModels = new ObservableCollection<ToolModelModel>();
            
            ToolModelModelsCollectionView = new ObservableCollection<ToolModelModel>();

            ToolStatusCollectionView = new ObservableCollection<HelperTableItemModel<Status, string>>();

            CostCentersCollectionView = new ObservableCollection<HelperTableItemModel<CostCenter, string>>();

            ConfigurableFieldsCollectionView = new ObservableCollection<HelperTableItemModel<ConfigurableField, string>>();

            ToolTypesCollectionView = new ObservableCollection<HelperTableItemModel<ToolType, string>>();

            _cmCmkTuples = new ObservableCollection<CmCmkModel>();

            LoadedCommand = new RelayCommand(ExecuteLoaded, CanExecuteLoaded);
            AddToolCommand = new RelayCommand(AddToolExecute, AddToolCanExecute);
            RemoveToolsCommand = new RelayCommand(ExecuteRemoveTools, CanExecuteRemove);
            SaveToolCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
        }

        private bool CanExecuteSave(object arg)
        {
            if (SelectedTool == null)
            {
                return false;
            }

            return !_toolBeforeChange.EqualsByContent(SelectedTool);
        }

        private void ExecuteSave(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);
            var diff = new ToolDiff(null, _toolBeforeChange.Entity, _selectedTool.Entity);

            var result = VerifyLocationDiff(diff);

            if (result == MessageBoxResult.Yes)
            {
                _useCase.UpdateTool(diff);
            }
            if (result == MessageBoxResult.No)
            {
                SelectedTool.UpdateWith(_toolBeforeChange.Entity);
                ClearShownChanges?.Invoke(this, null);
            }
            if (result != MessageBoxResult.Yes)
            {
                _startUp.RaiseShowLoadingControl(false);
            }
        }

        #region Commands
        private bool CanExecuteLoaded(object arg)
        {
            return true;
        }
        private void ExecuteLoaded(object arg)
        {
            LoadItems();
        }

        private bool AddToolCanExecute(object arg) { return true; }
        private void AddToolExecute(object obj)
        {
            var previousSelectedTool = SelectedTool;
            SelectedTool = null;
            if (SelectedTool != null)
            {
                return;
            }

            AssistentView assistent;

            _startUp.RaiseShowLoadingControl(true);

            if (previousSelectedTool == null)
            {
                assistent = _startUp.OpenAddToolAssistent();
            }
            else
            {
                assistent = _startUp.OpenAddToolAssistent(previousSelectedTool.Entity);
            }

            assistent.EndOfAssistent += (s, e) =>
            {
                _startUp.RaiseShowLoadingControl(true);
                var tool = (Tool)(assistent.DataContext as AssistentViewModel)?.FillResultObject(new Tool());
                if (tool != null)
                {
                    tool.Id = new ToolId(0);
                    _useCase.AddTool(tool);
                }

                previousSelectedTool = null;
            };
            assistent.Closed += (s, e) =>
            {
                _startUp.RaiseShowLoadingControl(false);
                SelectedTool = previousSelectedTool;
            };

            ShowDialogRequest?.Invoke(this, assistent);
        }

        private bool CanExecuteRemove(object arg)
        {
            return SelectedTool != null;
        }

        private void ExecuteRemoveTools(object arg)
        {
            Action<MessageBoxResult> resultAction = (r) =>
            {
                if (r != MessageBoxResult.Yes)
                {
                    _startUp.RaiseShowLoadingControl(false);
                    return;
                }

                SelectedTool?.UpdateWith(_toolBeforeChange?.Entity);

                _useCase.RemoveTool(SelectedTool?.Entity, this);
            };

            var args = new MessageBoxEventArgs(resultAction,
                _localization.Strings.GetString("Do you really want to remove this item?"),
                _localization.Strings.GetParticularString("Window Title", "Warning"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            _startUp.RaiseShowLoadingControl(true);
            MessageBoxRequest?.Invoke(this, args);
        }

        #endregion

        #region Interface Implementations
        // IToolModelGui
        public void ShowTools(List<Tool> tools)
        {
            if (tools == null)
            {
                return;
            }

            _guiDispatcher.Invoke(() =>
            {
                foreach (var tool in tools)
                {
                    var toolModel = InterfaceAdapters.Models.ToolModel.GetModelFor(tool, _localization);

                    if (AllToolModels.Where(x => x.Entity.EqualsById(tool)).ToList().Count == 0)
                    {
                        AllToolModels.Add(toolModel);
                    }
                }

                if (_idOfSelectedToolAfterLoadTools != null && _idOfSelectedToolAfterLoadTools.ToLong() != SelectedTool?.Id)
                {
                    var newSelectedTool = AllToolModels.FirstOrDefault(x => x.Id == _idOfSelectedToolAfterLoadTools.ToLong());

                    if (newSelectedTool != null)
                    {
                        SelectedTool = newSelectedTool;
                        SelectionRequest?.Invoke(this, newSelectedTool);
                    }

                    _idOfSelectedToolAfterLoadTools = null;
                }

                if (SelectedTool != null)
                {
                    SelectionRequest?.Invoke(this, SelectedTool);
                }
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void AddTool(Tool newTool)
        {
            if (newTool == null)
            {
                return;
            }

            _guiDispatcher.Invoke(() =>
            {
                _idOfSelectedToolAfterLoadTools = newTool.Id;
                _useCase.LoadToolsForModel(newTool.ToolModel);
            });
        }

        public void ShowToolErrorMessage()
        {
            var args = new MessageBoxEventArgs(r => { },
                                               _localization.Strings.GetParticularString("ToolViewModel", "An error occured in the tool"),
                                               "",
                                               MessageBoxButton.OK,
                                               MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void ShowCommentForTool(Tool tool, string comment)
        {
            _guiDispatcher.Invoke(() =>
            {
                var toolModel = AllToolModels.FirstOrDefault(x => x.Entity.EqualsById(tool));

                if (!(toolModel is null))
                {
                    toolModel.Comment = comment;
                    if (_toolBeforeChange != null)
                    {
                        _toolBeforeChange.Comment = comment;
                    }
                }
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowCommentForToolError()
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

        public void ShowPictureForTool(long toolId, Picture picture)
        {
            _guiDispatcher.Invoke(() =>
            {
                var tool = AllToolModels.FirstOrDefault(x => x.Id == toolId);

                if (tool == null)
                {
                    return;
                }

                tool.Picture = PictureModel.GetModelFor(picture);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowModelsWithAtLeastOneTool(List<ToolModel> models)
        {
            _guiDispatcher.Invoke(() =>
            {
                AllToolModelModels.Clear();
                models.ForEach(x => AllToolModelModels.Add(ToolModelModel.GetModelFor(x, _localization)));
            });
            _startUp.RaiseShowLoadingControl(false);
        }

        public void ShowRemoveToolErrorMessage()
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

        public void RemoveTool(Tool tool)
        {
            _guiDispatcher.Invoke(() =>
            {
                foreach (var toolModel in AllToolModels)
                {
                    if (toolModel.Entity.EqualsById(tool))
                    {
                        AllToolModels.Remove(toolModel);
                        break;
                    }
                }
                _startUp.RaiseShowLoadingControl(false);
            });

        }

        public MessageBoxResult VerifyLocationDiff(ToolDiff diff)
        {
            var changes = GetChangesFromDiff(diff).ToList();

            if (changes.Count == 0)
            {
                return MessageBoxResult.OK;
            }
            var args = new VerifyChangesEventArgs(changes);
            RequestVerifyChangesView?.Invoke(this, args);
            diff.Comment = new HistoryComment(args.Comment);
            return args.Result;
        }

        public bool IsInventoryOrSerialNumberUnique(string oldSerialNumber, string newSerialNumber, string oldInventoryNumber, string newInventoryNumber)
        {
            if (oldSerialNumber != newSerialNumber)
            {
                if (!_useCase.IsSerialNumberUnique(newSerialNumber))
                {
                    return false;
                }
            }

            if (oldInventoryNumber != newInventoryNumber)
            {
                if (!_useCase.IsInventoryNumberUnique(newInventoryNumber))
                {
                    return false;
                }
            }

            return true;
        }

        public void UpdateTool(Tool updateTool)
        {
            _guiDispatcher.Invoke(() =>
            {
                var oldTool = AllToolModels.FirstOrDefault(x => x.Entity.EqualsById(updateTool));
                var newTool = InterfaceAdapters.Models.ToolModel.GetModelFor(updateTool, _localization);
                oldTool?.UpdateWith(newTool.Entity);

                if (oldTool?.EqualsById(_toolBeforeChange) ?? false)
                {
                    _toolBeforeChange = oldTool?.CopyDeep();
                }

                ClearShownChanges?.Invoke(this, null);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowEntryAlreadyExistsMessage(Tool diffNewTool)
        {
            // Intentionally empty
        }

        public void ToolAlreadyExists()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("ToolViewModel", "A tool with this serial number or inventory number already exists"),
                messageBoxImage: MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void ShowRemoveToolPreventingReferences(List<LocationToolAssignmentReferenceLink> references)
        {
            _guiDispatcher.Invoke(() =>
            {
                var referenceList = new ReferenceList
                {
                    Header = _localization.Strings.GetParticularString("HelperTableViewModel", "Location tool assignments"),
                    References = references.Select(x => x.DisplayName).ToList()
                };
                ReferencesDialogRequest?.Invoke(this, referenceList);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        private IEnumerable<SingleValueChangeModel> GetChangesFromDiff(ToolDiff diff)
        {
            string entity = _displayFormatter.Format(diff.NewTool);

            if (diff.OldTool?.InventoryNumber?.ToDefaultString() != diff.NewTool?.InventoryNumber?.ToDefaultString())
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolAttribute", "Inventory number"),
                    OldValue = diff.OldTool?.InventoryNumber?.ToDefaultString(),
                    NewValue = diff.NewTool?.InventoryNumber?.ToDefaultString()
                });
            }

            if (diff.OldTool?.SerialNumber?.ToDefaultString() != diff.NewTool?.SerialNumber?.ToDefaultString())
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolAttribute", "Serial number"),
                    OldValue = diff.OldTool?.SerialNumber?.ToDefaultString(),
                    NewValue = diff.NewTool.SerialNumber?.ToDefaultString()
                });
            }

            if (!diff.OldTool?.ToolModel?.EqualsById(diff.NewTool?.ToolModel) ?? diff.NewTool == null)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolAttribute", "Tool model"),
                    OldValue = diff.OldTool?.ToolModel?.Description?.ToDefaultString(),
                    NewValue = diff.NewTool?.ToolModel?.Description?.ToDefaultString()
                });
            }

            if ((diff.OldTool?.Status == null && diff.NewTool?.Status != null)
                || (!diff.OldTool?.Status?.EqualsById(diff.NewTool?.Status) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolAttribute", "Status"),
                    OldValue = diff.OldTool?.Status?.Value?.ToDefaultString(),
                    NewValue = diff.NewTool?.Status?.Value?.ToDefaultString()
                });
            }

            if ((diff.OldTool?.CostCenter == null && diff.NewTool?.CostCenter != null)
                || (!diff.OldTool?.CostCenter?.EqualsById(diff.NewTool?.CostCenter) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolAttribute", "Cost center"),
                    OldValue = diff.OldTool?.CostCenter?.Value?.ToDefaultString(),
                    NewValue = diff.NewTool?.CostCenter?.Value?.ToDefaultString()
                });
            }

            if ((diff.OldTool?.ConfigurableField == null && diff.NewTool?.ConfigurableField != null)
                || (!diff.OldTool?.ConfigurableField?.EqualsById(diff.NewTool?.ConfigurableField) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolAttribute", "Configurable field"),
                    OldValue = diff.OldTool?.ConfigurableField?.Value?.ToDefaultString(),
                    NewValue = diff.NewTool?.ConfigurableField?.Value?.ToDefaultString()
                });
            }

            if ((diff?.OldTool?.Accessory == null && diff.NewTool?.Accessory != null)
                || (!diff.OldTool?.Accessory?.Equals(diff.NewTool?.Accessory) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolAttribute", "Accessory"),
                    OldValue = diff.OldTool?.Accessory,
                    NewValue = diff.NewTool?.Accessory
                });
            }

            if (diff.OldTool?.Comment != diff.NewTool?.Comment)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolAttribute", "Comment"),
                    OldValue = diff.OldTool?.Comment,
                    NewValue = diff.NewTool?.Comment
                });
            }

            if ((diff.OldTool?.AdditionalConfigurableField1 == null && diff.NewTool?.AdditionalConfigurableField1 != null)
                || (!diff.OldTool?.AdditionalConfigurableField1?.Equals(diff.NewTool?.AdditionalConfigurableField1) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolAttribute", "Additional configurable Field1"),
                    OldValue = diff.OldTool.AdditionalConfigurableField1.ToDefaultString(),
                    NewValue = diff.NewTool.AdditionalConfigurableField1.ToDefaultString()
                });
            }

            if ((diff.OldTool?.AdditionalConfigurableField2 == null && diff.NewTool?.AdditionalConfigurableField2 != null)
                || (!diff.OldTool?.AdditionalConfigurableField2?.Equals(diff.NewTool?.AdditionalConfigurableField2) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolAttribute", "Additional configurable Field2"),
                    OldValue = diff.OldTool.AdditionalConfigurableField2.ToDefaultString(),
                    NewValue = diff.NewTool.AdditionalConfigurableField2.ToDefaultString()
                });
            }

            if ((diff.OldTool?.AdditionalConfigurableField3 == null && diff.NewTool?.AdditionalConfigurableField3 != null)
                || (!diff.OldTool?.AdditionalConfigurableField3?.Equals(diff.NewTool?.AdditionalConfigurableField3) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ToolAttribute", "Additional configurable Field3"),
                    OldValue = diff.OldTool.AdditionalConfigurableField3.ToDefaultString(),
                    NewValue = diff.NewTool.AdditionalConfigurableField3.ToDefaultString()
                });
            }
        }

        public void ShowToolModels(List<ToolModel> toolModels)
        {
            _guiDispatcher.Invoke(() =>
            {
                var toolModel = SelectedTool;
                SelectedTool = null;
                ToolModelModelsCollectionView.Clear();
                toolModels.ForEach(x => ToolModelModelsCollectionView.Add(ToolModelModel.GetModelFor(x, _localization)));
                SelectedTool = toolModel;
            });
        }

        public void ShowLoadingErrorMessage()
        {
            var args = new MessageBoxEventArgs((r) => { },
                                               _localization?.Strings.GetParticularString("Tool Model", "An error has occured while loading the tool models") ?? "LoadingError",
                                               "",
                                               MessageBoxButton.OK,
                                               MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void SetPictureForToolModel(long toolModelId, Picture picture)
        {
            _guiDispatcher.Invoke(() =>
            {
                if (_selectedTool?.ToolModelModel?.Id == toolModelId)
                {
                    _selectedTool.ToolModelModel.Picture = PictureModel.GetModelFor(picture);
                }

                if (_toolBeforeChange?.ToolModelModel?.Id == toolModelId)
                {
                    _toolBeforeChange.ToolModelModel.Picture = PictureModel.GetModelFor(picture);
                }
            });
        }

        public void ShowRemoveToolModelsErrorMessage()
        {
            // Intentionally empty
        }

        public void RemoveToolModels(List<ToolModel> toolModels)
        {
            _guiDispatcher.Invoke(() =>
            {
                foreach (var tm in toolModels)
                {
                    foreach (var tmm in ToolModelModelsCollectionView)
                    {
                        if (tmm.Entity.EqualsById(tm))
                        {
                            ToolModelModelsCollectionView.Remove(tmm);
                            break;
                        }
                    }
                }
            });
        }

        public void ShowCmCmk(double cm, double cmk)
        {
            _guiDispatcher.Invoke(
                () =>
                {
                    CmCmkTuples.Clear();
                    CmCmkTuples.Add(new CmCmkModel {Cm = cm, Cmk = cmk});
                    AllToolModels.ForEach(
                        toolModel =>
                        {
                            if (toolModel.ToolModelModel == null)
                            {
                                return;
                            }
                            toolModel.ToolModelModel.CmLimit = cm;
                            toolModel.ToolModelModel.CmkLimit = cmk;
                        });
                    if (_toolBeforeChange?.ToolModelModel is ToolModelModel model)
                    {
                        model.CmLimit = cm;
                        model.CmkLimit = cmk;
                    }
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
            _guiDispatcher.Invoke(() => { ToolModelModelsCollectionView.Add(ToolModelModel.GetModelFor(toolModel, _localization)); });
        }

        public void UpdateToolModel(ToolModel toolModel)
        {
            _guiDispatcher.Invoke(() =>
            {
                var itemToUpdate =
                    ToolModelModelsCollectionView.FirstOrDefault((item) => item.Entity.EqualsById(toolModel));
                if (itemToUpdate is null)
                {
                    return;
                }
                itemToUpdate.UpdateWith(toolModel);
            });
        }

        public void ShowEntryAlreadyExistsMessage(ToolModel toolModel)
        {
            // Intentionally empty
        }

        public bool ShowDiffDialog(ToolModelDiff diff)
        {
            // Intentionally empty
            return false;
        }

        public void ShowItems(List<Status> items)
        {
            _guiDispatcher.Invoke(() =>
            {
                ToolStatusCollectionView.Clear();
                items.ForEach(x => ToolStatusCollectionView.Add(HelperTableItemModel.GetModelForStatus(x)));
            });
        }

        public void ShowItems(List<CostCenter> items)
        {
            _guiDispatcher.Invoke(() =>
            {
                CostCentersCollectionView.Clear();
                items.ForEach(x => CostCentersCollectionView.Add(HelperTableItemModel.GetModelForCostCenter(x)));
            });
        }

        public void ShowItems(List<ConfigurableField> items)
        {
            _guiDispatcher.Invoke(() =>
            {
                ConfigurableFieldsCollectionView.Clear();
                items.ForEach(
                    x => ConfigurableFieldsCollectionView.Add(HelperTableItemModel.GetModelForConfigurableField(x)));
            });
        }

        public void ShowItems(List<ToolType> items)
        {
            _guiDispatcher.Invoke(() =>
            {
                ToolTypesCollectionView.Clear();
                items.ForEach(
                    x => ToolTypesCollectionView.Add(HelperTableItemModel.GetModelForToolType(x)));
            });
        }

        public void ShowErrorMessage()
        {
            // Intentionally empty
        }

        public void ShowRemoveToolModelPreventingReferences(List<ToolReferenceLink> references)
        {
            // Intentionally empty
        }

        public void Add(ToolType newItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                ToolTypesCollectionView.Add(HelperTableItemModel.GetModelForToolType(newItem));
            });
        }

        public void Remove(ToolType removeItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var models = ToolTypesCollectionView.Where(x => x.Entity.EqualsById(removeItem)).ToList();

                foreach (var m in models)
                {
                    ToolTypesCollectionView.Remove(m);
                }
            });
        }

        public void Save(ToolType savedItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var itemToUpdate =
                    ToolTypesCollectionView.FirstOrDefault((item) => item.Entity.EqualsById(savedItem));
                if (itemToUpdate is null)
                {
                    return;
                }
                itemToUpdate.Value = savedItem.Value.ToDefaultString();
            });
        }

        public void Add(ConfigurableField newItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                ConfigurableFieldsCollectionView.Add(HelperTableItemModel.GetModelForConfigurableField(newItem));
            });
        }

        public void Remove(ConfigurableField removeItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var models = ConfigurableFieldsCollectionView.Where(x => x.Entity.EqualsById(removeItem)).ToList();

                foreach (var m in models)
                {
                    ConfigurableFieldsCollectionView.Remove(m);
                }
            });
        }

        public void Save(ConfigurableField savedItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var itemToUpdate =
                    ConfigurableFieldsCollectionView.FirstOrDefault((item) => item.Entity.EqualsById(savedItem));
                if (itemToUpdate is null)
                {
                    return;
                }
                itemToUpdate.Value = savedItem.Value.ToDefaultString();
            });
        }

        public void Add(CostCenter newItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                CostCentersCollectionView.Add(HelperTableItemModel.GetModelForCostCenter(newItem));
            });
        }

        public void Remove(CostCenter removeItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var models = CostCentersCollectionView.Where(x => x.Entity.EqualsById(removeItem)).ToList();
                foreach (var m in models)
                {
                    CostCentersCollectionView.Remove(m);
                }
            });
        }

        public void Save(CostCenter savedItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var itemToUpdate =
                    CostCentersCollectionView.FirstOrDefault((item) => item.Entity.EqualsById(savedItem));
                if (itemToUpdate is null)
                {
                    return;
                }

                itemToUpdate.Value = savedItem.Value.ToDefaultString();
            });
        }

        public void Add(Status newItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                ToolStatusCollectionView.Add(HelperTableItemModel.GetModelForStatus(newItem));
            });
        }

        public void Remove(Status removeItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var models = ToolStatusCollectionView.Where(x => x.Entity.EqualsById(removeItem)).ToList();
                foreach (var m in models)
                {
                    ToolStatusCollectionView.Remove(m);
                }
            });
        }

        public void Save(Status savedItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var itemToUpdate =
                    ToolStatusCollectionView.FirstOrDefault((item) => item.Entity.EqualsById(savedItem));
                if (itemToUpdate is null)
                {
                    return;
                }
                itemToUpdate.Value = savedItem.Value.ToDefaultString();
            });
        }

        #endregion

        public void SetDispatcher(Dispatcher dispatcher)
        {
            _guiDispatcher = dispatcher;
        }

        private void LoadItems()
        {
            _startUp.RaiseShowLoadingControl(true);
            _useCase?.LoadModelsWithAtLeastOneTool();
            _useCase?.ToolModelUseCase.ShowToolModels();
            _useCase?.StatusUseCase.LoadItems(this);
            _useCase?.CostCenterUseCase.LoadItems(this);
            _useCase?.ConfigurableFieldUseCase.LoadItems(this);
            _useCase?.ToolTypeUseCase.LoadItems(this);
        }

        public void ToolModelExpanded(ToolModelModel toolmodel)
        {
            _startUp.RaiseShowLoadingControl(true);
            _useCase.LoadToolsForModel(toolmodel?.Entity);
        }

        public bool CanClose()
        {
            if (_selectedTool != null && _toolBeforeChange!= null && !_selectedTool.EqualsByContent(_toolBeforeChange))
            {
                var diff = new ToolDiff(null, _toolBeforeChange.Entity, _selectedTool.Entity);

                var result = VerifyLocationDiff(diff);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        _startUp.RaiseShowLoadingControl(true);
                        _useCase.UpdateTool(diff);
                        _selectedTool = null;
                        return true;
                    case MessageBoxResult.No:
                        SelectedTool.UpdateWith(_toolBeforeChange.Entity);
                        _selectedTool = null;
                        return true;
                    case MessageBoxResult.Cancel:
                        SelectionRequest?.Invoke(this, _selectedTool);
                        return false;
                }
            }

            _selectedTool = null;
            return true;
        }
    }
}
