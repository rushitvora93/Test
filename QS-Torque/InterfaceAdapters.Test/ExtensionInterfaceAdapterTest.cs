using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using Client.TestHelper.Factories;
using Common.Types.Enums;
using Core.Entities;
using Core.Entities.ReferenceLink;
using InterfaceAdapters.Communication;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Factories;

namespace InterfaceAdapters.Test
{
    class ExtensionInterfaceAdapterTest
    {
        private static IEnumerable<List<Extension>> ExtensionDatas =
            new List<List<Extension>>()
            {
                new List<Extension>()
                {
                    CreateExtension.Randomized(1234),
                    CreateExtension.Randomized(12111)
                },
                new List<Extension>()
                {
                    CreateExtension.Randomized(112312),
                    CreateExtension.Randomized(12)
                }
            };

        [TestCaseSource(nameof(ExtensionDatas))]
        public void LoadExtension(List<Extension> extensions)
        {
            var adapter = new ExtensionInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            adapter.ShowExtensions(extensions);
            CollectionAssert.AreEqual(extensions, adapter.Extensions.Select(x => x.Entity).ToList());
        }

        [Test]
        public void ShowExtensionsCallsShowLoadingControlRequest()
        {
            var adapter = new ExtensionInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            bool? requestArgs = null;
            adapter.ShowLoadingControlRequest += (s, e) => requestArgs = e;
            adapter.ShowExtensions(new List<Extension>());
            Assert.IsFalse(requestArgs);
        }

        [Test]
        public void ShowReferencedLocationsCallsShowLoadingControlRequest()
        {
            var adapter = new ExtensionInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            bool? requestArgs = null;
            adapter.ShowLoadingControlRequest += (s, e) => requestArgs = e;
            adapter.ShowReferencedLocations(new List<LocationReferenceLink>());
            Assert.IsFalse(requestArgs);
        }


        private static IEnumerable<List<LocationReferenceLink>> RefLocData =
            new List<List<LocationReferenceLink>>()
            {
                new List<LocationReferenceLink>()
                {
                    new LocationReferenceLink(new QstIdentifier(1), new LocationNumber("14134"), new LocationDescription("sdfasfs"), null),
                    new LocationReferenceLink(new QstIdentifier(4), new LocationNumber("234df"), new LocationDescription("6ddfgd"), null)
                },
                new List<LocationReferenceLink>()
                {
                    new LocationReferenceLink(new QstIdentifier(1), new LocationNumber("14134"), new LocationDescription("sdfasfs"), null),
                    new LocationReferenceLink(new QstIdentifier(4), new LocationNumber("234df"), new LocationDescription("6ddfgd"), null)
                }
            };


        [TestCaseSource(nameof(RefLocData))]
        public void ShowReferencedLocations(List<LocationReferenceLink> locRefs)
        {
            var adapter = new ExtensionInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            adapter.ShowReferencedLocations(locRefs);
            CollectionAssert.AreEqual(locRefs, adapter.ReferencedLocations);
        }

        [Test]
        public void AddExtensionAddsExtensionToList()
        {
            var adapter = new ExtensionInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            var extension = CreateExtension.Randomized(324);
            adapter.AddExtension(extension);

            Assert.AreSame(extension, adapter.Extensions.Last().Entity);
        }

        [Test]
        public void AddExtensionSetsSelectedExtension()
        {
            var adapter = new ExtensionInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            var extension = CreateExtension.Randomized(324);
            adapter.AddExtension(extension);

            Assert.AreSame(extension, adapter.SelectedExtension.Entity);
        }

        [Test]
        public void UpdateExtensionsUpdateExtensionInList()
        {
            var adapter = new ExtensionInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            adapter.Extensions = new ObservableCollection<ExtensionModel>()
            {
                ExtensionModel.GetModelFor(CreateExtension.Randomized(435), new NullLocalizationWrapper()),
                ExtensionModel.GetModelFor(CreateExtension.RandomizedWithId(435, 5), new NullLocalizationWrapper()),
                ExtensionModel.GetModelFor(CreateExtension.Randomized(345), new NullLocalizationWrapper())
            };

            var extension = CreateExtension.RandomizedWithId(11, 5);
            adapter.UpdateExtension(extension);

            Assert.IsTrue(extension.EqualsByContent(adapter.Extensions[1].Entity));
        }

        [Test]
        public void UpdateExtensionsUpdatesSelectedExtension()
        {
            var adapter = new ExtensionInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            adapter.SelectedExtension = ExtensionModel.GetModelFor(CreateExtension.RandomizedWithId(435, 5), new NullLocalizationWrapper());
            var extension = CreateExtension.RandomizedWithId(33, 5);
            adapter.UpdateExtension(extension);
            Assert.IsTrue(extension.EqualsByContent(adapter.SelectedExtension.Entity));
            Assert.IsTrue(extension.EqualsByContent(adapter.SelectedExtensionWithoutChanges.Entity));
        }

