using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.Test.ViewModels;
using InterfaceAdapters.Models;
using NUnit.Framework;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Assistent
{
    class TestLevelSetAssistantPlanTest
    {
        [Test]
        public void InitializeCallsUseCase()
        {
            var tuple = CreateTestLevelSetAssistantPlanTuple();
            tuple.assistantPlan.Initialize();
            Assert.IsTrue(tuple.useCase.LoadTestLevelSetsCalled);
            Assert.AreSame(tuple.assistantPlan, tuple.useCase.LoadTestLevelSetsErrorHandler);
        }

        [Test]
        public void ShowTestLevelSetErrorCallsMessageBoxRequest()
        {
            var tuple = CreateTestLevelSetAssistantPlanTuple();
            var invoked = false;
            tuple.assistantPlan.MessageBoxRequest += (s, e) => invoked = true;
            tuple.assistantPlan.ShowTestLevelSetError();
            Assert.IsTrue(invoked);
        }

        [TestCase()]
        public void ChangeTestLevelSetsRefillsItems()
        {
            var tuple = CreateTestLevelSetAssistantPlanTuple();
            Assert.IsFalse(tuple.interfaceAdapter.TestLevelSets.Any());
            tuple.interfaceAdapter.TestLevelSets = new ObservableCollection<TestLevelSetModel>(new List<TestLevelSetModel>()
            {
                new TestLevelSetModel(new TestLevelSet()) { Id = 1 },
                new TestLevelSetModel(new TestLevelSet()) { Id = 2 },
                new TestLevelSetModel(new TestLevelSet()) { Id = 3 }
            });

            Assert.AreEqual(3, (tuple.assistantItem.ItemsCollectionView.SourceCollection as ObservableCollection<DisplayMemberModel<TestLevelSet>>).Count);
            Assert.AreEqual(1, (tuple.assistantItem.ItemsCollectionView.SourceCollection as ObservableCollection<DisplayMemberModel<TestLevelSet>>)[0].Item.Id.ToLong());
            Assert.AreEqual(2, (tuple.assistantItem.ItemsCollectionView.SourceCollection as ObservableCollection<DisplayMemberModel<TestLevelSet>>)[1].Item.Id.ToLong());
            Assert.AreEqual(3, (tuple.assistantItem.ItemsCollectionView.SourceCollection as ObservableCollection<DisplayMemberModel<TestLevelSet>>)[2].Item.Id.ToLong());
        }

        [TestCase(2)]
        [TestCase(5)]
        public void AddTestLevelSetTest(long id)
        {
            var tuple = CreateTestLevelSetAssistantPlanTuple();

            tuple.interfaceAdapter.TestLevelSets = new ObservableCollection<TestLevelSetModel>(new List<TestLevelSetModel>()
            {
                new TestLevelSetModel(new TestLevelSet()) { Id = 3 }
            });

            tuple.interfaceAdapter.TestLevelSets.Add(new TestLevelSetModel(new TestLevelSet()) { Id = id });

            Assert.AreEqual(2, (tuple.assistantItem.ItemsCollectionView.SourceCollection as ObservableCollection<DisplayMemberModel<TestLevelSet>>).Count);
            Assert.AreEqual(id, (tuple.assistantItem.ItemsCollectionView.SourceCollection as ObservableCollection<DisplayMemberModel<TestLevelSet>>)[1].Item.Id.ToLong());
        }

        [TestCase(1)]
        [TestCase(2)]
        public void RemoveToolModelTest(int index)
        {
            var tuple = CreateTestLevelSetAssistantPlanTuple();

            var list = new List<TestLevelSetModel>()
            {
                new TestLevelSetModel(new TestLevelSet()) { Id = 3 },
                new TestLevelSetModel(new TestLevelSet()) { Id = 5 },
                new TestLevelSetModel(new TestLevelSet()) { Id = 1 }
            };
            var id = list[index].Id;
            tuple.interfaceAdapter.TestLevelSets = new ObservableCollection<TestLevelSetModel>(list);

            tuple.interfaceAdapter.TestLevelSets.Remove(list[index]);

            Assert.AreEqual(2, (tuple.assistantItem.ItemsCollectionView.SourceCollection as ObservableCollection<DisplayMemberModel<TestLevelSet>>).Count);
            Assert.IsFalse((tuple.assistantItem.ItemsCollectionView.SourceCollection as ObservableCollection<DisplayMemberModel<TestLevelSet>>).Any(x => x.Item.Id.ToLong() == id));
        }



        private static (TestLevelSetAssistantPlan assistantPlan, TestLevelSetUseCaseMock useCase, TestLevelSetInterfaceMock interfaceAdapter, ListAssistentItemModel<TestLevelSet> assistantItem) CreateTestLevelSetAssistantPlanTuple()
        {
            var useCase = new TestLevelSetUseCaseMock();
            var interfaceAdapter = new TestLevelSetInterfaceMock();
            interfaceAdapter.TestLevelSets = new ObservableCollection<TestLevelSetModel>();
            var assistantItem = new ListAssistentItemModel<TestLevelSet>(Dispatcher.CurrentDispatcher, new List<TestLevelSet>(), "", "", null, (o, i) => { }, null, s => "", () => { });
            var assistantPlan = new TestLevelSetAssistantPlan(useCase, interfaceAdapter, new NullLocalizationWrapper(), assistantItem, null);
            return (assistantPlan, useCase, interfaceAdapter, assistantItem);
        }
    }
}
