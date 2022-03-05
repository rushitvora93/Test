using Core.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapters.Test
{
    class LocationInterfaceAdapterTest
    {
        [Test]
        public void ShowLocationTreeFillsLocationTreeDirectories()
        {
            var adapter = new LocationInterfaceAdapter(new NullLocalizationWrapper());
            var list = new List<LocationDirectory>() { new LocationDirectory(), new LocationDirectory() };
            adapter.ShowLocationTree(list);
            Assert.AreSame(list[0], adapter.LocationTree.LocationDirectoryModels[0].Entity);
            Assert.AreSame(list[1], adapter.LocationTree.LocationDirectoryModels[1].Entity);
        }

        [Test]
        public void AddLocationUpdatesLocationTree()
        {
            var adapter = new LocationInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetGuiDispatcher(System.Windows.Threading.Dispatcher.CurrentDispatcher);
            var entity = new Location();
            adapter.AddLocation(entity);
            Assert.IsTrue(adapter.LocationTree.LocationModels.Any(x => x.Entity == entity));
        }

        [TestCase(5)]
        [TestCase(3)]
        public void RemoveLocationUpdatesLocationTree(long id)
        {
            var adapter = new LocationInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetGuiDispatcher(System.Windows.Threading.Dispatcher.CurrentDispatcher);
            var entity = new Location() { Id = new LocationId(id) };
            adapter.LocationTree.LocationModels.Add(new InterfaceAdapters.Models.LocationModel(entity, new NullLocalizationWrapper(), null));
            adapter.RemoveLocation(entity);
            Assert.IsFalse(adapter.LocationTree.LocationModels.Any(x => x.Entity == entity));
        }

        [Test]
        public void AddLocationDirectoryUpdatesLocationTree()
        {
            var adapter = new LocationInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetGuiDispatcher(System.Windows.Threading.Dispatcher.CurrentDispatcher);
            var entity = new LocationDirectory();
            adapter.AddLocationDirectory(entity);
            Assert.IsTrue(adapter.LocationTree.LocationDirectoryModels.Any(x => x.Entity == entity));
        }

        [TestCase(5)]
        [TestCase(3)]
        public void RemoveDirectoryUpdatesLocationTree(long id)
        {
            var adapter = new LocationInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetGuiDispatcher(System.Windows.Threading.Dispatcher.CurrentDispatcher);
            var entity = new LocationDirectory() { Id = new LocationDirectoryId(id) };
            adapter.LocationTree.LocationDirectoryModels.Add(new InterfaceAdapters.Models.LocationDirectoryHumbleModel(entity));
            adapter.RemoveDirectory(entity.Id);
            Assert.IsFalse(adapter.LocationTree.LocationDirectoryModels.Any(x => x.Entity == entity));
        }

        [TestCase(5, 10)]
        [TestCase(3, 11)]
        public void ChangeLocationParentUpdatesLocationTree(long id, long newParentId)
        {
            var adapter = new LocationInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetGuiDispatcher(System.Windows.Threading.Dispatcher.CurrentDispatcher);
            var entity = new Location() { Id = new LocationId(id) };
            adapter.LocationTree.LocationModels.Add(new InterfaceAdapters.Models.LocationModel(entity, new NullLocalizationWrapper(), null));
            adapter.ChangeLocationParent(entity, new LocationDirectoryId(newParentId));
            Assert.AreEqual(newParentId, adapter.LocationTree.LocationModels.First(x => x.Entity == entity).ParentId);
        }

        [Test]
        public void ShowLocationUpdatesLocationTree()
        {
            var adapter = new LocationInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetGuiDispatcher(System.Windows.Threading.Dispatcher.CurrentDispatcher);
            var entity = new Location();
            adapter.ShowLocation(entity);
            Assert.IsTrue(adapter.LocationTree.LocationModels.Any(x => x.Entity == entity));
        }

        [TestCase(5, 10)]
        [TestCase(3, 11)]
        public void ChangeLocationDirectoryParentUpdatesLocationTree(long id, long newParentId)
        {
            var adapter = new LocationInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetGuiDispatcher(System.Windows.Threading.Dispatcher.CurrentDispatcher);
            var entity = new LocationDirectory() { Id = new LocationDirectoryId(id) };
            adapter.LocationTree.LocationDirectoryModels.Add(new InterfaceAdapters.Models.LocationDirectoryHumbleModel(entity));
            adapter.ChangeLocationDirectoryParent(entity, new LocationDirectoryId(newParentId));
            Assert.AreEqual(newParentId, adapter.LocationTree.LocationDirectoryModels.First(x => x.Entity == entity).ParentId);
        }
    }
}
