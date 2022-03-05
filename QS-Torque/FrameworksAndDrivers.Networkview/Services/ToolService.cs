using System.Collections.Generic;
using System.Threading.Tasks;
using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Google.Protobuf;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using ToolService;
using Tool = DtoTypes.Tool;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class ToolService : global::ToolService.ToolService.ToolServiceBase
    {
        private readonly ILogger<ToolService> _logger;
        private readonly IToolUseCase _toolUseCase;
        private readonly Mapper _mapper = new Mapper();

        public ToolService(ILogger<ToolService> logger, IToolUseCase toolUseCase)
        {
            _logger = logger;
            _toolUseCase = toolUseCase;
        }

        [Authorize(Policy = nameof(LoadTools))]
        public override Task<ListOfTools> LoadTools(LoadToolsRequest request, ServerCallContext context)
        {
            var entities = _toolUseCase.LoadTools(request.Index, request.Size);
            var listOfEntities = new ListOfTools();
            entities.ForEach(s => listOfEntities.Tools.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(GetToolById))]
        public override Task<Tool> GetToolById(Long request, ServerCallContext context)
        {
            var tool = _toolUseCase.GetToolById(new ToolId(request.Value));
            return Task.FromResult(_mapper.DirectPropertyMapping(tool));
        }

        [Authorize(Policy = nameof(InsertToolsWithHistory))]
        public override Task<ListOfTools> InsertToolsWithHistory(InsertToolsWithHistoryRequest request, ServerCallContext context)
        {
            var entities = _toolUseCase.InsertToolsWithHistory(GetToolDiffs(request.ToolDiffs), request.ReturnList);
            return Task.FromResult(GetListOfToolsFromTools(entities));
        }

        [Authorize(Policy = nameof(UpdateToolsWithHistory))]
        public override Task<ListOfTools> UpdateToolsWithHistory(UpdateToolsWithHistoryRequest request, ServerCallContext context)
        {
            var entities = _toolUseCase.UpdateToolsWithHistory(GetToolDiffs(request.ToolDiffs));
            return Task.FromResult(GetListOfToolsFromTools(entities));
        }

        [Authorize(Policy = nameof(GetLocationToolAssignmentLinkForTool))]
        public override Task<ListOfLocationToolAssignmentReferenceLink> GetLocationToolAssignmentLinkForTool(Long request, ServerCallContext context)
        {
            var entities = _toolUseCase.GetLocationToolAssignmentLinkForTool(new ToolId(request.Value));
            var listOfEntities = new ListOfLocationToolAssignmentReferenceLink();
            entities.ForEach(s => listOfEntities.Links.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(IsInventoryNumberUnique))]
        public override Task<Bool> IsInventoryNumberUnique(String request, ServerCallContext context)
        {
            var result = _toolUseCase.IsInventoryNumberUnique(request.Value);
            return Task.FromResult(new Bool() { Value = result });
        }

        [Authorize(Policy = nameof(IsSerialNumberUnique))]
        public override Task<Bool> IsSerialNumberUnique(String request, ServerCallContext context)
        {
            var result = _toolUseCase.IsSerialNumberUnique(request.Value);
            return Task.FromResult(new Bool() { Value = result });
        }

        [Authorize(Policy = nameof(LoadPictureForTool))]
        public override Task<LoadPictureForToolResponse> LoadPictureForTool(LoadPictureForToolRequest request, ServerCallContext context)
        {
            var result = new LoadPictureForToolResponse();
            var picture = _toolUseCase.LoadPictureForTool(new ToolId(request.ToolId), request.FileType);
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

        [Authorize(Policy = nameof(IsSerialNumberUnique))]
        public override Task<String> GetToolComment(Long request, ServerCallContext context)
        {
            return Task.FromResult(new String()
                { Value = _toolUseCase.GetToolComment(new ToolId(request.Value)) });
        }

        [Authorize(Policy = nameof(LoadToolsForModel))]
        public override Task<ListOfTools> LoadToolsForModel(Long request, ServerCallContext context)
        {
            var entities = _toolUseCase.LoadToolsForModel(new ToolModelId(request.Value));
            var listOfEntities = new ListOfTools();
            entities.ForEach(s => listOfEntities.Tools.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(LoadModelsWithAtLeasOneTool))]
        public override Task<ListOfToolModel> LoadModelsWithAtLeasOneTool(NoParams request, ServerCallContext context)
        {
            var entities = _toolUseCase.LoadModelsWithAtLeasOneTool();
            var listOfEntities = new ListOfToolModel();
            entities.ForEach(s => listOfEntities.ToolModels.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        private ListOfTools GetListOfToolsFromTools(List<Server.Core.Entities.Tool> entities)
        {
            var listOfEntities = new ListOfTools();
            foreach (var entity in entities)
            {
                listOfEntities.Tools.Add(_mapper.DirectPropertyMapping(entity));
            }

            return listOfEntities;
        }

        private List<Server.Core.Diffs.ToolDiff> GetToolDiffs(ListOfToolDiffs toolDiffs)
        {
            var diffs = new List<Server.Core.Diffs.ToolDiff>();

            foreach (var diff in toolDiffs.ToolDiffs)
            {
                var user = new Server.Core.Entities.User() { UserId = new UserId(diff.UserId) };
                diffs.Add(new Server.Core.Diffs.ToolDiff(user, new HistoryComment(diff.Comment),
                    GetToolFromToolDto(diff.OldTool),
                    GetToolFromToolDto(diff.NewTool)));
            }

            return diffs;
        }

        private Server.Core.Entities.Tool GetToolFromToolDto(DtoTypes.Tool entityDto)
        {
            if (entityDto == null)
            {
                return null;
            }

            return _mapper.DirectPropertyMapping(entityDto);
        }
    }
}