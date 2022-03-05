using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.Test.ViewModels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using Client.TestHelper.Factories;
using Common.Types.Enums;
using InterfaceAdapters.Communication;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Assistent
{
    class TestEquipmentModelAssistantPlanTest
    {
        [Test]
        public void InitializeCallsUseCase()
        {
            var environment = new Environment();
            environment.AssistantPlan.Initialize();
            Assert.AreSame(environment.AssistantPlan, environment.Mock.UseCase.LoadTestEquipmentModelsParameter);
        }

        [Test]
        public void ShowErrorMessageLoadingTestEquipmentModelsCallsMessageBoxRequest()
        {
            var environment = new Environment();
            var invoked = false;
            environment.AssistantPlan.MessageBoxRequest += (s, e) => invoked = true;
            environment.AssistantPlan.ShowErrorMessageLoadingTestEquipmentModels();
            Assert.IsTrue(invoked);
        }

        static IEnumerable<(ObservableCollection<TestEquipmentModelHumbleModel>, TestEquipmentType)> TestEquipmentModelDatas = new List<(ObservableCollection<TestEquipmentModelHumbleModel>, TestEquipmentType)>()
        {
            (
                new ObservableCollection<TestEquipmentModelHumbleModel>()
                {
                    TestEquipmentModelHumbleModel.GetModelFor(CreateTestEquipmentModel.RandomizedWithTestEquipmentType(325354, TestEquipmentType.Bench), null),
                    TestEquipmentModelHumbleModel.GetModelFor(CreateTestEquipmentModel.RandomizedWithTestEquipmentType(456456, TestEquipmentType.AcqTool), null),
                    TestEquipmentModelHumbleModel.GetModelFor(CreateTestEquipmentModel.RandomizedWithTestEquipmentType(45678, TestEquipmentType.Bench), null)
                },
                TestEquipmentType.Bench
            ),
            (
                new ObservableCollection<TestEquipmentModelHumbleModel>()
                {
                    TestEquipmentModelHumbleModel.GetModelFor(CreateTestEquipmentModel.RandomizedWithTestEquipmentType(435, TestEquipmentType.AcqTool), null),
                    TestEquipmentModelHumbleModel.GetModelFor(CreateTestEquipmentModel.RandomizedWithTestEquipmentType(789, TestEquipmentType.AcqTool), null),
                    TestEquipmentModelHumbleModel.GetModelFor(CreateTestEquipmentModel.RandomizedWithTestEquipmentType(333, TestEquipmentType.ManualInput), null),
                    TestEquipmentModelHumbleModel.GetModelFor(CreateTestEquipmentModel.RandomizedWithTestEquipmentType(7899, TestEquipmentType.Analyse), null)
                },
                TestEquipmentType.AcqTool
            )
        };

        [TestCaseSource(nameof(TestEquipmentModelDatas))]
        public void TestEquipmentModelAssistantShowsOnlyModelsWithAssistantPlanTestEquipmentTypeWhenSetNewTestEquipmentModels((ObservableCollection<TestEquipmentModelHumbleModel> testEquipmentModels, TestEquipmentType type) data)
        {
            var environment = new Environment();
            environment.AssistantPlan.TestEquipmentType = data.type;

            Assert.IsFalse(environment.Mock.InterfaceAdapter.TestEquipmentModels.Any());

            environment.Mock.InterfaceAdapter.TestEquipmentModels = data.testEquipmentModels;

            var expected = environment.Mock.InterfaceAdapter.TestEquipmentModels.Select(x => x.Entity).Where(x => x.Type == data.type).ToList();

            var assistantItemCollection =
                (environment.Mock.AssistantItem.ItemsCollectionView.SourceCollection as
                    ObservableCollection<DisplayMemberModel<TestEquipmentModel>>).Select(x => x.Item).ToList();

            CollectionAssert.AreEqual(expected, assistantItemCollection);
        }

        [TestCaseSource(nameof(TestEquipmentModelDatas))]
        public void TestEquipmentModelAssistantShowsOnlyModelsWithAssistantPlanTestEquipmentTypeWhenAddNewTestEquipmentModels((ObservableCollection<TestEquipmentModelHumbleModel> testEquipmentModels, TestEquipmentType type) data)
        {
            var environment = new Environment();
            environment.AssistantPlan.TestEquipmentType = data.type;

            var collection = new List<TestEquipmentModelHumbleModel>()
            {
                TestEquipmentModelHumbleModel.GetModelFor(CreateTestEquipmentModel.RandomizedWithTestEquipmentType(325354, data.type), null),
                TestEquipmentModelHumbleModel.GetModelFor(CreateTestEquipmentModel.RandomizedWithTestEquipmentType(456456, data.type), null),
            };

            environment.Mock.InterfaceAdapter.TestEquipmentModels =
                new ObservableCollection<TestEquipmentModelHumbleModel>(collection);

            foreach (var model in data.testEquipmentModels)
            {
                environment.Mock.InterfaceAdapter.TestEquipmentModels.Add(model);
                collection.Add(model);
            }

            var assistantItemCollection =
                (environment.Mock.AssistantItem.ItemsCollectionView.SourceCollection as
                    ObservableCollection<DisplayMemberModel<TestEquipmentModel>>).Select(x => x.Item).ToList();


            var expected = collection.Where(x => x.TestEquipmentType.Type == data.type).Select(x => x.Entity).ToList();
            CollectionAssert.AreEqual(expected, assistantItemCollection);
        }

        [TestCase(1, TestEquipmentType.Wrench)]
        [TestCase(2, TestEquipmentType.Analyse)]
        public void RemoveTestEquipmentModelWithAvailableTypeRemovesItemFromList(int index, TestEquipmentType type)
        {
            var environment = new Environment();
            environment.AssistantPlan.TestEquipmentType = type;

            var collection = new List<TestEquipmentModelHumbleModel>()
            {
                TestEquipmentModelHumbleModel.GetModelFor(CreateTestEquipmentModel.RandomizedWithTestEquipmentType(325354, type), null),
                TestEquipmentModelHumbleModel.GetModelFor(CreateTestEquipmentModel.RandomizedWithTestEquipmentType(435, type), null),
                TestEquipmentModelHumbleModel.GetModelFor(CreateTestEquipmentModel.RandomizedWithTestEquipmentType(56231, type), null),
                TestEquipmentModelHumbleModel.GetModelFor(CreateTestEquipmentModel.RandomizedWithTestEquipmentType(2234,type), null)
            };

            var removedModel = collection[index];
            environment.Mock.InterfaceAdapter.TestEquipmentModels = new ObservableCollection<TestEquipmentModelHumbleModel>(collection);

            environment.Mock.InterfaceAdapter.TestEquipmentModels.Remove(collection[index]);

            var assistantItemCollection =
                (environment.Mock.AssistantItem.ItemsCollectionView.SourceCollection as
                    ObservableCollection<DisplayMemberModel<TestEquipmentModel>>).Select(x => x.Item).ToList();

            Assert.AreEqual(collection.Count - 1, assistantItemCollection.Count);
            Assert.IsFalse(assistantItemCollection.Any(x => x.EqualsByContent(removedModel.Entity)));
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    UseCase = new TransferToTestEquipmentViewModelTest.TestEquipmentUseCaseMock();
                    InterfaceAdapter = new TransferToTestEquipmentViewModelTest.TestEquipmentInterfaceAdapterMock();
                    InterfaceAdapter.TestEquipmentModels = new System.Collections.ObjectModel.ObservableCollection<InterfaceAdapters.Communication.TestEquipmentModelHumbleModel>();
                    DefaultTestEquipmentModelId = new TestEquipmentModelId(1);
                    AssistantItem = new ListAssistentItemModel<TestEquipmentModel>(Dispatcher.CurrentDispatcher, new List<TestEquipmentModel>(), "", "", null, (o, i) => { }, null, s => "", () => { });

                }

                public readonly TransferToTestEquipmentViewModelTest.TestEquipmentUseCaseMock UseCase;
                public readonly TransferToTestEquipmentViewModelTest.TestEquipmentInterfaceAdapterMock InterfaceAdapter;
                public readonly ListAssistentItemModel<TestEquipmentModel> AssistantItem;
                public readonly TestEquipmentModelId DefaultTestEquipmentModelId;
            }

            public Environment()
            {
                Mock = new Mocks();
                AssistantPlan = new TestEquipmentModelAssistantPlan(Mock.UseCase, Mock.InterfaceAdapter, new NullLocalizationWrapper(), Mock.AssistantItem, Mock.DefaultTestEquipmentModelId);
            }

            public readonly Mocks Mock;
            public readonly TestEquipmentModelAssistantPlan AssistantPlan;
        }
    }
}
