using System.Collections.Generic;
using System.Threading.Tasks;
using BasicTypes;
using Common.Types.Exceptions;
using DtoTypes;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Grpc.Core;
using HelperTableService;
using Microsoft.Extensions.Logging;
using Server.UseCases.UseCases;
using Microsoft.AspNetCore.Authorization;
using Server.Core.Entities;
using State;
using HelperTableEntity = Server.Core.Entities.HelperTableEntity;
using HelperTableEntityDiff = Server.Core.Diffs.HelperTableEntityDiff;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class HelperTableService : global::HelperTableService.HelperTableService.HelperTableServiceBase
    {
        private readonly ILogger<SetupService> _logger;
        private readonly IHelperTableUseCase _helperTableUseCase;
        private readonly Mapper _mapper = new Mapper();

        public HelperTableService(ILogger<SetupService> logger, IHelperTableUseCase helperTableUseCase)
        {
            _logger = logger;
            _helperTableUseCase = helperTableUseCase;
        }

        [Authorize(Policy = nameof(GetHelperTableByNodeId))]
        public override Task<ListOfHelperTableEntity> GetHelperTableByNodeId(Long request, ServerCallContext context)
        {
            var helperTableEntities = _helperTableUseCase.GetHelperTableByNodeId((NodeId)request.Value);
            var listOfHelperTableEntities = new ListOfHelperTableEntity();
            helperTableEntities.ForEach(s => listOfHelperTableEntities.HelperTableEntities.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfHelperTableEntities);
        }

        [Authorize(Policy = nameof(GetAllHelperTableEntities))]
        public override Task<ListOfHelperTableEntity> GetAllHelperTableEntities(NoParams request, ServerCallContext context)
        {
            var helperTableEntities = _helperTableUseCase.GetAllHelperTableEntities();
            var listOfHelperTableEntities = new ListOfHelperTableEntity();
            helperTableEntities.ForEach(s => listOfHelperTableEntities.HelperTableEntities.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfHelperTableEntities);
        }

        [Authorize(Policy = nameof(GetHelperTableEntityModelLinks))]
        public override Task<ListOfModelLink> GetHelperTableEntityModelLinks(HelperTableEntityLinkRequest request, ServerCallContext context)
        {
            var helperTableEntityModelLinks = _helperTableUseCase.GetHelperTableEntityModelLinks(request.Id, (NodeId)request.NodeId);
            var listOfModelLink = new ListOfModelLink();
            helperTableEntityModelLinks.ForEach(s => listOfModelLink.ModelLinks.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfModelLink);
        }

        [Authorize(Policy = nameof(GetHelperTableEntityToolLinks))]
        public override Task<ListOfToolReferenceLink> GetHelperTableEntityToolLinks(HelperTableEntityLinkRequest request, ServerCallContext context)
        {
            var helperTableEntityModelLinks = _helperTableUseCase.GetHelperTableEntityToolLinks(request.Id, (NodeId)request.NodeId);
            var listOfToolReferenceLink = new ListOfToolReferenceLink();
            helperTableEntityModelLinks.ForEach(s => listOfToolReferenceLink.ToolReferenceLinks.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfToolReferenceLink);
        }

        [Authorize(Policy = nameof(InsertHelperTableEntityWithHistory))]
        public override Task<ListOfHelperTableEntity> InsertHelperTableEntityWithHistory(InsertHelperTableEntityWithHistoryRequest request, ServerCallContext context)
        {
            try
            {
                var entities = _helperTableUseCase.InsertHelperTableEntityWithHistory(GetHelperTableDiffs(request.HelperTableEntityDiffs), request.ReturnList);
                return Task.FromResult(GetListOfHelperTableEntitiesFromHelperTableEntities(entities));
            }
            catch (EntryAlreadyExistsException e)
            {
                throw new RpcException(new Grpc.Core.Status(StatusCode.AlreadyExists, e.Message));
            }
        }

        [Authorize(Policy = nameof(UpdateHelperTableEntityWithHistory))]
        public override Task<ListOfHelperTableEntity> UpdateHelperTableEntityWithHistory(UpdateHelperTableEntityWithHistoryRequest request, ServerCallContext context)
        {
            var entities = _helperTableUseCase.UpdateHelperTableEntityWithHistory(GetHelperTableDiffs(request.HelperTableEntityDiffs));
            return Task.FromResult(GetListOfHelperTableEntitiesFromHelperTableEntities(entities));
        }

        private ListOfHelperTableEntity GetListOfHelperTableEntitiesFromHelperTableEntities(List<HelperTableEntity> entities)
        {
            var listOfHelperTableEntity = new ListOfHelperTableEntity();
            foreach (var entity in entities)
            {
                listOfHelperTableEntity.HelperTableEntities.Add(_mapper.DirectPropertyMapping(entity));
            }

            return listOfHelperTableEntity;
        }

        private List<HelperTableEntityDiff> GetHelperTableDiffs(ListOfHelperTableEntityDiff helperTableEntityDiffs)
        {
            var diffs = new List<HelperTableEntityDiff>();

            foreach (var diff in helperTableEntityDiffs.HelperTableEntityDiff)
            {
                var user = new Server.Core.Entities.User() { UserId = new UserId(diff.UserId) };
                diffs.Add(new HelperTableEntityDiff(user, new HistoryComment(diff.Comment),
                    GetHelperTableEntityFromHelperTableEntityDto(diff.OldHelperTableEntity),
                    GetHelperTableEntityFromHelperTableEntityDto(diff.NewHelperTableEntity)));
            }

            return diffs;
        }

        private HelperTableEntity GetHelperTableEntityFromHelperTableEntityDto(DtoTypes.HelperTableEntity entityDto)
        {
            if (entityDto == null)
            {
                return null;
            }

            return _mapper.DirectPropertyMapping(entityDto);
        }
    }
}
