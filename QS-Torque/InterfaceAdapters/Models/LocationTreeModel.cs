using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InterfaceAdapters.Models;

namespace InterfaceAdapters.Models
{
    public class LocationTreeModel : IElementTreeSource<LocationDirectoryHumbleModel>
    {
        public ObservableCollection<LocationModel> LocationModels { get; private set; }
        public ObservableCollection<LocationDirectoryHumbleModel> LocationDirectoryModels { get; private set; }
        public ObservableCollection<LocationDirectoryHumbleModel> TreeElements => LocationDirectoryModels;


        #region Methods

        public Dictionary<LocationDirectoryHumbleModel, LocationDirectoryHumbleModel> GetTreeEdgesFor(
            IEnumerable<LocationDirectoryHumbleModel> elements)
        {
            var edges = new Dictionary<LocationDirectoryHumbleModel, LocationDirectoryHumbleModel>();
            var idDict = new Dictionary<long, LocationDirectoryHumbleModel>();

            foreach (var directoryModel in LocationDirectoryModels)
            {
                idDict.Add(directoryModel.Id, directoryModel);
            }

            foreach (var directoryModel in elements)
            {
                edges.Add(directoryModel,
                    directoryModel?.ParentId == null || directoryModel.ParentId <= 0
                        ? null
                        : idDict[directoryModel.ParentId]);
            }

            return edges;
        }

        public Dictionary<LocationDirectoryHumbleModel, LocationDirectoryHumbleModel> GetDirectoryEdges()
        {
            return GetTreeEdgesFor(LocationDirectoryModels.ToList());
        }

        public Dictionary<LocationModel, LocationDirectoryHumbleModel> GetLocationEdges()
        {
            var edges = new Dictionary<LocationModel, LocationDirectoryHumbleModel>();
            var idDict = new Dictionary<long, LocationDirectoryHumbleModel>();

            foreach (var directoryModel in LocationDirectoryModels)
            {
                idDict.Add(directoryModel.Id, directoryModel);
            }

            foreach (var locationModel in LocationModels)
            {
                edges.Add(locationModel, locationModel?.ParentId == null ? null : idDict[locationModel.ParentId]);
            }

            return edges;
        }

        public IEnumerable<LocationDirectoryHumbleModel> GetElementsTopologicalSorted(
            IEnumerable<LocationDirectoryHumbleModel> elements,
            Dictionary<LocationDirectoryHumbleModel, LocationDirectoryHumbleModel> edges)
        {
            List<LocationDirectoryHumbleModel> visitedDirectories = new List<LocationDirectoryHumbleModel>();
            List<LocationDirectoryHumbleModel> sortedList = new List<LocationDirectoryHumbleModel>();

            foreach (var dir in elements)
            {
                if (!visitedDirectories.Contains(dir))
                {
                    SortDirectory(dir, edges, visitedDirectories, sortedList);
                }
            }

            return sortedList;
        }

        public List<LocationDirectoryHumbleModel> GetDirectoriesTopologicalSorted(
            Dictionary<LocationDirectoryHumbleModel, LocationDirectoryHumbleModel> directoryEdges)
        {
            return GetElementsTopologicalSorted(LocationDirectoryModels, directoryEdges).ToList();
        }

        private void SortDirectory(LocationDirectoryHumbleModel directoryHumble,
            Dictionary<LocationDirectoryHumbleModel, LocationDirectoryHumbleModel> directoryEdges,
            List<LocationDirectoryHumbleModel> visitedDirectories,
            List<LocationDirectoryHumbleModel> sortedList)
        {
            visitedDirectories.Add(directoryHumble);

            var subDirectories = directoryEdges
                .Where(x => x.Value?.EqualsById(directoryHumble) == true && !visitedDirectories.Contains(x.Key))
                .Select(x => x.Key);

            foreach (var sub in subDirectories)
            {
                SortDirectory(sub, directoryEdges, visitedDirectories, sortedList);
            }

            sortedList.Insert(0, directoryHumble);
        }

        #endregion


        public LocationTreeModel()
        {
            LocationModels = new ObservableCollection<LocationModel>();
            LocationDirectoryModels = new ObservableCollection<LocationDirectoryHumbleModel>();
        }

        public LocationTreeModel(ObservableCollection<LocationModel> locationModels,
            ObservableCollection<LocationDirectoryHumbleModel> locationDirectoryModels)
        {
            LocationModels = locationModels;
            LocationDirectoryModels = locationDirectoryModels;
        }
    }
}