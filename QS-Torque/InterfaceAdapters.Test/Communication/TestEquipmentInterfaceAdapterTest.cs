using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using Client.TestHelper.Factories;
using Core.Entities;
using InterfaceAdapters.Communication;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Factories;

namespace InterfaceAdapters.Test.Communication
{
    class TestEquipmentInterfaceAdapterTest
    {
        private static IEnumerable<List<TestEquipmentModel>> TestEquipmentModelDatas =
            new List<List<TestEquipmentModel>>()
            {
                new List<TestEquipmentModel>()
                {
                    CreateTestEquipmentModel.Randomized(12324),
                    CreateTestEquipmentModel.Randomized(11111)
                },
                new List<TestEquipmentModel>()
                {
                    CreateTestEquipmentModel.Randomized(324),
                    CreateTestEquipmentModel.Randomized(78788)
                }
            };

        [TestCaseSource(nameof(TestEquipmentModelDatas))]
        public void LoadTestEquipmentModelsSetTestEquipmentModels(List<TestEquipmentModel> models)
        {
            var adapter = new TestEquipmentInterfaceAdapter(new NullLocalizationWrapper());
            adapter.LoadTestEquipmentModels(models);
            CollectionAssert.AreEqual(models, adapter.TestEquipmentModels.Select(x => x.Entity).ToList());
        }

        [Test]
        public void AddTestEquipmentAddTestEquipmentToListAndSetSelectedTestEquipment()
        {
            var adapter = new TestEquipmentInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            var testEquipment = CreateTestEquipment.Randomized(43536);
            adapter.AddTestEquipment(testEquipment);
            Assert.IsTrue(adapter.TestEquipments.Any(x => x.Entity.EqualsByContent(testEquipment)));
            Assert.IsTrue(testEquipment.EqualsByContent(adapter.SelectedTestEquipment.Entity));
        }

        [Test]
        public void AddTestEquipmentCallsSelectionRequestTestEquipmentInvoke()
        {
            var adapter = new TestEquipmentInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            var testEquipment = CreateTestEquipment.Randomized(43536);
            var invoked = false;
            adapter.SelectionRequestTestEquipment += (s, e) => invoked = true;
            adapter.AddTestEquipment(testEquipment);
            Assert.IsTrue(invoked);
        }


        private static IEnumerable<(List<TestEquipmentModel>, int)>
            AddTestEquipmentUpdatesTestEquipmentModelInTestEquipmentModelsData =
                new List<(List<TestEquipmentModel>, int)>()
                {
                    (
                        new List<TestEquipmentModel>()
                        {
                            CreateTestEquipmentModel.Randomized(5464562),
                            CreateTestEquipmentModel.Randomized(3456),
                            CreateTestEquipmentModel.Randomized(23423)
                         },
                        1
                     ),
                    (
                        new List<TestEquipmentModel>()
                        {
                            CreateTestEquipmentModel.Randomized(234),
                            CreateTestEquipmentModel.Randomized(556),
                            CreateTestEquipmentModel.Randomized(254353423),
                            CreateTestEquipmentModel.Randomized(456)
                        },
                        3
                    )
                };

        [TestCaseSource(nameof(AddTestEquipmentUpdatesTestEquipmentModelInTestEquipmentModelsData))]
        public void AddTestEquipmentUpdatesTestEquipmentModelInTestEquipmentModels((List<TestEquipmentModel> testEquipmentModels, int index) data)
        {
            var adapter = new TestEquipmentInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            adapter.LoadTestEquipmentModels(data.testEquipmentModels);

            var testEquipment = CreateTestEquipment.Randomized(111);
            testEquipment.TestEquipmentModel = CreateTestEquipmentModel.Randomized(3345);
            testEquipment.TestEquipmentModel.Id = new TestEquipmentModelId(data.testEquipmentModels[data.index].Id.ToLong());
            adapter.AddTestEquipment(testEquipment);

            Assert.IsTrue(adapter.TestEquipmentModels.Select(x => x.Entity).Contains(testEquipment.TestEquipmentModel));
        }

        private static IEnumerable<(List<Core.Entities.TestEquipmentModel>, List<Core.Entities.TestEquipment>)> ShowTestEquipmentsAddsTestEquipmentsData =
            new List<(List<Core.Entities.TestEquipmentModel>, List<Core.Entities.TestEquipment>)>()
            {
                (
                    new List<TestEquipmentModel>()
                    {
                        CreateTestEquipmentModel.Randomized(12324),
                        CreateTestEquipmentModel.Randomized(11111)
                    },
                    new List<TestEquipment>()
                    {
                        CreateTestEquipment.Randomized(756757),
                        CreateTestEquipment.Randomized(3245677),
                    }
                )
            };

