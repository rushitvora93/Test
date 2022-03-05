using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using Common.Types.Enums;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;

namespace InterfaceAdapters
{
    public interface IExtensionInterface : INotifyPropertyChanged
    {
        ExtensionModel SelectedExtension { get; set; }
        ExtensionModel SelectedExtensionWithoutChanges { get; set; }
        ObservableCollection<ExtensionModel> Extensions { get; }
        ObservableCollection<LocationReferenceLink> ReferencedLocations { get; }
        void SetDispatcher(Dispatcher dispatcher);

        event EventHandler<bool> ShowLoadingControlRequest;
    }

    public class ExtensionInterfaceAdapter : BindableBase, IExtensionGui, IExtensionInterface, IGetUpdatedByLanguageChanges
    {
        private readonly ILocalizationWrapper _localization;
        private Dispatcher _guiDispatcher;

        public ExtensionInterfaceAdapter(ILocalizationWrapper localization)
        {
            _localization = localization;
            _localization.Subscribe(this);
        }

        private ExtensionModel _selectedExtension;
        public ExtensionModel SelectedExtension
        {
            get => _selectedExtension;
            set
            {
                _selectedExtension = value;
                RaisePropertyChanged();
                SelectedExtensionWithoutChanges = _selectedExtension?.CopyDeep();
            }
        }

        private ExtensionModel _selectedExtensionWithoutChanges;
        public ExtensionModel SelectedExtensionWithoutChanges
        {
            get => _selectedExtensionWithoutChanges;
            set
            {
                _selectedExtensionWithoutChanges = value;
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

        public void SetDispatcher(Dispatcher dispatcher)
        {
            _guiDispatcher = dispatcher;
        }

        private void SetInventoryNumberForStandardExtension(ObservableCollection<ExtensionModel> extensions)
        {
            var standardExtension = extensions.SingleOrDefault(x => x.Id == (long)SpecialDbIds.NoEntrySelected);
            if (standardExtension != null)
            {
                standardExtension.InventoryNumber = _localization.Strings.GetParticularString("Extension", "No Extension");
            }
        }

        public void ShowExtensions(List<Extension> extensions)
        {
            _guiDispatcher.Invoke(() =>
            {
                Extensions = new ObservableCollection<ExtensionModel>(extensions.Select(x => ExtensionModel.GetModelFor(x, _localization)));
                SetInventoryNumberForStandardExtension(Extensions);
                ShowLoadingControlRequest?.Invoke(this, false);
                RaisePropertyChanged(nameof(Extensions));
            });
        }

        private ObservableCollection<LocationReferenceLink> _referencedLocations = new ObservableCollection<LocationReferenceLink>();
        public ObservableCollection<LocationReferenceLink> ReferencedLocations
        {
            get => _referencedLocations;
            set
            {
                _referencedLocations = value;
                RaisePropertyChanged();
            }
        }

        public void ShowReferencedLocations(List<LocationReferenceLink> locationReferenceLinks)
        {
            _guiDispatcher.Invoke(() =>
            {
                ReferencedLocations.Clear();
                foreach (var locationRef in locationReferenceLinks)
                {
                    ReferencedLocations.Add(locationRef);
                }
                ShowLoadingControlRequest?.Invoke(this, false);
            });
        }

        public void AddExtension(Extension addedExtension)
        {
            _guiDispatcher.Invoke(() =>
            {
                var extensionModel = ExtensionModel.GetModelFor(addedExtension, _localization);
                Extensions.Add(extensionModel);
                SelectedExtension = extensionModel;
            });
        }

        public void UpdateExtension(Extension updatedExtension)
        {
            _guiDispatcher.Invoke(() =>
            {
                foreach (var item in Extensions)
                {
                    if (item.Entity.EqualsById(updatedExtension))
                    {
                        item.UpdateWith(updatedExtension);
                    }
                }

                if (SelectedExtension?.Entity.EqualsById(updatedExtension) ?? false)
                {
                    SelectedExtension.UpdateWith(updatedExtension);
                    SelectedExtensionWithoutChanges = SelectedExtension?.CopyDeep();
                }
            });
        }

        public void RemoveExtension(Extension removedExtension)
        {
            _guiDispatcher.Invoke(() =>
            {
                foreach (var extension in Extensions)
                {
                    if (!extension.Entity.EqualsById(removedExtension))
                        continue;

                    Extensions.Remove(extension);
                    break;
                }
                ShowLoadingControlRequest?.Invoke(this, false);
            });
        }


        public event EventHandler<bool> ShowLoadingControlRequest;
        public void LanguageUpdate()
        {
            SetInventoryNumberForStandardExtension(Extensions);
        }
    }
}
