using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.UseCases.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class HistoryService : global::HistoryService.Histories.HistoriesBase
    {
        private readonly ILogger<HistoryService> _logger;
        private readonly IHistoryUseCase _useCase;

        public HistoryService(IHistoryUseCase locationUseCase, ILogger<HistoryService> logger)
        {
            _logger = logger;
            _useCase = locationUseCase;
        }


        [Authorize(Policy = nameof(LoadLocationDiffsFor))]
        public override Task<ListOfLocationDiff> LoadLocationDiffsFor(Long request, ServerCallContext context)
        {
            var diffs = _useCase.LoadLocationDiffsFor(new Server.Core.Entities.LocationId(request.Value));
            var mapper = new Mapper();
            var dtos = new ListOfLocationDiff();
            diffs.ForEach(x => dtos.LocationDiffs.Add(mapper.DirectPropertyMapping(x)));
            return Task.FromResult(dtos);
        }
    }
}
