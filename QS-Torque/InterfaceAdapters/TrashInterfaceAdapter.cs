using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using System.Xml.Linq;
using Common.Types.Enums;
using Core.Entities;
using Core.UseCases;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;
using ToolModel = Core.Entities.ToolModel;

namespace InterfaceAdapters
{
    public interface ITrashInterface : INotifyPropertyChanged
    {
        LocationTreeModel LocationTree { get; }
        ObservableCollection<ExtensionModel> Extensions { get; }
        void SetGuiDispatcher(Dispatcher Dispatcher);
    }


    public class TrashInterfaceAdapter : InterfaceAdapter<ITrashGui>, ITrashGui, ITrashInterface, IGetUpdatedByLanguageChanges
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
		
        private ObservableCollection<ToolModelModel> _toolModels = new ObservableCollection<ToolModelModel>();
        public ObservableCollection<ToolModelModel> ToolModels
        {
            get => _toolModels;
            set
            {
                _toolModels = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ExtensionModel> _extensions = new ObservableCollection<ExtensionModel>();
        public ObservableCollection<ExtensionModel> Extensions
        {
            get => _extensions;
            set
            {
                _extensions = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ToolModelModel> AllToolModelModels { get; private set; }

        public TrashInterfaceAdapter(ILocalizationWrapper localization)
        {
            _localization = localization;
            _localization.Subscribe(this);
        }

        public ObservableCollection<InterfaceAdapters.Models.ToolModel> AllToolModels { get; private set; }

        public void SetGuiDispatcher(Dispatcher Dispatcher)
        {
            _guiDispatcher = Dispatcher;
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

        public void ShowLocation(Location location)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowLocation(location));
            _guiDispatcher?.Invoke(() => LocationTree.LocationModels.Add(LocationModel.GetModelFor(location, _localization, null)));
        }

        public void UpdateLocation(Location location)
        {
            InvokeActionOnGuiInterfaces(gui => gui.UpdateLocation(location));
        }

        public void RestoreLocation(Location location)
        {
            InvokeActionOnGuiInterfaces(gui => gui.RestoreLocation(location));
        }

        public void ShowRestoreLocationError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowRestoreLocationError());
        }

        public void RestoreDirectory(LocationDirectoryId selectedDirectoryId)
        {
            InvokeActionOnGuiInterfaces(gui => gui.RestoreDirectory(selectedDirectoryId));
        }

        public void ShowDeletedExtensions(List<Extension> extensions)
        {
            _guiDispatcher.Invoke(() =>
            {
                Extensions = new ObservableCollection<ExtensionModel>(extensions.Select(x => ExtensionModel.GetModelFor(x, _localization)));
                SetInventoryNumberForStandardExtension(Extensions);
                ShowLoadingControlRequest?.Invoke(this, false);
                RaisePropertyChanged(nameof(Extensions));
            });
        }

        private void SetInventoryNumberForStandardExtension(ObservableCollection<ExtensionModel> extensions)
        {
            var standardExtension = extensions.SingleOrDefault(x => x.Id == (long)SpecialDbIds.NoEntrySelected);
            if (standardExtension != null)
            {
                standardExtension.InventoryNumber = _localization.Strings.GetParticularString("Extension", "No Extension");
            }
        }

        public event EventHandler<bool> ShowLoadingControlRequest;

        public void LanguageUpdate()
        {
            SetInventoryNumberForStandardExtension(Extensions);
        }

        public void ShowDeletedModelsWithAtLeastOneTool(List<ToolModel> models)
        {
            AllToolModelModels = new ObservableCollection<ToolModelModel>();
            _guiDispatcher.Invoke(() =>
            {
                AllToolModelModels.Clear();
                models.ForEach(x => AllToolModelModels.Add(ToolModelModel.GetModelFor(x, _localization)));
            });
            ShowLoadingControlRequest?.Invoke(this, false);
        }

        public void RestoreTool(Tool tool)
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
                ShowLoadingControlRequest?.Invoke(this, false);
            });
        }

        public void RestoreExtension(Extension RestoreExtension)
        {
            _guiDispatcher.Invoke(() =>
            {
                foreach (var extension in Extensions)
                {
                    if (!extension.Entity.EqualsById(RestoreExtension))
                        continue;

                    Extensions.Remove(extension);
                    break;
                }
                ShowLoadingControlRequest?.Invoke(this, false);
            });
		}

        private void SetInventoryNumberForStandardToolModel(ObservableCollection<ToolModelModel> toolModels)
        {
            var standardToolModel = toolModels.SingleOrDefault(x => x.Id == (long)SpecialDbIds.NoEntrySelected);
            if (standardToolModel != null)
            {
                standardToolModel.Description = _localization.Strings.GetParticularString("ToolModel", "No ToolModel");
            }
        }

        public void ShowDeletedToolModels(List<Core.Entities.ToolModel> toolModels)
        {
            _guiDispatcher.Invoke(() =>
            {
                ToolModels = new ObservableCollection<InterfaceAdapters.Models.ToolModelModel>(toolModels.Select(x => InterfaceAdapters.Models.ToolModelModel.GetModelFor(x, _localization)));
                SetInventoryNumberForStandardToolModel(ToolModels);
                ShowLoadingControlRequest?.Invoke(this, false);
                RaisePropertyChanged(nameof(ToolModels));
            });
        }
    }
}
