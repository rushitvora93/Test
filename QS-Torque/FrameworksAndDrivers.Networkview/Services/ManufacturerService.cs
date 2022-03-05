using System.Collections.Generic;
using System.Threading.Tasks;
using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Grpc.Core;
using ManufacturerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.UseCases.UseCases;
using Manufacturer = DtoTypes.Manufacturer;
using ManufacturerDiff = Server.Core.Diffs.ManufacturerDiff;
using User = Server.Core.Entities.User;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class ManufacturerService : global::ManufacturerService.Manufacturers.ManufacturersBase
    {
        private readonly ILogger<ManufacturerService> _logger;
        private readonly IManufacturerUseCase _manufacturerUseCase;
        private readonly Mapper _mapper = new Mapper();

        public ManufacturerService(ILogger<ManufacturerService> logger, IManufacturerUseCase manufacturerUseCase)
        {
            _logger = logger;
            _manufacturerUseCase = manufacturerUseCase;
        }

        [Authorize(Policy = nameof(LoadManufacturers))]
        public override Task<ListOfManufacturers> LoadManufacturers(NoParams request, ServerCallContext context)
        {
            var manufacturers = _manufacturerUseCase.LoadManufacturers();
            return Task.FromResult(GetListOfManufacturersFromManufacturers(manufacturers));
        }

        [Authorize(Policy = nameof(GetManufacturerComment))]
        public override Task<StringResponse> GetManufacturerComment(LongRequest request, ServerCallContext context)
        {
            return Task.FromResult(new StringResponse()
                {Value = _manufacturerUseCase.GetManufacturerComment(new ManufacturerId(request.Value))});
        }

        [Authorize(Policy = nameof(GetManufacturerModelLinks))]
        public override Task<ListOfModelLink> GetManufacturerModelLinks(LongRequest request, ServerCallContext context)
        {
            var modelLinks = _manufacturerUseCase.GetManufacturerModelLinks(new ManufacturerId(request.Value));
            return Task.FromResult(GetListOfModelLinksFromToolModelReferenceLink(modelLinks));
        }

        [Authorize(Policy = nameof(InsertManufacturerWithHistory))]
        public override Task<ListOfManufacturers> InsertManufacturerWithHistory(InsertManufacturerWithHistoryRequest request, ServerCallContext context)
        {
            var manufacturers = _manufacturerUseCase.InsertManufacturersWithHistory(GetManufacturerDiffs(request.ManufacturerDiffs), request.ReturnList);
            return Task.FromResult(GetListOfManufacturersFromManufacturers(manufacturers));
        }

        [Authorize(Policy = nameof(UpdateManufacturerWithHistory))]
        public override Task<ListOfManufacturers> UpdateManufacturerWithHistory(UpdateManufacturerWithHistoryRequest request, ServerCallContext context)
        {
            var manufacturers = _manufacturerUseCase.UpdateManufacturersWithHistory(GetManufacturerDiffs(request.ManufacturerDiffs));
            return Task.FromResult(GetListOfManufacturersFromManufacturers(manufacturers));
        }

        private ListOfManufacturers GetListOfManufacturersFromManufacturers(List<Server.Core.Entities.Manufacturer> manufacturers)
        {
            var listOfManufacturers = new ListOfManufacturers();
            foreach (var manufacturer in manufacturers)
            {
                listOfManufacturers.Manufacturers.Add(_mapper.DirectPropertyMapping(manufacturer));
            }

            return listOfManufacturers;
        }

        private Server.Core.Entities.Manufacturer GetManufacturerFromManufacturerDto(Manufacturer manufacturerDto)
        {
            if (manufacturerDto == null)
            {
                return null;
            }

            return _mapper.DirectPropertyMapping(manufacturerDto);
        }

        private ListOfModelLink GetListOfModelLinksFromToolModelReferenceLink(List<ToolModelReferenceLink> modelLinks)
        {
            var listOfModelLinks = new ListOfModelLink();
            foreach (var modelLink in modelLinks)
            {
                listOfModelLinks.ModelLinks.Add(_mapper.DirectPropertyMapping(modelLink));
            }

            return listOfModelLinks;
        }

        private List<ManufacturerDiff> GetManufacturerDiffs(ListOfManufacturerDiffs listOfManufacturerDiffs)
        {
            var manufacturerDiffs = new List<ManufacturerDiff>();

            foreach (var manufacturerDiff in listOfManufacturerDiffs.ManufacturerDiff)
            {
                var user = new User() { UserId = new UserId(manufacturerDiff.UserId) };
                manufacturerDiffs.Add(new ManufacturerDiff(user, new HistoryComment(manufacturerDiff.Comment),
                    GetManufacturerFromManufacturerDto(manufacturerDiff.OldManufacturer),
                    GetManufacturerFromManufacturerDto(manufacturerDiff.NewManufacturer)));
            }

            return manufacturerDiffs;
        }
    }
}
