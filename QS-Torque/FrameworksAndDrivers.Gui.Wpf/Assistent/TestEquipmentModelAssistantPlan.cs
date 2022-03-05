using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Common.Types.Enums;
using Core.Entities;
using Core.UseCases.Communication;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Model;
using InterfaceAdapters.Communication;
using InterfaceAdapters.Localization;
using Syncfusion.Data.Extensions;

namespace FrameworksAndDrivers.Gui.Wpf.Assistent
{
    public class TestEquipmentModelAssistantPlan : ListAssistentPlan<TestEquipmentModel>, ITestEquipmentErrorGui
    {
        private bool _isInitializing;
        private readonly ITestEquipmentUseCase _useCase;
        private readonly ITestEquipmentInterface _interfaceAdapter;
        private readonly ILocalizationWrapper _localization;
        private readonly TestEquipmentModelId _defaultTestEquipmentModelId;
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public TestEquipmentType TestEquipmentType = TestEquipmentType.Wrench;

        public TestEquipmentModelAssistantPlan(ITestEquipmentUseCase useCase, ITestEquipmentInterface interfaceAdapter, 
            ILocalizationWrapper localization, ListAssistentItemModel<TestEquipmentModel> item,
            TestEquipmentModelId defaultTestEquipmentModelId = null) : base(item)
        {
            _useCase = useCase;
            _interfaceAdapter = interfaceAdapter;
            _localization = localization;
            _defaultTestEquipmentModelId = defaultTestEquipmentModelId;

            PropertyChangedEventManager.AddHandler(interfaceAdapter,
                InterfaceAdapter_TestEquipmentModelsChanged,
                nameof(TestEquipmentInterfaceAdapter.TestEquipmentModels));
            _interfaceAdapter.TestEquipmentModels.CollectionChanged += InterfaceAdapterTestEquipmentModels_CollectionChanged;
        }

        public override void Initialize()
        {
            _isInitializing = true;
            _useCase?.LoadTestEquipmentModels(this);

            base.Initialize();
        }

        public void RefillListItems()
        {
            _interfaceAdapter.TestEquipmentModels.CollectionChanged += InterfaceAdapterTestEquipmentModels_CollectionChanged;
            AssistentItem.RefillListItems(_interfaceAdapter.TestEquipmentModels.Select(x => x.Entity).Where(x => x.Type == TestEquipmentType));

            if (_isInitializing && _defaultTestEquipmentModelId != null)
            {
                // Select default value
                AssistentItem.EnteredDisplayMemberModel = AssistentItem.ItemsCollectionView.SourceCollection
                    .OfType<DisplayMemberModel<TestEquipmentModel>>()
                    .FirstOrDefault(x => x.Item.Id.Equals(_defaultTestEquipmentModelId));
                _isInitializing = false;
            }
        }


        private void InterfaceAdapter_TestEquipmentModelsChanged(object sender, PropertyChangedEventArgs e)
        {
            RefillListItems();
        }

        private void InterfaceAdapterTestEquipmentModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (TestEquipmentModelHumbleModel i in e.NewItems)
                    {
                        if (i.Entity.Type == TestEquipmentType)
                        {
                            AssistentItem.AddListItem(i.Entity);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var testEquipmentModels = AssistentItem.GetCurrentListItems();
                    var idsToRemove = e.OldItems.ToList<TestEquipmentModelHumbleModel>().Select(x => x.Entity.Id);
                    var removingItems = testEquipmentModels.Where(x => idsToRemove.FirstOrDefault(y => y.Equals(x.Id)) != null).ToList();

                    // Update ListItems
                    foreach (var removingItem in removingItems)
                    {
                        testEquipmentModels.Remove(removingItem);
                    }
                    AssistentItem.RefillListItems(testEquipmentModels);
                    break;
            }
        }

        public void ShowProblemSavingTestEquipment()
        {
            //intentionally empty
        }

        public void ShowProblemRemoveTestEquipment()
        {
            //intentionally empty
        }

        public void ShowErrorMessageLoadingTestEquipments()
        {
            //intentionally empty
        }

        public void TestEquipmentAlreadyExists()
        {
            //intentionally empty
        }

        public void TestEquipmentModelAlreadyExists()
        {
            //intentionally empty
        }

        public void ShowErrorMessageLoadingTestEquipmentModels()
        {
            var args = new MessageBoxEventArgs(r => { },
               _localization.Strings.GetParticularString("TestEquipmentModelAssistantPlan", "Some errors occurred while loading the test equipment models"),
               _localization.Strings.GetParticularString("TestEquipmentModelAssistantPlan", "Error"),
               MessageBoxButton.OK,
               MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }
    }
}
