using FrameworksAndDrivers.Gui.Wpf.Model;
using NUnit.Framework;
using System.Collections.ObjectModel;
using System.Linq;
using Core.Entities;
using InterfaceAdapters.Models;
using TestHelper.Factories;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Models
{
    public class LocationTreeModelTest
    {
        [Test]
        public void ConstructingLocationTreeSetsParameters()
        {
            var locations = new ObservableCollection<LocationModel>();
            var directories = new ObservableCollection<LocationDirectoryHumbleModel>();
            var loctree = new LocationTreeModel(locations, directories);

            Assert.AreEqual(locations, loctree.LocationModels);
            Assert.AreEqual(directories, loctree.LocationDirectoryModels);
        }

        [Test]
        public void GetDirectoryEdgesReturnsFilledDictionary()
        {
            var directories = new ObservableCollection<LocationDirectoryHumbleModel>()
            {
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 98, ParentId = 32},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 32}
            };
            var locTree = new LocationTreeModel(null, directories);

            var result = locTree.GetDirectoryEdges();

            Assert.IsNull(result[directories[1]]);
            Assert.AreEqual(directories[1], result[directories[0]]);
        }

        [Test]
        public void GetTreeEdgesForReturnsFilledDictionary()
        {
            var directories = new ObservableCollection<LocationDirectoryHumbleModel>()
            {
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 98, ParentId = 32},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 32}
            };
            var locTree = new LocationTreeModel(null, directories);

            var result = locTree.GetTreeEdgesFor(directories);

            Assert.IsNull(result[directories[1]]);
            Assert.AreEqual(directories[1], result[directories[0]]);
        }

        [Test]
        public void GetLocationEdgesReturnsFilledDictionary()
        {
            var directories = new ObservableCollection<LocationDirectoryHumbleModel>()
            {
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 98, ParentId = 32},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 32}
            };
            var locations = new ObservableCollection<LocationModel>()
            {
                new LocationModel(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null) {Id = 63, ParentId = 32},
                new LocationModel(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null) {Id = 41, ParentId = 98}
            };

            var locTree = new LocationTreeModel(locations, directories);

            var result = locTree.GetLocationEdges();

            Assert.AreEqual(directories[0], result[locations[1]]);
            Assert.AreEqual(directories[1], result[locations[0]]);
        }

        [Test]
        public void GetDirectoriesTopologicalSortedReturnsObjectsInRightOrder()
        {
            var directories = new ObservableCollection<LocationDirectoryHumbleModel>()
            {
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 1},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 2, ParentId = 1},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 3, ParentId = 2},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 4, ParentId = 3},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 5, ParentId = 3},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 6, ParentId = 2},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 7, ParentId = 6},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 8, ParentId = 6},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 9, ParentId = 2},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 10, ParentId = 9},
            };
            var locTree = new LocationTreeModel(null, directories);

            var result = locTree.GetDirectoriesTopologicalSorted(locTree.GetDirectoryEdges());

            Assert.Greater(result.IndexOf((directories[1])), result.IndexOf(directories[0]));
            Assert.Greater(result.IndexOf((directories[2])), result.IndexOf(directories[1]));
            Assert.Greater(result.IndexOf((directories[5])), result.IndexOf(directories[1]));
            Assert.Greater(result.IndexOf((directories[8])), result.IndexOf(directories[1]));
            Assert.Greater(result.IndexOf((directories[3])), result.IndexOf(directories[2]));
            Assert.Greater(result.IndexOf((directories[4])), result.IndexOf(directories[2]));
            Assert.Greater(result.IndexOf((directories[6])), result.IndexOf(directories[5]));
            Assert.Greater(result.IndexOf((directories[7])), result.IndexOf(directories[5]));
            Assert.Greater(result.IndexOf((directories[9])), result.IndexOf(directories[8]));
        }

        [Test]
        public void GetElementsTopologicalSortedReturnsObjectsInRightOrder()
        {
            var directories = new ObservableCollection<LocationDirectoryHumbleModel>()
            {
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 1},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 2, ParentId = 1},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 3, ParentId = 2},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 4, ParentId = 3},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 5, ParentId = 3},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 6, ParentId = 2},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 7, ParentId = 6},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 8, ParentId = 6},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 9, ParentId = 2},
                new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 10, ParentId =9},
            };
            var locTree = new LocationTreeModel(null, directories);

            var result = locTree.GetElementsTopologicalSorted(directories, locTree.GetDirectoryEdges()).ToList();

            Assert.Greater(result.IndexOf((directories[1])), result.IndexOf(directories[0]));
            Assert.Greater(result.IndexOf((directories[2])), result.IndexOf(directories[1]));
            Assert.Greater(result.IndexOf((directories[5])), result.IndexOf(directories[1]));
            Assert.Greater(result.IndexOf((directories[8])), result.IndexOf(directories[1]));
            Assert.Greater(result.IndexOf((directories[3])), result.IndexOf(directories[2]));
            Assert.Greater(result.IndexOf((directories[4])), result.IndexOf(directories[2]));
            Assert.Greater(result.IndexOf((directories[6])), result.IndexOf(directories[5]));
            Assert.Greater(result.IndexOf((directories[7])), result.IndexOf(directories[5]));
            Assert.Greater(result.IndexOf((directories[9])), result.IndexOf(directories[8]));
        }
    }
}