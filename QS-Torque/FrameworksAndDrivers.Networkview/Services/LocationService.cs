using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Google.Protobuf;
using Grpc.Core;
using LocationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class LocationService : global::LocationService.LocationService.LocationServiceBase
    {
        private readonly ILogger<LocationService> _logger;
        private readonly ILocationUseCase _locationUseCase;
        private readonly Mapper _mapper = new Mapper();

        public LocationService(ILogger<LocationService> logger, ILocationUseCase locationUseCase)
        {
            _logger = logger;
            _locationUseCase = locationUseCase;
        }

        [Authorize(Policy = nameof(LoadLocationDirectories))]
        public override Task<ListOfLocationDirectory> LoadLocationDirectories(NoParams request, ServerCallContext context)
        {
            var entities = _locationUseCase.LoadLocationDirectories();
            var listOfEntities = new ListOfLocationDirectory();
            entities.ForEach(s => listOfEntities.LocationDirectories.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(LoadDeletedLocationDirectories))]
        public override Task<ListOfLocationDirectory> LoadDeletedLocationDirectories(NoParams request, ServerCallContext context)
        {
            var entities = _locationUseCase.LoadAllLocationDirectories();
            var listOfEntities = new ListOfLocationDirectory();
            entities.ForEach(s => listOfEntities.LocationDirectories.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(InsertLocationDirectoriesWithHistory))]
        public override Task<ListOfLocationDirectory> InsertLocationDirectoriesWithHistory(InsertLocationDirectoriesWithHistoryRequest request,
            ServerCallContext context)
        {
            var entities = _locationUseCase.InsertLocationDirectoriesWithHistory(GetLocationDirectoryDiffs(request.LocationDirectoryDiffs), request.ReturnList);
            return Task.FromResult(GetListOfLocationDirectoryFromLocationDirectory(entities));
        }

        [Authorize(Policy = nameof(UpdateLocationDirectoriesWithHistory))]
        public override Task<ListOfLocationDirectory> UpdateLocationDirectoriesWithHistory(UpdateLocationDirectoriesWithHistoryRequest request,
            ServerCallContext context)
        {
            var entities = _locationUseCase.UpdateLocationDirectoriesWithHistory(GetLocationDirectoryDiffs(request.LocationDirectoryDiffs));
            return Task.FromResult(GetListOfLocationDirectoryFromLocationDirectory(entities));
        }

        [Authorize(Policy = nameof(LoadLocations))]
        public override Task<ListOfLocation> LoadLocations(LoadLocationsRequest request, ServerCallContext context)
        {
            var entities = _locationUseCase.LoadLocations(request.Index, request.Size);
            var listOfEntities = new ListOfLocation();
            entities.ForEach(s => listOfEntities.Locations.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }


        [Authorize(Policy = nameof(LoadDeletedLocations))]
        public override Task<ListOfLocation> LoadDeletedLocations(LoadLocationsRequest request, ServerCallContext context)
        {
            var entities = _locationUseCase.LoadDeletedLocations(request.Index, request.Size);
            var listOfEntities = new ListOfLocation();
            entities.ForEach(s => listOfEntities.Locations.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(LoadLocationsByIds))]
        public override Task<ListOfLocation> LoadLocationsByIds(ListOfLongs request, ServerCallContext context)
        {
            var entities = _locationUseCase.LoadLocationsByIds(request.Values.Select(x => new LocationId(x.Value)).ToList());
            var listOfEntities = new ListOfLocation();
            entities.ForEach(s => listOfEntities.Locations.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(InsertLocationsWithHistory))]
        public override Task<ListOfLocation> InsertLocationsWithHistory(InsertLocationsWithHistoryRequest request, ServerCallContext context)
        {
            var entities = _locationUseCase.InsertLocationsWithHistory(GetLocationDiffs(request.LocationDiffs), request.ReturnList);
            return Task.FromResult(GetListOfLocationFromLocation(entities));
        }

        [Authorize(Policy = nameof(UpdateLocationsWithHistory))]
        public override Task<ListOfLocation> UpdateLocationsWithHistory(UpdateLocationsWithHistoryRequest request, ServerCallContext context)
        {
            var entities = _locationUseCase.UpdateLocationsWithHistory(GetLocationDiffs(request.LocationDiffs));
            return Task.FromResult(GetListOfLocationFromLocation(entities));
        }

        [Authorize(Policy = nameof(IsUserIdUnique))]
        public override Task<Bool> IsUserIdUnique(String request, ServerCallContext context)
        {
            var result = _locationUseCase.IsUserIdUnique(request.Value);
            return Task.FromResult(new Bool() { Value = result });
        }

        [Authorize(Policy = nameof(GetReferencedLocPowIdsForLocationId))]
        public override Task<ListOfLongs> GetReferencedLocPowIdsForLocationId(Long request, ServerCallContext context)
        {
            var references =
                _locationUseCase.GetReferencedLocPowIdsForLocationId(new LocationId(request.Value));
            var listOfLongs = new ListOfLongs();
            references.ForEach(r => listOfLongs.Values.Add(new Long() { Value = r }));
            return Task.FromResult(listOfLongs);
        }

        [Authorize(Policy = nameof(GetLocationComment))]
        public override Task<String> GetLocationComment(Long request, ServerCallContext context)
        {
            return Task.FromResult(new String()
            { Value = _locationUseCase.GetLocationComment(new LocationId(request.Value)) });
        }

        [Authorize(Policy = nameof(LoadPictureForLocation))]
        public override Task<LoadPictureForLocationResponse> LoadPictureForLocation(LoadPictureForLocationRequest request, ServerCallContext context)
        {
            var result = new LoadPictureForLocationResponse();
            var picture = _locationUseCase.LoadPictureForLocation(new LocationId(request.LocationId), request.FileType);
            if (picture == null)
            {
                return Task.FromResult(result);
            }
            result.Picture = _mapper.DirectPropertyMapping(picture);
            if (picture.PictureBytes != null)
            {
                result.Picture.Image = ByteString.CopyFrom(picture.PictureBytes);
            }
            return Task.FromResult(result);
        }

        private ListOfLocationDirectory GetListOfLocationDirectoryFromLocationDirectory(List<Server.Core.Entities.LocationDirectory> entities)
        {
            var listOfEntities = new ListOfLocationDirectory();
            foreach (var entity in entities)
            {
                listOfEntities.LocationDirectories.Add(_mapper.DirectPropertyMapping(entity));
            }

            return listOfEntities;
        }

        private List<Server.Core.Diffs.LocationDirectoryDiff> GetLocationDirectoryDiffs(ListOfLocationDirectoryDiff locationDirectoryDiffs)
        {
            var diffs = new List<Server.Core.Diffs.LocationDirectoryDiff>();

            foreach (var diff in locationDirectoryDiffs.LocationDirectoyDiffs)
            {
                var user = new Server.Core.Entities.User() { UserId = new UserId(diff.UserId) };
                diffs.Add(new Server.Core.Diffs.LocationDirectoryDiff(user, new HistoryComment(diff.Comment),
                    GetLocationDirectoryFromLocationDirectoryDto(diff.OldLocationDirectory),
                    GetLocationDirectoryFromLocationDirectoryDto(diff.NewLocationDirectory)));
            }

            return diffs;
        }

        private Server.Core.Entities.LocationDirectory GetLocationDirectoryFromLocationDirectoryDto(DtoTypes.LocationDirectory entityDto)
        {
            if (entityDto == null)
            {
                return null;
            }

            return _mapper.DirectPropertyMapping(entityDto);
        }

        private ListOfLocation GetListOfLocationFromLocation(List<Server.Core.Entities.Location> entities)
        {
            var listOfEntities = new ListOfLocation();
            foreach (var entity in entities)
            {
                listOfEntities.Locations.Add(_mapper.DirectPropertyMapping(entity));
            }

            return listOfEntities;
        }

        private List<Server.Core.Diffs.LocationDiff> GetLocationDiffs(ListOfLocationDiff locationDiffs)
        {
            var diffs = new List<Server.Core.Diffs.LocationDiff>();
            var mapper = new Mapper();

            foreach (var diff in locationDiffs.LocationDiffs)
            {
                diffs.Add(new Server.Core.Diffs.LocationDiff(mapper.DirectPropertyMapping(diff.User),
                    new HistoryComment(diff.Comment),
                    GetLocationFromLocationDto(diff.OldLocation),
                    GetLocationFromLocationDto(diff.NewLocation)));
            }

            return diffs;
        }

        private Server.Core.Entities.Location GetLocationFromLocationDto(DtoTypes.Location entityDto)
        {
            if (entityDto == null)
            {
                return null;
            }

            return _mapper.DirectPropertyMapping(entityDto);
        }
    }
}
