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
using LocationToolAssignmentReferenceLink = Core.Entities.ReferenceLink.LocationToolAssignmentReferenceLink;
using Tool = Core.Entities.Tool;
using User = Core.Entities.User;
using LocationReferenceLink = Core.Entities.ReferenceLink.LocationReferenceLink;
using System.Linq;
using BasicTypes;
using Core;
using ToolService;
using ExtensionService;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public class TrashDataAccess : ITrashData
    {
        private readonly IClientFactory _clientFactory;
        private static readonly ILog Log = LogManager.GetLogger(typeof(LocationUseCase));
        private readonly Mapper _mapper = new Mapper();

        private const int LocationPackageSize = 25000;
        private readonly ILocationToolAssignmentDisplayFormatter _locationToolAssignmentDisplayFormatter;
        private readonly ILocationDisplayFormatter _locationDisplayFormatter;

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

        private IToolClient GetToolClient()
        {
            return _clientFactory.AuthenticationChannel.GetToolClient();
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

        public List<ToolModel> LoadDeletedModelsWithAtLeastOneTool()
        {
            var dtos = GetToolClient().LoadDeletedModelsWithAtLeastOneTool();

            var tools = new List<ToolModel>();

            foreach (var model in dtos.ToolModels)
            {
                tools.Add(_mapper.DirectPropertyMapping(model));
            }

            return tools;
        }

        public List<ToolModel> LoadDeletedToolModels()
        {
            var mapper = new Mapper();
            var result = GetToolModelClient().GetAllDeletedToolModels();
            return result.ToolModels.Select(toolModelDto => mapper.DirectPropertyMapping(toolModelDto)).ToList();
        }

        public List<LocationToolAssignmentReferenceLink> LoadLocationToolAssignmentLinksForToolId(ToolId toolId)
        {
            var locationToolLinks = GetToolClient().GetLocationToolAssignmentLinkForTool(new Long()
            {
                Value = toolId.ToLong()
            });

            var modelLinks = new List<LocationToolAssignmentReferenceLink>();

            foreach (var locToolLink in locationToolLinks.Links)
            {
                modelLinks.Add(new LocationToolAssignmentReferenceLink(
                    new QstIdentifier(locToolLink.Id),
                    new LocationDescription(locToolLink.LocationName),
                    new LocationNumber(locToolLink.LocationNumber),
                    locToolLink.ToolSerialNumber,
                    locToolLink.ToolInventoryNumber,
                    _locationToolAssignmentDisplayFormatter,
                    new LocationId(locToolLink.LocationId),
                    new ToolId(locToolLink.ToolId)));
            }
            return modelLinks;
        }

        public void RestoreTool(Tool tool, User byUser)
        {
            if (byUser == null)
            {
                throw new ArgumentException("User should not be null");
            }

            var oldTool = _mapper.DirectPropertyMapping(tool);
            var newTool = _mapper.DirectPropertyMapping(tool);
            oldTool.Alive = false;
            newTool.Alive = true;

            var request = new UpdateToolsWithHistoryRequest()
            {
                ToolDiffs = new ListOfToolDiffs()
                {
                    ToolDiffs =
                    {
                        new List<DtoTypes.ToolDiff>()
                        {
                            new DtoTypes.ToolDiff()
                            {
                                UserId = byUser.UserId.ToLong(),
                                Comment = "",
                                OldTool = oldTool,
                                NewTool = newTool
                            }
                        }
                    }
                }
            };

            GetToolClient().UpdateToolsWithHistory(request);
        }

        public List<LocationReferenceLink> LoadReferencedLocations(ExtensionId id)
        {
            var locationLinkDtos = GetExtensionClient().GetExtensionLocationLinks(new LongRequest() { Value = id.ToLong() });
            var locationReferenceLinks = new List<LocationReferenceLink>();

            foreach (var locationLinkDto in locationLinkDtos.LocationLinks)
            {
                try
                {
                    var locationReferenceLink = new LocationReferenceLink(new QstIdentifier(locationLinkDto.Id), new LocationNumber(locationLinkDto.Number), new LocationDescription(locationLinkDto.Description), _locationDisplayFormatter);
                    locationReferenceLinks.Add(locationReferenceLink);
                }
                catch (Exception exception)
                {
                    Log.Error($"Error while mapping Location with Id { locationLinkDto?.Id}", exception);
                }
            }

            return locationReferenceLinks;
        }

        public void RestoreExtension(Extension extension, User user)
        {
            if (user == null)
            {
                throw new ArgumentException("User should not be null");
            }

            var mapper = new Mapper();
            var oldExtension = mapper.DirectPropertyMapping(extension);
            var newExtension = mapper.DirectPropertyMapping(extension);
            oldExtension.Alive = false;
            newExtension.Alive = true;

            var request = new UpdateExtensionsRequest()
            {
                ExtensionsDiffs = new ListOfExtensionsDiffs()
                {
                    ExtensionsDiff =
                    {
                        new DtoTypes.ExtensionDiff()
                        {
                            UserId = user.UserId.ToLong(),
                            Comment =  "",
                            OldExtension = oldExtension,
                            NewExtension = newExtension
                        }
                    }
                }
            };

            GetExtensionClient().UpdateExtensions(request);
        }
    }
}