        [TestCaseSource(nameof(ShowTestEquipmentsAddsTestEquipmentsData))]
        public void ShowTestEquipmentsAddsAdapterTestEquipmentsAndModels((List<Core.Entities.TestEquipmentModel> models, List<Core.Entities.TestEquipment> testEquipments) data)
        {
            foreach (var model in data.models)
            {
                model.TestEquipments = data.testEquipments;
            }

            var adapter = new TestEquipmentInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            adapter.ShowTestEquipments(data.models);

            var comparer = new Func<TestEquipmentModel, TestEquipmentModel, bool>((dto, entity) =>
                entity.EqualsByContent(dto)
            );
            
            CheckerFunctions.CollectionAssertAreEquivalent(data.models, adapter.TestEquipmentModels.Select(x => x.Entity).ToList(), comparer);

            var comparerTestEquipment = new Func<Core.Entities.TestEquipment, Core.Entities.TestEquipment, bool>((dto, entity) =>
                entity.Equals(dto)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.testEquipments, adapter.TestEquipments.GroupBy(x => x.Entity).Select(x => x.Key).ToList(), comparerTestEquipment);
        }

        [Test]
        public void ShowTestEquipmentsCallsShowLoadingControlRequest()
        {
            var adapter = new TestEquipmentInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            bool? requestArgs = null;
            adapter.ShowLoadingControlRequest += (s, e) => requestArgs = e;
            adapter.ShowTestEquipments(new List<TestEquipmentModel>());
            Assert.IsFalse(requestArgs);
        }

        [Test]
        public void UpdateTestEquipmentCallsShowLoadingControlRequest()
        {
            var adapter = new TestEquipmentInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            bool? requestArgs = null;
            adapter.ShowLoadingControlRequest += (s, e) => requestArgs = e;
            adapter.UpdateTestEquipment(CreateTestEquipment.Randomized(24));
            Assert.IsFalse(requestArgs);
        }

        [Test]
        public void UpdateTestEquipmentModelCallsShowLoadingControlRequest()
        {
            var adapter = new TestEquipmentInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            bool? requestArgs = null;
            adapter.ShowLoadingControlRequest += (s, e) => requestArgs = e;
            adapter.UpdateTestEquipmentModel(CreateTestEquipmentModel.Randomized(24));
            Assert.IsFalse(requestArgs);
        }

        [Test]
        public void CreateTestEquipmentFillsDataGateVersions()
        {
            var adapter = new TestEquipmentInterfaceAdapter(new NullLocalizationWrapper());
            Assert.IsTrue(adapter.DataGateVersions.Count == 8);
            for (var i = 0; i < 8; i++)
            {
                Assert.AreEqual(i, adapter.DataGateVersions[i].DataGateVersionsId);
            }
        }

        [Test]
        public void RemoveTestEquipmentCallsShowLoadingControlRequest()
        {
            var adapter = new TestEquipmentInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            bool? requestArgs = null;
            adapter.ShowLoadingControlRequest += (s, e) => requestArgs = e;
            adapter.RemoveTestEquipment(CreateTestEquipment.Randomized(24));
            Assert.IsFalse(requestArgs);
        }

        private static IEnumerable<(List<TestEquipmentHumbleModel>, TestEquipment)>
            RemoveTestEquipmentRemovesTestEquipmentFromTestEquipmentsData = new List<(List<TestEquipmentHumbleModel>, TestEquipment)>()
            {
                (
                    new List<TestEquipmentHumbleModel>()
                    {
                        TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.RandomizedWithId(435,1), null),
                        TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.RandomizedWithId(4563545,2), null),
                        TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.RandomizedWithId(67788,3), null),
                        TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.RandomizedWithId(234532,4), null),
                    },
                    CreateTestEquipment.RandomizedWithId(3243536, 4)
                ),
                (
                    new List<TestEquipmentHumbleModel>()
                    {
                        TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.RandomizedWithId(456,1), null),
                        TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.RandomizedWithId(2342,2), null),
                        TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.RandomizedWithId(234,3), null),
                        TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.RandomizedWithId(34,4), null),
                    },
                    CreateTestEquipment.RandomizedWithId(3243536, 1)
                ),
            };

        [TestCaseSource(nameof(RemoveTestEquipmentRemovesTestEquipmentFromTestEquipmentsData))]
        public void RemoveTestEquipmentRemovesTestEquipmentFromTestEquipments((List<TestEquipmentHumbleModel> collection, TestEquipment testEquipment) data)
        {
            var adapter = new TestEquipmentInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);

            foreach (var testEquipmentHumble in data.collection)
            {
                adapter.TestEquipments.Add(testEquipmentHumble);
            }

            var testEquipmentCount = adapter.TestEquipments.ToList().Count - 1;

            adapter.RemoveTestEquipment(data.testEquipment);

            Assert.AreEqual(testEquipmentCount, adapter.TestEquipments.ToList().Count);
            Assert.AreEqual(0, adapter.TestEquipments.Where(x => x.Entity.Id.ToLong() == data.testEquipment.Id.ToLong()).ToList().Count);
        }
    }
}
