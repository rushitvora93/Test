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
    public class TestLevelSetAssistantPlan : ListAssistentPlan<TestLevelSet>, ITestLevelSetErrorHandler
    {
        private ITestLevelSetUseCase _useCase;
        private ITestLevelSetInterface _interfaceAdapter;
        private TestLevelSetId _defaultTestLevelSetId;
        private bool _isInitializing;
        private ILocalizationWrapper _localization;

        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;


        public TestLevelSetAssistantPlan(ITestLevelSetUseCase useCase, ITestLevelSetInterface interfaceAdapter, ILocalizationWrapper localization, ListAssistentItemModel<TestLevelSet> item, TestLevelSetId defaultTestLevelSetId = null) : base(item)
        {
            _useCase = useCase;
            _interfaceAdapter = interfaceAdapter;
            _defaultTestLevelSetId = defaultTestLevelSetId;
            _localization = localization;

            PropertyChangedEventManager.AddHandler(interfaceAdapter,
                InterfaceAdapter_TestLevelSetsChanged,
                nameof(TestLevelSetInterfaceAdapter.TestLevelSets));
            _interfaceAdapter.TestLevelSets.CollectionChanged += InterfaceAdapterTestLevelSets_CollectionChanged;
        }
        
        public TestLevelSetAssistantPlan(ITestLevelSetUseCase useCase, ITestLevelSetInterface interfaceAdapter, ILocalizationWrapper localization, List<AssistentPlan> subPlans, ListAssistentItemModel<TestLevelSet> item, TestLevelSetId defaultTestLevelSetId = null) : base(subPlans, item)
        {
            _useCase = useCase;
            _interfaceAdapter = interfaceAdapter;
            _defaultTestLevelSetId = defaultTestLevelSetId;
            _localization = localization;

            PropertyChangedEventManager.AddHandler(interfaceAdapter,
                InterfaceAdapter_TestLevelSetsChanged,
                nameof(TestLevelSetInterfaceAdapter.TestLevelSets));
            _interfaceAdapter.TestLevelSets.CollectionChanged += InterfaceAdapterTestLevelSets_CollectionChanged;
        }


        public override void Initialize()
        {
            _isInitializing = true;
            _useCase?.LoadTestLevelSets(this);

            base.Initialize();
        }

        public void ShowTestLevelSetError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("TestLevelSetAssistantPlan", "Some errors occurred while loading the test level sets"),
                _localization.Strings.GetParticularString("TestLevelSetAssistantPlan", "Error"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        private void InterfaceAdapter_TestLevelSetsChanged(object sender, PropertyChangedEventArgs e)
        {
            _interfaceAdapter.TestLevelSets.CollectionChanged += InterfaceAdapterTestLevelSets_CollectionChanged;
            AssistentItem.RefillListItems(_interfaceAdapter.TestLevelSets.Select(x => x.Entity));

            if (_isInitializing && _defaultTestLevelSetId != null)
            {
                // Select default value
                AssistentItem.EnteredDisplayMemberModel = AssistentItem.ItemsCollectionView.SourceCollection.OfType<DisplayMemberModel<TestLevelSet>>().FirstOrDefault(x => x.Item.Id.Equals(_defaultTestLevelSetId));
                _isInitializing = false;
            }
        }

        private void InterfaceAdapterTestLevelSets_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (TestLevelSetModel i in e.NewItems)
                    {
                        AssistentItem.AddListItem(i.Entity); 
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var testLevelSets = AssistentItem.GetCurrentListItems();
                    var idsToRemove = e.OldItems.ToList<TestLevelSetModel>().Select(x => x.Entity.Id);
                    var removingItems = testLevelSets.Where(x => idsToRemove.FirstOrDefault(y => y.Equals(x.Id)) != null).ToList();

                    // Update ListItems
                    foreach (var removingItem in removingItems)
                    {
                        testLevelSets.Remove(removingItem);
                    }
                    AssistentItem.RefillListItems(testLevelSets);
                    break;
            }
        }
    }
}
