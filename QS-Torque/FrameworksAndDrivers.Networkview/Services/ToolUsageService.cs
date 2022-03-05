using System.Collections.Generic;
using System.Threading.Tasks;
using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using ToolUsageService;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class ToolUsageService : global::ToolUsageService.ToolUsageService.ToolUsageServiceBase
    {
        private readonly ILogger<ToolUsageService> _logger;
        private readonly IToolUsageUseCase _toolUsageUseCase;
        private readonly Mapper _mapper = new Mapper();

        public ToolUsageService(ILogger<ToolUsageService> logger, IToolUsageUseCase toolUsageUseCase)
        {
            _logger = logger;
            _toolUsageUseCase = toolUsageUseCase;
        }

        [Authorize(Policy = nameof(GetAllToolUsages))]
        public override Task<ListOfToolUsage> GetAllToolUsages(NoParams request, ServerCallContext context)
        {
            var entities = _toolUsageUseCase.GetAllToolUsages();
            var listOfEntities = new ListOfToolUsage();
            entities.ForEach(s => listOfEntities.ToolUsageList.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(GetToolUsageLocationToolAssignmentReferences))]
        public override Task<ListOfLongs> GetToolUsageLocationToolAssignmentReferences(Long request, ServerCallContext context)
        {
            var references =
                _toolUsageUseCase.GetToolUsageLocationToolAssignmentReferences(
                    new ToolUsageId(request.Value));
            var listOfLongs = new ListOfLongs();
            references.ForEach(r => listOfLongs.Values.Add(new Long() { Value = r }));
            return Task.FromResult(listOfLongs);
        }

        [Authorize(Policy = nameof(InsertToolUsagesWithHistory))]
        public override Task<ListOfToolUsage> InsertToolUsagesWithHistory(InsertToolUsagesWithHistoryRequest request, ServerCallContext context)
        {
            var entities = _toolUsageUseCase.InsertToolUsagesWithHistory(GetToolUsageDiffs(request.ToolUsageDiffs), request.ReturnList);
            return Task.FromResult(GetListOfToolUsagesFromToolUsages(entities));
        }

        [Authorize(Policy = nameof(UpdateToolUsagesWithHistory))]
        public override Task<ListOfToolUsage> UpdateToolUsagesWithHistory(UpdateToolUsagesWithHistoryRequest request, ServerCallContext context)
        {
            var entities = _toolUsageUseCase.UpdateToolUsagesWithHistory(GetToolUsageDiffs(request.ToolUsageDiffs));
            return Task.FromResult(GetListOfToolUsagesFromToolUsages(entities));
        }

        private ListOfToolUsage GetListOfToolUsagesFromToolUsages(List<Server.Core.Entities.ToolUsage> entities)
        {
            var listOfEntities = new ListOfToolUsage();
            foreach (var entity in entities)
            {
                listOfEntities.ToolUsageList.Add(_mapper.DirectPropertyMapping(entity));
            }

            return listOfEntities;
        }

        private List<Server.Core.Diffs.ToolUsageDiff> GetToolUsageDiffs(ListOfToolUsageDiffs toolUsageDiffs)
        {
            var diffs = new List<Server.Core.Diffs.ToolUsageDiff>();

            foreach (var diff in toolUsageDiffs.ToolUsageDiffs)
            {
                var user = new Server.Core.Entities.User() { UserId = new UserId(diff.UserId) };
                diffs.Add(new Server.Core.Diffs.ToolUsageDiff(user, new HistoryComment(diff.Comment),
                    GetToolUsageFromToolUsageDto(diff.OldToolUsage),
                    GetToolUsageFromToolUsageDto(diff.NewToolUsage)));
            }

            return diffs;
        }

        private Server.Core.Entities.ToolUsage GetToolUsageFromToolUsageDto(DtoTypes.ToolUsage entityDto)
        {
            if (entityDto == null)
            {
                return null;
            }

            return _mapper.DirectPropertyMapping(entityDto);
        }
    }
}
