using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Model;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;
using Syncfusion.Data.Extensions;

namespace FrameworksAndDrivers.Gui.Wpf.Assistent
{
    public class ExtensionAssistantPlan : ListAssistentPlan<Extension>, IExtensionErrorGui
    {
        private readonly IExtensionUseCase _useCase;
        private readonly IExtensionInterface _extensionInterface;
        private readonly ILocalizationWrapper _localization;
        private readonly ExtensionId _extensionId;
        private bool _isInitializing;
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;

        public ExtensionAssistantPlan(IExtensionUseCase useCase, IExtensionInterface extensionInterface,ILocalizationWrapper localization, ListAssistentItemModel<Extension> item, ExtensionId defaultExtensionId = null) : base(item)
        {
            _useCase = useCase;
            _extensionInterface = extensionInterface;
            _localization = localization;
            _extensionId = defaultExtensionId;

            PropertyChangedEventManager.AddHandler(_extensionInterface,
                InterfaceAdapter_ExtensionsChanged,
                nameof(ExtensionInterfaceAdapter.Extensions));
            _extensionInterface.Extensions.CollectionChanged += InterfaceAdapterExtensions_CollectionChanged;
        }

        public ExtensionAssistantPlan(IExtensionUseCase useCase, ILocalizationWrapper localization, List<AssistentPlan> subPlans, ListAssistentItemModel<Extension> item, ExtensionId defaultExtensionId = null) : base(subPlans, item)
        {
            _useCase = useCase;
            _localization = localization;
            _extensionId = defaultExtensionId;

            PropertyChangedEventManager.AddHandler(_extensionInterface,
                InterfaceAdapter_ExtensionsChanged,
                nameof(ExtensionInterfaceAdapter.Extensions));
            _extensionInterface.Extensions.CollectionChanged += InterfaceAdapterExtensions_CollectionChanged;
        }

        #region Overrides
        public override void Initialize()
        {
            _isInitializing = true;
            _useCase?.ShowExtensions(this);

            base.Initialize();
        }
        #endregion

        private void InterfaceAdapter_ExtensionsChanged(object sender, PropertyChangedEventArgs e)
        {
            _extensionInterface.Extensions.CollectionChanged += InterfaceAdapterExtensions_CollectionChanged;
            AssistentItem.RefillListItems(_extensionInterface.Extensions.Select(x => x.Entity));

            if (_isInitializing && _extensionId != null)
            {
                // Select default value
                AssistentItem.EnteredDisplayMemberModel = AssistentItem.ItemsCollectionView.SourceCollection.OfType<DisplayMemberModel<Extension>>().FirstOrDefault(x => x.Item.Id.Equals(_extensionId));
                _isInitializing = false;
            }
        }

        private void InterfaceAdapterExtensions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (ExtensionModel i in e.NewItems)
                    {
                        AssistentItem.AddListItem(i.Entity);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var extensions = AssistentItem.GetCurrentListItems();
                    var idsToRemove = e.OldItems.ToList<ExtensionModel>().Select(x => x.Entity.Id);
                    var removingItems = extensions.Where(x => idsToRemove.FirstOrDefault(y => y.Equals(x.Id)) != null).ToList();

                    // Update ListItems
                    foreach (var removingItem in removingItems)
                    {
                        extensions.Remove(removingItem);
                    }
                    AssistentItem.RefillListItems(extensions);
                    break;
            }
        }

        public void ShowErrorMessageLoadingExentsions()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("ExtensionAssistantPlan", "Some errors occurred while loading the extensions"),
                _localization.Strings.GetParticularString("ExtensionAssistantPlan", "Error"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowErrorMessageLoadingReferencedLocations()
        {
            //Intentiaonaly empty
        }

        public void ShowProblemSavingExtension()
        {
            //Intentiaonaly empty
        }

        public void ExtensionAlreadyExists()
        {
            //Intentiaonaly empty
        }

        public void ShowProblemRemoveExtension()
        {
            //Intentiaonaly empty
        }
    }
}
