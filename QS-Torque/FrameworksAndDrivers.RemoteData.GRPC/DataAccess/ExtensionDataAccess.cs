using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Core;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using log4net;
using DateTime = System.DateTime;
using Extension = Core.Entities.Extension;
using User = Core.Entities.User;
using ExtensionService;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface IExtensionClient
    {
        ListOfExtensions LoadExtensions();
        ListOfLocationLink GetExtensionLocationLinks(LongRequest extensionId);
        ListOfExtensions InsertExtensions(InsertExtensionsRequest request);
        void UpdateExtensions(UpdateExtensionsRequest request);
        Bool IsExtensionInventoryNumberUnique(BasicTypes.String inventoryNumber);
        ListOfExtensions LoadDeletedExtensions();
    }
    public class ExtensionDataAccess : IExtensionDataAccess
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ExtensionDataAccess));
        private readonly IClientFactory _clientFactory;
        private readonly ILocationDisplayFormatter _locationDisplayFormatter;
        private readonly ITimeDataAccess _timeDataAccess;

        public ExtensionDataAccess(IClientFactory clientFactory, ILocationDisplayFormatter locationDisplayFormatter, ITimeDataAccess timeDataAccess)
        {
            _clientFactory = clientFactory;
            _locationDisplayFormatter = locationDisplayFormatter;
            _timeDataAccess = timeDataAccess;
        }
        private IExtensionClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetExtensionClient();
        }

        public List<Extension> LoadExtensions()
        {
            var extensions = new List<Extension>();
          
            var dtoList = GetClient().LoadExtensions();
            var mapper = new Mapper();
            
            foreach(var dto in dtoList.Extensions)
            {
                extensions.Add(mapper.DirectPropertyMapping(dto));
            }

            return extensions;
        }

        public List<LocationReferenceLink> LoadReferencedLocations(ExtensionId id)
        {
            var locationLinkDtos = GetClient().GetExtensionLocationLinks(new LongRequest() { Value = id.ToLong() });
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

        public Extension AddExtension(Extension extension, User byUser)
        {
            if (byUser == null)
            {
                throw new ArgumentException("User should not be null");
            }
            var mapper = new Mapper();
            var request = new InsertExtensionsRequest()
            {
                ExtensionsDiffs = new ListOfExtensionsDiffs()
                {
                    ExtensionsDiff =
                    {
                        new DtoTypes.ExtensionDiff()
                        {
                            UserId = byUser.UserId.ToLong(),
                            Comment = "",
                            NewExtension = mapper.DirectPropertyMapping(extension)
                        }
                    }
                },
                ReturnList = true
            };

            var result = GetClient().InsertExtensions(request);

            if (result?.Extensions.FirstOrDefault() == null)
            {
                throw new NullReferenceException("Server returned null when adding a Extension");
            }

            return mapper.DirectPropertyMapping(result.Extensions.FirstOrDefault());
        }

        public void SaveExtension(Client.Core.Diffs.ExtensionDiff extensionDiff)
        {
            if (extensionDiff.OldExtension.Id.ToLong() != extensionDiff.NewExtension.Id.ToLong())
            {
                throw new ArgumentException("Mismatching ExtensionIds");
            }

            if (extensionDiff.OldExtension.EqualsByContent(extensionDiff.NewExtension))
            {
                return;
            }

            var mapper = new Mapper();
            var oldExtension = mapper.DirectPropertyMapping(extensionDiff.OldExtension);
            var newExtension = mapper.DirectPropertyMapping(extensionDiff.NewExtension);
            oldExtension.Alive = true;
            newExtension.Alive = true;

            var request = new UpdateExtensionsRequest()
            {
                ExtensionsDiffs = new ListOfExtensionsDiffs()
                {
                    ExtensionsDiff =
                    {
                        new DtoTypes.ExtensionDiff()
                        {
                            UserId = extensionDiff.User.UserId.ToLong(),
                            Comment = extensionDiff.Comment?.ToDefaultString() ?? "",
                            OldExtension = oldExtension,
                            NewExtension = newExtension
                        }
                    }
                },
            };

            GetClient().UpdateExtensions(request);
        }

        public bool IsInventoryNumberUnique(ExtensionInventoryNumber inventoryNumber)
        {
            return GetClient().IsExtensionInventoryNumberUnique(new BasicTypes.String() { Value = inventoryNumber.ToDefaultString() }).Value;
        }

        public void RemoveExtension(Extension extension, User user)
        {
            if (user == null)
            {
                throw new ArgumentException("User should not be null");
            }

            var mapper = new Mapper();
            var oldExtension = mapper.DirectPropertyMapping(extension);
            var newExtension = mapper.DirectPropertyMapping(extension);
            oldExtension.Alive = true;
            newExtension.Alive = false;

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

            GetClient().UpdateExtensions(request);
        }
    }
}
