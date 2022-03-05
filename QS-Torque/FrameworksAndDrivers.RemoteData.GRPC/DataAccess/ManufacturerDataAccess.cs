using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using ManufacturerService;
using Manufacturer = Core.Entities.Manufacturer;
using ManufacturerDiff = Core.UseCases.ManufacturerDiff;
using User = Core.Entities.User;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface IManufacturerClient
    {
        ListOfManufacturers LoadManufacturers();
        StringResponse GetManufacturerComment(LongRequest longRequest);
        ListOfModelLink GetManufacturerModelLinks(LongRequest longRequest);
        ListOfManufacturers InsertManufacturerWithHistory(InsertManufacturerWithHistoryRequest request);
        ListOfManufacturers UpdateManufacturerWithHistory(UpdateManufacturerWithHistoryRequest request);
    }

    public class ManufacturerDataAccess : IManufacturerData
    {
        private readonly IClientFactory _clientFactory;
        private readonly Mapper _mapper = new Mapper();


        public ManufacturerDataAccess(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private IManufacturerClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetManufacturerClient();
        }

        public List<Manufacturer> LoadManufacturer()
        {
            var manufacturerResponse = GetClient().LoadManufacturers();

            var manufacturers = new List<Manufacturer>();
            foreach (var manufacturer in manufacturerResponse.Manufacturers)
            {
                manufacturers.Add(_mapper.DirectPropertyMapping(manufacturer));
            }

            return manufacturers;
        }

        public Manufacturer AddManufacturer(Manufacturer manufacturer, User byUser)
        {
            if (byUser == null)
            {
                throw new ArgumentException("User should not be null");
            }

            var request = new InsertManufacturerWithHistoryRequest()
            {
                ManufacturerDiffs = new ListOfManufacturerDiffs()
                {
                    ManufacturerDiff =
                    {
                        new DtoTypes.ManufacturerDiff()
                        {
                            UserId = byUser.UserId.ToLong(),
                            Comment = "",
                            NewManufacturer = _mapper.DirectPropertyMapping(manufacturer)
                        }
                    }
                },
                ReturnList = true
            };

            var result = GetClient().InsertManufacturerWithHistory(request);

            if (result?.Manufacturers.FirstOrDefault() == null)
            {
                throw new NullReferenceException("Server returned null when Adding a Manufacturer");
            }

            return _mapper.DirectPropertyMapping(result.Manufacturers.FirstOrDefault());
        }

        
        public void RemoveManufacturer(Manufacturer manufacturer, User byUser)
        {
            if (byUser == null)
            {
                throw new ArgumentException("User should not be null");
            }

            var oldManufacturer = _mapper.DirectPropertyMapping(manufacturer);
            var newManufacturer = _mapper.DirectPropertyMapping(manufacturer);
            oldManufacturer.Alive = true;
            newManufacturer.Alive = false;

            var request = new UpdateManufacturerWithHistoryRequest()
            {
                ManufacturerDiffs = new ListOfManufacturerDiffs()
                {
                    ManufacturerDiff =
                    {
                        new DtoTypes.ManufacturerDiff()
                        {
                            UserId = byUser.UserId.ToLong(),
                            Comment = "",
                            OldManufacturer = oldManufacturer,
                            NewManufacturer = newManufacturer
                        }
                    }
                }
            };

            GetClient().UpdateManufacturerWithHistory(request);
        }

        public Manufacturer SaveManufacturer(ManufacturerDiff manufacturer)
        {
            if (!manufacturer.GetOldManufacturer().EqualsById(manufacturer.GetNewManufacturer()))
            {
                throw new ArgumentException("Mismatching ManufacturerIds");
            }

            var oldManufacturer = _mapper.DirectPropertyMapping(manufacturer.GetOldManufacturer());
            var newManufacturer = _mapper.DirectPropertyMapping(manufacturer.GetNewManufacturer());
            oldManufacturer.Alive = true;
            newManufacturer.Alive = true;

            var request = new UpdateManufacturerWithHistoryRequest()
            {
                ManufacturerDiffs = new ListOfManufacturerDiffs()
                {
                    ManufacturerDiff =
                    {
                        new DtoTypes.ManufacturerDiff()
                        {
                            UserId = manufacturer.GetUser().UserId.ToLong(),
                            Comment = manufacturer.GetComment().ToDefaultString(),
                            OldManufacturer = oldManufacturer,
                            NewManufacturer = newManufacturer
                        }
                    }
                }
            };

            GetClient().UpdateManufacturerWithHistory(request);

            return manufacturer.GetNewManufacturer();
        }

        public string LoadManufacturerForComment(Manufacturer manufacturer)
        {
            var stringResponse = GetClient().GetManufacturerComment(new LongRequest()
            {
                Value = manufacturer.Id.ToLong()
            });

            return stringResponse.Value;
        }

        public List<ToolModelReferenceLink> LoadToolModelReferenceLinksForManufacturerId(long manufacturerId)
        {
            var listOfModelLink = GetClient().GetManufacturerModelLinks(new LongRequest()
            {
                Value = manufacturerId
            });

            var modelLinks = new List<ToolModelReferenceLink>();

            foreach (var modelLink in listOfModelLink.ModelLinks)
            {
                modelLinks.Add(new ToolModelReferenceLink()
                {
                    Id = new QstIdentifier(modelLink.Id),
                    DisplayName = modelLink.Model
                });
            }
            return modelLinks;
        }
    }
}