        [Test]
        public void UpdateExtensionsDontUpdatesSelectedExtensionIfOtherId()
        {
            var adapter = new ExtensionInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            adapter.SelectedExtension = ExtensionModel.GetModelFor(CreateExtension.RandomizedWithId(435, 5), new NullLocalizationWrapper());
            var extension = CreateExtension.RandomizedWithId(33, 8);
            adapter.UpdateExtension(extension);
            Assert.IsFalse(extension.EqualsByContent(adapter.SelectedExtension.Entity));
            Assert.IsFalse(extension.EqualsByContent(adapter.SelectedExtensionWithoutChanges.Entity));
        }

        [Test]
        public void RemoveExtensionCallsShowLoadingControlRequest()
        {
            var adapter = new ExtensionInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            bool? requestArgs = null;
            adapter.ShowLoadingControlRequest += (s, e) => requestArgs = e;
            adapter.RemoveExtension(CreateExtension.Randomized(24));
            Assert.IsFalse(requestArgs);
        }

        private static IEnumerable<(List<ExtensionModel>, Extension)>
            RemoveExtensionRemovesExtensionFromExtensionsData = new List<(List<ExtensionModel>, Extension)>()
            {
                (
                    new List<ExtensionModel>()
                    {
                        ExtensionModel.GetModelFor(CreateExtension.RandomizedWithId(435,1), null),
                        ExtensionModel.GetModelFor(CreateExtension.RandomizedWithId(4563545,2), null),
                        ExtensionModel.GetModelFor(CreateExtension.RandomizedWithId(67788,3), null),
                        ExtensionModel.GetModelFor(CreateExtension.RandomizedWithId(234532,4), null),
                    },
                    CreateExtension.RandomizedWithId(3243536, 4)
                ),
                (
                    new List<ExtensionModel>()
                    {
                        ExtensionModel.GetModelFor(CreateExtension.RandomizedWithId(456,1), null),
                        ExtensionModel.GetModelFor(CreateExtension.RandomizedWithId(2342,2), null),
                        ExtensionModel.GetModelFor(CreateExtension.RandomizedWithId(234,3), null),
                        ExtensionModel.GetModelFor(CreateExtension.RandomizedWithId(34,4), null),
                    },
                    CreateExtension.RandomizedWithId(3243536, 1)
                ),
            };

        [TestCaseSource(nameof(RemoveExtensionRemovesExtensionFromExtensionsData))]
        public void RemoveExtensionRemovesExtensionFromExtensions((List<ExtensionModel> collection, Extension extension) data)
        {
            var adapter = new ExtensionInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);

            foreach (var extensionHumble in data.collection)
            {
                adapter.Extensions.Add(extensionHumble);
            }

            var extensionCount = adapter.Extensions.ToList().Count - 1;

            adapter.RemoveExtension(data.extension);

            Assert.AreEqual(extensionCount, adapter.Extensions.ToList().Count);
            Assert.AreEqual(0, adapter.Extensions.Where(x => x.Entity.Id.ToLong() == data.extension.Id.ToLong()).ToList().Count);
        }

        private static IEnumerable<List<(Extension, bool)>> ExtensionsSetNoExtensionEntityInvenotryNumber =
            new List<List<(Extension, bool)>>()
            {
                new List<(Extension, bool)>()
                {
                    (
                        CreateExtension.RandomizedWithIdAndInventoryNumber(5555,(long) SpecialDbIds.NoEntrySelected,"ABC"),
                        true
                    ),
                    (
                        CreateExtension.RandomizedWithIdAndInventoryNumber(334,4,"ABC"),
                        false
                    )
                },
                new List<(Extension, bool)>()
                {
                    (
                        CreateExtension.RandomizedWithIdAndInventoryNumber(234,4,"ABC"),
                        false
                    ),
                    (
                        CreateExtension.RandomizedWithIdAndInventoryNumber(1212,(long) SpecialDbIds.NoEntrySelected,"ABC"),
                        true
                    )
                }
            };

        [TestCaseSource(nameof(ExtensionsSetNoExtensionEntityInvenotryNumber))]
        public void ShowExtensionsSetsInventoryNumberOfNoExtensionEntity(List<(Extension extension, bool changeInventoryNumber)> extensionData)
        {
            var adapter = new ExtensionInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);

            adapter.ShowExtensions(extensionData.Select(x => x.extension.CopyDeep()).ToList());
            var counter = 0;
            foreach (var data in extensionData)
            {
                Assert.AreEqual(data.changeInventoryNumber,
                    data.extension.InventoryNumber.ToDefaultString() != adapter.Extensions[counter].InventoryNumber);
                counter++;
            }
        }

        [TestCaseSource(nameof(ExtensionsSetNoExtensionEntityInvenotryNumber))]
        public void LanguageUpdateSetsInventoryNumberOfNoExtensionEntity(List<(Extension extension, bool changeInventoryNumber)> extensionData)
        {
            var adapter = new ExtensionInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetDispatcher(Dispatcher.CurrentDispatcher);
            adapter.Extensions = new ObservableCollection<ExtensionModel>(extensionData.Select(x => ExtensionModel.GetModelFor(x.extension.CopyDeep(), new NullLocalizationWrapper())));

            adapter.LanguageUpdate();
            var counter = 0;
            foreach (var data in extensionData)
            {
                Assert.AreEqual(data.changeInventoryNumber,
                    data.extension.InventoryNumber.ToDefaultString() != adapter.Extensions[counter].InventoryNumber);
                counter++;
            }
        }
    }
}
