using System;
using System.Collections.Generic;
using Core.Entities;
using Core.UseCases;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using LocationService;
using log4net;
using Location = Core.Entities.Location;
using Extension = Core.Entities.Extension;
using ToolModel = Core.Entities.ToolModel;
using LocationDirectory = Core.Entities.LocationDirectory;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public class TrashDataAccess : ITrashData
    {
        private readonly IClientFactory _clientFactory;
        private static readonly ILog Log = LogManager.GetLogger(typeof(LocationUseCase));
        private readonly Mapper _mapper = new Mapper();

        private const int LocationPackageSize = 25000;

        public TrashDataAccess(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private ILocationClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetLocationClient();
        }

        private IExtensionClient GetExtensionClient()
        {
            return _clientFactory.AuthenticationChannel.GetExtensionClient();
        }
        private IToolModelClient GetToolModelClient()
        {
            return _clientFactory.AuthenticationChannel.GetToolModelClient();
        }
        public IEnumerable<Location> LoadDeletedLocations()
        {
            var locations = new List<Location>();
            var index = 0;
            ListOfLocation listOfLocation;

            do
            {
                var loadLocationRequest = new LoadLocationsRequest()
                {
                    Index = index,
                    Size = LocationPackageSize
                };

                listOfLocation = GetClient().LoadDeletedLocations(loadLocationRequest);
                index += LocationPackageSize;

                foreach (var dto in listOfLocation.Locations)
                {
                    Location location = null;
                    try
                    {
                        location = _mapper.DirectPropertyMapping(dto);
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Error while mapping Location with Id {dto.Id}", e);
                    }

                    if (location != null)
                    {
                        locations.Add(location);
                    }
                }

            } while (listOfLocation.Locations.Count > 0);

            return locations;
        }

        public List<LocationDirectory> LoadAllDirectories()
        {
            var listOfDeletedLocationDirectory = GetClient().LoadAllLocationDirectories();
            var entities = new List<LocationDirectory>();
            var mapper = new Mapper();
            foreach (var locationDirectory in listOfDeletedLocationDirectory.LocationDirectories)
            {
                var directory = mapper.DirectPropertyMapping(locationDirectory);
                if (directory.ParentId.Equals(new LocationDirectoryId(0)))
                {
                    directory.Name = new LocationDirectoryName("Measurement Point");
                }
                entities.Add(directory);
            }
            return entities;
        }

        public List<Extension> LoadDeletedExtensions()
        {
            var extensions = new List<Extension>();

            var dtoList = GetExtensionClient().LoadDeletedExtensions();
            var mapper = new Mapper();

            foreach (var dto in dtoList.Extensions)
            {
                extensions.Add(mapper.DirectPropertyMapping(dto));
            }

            return extensions;
        }
        public List<ToolModel> LoadDeletedToolModels()
        {
            var toolModels = new List<ToolModel>();

            var dtoList = GetToolModelClient().LoadDeletedToolModels();
            var mapper = new Mapper();

            foreach (var dto in dtoList.ToolModels)
            {
                toolModels.Add(mapper.DirectPropertyMapping(dto));
            }

            return toolModels;
        }
    }
}
