using BasicTypes;
using DtoTypes;
using ExtensionService;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Grpc.Core;
using LocationService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class LocationClient : ILocationClient
    {
        private readonly LocationService.LocationService.LocationServiceClient _locationClient;

        public LocationClient(LocationService.LocationService.LocationServiceClient locationClient)
        {
            _locationClient = locationClient;
        }

        public ListOfLocationDirectory LoadLocationDirectories()
        {
            return _locationClient.LoadLocationDirectories(new NoParams(), new CallOptions());
        }

        public ListOfLocationDirectory InsertLocationDirectoriesWithHistory(InsertLocationDirectoriesWithHistoryRequest request)
        {
            return _locationClient.InsertLocationDirectoriesWithHistory(request, new CallOptions());
        }

        public ListOfLocationDirectory UpdateLocationDirectoriesWithHistory(UpdateLocationDirectoriesWithHistoryRequest request)
        {
            return _locationClient.UpdateLocationDirectoriesWithHistory(request, new CallOptions());
        }

        public ListOfLocation LoadLocations(LoadLocationsRequest request)
        {
            return _locationClient.LoadLocations(request, new CallOptions());
        }

        public ListOfLocation LoadLocationsByIds(ListOfLongs ids)
        {
            return _locationClient.LoadLocationsByIds(ids, new CallOptions());
        }

        public ListOfLocation InsertLocationsWithHistory(InsertLocationsWithHistoryRequest request)
        {
            return _locationClient.InsertLocationsWithHistory(request, new CallOptions());
        }

        public ListOfLocation UpdateLocationsWithHistory(UpdateLocationsWithHistoryRequest request)
        {
            return _locationClient.UpdateLocationsWithHistory(request, new CallOptions());
        }

        public Bool IsUserIdUnique(String name)
        {
            return _locationClient.IsUserIdUnique(name, new CallOptions());
        }

        public ListOfLongs GetReferencedLocPowIdsForLocationId(Long locationId)
        {
            return _locationClient.GetReferencedLocPowIdsForLocationId(locationId, new CallOptions());
        }

        public String GetLocationComment(Long locationId)
        {
            return _locationClient.GetLocationComment(locationId);
        }

        public LoadPictureForLocationResponse LoadPictureForLocation(LoadPictureForLocationRequest request)
        {
            return _locationClient.LoadPictureForLocation(request);
        }

        public ListOfLocationDirectory LoadAllLocationDirectories()
        {
            return _locationClient.LoadDeletedLocationDirectories(new NoParams(), new CallOptions());
        }

        public ListOfLocation LoadDeletedLocations(LoadLocationsRequest request)
        {
            return _locationClient.LoadDeletedLocations(request, new CallOptions());
        }
    }
}
